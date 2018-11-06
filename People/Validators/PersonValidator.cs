using FluentValidation;
using HotelSystem.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace People.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        private Regex _onlyAlphaNumericRegex = new Regex(ValidationConstants.OnlyAlphaNumericCharactersRegex);
        private Regex _onlyAlphaRegex = new Regex(ValidationConstants.OnlyAlphaCharactersRegex);
        private Regex _onlyNumericRegex = new Regex(ValidationConstants.OnlyNumericCharactersRegex);

        public PersonValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty()
                .WithMessage(ErrorStrings.RequiredField)
                .MaximumLength(ValidationConstants.GeneralMaxCharacterLimit)
                .WithMessage(ErrorStrings.OutsideGeneralMaxCharacterLimit)
                .Matches(_onlyAlphaRegex)
                .WithMessage(ErrorStrings.OnlyAlphaCharacters);

            RuleFor(person => person.Age)
                .NotEmpty()
                .WithMessage(ErrorStrings.RequiredField)
                .ExclusiveBetween(ValidationConstants.MinAge, ValidationConstants.MaxAge)
                .WithMessage(ErrorStrings.InvalidAge);

            RuleFor(person => person.AddressLineOne)
                .NotEmpty()
                .WithMessage(ErrorStrings.RequiredField)
                .Matches(_onlyAlphaNumericRegex)
                .WithMessage(ErrorStrings.OnlyAlphaNumericCharacters)
                .MaximumLength(ValidationConstants.GeneralMaxCharacterLimit)
                .WithMessage(ErrorStrings.OutsideGeneralMaxCharacterLimit);

            RuleFor(person => person.AddressLineTwo)
                .MaximumLength(ValidationConstants.GeneralMaxCharacterLimit)
                .When(person => !String.IsNullOrEmpty(person.AddressLineTwo))
                .WithMessage(ErrorStrings.OutsideGeneralMaxCharacterLimit)
                .Matches(_onlyAlphaNumericRegex)
                .When(person => !String.IsNullOrEmpty(person.AddressLineTwo))
                .WithMessage(ErrorStrings.OnlyAlphaNumericCharacters);

            RuleFor(person => person.City)
                .NotEmpty()
                .WithMessage(ErrorStrings.RequiredField)
                .MaximumLength(ValidationConstants.GeneralMaxCharacterLimit)
                .WithMessage(ErrorStrings.OutsideGeneralMaxCharacterLimit)
                .Matches(_onlyAlphaRegex)
                .WithMessage(ErrorStrings.OnlyAlphaCharacters);

            RuleFor(person => person.PostCode)
                .NotEmpty()
                .WithMessage(ErrorStrings.RequiredField)
                .Matches(_onlyAlphaNumericRegex)
                .WithMessage(ErrorStrings.OnlyAlphaNumericCharacters)
                .MaximumLength(ValidationConstants.GeneralMaxCharacterLimit)
                .WithMessage(ErrorStrings.OutsideGeneralMaxCharacterLimit);

            RuleFor(person => person.PhoneNumber)
                .NotEmpty()
                .WithMessage(ErrorStrings.RequiredField)
                .Matches(_onlyNumericRegex)
                .WithMessage(ErrorStrings.OnlyNumericCharacters);

            RuleFor(person => person.CreditCardNumber)
                .Matches(_onlyNumericRegex)
                .When(person => !String.IsNullOrEmpty(person.CreditCardNumber))
                .WithMessage(ErrorStrings.OnlyNumericCharacters);
        }
    }
}
