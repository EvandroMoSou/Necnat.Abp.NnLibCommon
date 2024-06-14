using System;
using System.Collections.Generic;
using System.Text;

namespace Necnat.Abp.NnLibCommon.Br.Dtos
{
    public class TelefoneDto
    {
        public int? TipoTelefone { get; set; }
        public string? Ddi { get; set; }
        public string? Ddd { get; set; }
        public string? Numero { get; set; }
        public string? NomeContato { get; set; }
        public bool? InPrincipal { get; set; }
        public bool? InRecebeNotificacoes { get; set; }
    }
}
