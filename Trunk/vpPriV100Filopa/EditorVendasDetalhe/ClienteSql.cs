//using Microsoft.VisualBasic;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Vimaponto.PrimaveraV100.Clientes.Filopa.Properties;

//namespace EditorVendasDetalhe
//{
//    class ClienteSql
//    {

//        public static long ExecutarQueryPriEmpre(string SqlQuery)
//        {
//            if (String.IsNullOrEmpty(Strings.Trim(Settings.Default.ConnectionStringPrimaveraEmpresas)))
//            {
//                return 0;
//            }

//            BaseDados dbl = new BaseDados(Settings.Default.ConnectionStringPrimaveraEmpresas);
//            return dbl.ExecutarQuery(SqlQuery);
//        }

//        public static object ExecutarScalarPriEmpre(string SqlQuery)
//        {
//            if (String.IsNullOrEmpty(Strings.Trim(Settings.Default.ConnectionStringPrimaveraEmpresas)))
//            {
//                return null;
                
//            }

//            BaseDados dbl = new BaseDados(Settings.Default.ConnectionStringPrimaveraEmpresas);
//            return dbl.ExecutarScalar(SqlQuery);
//        }

//        public static bool ExecutarDataTablePriEmpre(string SqlQuery, ref DataTable Tabela)
//        {
//            if (String.IsNullOrEmpty(Strings.Trim(Settings.Default.ConnectionStringPrimaveraEmpresas)))
//            {
//                Tabela = default;
//                return false;
                
//            }

//            BaseDados dbl = new BaseDados(Settings.Default.ConnectionStringPrimaveraEmpresas);
//            return dbl.ExecutarDataTable(SqlQuery, ref Tabela);
//        }

//        public static long ExecutarQuery(string SqlQuery)
//        {
//            if (String.IsNullOrEmpty(Strings.Trim(Settings.Default.ConnectionStringPrimaveraEmpresas)))
//            {
//                return 0L;
                
//            }

//            BaseDados dbl = new BaseDados(Settings.Default.ConnectionStringPrimaveraSystem);
//            return dbl.ExecutarQuery(SqlQuery);
//        }

//        public static object ExecutarScalar(string SqlQuery)
//        {
//            if (String.IsNullOrEmpty(Strings.Trim(Settings.Default.ConnectionStringPrimaveraEmpresas)))
//            {
//                return null;
                
//            }

//            BaseDados dbl = new BaseDados(Settings.Default.ConnectionStringPrimaveraSystem);
//            return dbl.ExecutarScalar(SqlQuery);
//        }

//        public static bool ExecutarDataTable(string SqlQuery, ref DataTable Tabela)
//        {
//            if (String.IsNullOrEmpty(Strings.Trim(Settings.Default.ConnectionStringPrimaveraEmpresas)))
//            {
//                Tabela = default;
//                return false;
                
//            }

//            BaseDados dbl = new BaseDados(Settings.Default.ConnectionStringPrimaveraSystem);
//            return dbl.ExecutarDataTable(SqlQuery, ref Tabela);
//        }
//    }

//}
