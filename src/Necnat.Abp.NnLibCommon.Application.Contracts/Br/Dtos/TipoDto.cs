//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Necnat.Abp.NnLibCommon.Br.Dtos
//{
//    //Mais especifico (1 contato c/ e 1 sem email)
//    public class TipoDto
//    {
//        public int TipoId { get; set; }
//        public string TipoNome { get; set; } = string.Empty;
//        public short QtdMinima { get; set; }
//        public short QtdMaxima { get; set; }

//        public TipoDto()
//        {
//        }

//        public TipoDto(int tipoId, string tipoNome, short qtdMinima, short qtdMaxima)
//        {
//            TipoId = tipoId;
//            TipoNome = tipoNome;
//            QtdMinima = qtdMinima;
//            QtdMaxima = qtdMaxima;
//        }

//        public void Initialize<TEnum>(TEnum tipo, short qtdMinima, short qtdMaxima)
//            where TEnum : struct, Enum
//        {
//            TipoId = Convert.ToInt32(tipo);
//            TipoNome = tipo.AsString(EnumFormat.Description)!;
//            QtdMinima = qtdMinima;
//            QtdMaxima = qtdMaxima;
//        }
//    }
//}
