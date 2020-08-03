cmake_minimum_required(VERSION 2.8)
project(echo-server)

set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -pthread")

add_executable(echoServer echo-server.cpp)
install(TARGETS ${PROJECT_NAME} DESTINATION /var/local)
