using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class
{
    class EmpresaGrupo
    {

        private string sEmpresa;
        private string sPRIEmpresa;
        private string sConnectionString;
        private string sInstancia;
        private string sUser;
        private string sPassword;

        // Public Sub New(ByVal Nome As String, ByVal ConnectionString As String)
        public EmpresaGrupo(string connection, string Apagar)
        {
            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder(connection);
            conn.InitialCatalog = "PRI" + PriV100Api.VSO.Contexto.Empresa;
            sPRIEmpresa = conn.InitialCatalog;
            sEmpresa = PriV100Api.VSO.Contexto.Empresa;
            sInstancia =conn.DataSource;
            sPassword = conn.Password;
            sUser = conn.UserID;
            sConnectionString = conn.ToString();
        }

        public EmpresaGrupo(string Nome, string Instancia, string User, string Password, string Apagar)
        {
            sPRIEmpresa = "PRI" + Nome;
            sEmpresa = Nome;
            sInstancia = Instancia;
            sPassword = Password;
            sUser = User;
            sConnectionString = "Data Source=" + Instancia + ";Initial Catalog=" + sPRIEmpresa + ";Persist Security Info=True;User ID=" + User + ";Password=" + Password + ";Connect Timeout=0";
        }

        public string PRIEmpresa
        {
            get
            {
                return sPRIEmpresa;
            }
            set
            {
                sPRIEmpresa = value;
            }
        }


        public string User
        {
            get
            {
                return sUser;
            }
            set
            {
                sUser = value;
            }
        }


        public string Empresa
        {
            get
            {
                return sEmpresa;
            }
            set
            {
                sEmpresa = value;
            }
        }

        public string Instancia
        {
            get
            {
                return sInstancia;
            }
            set
            {
                sInstancia = value;
            }
        }

        public string Password
        {
            get
            {
                return sPassword;
            }
            set
            {
                sPassword = value;
            }
        }


        public string ConnectionString
        {
            get
            {
                return sConnectionString;
            }
            set
            {
                sConnectionString = value;
            }
        }

    }
}
