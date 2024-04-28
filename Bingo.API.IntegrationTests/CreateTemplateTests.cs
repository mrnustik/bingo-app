using System.Net;
using System.Net.Http.Json;
using Bingo.API.Templates.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Bingo.API.IntegrationTests;

public class CreateTemplateTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CreateTemplateTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateTemplate_WithMissingName_ReturnsValidationError()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new CreateTemplateRequest(
            string.Empty,
            Array.Empty<string>());
        var content = JsonContent.Create(request);

        // Act
        var response = await client.PostAsync("/templates/", content);
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task CreateTemplate_WithMissingOptions_ReturnsValidationError()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new CreateTemplateRequest(
            "Valid_Name",
            null!);
        var content = JsonContent.Create(request);

        // Act
        var response = await client.PostAsync("/templates/", content);
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task CreateTemplate_WithValidOptions_ReturnsCreated()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new CreateTemplateRequest(
            "Valid_Name",
            ["Options"]);
        var content = JsonContent.Create(request);

        // Act
        var response = await client.PostAsync("/templates/", content);
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);
    }
}