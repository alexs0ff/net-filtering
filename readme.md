## Описание
Репозиторий для демонстрации использования нескольких библиотек для фильтрации, пагинации и сортировки данных со стороны backend на платформе .NET 8. Использованы такие библиотеки как [OData](https://github.com/OData/odata.net), 
[Sieve](https://github.com/Biarity/Sieve) и своя реализация основанная на некотором толковании паттерна [спецификация](https://habr.com/ru/articles/325280/)

## Как использовать
Проект полностью готов к использованию через docker-compose:
```
> docker-compose up
```

Далее необходимо перейти по адресу `http://localhost:5179` и начать тестирование этих библиотек.
