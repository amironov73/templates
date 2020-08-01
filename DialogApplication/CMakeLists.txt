cmake_minimum_required(VERSION 2.8)
project(DialogApplication)

set(CMAKE_CXX_STANDARD 11)

add_executable(DialogApplication WIN32
        DialogApplication.cpp DialogApplication.rc resource.h)