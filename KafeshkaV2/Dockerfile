# Используем .NET SDK как базовый образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Копируем файлы проекта для воспроизводимости кэша в случае изменения только файла *.csproj
COPY KafeshkaV2.csproj .

# Восстанавливаем зависимости
RUN dotnet restore

# Копируем все остальные файлы и выполняем сборку
COPY . .
RUN dotnet build -c Release

# Публикация приложения
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Используем .NET runtime как базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Копируем файлы из папки publish в образ
COPY --from=publish /app/publish .

# Указываем порт для API
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["./KafeshkaV2"]
