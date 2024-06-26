# Разработка приложения кондитерского завода (C#)
![image](https://github.com/MEAWWWS/Conditers/assets/114382568/2fe08f3c-1bf6-4fdb-b5cc-71cfa616befa)

Интерфейс приложения, справа ингридиенты на складе

![image](https://github.com/MEAWWWS/Conditers/assets/114382568/fdfad1a8-6e1a-4765-8b2a-ad235fab69d6)

Изменение кол-ва ингридиентов происходит при изменении данных и нажати на кнопку "Изменить"

![image](https://github.com/MEAWWWS/Conditers/assets/114382568/2830f202-4ff0-41d3-8b10-ef3e9c427dd1)

Подсчет происходит при нажатии на кнопку "Подсчитать кол-во конфет"

![image](https://github.com/MEAWWWS/Conditers/assets/114382568/4fd47242-702c-4109-9445-549a230bf022)

Подсчёт логистики происходит при нажатии на кнопку "Подсчитать логистику" и вводе данных

![image](https://github.com/MEAWWWS/Conditers/assets/114382568/9d5214ca-08bb-4fc7-bbc8-dd5e2a6916dc)

Результат юнит тестов

![image](https://github.com/MEAWWWS/Conditers/assets/114382568/327138de-5f4f-4fbf-a330-8012e66ab1c2)

Логи хранящиеся в .txt файле

## Функциональность

1. **Просмотр ингридиентов на складе**: Возможность видеть кол-во ингридиентов на складе.
2. **Изменение кол-ва ингридиентов на складе**: При нажатии на строку справа выводятся данные коотрые можно изменить.
3. **Обновление**: Обновление таблицы склада.
4. **Подсчёт кол-ва конфет**: Нужно нажать копку и произойдёт подсчёт который покажет кол-во конфет которые можно произвести.
5. **Подсчёт логистики**: Можно посчитать цену за доставку введя данные по расстоянию и цене за км.
6. **Логирование**: Все нажатия логируются в файл в проекте.

## Технические детали
- **Разработано на**: WPF
- **База данных**: SQLite с использованием Entity Framework
- **Тесты**: Unit тесты модуля dll MSTest
- **Подключение к dll**: Функционал из написанной библиотеки dll ConfectioneryFactoryDll

## Создание классов Garbage, User, UserJob, ConfectioneryFactory и DataBaseClass для связи с БД

``` C#
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectioneryFactoryDll;

namespace Conditer
{
    public class DataBaseClass : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserJob> UserJobs { get; set; } = null!;
        public DbSet<Garbage> Garbages { get; set; } = null!;
        private static DataBaseClass db;
        private DataBaseClass(){}
        public static DataBaseClass GetDatabase()
        {
            if (db == null)
            {
                db = new DataBaseClass();
            }
            return db;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ConditerDB.db;");
        }
    }
}

namespace ConfectioneryFactoryDll
{
    public class ConfectioneryFactory
    {
        /// <summary>
        /// Calculates how many candies can be produced from these materials
        /// </summary>
        /// <param name="cereals"></param>
        /// <param name="butter"></param>
        /// <param name="sugar"></param>
        /// <param name="cacao"></param>
        /// <param name="water"></param>
        /// <returns></returns>
        public static int CountPossibleProducts(double cereals, double butter, double sugar, double cacao, double water)
        {
            int candiesPerBatch = 6;

            double batchesFromManna = cereals / 130;
            double batchesFromButter = butter / 60;
            double batchesFromSugar = sugar / 60;
            double batchesFromCacao = cacao / 3.5;
            double batchesFromWater = water / 180;

            int possibleBatches = (int)Math.Min(batchesFromManna, Math.Min(batchesFromButter, Math.Min(batchesFromSugar, Math.Min(batchesFromCacao, batchesFromWater))));

            int totalCandies = possibleBatches * candiesPerBatch;
            return totalCandies;
        }
        /// <summary>
        /// Calculates the cost of logistics
        /// </summary>
        /// <param name="distanceToMagazine"></param>
        /// <param name="kilometrPrice"></param>
        /// <returns></returns>
        public static double CountLogisticPrice(double distanceToMagazine, double kilometrPrice)
        {
            return kilometrPrice * distanceToMagazine;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFactoryDll
{
    public class Garbage
    {
        [Key]
        public int Id { get; set; }
        public double CerealsCount { get; set; }
        public double ButterCount { get; set; }
        public double SugarCount { get; set; }
        public double CacaoCount { get; set; }
        public double WaterCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFactoryDll
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFactoryDll
{
    public class UserJob
    {
        [Key]
        public int Id { get; set; }
        public string JobName { get; set; }
    }
}

## Код Unit тестов

``` C#
using ConfectioneryFactoryDll;
namespace TestConfectioneryFactoryDll
{
    [TestClass]
    public class UnitTestConfectioneryFactory
    {
        [TestMethod]
        public void TestCountLogisticPrice()
        {
            double km = 6;
            double price = 150;

            double reality = ConfectioneryFactory.CountLogisticPrice(km, price);
            double expectation = 900;

            Assert.AreEqual(expectation, reality);
        }
        [TestMethod]
        public void TestCountPossibleProducts()
        {
            double cereals = 130;
            double butter = 60;
            double sugar = 60;
            double cacao = 3.5;
            double water = 180;

            double reality = ConfectioneryFactory.CountPossibleProducts(cereals, butter, sugar, cacao, water);
            double expectation = 6;

            Assert.AreEqual(expectation, reality);
        }
    }
}
```

## Код создания таблиц SQLite

``` SQLite
CREATE TABLE "Garbages" (
	"Id"	INTEGER,
	"CerealsCount"	REAL NOT NULL,
	"ButterCount"	REAL NOT NULL,
	"SugarCount"	REAL NOT NULL,
	"CacaoCount"	REAL NOT NULL,
	"WaterCount"	REAL NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
)

CREATE TABLE "UserJobs" (
	"Id"	INTEGER NOT NULL,
	"JobName"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
)

CREATE TABLE "Users" (
	"Id"	INTEGER,
	"Name"	TEXT NOT NULL,
	"Surname"	TEXT NOT NULL,
	"Patronymic"	TEXT NOT NULL,
	"JobId"	INTEGER NOT NULL,
	"Login"	TEXT NOT NULL UNIQUE,
	"Password"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("JobId") REFERENCES "UserJobs"("Id")
)
Garbages - ингридиенты, UserJobs - роли, Users - Пользователи
```

## Автор программы
### Зимин.М
