# news-receiver-dotnetcore

Для запуска приложения, необходимо установить .NET Core 3.0: https://dotnet.microsoft.com/download/dotnet-core/3.0

Запуск: dotnet run dotnet run -p path\to\a\project\publishing_api.csproj -c Release --urls http://*:5000

Проверка: перейти в браузере на http://localhost:5000

Логин и пароль устанавливается в файле конфигурации проекта в секции "ps_credentials": appsettings.json

По умолчанию
	логин: your_login
	пароль: your_password