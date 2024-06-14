using System;
using System.Collections.Generic;
using System.Text;

namespace Necnat.Abp.NnLibCommon.Br.Dtos
{
    public class ContatoSimplesDto
    {
        public int? TipoContato { get; set; }
        public string? Nome { get; set; }
        public TelefoneDto? Telefone { get; set; }
        public EmailDto? Email { get; set; }
    }
}
