#include "documented.h"

/**
 * @file functions.c
 *
 * @brief Разнообразные вычисления.
 *
 * @details
 * Модуль содержит функции для различных вычислений,
 * например, суммы или произведения двух чисел.
 *
 * @code
 * int arg1 = 1, arg2 = 2;
 * int sum = compute_sum (arg1, arg2);
 * int product = compute_product (arg1, arg2);
 *
 * printf ("Sum is %d, product is %d\n", sum, product);
 * @endcode
 */

/**
 * Вычисление суммы двух целых чисел.
 *
 * @param arg1 Первое число.
 * @param arg2 Второе число.
 * @return Сумма.
 */
int compute_sum (int arg1, int arg2)
{
    return arg1 + arg2;
}

/**
 * Вычисление произведения двух целых чисел.
 *
 * @param arg1 Первое число.
 * @param arg2 Второе число.
 * @return Произведение.
 */
int compute_product (int arg1, int arg2)
{
    return arg1 * arg2;
}
