namespace Necnat.Abp.NnLibCommon.Models
{
    public class GenericSearchModel<TResultRequestDto>
    {
        public string Filter { get; set; }
        public TResultRequestDto ResultRequestDto { get; set; }
        public string? Error { get; set; }

        public GenericSearchModel(string filter, TResultRequestDto resultRequestDto, string? error = null)
        {
            Filter = filter;
            ResultRequestDto = resultRequestDto;
            Error = error;
        }
    }
}
