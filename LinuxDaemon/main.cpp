#include <iostream>
#include <string>
#include <csignal>
#include <sys/socket.h>
#include <sys/wait.h>
#include <netinet/in.h>
#include <unistd.h>
#include <future>

void handleSignal (int sig)
{
    if (sig == SIGINT) {
        std::cout << "SIGINT received" << std::endl;
        std::exit (0);
    }
}

void handleClient (int clientHandle)
{
    while (true) {
        char recvBuffer [1024];
        const auto recvSize =
                ::recv (clientHandle, recvBuffer, sizeof (recvBuffer), 0);
        if (recvSize == -1) {
            std::cerr << "recv() failed: " << std::endl;
            break;
        }
        if (recvSize == 0) {
            // end of the message
            break;
        }

        const auto sendSize = ::send (clientHandle, recvBuffer, recvSize, 0);
        if (sendSize != recvSize) {
            std::cerr << "send() failed: " << std::endl;
            std::exit (5);
        }
    }

    ::close (clientHandle);
}

void doServer()
{
    int listenHandle = ::socket (AF_INET, SOCK_STREAM, 0);
    if (listenHandle == -1) {
        std::cerr << "socket() failed" << std::endl;
        std::exit (1);
    }

    struct sockaddr_in localAddress { 0 };
    localAddress.sin_family = AF_INET;
    localAddress.sin_addr.s_addr = htonl (INADDR_ANY);
    localAddress.sin_port = htons (9889);

    auto returnCode = ::bind (listenHandle,
                              (struct sockaddr*) &localAddress,
                              sizeof (localAddress));
    if (returnCode == -1) {
        std::cerr << "bind() failed" << std::endl;
        ::close (listenHandle);
        std::exit (2);
    }

    returnCode = ::listen (listenHandle, 0);
    if (returnCode == -1) {
        std::cerr << "listen() failed" << std::endl;
        ::close (listenHandle);
        std::exit (3);
    }

    while (true) {
        auto clientHandle = ::accept (listenHandle, nullptr, nullptr);
        if (clientHandle == -1) {
            std::cerr << "accept() failed" << std::endl;
            ::close (listenHandle);
            std::exit (4);
        }

        std::thread workerThread ([=] { handleClient (clientHandle); });
        workerThread.detach();
    }
}

int main()
{
    ::signal (SIGINT, handleSignal);
    doServer();
    return 0;
}
