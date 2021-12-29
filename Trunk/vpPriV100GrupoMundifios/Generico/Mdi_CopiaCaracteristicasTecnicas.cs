//using Microsoft.VisualBasic;
//using StdBE100;
//using System;
//using System.Data.SqlClient;
//using System.Windows.Forms;

//namespace Generico
//{
//    internal static class Mdi_CopiaCaracteristicasTecnicas
//    {
//        public const string FornecedorIndiferenciado = "FVD";
//        private static Recordset ConsultaSQL_PRIEMPRE = new Recordset();
//        private static SqlConnection LigacaoSQL_PRIEMPRE = new SqlConnection();
//        private static string Connection_PRIEMPRE;
//        private static SqlCommand StringSQL_PRIEMPRE = new SqlCommand();

//        // # Abrir ligaçao à base de dados remota
//        private static Recordset ConsultaSQL_ORIGEM = new Recordset();

//        private static SqlConnection LigacaoSQL_ORIGEM = new SqlConnection();
//        private static string Connection_ORIGEM;
//        private static SqlCommand StringSQL_ORIGEM = new SqlCommand();

//        private static Recordset ConsultaSQL_DESTINO = new Recordset();
//        private static SqlConnection LigacaoSQL_DESTINO = new SqlConnection();
//        private static string Connection_DESTINO;
//        private static SqlCommand StringSQL_DESTINO = new SqlCommand();

//        private static string NomeEmpresa_ORIGEM;
//        private static string Instancia_ORIGEM;

//        private static string Instancia_DESTINO;
//        // NomeEmpresa_DESTINO -> Passada por parametro

//        private static string StrAvisos;

//        public const string Str_PRIEMPRE_INSTANCIA = @"192.168.1.6\PRILPV900";
//        public const string Str_PRIEMPRE_PASSWORD = "VIMAPRILPV900";

//        public const string PASSWORD_ORIGEM = "VIMAPRILPV900";
//        public const string PASSWORD_DESTINO = "VIMAPRILPV900";

//        public static ErpBS100.ErpBS BSO = new ErpBS100.ErpBS();

//        public static bool CopiarCaractTec(string NomeEmpresa_DESTINO, CmpBE100.CmpBEDocumentoCompra DocumentoCompra)
//        {
//            string ArtigoaCopiar=""; // Importante para se der algum erro/Problema, saber que artigo estava a copiar

//            if (Module1.AbreEmpresa(NomeEmpresa_DESTINO))
//            {
//                switch (BSO.Base.Fornecedores.Edita(DocumentoCompra.Entidade).CamposUtil["CDU_EntidadeInterna"].Valor)
//                {
//                    case "0001":
//                        {
//                            NomeEmpresa_ORIGEM = "MUNDIFIOS";
//                            break;
//                        }

//                    case "0002":
//                        {
//                            NomeEmpresa_ORIGEM = "INOVAFIL";
//                            break;
//                        }

//                    case "0003":
//                        {
//                            NomeEmpresa_ORIGEM = "AVEFIOS";
//                            break;
//                        }

//                    case "0004":
//                        {
//                            NomeEmpresa_ORIGEM = "YARNTRADE";
//                            break;
//                        }

//                    case "0006":
//                        {
//                            NomeEmpresa_ORIGEM = "MUNDITALIA";
//                            break;
//                        }

//                    case "0007":
//                        {
//                            NomeEmpresa_ORIGEM = "MIXYARN";
//                            break;
//                        }

//                    default:
//                        {
//                            return true;
//                        }
//                }
//            }

//            Module1.FechaEmpresa();

//            try
//            {
//                Connection_PRIEMPRE = "Provider=SQLOLEDB.1;Password=" + Str_PRIEMPRE_PASSWORD + ";Persist Security Info=True;User ID=sa;Initial Catalog=PRIEMPRE;Data Source=" + Str_PRIEMPRE_INSTANCIA + ";connection timeout=0";

//                if (LigacaoSQL_PRIEMPRE.State == 0)
//                    LigacaoSQL_PRIEMPRE.Open(Connection_PRIEMPRE);
//                if (ConsultaSQL_PRIEMPRE.State == 1)
//                    ConsultaSQL_PRIEMPRE.Close();
//                ConsultaSQL_PRIEMPRE.ActiveConnection = LigacaoSQL_PRIEMPRE;

//                if (!IdentificarConnection_ORIGEM(NomeEmpresa_ORIGEM))
//                    goto Avisos;

//                if (!IdentificarConnection_DESTINO(NomeEmpresa_DESTINO))
//                    goto Avisos;

//                ConsultaSQL_PRIEMPRE.Close();

//                for (int i = 1, loopTo = DocumentoCompra.Linhas.NumItens; i <= loopTo; i++)
//                {
//                    if (DocumentoCompra.Linhas.GetEdita(i).TipoLinha != "60")
//                    {
//                        ArtigoaCopiar = DocumentoCompra.Linhas.GetEdita(i).Artigo;
//                        if (!RegistarValores(DocumentoCompra.Linhas.GetEdita(i).Artigo, DocumentoCompra.Linhas.GetEdita(i).Lote))
//                            goto Avisos;
//                    }
//                }

//                FecharConnections();

//                return true;

//            }
//            catch
//            {
//                Interaction.MsgBox(Information.Err.Description, Constants.vbCritical, "Copiar Características Técnicas");
//                return false;

//            }

//            Avisos:
//            if (Strings.Len(StrAvisos) > 0)
//            {
//                MessageBox.Show(StrAvisos, "Características do Artigo: " + ArtigoaCopiar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                StrAvisos = "";
//            }

//            if (MsgBox("Não foi possível realizar a cópia de características!" + Constants.vbNewLine + "Deseja mesmo assim gravar o documento?", (int)Constants.vbQuestion + (int)Constants.vbYesNo) == Constants.vbNo)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }

//            FecharConnections();
//        }

//        private static void FecharConnections()
//        {
//            if (ConsultaSQL_PRIEMPRE.State == 1)
//                ConsultaSQL_PRIEMPRE.Close();
//            if (ConsultaSQL_ORIGEM.State == 1)
//                ConsultaSQL_ORIGEM.Close();
//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();

//            if (LigacaoSQL_ORIGEM.State == System.Data.ConnectionState.Open)
//                LigacaoSQL_ORIGEM.Close();
//            if (LigacaoSQL_DESTINO.State == System.Data.ConnectionState.Open)
//                LigacaoSQL_DESTINO.Close();
//            if (LigacaoSQL_PRIEMPRE.State == System.Data.ConnectionState.Open)
//                LigacaoSQL_PRIEMPRE.Close();

//            StrAvisos = "";
//        }

//        private static bool IdentificarConnection_DESTINO(string NomeEmpresa_DESTINO)
//        {
//            if (ConsultaSQL_PRIEMPRE.State == 1)
//                ConsultaSQL_PRIEMPRE.Close();

//            // #Consultar a instancia da empresa grupo
//            ConsultaSQL_PRIEMPRE.Open("SELECT *  FROM TDU_EmpresasGrupo Where CDU_Empresa = '" + NomeEmpresa_DESTINO + "'");

//            // # Se existir esta empresa nas empresas de Grupo
//            if (ConsultaSQL_PRIEMPRE.BOF != true)
//            {
//                ConsultaSQL_PRIEMPRE.MoveFirst();

//                // # Identifico a instancia
//                Instancia_DESTINO = ConsultaSQL_PRIEMPRE("CDU_Instancia");

//                switch (Instancia_DESTINO)
//                {
//                    case @".\PRILPV900":
//                    case @"192.168.1.6\PRILPV900" // #Cliente
//                   :
//                        {
//                            Connection_DESTINO = "Provider=SQLOLEDB.1;Password=" + PASSWORD_DESTINO + ";Persist Security Info=True;User ID=sa;Initial Catalog=" + "PRI" + ConsultaSQL_PRIEMPRE("CDU_Empresa") + ";Data Source=" + Instancia_DESTINO + ";connection timeout=0";
//                            return true;
//                        }

//                    default:
//                        {
//                            MessageBox.Show("A intancia " + Instancia_DESTINO + " está configurada na PRIEMRPE e não está configurada no VBA.", "",MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return false;
//                        }
//                }
//            }
//            else
//            {
//                AdicionarAviso("A Empresa para onde as caraceterístas vão ser copiadas '" + NomeEmpresa_DESTINO + "' não existe na PRIEMRE.");
//                return false;
//            }
//        }

//        private static bool IdentificarConnection_ORIGEM(string NomeEmpresa_ORIGEM)
//        {
//            if (ConsultaSQL_PRIEMPRE.State == 1)
//                ConsultaSQL_PRIEMPRE.Close();

//            // #Consultar a instancia da empresa grupo
//            ConsultaSQL_PRIEMPRE.Open("SELECT *  FROM TDU_EmpresasGrupo Where CDU_Empresa = '" + NomeEmpresa_ORIGEM + "'");

//            // # Se existir esta empresa nas empresas de Grupo
//            if (ConsultaSQL_PRIEMPRE.BOF != true)
//            {
//                ConsultaSQL_PRIEMPRE.MoveFirst();

//                // # Identifico a instancia
//                Instancia_ORIGEM = ConsultaSQL_PRIEMPRE("CDU_Instancia");

//                switch (Instancia_ORIGEM)
//                {
//                    case @".\PRILPV900":
//                    case @"192.168.1.6\PRILPV900" // #Cliente
//                   :
//                        {
//                            Connection_ORIGEM = "Provider=SQLOLEDB.1;Password=" + PASSWORD_ORIGEM + ";Persist Security Info=True;User ID=sa;Initial Catalog=" + "PRI" + ConsultaSQL_PRIEMPRE("CDU_Empresa") + ";Data Source=" + Instancia_ORIGEM + ";connection timeout=0";
//                            return true;
//                        }

//                    default:
//                        {
//                            MessageBox.Show("A intancia " + Instancia_ORIGEM + " está configurada na PRIEMPRE e não está configurada no VBA.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            return false;
//                        }
//                }
//            }
//            else
//            {
//                AdicionarAviso("O Fornecedor com o código '" + NomeEmpresa_ORIGEM + "' não existe na PRIEMPRE.");
//                return false;
//            }
//        }

//        private static void AdicionarAviso(string Aviso)
//        {
//            StrAvisos = StrAvisos + Constants.vbNewLine + Aviso;
//        }

//        private static void RegistarValores(string Artigo, string lote)
//        {
//            StdBELista ListaConsulta;
//            string sInsertLabLot; // Query usada para a TDU_LaboratorioLote
//            string sInsertLabLotComp; // Query usada para a TDU_LaboratorioLoteComparacao
//            long UltimoNr; // Depois de inserir na tabela TDU_LaboratorioLote preciso saber o Nr que inseriu para inserir na tabela de Comparacoes
//            long Fornecedor_Nr; // Depois de identificar o registo na LaboratorioLote através do Artigo + Lote, guardo nesta variável a chave para depois ir ler as comparações
//            DateTime Fornecedor_DataRelInt; // Necessário para depois comparar com a Data da empresa Atual

//            string Fornecedor;
//            string AngulosCone;
//            Fornecedor = "";
//            AngulosCone = "";
//            try
//            {
//                if (LigacaoSQL_ORIGEM.State == System.Data.ConnectionState.Open)
//                    LigacaoSQL_ORIGEM.Close();
//                if (LigacaoSQL_ORIGEM.State == System.Data.ConnectionState.Closed)
//                    LigacaoSQL_ORIGEM.Open(Connection_ORIGEM);

//                        ConsultaSQL_ORIGEM.ActiveConnection = LigacaoSQL_ORIGEM;

//                ConsultaSQL_ORIGEM.Open("SELECT *  FROM TDU_LaboratorioLote where CDU_CodArtigo = '" + Artigo + "' and CDU_LoteArt = '" + lote + "' order by CDU_DataRelInt DESC");

//                if (ConsultaSQL_ORIGEM.BOF == true)
//                {
//                    return true;
//                }

//                ConsultaSQL_ORIGEM.MoveFirst();

//                Fornecedor_Nr = ConsultaSQL_ORIGEM("CDU_NR");
//                Fornecedor_DataRelInt = ConsultaSQL_ORIGEM("CDU_DataRelInt");

//                if (LigacaoSQL_DESTINO.State == System.Data.ConnectionState.Open)
//                    LigacaoSQL_DESTINO.Close();
//                if (LigacaoSQL_DESTINO.State == 0)
//                    LigacaoSQL_DESTINO.Open(Connection_DESTINO);

//                ConsultaSQL_DESTINO.ActiveConnection = LigacaoSQL_DESTINO;

//                if (ConsultaSQL_DESTINO.State == 1)
//                    ConsultaSQL_DESTINO.Close();

//                ConsultaSQL_DESTINO.Open("SELECT CDU_DataRelInt  FROM TDU_LaboratorioLote where CDU_CodArtigo = '" + Artigo + "' and CDU_LoteArt = '" + lote + "' order by CDU_DataRelInt DESC");

//                if (ConsultaSQL_DESTINO.BOF == false)
//                {
//                    ConsultaSQL_DESTINO.MoveFirst();

//                    // #        Data no FORNECEDOR    >  DATA EMPRESA ATUAL
//                    if (DateDiff("s", Conversions.ToDate(Fornecedor_DataRelInt), Conversions.ToDate(ConsultaSQL_DESTINO("CDU_DataRelInt"))) >= 0)
//                    {
//                        return true;
//                    }
//                }
//                    if (ConsultaSQL_DESTINO.State == 1)
//                        ConsultaSQL_DESTINO.Close();

//                    if (!ValidaTipoProcesso(ConsultaSQL_ORIGEM("CDU_TipoProcesso") + ""))
//                        return;
//                    // A pedido do Eng.º Pedro da Mundifios a 25-11-2015 foi ignorada esta tabela (ver mail)
//                    // If Not ValidaComposicao(ConsultaSQL_ORIGEM("CDU_Composicao") & "") Then Exit Function
//                    Fornecedor = ConsultaSQL_ORIGEM("CDU_Fornec") + "";
//                    if (!ValidaFornecedor(Fornecedor))
//                        return;
//                    if (!ValidaTipoTorcedur(ConsultaSQL_ORIGEM("CDU_TipoTorcedur") + ""))
//                        return;
//                    if (!ValidaTipoUsoFio(ConsultaSQL_ORIGEM("CDU_TipoUsoFio") + ""))
//                        return;
//                    if (!ValidaTipoAcondicionamento(ConsultaSQL_ORIGEM("CDU_TipoAcond") + ""))
//                        return;
//                    AngulosCone = ConsultaSQL_ORIGEM("CDU_AnguloCone") + "";
//                    if (!ValidaAngulosCone(AngulosCone))
//                        return;

//                    // # Preparar a Query do Insert
//                    sInsertLabLot = " INSERT INTO [dbo].[TDU_LaboratorioLote] " + " ([CDU_NR],[CDU_NumTestFisico],[CDU_DataRelInt],[CDU_DataTestFisico],[CDU_CodArtigo],[CDU_LoteArt],[CDU_DataTestQuim],[CDU_TipoProcesso],[CDU_Composicao],[CDU_Fornec],[CDU_PaisFornec],[CDU_TipoTorcedur],[CDU_TipoUsoFio],[CDU_TipoAcond],[CDU_Obs],[CDU_TFOper],[CDU_TFHR],[CDU_TFNETeorico],[CDU_TFNe],[CDU_TFNeCVb],[CDU_TFUster],[CDU_TFUsterCVb],[CDU_TFUsterCVm],[CDU_TFUsterPontFinos],[CDU_TFUsterNeps],[CDU_TFUsterRCount],[CDU_TFUsterPontGrossos],[CDU_TFTorcaoTipT],[CDU_TFTorcaoTPM],[CDU_TFTorcaoCVbTPM],[CDU_TFRetorcaoTip],[CDU_TFRetTPM],[CDU_TFRetCVbTPM],[CDU_TFObs],[CDU_TFRkmTen],[CDU_TFRkmCVbTen],[CDU_TFRkmAlong],[CDU_TFRkmCVb],[CDU_TQOper],[CDU_TQAspMalha],[CDU_TQTing],[CDU_TQBraq],[CDU_TQAntrac],[CDU_TQObs],[CDU_RSTestFisSit], " + " [CDU_RSTestQuiSit],[CDU_RSSitFinFio],[CDU_RSTestFisObs],[CDU_RSTestQuimObs],[CDU_RSSitFinConfor],[CDU_RSSitFinFioObs],[CDU_Avisa],[CDU_Imprime],[CDU_Obriga],[CDU_BobineReserva],[CDU_ConeMetrado],[CDU_PesoBobine],[CDU_TFPontosFinos40],[CDU_TFPontosGrossos35],[CDU_TFNeps140],[CDU_TFNeps280],[CDU_TFIndicePilosidade],[CDU_TFTorcaoTeorica],[CDU_TFRetorcaoTeorica],[CDU_TFABCD3],[CDU_TFE],[CDU_TFG],[CDU_TFI2],[CDU_TFABCD1],[CDU_TFGrauASTM],[CDU_TFAtrito],[CDU_TipoFiacao],[CDU_TipoAcabamento],[CDU_AnguloCone],[CDU_BaseMaxima],[CDU_PesoCone],[CDU_TipoNo],[CDU_Embalagem],[CDU_PontaReserva],[CDU_TFUV],[CDU_TFTorcaoRaiz],[CDU_Alerta],[CDU_IsAmostra],[CDU_DescricaoAmostra],[CDU_FornecedorAmostra],[CDU_TFTorcaoArtigo],[CDU_TFTorcaoArtigoLote], [CDU_AltDataTesteFisico] , [CDU_AltDataTesteQuimico]) " + " VALUES ";

//                    sInsertLabLot = sInsertLabLot + "( (SELECT MAX(CDU_NR+1) FROM TDU_LaboratorioLote ) ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_NumTestFisico")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_NumTestFisico") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_DataRelInt")), "Null", " CONVERT(DATETIME,'" + Strings.Format(ConsultaSQL_ORIGEM("CDU_DataRelInt"), "yyyy-MM-dd HH:mm:ss") + "' ,102) ") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_DataTestFisico")), "Null", " CONVERT(DATETIME,'" + Strings.Format(ConsultaSQL_ORIGEM("CDU_DataTestFisico"), "yyyy-MM-dd HH:mm:ss") + "' ,102) ") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_CodArtigo")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_CodArtigo") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_LoteArt")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_LoteArt") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_DataTestQuim")), "Null", " CONVERT(DATETIME,'" + Strings.Format(ConsultaSQL_ORIGEM("CDU_DataTestQuim"), "yyyy-MM-dd HH:mm:ss") + "' ,102) ") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TipoProcesso")), "NULL", " '" + ConsultaSQL_ORIGEM("CDU_TipoProcesso") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Composicao")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Composicao") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Fornec")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Fornec") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_PaisFornec")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_PaisFornec") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TipoTorcedur")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TipoTorcedur") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TipoUsoFio")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TipoUsoFio") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TipoAcond")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TipoAcond") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Obs")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Obs") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFOper")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TFOper") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFHR")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFHR"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFNETeorico")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFNETeorico"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFNe")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFNe"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFNeCVb")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFNeCVb"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFUster")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFUster"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFUsterCVb")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFUsterCVb"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFUsterCVm")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFUsterCVm"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFUsterPontFinos")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFUsterPontFinos"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFUsterNeps")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFUsterNeps"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFUsterRCount")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFUsterRCount"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFUsterPontGrossos")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFUsterPontGrossos"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFTorcaoTipT")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TFTorcaoTipT") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFTorcaoTPM")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFTorcaoTPM"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFTorcaoCVbTPM")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFTorcaoCVbTPM"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFRetorcaoTip")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TFRetorcaoTip") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFRetTPM")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFRetTPM"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFRetCVbTPM")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFRetCVbTPM"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFObs")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TFObs") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFRkmTen")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFRkmTen"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFRkmCVbTen")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFRkmCVbTen"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFRkmAlong")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFRkmAlong"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFRkmCVb")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFRkmCVb"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TQOper")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TQOper") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TQAspMalha")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TQAspMalha") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TQTing")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TQTing") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TQBraq")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TQBraq") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TQAntrac")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TQAntrac") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TQObs")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TQObs") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RSTestFisSit")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_RSTestFisSit") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RSTestQuiSit")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_RSTestQuiSit") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RSSitFinFio")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_RSSitFinFio") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RSTestFisObs")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_RSTestFisObs") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RSTestQuimObs")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_RSTestQuimObs") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RSSitFinConfor")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_RSSitFinConfor") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RSSitFinFioObs")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_RSSitFinFioObs") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Avisa")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Avisa") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Imprime")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Imprime") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Obriga")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Imprime") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_BobineReserva")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_BobineReserva") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_ConeMetrado")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_ConeMetrado") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_PesoBobine")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_PesoBobine") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFPontosFinos40")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFPontosFinos40"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFPontosGrossos35")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFPontosGrossos35"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFNeps140")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFNeps140"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFNeps280")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFNeps280"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFIndicePilosidade")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFIndicePilosidade"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFTorcaoTeorica")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFTorcaoTeorica"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFRetorcaoTeorica")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFRetorcaoTeorica"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFABCD3")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFABCD3"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFE")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFE"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFG")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFG"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFI2")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFI2"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFABCD1")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFABCD1"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFGrauASTM")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TFGrauASTM") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFAtrito")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFAtrito"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TipoFiacao")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TipoFiacao") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TipoAcabamento")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TipoAcabamento") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_AnguloCone")), "NULL", " '" + ConsultaSQL_ORIGEM("CDU_AnguloCone") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_BaseMaxima")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_BaseMaxima"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_PesoCone")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_PesoCone"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TipoNo")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TipoNo") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Embalagem")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Embalagem") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_PontaReserva")), "Null", Interaction.IIf(ConsultaSQL_ORIGEM("CDU_PontaReserva") == true, 1, 0)) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFUV")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TFUV") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFTorcaoRaiz")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TFTorcaoRaiz"), ",", ".")) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Alerta")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Alerta") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_IsAmostra")), "Null", Interaction.IIf(ConsultaSQL_ORIGEM("CDU_IsAmostra") == true, 1, 0)) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_DescricaoAmostra")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_DescricaoAmostra") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_FornecedorAmostra")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_FornecedorAmostra") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFTorcaoArtigo")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TFTorcaoArtigo") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TFTorcaoArtigoLote")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TFTorcaoArtigo") + "'") + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_AltDataTesteFisico")), "Null", Interaction.IIf(ConsultaSQL_ORIGEM("CDU_AltDataTesteFisico") == true, 1, 0)) + " ";
//                    sInsertLabLot = sInsertLabLot + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_AltDataTesteQuimico")), "Null", Interaction.IIf(ConsultaSQL_ORIGEM("CDU_AltDataTesteQuimico") == true, 1, 0)) + " )";

//                if (ConsultaSQL_ORIGEM.State == 1)
//                    ConsultaSQL_ORIGEM.Close();

//                ConsultaSQL_ORIGEM.Open("SELECT * FROM TDU_LaboratorioLoteComparacao WHERE CDU_NumeroRelatorio = " + Fornecedor_Nr + " ");

//                if (ConsultaSQL_ORIGEM.BOF != true)
//                {
//                    ConsultaSQL_ORIGEM.MoveFirst();

//                    // # Preparar a do Insert na TDU_LaboratorioLoteComparacao
//                    // # Como ainda não sei qual é o Nr que registei, coloco a TAG @1@ para substituir depois
//                    sInsertLabLotComp = " INSERT INTO [dbo].[TDU_LaboratorioLoteComparacao] " + " ([CDU_NumeroRelatorio],[CDU_HR],[CDU_Ne],[CDU_NeCVb],[CDU_U],[CDU_UCVb],[CDU_CVm],[CDU_PontosFinos],[CDU_PontosGrossos],[CDU_Neps],[CDU_RKM],[CDU_RKMCVb],[CDU_Alongamento],[CDU_AlongamentoCVb],[CDU_TPI],[CDU_TPICVb], " + " [CDU_TipoTorcao],[CDU_PontosFinos40],[CDU_PontosGrossos35],[CDU_Neps140],[CDU_Neps280],[CDU_IndicePilosidade],[CDU_TorcaoTeorica],[CDU_ABCD3],[CDU_E],[CDU_G],[CDU_I2],[CDU_ABCD1],[CDU_Atrito],[CDU_GrauASTM],[CDU_AlfaTPI]) " + " VALUES (";

//                    // # Será substituído mais em baixo!
//                    sInsertLabLotComp = sInsertLabLotComp + "@1@";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_HR")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_HR"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Ne")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_Ne"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_NeCVb")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_NeCVb"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_U")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_U"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_UCVb")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_UCVb"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_CVm")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_CVm"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_PontosFinos")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_PontosFinos"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_PontosGrossos")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_PontosGrossos"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Neps")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_Neps"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RKM")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_RKM"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_RKMCVb")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_RKMCVb"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Alongamento")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_Alongamento"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_AlongamentoCVb")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_AlongamentoCVb"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TPI")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TPI"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TPICVb")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TPICVb"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TipoTorcao")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_TipoTorcao") + "'") + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_PontosFinos40")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_PontosFinos40"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_PontosGrossos35")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_PontosGrossos35"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Neps140")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_Neps140"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Neps280")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_Neps280"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_IndicePilosidade")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_IndicePilosidade"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_TorcaoTeorica")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_TorcaoTeorica"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_ABCD3")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_ABCD3"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_E")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_E"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_G")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_G"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_I2")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_I2"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_ABCD1")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_ABCD1"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Atrito")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_Atrito"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_GrauASTM")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_GrauASTM"), ",", ".")) + " ";
//                    sInsertLabLotComp = sInsertLabLotComp + ", " + Interaction.IIf(String.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_AlfaTPI")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_AlfaTPI"), ",", ".")) + " )";
//                }

//                StringSQL_DESTINO.ActiveConnection = LigacaoSQL_DESTINO;

//                StringSQL_DESTINO.CommandText = sInsertLabLot;
//                StringSQL_DESTINO.Execute;

//            UltimoNr = 0;

//                if (ConsultaSQL_DESTINO.State = 1)
//                    ConsultaSQL_DESTINO.Close();

//                ConsultaSQL_DESTINO.Open("SELECT MAX(CDU_NR) as Ultimo FROM TDU_LaboratorioLote");

//                if (ConsultaSQL_DESTINO.BOF == false)
//                {
//                    ConsultaSQL_DESTINO.MoveFirst();
//                    UltimoNr = ConsultaSQL_DESTINO("Ultimo");
//                }

//                // # Caso seja necessário registar comparação..
//                if (Strings.Len(sInsertLabLotComp) > 0)
//                {
//                    // # Identificar o o Nº do registo que foi criado!
//                    // Set ListaConsulta = Aplicacao.BSO.Consulta("SELECT MAX(CDU_NR) as Ultimo FROM TDU_LaboratorioLote")

//                    // If ListaConsulta.NumLinhas > 0 Then
//                    // ListaConsulta.Inicio
//                    // UltimoNr = ListaConsulta("Ultimo")
//                    // Else
//                    // '# Nunca irá entrar Aqui...
//                    // AdicionarAviso (" A consulta para identificar o registo acabado de inserir na TDU_LaboratorioLote não retornou valores")
//                    // RegistarValores = False
//                    // Exit Function
//                    // End If

//                    if (UltimoNr == 0)
//                    {
//                        // # Nunca irá entrar Aqui...
//                        AdicionarAviso(" A consulta para identificar o registo acabado de inserir na TDU_LaboratorioLote não retornou valores");
//                        return false;
//                    }

//                    // # Substituir a TAG @1@ pelo número do ul
//                    sInsertLabLotComp = Strings.Replace(sInsertLabLotComp, "@1@", UltimoNr);
//                    ;

//                    // #Antes de inserir garanto que não existe esta chave (nº)
//                    StringSQL_DESTINO.CommandText = " DELETE from TDU_LaboratorioLoteComparacao where CDU_NumeroRelatorio = " + UltimoNr + " ";
//                    StringSQL_DESTINO.Execute();

//                    // # Inserir na tabela de Comparacao
//                    StringSQL_DESTINO.CommandText = sInsertLabLotComp;
//                    StringSQL_DESTINO.Execute();
//                }

//                RegistarAnexos(Fornecedor_Nr, UltimoNr);
//                if (LigacaoSQL_ORIGEM.State == System.Data.ConnectionState.Open)
//                    LigacaoSQL_ORIGEM.Close();

//                return true;

//            }
//            catch
//            {
//                AdicionarAviso(Information.Err.Description);
//                return false;

//            }

//        }

//        private static bool ValidaTipoProcesso(string Processo)
//        {
//            bool ValidaTipoProcessoRet = default;
//            StdBELista ListaConsulta;
//            string SqlString;
//            long i;
//            if (Strings.Len(Processo) == 0)
//            {
//                ValidaTipoProcessoRet = true;
//                return ValidaTipoProcessoRet;
//            }

//            // #Definir a query
//            SqlString = "SELECT CDU_Tipo FROM TDU_TipoProcesso Where CDU_Tipo = '@1@' ";

//            // # Atribuir os filtros
//            SqlString = Strings.Replace(SqlString, "@1@", Processo);

//            // # Fazer Consulta
//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();

//            // verificar se existe o mesmo artigo na empresa atual
//            ConsultaSQL_DESTINO.Open(SqlString);

//            // # se exitir
//            if (ConsultaSQL_DESTINO.BOF == false)
//            {
//                // # retorna true
//                ValidaTipoProcessoRet = true;
//            }
//            else
//            {
//                AdicionarAviso("O Processo com o código " + Processo + " não existe.");
//                ValidaTipoProcessoRet = false;
//            }
//            // ConsultaSQL_DESTINO.Close

//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();
//            return ValidaTipoProcessoRet;
//        }

//        private static bool ValidaComposicao(string Composicao)
//        {
//            bool ValidaComposicaoRet = default;
//            var ListaConsulta = default(StdBELista);
//            string SqlString;
//            long i;

//            // # Se não tiver composição, retorna verdadeiro
//            if (Strings.Len(Composicao) == 0)
//            {
//                ValidaComposicaoRet = true;
//                return ValidaComposicaoRet;
//            }

//            // #Definir a query
//            SqlString = "SELECT CDU_Composicao FROM TDU_Composicoes Where CDU_Composicao = '@1@' ";

//            // # Atribuir os filtros
//            SqlString = Strings.Replace(SqlString, "@1@", Composicao);
//            ;

//            // # se exitir
//            if (ListaConsulta.NumLinhas() > 0)
//            {
//                // # retorna true
//                ValidaComposicaoRet = true;
//            }
//            else
//            {
//                AdicionarAviso("A Composicao com o código " + Composicao + " não existe.");
//                ValidaComposicaoRet = false;
//            }

//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();
//            return ValidaComposicaoRet;
//        }

//        private static bool ValidaFornecedor(ref string Fornecedor)
//        {
//            bool ValidaFornecedorRet = default;
//            StdBELista ListaConsulta;
//            string SqlString;
//            long i;
//            if (Strings.Len(Fornecedor) == 0)
//            {
//                ValidaFornecedorRet = true;
//                return ValidaFornecedorRet;
//            }

//            // #Definir a query
//            SqlString = "SELECT Fornecedor FROM Fornecedores Where Fornecedor = '@1@' ";

//            // # Atribuir os filtros
//            SqlString = Strings.Replace(SqlString, "@1@", Fornecedor);
//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();

//            // # Fazer Consulta
//            ConsultaSQL_DESTINO.Open(SqlString);

//            // # se exitir
//            if (ConsultaSQL_DESTINO.BOF == false)
//            {
//                // # retorna true
//                ValidaFornecedorRet = true;
//            }
//            else
//            {
//                // Alteração pedida por Eng. Joaquim 10/04/2017
//                // AdicionarAviso ("O Fornecedor com o código " & Fornecedor & " não existe.")

//                Fornecedor = FornecedorIndiferenciado;
//                ValidaFornecedorRet = true;
//            }

//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();
//            return ValidaFornecedorRet;
//        }

//        private static bool ValidaTipoTorcedur(string TipoTorc)
//        {
//            bool ValidaTipoTorcedurRet = default;
//            StdBELista ListaConsulta;
//            string SqlString;
//            long i;
//            if (Strings.Len(TipoTorc) == 0)
//            {
//                ValidaTipoTorcedurRet = true;
//                return ValidaTipoTorcedurRet;
//            }

//            // #Definir a query
//            SqlString = " SELECT CDU_Tipo FROM TDU_TipoTorcedura where CDU_Tipo = '@1@' ";

//            // # Atribuir os filtros
//            SqlString = Strings.Replace(SqlString, "@1@", TipoTorc);
//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();

//            // # Fazer Consulta
//            ConsultaSQL_DESTINO.Open(SqlString);

//            // # se exitir
//            if (ConsultaSQL_DESTINO.BOF == false)
//            {
//                // # retorna true
//                ValidaTipoTorcedurRet = true;
//            }
//            else
//            {
//                AdicionarAviso("O TipoTorcedura com o código " + TipoTorc + " não existe.");
//                ValidaTipoTorcedurRet = false;
//            }

//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();
//            return ValidaTipoTorcedurRet;
//        }

//        private static bool ValidaTipoUsoFio(string TipoUsoFio)
//        {
//            bool ValidaTipoUsoFioRet = default;
//            StdBELista ListaConsulta;
//            string SqlString;
//            long i;
//            if (Strings.Len(TipoUsoFio) == 0)
//            {
//                ValidaTipoUsoFioRet = true;
//                return ValidaTipoUsoFioRet;
//            }

//            // #Definir a query
//            SqlString = " SELECT * FROM TDU_TipoUsoFio WHERE CDU_Tipo = @1@ ";

//            // # Atribuir os filtros
//            SqlString = Strings.Replace(SqlString, "@1@", TipoUsoFio);

//            // # Fazer Consulta
//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();

//            // # Fazer Consulta
//            ConsultaSQL_DESTINO.Open(SqlString);

//            // # se exitir
//            if (ConsultaSQL_DESTINO.BOF == false)
//            {
//                // # retorna true
//                ValidaTipoUsoFioRet = true;
//            }
//            else
//            {
//                AdicionarAviso("O Tipo de Uso de Fio com o código " + TipoTorc + " não existe.");
//                ValidaTipoUsoFioRet = false;
//            }

//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();
//            return ValidaTipoUsoFioRet;
//        }

//        private static bool ValidaTipoAcondicionamento(string TipoAcond)
//        {
//            bool ValidaTipoAcondicionamentoRet = default;
//            StdBELista ListaConsulta;
//            string SqlString;
//            long i;
//            if (Strings.Len(TipoAcond) == 0)
//            {
//                ValidaTipoAcondicionamentoRet = true;
//                return ValidaTipoAcondicionamentoRet;
//            }

//            // #Definir a query
//            SqlString = " SELECT * FROM TDU_TipoAcondicionamento WHERE CDU_Tipo = @1@";

//            // # Atribuir os filtros
//            SqlString = Strings.Replace(SqlString, "@1@", TipoAcond);

//            // # Fazer Consulta
//            ConsultaSQL_DESTINO.Open(SqlString);

//            // # se exitir
//            if (ConsultaSQL_DESTINO.BOF == false)
//            {
//                // # retorna true
//                ValidaTipoAcondicionamentoRet = true;
//            }
//            else
//            {
//                AdicionarAviso("O Tipo de Acondicionamento " + TipoTorc + " não existe.");
//                ValidaTipoAcondicionamentoRet = false;
//            }

//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();
//            return ValidaTipoAcondicionamentoRet;
//        }

//        private static bool ValidaAngulosCone(ref string AnguloCone)
//        {
//            bool ValidaAngulosConeRet = default;
//            StdBELista ListaConsulta;
//            string SqlString;
//            long i;
//            if (Strings.Len(AnguloCone) == 0)
//            {
//                ValidaAngulosConeRet = true;
//                return ValidaAngulosConeRet;
//            }

//            // #Definir a query
//            SqlString = " SELECT * FROM TDU_AngulosCone WHERE CDU_Codigo = @1@ ";

//            // # Atribuir os filtros
//            SqlString = Strings.Replace(SqlString, "@1@", AnguloCone);

//            // # Fazer Consulta
//            ConsultaSQL_DESTINO.Open(SqlString);

//            // # se exitir
//            if (ConsultaSQL_DESTINO.BOF == false)
//            {
//                // # retorna true
//                ValidaAngulosConeRet = true;
//            }
//            else
//            {
//                AnguloCone = "";
//                // Alteração pedida por Eng. Joaquim 10/04/2017
//                // AdicionarAviso ("O Angulo Cone " & TipoTorc & " não existe.")

//                ValidaAngulosConeRet = true;
//            }

//            if (ConsultaSQL_DESTINO.State == 1)
//                ConsultaSQL_DESTINO.Close();
//            return ValidaAngulosConeRet;
//        }

//        private static bool RegistarAnexos(long NumeroRelatorioOrigem, long NumeroRelatorio)
//        {
//            string sInsertLabLotAnexos; // Query usada para a TDU_LaboratorioLoteComparacao
//            int i;

//            try
//            {
//                if (ConsultaSQL_ORIGEM.State == 1)
//                    ConsultaSQL_ORIGEM.Close();

//                // # consultar TDU_LaboratorioLoteComparacao porque vou precisar de criar o registo na minha Tabela.
//                ConsultaSQL_ORIGEM.Open(" SELECT * FROM TDU_AnexosLaboratorioLote WHERE CDU_AnexoNumRelatorio = " + NumeroRelatorioOrigem + " ");

//                // # só entra aqui se tiver Comparações
//                if (ConsultaSQL_ORIGEM.BOF != true)
//                {
//                    ConsultaSQL_ORIGEM.MoveFirst();
//                    ;

//                    // Apagar possíveis anexos que existam....
//                    StringSQL_DESTINO.CommandText = " DELETE from TDU_AnexosLaboratorioLote where CDU_AnexoNumRelatorio = " + NumeroRelatorio + " ";
//                    StringSQL_DESTINO.Execute();
//                    while (!ConsultaSQL_ORIGEM.EOF)
//                    {
//                        // # Preparar a do Insert na TDU_LaboratorioLoteComparacao
//                        // # Como ainda não sei qual é o Nr que registei, coloco a TAG @1@ para substituir depois
//                        sInsertLabLotAnexos = " INSERT INTO [dbo].[TDU_AnexosLaboratorioLote] ([CDU_Anexo] ,[CDU_AnexoNumRelatorio] ,[CDU_Caminho] ,[CDU_NomeFicheiro],[CDU_DataAnexo])" + " Values (";
//                        sInsertLabLotAnexos = sInsertLabLotAnexos + " " + Interaction.IIf(string.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Anexo")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_Anexo"), ",", ".")) + " ";
//                        sInsertLabLotAnexos = sInsertLabLotAnexos + "," + NumeroRelatorio + " ";
//                        sInsertLabLotAnexos = sInsertLabLotAnexos + ", " + Interaction.IIf(string.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_Caminho")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_Caminho") + "'") + " ";
//                        sInsertLabLotAnexos = sInsertLabLotAnexos + ", " + Interaction.IIf(string.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_NomeFicheiro")), "Null", " '" + ConsultaSQL_ORIGEM("CDU_NomeFicheiro") + "'") + " ";
//                        sInsertLabLotAnexos = sInsertLabLotAnexos + ", " + Interaction.IIf(string.IsNullOrEmpty(ConsultaSQL_ORIGEM("CDU_DataAnexo")), "Null", Strings.Replace(ConsultaSQL_ORIGEM("CDU_DataAnexo"), ",", ".")) + " ";
//                        sInsertLabLotAnexos = sInsertLabLotAnexos + " )";

//                        // #Inserir o registo
//                        StringSQL_DESTINO.CommandText = sInsertLabLotAnexos;
//                        StringSQL_DESTINO.Execute();

//                        // Avançar o apontador
//                        ConsultaSQL_ORIGEM.MoveNext();
//                    }
//                }

//                if (ConsultaSQL_ORIGEM.State == 1)
//                    ConsultaSQL_ORIGEM.Close();
//                return true;

//            }
//            catch
//            {
//                AdicionarAviso(Information.Err.Description);
//                return false;
//            }

//        }
//    }
//}