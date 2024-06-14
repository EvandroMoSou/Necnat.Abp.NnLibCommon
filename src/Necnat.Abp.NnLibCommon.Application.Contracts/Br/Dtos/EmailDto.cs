using System;
using System.Collections.Generic;
using System.Text;

namespace Necnat.Abp.NnLibCommon.Br.Dtos
{
    public class EmailDto
    {
        public int? TipoEmail { get; set; }
        public string? Email { get; set; }
        public bool? InRecebeNotificacoes { get; set; }
        public bool? InPrincipal { get; set; }
    }
}
