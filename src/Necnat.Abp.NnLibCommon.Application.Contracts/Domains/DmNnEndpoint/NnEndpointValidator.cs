using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Extensions;
using Necnat.Abp.NnLibCommon.Validators;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public static class NnEndpointValidator
    {
        public static List<string>? Validate(NnEndpointDto dto, IStringLocalizer stringLocalizer)
        {
            var lError = new List<string>();

            lError.AddIfNotIsNullOrWhiteSpace(ValidateDisplayName(dto.DisplayName, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateTag(dto.Tag, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateUrlUri(dto.UrlUri, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsActive(dto.IsActive, stringLocalizer));

            if (lError.Count > 0)
                return lError;

            return null;
        }

        public static string? ValidateDisplayName(string? value, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizer[ValidationMessages.Required], NnEndpointConsts.DisplayNameDisplay);

            if (value!.Length > NnEndpointConsts.MaxDisplayNameLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], NnEndpointConsts.DisplayNameDisplay, NnEndpointConsts.MaxDisplayNameLength);

            return null;
        }

        public static string? ValidateTag(string? value, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizer[ValidationMessages.Required], NnEndpointConsts.TagDisplay);

            if (value!.Length > NnEndpointConsts.MaxTagLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], NnEndpointConsts.TagDisplay, NnEndpointConsts.MaxTagLength);

            return null;
        }

        public static string? ValidateUrlUri(string? value, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizer[ValidationMessages.Required], NnEndpointConsts.UrlUriDisplay);

            if (value!.Length > NnEndpointConsts.MaxUrlUriLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], NnEndpointConsts.UrlUriDisplay, NnEndpointConsts.MaxUrlUriLength);

            return null;
        }

        public static string? ValidateIsActive(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NnEndpointConsts.IsActiveDisplay);

            return null;
        }

        public static List<string>? Validate(NnEndpointResultRequestDto resultRequestDto, IStringLocalizer stringLocalizer)
        {
            var lError = new List<string>();

            if (lError.Count > 0)
                return lError;

            return null;
        }
    }
}
