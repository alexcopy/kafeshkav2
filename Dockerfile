# Используем .NET SDK как базовый образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Копируем файлы проекта для воспроизводимости кэша в случае изменения только файла *.csproj
COPY KafeshkaV2/KafeshkaV2.csproj .

# Восстанавливаем зависимости
RUN dotnet restore

COPY KafeshkaV2/. .
RUN rm -f appsettings.json
COPY KafeshkaV2/appsettings_docker.json appsettings.json
RUN dotnet build -c Release

# Публикация приложения
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Используем .NET runtime как базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Копируем файлы из папки publish в образ
COPY --from=publish /app/publish .



ENV ASPNETCORE_ENVIRONMENT Production


# Указываем порт для API
EXPOSE 8080


ENTRYPOINT ["sh", "-c", "./KafeshkaV2 && dotnet ef database update"]
# Запускаем приложение
