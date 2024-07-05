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
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsAuthServer(dto.IsAuthServer, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsBilling(dto.IsBilling, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsAuthorization(dto.IsAuthorization, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidatePermissionsGroupName(dto.PermissionsGroupName, dto.IsAuthorization, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateIsHierarchyComponent(dto.IsAuthorization, stringLocalizer));
            lError.AddIfNotIsNullOrWhiteSpace(ValidateHierarchyComponentTypeId(dto.HierarchyComponentTypeId, dto.IsHierarchyComponent, stringLocalizer));

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

        public static string? ValidateIsAuthServer(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.IsAuthServerDisplay);

            return null;
        }

        public static string? ValidateIsBilling(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.IsBillingDisplay);

            return null;
        }

        public static string? ValidateIsAuthorization(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.IsAuthorizationDisplay);

            return null;
        }

        public static string? ValidatePermissionsGroupName(string? value, bool? isAuthorization, IStringLocalizer stringLocalizer)
        {
            if (string.IsNullOrWhiteSpace(value) && isAuthorization == true)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.PermissionsGroupNameDisplay);

            if (value!.Length > NecnatEndpointConsts.MaxPermissionsGroupNameLength)
                return string.Format(stringLocalizer[ValidationMessages.MaxLength], NecnatEndpointConsts.PermissionsGroupNameDisplay, NecnatEndpointConsts.MaxPermissionsGroupNameLength);

            return null;
        }

        public static string? ValidateIsHierarchyComponent(bool? value, IStringLocalizer stringLocalizer)
        {
            if (value == null)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.IsHierarchyComponentDisplay);

            return null;
        }

        public static string? ValidateHierarchyComponentTypeId(short? value, bool? isHierarchyComponent, IStringLocalizer stringLocalizer)
        {
            if ((value == null || value == 0) && isHierarchyComponent == true)
                return string.Format(stringLocalizer[ValidationMessages.Required], NecnatEndpointConsts.HierarchyComponentTypeIdDisplay);

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
