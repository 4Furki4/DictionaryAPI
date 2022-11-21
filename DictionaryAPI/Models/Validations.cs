using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Models.ViewModels;
using FluentValidation;

namespace DictionaryAPI.Models
{
    public class Validations
    {
        public class WordViewModelValidation : AbstractValidator<WordViewModel>
        {
            public WordViewModelValidation()
            {
                RuleFor(w => w.Name)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(40);
                RuleFor(w => w.Definitions).NotEmpty();
            }
        }

        public class DefinitionViewModelValidation : AbstractValidator<DefinitionViewModel>
        {
            public DefinitionViewModelValidation()
            {
                RuleFor(d => d.Definition)
                    .NotEmpty()
                    .NotNull()
                    .MinimumLength(2)
                    .MaximumLength(40);
                RuleFor(d => d.DefinitionType)
                    .NotEmpty()
                    .NotNull()
                    .MinimumLength(2)
                    .MaximumLength(40);
            }
        }

        public class WordValidation : AbstractValidator<Word>
        {
            public WordValidation()
            {
                RuleFor(w => w.Name)
                    .NotNull()
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(40);
            }
        }
        public class DefinitionsValidation : AbstractValidator<Definition>
        {
            public DefinitionsValidation()
            {
                RuleFor(d => d.WordDefinition)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(40);
                RuleFor(d => d.DefinitionType)
                    .NotEmpty()
                    .NotNull()
                    .MinimumLength(2)
                    .MaximumLength(40);
            }
        }
    }
}
