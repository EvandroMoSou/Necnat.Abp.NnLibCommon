using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Dtos;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Validators
{
    public struct NullValidator<TEntityDto>
        : IValidator<TEntityDto>
    {
        public static List<string>? Validate(OptionalPagedAndSortedResultRequestDto getListInput, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }

        public static List<string>? ValidateCreate(TEntityDto entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }

        public static List<string>? ValidateUpdate(TEntityDto entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }
    }

    public struct NullValidator<TEntityDto, TGetListInput>
        : IValidator<TEntityDto, TGetListInput>
    {
        public static List<string>? Validate(TGetListInput getListInput, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }

        public static List<string>? ValidateCreate(TEntityDto entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }

        public static List<string>? ValidateUpdate(TEntityDto entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }
    }

    public struct NullValidator<TCreateInput, TUpdateInput, TGetListInput>
        : IValidator<TCreateInput, TUpdateInput, TGetListInput>
    {
        public static List<string>? Validate(TGetListInput getListInput, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }

        public static List<string>? ValidateCreate(TCreateInput entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }

        public static List<string>? ValidateUpdate(TUpdateInput entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict)
        {
            return null;
        }
    }
}
