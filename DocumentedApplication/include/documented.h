#ifndef DOCUMENTED_H
#define DOCUMENTED_H

typedef struct
{
    char *name;
    int age;
} Person;

Person* create_person (char *name, int age);
void print_person (Person *person);

int compute_sum (int arg1, int arg2);
int compute_product (int arg1, int arg2);

#endif
