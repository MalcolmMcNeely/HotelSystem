using System.Text.RegularExpressions;

namespace HotelSystem.Business
{
    public class ValidationConstants
    {
        public const string OnlyAlphaNumericCharactersRegex = @"^[a-zA-Z0-9\s]*$";
        public const string OnlyNumericCharactersRegex = @"^[0-9\s]*$";

        public const int GeneralMaxCharacterLimit = 50;
        public const int MinAge = 0;
        public const int MaxAge = 130;
    }
}
