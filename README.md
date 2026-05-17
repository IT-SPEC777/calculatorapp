# CalculatorApp

Веб-приложение калькулятора на **.NET 10** с настроенным **CI/CD** в **GitFlic**.

## Возможности

- REST API калькулятора (сложение, вычитание, умножение, деление, проверка простого числа)
- Модульные тесты (xUnit)
- Интеграционные тесты
- CI/CD пайплайн (сборка → тесты → линтеры → анализ → деплой)
- Healthcheck endpoint
- Swagger-документация
- Поддержка изолированной (air-gapped) среды

## Быстрый старт

### Требования

- .NET SDK 10.0
- Git
- Docker (опционально)

### Локальный запуск

```bash
# Клонировать репозиторий
git clone http://localhost:8080/project/root_admin/calculatorapp/
cd calculatorapp

# Восстановить зависимости
dotnet restore CalculatorApp.slnx

# Собрать проект
dotnet build CalculatorApp.slnx -c Release

# Запустить тесты
dotnet test CalculatorApp.slnx -c Release

# Запустить API
dotnet run --project CalculatorApp.Api --urls "http://0.0.0.0:5000"