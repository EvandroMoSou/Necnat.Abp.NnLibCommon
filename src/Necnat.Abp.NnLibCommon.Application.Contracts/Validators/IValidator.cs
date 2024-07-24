using Microsoft.Extensions.Localization;
using Necnat.Abp.NnLibCommon.Dtos;
using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Validators;

public interface IValidator<TEntityDto>
    : IValidator<TEntityDto, OptionalPagedAndSortedResultRequestDto>
{

}

public interface IValidator<TEntityDto, TGetListInput>
    : IValidator<TEntityDto, TEntityDto, TGetListInput>
{

}

public interface IValidator<TCreateInput, TUpdateInput, TGetListInput>
{
    static abstract List<string>? ValidateCreate(TCreateInput entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict);
    static abstract List<string>? ValidateUpdate(TUpdateInput entityDto, Dictionary<string, IStringLocalizer> stringLocalizerDict);
    static abstract List<string>? Validate(TGetListInput getListInput, Dictionary<string, IStringLocalizer> stringLocalizerDict);
}
