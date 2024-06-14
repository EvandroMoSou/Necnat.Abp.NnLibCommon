//using Necnat.Abp.NnLibCommon.Br.Consts;
//using Necnat.Abp.NnLibCommon.Extensions;

//namespace Necnat.Abp.NnLibCommon.Br.Validators
//{
//    public static class TelefoneValidator
//    {
//        public static string? Validate(string fieldName, string? telefone, bool isRequired = false)
//        {
//            telefone = telefone?.OnlyDigits().TrimStart('0');

//            if (isRequired)
//            {
//                var error = ComumValidator.ValidateRequired(fieldName, telefone);
//                if (error != null)
//                    return error;
//            }

//            return ComumValidator.ValidateMinMaxLength(fieldName, telefone, TelefoneConsts.MinNumeroLength, TelefoneConsts.MaxNumeroLength);
//        }

//        public static string? ValidateDdi(string fieldName, string? ddi, bool isRequired = false)
//        {
//            ddi = ddi?.OnlyDigits().TrimStart('0');

//            if (isRequired)
//            {
//                var eIsRequired = ComumValidator.ValidateRequired(fieldName, ddi);
//                if (eIsRequired != null)
//                    return eIsRequired;
//            }

//            var eMinMaxLength = ComumValidator.ValidateMinMaxLength(fieldName, ddi, TelefoneConsts.MinDdiLength, TelefoneConsts.MaxDdiLength);
//            if (eMinMaxLength != null)
//                return eMinMaxLength;

//            return null;
//        }

//        public static string? ValidateDdd(string fieldName, string? ddd, bool isRequired = false)
//        {
//            ddd = ddd?.OnlyDigits().TrimStart('0');

//            if (isRequired)
//            {
//                var eIsRequired = ComumValidator.ValidateRequired(fieldName, ddd);
//                if (eIsRequired != null)
//                    return eIsRequired;
//            }

//            var eMinMaxLength = ComumValidator.ValidateMinMaxLength(fieldName, ddd, TelefoneConsts.MinDddLength, TelefoneConsts.MaxDddLength);
//            if (eMinMaxLength != null)
//                return eMinMaxLength;

//            return null;
//        }

//        public static string? ValidateWithDdd(string fieldName, string? telefone, bool isRequired = false)
//        {
//            telefone = telefone?.OnlyDigits().TrimStart('0');

//            if (isRequired)
//            {
//                var eIsRequired = ComumValidator.ValidateRequired(fieldName, telefone);
//                if (eIsRequired != null)
//                    return eIsRequired;
//            }

//            var eMinMaxLength = ComumValidator.ValidateMinMaxLength(fieldName, telefone, TelefoneConsts.MinNumeroLength + 2, TelefoneConsts.MaxNumeroLength + 2);
//            if (eMinMaxLength != null)
//                return eMinMaxLength + " Verifique se você adicionou o DDD.";

//            return null;
//        }
//    }
//}
