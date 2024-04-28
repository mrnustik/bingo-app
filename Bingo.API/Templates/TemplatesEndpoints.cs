using System.Net;
using Bingo.API.Templates.Requests;
using Bingo.API.Templates.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bingo.API.Templates;

public static class TemplatesEndpoints
{
    public static WebApplication MapTemplatesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("templates");
        group.MapPost("/", HandlePostNewTemplate);
        group.MapGet("/", HandleGetTemplates);
        group.MapGet("/{name}", HandleGetTemplateByName);
        return app;
    }

    private static async Task<Ok<IReadOnlyCollection<TemplateResponse>>> HandleGetTemplates(CancellationToken ct)
    {
        var response = Array.Empty<TemplateResponse>();
        await Task.Delay(500, ct);
        return TypedResults.Ok<IReadOnlyCollection<TemplateResponse>>(response);
    }

    private static async Task<Results<Ok<TemplateResponse>, NotFound>> HandleGetTemplateByName(string name, CancellationToken ct)
    {
        var response = new TemplateResponse(
            name,
            []);
        await Task.Delay(500, ct);
        return TypedResults.Ok(response);
    }
    
    private static async Task<Results<Created<TemplateResponse>, ValidationProblem>> HandlePostNewTemplate(
        CreateTemplateRequest request, 
        IValidator<CreateTemplateRequest> validator,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }
        await Task.Delay(500, ct);
        var response = new TemplateResponse(
            request.Name,
            request.Options);
        return TypedResults.Created($"/templates/{response.Name}", response);
    } 
}