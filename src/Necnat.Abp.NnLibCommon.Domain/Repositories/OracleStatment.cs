using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public class OracleStatment : IDbStatement
    {
        public string GenerateCountStatement(string tableName)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine(" COUNT(1)");
            sb.AppendLine($"FROM {tableName}");

            return sb.ToString();
        }

        public string GenerateDeleteStatement(string tableName, string primaryKeyName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"DELETE FROM {tableName}");
            sb.AppendLine($"WHERE {primaryKeyName} = {ToParameter(primaryKeyName)}");

            return sb.ToString();
        }

        public string GenerateGetListStatement(string tableName)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine(" *");
            sb.AppendLine($"FROM {tableName}");

            return sb.ToString();
        }

        public string GenerateGetStatement(string tableName, string primaryKeyName)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine(" *");
            sb.AppendLine($"FROM {tableName}");
            sb.AppendLine($"WHERE {primaryKeyName} = {ToParameter(primaryKeyName)}");
            sb.AppendLine($"AND ROWNUM = 1");

            return sb.ToString();
        }

        public string GenerateInsertStatement(string tableName, string primaryKeyName, List<string> lColumn)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"    INSERT INTO {tableName}");
            sb.AppendLine("    (");
            sb.AppendLine(string.Join(", ", lColumn));
            sb.AppendLine("    )");
            sb.AppendLine("    VALUES");
            sb.AppendLine("    (");
            sb.AppendLine(string.Join(", ", lColumn.Select(x => ToParameter(x))));
            sb.AppendLine("    )");
            sb.AppendLine($"    RETURNING {primaryKeyName} INTO :return_value");

            return sb.ToString();
        }

        public string GenerateInsertWithPrimaryKeyValueStatement(string tableName, string primaryKeyName, List<string> lColumn, string primaryKeyValue)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"    INSERT INTO {tableName}");
            sb.AppendLine("    (");
            sb.AppendLine($"     {primaryKeyName},");
            sb.AppendLine(string.Join(", ", lColumn));
            sb.AppendLine("    )");
            sb.AppendLine("    VALUES");
            sb.AppendLine("    (");
            sb.AppendLine($"     {primaryKeyValue},");
            sb.AppendLine(string.Join(", ", lColumn.Select(x => ToParameter(x))));
            sb.AppendLine("    )");
            sb.AppendLine($"    RETURNING {primaryKeyName} INTO :return_value");

            return sb.ToString();
        }

        public string GeneratePagedStatement(string statment, long skipCount, long maxResultCount, string sorting)
        {
            var sb = new StringBuilder();
            sb.AppendLine(statment);
            sb.AppendLine($"ORDER BY {sorting}");
            if (skipCount == 0)
                sb.AppendLine($"FETCH FIRST {maxResultCount} ROWS ONLY");
            else
                sb.AppendLine($"OFFSET {skipCount} ROWS FETCH NEXT {maxResultCount} ROWS ONLY");

            return sb.ToString();
        }

        public string GenerateUpdateStatement(string tableName, string primaryKeyName, List<string> lColumn)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"UPDATE {tableName} SET");
            sb.AppendLine(string.Join(", ", lColumn.Select(x => x + " = " + ToParameter(x))));
            sb.AppendLine($"WHERE {primaryKeyName} = {ToParameter(primaryKeyName)}");

            return sb.ToString();
        }

        public string ToParameter(string parameterName)
        {
            if (parameterName.Length > 29)
                parameterName = parameterName.Substring(0, 29);

            return ":p" + parameterName;
        }
    }
}
