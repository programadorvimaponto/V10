using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class
{
    class DataServiceLayer
    {

        public static long ExecutarQuery(string SqlQuery)
        {
            DataBaseLayer dbl = new DataBaseLayer();
            return dbl.ExecutarQuery(SqlQuery);
        }

        public static object ExecutarScalar(string SqlQuery)
        {
            DataBaseLayer dbl = new DataBaseLayer();
            return dbl.ExecutarScalar(SqlQuery);
        }

        public static bool ExecutarDataTable(string SqlQuery, ref DataTable Tabela)
        {
            DataBaseLayer dbl = new DataBaseLayer();
            return dbl.ExecutarDataTable(SqlQuery, ref Tabela);
        }

    }
}
