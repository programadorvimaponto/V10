using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vimaponto.Componentes.Sdk.Controlos.VmpGrid;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class
{
    public class Geral
    {

        public enum ListagemPropriedadesMundifios
        {
            NE,
            Componente,
            Tipo,
            Caracteristica,
            Torcao1,
            Torcao2,
            Referencia,
            Cone,
            Programa,
            Texturizacao,
            Dimensao,
            Categoria,
            Cor,
            Armazens
        }

        public enum ListagemMundifios
        {
            Armazens,
            TipoArtigo,
            GrupoTaxaDesperdicio,
            IntrastatPautal,
            CodigoAntigo
        }


    }
}
