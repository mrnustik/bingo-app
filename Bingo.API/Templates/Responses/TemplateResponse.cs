namespace Bingo.API.Templates.Responses;

public record TemplateResponse(
    string Name,
    IReadOnlyCollection<string> Options);