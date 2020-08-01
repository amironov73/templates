cmake_minimum_required(VERSION 2.8)
project(WindowedApplication)

set(CMAKE_CXX_STANDARD 11)

add_executable(WindowedApplication WIN32
        main.cpp main.rc resource.h)