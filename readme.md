# Тестовое задание для EffectiveMobile

## Стек

* Платформа разработки - .NET 8 
* REST API - ASP.NET Core 8
* ORM - SQLKata
* Миграции - EF8

## Описани

Конечная точка для фильтрации: **http://localhost:5080/api/Order?_cityDistrict=""&_firstDeliveryDateTime=""**  
Валидные входные данные: _cityDistrict - может быть "Юг" "Север" "Центр", _firstDeliveryDateTime - от 2024-10-24 до 2024-10-25  
Для обработки ошибок используется middleware, при невалидном запросе выдаёт статускод с описанием ошибки.  
Файл с результатом выборки хранится по пути "EffectiveMobile\EffectiveMobile.API\bin\filtered_orders.txt", в нём находится результат последнего запроса.
Дамп базы - dump-EffectiveMobile. В нём создал 1000 тестов записей. 

