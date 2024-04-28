using System.Net.Http.Json;
using Bingo.API.Templates.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Bingo.API.IntegrationTests;

public class GetAllTemplateTests
{
    [Fact]
    public async Task GetAllTemplates_WithNoSeededData_ReturnsEmptyArray()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>();
        var httpClient = factory.CreateClient();
        
        // Act
        var response = await httpClient.GetAsync("/templates/");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var templates = await response.Content
            .ReadFromJsonAsync<IReadOnlyCollection<TemplateResponse>>();
        templates
            .Should()
            .BeEmpty();
    }
}