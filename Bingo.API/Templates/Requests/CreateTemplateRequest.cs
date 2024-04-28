using FluentValidation;

namespace Bingo.API.Templates.Requests;

public record CreateTemplateRequest(
    string Name,
    IReadOnlyCollection<string> Options)
{
    public class Validator : AbstractValidator<CreateTemplateRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Options)
                .NotNull();
        }
    }
};