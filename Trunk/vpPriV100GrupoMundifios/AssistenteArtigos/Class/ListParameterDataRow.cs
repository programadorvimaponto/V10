using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class
{
   public partial class ListParameterDataRow :ListParameter
    {
        private DataRow row;

        public ListParameterDataRow(DataRow RowData, int CodigoIndex, int DescricaoIndex, object IdRegisto = null): base(CodigoIndex, DescricaoIndex, IdRegisto)
        {
            row = RowData; 
        }

        protected override string GetString(object O)
        {
            int iIndex = System.Convert.ToInt32(O);
            if (iIndex >= 0)
                return row[iIndex].ToString();
            else
                return string.Empty;
        }

        protected override void SetString(ref object O, string Value)
        {
            int iIndex = System.Convert.ToInt32(O);
            int i = Value.Length;
            if (iIndex >= 0)
                row[iIndex] = Value;
        }

    }
}
