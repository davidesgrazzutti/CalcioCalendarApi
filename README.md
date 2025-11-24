# CalcioCalendarApi ⚽

API minimale in **.NET 8** che espone un calendario di partite di Serie
A (dati di esempio/fittizi) per essere consumato da applicazioni
front-end, come l'app **Calcio Calendar** in React Native / Expo.

I dati sono attualmente in memoria (lista statica), senza database, e
includono più giornate del campionato.

## 🛠 Stack Tecnologico

-   .NET 8
-   ASP.NET Core Minimal API
-   C#
-   Docker
-   Render.com

## 🚀 Avvio in locale

``` bash
git clone https://github.com/davidesgrazzutti/CalcioCalendarApi.git
cd CalcioCalendarApi
dotnet restore
dotnet run
```

## 🌐 Endpoints

### GET /api/fixtures

Restituisce tutte le partite.

### GET /api/fixtures/{matchday}

Restituisce le partite della giornata richiesta.

