using FluentValidation;
using Guests.ViewModels;
using HotelSystem.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Guests.Validators
{
    public class GuestViewViewModelValidator : AbstractValidator<GuestViewViewModel>
    {
        private Regex _onlyAlphaNumericRegex = new Regex(ValidationConstants.OnlyAlphaNumericCharactersRegex);

        public GuestViewViewModelValidator()
        {
            RuleFor(viewModel => viewModel.FilterString)
                .Matches(_onlyAlphaNumericRegex)
                .WithMessage(ErrorStrings.OnlyAlphaNumericCharacters);
        }
    }
}
