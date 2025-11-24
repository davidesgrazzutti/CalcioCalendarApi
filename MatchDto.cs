namespace CalcioCalendarApi;

public class MatchDto
{
    public string Id { get; set; } = default!;
    public int Matchday { get; set; }

    // stringhe perché nel tuo JSON sono "2025-11-22" / "15:00"
    public string Date { get; set; } = default!;
    public string Time { get; set; } = default!;

    public string HomeTeam { get; set; } = default!;
    public string AwayTeam { get; set; } = default!;

    // NB: uso "Stadium" perché nel tuo JSON hai "stadium"
    public string Stadium { get; set; } = default!;
    public string City { get; set; } = default!;

    // RISULTATI (opzionali)
    public int? HomeGoals { get; set; }   // null = non ancora giocata
    public int? AwayGoals { get; set; }
    public string? Status { get; set; }   // "NS", "FT", "LIVE", ecc.
}
