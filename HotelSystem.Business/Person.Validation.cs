using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelSystem.Business
{
    public partial class Person
    {
        private Regex _onlyAlphaNumericRegex = new Regex(ValidationConstants.OnlyAlphaNumericCharactersRegex);
        private Regex _onlyNumericRegec = new Regex(ValidationConstants.OnlyNumericCharactersRegex);

        private void ValidateAge()
        {
            ClearError(nameof(Age));

            if (Age < ValidationConstants.MinAge || Age > ValidationConstants.MaxAge)
            {
                AddError(nameof(Age), ErrorStrings.InvalidAge);
            }
        }

        private void ValidatePhoneNumber()
        {
            ClearError(nameof(PhoneNumber));

            if (PhoneNumber.Length > ValidationConstants.GeneralMaxCharacterLimit)
            {
                AddError(PhoneNumber, ErrorStrings.OutsideGeneralMaxCharacterLimit);
            }
            else if (!_onlyNumericRegec.IsMatch(PhoneNumber))
            {
                AddError(PhoneNumber, ErrorStrings.OnlyNumericCharacters);
            }
        }

        private void ValidateText(ref string fieldReference, [CallerMemberName] string propertyName = null)
        {
            ClearError(propertyName);

            if (string.IsNullOrWhiteSpace(fieldReference))
            {
                AddError(propertyName, ErrorStrings.RequiredField);
            }
            else if (fieldReference.Length > ValidationConstants.GeneralMaxCharacterLimit)
            {
                AddError(propertyName, ErrorStrings.OutsideGeneralMaxCharacterLimit);
            }
            else if (!_onlyAlphaNumericRegex.IsMatch(fieldReference))
            {
                AddError(propertyName, ErrorStrings.OnlyAlphaNumericCharacters);
            }
        }
    }
}
