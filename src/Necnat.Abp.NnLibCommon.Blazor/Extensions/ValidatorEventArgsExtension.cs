using Blazorise;

namespace Necnat.Abp.NnLibCommon.Blazor.Extensions
{
    public static class ValidatorEventArgsExtension
    {
        public static void WithError(this ValidatorEventArgs args, string? error)
        {
            args.Status = string.IsNullOrEmpty(error)
                ? ValidationStatus.Success
                : ValidationStatus.Error;
            args.ErrorText = error ?? string.Empty;
        }
    }
}
