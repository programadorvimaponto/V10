using Generico;
using IntBE100;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Attributes;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.Windows.Forms;

namespace Inovafil
{
    public class VndIsEditorVendas : EditorVendas
    {
        private bool AspectoMalha;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            // #########################################################################################################################
            // ##Verifica se na Ficha Laboratorio/Lote o campo Aspecto da Malha está preenchido. A pedido de Dr.Rita - JFC 27/10/2017 ##
            // #########################################################################################################################
            if (this.DocumentoVenda.Tipodoc == "GR")
            {
                AspectoMalha = true;

                ValidaMalha();

                if (AspectoMalha == false)
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "Aspecto da malha não está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
            }
            // #########################################################################################################################
            // ##Verifica se na Ficha Laboratorio/Lote o campo Aspecto da Malha está preenchido. A pedido de Dr.Rita - JFC 27/10/2017 ##
            // #########################################################################################################################

            // JFC 14/09/2020 Colocar a Carga com a morada da Fiação.
            this.DocumentoVenda.CargaDescarga.MoradaCarga = "Rua Comendador Manuel Gonçalves nº 25";
            this.DocumentoVenda.CargaDescarga.Morada2Carga = "";
            this.DocumentoVenda.CargaDescarga.LocalidadeCarga = "S Cosme do Vale";
            this.DocumentoVenda.CargaDescarga.DistritoCarga = "03";
            this.DocumentoVenda.CargaDescarga.PaisCarga = "PT";
            this.DocumentoVenda.CargaDescarga.CodPostalCarga = "4770-583";
            this.DocumentoVenda.CargaDescarga.CodPostalLocalidadeCarga = "V N Famalicão";
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            // *******************************************************************************************************************************************
            // #### Enviar E-mail para o laboratorio para validar percentagens das mesclas. JFC - 20/11/2018
            // *******************************************************************************************************************************************

            if (this.DocumentoVenda.Tipodoc == "ECL")
                EnviaMailPreMescla();

            // *******************************************************************************************************************************************
            // #### Enviar E-mail para o laboratorio para validar percentagens das mesclas. JFC - 20/11/2018
            // *******************************************************************************************************************************************

            // *******************************************************************************************************************************************
            // #### Atualizar o Armazem Stock Service na Mundifios 14/12/2018 - JFC
            // *******************************************************************************************************************************************

            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
            {
                if (DocumentoVenda.Linhas.GetEdita(i).Armazem == "INO")
                {
                    string query = " INSERT INTO PRIMUNDIFIOS.dbo.ArtigoLote(Artigo, Lote, Descricao, DataFabrico, Validade, Controlador, Activo, Observacoes, CDU_TipoQualidade, CDU_Parafinado)"
                    + " SELECT al.Artigo,"
                    + " al.Lote,"
                    + " al.Descricao,"
                    + " al.DataFabrico,"
                    + " al.Validade,"
                    + " al.Controlador,"
                    + " al.Activo,"
                    + " al.Observacoes,"
                    + " al.CDU_TipoQualidade,"
                    + " al.CDU_Parafinado"
                    + " FROM ArtigoLote al"
                    + " WHERE concat(al.Artigo, al.lote) in"
                    + " (SELECT concat(aa.Artigo, aa.Lote)"
                    + " FROM ArtigoArmazem aa"
                    + " WHERE aa.Armazem = 'INO'"
                    + " AND aa.Artigo in"
                    + " (SELECT a.Artigo"
                    + " FROM primundifios.dbo.Artigo a"
                    + " WHERE a.Artigo = aa.Artigo))"
                    + " AND concat(al.Artigo, al.lote) not in"
                    + " (SELECT concat(al2.Artigo, al2.Lote)"
                    + " FROM primundifios.dbo.ArtigoLote al2"
                    + " WHERE al2.Artigo = al.Artigo"
                    + " AND al2.Artigo IS NOT NULL"
                    + " AND al2.Lote IS NOT NULL)";

                    BSO.DSO.ExecuteSQL(query);

                    IntBEDocumentoInterno DocInt = new IntBEDocumentoInterno();
                    StdBELista listaDoc = new StdBELista();

                    listaDoc = BSO.DSO.Consulta("select Numerador from PRIMUNDIFIOS.dbo.SeriesInternos where TipoDoc = 'ES' and Serie='INO'");
                    if (listaDoc.Valor("Numerador")==0)
                    {
                        listaDoc = null;
                        listaDoc = BSO.DSO.Consulta("SELECT aa.Armazem,aa.Artigo, aa.Lote, aa.StkActual - isnull( (SELECT sum(ln.Quantidade-lns.QuantTrans) FROM CabecDoc cd INNER JOIN LinhasDoc ln ON ln.IdCabecDoc=cd.Id INNER JOIN CabecDocStatus cds ON cds.IdCabecDoc=cd.Id INNER JOIN LinhasDocStatus lns ON lns.IdLinhasDoc=ln.Id WHERE cds.Anulado='0' AND cd.TipoDoc='ECL' AND ln.Armazem=aa.Armazem AND ln.Artigo=aa.Artigo AND ln.Lote=aa.Lote AND lns.Fechado='0' AND lns.EstadoTrans='P' ),0) AS 'StkActual', aa.Localizacao, aa.PCMedio, aa.PCUltimo FROM ArtigoArmazem aa WHERE aa.Armazem='INO' AND aa.Artigo in (SELECT a.Artigo FROM primundifios.dbo.Artigo a WHERE a.Artigo=aa.Artigo) And aa.StkActual > 0");

                        Module1.AbreEmpresa("MUNDIFIOS");


                        DocInt.ID = PSO.FuncoesGlobais.CriaGuid(true);

                        DocInt.Tipodoc = "ES";
                        DocInt.Serie = "INO";

                        // Preenche a restante informação no documento
                        Module1.emp.Internos.Documentos.PreencheDadosRelacionados(DocInt);
                        // Data de Introdução
                        DocInt.Data = DateTime.Now;
                        DocInt.DataVencimento = DateTime.Now;
                    }
                    else
                    {
                        listaDoc = null;
                        listaDoc = BSO.DSO.Consulta("SELECT aa.Armazem,aa.Artigo,aa.Lote, aa.StkActual - isnull( (SELECT sum(ln.Quantidade-lns.QuantTrans) FROM CabecDoc cd INNER JOIN LinhasDoc ln ON ln.IdCabecDoc=cd.Id INNER JOIN CabecDocStatus cds ON cds.IdCabecDoc=cd.Id INNER JOIN LinhasDocStatus lns ON lns.IdLinhasDoc=ln.Id WHERE cds.Anulado='0' AND cd.TipoDoc='ECL' AND ln.Armazem=aa.Armazem AND ln.Artigo=aa.Artigo AND ln.Lote=aa.Lote AND lns.Fechado='0' AND lns.EstadoTrans='P' ),0) AS 'StkActual', aa.Localizacao, aa.PCMedio, aa.PCUltimo FROM ArtigoArmazem aa WHERE aa.Armazem='INO' AND aa.Artigo in (SELECT a.Artigo FROM primundifios.dbo.Artigo a WHERE a.Artigo=aa.Artigo) And aa.StkActual > 0 ");

                        Module1.AbreEmpresa("MUNDIFIOS");
                         DocInt = new IntBEDocumentoInterno();

                        DocInt = Module1.emp.Internos.Documentos.Edita("ES", 1, "INO", "000");

                        DocInt.Linhas.RemoveTodos();

                        DocInt.Data = DateTime.Now;

                    }

                    try 
                    {
                        for (int j = 1; j <= listaDoc.NumLinhas(); j++)
                        {
                            Module1.emp.Internos.Documentos.AdicionaLinha(DocInt, listaDoc.Valor("Artigo"), listaDoc.Valor("Armazem"), listaDoc.Valor("Localizacao"), listaDoc.Valor("Lote"), Math.Round(Convert.ToDouble(listaDoc.Valor("PCMedio")),5), 0, listaDoc.Valor("StkActual"), 1, 1, 1);
                            DocInt.Linhas.GetEdita(DocInt.Linhas.NumItens).DataStock = DateTime.Now;
                            listaDoc.Seguinte();
                        }

                        // ----------------------------------
                        // GRAVAÇÃO DO DOCUMENTO
                        Module1.emp.Internos.Documentos.Actualiza(DocInt);
                        // GRAVAÇÃO DO DOCUMENTO
                        // ----------------------------------

                        // MENSAGEM FINAL

                        string strDetalhe;
                        strDetalhe = Constants.vbNullString;

                        strDetalhe = strDetalhe + "Documento Interno: " + DocInt.Tipodoc + " Nº " + System.Convert.ToString(DocInt.NumDoc) + "/" + DocInt.Serie + Constants.vbCrLf;

                        MessageBox.Show("Documento gerado com sucesso. \nInformações: " + strDetalhe, "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //'   MENSAGEM FINAL
                        //'----------------------------------

                        DocInt = null;

                        Module1.FechaEmpresa();

                        break;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao gerar Documento. \nExceção: "+ex.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        Module1.FechaEmpresa();
                    }

                }
            }
        }

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            base.DepoisDeTransformar(e);

            // JFC 14/09/2020 Colocar a Carga com a morada da Fiação.
            if (BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).TipoDocumento == 3)
            {
                this.DocumentoVenda.CargaDescarga.MoradaCarga = "Rua Comendador Manuel Gonçalves nº 25";
                this.DocumentoVenda.CargaDescarga.Morada2Carga = "";
                this.DocumentoVenda.CargaDescarga.LocalidadeCarga = "S Cosme do Vale";
                this.DocumentoVenda.CargaDescarga.DistritoCarga = "03";
                this.DocumentoVenda.CargaDescarga.PaisCarga = "PT";
                this.DocumentoVenda.CargaDescarga.CodPostalCarga = "4770-583";
                this.DocumentoVenda.CargaDescarga.CodPostalLocalidadeCarga = "V N Famalicão";
            }

            int linha;
            for (linha = 1; linha <= this.DocumentoVenda.Linhas.NumItens; linha++)

                this.DocumentoVenda.Linhas.GetEdita(linha).Quantidade = Math.Round(this.DocumentoVenda.Linhas.GetEdita(linha).Quantidade, 2);

            // *******************************************************************************************************************************************
            // ### Adicionado por Bruno 14/10/2019 ###depois de transformar, validar se o documento final é uma GR, se sim colocar o campo LinhasDoc.CDU_Observacoes a vazio
            // *******************************************************************************************************************************************
            if (this.DocumentoVenda.Tipodoc == "GR")
            {
                for (linha = 1; linha <= this.DocumentoVenda.Linhas.NumItens; linha++)
                    // BSO.DSO.BDAPL.Execute ("UPDATE LinhasDoc(linhai) SET CDU_Obeservacopes = "" where  WHERE IDLINHASDOC = '")
                    this.DocumentoVenda.Linhas.GetEdita(linha).CamposUtil["cdu_observacoes"].Valor = "";
            }
        }
        [ContextSyncManagement(ContextSyncManagement.Manual)]

        public override void ValidaLinha(int NumLinha, ExtensibilityEventArgs e)
        {
            base.RefreshContext();

            base.ValidaLinha(NumLinha, e);
            // #################################################################
            // ##Verificação de Artigo em Stock - Pedido Dra. Rita -22/03/2019##
            // #################################################################

            if (this.DocumentoVenda.Tipodoc == "ECL" & this.DocumentoVenda.Linhas.GetEdita(NumLinha).Armazem != "PLA")
            {
                long j;
                StdBELista sqlArtigoStock;
                string strArtigoStock;

                strArtigoStock = "Atenção! Artigo em Stock" + Strings.Chr(13) + Strings.Chr(13) + " -     Artigo       -   Lote   - Arm - Stock";

                sqlArtigoStock = BSO.Consulta("select top 1 * from ArtigoArmazem aa where aa.StkActual>'0' and aa.Artigo='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo + "' and aa.Lote='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote + "' and aa.Armazem='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Armazem + "'");
                if (sqlArtigoStock.Vazia() == true)
                {
                    sqlArtigoStock = BSO.Consulta("select aa.Artigo, aa.Lote, aa.Armazem, aa.StkActual from ArtigoArmazem aa where aa.StkActual>'0' and aa.Artigo='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo + "'");
                    if (sqlArtigoStock.Vazia() == false)
                    {
                        sqlArtigoStock.Inicio();

                        for (j = 1; j <= sqlArtigoStock.NumLinhas(); j++)
                        {
                            strArtigoStock = strArtigoStock + Strings.Chr(13) + " - " + sqlArtigoStock.Valor("Artigo") + " - " + sqlArtigoStock.Valor("Lote") + " - " + sqlArtigoStock.Valor("Armazem") + " - " + sqlArtigoStock.Valor("StkActual") + " Kg";
                            sqlArtigoStock.Seguinte();
                        }
                        MessageBox.Show(strArtigoStock, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            // #####################################################################
            // ##Verificação de Artigo em Stock - Pedido Dra. Rita -22/03/2019 JFC##
            // #####################################################################

            this.DocumentoVenda.Linhas.GetEdita(NumLinha).Quantidade = Math.Round(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Quantidade, 2);
            base.CommitContext();
        }

        private StdBELista QntECL;
        private string SqlQntECL;
        private string SqlStringArtLoteRestricoes;
        private StdBELista ListaArtLoteRestricoes;
        private int MsgErro;

        private void ValidaMalha()
        {
            int j;

            for (j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
            {
                if (this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(j).Lote + "" != "" & this.DocumentoVenda.Linhas.GetEdita(j).Lote + "" != "<L01>" & this.DocumentoVenda.Linhas.GetEdita(j).Armazem == "PA")
                {
                    if (this.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal + "" != "")
                    {
                        SqlQntECL = "select Quantidade from LinhasDoc where ID='" + this.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal + "'";
                        QntECL = BSO.Consulta(SqlQntECL);

                        QntECL.Inicio();

                        if (QntECL.Valor("Quantidade") > 499)
                        {
                            SqlStringArtLoteRestricoes = "SELECT * FROM [TDU_LABORATORIOLOTE] "
                                 + "WHERE cdu_codARTIGO = '" + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "' and cdu_loteart = '" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + "'";

                            ListaArtLoteRestricoes = BSO.Consulta(SqlStringArtLoteRestricoes);

                            if (ListaArtLoteRestricoes.Vazia() == false)
                            {
                                ListaArtLoteRestricoes.Inicio();

                                if (ListaArtLoteRestricoes.Valor("CDU_TQAspMalha") + "" == "")
                                {
                                    AspectoMalha = false;
                                    MsgErro = (int)MessageBox.Show("O Artigo: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + ", Lote: " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " não tem aspecto da malha preenchido!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                AspectoMalha = false;
                                MsgErro = (int)MessageBox.Show("O Artigo: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + ", Lote: " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " não possui características técnicas", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else if (this.DocumentoVenda.Linhas.GetEdita(j).Quantidade > 499)
                    {
                        SqlStringArtLoteRestricoes = "SELECT * FROM [TDU_LABORATORIOLOTE] "
                             + "WHERE cdu_codARTIGO = '" + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "' and cdu_loteart = '" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + "'";

                        ListaArtLoteRestricoes = BSO.Consulta(SqlStringArtLoteRestricoes);

                        if (ListaArtLoteRestricoes.Vazia() == false)
                        {
                            ListaArtLoteRestricoes.Inicio();

                            if (ListaArtLoteRestricoes.Valor("CDU_TQAspMalha") + "" == "")
                            {
                                AspectoMalha = false;

                                MsgErro = (int)MessageBox.Show("O Artigo: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + ", Lote: " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " não tem aspecto da malha preenchido!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            AspectoMalha = false;
                            MsgErro = (int)MessageBox.Show("O Artigo: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + ", Lote: " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " não possui características técnicas", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private StdBELista OpBalanca;
        private string SqlOpBalanca;
        private string VarMensagem;
        private string VarFrom;
        private string VarTo;
        private string VarAssunto;
        private string VarUtilizador;
        private string VarTextoInicialMsg;

        private void EnviaMailPreMescla()
        {
            bool EnviaMail;
            string DescExtra;
            VarMensagem = "";
            EnviaMail = false;

            for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
            {
                if (DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" && DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "" && DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "<L01>" && DocumentoVenda.Linhas.GetEdita(i).Armazem == "PA")
                {
                    DescExtra = BSO.Base.Artigos.DaValorAtributo(DocumentoVenda.Linhas.GetEdita(i).Artigo, "CDU_DescricaoExtra");

                    if (DescExtra.Contains("JP") || DescExtra.Contains("Injetado") || DescExtra.Contains("Moulinet") || DescExtra.Contains("FT") || DescExtra.Contains("RB") || DescExtra.Contains("Mosaic"))
                    {
                        EnviaMail = true;

                        SqlOpBalanca = "select ob.PRD_IDOperacao, ao.PRD_Artigo, ob.Mescla, ob.Ordem, ob.Descricao, ob.Percentagem, ob.PercentagemMescla from VIM_ArtigoOperacoes ao " +
                                       "inner join VMP_PLA_OpBalanca ob on  ob.PRD_IDOperacao=ao.PRD_IDOperacao " +
                                       "where  ao.PRD_Operacao='10' and  ao.PRD_Artigo = '" + DocumentoVenda.Linhas.GetEdita(i).Artigo + "' " +
                                       "order by ob.PRD_IDOperacao, ao.PRD_Artigo, ob.Mescla, ob.Ordem asc";
                        OpBalanca = BSO.Consulta(SqlOpBalanca);

                        OpBalanca.Inicio();

                        VarMensagem = VarMensagem + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Artigo: " + DocumentoVenda.Linhas.GetEdita(i).Artigo + " - " + DocumentoVenda.Linhas.GetEdita(i).Descricao + Strings.Chr(13) + ""
                                        + "Mescla - PercentagemMescla - Componente" + Strings.Chr(13) + ""
                                        + "     " + OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

                        OpBalanca.Seguinte();

                        VarMensagem = VarMensagem + ""
                        + "     " + OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

                        OpBalanca.Seguinte();

                        VarMensagem = VarMensagem + ""
                        + "     " + OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

                        OpBalanca.Seguinte();

                        VarMensagem = VarMensagem + ""
                        + "     " + OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

                        OpBalanca.Seguinte();

                        VarMensagem = VarMensagem + ""
                        + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

                        OpBalanca.Seguinte();

                        VarMensagem = VarMensagem + ""
                        + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

                        OpBalanca.Seguinte();

                        VarMensagem = VarMensagem + ""
                       + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

                        OpBalanca.Seguinte();

                        VarMensagem = VarMensagem + ""
                        + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";

                        OpBalanca.Seguinte();

                        VarMensagem = VarMensagem + ""
                        + "     " & OpBalanca.Valor("Mescla") + " -     " + OpBalanca.Valor("PercentagemMescla") + "          - " + OpBalanca.Valor("Descricao") + Strings.Chr(13) + "";
                    }
                }
            }

            if (EnviaMail == true)
            {
                VarFrom = "";
                VarTo = "informatica@mundifios.pt; edite.ferreira@inovafil.pt; paulo.araujo@inovafil.pt";

                if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
                    VarTextoInicialMsg = "Bom dia, ";
                else
if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
                    VarAssunto = "ECL " + DocumentoVenda.NumDoc + "/" + DocumentoVenda.Serie + " Validar Mescla Percentagem";

                VarUtilizador = Aplicacao.Utilizador.Utilizador;

                BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateAndTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
            }
        }
    }
}