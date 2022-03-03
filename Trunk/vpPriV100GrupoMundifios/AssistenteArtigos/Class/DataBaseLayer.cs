using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Properties;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class
{
    class DataBaseLayer
    {

        private System.Data.SqlClient.SqlConnection _SqlConnect;
        private System.Data.SqlClient.SqlCommand _SqlCommand;

        public DataBaseLayer()
        {
            _SqlConnect = new System.Data.SqlClient.SqlConnection();
            _SqlCommand = new System.Data.SqlClient.SqlCommand();
            // # atribuir conexão ativa ao command
            _SqlConnect.ConnectionString =  Settings.Default.PRIEMPREConnectionString;
        }

        public long ExecutarQuery(string SqlQuery)
        {
            long Registos = 0;

            _SqlConnect.Open();

            _SqlCommand.Connection = _SqlConnect;
            _SqlCommand.CommandType = CommandType.Text;
            _SqlCommand.CommandText = SqlQuery;
            _SqlCommand.CommandTimeout = 0;

            // # executar query
            Registos = _SqlCommand.ExecuteNonQuery();

            _SqlConnect.Close();

            return Registos;
        }

        public object ExecutarScalar(string SqlQuery)
        {
            object valor;

            _SqlConnect.Open();

            _SqlCommand.Connection = _SqlConnect;
            _SqlCommand.CommandType = CommandType.Text;
            _SqlCommand.CommandText = SqlQuery;
            _SqlCommand.CommandTimeout = 0;

            // # executar query
            valor = _SqlCommand.ExecuteScalar();

            _SqlConnect.Close();

            return valor;
        }

        public bool ExecutarDataTable(string SqlQuery, ref DataTable Tabela)
        {
            if (Tabela == null)
                Tabela = new DataTable();

            _SqlConnect.Open();

            _SqlCommand.Connection = _SqlConnect;
            _SqlCommand.CommandType = CommandType.Text;
            _SqlCommand.CommandText = SqlQuery;
            _SqlCommand.CommandTimeout = 0;

            // # criar adapter
            System.Data.SqlClient.SqlDataAdapter _sqlAdapt = new System.Data.SqlClient.SqlDataAdapter(_SqlCommand);

            // # executar query
            _sqlAdapt.Fill(Tabela);

            _SqlConnect.Close();

            return true;
        }

    }
}
