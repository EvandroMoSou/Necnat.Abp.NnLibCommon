//using Necnat.Abp.NnLibCommon.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace Necnat.Abp.NnLibCommon.Br.Validators
//{
//    public static partial class ComumValidator
//    {
//        #region Enum

//        public static string? ValidateEnum<TEnum>(string fieldName, TEnum? enumValue, List<TEnum> lValidValue) where TEnum : struct, Enum
//        {
//            if (enumValue == null)
//                return null;

//            if (!lValidValue.Contains((TEnum)enumValue))
//            {
//                var lValue = new List<string>();
//                foreach (TEnum iEnum in lValidValue)
//                    lValue.Add($"{EnumsNET.Enums.GetUnderlyingValueUnsafe(iEnum)} ({iEnum.AsString(EnumFormat.Description)}");

//                return $"O campo {fieldName} deve ser: {string.Join(", ", lValue)}.";
//            }

//            return null;
//        }

//        public static string? ValidateEnumRequired<TEnum>(string fieldName, TEnum? enumValue)
//        {
//            if (enumValue == null)
//                return RequiredMessage(fieldName);

//            return null;
//        }

//        #endregion

//        #region String

//        public static string? ValidateMaxLength(string fieldName, string? value, int maxLength)
//        {
//            if (string.IsNullOrWhiteSpace(value))
//                return null;

//            if (value!.Length > maxLength)
//                return $"O campo {fieldName} deve ter no máximo {maxLength} caracteres.";

//            return null;
//        }

//        public static string? ValidateMinLength(string fieldName, string? value, int minLength)
//        {
//            if (string.IsNullOrWhiteSpace(value))
//                return null;

//            if (value!.Length < minLength)
//                return $"O campo {fieldName} deve ter no mínimo {minLength} caracteres.";

//            return null;
//        }

//        public static string? ValidateMinMaxLength(string fieldName, string? value, int minLength, int maxLength)
//        {
//            if (string.IsNullOrWhiteSpace(value))
//                return null;

//            if (value!.Length < minLength || value.Length > maxLength)
//            {
//                if (minLength == maxLength)
//                    return $"O campo {fieldName} deve ter {minLength} caracteres.";
//                else
//                    return $"O campo {fieldName} deve ter entre {minLength} a {maxLength} caracteres.";
//            }

//            return null;
//        }

//        public static string? ValidateRequired(string fieldName, string? value)
//        {
//            if (string.IsNullOrWhiteSpace(value))
//                return RequiredMessage(fieldName);

//            return null;
//        }

//        #endregion

//        #region DateTime

//        public static string? ValidateRequired(string fieldName, DateTime? value)
//        {
//            if (value == null || value == default(DateTime) || value == DateTime.MinValue || value == DateTime.MaxValue)
//                return RequiredMessage(fieldName);

//            return null;
//        }

//        public static string? ValidateLess(string fieldNameStart, string fieldNameEnd, DateTime? valueStart, DateTime? valueEnd)
//        {
//            if (valueStart == null || valueEnd == null)
//                return null;

//            if (valueStart > valueEnd)
//                return $"O campo {fieldNameStart} não pode ser maior que o {fieldNameEnd}.";

//            return null;
//        }

//        public static string? ValidateNotSameYear(string fieldNameOne, string fieldNameTwo, DateTime? valueOne, DateTime? valueTwo)
//        {
//            if (valueOne == null || valueTwo == null)
//                return null;

//            if (((DateTime)valueOne).Year != ((DateTime)valueTwo).Year)
//                return $"O campo {fieldNameOne} e {fieldNameTwo} devem ser do mesmo ano.";

//            return null;
//        }

//        #endregion

//        #region Long

//        public static string? ValidateRequired(string fieldName, long? value)
//        {
//            if (value == null)
//                return RequiredMessage(fieldName);

//            return null;
//        }

//        public static string? ValidateLess(string fieldNameStart, string fieldNameEnd, long? valueStart, long? valueEnd)
//        {
//            if (valueStart == null || valueEnd == null)
//                return null;

//            if (valueStart > valueEnd)
//                return $"O campo {fieldNameStart} não pode ser maior que o {fieldNameEnd}.";

//            return null;
//        }

//        #endregion

//        #region Bool

//        public static string? ValidateRequired(string fieldName, bool? value)
//        {
//            if (value == null)
//                return RequiredMessage(fieldName);

//            return null;
//        }

//        #endregion

//        #region List

//        public static string? ValidateRequired<T>(string fieldName, IEnumerable<T>? lValue)
//        {
//            if (lValue == null || lValue.Count() < 1)
//                return RequiredMessage(fieldName);

//            return null;
//        }

//        #endregion

//        public static string? ValidateNome(string fieldName, string? value)
//        {
//            if (string.IsNullOrWhiteSpace(value))
//                return null;

//            var result = ValidateMinLength(fieldName, value, 3);
//            if (result != null)
//                return result;

//            var splitName = value!.Split(' ');
//            if (splitName.Length < 2)
//                return $"O campo {fieldName} deve ter no mínimo duas palavras.";

//            if (value.Contains("  "))
//                return $"O campo {fieldName} não pode ter espaços duplicados.";

//            if (splitName[0].Length < 2 && splitName[1].Length < 2)
//                return $"O campo {fieldName} deve ter mais de um caracter nas duas primeiras palavras.";

//            string pattern = @"(?i)[^a-záéíóúàèìòùâêîôûãõäöüç'\s]";
//            Regex rgx = new Regex(pattern);
//            if (!rgx.IsMatch(value.ToLower()))
//                return $"O campo {fieldName} só deve conter letras, acentos gráficos e caracteres apóstrofo.";

//            if (splitName.Length == 2 && splitName[0].Length < 3 && splitName[1].Length < 3)
//                return $"O campo {fieldName} deve ter mais de dois caracteres nas duas únicas palavras.";

//            return null;
//        }

//        public static string? ValidateCns(string fieldName, string cns)
//        {
//            cns = cns.OnlyDigits();

//            var result = ValidateMinMaxLength(fieldName, cns, 15, 15);
//            if (result != null)
//                return result;

//            var primeiroDigito = cns.Substring(0, 1);

//            if (primeiroDigito == "1" || primeiroDigito == "2")
//            {
//                if (!IsValidCnsComInicio1ou2(cns))
//                    return $"O campo {fieldName} é inválido.";
//            }
//            else if (primeiroDigito == "7" || primeiroDigito == "8" || primeiroDigito == "9")
//            {
//                if (!IsValidCnsComInicio7ou8ou9(cns))
//                    return $"O campo {fieldName} é inválido.";
//            }
//            else
//                return $"O campo {fieldName} deve iniciar com 1, 2, 7, 8 ou 9.";

//            return null;
//        }

//        public static string? ValidateCpf(string fieldName, string cpf)
//        {
//            cpf = cpf.OnlyDigits();

//            var result = ValidateMinMaxLength(fieldName, cpf, 11, 11);
//            if (result != null)
//                return result;

//            if (!IsValidCpf(cpf))
//                return $"O campo {fieldName} é inválido.";

//            return null;
//        }

//        public static bool IsValidCns(string cns)
//        {
//            cns = cns.OnlyDigits();

//            if (cns.Length != 15)
//                return false;

//            var primeiroDigito = cns.Substring(0, 1);
//            if (primeiroDigito == "1" || primeiroDigito == "2")
//                return IsValidCnsComInicio1ou2(cns);
//            else if (primeiroDigito == "7" || primeiroDigito == "8" || primeiroDigito == "9")
//                return IsValidCnsComInicio7ou8ou9(cns);
//            else
//                return false;
//        }

//        private static bool IsValidCnsComInicio1ou2(string cns)
//        {
//            float soma;
//            float resto, dv;
//            string pis = string.Empty;
//            string resultado = string.Empty;
//            pis = cns.Substring(0, 11);

//            soma = int.Parse(pis.Substring(0, 1)) * 15 +
//                int.Parse(pis.Substring(1, 1)) * 14 +
//                int.Parse(pis.Substring(2, 1)) * 13 +
//                int.Parse(pis.Substring(3, 1)) * 12 +
//                int.Parse(pis.Substring(4, 1)) * 11 +
//                int.Parse(pis.Substring(5, 1)) * 10 +
//                int.Parse(pis.Substring(6, 1)) * 9 +
//                int.Parse(pis.Substring(7, 1)) * 8 +
//                int.Parse(pis.Substring(8, 1)) * 7 +
//                int.Parse(pis.Substring(9, 1)) * 6 +
//                int.Parse(pis.Substring(10, 1)) * 5;

//            resto = soma % 11;
//            dv = 11 - resto;

//            if (dv == 11)
//                dv = 0;

//            if (dv == 10)
//            {
//                soma = int.Parse(pis.Substring(0, 1)) * 15 +
//                    int.Parse(pis.Substring(1, 1)) * 14 +
//                    int.Parse(pis.Substring(2, 1)) * 13 +
//                    int.Parse(pis.Substring(3, 1)) * 12 +
//                    int.Parse(pis.Substring(4, 1)) * 11 +
//                    int.Parse(pis.Substring(5, 1)) * 10 +
//                    int.Parse(pis.Substring(6, 1)) * 9 +
//                    int.Parse(pis.Substring(7, 1)) * 8 +
//                    int.Parse(pis.Substring(8, 1)) * 7 +
//                    int.Parse(pis.Substring(9, 1)) * 6 +
//                    int.Parse(pis.Substring(10, 1)) * 5 + 2;

//                resto = soma % 11;
//                dv = 11 - resto;
//                resultado = pis + "001" + ((int)dv).ToString();
//            }
//            else
//            {
//                resultado = pis + "000" + ((int)dv).ToString();
//            }

//            return cns == resultado;
//        }

//        private static bool IsValidCnsComInicio7ou8ou9(string cns)
//        {
//            float resto, soma;

//            soma = int.Parse(cns.Substring(0, 1)) * 15 +
//                int.Parse(cns.Substring(1, 1)) * 14 +
//                int.Parse(cns.Substring(2, 1)) * 13 +
//                int.Parse(cns.Substring(3, 1)) * 12 +
//                int.Parse(cns.Substring(4, 1)) * 11 +
//                int.Parse(cns.Substring(5, 1)) * 10 +
//                int.Parse(cns.Substring(6, 1)) * 9 +
//                int.Parse(cns.Substring(7, 1)) * 8 +
//                int.Parse(cns.Substring(8, 1)) * 7 +
//                int.Parse(cns.Substring(9, 1)) * 6 +
//                int.Parse(cns.Substring(10, 1)) * 5 +
//                int.Parse(cns.Substring(11, 1)) * 4 +
//                int.Parse(cns.Substring(12, 1)) * 3 +
//                int.Parse(cns.Substring(13, 1)) * 2 +
//                int.Parse(cns.Substring(14, 1)) * 1;

//            resto = soma % 11;

//            return resto == 0;
//        }

//        public static bool IsValidCpf(string cpf)
//        {
//            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
//            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
//            string tempCpf;
//            string digito;
//            int soma;
//            int resto;

//            cpf = cpf.Trim();
//            cpf = cpf.OnlyDigits();

//            if (cpf.Length != 11)
//                return false;

//            tempCpf = cpf.Substring(0, 9);
//            soma = 0;

//            for (int i = 0; i < 9; i++)
//                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

//            resto = soma % 11;

//            if (resto < 2)
//                resto = 0;
//            else
//                resto = 11 - resto;

//            digito = resto.ToString();
//            tempCpf = tempCpf + digito;
//            soma = 0;

//            for (int i = 0; i < 10; i++)
//                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

//            resto = soma % 11;

//            if (resto < 2)
//                resto = 0;
//            else
//                resto = 11 - resto;

//            digito = digito + resto.ToString();

//            return cpf.EndsWith(digito);
//        }

//        #region Messages

//        public static string RequiredMessage(string fieldName)
//        {
//            return "O campo " + fieldName + " é obrigatório.";
//        }

//        #endregion
//    }
//}
