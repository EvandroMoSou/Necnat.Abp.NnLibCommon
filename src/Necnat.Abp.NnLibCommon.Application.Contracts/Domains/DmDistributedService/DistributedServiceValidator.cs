using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Extensions;
using Necnat.Abp.NnLibCommon.Validators;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public struct DistributedServiceValidator : IValidator<DistributedServiceDto, DistributedServiceResultRequestDto>
    {
        public static List<string>? ValidateCreate(DistributedServiceDto entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return Validate(entityDto, stringLocalizerDict);
        }

        public static List<string>? ValidateUpdate(DistributedServiceDto entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return Validate(entityDto, stringLocalizerDict);
        }

        public static List<string>? Validate(DistributedServiceDto dto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            var lError = new List<string>();

            lError.AddIfNotIsNullOrWhiteSpace(ValidateApplicationName(dto.ApplicationName, stringLocalizerDict));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateTag(dto.Tag, stringLocalizerDict));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateUrl(dto.Url, stringLocalizerDict));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsActive(dto.IsActive, stringLocalizerDict));

            if (lError.Count > 0)
                return lError;

            return null;
        }

        public static string? ValidateApplicationName(string? value, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizerDict[ValidatorConsts.StringLocalizerNecnat][ValidationMessages.Required], DistributedServiceConsts.ApplicationNameDisplay);

            if (value!.Length > DistributedServiceConsts.MaxApplicationNameLength)
                return string.Format(stringLocalizerDict[ValidatorConsts.StringLocalizerNecnat][ValidationMessages.MaxLength], DistributedServiceConsts.ApplicationNameDisplay, DistributedServiceConsts.MaxApplicationNameLength);

            return null;
        }

        public static string? ValidateTag(string? value, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizerDict[ValidatorConsts.StringLocalizerNecnat][ValidationMessages.Required], DistributedServiceConsts.TagDisplay);

            if (value!.Length > DistributedServiceConsts.MaxTagLength)
                return string.Format(stringLocalizerDict[ValidatorConsts.StringLocalizerNecnat][ValidationMessages.MaxLength], DistributedServiceConsts.TagDisplay, DistributedServiceConsts.MaxTagLength);

            return null;
        }

        public static string? ValidateUrl(string? value, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizerDict[ValidatorConsts.StringLocalizerNecnat][ValidationMessages.Required], DistributedServiceConsts.UrlDisplay);

            if (value!.Length > DistributedServiceConsts.MaxUrlLength)
                return string.Format(stringLocalizerDict[ValidatorConsts.StringLocalizerNecnat][ValidationMessages.MaxLength], DistributedServiceConsts.UrlDisplay, DistributedServiceConsts.MaxUrlLength);

            return null;
        }

        public static string? ValidateIsActive(bool? value, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            if (value == null)
                return string.Format(stringLocalizerDict[ValidatorConsts.StringLocalizerNecnat][ValidationMessages.Required], DistributedServiceConsts.IsActiveDisplay);

            return null;
        }

        public static List<string>? Validate(DistributedServiceResultRequestDto resultRequestDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            var lError = new List<string>();

            if (lError.Count > 0)
                return lError;

            return null;
        }
    }
}
