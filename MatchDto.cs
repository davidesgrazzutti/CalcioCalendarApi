namespace CalcioCalendarApi;

public record MatchDto(
    string Id,
    int Matchday,
    string Date,
    string Time,
    string HomeTeam,
    string AwayTeam,
    string Venue,
    string City
);
