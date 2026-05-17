# ============================================================
# Dockerfile для CalculatorApp.Api
# .NET 10.0 | Многоэтапная сборка
# ============================================================

# ---------- ЭТАП 1: SDK (сборка) ----------
# Используем .NET 10 SDK для компиляции проекта
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копируем файлы проектов для восстановления зависимостей
# Это кэшируется Docker-ом — повторный билд быстрее
COPY ["CalculatorApp.Api/CalculatorApp.Api.csproj", "CalculatorApp.Api/"]
COPY ["CalculatorApp.Core/CalculatorApp.Core.csproj", "CalculatorApp.Core/"]
COPY ["nuget.config", "."]

# Восстанавливаем NuGet-пакеты
RUN dotnet restore "CalculatorApp.Api/CalculatorApp.Api.csproj"

# Копируем весь исходный код
COPY . .

# Собираем проект в Release
WORKDIR "/src/CalculatorApp.Api"
RUN dotnet build "CalculatorApp.Api.csproj" -c Release -o /app/build

# ---------- ЭТАП 2: Публикация ----------
FROM build AS publish
# Публикуем приложение (оптимизированная версия)
RUN dotnet publish "CalculatorApp.Api.csproj" -c Release -o /app/publish

# ---------- ЭТАП 3: Финальный образ (runtime) ----------
# Используем лёгкий ASP.NET runtime (без SDK)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Открываем порты для HTTP и HTTPS
EXPOSE 80
EXPOSE 443

# Копируем опубликованное приложение из этапа publish
COPY --from=publish /app/publish .

# Запускаем приложение
ENTRYPOINT ["dotnet", "CalculatorApp.Api.dll"]