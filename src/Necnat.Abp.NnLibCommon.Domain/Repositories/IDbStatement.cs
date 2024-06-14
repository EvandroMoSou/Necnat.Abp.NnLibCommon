using System.Collections.Generic;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public interface IDbStatement
    {
        string GenerateCountStatement(string tableName);
        string GenerateDeleteStatement(string tableName, string primaryKeyName);
        string GenerateGetListStatement(string tableName);
        string GenerateGetStatement(string tableName, string primaryKeyName);
        string GenerateInsertStatement(string tableName, string primaryKeyName, List<string> lColumn);
        string GenerateInsertWithPrimaryKeyValueStatement(string tableName, string primaryKeyName, List<string> lColumn, string primaryKeyValue);
        string GeneratePagedStatement(string statment, long skipCount, long maxResultCount, string sorting);
        string GenerateUpdateStatement(string tableName, string primaryKeyName, List<string> lColumn);
        string ToParameter(string parameterName);
    }
}
