#include <stdlib.h>
#include <stdio.h>
#include "documented.h"

/**
 * @mainpage
 * @brief
 * Пример приложения, документированного с помощью Doxygen.
 *
 * @image html kitten.jpg "Картинка с милым котенком" width=200px
 *
 * @details
 * Это приложение не выполняет никаких полезных функций.
 * Оно служит примером того, как можно документировать
 * функции и структуры с помощью Doxygen.
 *
 * Просто создайте документацию и наслаждайтесь ею!
 */

/**
 * @file main.c
 * @brief Содержит точку входа в программу.
 *
 * @def FIRST_ARG
 * @brief Первый аргумент.
 *
 * @def SECOND_ARG
 * @brief Второй аргумент.
 */

#define FIRST_ARG  10
#define SECOND_ARG 15

/**
 * @private
 */
void some_calculations (void)
{
    int arg1 = FIRST_ARG, arg2 = SECOND_ARG;

    printf ("%d + %d = %d\n", arg1, arg2, compute_sum (arg1, arg2));
    printf ("%d * %d = %d\n", arg1, arg2, compute_product (arg1, arg2));
}

/**
 * @private
 */
void person_manipulations (void)
{
    Person *person = create_person ("Alexey", 47);
    print_person (person);
    free (person);
}

/**
 * Точка входа в программу.
 * @return Результат выполнения программы.
 */
int main ()
{
    some_calculations ();
    person_manipulations ();

    return 0;
}
