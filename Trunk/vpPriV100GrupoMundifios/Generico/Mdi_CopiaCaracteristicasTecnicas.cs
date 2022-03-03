using Microsoft.VisualBasic;
using StdBE100;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using CmpBE100;
using Generico;
using Vimaponto.PrimaveraV100;

namespace Generico
{
    internal class Mdi_CopiaCaracteristicasTecnicas
    {
        static object afetados = 0;
        static object a = "";
        static int b = -1;
        public const string FornecedorIndiferenciado = "FVD";
        private static ADODB.Recordset ConsultaSQL_PRIEMPRE = new ADODB.Recordset();
        private static ADODB.Connection LigacaoSQL_PRIEMPRE = new ADODB.Connection();
        private static string Connection_PRIEMPRE;
        private static ADODB.Command StringSQL_PRIEMPRE = new ADODB.Command();

        // # Abrir ligaçao à base de dados remota
        private static ADODB.Recordset ConsultaSQL_ORIGEM = new ADODB.Recordset();
        private static ADODB.Connection LigacaoSQL_ORIGEM = new ADODB.Connection();
        private static string Connection_ORIGEM;
        private static ADODB.Command StringSQL_ORIGEM = new ADODB.Command();
        private static ADODB.Recordset ConsultaSQL_DESTINO = new ADODB.Recordset();
        private static ADODB.Connection LigacaoSQL_DESTINO = new ADODB.Connection();
        private static string Connection_DESTINO;
        private static ADODB.Command StringSQL_DESTINO = new ADODB.Command();
        private static string NomeEmpresa_ORIGEM;
        private static string Instancia_ORIGEM;
        private static string Instancia_DESTINO;
        // NomeEmpresa_DESTINO -> Passada por parametro

        private static string StrAvisos;
        public const string Str_PRIEMPRE_INSTANCIA = @"SVBD\PRILEV100";
        public const string Str_PRIEMPRE_PASSWORD = "VIMAPRILEV100";
        public const string PASSWORD_ORIGEM = "VIMAPRILEV100";
        public const string PASSWORD_DESTINO = "VIMAPRILEV100";     
        
        //public const string Str_PRIEMPRE_INSTANCIA = @"192.168.1.6\PRILPV100";
        //public const string Str_PRIEMPRE_PASSWORD = "VIMAPRILPV100";
        //public const string PASSWORD_ORIGEM = "VIMAPRILPV100";
        //public const string PASSWORD_DESTINO = "VIMAPRILPV100";

        // Str_EMPRESA_INSTANCIA -> Está na tabela TDU_EmpresasGrupo

        // Ao ser efetuada uma compra na empresa MUNDIFIOS à empresa INOVAFIL

        // NomeEmpresa_ORIGEM  -> INOVAFIL
        // NomeEmpresa_DESTINO -> MUNDIFIOS

        private static void Avisos(string ArtigoaCopiar)
        {
            if (Strings.Len(StrAvisos) > 0)
            {
                MessageBox.Show(StrAvisos, "Características do Artigo: " + ArtigoaCopiar);
                StrAvisos = "";
            }
        }

        public static bool CopiarCaractTec(string NomeEmpresa_DESTINO, CmpBEDocumentoCompra DocumentoCompra)
        {
            string ArtigoaCopiar=""; // Importante para se der algum erro/Problema, saber que artigo estava a copiar


                switch (PriV100Api.BSO.Base.Fornecedores.Edita(DocumentoCompra.Entidade).CamposUtil["CDU_EntidadeInterna"].Valor)
                {
                    case "0001":
                        {
                            NomeEmpresa_ORIGEM = "MUNDIFIOS";
                            break;
                        }

                    case "0002":
                        {
                            NomeEmpresa_ORIGEM = "INOVAFIL";
                            break;
                        }

                    case "0003":
                        {
                            NomeEmpresa_ORIGEM = "AVEFIOS";
                            break;
                        }

                    case "0004":
                        {
                            NomeEmpresa_ORIGEM = "YARNTRADE";
                            break;
                        }

                    case "0006":
                        {
                            NomeEmpresa_ORIGEM = "MUNDITALIA";
                            break;
                        }

                    case "0007":
                        {
                            NomeEmpresa_ORIGEM = "MIXYARN";
                            break;
                        }

                    default:
                        {
                            return true;
                        }
                }

            try
            {
                Connection_PRIEMPRE = "Provider=SQLOLEDB.1;Password=" + Str_PRIEMPRE_PASSWORD + ";Persist Security Info=True;User ID=sa;Initial Catalog=PRIEMPRE;Data Source=" + Str_PRIEMPRE_INSTANCIA + ";connection timeout=0";

                if (LigacaoSQL_PRIEMPRE.State == 0)
                    LigacaoSQL_PRIEMPRE.Open(Connection_PRIEMPRE);
                if (ConsultaSQL_PRIEMPRE.State == 1)
                    ConsultaSQL_PRIEMPRE.Close();

                ConsultaSQL_PRIEMPRE.ActiveConnection = LigacaoSQL_PRIEMPRE;


                if (!IdentificarConnection_ORIGEM(NomeEmpresa_ORIGEM))
                    Avisos(ArtigoaCopiar);

                if (!IdentificarConnection_DESTINO(NomeEmpresa_DESTINO))
                     Avisos(ArtigoaCopiar);

                // # Fechar a consulta porque não vou precisar mais dela
                ConsultaSQL_PRIEMPRE.Close();

                int i;
                for (i = 1; i <= DocumentoCompra.Linhas.NumItens; i++)
                {
                    if (DocumentoCompra.Linhas.GetEdita(i).TipoLinha != "60")
                    {
                        ArtigoaCopiar = DocumentoCompra.Linhas.GetEdita(i).Artigo;
                        if (!RegistarValores(DocumentoCompra.Linhas.GetEdita(i).Artigo, DocumentoCompra.Linhas.GetEdita(i).Lote))
                            Avisos(ArtigoaCopiar);
                    }
                }

                FecharConnections();

                return true;

                if (MessageBox.Show("Não foi possível realizar a cópia de características! Deseja mesmo assim gravar o documento?", "Copiar Características Técnicas", MessageBoxButtons.YesNo) == DialogResult.No)
                    return false;
                else
                    return true;

                FecharConnections();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Copiar Características Técnicas");
                return false;
            }
        }


        private static void FecharConnections()
        {
            if (ConsultaSQL_PRIEMPRE.State == 1)
                ConsultaSQL_PRIEMPRE.Close();
            if (ConsultaSQL_ORIGEM.State == 1)
                ConsultaSQL_ORIGEM.Close();
            if (ConsultaSQL_DESTINO.State == 1)
                ConsultaSQL_DESTINO.Close();

            if (LigacaoSQL_ORIGEM.State == 1)
                LigacaoSQL_ORIGEM.Close();
            if (LigacaoSQL_DESTINO.State == 1)
                LigacaoSQL_DESTINO.Close();
            if (LigacaoSQL_PRIEMPRE.State == 1)
                LigacaoSQL_PRIEMPRE.Close();

            StrAvisos = "";
        }


        private static bool IdentificarConnection_ORIGEM(string NomeEmpresa_ORIGEM)
        {
            if (ConsultaSQL_PRIEMPRE.State == 1)
                ConsultaSQL_PRIEMPRE.Close();

            // #Consultar a instancia da empresa grupo
            ConsultaSQL_PRIEMPRE.Open("SELECT * FROM TDU_EmpresasGrupo Where CDU_Empresa = '" + NomeEmpresa_ORIGEM + "'");

            // # Se existir esta empresa nas empresas de Grupo
            if (ConsultaSQL_PRIEMPRE.BOF != true)
            {
                ConsultaSQL_PRIEMPRE.MoveFirst();

                // # Identifico a instancia
                Instancia_ORIGEM = ConsultaSQL_PRIEMPRE.Fields["CDU_Instancia"].Value.ToString();

                switch (Instancia_ORIGEM)
                {
                    case @".\PRILEV100":
                        Connection_ORIGEM = "Provider=SQLOLEDB.1;Password=" + PASSWORD_ORIGEM + ";Persist Security Info=True;User ID=sa;Initial Catalog=" + "PRI" + ConsultaSQL_PRIEMPRE.Fields["CDU_Empresa"].Value + ";Data Source=" + Instancia_ORIGEM + ";connection timeout=0";
                        return true;
                    case @"SVBD\PRILEV100": // #Cliente
                            Connection_ORIGEM = "Provider=SQLOLEDB.1;Password=" + PASSWORD_ORIGEM + ";Persist Security Info=True;User ID=sa;Initial Catalog=" + "PRI" + ConsultaSQL_PRIEMPRE.Fields["CDU_Empresa"].Value + ";Data Source=" + Instancia_ORIGEM + ";connection timeout=0";
                            return true;

                    default:
                        {
                            MessageBox.Show("A intancia " + Instancia_DESTINO + " está configurada na PRIEMPRE e não está configurada.", "Atenção");
                            return false;
                        }
                }
            }
            else
            {
                AdicionarAviso("O Fornecedor com o código '" + NomeEmpresa_ORIGEM + "' não existe na PRIEMPRE.");
                return false;
            }
        }


        private static bool IdentificarConnection_DESTINO(string NomeEmpresa_DESTINO)
        {
            if (ConsultaSQL_PRIEMPRE.State == 1)
                ConsultaSQL_PRIEMPRE.Close();

            // #Consultar a instancia da empresa grupo
            ConsultaSQL_PRIEMPRE.Open("SELECT * FROM TDU_EmpresasGrupo Where CDU_Empresa = '" + NomeEmpresa_DESTINO + "'");

            // # Se existir esta empresa nas empresas de Grupo
            if (ConsultaSQL_PRIEMPRE.BOF != true)
            {
                ConsultaSQL_PRIEMPRE.MoveFirst();

                // # Identifico a instancia
                Instancia_DESTINO = ConsultaSQL_PRIEMPRE.Fields["CDU_Instancia"].Value.ToString();

                switch (Instancia_DESTINO)
                {
                    case @".\PRILEV100":
                        Connection_DESTINO = "Provider=SQLOLEDB.1;Password=" + PASSWORD_DESTINO + ";Persist Security Info=True;User ID=sa;Initial Catalog=" + "PRI" + ConsultaSQL_PRIEMPRE.Fields["CDU_Empresa"].Value + ";Data Source=" + Instancia_DESTINO + ";connection timeout=0";
                        return true;
                    case @"SVBD\PRILEV100": // #Cliente
                            Connection_DESTINO = "Provider=SQLOLEDB.1;Password=" + PASSWORD_DESTINO + ";Persist Security Info=True;User ID=sa;Initial Catalog=" + "PRI" + ConsultaSQL_PRIEMPRE.Fields["CDU_Empresa"].Value + ";Data Source=" + Instancia_DESTINO + ";connection timeout=0";
                            return true;

                    default:
                        {
                            MessageBox.Show("A intancia " + Instancia_DESTINO + " está configurada na PRIEMPRE e não está configurada.", "Atenção");
                            return false;
                        }
                }
            }
            else
            {
                AdicionarAviso("A Empresa para onde as características vão ser copiadas '" + NomeEmpresa_DESTINO + "' não existe na PRIEMPRE.");
                return false;
            }
        }


        private static bool RegistarValores(string Artigo, string lote)
        
        {
            string sInsertLabLot; // Query usada para a TDU_LaboratorioLote
            string sInsertLabLotComp; // Query usada para a TDU_LaboratorioLoteComparacao
            int UltimoNr; // Depois de inserir na tabela TDU_LaboratorioLote preciso saber o Nr que inseriu para inserir na tabela de Comparacoes
            int Fornecedor_Nr; // Depois de identificar o registo na LaboratorioLote através do Artigo + Lote, guardo nesta variável a chave para depois ir ler as comparações
            DateTime Fornecedor_DataRelInt; // Necessário para depois comparar com a Data da empresa Atual

            string Fornecedor;
            string AngulosCone;
            Fornecedor = "";
            AngulosCone = "";

            try
            {

                if (LigacaoSQL_ORIGEM.State == 1)
                    LigacaoSQL_ORIGEM.Close();
                if (LigacaoSQL_ORIGEM.State == 0)
                    LigacaoSQL_ORIGEM.Open(Connection_ORIGEM);

                ConsultaSQL_ORIGEM.ActiveConnection = LigacaoSQL_ORIGEM;


                // #Consulta à base de dados do fornecedor. INOVAFIL por exemplo
                // Não faço select das duas tabelas com um join porque podem haver registos com o mesmo nome!
                ConsultaSQL_ORIGEM.Open("SELECT * FROM TDU_LaboratorioLote where CDU_CodArtigo = '" + Artigo + "' and CDU_LoteArt = '" + lote + "' order by CDU_DataRelInt DESC");

                // # Se não tiver registos, sai!
                if (ConsultaSQL_ORIGEM.BOF == true)
                {
                    return true;
                }

                ConsultaSQL_ORIGEM.MoveFirst();

                // Guardo a Chave para identificar a comparacao
                Fornecedor_Nr = int.Parse(ConsultaSQL_ORIGEM.Fields["CDU_NR"].Value.ToString());
                Fornecedor_DataRelInt = DateTime.Parse(ConsultaSQL_ORIGEM.Fields["CDU_DataRelInt"].Value.ToString());


                if (LigacaoSQL_DESTINO.State == 1)
                    LigacaoSQL_DESTINO.Close();
                if (LigacaoSQL_DESTINO.State == 0)
                    LigacaoSQL_DESTINO.Open(Connection_DESTINO);

                ConsultaSQL_DESTINO.ActiveConnection = LigacaoSQL_DESTINO;


                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();

                // verificar se existe o mesmo artigo na empresa atual
                ConsultaSQL_DESTINO.Open("SELECT CDU_DataRelInt FROM TDU_LaboratorioLote where CDU_CodArtigo = '" + Artigo + "' and CDU_LoteArt = '" + lote + "' order by CDU_DataRelInt DESC");
                if (ConsultaSQL_DESTINO.BOF == false)
                {
                    ConsultaSQL_DESTINO.MoveFirst();

                    // #        Data no FORNECEDOR    >  DATA EMPRESA ATUAL
                    if (DateAndTime.DateDiff("s", (DateTime)Fornecedor_DataRelInt, (DateTime)ConsultaSQL_DESTINO.Fields["CDU_DataRelInt"].Value) >= 0)
                    {
                        return true;
                    }
                }

                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();

                if (!ValidaTipoProcesso(ConsultaSQL_ORIGEM.Fields["CDU_TipoProcesso"].Value + ""))
                    return false;
                // A pedido do Eng.º Pedro da Mundifios a 25-11-2015 foi ignorada esta tabela (ver mail)
                // If Not ValidaComposicao(ConsultaSQL_ORIGEM("CDU_Composicao") & "") Then Exit Function
                Fornecedor = ConsultaSQL_ORIGEM.Fields["CDU_Fornec"].Value + "";
                if (!ValidaFornecedor(ref Fornecedor))
                    return false;
                if (!ValidaTipoTorcedur(ConsultaSQL_ORIGEM.Fields["CDU_TipoTorcedur"].Value + ""))
                    return false;
                if (!ValidaTipoUsoFio(ConsultaSQL_ORIGEM.Fields["CDU_TipoUsoFio"].Value + ""))
                    return false;
                if (!ValidaTipoAcondicionamento(ConsultaSQL_ORIGEM.Fields["CDU_TipoAcond"].Value + ""))
                    return false;
                AngulosCone = ConsultaSQL_ORIGEM.Fields["CDU_AnguloCone"].Value + "";
                if (!ValidaAngulosCone(ref AngulosCone))
                    return false;

                // # Preparar a Query do Insert
                sInsertLabLot = " INSERT INTO [dbo].[TDU_LaboratorioLote] " + " ([CDU_NR],[CDU_NumTestFisico],[CDU_DataRelInt],[CDU_DataTestFisico],[CDU_CodArtigo],[CDU_LoteArt],[CDU_DataTestQuim],[CDU_TipoProcesso],[CDU_Composicao],[CDU_Fornec],[CDU_PaisFornec],[CDU_TipoTorcedur],[CDU_TipoUsoFio],[CDU_TipoAcond],[CDU_Obs],[CDU_TFOper],[CDU_TFHR],[CDU_TFNETeorico],[CDU_TFNe],[CDU_TFNeCVb],[CDU_TFUster],[CDU_TFUsterCVb],[CDU_TFUsterCVm],[CDU_TFUsterPontFinos],[CDU_TFUsterNeps],[CDU_TFUsterRCount],[CDU_TFUsterPontGrossos],[CDU_TFTorcaoTipT],[CDU_TFTorcaoTPM],[CDU_TFTorcaoCVbTPM],[CDU_TFRetorcaoTip],[CDU_TFRetTPM],[CDU_TFRetCVbTPM],[CDU_TFObs],[CDU_TFRkmTen],[CDU_TFRkmCVbTen],[CDU_TFRkmAlong],[CDU_TFRkmCVb],[CDU_TQOper],[CDU_TQAspMalha],[CDU_TQTing],[CDU_TQBraq],[CDU_TQAntrac],[CDU_TQObs],[CDU_RSTestFisSit], " + " [CDU_RSTestQuiSit],[CDU_RSSitFinFio],[CDU_RSTestFisObs],[CDU_RSTestQuimObs],[CDU_RSSitFinConfor],[CDU_RSSitFinFioObs],[CDU_Avisa],[CDU_Imprime],[CDU_Obriga],[CDU_BobineReserva],[CDU_ConeMetrado],[CDU_PesoBobine],[CDU_TFPontosFinos40],[CDU_TFPontosGrossos35],[CDU_TFNeps140],[CDU_TFNeps280],[CDU_TFIndicePilosidade],[CDU_TFTorcaoTeorica],[CDU_TFRetorcaoTeorica],[CDU_TFABCD3],[CDU_TFE],[CDU_TFG],[CDU_TFI2],[CDU_TFABCD1],[CDU_TFGrauASTM],[CDU_TFAtrito],[CDU_TipoFiacao],[CDU_TipoAcabamento],[CDU_AnguloCone],[CDU_BaseMaxima],[CDU_PesoCone],[CDU_TipoNo],[CDU_Embalagem],[CDU_PontaReserva],[CDU_TFUV],[CDU_TFTorcaoRaiz],[CDU_Alerta],[CDU_IsAmostra],[CDU_DescricaoAmostra],[CDU_FornecedorAmostra],[CDU_TFTorcaoArtigo],[CDU_TFTorcaoArtigoLote], [CDU_AltDataTesteFisico] , [CDU_AltDataTesteQuimico]) " + " VALUES ";

                sInsertLabLot = sInsertLabLot + "( (SELECT MAX(CDU_NR+1) FROM TDU_LaboratorioLote ) ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_NumTestFisico"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_NumTestFisico"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_DataRelInt"].Value), "Null", " CONVERT(DATETIME,'" + Strings.Format(ConsultaSQL_ORIGEM.Fields["CDU_DataRelInt"].Value, "yyyy-MM-dd HH:mm:ss") + "' ,102) ") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_DataTestFisico"].Value), "Null", " CONVERT(DATETIME,'" + Strings.Format(ConsultaSQL_ORIGEM.Fields["CDU_DataTestFisico"].Value, "yyyy-MM-dd HH:mm:ss") + "' ,102) ") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_CodArtigo"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_CodArtigo"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_LoteArt"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_LoteArt"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_DataTestQuim"].Value), "Null", " CONVERT(DATETIME,'" + Strings.Format(ConsultaSQL_ORIGEM.Fields["CDU_DataTestQuim"].Value, "yyyy-MM-dd HH:mm:ss") + "' ,102) ") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TipoProcesso"].Value), "NULL", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TipoProcesso"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Composicao"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Composicao"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Fornec"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Fornec"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_PaisFornec"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_PaisFornec"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TipoTorcedur"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TipoTorcedur"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TipoUsoFio"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TipoUsoFio"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TipoAcond"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TipoAcond"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Obs"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Obs"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFOper"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TFOper"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFHR"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFHR"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFNETeorico"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFNETeorico"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFNe"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFNe"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFNeCVb"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFNeCVb"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFUster"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFUster"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterCVb"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterCVb"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterCVm"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterCVm"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterPontFinos"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterPontFinos"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterNeps"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterNeps"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterRCount"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterRCount"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterPontGrossos"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFUsterPontGrossos"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoTipT"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoTipT"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoTPM"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoTPM"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoCVbTPM"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoCVbTPM"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFRetorcaoTip"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TFRetorcaoTip"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFRetTPM"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFRetTPM"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFRetCVbTPM"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFRetCVbTPM"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFObs"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TFObs"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFRkmTen"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFRkmTen"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFRkmCVbTen"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFRkmCVbTen"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFRkmAlong"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFRkmAlong"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFRkmCVb"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFRkmCVb"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TQOper"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TQOper"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TQAspMalha"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TQAspMalha"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TQTing"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TQTing"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TQBraq"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TQBraq"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TQAntrac"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TQAntrac"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TQObs"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TQObs"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RSTestFisSit"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_RSTestFisSit"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RSTestQuiSit"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_RSTestQuiSit"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RSSitFinFio"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_RSSitFinFio"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RSTestFisObs"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_RSTestFisObs"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RSTestQuimObs"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_RSTestQuimObs"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RSSitFinConfor"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_RSSitFinConfor"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RSSitFinFioObs"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_RSSitFinFioObs"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Avisa"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Avisa"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Imprime"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Imprime"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Obriga"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Imprime"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_BobineReserva"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_BobineReserva"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_ConeMetrado"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_ConeMetrado"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_PesoBobine"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_PesoBobine"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFPontosFinos40"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFPontosFinos40"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFPontosGrossos35"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFPontosGrossos35"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFNeps140"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFNeps140"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFNeps280"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFNeps280"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFIndicePilosidade"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFIndicePilosidade"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoTeorica"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoTeorica"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFRetorcaoTeorica"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFRetorcaoTeorica"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFABCD3"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFABCD3"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFE"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFE"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFG"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFG"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFI2"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFI2"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFABCD1"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFABCD1"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFGrauASTM"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TFGrauASTM"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFAtrito"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFAtrito"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TipoFiacao"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TipoFiacao"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TipoAcabamento"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TipoAcabamento"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_AnguloCone"].Value), "NULL", " '" + ConsultaSQL_ORIGEM.Fields["CDU_AnguloCone"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_BaseMaxima"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_BaseMaxima"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_PesoCone"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_PesoCone"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TipoNo"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TipoNo"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Embalagem"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Embalagem"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_PontaReserva"].Value), "Null", Interaction.IIf(bool.Parse(ConsultaSQL_ORIGEM.Fields["CDU_PontaReserva"].Value.ToString()) == true, 1, 0)) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFUV"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TFUV"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoRaiz"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoRaiz"].Value.ToString(), ",", ".")) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Alerta"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Alerta"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_IsAmostra"].Value), "Null", Interaction.IIf(bool.Parse(ConsultaSQL_ORIGEM.Fields["CDU_IsAmostra"].Value.ToString()) == true, 1, 0)) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_DescricaoAmostra"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_DescricaoAmostra"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_FornecedorAmostra"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_FornecedorAmostra"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoArtigo"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoArtigo"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoArtigoLote"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TFTorcaoArtigo"].Value + "'") + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_AltDataTesteFisico"].Value), "Null", Interaction.IIf(bool.Parse(ConsultaSQL_ORIGEM.Fields["CDU_AltDataTesteFisico"].Value.ToString()) == true, 1, 0)) + " ";
                sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_AltDataTesteQuimico"].Value), "Null", Interaction.IIf(bool.Parse(ConsultaSQL_ORIGEM.Fields["CDU_AltDataTesteQuimico"].Value.ToString()) == true, 1, 0)) + " )";

                // If LigacaoSQL_ORIGEM.State = 1 Then LigacaoSQL_ORIGEM.Close
                // If LigacaoSQL_ORIGEM.State = 0 Then LigacaoSQL_ORIGEM.Open Connection_ORIGEM
                // Set ConsultaSQL_ORIGEM.ActiveConnection = LigacaoSQL_ORIGEM

                if (ConsultaSQL_ORIGEM.State == 1)
                    ConsultaSQL_ORIGEM.Close();

                // # consultar TDU_LaboratorioLoteComparacao porque vou precisar de criar o registo na minha Tabela.
                ConsultaSQL_ORIGEM.Open("SELECT * FROM TDU_LaboratorioLoteComparacao WHERE CDU_NumeroRelatorio = " + Fornecedor_Nr + " ");

                // # só entra aqui se tiver Comparações
                if (ConsultaSQL_ORIGEM.BOF != true)
                {
                    ConsultaSQL_ORIGEM.MoveFirst();

                    // # Preparar a do Insert na TDU_LaboratorioLoteComparacao
                    // # Como ainda não sei qual é o Nr que registei, coloco a TAG @1@ para substituir depois
                    sInsertLabLotComp = " INSERT INTO [dbo].[TDU_LaboratorioLoteComparacao] " + " ([CDU_NumeroRelatorio],[CDU_HR],[CDU_Ne],[CDU_NeCVb],[CDU_U],[CDU_UCVb],[CDU_CVm],[CDU_PontosFinos],[CDU_PontosGrossos],[CDU_Neps],[CDU_RKM],[CDU_RKMCVb],[CDU_Alongamento],[CDU_AlongamentoCVb],[CDU_TPI],[CDU_TPICVb], " + " [CDU_TipoTorcao],[CDU_PontosFinos40],[CDU_PontosGrossos35],[CDU_Neps140],[CDU_Neps280],[CDU_IndicePilosidade],[CDU_TorcaoTeorica],[CDU_ABCD3],[CDU_E],[CDU_G],[CDU_I2],[CDU_ABCD1],[CDU_Atrito],[CDU_GrauASTM],[CDU_AlfaTPI]) " + " VALUES (";

                    // # Será substituído mais em baixo!
                    sInsertLabLotComp = sInsertLabLotComp + "@1@";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_HR"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_HR"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Ne"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_Ne"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_NeCVb"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_NeCVb"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_U"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_U"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_UCVb"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_UCVb"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_CVm"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_CVm"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_PontosFinos"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_PontosFinos"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_PontosGrossos"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_PontosGrossos"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Neps"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_Neps"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RKM"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_RKM"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_RKMCVb"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_RKMCVb"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Alongamento"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_Alongamento"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_AlongamentoCVb"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_AlongamentoCVb"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TPI"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TPI"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TPICVb"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TPICVb"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TipoTorcao"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_TipoTorcao"].Value + "'") + " ";

                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_PontosFinos40"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_PontosFinos40"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_PontosGrossos35"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_PontosGrossos35"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Neps140"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_Neps140"].Value.ToString(), ",", ".")) + " ";

                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Neps280"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_Neps280"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_IndicePilosidade"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_IndicePilosidade"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_TorcaoTeorica"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_TorcaoTeorica"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_ABCD3"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_ABCD3"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_E"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_E"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_G"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_G"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_I2"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_I2"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_ABCD1"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_ABCD1"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Atrito"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_Atrito"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_GrauASTM"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_GrauASTM"].Value.ToString(), ",", ".")) + " ";
                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_AlfaTPI"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_AlfaTPI"].Value.ToString(), ",", ".")) + " )";



                    //'#se sim insiro o registo da empresa origem na emrpesa atual
                    //'If LigacaoSQL_DESTINO.State = 1 Then LigacaoSQL_DESTINO.Close
                    //'If LigacaoSQL_DESTINO.State = 0 Then LigacaoSQL_DESTINO.Open Connection_DESTINO
                    //'Set ConsultaSQL_DESTINO.ActiveConnection = LigacaoSQL_DESTINO
                    StringSQL_DESTINO.ActiveConnection = LigacaoSQL_DESTINO;


                    // verificar se existe o mesmo artigo na empresa atual
                    StringSQL_DESTINO.CommandText = sInsertLabLot;
                    StringSQL_DESTINO.Execute(out afetados, ref a, b);

                    UltimoNr = 0;

                    // #se sim insiro o registo da empresa origem na emrpesa atual

                    if (ConsultaSQL_DESTINO.State == 1)
                        ConsultaSQL_DESTINO.Close();

                    // # Identificar o o Nº do registo que foi criado!
                    ConsultaSQL_DESTINO.Open("SELECT MAX(CDU_NR) as Ultimo FROM TDU_LaboratorioLote");
                    if (ConsultaSQL_DESTINO.BOF == false)
                    {
                        ConsultaSQL_DESTINO.MoveFirst();
                        UltimoNr = int.Parse(ConsultaSQL_DESTINO.Fields["Ultimo"].Value.ToString());
                    }

                    // #Se inserir é que vou inserir também a comparação!!!!!!!!!! com o codigo que acabei de inserir

                    // # Caso seja necessário registar comparação..
                    if (Strings.Len(sInsertLabLotComp) > 0)
                    {

                        // # Identificar o o Nº do registo que foi criado!
                        // Set ListaConsulta = Aplicacao.BSO.Consulta("SELECT MAX(CDU_NR) as Ultimo FROM TDU_LaboratorioLote")

                        // If ListaConsulta.NumLinhas > 0 Then
                        // ListaConsulta.Inicio
                        // UltimoNr = ListaConsulta("Ultimo")
                        // Else
                        // '# Nunca irá entrar Aqui...
                        // AdicionarAviso (" A consulta para identificar o registo acabado de inserir na TDU_LaboratorioLote não retornou valores")
                        // RegistarValores = False
                        // Exit Function
                        // End If

                        if (UltimoNr == 0)
                        {
                            // # Nunca irá entrar Aqui...
                            AdicionarAviso(" A consulta para identificar o registo acabado de inserir na TDU_LaboratorioLote não retornou valores");
                            return false;
                        }


                        // # Substituir a TAG @1@ pelo número do ul
                        sInsertLabLotComp = Strings.Replace(sInsertLabLotComp, "@1@", UltimoNr.ToString());



                        //'If LigacaoSQL_DESTINO.State = 1 Then LigacaoSQL_DESTINO.Close
                        //'If LigacaoSQL_DESTINO.State = 0 Then LigacaoSQL_DESTINO.Open Connection_DESTINO
                        //'Set ConsultaSQL_DESTINO.ActiveConnection = LigacaoSQL_DESTINO
                        StringSQL_DESTINO.ActiveConnection = LigacaoSQL_DESTINO;


                        // #Antes de inserir garanto que não existe esta chave (nº)
                        StringSQL_DESTINO.CommandText = " DELETE from TDU_LaboratorioLoteComparacao where CDU_NumeroRelatorio = " + UltimoNr + " ";
                        StringSQL_DESTINO.Execute(out afetados, ref a, b);

                        // # Inserir na tabela de Comparacao
                        StringSQL_DESTINO.CommandText = sInsertLabLotComp;
                        StringSQL_DESTINO.Execute(out afetados, ref a, b);
                    }

                    RegistarAnexos(Fornecedor_Nr, UltimoNr);
                    if (LigacaoSQL_ORIGEM.State == 1)
                        LigacaoSQL_ORIGEM.Close();

                    return true;

                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                AdicionarAviso(ex.ToString());
                return false;
            }
        }



        private static bool ValidaTipoProcesso(string Processo)
        {
            string SqlString;

            if (Strings.Len(Processo) == 0)
            {
                return true;
            }

            // #Definir a query
            SqlString = "SELECT CDU_Tipo FROM TDU_TipoProcesso Where CDU_Tipo = '@1@' ";

            // # Atribuir os filtros
            SqlString = Strings.Replace(SqlString, "@1@", Processo);

            // # Fazer Consulta
            if (ConsultaSQL_DESTINO.State == 1)
                ConsultaSQL_DESTINO.Close();

            // verificar se existe o mesmo artigo na empresa atual
            ConsultaSQL_DESTINO.Open(SqlString);

            // # se exitir
            if (ConsultaSQL_DESTINO.BOF == false)
            {
                // # retorna true
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();

                return true;
            }
            else
            {
                AdicionarAviso("O Processo com o código " + Processo + " não existe.");

                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();

                return false;
            }
            // ConsultaSQL_DESTINO.Close

        }

        private static bool ValidaComposicao(string Composicao)
        {
            StdBELista ListaConsulta;
            string SqlString;

            // # Se não tiver composição, retorna verdadeiro
            if (Strings.Len(Composicao) == 0)
            {
                return true;
            }

            // #Definir a query
            SqlString = "SELECT CDU_Composicao FROM TDU_Composicoes Where CDU_Composicao = '@1@' ";

            // # Atribuir os filtros
            SqlString = Strings.Replace(SqlString, "@1@", Composicao);



            ListaConsulta = PriV100Api.BSO.Consulta(SqlString);


            // # se exitir
            if (ListaConsulta.NumLinhas() > 0)
            {
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                // # retorna true
                return true;
            }
            else
            {
                AdicionarAviso("A Composicao com o código " + Composicao + " não existe.");
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                return false;
            }


        }


        private static bool ValidaFornecedor(ref string Fornecedor)
        {
            string SqlString;

            if (Strings.Len(Fornecedor) == 0)
            {
                return true;
            }

            // #Definir a query
            SqlString = "SELECT Fornecedor FROM Fornecedores Where Fornecedor = '@1@' ";

            // # Atribuir os filtros
            SqlString = Strings.Replace(SqlString, "@1@", Fornecedor);

            if (ConsultaSQL_DESTINO.State == 1)
                ConsultaSQL_DESTINO.Close();

            // # Fazer Consulta
            ConsultaSQL_DESTINO.Open(SqlString);

            // # se exitir
            if (ConsultaSQL_DESTINO.BOF == false)
            {
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                // # retorna true
                return true;
            }
            else
            {

                // Alteração pedida por Eng. Joaquim 10/04/2017
                // AdicionarAviso ("O Fornecedor com o código " & Fornecedor & " não existe.")

                Fornecedor = FornecedorIndiferenciado;
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                return false;
            }


        }

        private static bool ValidaTipoTorcedur(string TipoTorc)
        {
            string SqlString;

            if (Strings.Len(TipoTorc) == 0)
            {
                return true;
            }

            // #Definir a query
            SqlString = "SELECT CDU_Tipo FROM TDU_TipoTorcedura where CDU_Tipo = '@1@' ";

            // # Atribuir os filtros
            SqlString = Strings.Replace(SqlString, "@1@", TipoTorc);

            if (ConsultaSQL_DESTINO.State == 1)
                ConsultaSQL_DESTINO.Close();

            // # Fazer Consulta
            ConsultaSQL_DESTINO.Open(SqlString);

            // # se exitir
            if (ConsultaSQL_DESTINO.BOF == false)
            {
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                // # retorna true
                return true;
            }
            else
            {
                AdicionarAviso("O TipoTorcedura com o código " + TipoTorc + " não existe.");
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                return false;
            }


        }



        private static bool ValidaTipoUsoFio(string TipoUsoFio)
        {
            string SqlString;

            if (Strings.Len(TipoUsoFio) == 0)
            {
                return true;
            }

            // #Definir a query
            SqlString = "SELECT * FROM TDU_TipoUsoFio WHERE CDU_Tipo = @1@ ";

            // # Atribuir os filtros
            SqlString = Strings.Replace(SqlString, "@1@", TipoUsoFio);

            // # Fazer Consulta
            if (ConsultaSQL_DESTINO.State == 1)
                ConsultaSQL_DESTINO.Close();

            // # Fazer Consulta
            ConsultaSQL_DESTINO.Open(SqlString);

            // # se exitir
            if (ConsultaSQL_DESTINO.BOF == false)
            {
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                // # retorna true
                return true;
            }
            else
            {
                AdicionarAviso("O Tipo de Uso de Fio com o código " + TipoUsoFio + " não existe.");
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                return false;
            }


        }


        private static bool ValidaTipoAcondicionamento(string TipoAcond)
        {
            string SqlString;

            if (Strings.Len(TipoAcond) == 0)
            {
                return true;
            }

            // #Definir a query
            SqlString = "SELECT * FROM TDU_TipoAcondicionamento WHERE CDU_Tipo = @1@";

            // # Atribuir os filtros
            SqlString = Strings.Replace(SqlString, "@1@", TipoAcond);

            if (ConsultaSQL_DESTINO.State == 1)
                ConsultaSQL_DESTINO.Close();
            // # Fazer Consulta
            ConsultaSQL_DESTINO.Open(SqlString);

            // # se exitir
            if (ConsultaSQL_DESTINO.BOF == false)
            {
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                // # retorna true
                return true;
            }
            else
            {
                AdicionarAviso("O Tipo de Acondicionamento " + TipoAcond + " não existe.");
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                return false;
            }


        }



        private static bool ValidaAngulosCone(ref string AnguloCone)
        {
            string SqlString;

            if (Strings.Len(AnguloCone) == 0)
            {
                return true;
            }

            // #Definir a query
            SqlString = "SELECT * FROM TDU_AngulosCone WHERE CDU_Codigo = @1@ ";

            // # Atribuir os filtros
            SqlString = Strings.Replace(SqlString, "@1@", AnguloCone);

            if (ConsultaSQL_DESTINO.State == 1)
                ConsultaSQL_DESTINO.Close();

            // # Fazer Consulta
            ConsultaSQL_DESTINO.Open(SqlString);

            // # se exitir
            if (ConsultaSQL_DESTINO.BOF == false)
            {
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                // # retorna true
                return true;
            }
            else
            {
                AnguloCone = "";
                // Alteração pedida por Eng. Joaquim 10/04/2017
                // AdicionarAviso ("O Angulo Cone " & TipoTorc & " não existe.")
                if (ConsultaSQL_DESTINO.State == 1)
                    ConsultaSQL_DESTINO.Close();
                return false;
            }


        }

        private static void AdicionarAviso(string Aviso)
        {
            StrAvisos = StrAvisos + Constants.vbNewLine + Aviso;
        }


        private static bool RegistarAnexos(long NumeroRelatorioOrigem, long NumeroRelatorio)
        {
            string sInsertLabLotAnexos; // Query usada para a TDU_LaboratorioLoteComparacao

            try
            {


                if (ConsultaSQL_ORIGEM.State == 1)
                    ConsultaSQL_ORIGEM.Close();

                // # consultar TDU_LaboratorioLoteComparacao porque vou precisar de criar o registo na minha Tabela.
                ConsultaSQL_ORIGEM.Open("SELECT * FROM TDU_AnexosLaboratorioLote WHERE CDU_AnexoNumRelatorio = " + NumeroRelatorioOrigem + " ");

                // # só entra aqui se tiver Comparações
                if (ConsultaSQL_ORIGEM.BOF != true)
                {
                    ConsultaSQL_ORIGEM.MoveFirst();



                    StringSQL_DESTINO.ActiveConnection = LigacaoSQL_DESTINO;


                    // Apagar possíveis anexos que existam....
                    StringSQL_DESTINO.CommandText = "DELETE from TDU_AnexosLaboratorioLote where CDU_AnexoNumRelatorio = " + NumeroRelatorio + " ";
                    StringSQL_DESTINO.Execute(out afetados, ref a, b);
                    while (!ConsultaSQL_ORIGEM.EOF)
                    {

                        // # Preparar a do Insert na TDU_LaboratorioLoteComparacao
                        // # Como ainda não sei qual é o Nr que registei, coloco a TAG @1@ para substituir depois
                        sInsertLabLotAnexos = "INSERT INTO [dbo].[TDU_AnexosLaboratorioLote] ([CDU_Anexo] ,[CDU_AnexoNumRelatorio] ,[CDU_Caminho] ,[CDU_NomeFicheiro],[CDU_DataAnexo])" + " Values (";

                        sInsertLabLotAnexos = sInsertLabLotAnexos + " " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Anexo"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_Anexo"].Value.ToString(), ",", ".")) + " ";
                        sInsertLabLotAnexos = sInsertLabLotAnexos + "," + NumeroRelatorio + " ";
                        sInsertLabLotAnexos = sInsertLabLotAnexos + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_Caminho"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_Caminho"].Value + "'") + " ";
                        sInsertLabLotAnexos = sInsertLabLotAnexos + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_NomeFicheiro"].Value), "Null", " '" + ConsultaSQL_ORIGEM.Fields["CDU_NomeFicheiro"].Value + "'") + " ";
                        sInsertLabLotAnexos = sInsertLabLotAnexos + ", " + Interaction.IIf(Convert.IsDBNull(ConsultaSQL_ORIGEM.Fields["CDU_DataAnexo"].Value), "Null", Strings.Replace(ConsultaSQL_ORIGEM.Fields["CDU_DataAnexo"].Value.ToString(), ",", ".")) + " ";
                        sInsertLabLotAnexos = sInsertLabLotAnexos + " )";

                        // #Inserir o registo
                        StringSQL_DESTINO.CommandText = sInsertLabLotAnexos;
                        StringSQL_DESTINO.Execute(out afetados,ref a,b);
                        // Avançar o apontador
                        ConsultaSQL_ORIGEM.MoveNext();
                    }
                }

                if (ConsultaSQL_ORIGEM.State == 1)
                    ConsultaSQL_ORIGEM.Close();

                return true;
            }
            catch (Exception ex)
            {
                AdicionarAviso(ex.ToString());
                return false;
            }
        }
    }
}