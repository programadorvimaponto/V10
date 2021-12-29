using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorVendasDetalhe
{
    class Enums
    {
        public sealed class TiposDocumento
        {
            public const string Encomenda = "NEC";
            public const string Contrato = "CNT";
            public const string Embarque = "EMB";
            public const string Proforma = "PF";
            public const string Fatura = "FA";
            public const string FaturaO = "FO";
            public const string FaturaI = "FI";
        }

        // Public ListaEntidadesGrupo() As String = {"0201", "0849", "0344", "0297", "M0580"}
        // Public ListaEntidadesGrupo() As String = {"0100", "0627", "0503"}

        public const string ARTIGOCOMISSAO = "COMISSAO";

        public const int NRCASASDECIMAIS = 2;
    }
}
