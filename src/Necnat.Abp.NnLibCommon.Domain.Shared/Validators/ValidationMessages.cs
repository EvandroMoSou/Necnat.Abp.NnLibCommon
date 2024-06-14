namespace Necnat.Abp.NnLibCommon.Validators
{
    public static class ValidationMessages
    {
        public const string GenericSearchField = "Generic";

        public const string ComprareLess = "The field {0} cannot be greater than {1}.";
        public const string DuplicateValues = "The field {0} contains duplicate values.";
        public const string GreaterThanZero = "The field {0} must be greater than zero.";
        public const string GreaterThanOrEqualZero = "The field {0} must be greater than or equal to zero.";
        public const string Length = "The field {0} must have {1} characters.";
        public const string LessThanZero = "The field {0} must be less than zero.";
        public const string LessThanOrEqualZero = "The field {0} must be less than or equal to zero.";
        public const string MaxLength = "The field {0} must have a maximum of {1} characters.";
        public const string MinLength = "The field {0} must have at least {1} characters.";
        public const string MinMaxLength = "The field {0} must have at least {1} and a maximum of {2} characters.";
        public const string NotSameYear = "The field {0} and {1} must be from the same year.";
        public const string NotSameYearMonth = "The field {0} and {1} must be from the same year and month.";
        public const string NotSameYearMonthDay = "The field {0} and {1} must be from the same year, month and day.";
        public const string Numeric = "The field {0} must be numeric.";
        public const string OneFilledAll3Required = "If one of the fields: {0} or {1}, is filled in, all are required.";
        public const string OneFilledAll4Required = "If one of the fields: {0}, {1} or {2}, is filled in, all are required.";
        public const string OneFilledBothRequired = "If one of the fields: {0} or {1}, is filled in, both are required.";
        public const string OneFilledTwoRequired = "If the field {0} is filled in, the field {1} is required.";
        public const string OneFilledTwoThreeRequired = "If the field {0} is filled in, the fields: {1} and {2} are required.";
        public const string OneFilledTwoThreeFourRequired = "If the field {0} is filled in, the fields: {1}, {2} and {3} are required.";
        public const string Required = "The field {0} is required.";
    }
}
