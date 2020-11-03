#include <stdlib.h>
#include <stdio.h>
#include "documented.h"

/**
 * @file person.c
 *
 * @brief
 * Функции для персоны.
 *
 * @details
 * Содержит функции для создания и вывода на консоль сведений о некой персоне.
 *
 * @struct Person
 *
 * @brief
 * Сведения о некой персоне.
 *
 * @details
 * Фамилия, имя, отчество и возраст некой персоны.
 *
 * @var Person::name
 * @brief Фамилия, имя и отчество некой персоны.
 * @details
 * Может также содержать просто имя персоны.
 *
 * @var Person::age
 * @brief Возраст персоны.
 * @details
 * Учитываются только полные (наступившие) годы.
 *
 */

/**
 * Создание и инициализация структуры.
 *
 * @param name Имя.
 * @param age Возраст.
 * @return Указатель на созданную структуру.
 */
Person* create_person (char *name, int age)
{
    Person *result = calloc (1, sizeof (*result));

    result->name = name;
    result->age = age;

    return result;
}

/**
 * Вывод на консоль сведений из структуры.
 *
 * @param person Указатель на структуру.
 */
void print_person (Person *person)
{
    printf ("%s of age %d\n", person->name, person->age);
}
