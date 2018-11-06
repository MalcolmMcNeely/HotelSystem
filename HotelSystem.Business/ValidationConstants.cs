using System.Text.RegularExpressions;

namespace HotelSystem.Business
{
    public class ValidationConstants
    {
        public const string OnlyAlphaNumericCharactersRegex = @"^[a-zA-Z0-9\s]*$";
        public const string OnlyAlphaCharactersRegex = @"^[a-zA-Z\s]*$";
        public const string OnlyNumericCharactersRegex = @"^[0-9\s]*$";

        public const int GeneralMaxCharacterLimit = 50;
        public const int MinAge = 0;
        public const int MaxAge = 130;

        public const decimal MinDecimal = -9999999.99M;
        public const decimal MaxDecimal = 9999999.99M;
    }
}
