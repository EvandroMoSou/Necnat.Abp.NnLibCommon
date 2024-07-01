namespace Necnat.Abp.NnLibCommon;

public static class NnLibCommonDbProperties
{
    public static string DbTablePrefix { get; set; } = "Ncnt";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "NnLibCommon";
}
