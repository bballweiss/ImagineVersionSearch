using FluentValidation;
using Newtonsoft.Json;
using System;

namespace ImagineSoftware.Models
{
    public class SearchRequest
    {
        [JsonProperty("input")]
        public string Input { get; set; }
    }

    public class SearchRequestValidator : AbstractValidator<SearchRequest>
    {
        public SearchRequestValidator()
        {
            RuleFor(searchRequest => searchRequest).NotEmpty();
            RuleFor(searchRequest => searchRequest.Input).NotEmpty();
            RuleFor(searchRequest => searchRequest.Input).NotEqual("undefined").WithMessage("Input must not be empty");
            RuleFor(searchRequest => searchRequest.Input).Must(input => input.Split('.').Length <= 3)
                .WithMessage("Too many version pieces.");
            RuleFor(searchRequest => searchRequest.Input).Custom((input, context) => {
                foreach (var versionPiece in input.Split('.'))
                {
                    if(!int.TryParse(versionPiece, out int garbage))
                    {
                        context.AddFailure($"{input} is not a valid version.");
                        break;
                    }
                }
                });
        }
    }
}
