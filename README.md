# fs-test
Тестовое  задание на создание файлового хранилища

## Запуск
1. В параметрах запуска `Properties/launchSettings.json` прописать данные подключения к postgresql. База будет создана автоматически. 
2. В `appsettings.json` / `appsettings.Development.json` прописать `StoragePath` куда будут сохраняться файлы (по умолчанию - /resources/ в корне с проектом).
3. Выполнить в корне с проектом команду `dotnet run`.
4. Открыть в браузере https://localhost:7286/swagger/index.html если не открылось автоматически.

Для загрузки файла через Swagger нужно нажать "Try it out". 