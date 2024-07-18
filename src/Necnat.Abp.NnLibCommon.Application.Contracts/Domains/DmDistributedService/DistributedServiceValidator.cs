using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Extensions;
using Necnat.Abp.NnLibCommon.Validators;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public static class DistributedServiceValidator
    {
        public static List<string>? Validate(DistributedServiceDto dto, IStringLocalizer stringLocalizer)
        {
            var lError = new List<string>();

            lError.AddIfNotIsNullOrWhiteSpace(ValidateApplicationName(dto.ApplicationName, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateTag(dto.Tag, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateUrl(dto.Url, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsActive(dto.IsActive, stringLocalizer));

            if (lError.Count > 0)
                return lError;

            return null;
        }

        public static string? ValidateApplicationName(string? value, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizer[ValidationMessages.Required], DistributedServiceConsts.ApplicationNameDisplay);

            if (value!.Length > DistributedServiceConsts.MaxApplicationNameLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], DistributedServiceConsts.ApplicationNameDisplay, DistributedServiceConsts.MaxApplicationNameLength);

            return null;
        }

        public static string? ValidateTag(string? value, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizer[ValidationMessages.Required], DistributedServiceConsts.TagDisplay);

            if (value!.Length > DistributedServiceConsts.MaxTagLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], DistributedServiceConsts.TagDisplay, DistributedServiceConsts.MaxTagLength);

            return null;
        }

        public static string? ValidateUrl(string? value, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizer[ValidationMessages.Required], DistributedServiceConsts.UrlDisplay);

            if (value!.Length > DistributedServiceConsts.MaxUrlLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], DistributedServiceConsts.UrlDisplay, DistributedServiceConsts.MaxUrlLength);

            return null;
        }

        public static string? ValidateIsActive(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], DistributedServiceConsts.IsActiveDisplay);

            return null;
        }

        public static List<string>? Validate(DistributedServiceResultRequestDto resultRequestDto, IStringLocalizer stringLocalizer)
        {
            var lError = new List<string>();

            if (lError.Count > 0)
                return lError;

            return null;
        }
    }
}
