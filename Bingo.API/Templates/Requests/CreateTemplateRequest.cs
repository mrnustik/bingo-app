namespace Bingo.API.Templates.Requests;

public record CreateTemplateRequest(
    string Name,
    IReadOnlyCollection<string> Options);