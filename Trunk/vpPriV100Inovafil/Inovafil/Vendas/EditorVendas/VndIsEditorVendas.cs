//using Primavera.Extensibility.Sales.Editors;
//using Primavera.Extensibility.BusinessEntities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualBasic;
//using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
//using System.Windows.Forms;
//using StdBE100;

//namespace Inovafil
//{
//    public class VndIsEditorVendas : EditorVendas
//    {
//            private bool AspectoMalha;
//            public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
//            {
//                base.AntesDeGravar(ref Cancel, e);

//                    // #########################################################################################################################
//                    // ##Verifica se na Ficha Laboratorio/Lote o campo Aspecto da Malha está preenchido. A pedido de Dr.Rita - JFC 27/10/2017 ##
//                    // #########################################################################################################################
//                    if (this.DocumentoVenda.Tipodoc == "GR")
//                    {
//                        AspectoMalha = true;

//                        ValidaMalha();

//                        if (AspectoMalha == false)
//                        {
//                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "Aspecto da malha não está preenchido." ,"", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                            Cancel = true;
//                            return;
//                        }
//                    }
//                    // #########################################################################################################################
//                    // ##Verifica se na Ficha Laboratorio/Lote o campo Aspecto da Malha está preenchido. A pedido de Dr.Rita - JFC 27/10/2017 ##
//                    // #########################################################################################################################

//                    // JFC 14/09/2020 Colocar a Carga com a morada da Fiação.
//                    this.DocumentoVenda.CargaDescarga.MoradaCarga = "Rua Comendador Manuel Gonçalves nº 25";
//                    this.DocumentoVenda.CargaDescarga.Morada2Carga = "";
//                    this.DocumentoVenda.CargaDescarga.LocalidadeCarga = "S Cosme do Vale";
//                    this.DocumentoVenda.CargaDescarga.DistritoCarga = "03";
//                    this.DocumentoVenda.CargaDescarga.PaisCarga = "PT";
//                    this.DocumentoVenda.CargaDescarga.CodPostalCarga = "4770-583";
//                    this.DocumentoVenda.CargaDescarga.CodPostalLocalidadeCarga = "V N Famalicão";
//            }

//        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
//        {
//            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

//                // *******************************************************************************************************************************************
//                // #### Enviar E-mail para o laboratorio para validar percentagens das mesclas. JFC - 20/11/2018
//                // *******************************************************************************************************************************************

//                if (this.DocumentoVenda.Tipodoc == "ECL")
//                    EnviaMailPreMescla();

//                // *******************************************************************************************************************************************
//                // #### Enviar E-mail para o laboratorio para validar percentagens das mesclas. JFC - 20/11/2018
//                // *******************************************************************************************************************************************

//                // *******************************************************************************************************************************************
//                // #### Atualizar o Armazem Stock Service na Mundifios 14/12/2018 - JFC
//                // *******************************************************************************************************************************************

//                bool AtualizaSS;
//                AtualizaSS = false;
//                for (var j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
//                {
//                    if (this.DocumentoVenda.Linhas.GetEdita(j).Armazem == "INO")
//                        AtualizaSS = true;
//                }

//                if (AtualizaSS == true)
//                    BSO.DSO.ExecuteSQL("exec priinovafil.dbo.spAtualizaSS");
//            }

//        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
//        {
//            base.DepoisDeTransformar(e);

//                // JFC 14/09/2020 Colocar a Carga com a morada da Fiação.
//                if (BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).TipoDocumento == 3)
//                {
//                    this.DocumentoVenda.CargaDescarga.MoradaCarga = "Rua Comendador Manuel Gonçalves nº 25";
//                    this.DocumentoVenda.CargaDescarga.Morada2Carga = "";
//                    this.DocumentoVenda.CargaDescarga.LocalidadeCarga = "S Cosme do Vale";
//                    this.DocumentoVenda.CargaDescarga.DistritoCarga = "03";
//                    this.DocumentoVenda.CargaDescarga.PaisCarga = "PT";
//                    this.DocumentoVenda.CargaDescarga.CodPostalCarga = "4770-583";
//                    this.DocumentoVenda.CargaDescarga.CodPostalLocalidadeCarga = "V N Famalicão";
//                }

//                int linha;
//                for (linha = 1; linha <= this.DocumentoVenda.Linhas.NumItens; linha++)

//                    this.DocumentoVenda.Linhas.GetEdita(linha).Quantidade = Math.Round(this.DocumentoVenda.Linhas.GetEdita(linha).Quantidade, 2);

//                // *******************************************************************************************************************************************
//                // ### Adicionado por Bruno 14/10/2019 ###depois de transformar, validar se o documento final é uma GR, se sim colocar o campo LinhasDoc.CDU_Observacoes a vazio
//                // *******************************************************************************************************************************************

//                if (this.DocumentoVenda.Tipodoc == "GR")
//                {
//                    for (linha = 1; linha <= this.DocumentoVenda.Linhas.NumItens; linha++)
//                        // BSO.DSO.BDAPL.Execute ("UPDATE LinhasDoc(linhai) SET CDU_Obeservacopes = "" where  WHERE IDLINHASDOC = '")
//                        this.DocumentoVenda.Linhas.GetEdita(linha).CamposUtil["cdu_observacoes"].Valor = "";
//                }
//            }

//        public override void ValidaLinha(int NumLinha, ExtensibilityEventArgs e)
//        {
//            base.ValidaLinha(NumLinha, e);
//                // #################################################################
//                // ##Verificação de Artigo em Stock - Pedido Dra. Rita -22/03/2019##
//                // #################################################################

//                if (this.DocumentoVenda.Tipodoc == "ECL" & this.DocumentoVenda.Linhas.GetEdita(NumLinha).Armazem != "PLA")
//                {
//                    long j;
//                    StdBELista sqlArtigoStock;
//                    string strArtigoStock;

//                    strArtigoStock = "Atenção! Artigo em Stock" + Strings.Chr(13) + Strings.Chr(13) + " -     Artigo       -   Lote   - Arm - Stock";

//                    sqlArtigoStock = BSO.Consulta("select top 1 * from ArtigoArmazem aa where aa.StkActual>'0' and aa.Artigo='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo + "' and aa.Lote='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote + "' and aa.Armazem='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Armazem + "'");
//                    if (sqlArtigoStock.Vazia() == true)
//                    {
//                        sqlArtigoStock = BSO.Consulta("select aa.Artigo, aa.Lote, aa.Armazem, aa.StkActual from ArtigoArmazem aa where aa.StkActual>'0' and aa.Artigo='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo + "'");
//                        if (sqlArtigoStock.Vazia() == false)
//                        {
//                            sqlArtigoStock.Inicio();

//                            for (j = 1; j <= sqlArtigoStock.NumLinhas(); j++)
//                            {
//                                strArtigoStock = strArtigoStock + Strings.Chr(13) + " - " + sqlArtigoStock.Valor("Artigo") + " - " + sqlArtigoStock.Valor("Lote") + " - " + sqlArtigoStock.Valor("Armazem") + " - " + sqlArtigoStock.Valor("StkActual") + " Kg";
//                                sqlArtigoStock.Seguinte();
//                            }
//                        MessageBox.Show(strArtigoStock, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

//                    }
//                }
//                }
//                // #####################################################################
//                // ##Verificação de Artigo em Stock - Pedido Dra. Rita -22/03/2019 JFC##
//                // #####################################################################

//                this.DocumentoVenda.Linhas.GetEdita(NumLinha).Quantidade = Math.Round(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Quantidade, 2);
//            }

//        StdBELista QntECL;
//        string SqlQntECL;
//        string SqlStringArtLoteRestricoes;
//        StdBELista ListaArtLoteRestricoes;
//        int MsgErro;

//        private void ValidaMalha()
//        {
//            int j;

//            for (j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
//            {
//                if (this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(j).Lote + "" != "" & this.DocumentoVenda.Linhas.GetEdita(j).Lote + "" != "<L01>" & this.DocumentoVenda.Linhas.GetEdita(j).Armazem == "PA")
//                {
//                    if (this.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal + "" != "")
//                    {
//                        SqlQntECL = "select Quantidade from LinhasDoc where ID='" + this.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal + "'";
//                        QntECL = BSO.Consulta(SqlQntECL);

//                        QntECL.Inicio();

//                        if (QntECL.Valor("Quantidade") > 499)
//                        {
//                            SqlStringArtLoteRestricoes = "SELECT * FROM [TDU_LABORATORIOLOTE] "
//                                 + "WHERE cdu_codARTIGO = '" + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "' and cdu_loteart = '" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + "'";

//                            ListaArtLoteRestricoes = BSO.Consulta(SqlStringArtLoteRestricoes);

//                            if (ListaArtLoteRestricoes.Vazia() == false)
//                            {
//                                ListaArtLoteRestricoes.Inicio();

//                                if (ListaArtLoteRestricoes.Valor("CDU_TQAspMalha") + "" == "")
//                                {
//                                    AspectoMalha = false;
//                                 MsgErro =   (int)MessageBox.Show("O Artigo: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + ", Lote: " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " não tem aspecto da malha preenchido!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                }
//                            }
//                            else
//                            {
//                                AspectoMalha = false;
//                                MsgErro = (int)MessageBox.Show("O Artigo: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + ", Lote: " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " não possui características técnicas", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            }
//                        }
//                    }
//                    else if (this.DocumentoVenda.Linhas.GetEdita(j).Quantidade > 499)
//                    {
//                        SqlStringArtLoteRestricoes = "SELECT * FROM [TDU_LABORATORIOLOTE] "
//                             + "WHERE cdu_codARTIGO = '" + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "' and cdu_loteart = '" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + "'";

//                        ListaArtLoteRestricoes = BSO.Consulta(SqlStringArtLoteRestricoes);

//                        if (ListaArtLoteRestricoes.Vazia() == false)
//                        {
//                            ListaArtLoteRestricoes.Inicio();

//                            if (ListaArtLoteRestricoes.Valor("CDU_TQAspMalha") + "" == "")
//                            {
//                                AspectoMalha = false;

//                                MsgErro = (int)MessageBox.Show("O Artigo: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + ", Lote: " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " não tem aspecto da malha preenchido!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            }
//                        }
//                        else
//                        {
//                            AspectoMalha = false;
//                            MsgErro = (int)MessageBox.Show("O Artigo: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + ", Lote: " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " não possui características técnicas", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                        }
//                    }
//                }
//            }
//        }

//        StdBELista OpBalanca;
//        string SqlOpBalanca;
//        string VarMensagem;
//        string VarFrom;
//        string VarTo;
//        string VarAssunto;
//        string VarUtilizador;
//        string VarTextoInicialMsg;

//        private void EnviaMailPreMescla()
//        {
//            bool EnviaMail;
//            string DescExtra;
//            VarMensagem = "";
//            EnviaMail = false;

//            for(int i=1;i<=DocumentoVenda.Linhas.NumItens;i++)
//            {
//                if(DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" && DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "" && DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "<L01>" && DocumentoVenda.Linhas.GetEdita(i).Armazem == "PA")
//                {
//                    DescExtra = BSO.Base.Artigos.DaValorAtributo(DocumentoVenda.Linhas.GetEdita(i).Artigo, "CDU_DescricaoExtra");

//                    if(DescExtra.Contains("JP") || DescExtra.Contains("Injetado") || DescExtra.Contains("Moulinet") || DescExtra.Contains("FT")||DescExtra.Contains("RB") || DescExtra.Contains("Mosaic"))
//                    {
//                        EnviaMail = true;

//                        SqlOpBalanca = "select ob.PRD_IDOperacao, ao.PRD_Artigo, ob.Mescla, ob.Ordem, ob.Descricao, ob.Percentagem, ob.PercentagemMescla from VIM_ArtigoOperacoes ao " +
//                                       "inner join VMP_PLA_OpBalanca ob on  ob.PRD_IDOperacao=ao.PRD_IDOperacao " +
//                                       "where  ao.PRD_Operacao='10' and  ao.PRD_Artigo = '" + DocumentoVenda.Linhas.GetEdita(i).Artigo + "' " +
//                                       "order by ob.PRD_IDOperacao, ao.PRD_Artigo, ob.Mescla, ob.Ordem asc";
//                        OpBalanca = BSO.Consulta(SqlOpBalanca);

//                        OpBalanca.Inicio();

//                        VarMensagem = VarMensagem + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Artigo: " + DocumentoVenda.Linhas.GetEdita(i).Artigo + " - " + DocumentoVenda.Linhas.GetEdita(i).Descricao + Strings.Chr(13) + ""
//                                        + "Mescla - PercentagemMescla - Componente" + Strings.Chr(13) + ""
//                                        + "     " + OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

//                        OpBalanca.Seguinte();

//                        VarMensagem = VarMensagem + ""
//                        + "     " + OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

//                        OpBalanca.Seguinte();

//                        VarMensagem = VarMensagem + ""
//                        + "     " + OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

//                        OpBalanca.Seguinte();

//                        VarMensagem = VarMensagem + ""
//                        + "     " + OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

//                        OpBalanca.Seguinte();

//                        VarMensagem = VarMensagem + ""
//                        + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

//                        OpBalanca.Seguinte();

//                        VarMensagem = VarMensagem + ""
//                        + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

//                        OpBalanca.Seguinte();

//                        VarMensagem = VarMensagem + ""
//                       + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

//                        OpBalanca.Seguinte();

//                        VarMensagem = VarMensagem + ""
//                        + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

//                        OpBalanca.Seguinte();

//                        VarMensagem = VarMensagem + ""
//                        + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";
//                    }

//                }

//            }

//            if(EnviaMail==true)
//            {
//                VarFrom = "";
//                VarTo = "informatica@mundifios.pt; edite.ferreira@inovafil.pt; paulo.araujo@inovafil.pt";

//                if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
//                    VarTextoInicialMsg = "Bom dia, ";
//                else
//if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
//                    VarAssunto = "ECL " + DocumentoVenda.NumDoc + "/" + DocumentoVenda.Serie + " Validar Mescla Percentagem";

//                VarUtilizador = Aplicacao.Utilizador.Utilizador;

//                BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateAndTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");

//            }

//        }

//    }
//}