using CalcioCalendarApi;
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
// DATI (finte giornate 12 e 13)
// ----------------------------
var fixtures = new List<MatchDto>
{
    new("2025-26-G12-CAG-GEN", 12, "2025-11-22", "15:00", "Cagliari", "Genoa", "Unipol Domus", "Cagliari"),
    new("2025-26-G12-UDI-BOL", 12, "2025-11-22", "15:00", "Udinese", "Bologna", "Bluenergy Stadium", "Udine"),
    new("2025-26-G12-FIO-JUV", 12, "2025-11-22", "18:00", "Fiorentina", "Juventus", "Stadio Artemio Franchi", "Firenze"),
    new("2025-26-G12-NAP-ATA", 12, "2025-11-22", "20:45", "Napoli", "Atalanta", "Stadio Diego Armando Maradona", "Napoli"),
    new("2025-26-G12-VER-PAR", 12, "2025-11-23", "12:30", "Hellas Verona", "Parma", "Stadio Marcantonio Bentegodi", "Verona"),
    new("2025-26-G12-CRE-ROM", 12, "2025-11-23", "15:00", "Cremonese", "Roma", "Stadio Giovanni Zini", "Cremona"),
    new("2025-26-G12-LAZ-LEC", 12, "2025-11-23", "18:00", "Lazio", "Lecce", "Stadio Olimpico", "Roma"),
    new("2025-26-G12-INT-MIL", 12, "2025-11-23", "20:45", "Inter", "AC Milan", "Stadio Giuseppe Meazza", "Milano"),
    new("2025-26-G12-TOR-COM", 12, "2025-11-24", "18:30", "Torino", "Como", "Stadio Olimpico Grande Torino", "Torino"),
    new("2025-26-G12-SAS-PIS", 12, "2025-11-24", "20:45", "Sassuolo", "Pisa", "MAPEI Stadium - Città del Tricolore", "Reggio Emilia"),

    new("2025-26-G13-PIS-TOR", 13, "2025-11-29", "12:30", "Pisa", "Torino", "Arena Garibaldi - Stadio Romeo Anconetani", "Pisa"),
    new("2025-26-G13-PAR-CAG", 13, "2025-11-29", "15:00", "Parma", "Cagliari", "Stadio Ennio Tardini", "Parma"),
    new("2025-26-G13-BOL-FIO", 13, "2025-11-29", "18:00", "Bologna", "Fiorentina", "Stadio Renato Dall'Ara", "Bologna"),
    new("2025-26-G13-JUV-NAP", 13, "2025-11-29", "20:45", "Juventus", "Napoli", "Allianz Stadium", "Torino"),
    new("2025-26-G13-GEN-UDI", 13, "2025-11-30", "12:30", "Genoa", "Udinese", "Stadio Luigi Ferraris", "Genova"),
    new("2025-26-G13-ROM-VER", 13, "2025-11-30", "15:00", "Roma", "Hellas Verona", "Stadio Olimpico", "Roma"),
    new("2025-26-G13-ATA-INT", 13, "2025-11-30", "18:00", "Atalanta", "Inter", "Gewiss Stadium", "Bergamo"),
    new("2025-26-G13-MIL-LAZ", 13, "2025-11-30", "20:45", "AC Milan", "Lazio", "Stadio Giuseppe Meazza", "Milano"),
    new("2025-26-G13-LEC-CRE", 13, "2025-12-01", "18:30", "Lecce", "Cremonese", "Stadio Via del Mare", "Lecce"),
    new("2025-26-G13-COM-SAS", 13, "2025-12-01", "20:45", "Como", "Sassuolo", "Stadio Giuseppe Sinigaglia", "Como")
};

// ----------------------------
// ENDPOINTS
// ----------------------------

// tutte le partite
app.MapGet("/api/fixtures", () =>
    Results.Ok(fixtures));

// solo una giornata (es: /api/fixtures/12)
app.MapGet("/api/fixtures/{matchday:int}", (int matchday) =>
{
    var result = fixtures.Where(f => f.Matchday == matchday).ToList();
    return result.Count == 0 ? Results.NotFound() : Results.Ok(result);
});

app.Run();
