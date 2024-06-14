//using Necnat.Abp.NnLibCommon.Consts;
//using Necnat.Abp.NnLibCommon.Extensions;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;

//namespace Necnat.Abp.NnLibCommon.Br.Validators
//{
//    public static class EmailValidator
//    {
//        public static List<string>? Validate(string email)
//        {
//            var lValidate = new List<string>();

//            lValidate.AddIfNotIsNullOrWhiteSpace(ComumValidator.ValidateMinMaxLength($"Email", email, EmailConsts.MinEmailLength, EmailConsts.MaxEmailLength));

//            if (!Regex.IsMatch(email, @"^[\w-_\.]+(@)(.+)$", RegexOptions.IgnoreCase))
//                lValidate.Add("O campo Email não apresenta o padrão endereco@domínio.extensao.");

//            if (lValidate.Count > 0)
//                return lValidate;

//            return null;
//        }
//    }
//}
