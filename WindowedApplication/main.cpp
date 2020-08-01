#include <windows.h>
#include <CommCtrl.h>
#include <string>

#include "resource.h"

// подключаем библиотеку современных контролов
#pragma comment (lib, "ComCtl32.lib")

// включаем поддержку тем
#if defined _M_IX86
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='x86' publicKeyToken='6595b64144ccf1df' language='*'\"")
#elif defined _M_X64
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='amd64' publicKeyToken='6595b64144ccf1df' language='*'\"")
#else
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#endif

static const char CLASS_NAME[] = "Sample Window Class";
static HWND mainWindow;
std::string mainText ("Hello, world!");;

static void MyDrawProc (HWND hwnd)
{
    ::PAINTSTRUCT ps;
    HDC hdc = ::BeginPaint (hwnd, &ps);

    ::FillRect (hdc, &ps.rcPaint, (HBRUSH) (COLOR_WINDOW+1));
    ::DrawText (hdc, mainText.data(), mainText.size(), &ps.rcPaint,
              DT_NOPREFIX|DT_CENTER|DT_VCENTER|DT_SINGLELINE);

    ::EndPaint (hwnd, &ps);
}

static LRESULT CALLBACK MyWindowProc (HWND hwnd, UINT uMsg,
                                      WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
        case WM_DESTROY:
            ::PostQuitMessage(0);
            return 0;

        case WM_PAINT:
            MyDrawProc (hwnd);
            return 0;

        default:
            break;
    }

    return DefWindowProc (hwnd, uMsg, wParam, lParam);
}

static void registerClass (HINSTANCE hInstance)
{
    ::WNDCLASS wc {};
    wc.lpfnWndProc   = MyWindowProc;
    wc.hInstance     = hInstance;
    wc.lpszClassName = CLASS_NAME;

    ::RegisterClass (&wc);
}

static void createWindow (HINSTANCE hInstance)
{
    DWORD style = WS_CAPTION|WS_SYSMENU;
    DWORD styleEx = 0; // WS_EX_TOOLWINDOW;

    mainWindow = ::CreateWindowEx
        (
            styleEx,              // Optional window styles
            CLASS_NAME,           // Window class
            "Learn WinAPI",       // Window text
            style,                // Window style

            // Size and position
            CW_USEDEFAULT, CW_USEDEFAULT, 250, 170,

            nullptr,       // Parent window
            nullptr,       // Menu
            hInstance,     // Instance handle
            nullptr        // Additional application data
        );

    // загружаем иконку диалога из ресурсов
    HICON hicon = (HICON) ::LoadImage
        (
            ::GetModuleHandle (nullptr),
            MAKEINTRESOURCE (IDI_MAIN_ICON),
            IMAGE_ICON,
            0,
            0,
            LR_DEFAULTCOLOR | LR_DEFAULTSIZE
        );
    ::SendMessage
        (
            mainWindow,
            WM_SETICON,
            ICON_BIG,
            (LPARAM) hicon
        );
    ::SendMessage
        (
            mainWindow,
            WM_SETICON,
            ICON_SMALL,
            (LPARAM) hicon
        );

}

static void messageLoop()
{
    ::MSG msg {};
    while (::GetMessage (&msg, nullptr, 0, 0))
    {
        ::TranslateMessage(&msg);
        ::DispatchMessage(&msg);
    }
}

int WINAPI WinMain
    (
        HINSTANCE hInstance,
        HINSTANCE,
        LPSTR pCmdLine,
        int nCmdShow
    )
{
    ::InitCommonControls();

    registerClass (hInstance);
    createWindow (hInstance);
    ::ShowWindow (mainWindow, nCmdShow);
    messageLoop();

    return 0;
}
