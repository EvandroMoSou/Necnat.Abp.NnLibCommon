using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Extensions;
using Necnat.Abp.NnLibCommon.Validators;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Domains
{
    public static class NecnatEndpointValidator
    {
        public static List<string>? Validate(NecnatEndpointDto dto, IStringLocalizer stringLocalizer)
        {
            var lError = new List<string>();

            lError.AddIfNotIsNullOrWhiteSpace(ValidateDisplayName(dto.DisplayName, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateEndpoint(dto.Endpoint, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsActive(dto.IsActive, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsAuthz(dto.IsAuthz, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsBilling(dto.IsBilling, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsUser(dto.IsUser, stringLocalizer));

            if (lError.Count > 0)
                return lError;

            return null;
        }

        public static string? ValidateDisplayName(string? value, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.DisplayNameDisplay);

            if (value!.Length > NecnatEndpointConsts.MaxDisplayNameLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], NecnatEndpointConsts.DisplayNameDisplay, NecnatEndpointConsts.MaxDisplayNameLength);

            return null;
        }

        public static string? ValidateEndpoint(string? value, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.EndpointDisplay);

            if (value!.Length > NecnatEndpointConsts.MaxEndpointLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], NecnatEndpointConsts.EndpointDisplay, NecnatEndpointConsts.MaxEndpointLength);

            return null;
        }

        public static string? ValidateIsActive(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.IsActiveDisplay);

            return null;
        }

        public static string? ValidateIsAuthz(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.IsAuthzDisplay);

            return null;
        }

        public static string? ValidateIsBilling(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.IsBillingDisplay);

            return null;
        }

        public static string? ValidateIsUser(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.IsUserDisplay);

            return null;
        }

        public static List<string>? Validate(NecnatEndpointResultRequestDto resultRequestDto, IStringLocalizer stringLocalizer)
        {
            var lError = new List<string>();

            if (lError.Count > 0)
                return lError;

            return null;
        }
    }
}
