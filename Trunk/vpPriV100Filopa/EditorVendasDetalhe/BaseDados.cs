using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EditorVendasDetalhe
{
    class BaseDados
    {


        private System.Data.SqlClient.SqlConnection _SqlConnect;
        private System.Data.SqlClient.SqlCommand _SqlCommand;
        private int _SqlCommandTimeout;

        public BaseDados(string ConnectionString, int CommandTimeout = 30)
        {
            _SqlConnect = new System.Data.SqlClient.SqlConnection();
            _SqlCommand = new System.Data.SqlClient.SqlCommand();
            _SqlCommandTimeout = CommandTimeout;

            // # atribuir conexão ativa ao command
            _SqlConnect.ConnectionString = ConnectionString;
        }

        public long ExecutarQuery(string SqlQuery)
        {
            long Registos = 0L;
            _SqlConnect.Open();
            _SqlCommand.Connection = _SqlConnect;
            _SqlCommand.CommandType = CommandType.Text;
            _SqlCommand.CommandText = SqlQuery;
            _SqlCommand.CommandTimeout = _SqlCommandTimeout;

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
            _SqlCommand.CommandTimeout = _SqlCommandTimeout;

            // # executar query
            valor = _SqlCommand.ExecuteScalar();
            _SqlConnect.Close();
            return valor;
        }

        public bool ExecutarDataTable(string SqlQuery, ref DataTable Tabela)
        {
            if (Tabela is null)
                Tabela = new DataTable();
            _SqlConnect.Open();
            _SqlCommand.Connection = _SqlConnect;
            _SqlCommand.CommandType = CommandType.Text;
            _SqlCommand.CommandText = SqlQuery;
            _SqlCommand.CommandTimeout = _SqlCommandTimeout;

            // # criar adapter
            var _sqlAdapt = new System.Data.SqlClient.SqlDataAdapter(_SqlCommand);

            // # executar query
            _sqlAdapt.Fill(Tabela);
            _SqlConnect.Close();
            return true;
        }
    }

}
