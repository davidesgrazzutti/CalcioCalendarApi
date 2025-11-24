using CalcioCalendarApi;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// CORS (per permettere alla tua app Expo/React Native di chiamare l'API)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
});

// serializzazione JSON (se in futuro usi enum ecc.)
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseCors();

// ----------------------------
// CARICAMENTO DATI DA fixtures.json
// ----------------------------
var fixturesPath = Path.Combine(app.Environment.ContentRootPath, "fixtures.json");

List<MatchDto> fixtures;

if (File.Exists(fixturesPath))
{
    try
    {
        var json = File.ReadAllText(fixturesPath);

        fixtures = JsonSerializer.Deserialize<List<MatchDto>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        ) ?? new List<MatchDto>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Errore nel parsing di fixtures.json: {ex.Message}");
        fixtures = new List<MatchDto>();
    }
}
else
{
    Console.WriteLine($"ATTENZIONE: file {fixturesPath} non trovato. La lista fixtures sarà vuota.");
    fixtures = new List<MatchDto>();
}

// ----------------------------
// ENDPOINTS
// ----------------------------

// tutte le partite (calendario + risultati se presenti)
app.MapGet("/api/fixtures", () =>
    Results.Ok(fixtures));

// partite di una singola giornata (es: /api/fixtures/12)
app.MapGet("/api/fixtures/{matchday:int}", (int matchday) =>
{
    var result = fixtures.Where(f => f.Matchday == matchday).ToList();
    return result.Count == 0 ? Results.NotFound() : Results.Ok(result);
});

// solo partite con risultato (gol valorizzati)
app.MapGet("/api/results", () =>
{
    var results = fixtures
        .Where(f => f.HomeGoals.HasValue && f.AwayGoals.HasValue)
        .ToList();

    return Results.Ok(results);
});

// risultati di una singola giornata
app.MapGet("/api/results/{matchday:int}", (int matchday) =>
{
    var results = fixtures
        .Where(f => f.Matchday == matchday &&
                    f.HomeGoals.HasValue &&
                    f.AwayGoals.HasValue)
        .ToList();

    return results.Count == 0 ? Results.NotFound() : Results.Ok(results);
});

app.Run();
