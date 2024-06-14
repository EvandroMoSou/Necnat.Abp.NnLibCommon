namespace Necnat.Abp.NnLibCommon.Br.Extensions
{
    public static class IntExtension
    {
        public static string CepString(this int cep)
        {
            return cep.ToString("00000000");
        }
    }
}
