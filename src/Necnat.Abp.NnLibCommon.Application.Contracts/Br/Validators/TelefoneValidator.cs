//using Necnat.Abp.NnLibCommon.Br.Dtos;
//using Necnat.Abp.NnLibCommon.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Necnat.Abp.NnLibCommon.Br.Validators
//{
//    public static class TelefoneDtoValidator
//    {
//        public static string? Validate(string fieldName, TelefoneDto? telefone, bool isRequired = false, bool withDdd = true)
//        {
//            if (telefone == null || string.IsNullOrWhiteSpace(telefone.Numero))
//            {
//                if (isRequired)
//                    return ComumValidator.RequiredMessage(fieldName);
//                else
//                    return null;
//            }

//            var error = TelefoneValidator.Validate(fieldName, telefone.Numero, true);
//            if (error != null)
//                return error;

//            if (withDdd)
//            {
//                error = TelefoneValidator.ValidateDdd("DDD do " + fieldName, telefone.Ddd, true);
//                if (error != null)
//                    return error;
//            }

//            return null;
//        }

//        public static string? ValidateListaTelefone(string fieldName, List<TelefoneDto>? lTelefone, List<TipoDto> lTipo, bool isRequired = false, bool withDdd = true, bool isRequiredPrincipal = true, bool isRequiredRecebeNotificacoes = false)
//        {
//            if (lTelefone == null || lTelefone.Count < 1)
//            {
//                if (isRequired)
//                    return ComumValidator.RequiredMessage(fieldName);
//                else
//                    return null;
//            }

//            if (isRequiredPrincipal)
//                foreach (var iTipoTelefone in lTelefone.Select(x => x.TipoTelefone))
//                    if (lTelefone.Where(x => x.TipoTelefone == iTipoTelefone && x.InPrincipal == true).Count() != 1)
//                        return "Cada tipo de telefone deve ter um, e apenas um, marcado como principal.";

//            if (isRequiredRecebeNotificacoes)
//            {
//                if (lTelefone.Any(x => x.Numero!.Length == 8 && x.InRecebeNotificacoes == true))
//                    return "Apenas celulares (com 9 dígitos) podem ser marcados como recebedor de notificações.";

//                if (lTelefone.Where(x => x.InRecebeNotificacoes == true).Count() < 1)
//                    return "Deve haver ao menos um telefone marcado como recebedor de notificações.";
//            }

//            var sb = new StringBuilder();
//            sb.AppendLine($"O campo {fieldName} {(isRequired ? "deve" : "pode")} conter:");
//            foreach (var iTipoPermitido in lTipo)
//            {
//                if (iTipoPermitido.QtdMinima != 0)
//                {
//                    sb.Append($"    No mínimo {iTipoPermitido.QtdMinima}");
//                }

//                if (iTipoPermitido.QtdMaxima != -1)
//                {
//                    if (iTipoPermitido.QtdMinima != 0)
//                        sb.Append(" e n");
//                    else
//                        sb.Append(" N");

//                    sb.Append($"o máximo {iTipoPermitido.QtdMinima}");
//                }

//                sb.AppendLine($" telefone(s) do tipo {iTipoPermitido.TipoNome} ({iTipoPermitido.TipoId}).");
//            }

//            var lError = new List<string>();
//            foreach (var iTelefone in lTelefone)
//            {
//                var tipoPermitido = lTipo.FirstOrDefault(x => x.TipoId == iTelefone.TipoTelefone);
//                if (tipoPermitido == null)
//                    return sb.ToString();

//                lError.AddIfNotIsNullOrWhiteSpace(Validate($"Telefone {tipoPermitido.TipoNome}: {iTelefone.Numero}", iTelefone, true, true));

//                if (tipoPermitido.QtdMaxima == 0)
//                    return sb.ToString();

//                tipoPermitido.QtdMinima--;
//                tipoPermitido.QtdMaxima--;
//            }

//            if (lTipo.Any(x => x.QtdMinima > 0))
//                return sb.ToString();

//            if (lError.Count > 0)
//                return string.Join(", ", lError);

//            return null;
//        }
//    }
//}
