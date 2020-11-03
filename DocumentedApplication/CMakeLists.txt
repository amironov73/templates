cmake_minimum_required(VERSION 3.17)
project(documented C)

set(CMAKE_C_STANDARD 90)

include_directories(${PROJECT_SOURCE_DIR}/include)
add_executable(documented src/main.c include/documented.h src/functions.c src/person.c)
