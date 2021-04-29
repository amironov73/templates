### FullMeat

Программа, демонстрирующая "полный фарш для кровавого энтерпрайза" на платформе .NET 5:

* Microsoft.Extensions.DependencyInjection;
* Microsoft.Extensions.Hosting;
* Microsoft.Extensions.Logging;  
* Microsoft.Extensions.Options;
* System.CommandLine.

Программа конфигурирует и запускает некий сервис по приготовлению мясных блюд. Часть информации о блюде он берет из командной строки (в частности, бренд), остальное - из файла `appsettings.json`.
