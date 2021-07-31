using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTripSystem.Data
{
    public static class DataConstants
    {
       public const int DefaultMaxLength = 20;
        public const int DefaultMinLength = 3;
        public const string DefaultErrorMessage = "Text should be between 3 and 20 symbols";

        public const int PlateNumberLength = 8;
        public const string InvalidPlateNumberMessage = "Plate number must be exactly 8 symbols";
     
        public const int FreeSeatsMaxCount = 8;
        public const int FreeSeatsMinCount = 1;

        public const string URLRegex = @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})";
        public const string InvalidRegexMessage = "Please enter valid URL";

        public const int DescriptionMaxLength = 300;
    }
}
