using System;

namespace Fuzzy_Logic.Constants
{
    public static class ErrorMessages
    {
        public const String InvalidRules = "One or more of the rules is invalid.";
        public const String InvalidInputValue = "Input values must be a doublem, decimal or integer.";
        public const String InvalidDefuzzType = "Membership Functions must be {0} defuzz type.";
        public const String InvalidMembershipFunction = "Membership functions must be trapezoid.";
        public const String InvalidArgumentA = "Argument a cannot be null or zero.";
        public const String InvalidArgumentB = "Argument b cannot be null or zero.";
        public const String InvalidArgumentTou = "Argument tou cannot be null or zero.";
    }
}