using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;

namespace PCustoPrTab
{
    public class VndIsEditorVendas : EditorVendas
    {
        private bool NovaEncomenda;

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("PCustoPrTab") == 1)
            {
                // *******************************************************************************************************************************************
                // #### Verifica e envia email se uma GR ou ECL for criada com o pre�o unitario inferior ao pre�o de custo - Bruno 05/02/2020 ####
                // *******************************************************************************************************************************************
                if (this.DocumentoVenda.Tipodoc == "ECL" & Strings.Right(this.DocumentoVenda.Serie, 1) != "B" & NovaEncomenda == true)
                {
                    VerificaPrecoAbaixoCustoEEnviaEmail();
                    VerificaPrecoCustoEEnviaEmail();
                }
                // *******************************************************************************************************************************************
                // #### Verifica e envia email se uma GR ou ECL for criada com o pre�o unitario inferior ao pre�o de custo - Bruno 05/02/2020 ####
                // *******************************************************************************************************************************************

                // #################################################################################################
                // ####### Coloca Pr. Tabela na Linha da ECL. Pedido de Mafalda - JFC 11-09-2020        ############
                // #################################################################################################
                
                if (this.DocumentoVenda.Tipodoc == "ECL")
                {
                    StdBELista prTab;
                    double PrecTab;

                    for (var j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(j).Lote + "" != "")
                        {
                            // Se a linha n�o tiver Pr. Tabela atribuido, atribuir pre�o atual. 99 - Nunca foi atribuido pre�o, 0 - J� houve tentativa mas n�o havia pre�o.
                            if (Information.IsNothing(this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_PrecTab"].Valor) | this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_PrecTab"].Valor.ToString() == "99")
                            {
                                prTab = BSO.Consulta("select primundifios.dbo.VMP_IEXF_DaPrecoTabela('" + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "','" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + "',3) as 'PrecTab'");

                                if (prTab.Vazia() == false)
                                {
                                    prTab.Inicio();
                                    PrecTab = prTab.Valor("PrecTab");
                                    // Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PrecTab") = prTab("PrecTab")
                                    BSO.DSO.ExecuteSQL("update ln set ln.CDU_PrecTab=replace('" + PrecTab + "',',','.') from LinhasDoc ln where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(j).IdLinha + "'");
                                }
                                else
                                    // Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PrecTab") = 0
                                    BSO.DSO.ExecuteSQL("update ln set ln.CDU_PrecTab=0 from LinhasDoc ln where ln.Id=" + this.DocumentoVenda.Linhas.GetEdita(j).IdLinha + "");
                            }
                        }
                    }
                }
            }
        }

        private string VarCliente;
        private string VarFrom;
        private string VarTo;
        private string VarAssunto;
        private string VarTextoInicialMsg;
        private string VarMensagem;
        private string VarArmazem;
        private string VarLinhas;
        private string VarUtilizador;
        private int VarLocalTeste; // 0 - ao seleccionar o cliente; 1 - antes de gravar o documento(ECL ou GR)
        private bool VarCancelaDoc;
        private bool VarNetTrans;

        // *******************************************************************************************************************************************
        // #### Bruno - Verifica pre�o de custo para enviar email #### - 05/02/2020
        // #### JFC   - Revis�o do c�digo                         #### - 10/05/2021
        // *******************************************************************************************************************************************
        private StdBELista ListaPCU;

        private string SqlStringPCU;
        private StdBELista ListaPTB;

        private void VerificaPrecoAbaixoCustoEEnviaEmail()
        {
            int i;
            long ln;
            bool enviarmail;
            string qualidade;
            double PrUnit, PCusto;
            string Header, Footer, Table, Nome;
            VarCliente = this.DocumentoVenda.Entidade;
            Nome = this.DocumentoVenda.Nome;
            if (this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupoNome"].Valor + "" != "")
            {
                VarCliente = this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor.ToString();
                Nome = this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupoNome"].Valor.ToString();
            }

            Header = "<html><head><style>" + "td {border: solid black;border-width: 1px;padding-left:5px;padding-right:5px;padding-top:1px;padding-bottom:1px;font: 11px arial} " + "</style></head><body><b>Mundifios, S.A.</b> <br>";

            VarFrom = "";

            VarTo = "export1@mundifios.pt; angelo@mundifios.pt; informatica@mundifios.pt";

            if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
            {
                VarTextoInicialMsg = "Bom dia,";
                Header = Header + "<br>Bom dia,<br><br>Foi lan�ado um documento no Primavera onde o pre�o unit�rio � inferior ao pre�o de custo<br>";
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
            {
                VarTextoInicialMsg = "Boa tarde,";
                Header = Header + "<br>Boa tarde,<br><br>Foi lan�ado um documento no Primavera onde o pre�o unit�rio � inferior ao pre�o de custo<br>";
            }
            else
            {
                VarTextoInicialMsg = "Boa noite,";
                Header = Header + "<br>Boa noite,<br><br>Foi lan�ado um documento no Primavera onde o pre�o unit�rio � inferior ao pre�o de custo<br>";
            }

            VarAssunto = "[Pre�o de Custo]Documento: " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "####") + "/" + this.DocumentoVenda.Serie + " (" + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + ")";

            VarUtilizador = Aplicacao.Utilizador.Utilizador;

            VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Foi lan�ado um documento no Primavera onde o pre�o unit�rio � inferior ao pre�o de custo" + Strings.Chr(13) + Strings.Chr(13) + ""
                          + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + Strings.Chr(13) + ""
                          + "Utilizador:                      " + VarUtilizador + Strings.Chr(13) + Strings.Chr(13) + ""
                          + "Cliente:                         " + VarCliente + " - " + Strings.Replace(Nome, "'", "") + Strings.Chr(13) + "";

            Header = Header + "<br>" + "<b>Empresa:</b>                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + "<br>" + "<b>Utilizador:</b>                      " + VarUtilizador + "<br><br>" + "<b>Cliente:</b>                         " + VarCliente + " - " + Strings.Replace(Nome, "'", "") + "<br>";

            VarLinhas = "";
            Table = "<table cellpadding=0 cellspacing=0 border=0>" + "<td bgcolor=#72C6FF><b>Artigo</b></td>" + "<td bgcolor=#72C6FF><b>Lote</b></td>" + "<td bgcolor=#72C6FF><b>T.Qual</b></td>" + "<td bgcolor=#72C6FF><b>Descri��o</b></td>" + "<td bgcolor=#72C6FF><b>Qtd</b></td>" + "<td bgcolor=#72C6FF><b>Pre�o</b></td>" + "<td bgcolor=#FF5019><b>PCusto</b></td>" + "<td bgcolor=#FF5019><b>PrTab</b></td></tr>";

            enviarmail = false;
            // 'Verifica se o pre�o de custo � superior ao Unitario
            for (i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
            {
                qualidade = "--";
                if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor != null)
                {
                    if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor.ToString() == "002")
                        qualidade = "c/Gar. p/Branco";
                    if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor.ToString() == "003")
                        qualidade = "Baixa Contamin.";
                }
                if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "")
                {
                    SqlStringPCU = "select primundifios.dbo.VMP_IEXF_DaPrecoCusto ('" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "','" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "','3') as 'PCusto'";
                    ListaPCU = BSO.Consulta(SqlStringPCU);
                    if (ListaPCU.Vazia() == false)
                    {
                        ListaPCU.Inicio();
                        PCusto = ListaPCU.Valor("PCusto");
                        // 1 - definir pre�ounit�rio (prunit+margempassagem ou precobase, / cambio)
                        PrUnit = 0;
                        if ((this.DocumentoVenda.Pais != "PT" & (Information.IsNothing(this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor) | this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor.ToString() != "PT")))
                            PrUnit = double.Parse(DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_PrecoBase"].Valor.ToString()) / (double)this.DocumentoVenda.Cambio;
                        else
                            PrUnit = (DocumentoVenda.Linhas.GetEdita(i).PrecUnit + double.Parse(DocumentoVenda.CamposUtil["CDU_MargemPassagemArtigo"].Valor.ToString())) / (double)this.DocumentoVenda.Cambio;

                        // 2 - confirmar armazem, se aep descontar valor no pcusto

                        if (this.DocumentoVenda.Linhas.GetEdita(i).Armazem == "AEP")
                        {
                            string sql;
                            StdBELista ListSQL;

                            sql = "select top 1 (le.CDU_CustoValor) as 'Direitos' " + "from TDU_CabecCustosEncomendas ce " + "inner join TDU_LinhasCustosEncomenda le on le.CDU_Serie=ce.CDU_Serie and le.CDU_NumDoc=ce.CDU_NumDoc and le.CDU_TipoDoc=ce.CDU_TipoDoc and le.CDU_NumLinha=ce.CDU_NumLinha " + "inner join CabecCompras cc2 on cc2.TipoDoc=ce.CDU_TipoDoc and cc2.NumDoc=ce.CDU_NumDoc and cc2.Serie=ce.CDU_Serie " + "where cc2.DataDoc <= '" + Strings.Format(this.DocumentoVenda.DataDoc, "yyyy-MM-dd HH:mm:ss") + "'" + "and ce.CDU_Artigo='" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "'" + "and ce.CDU_Lote='" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "'" + "and ce.CDU_TipoDoc='ECF' " + "and le.CDU_Descricao like '%Custos Alfandegarios%' ";
                            ListSQL = BSO.Consulta(sql);
                            if (ListSQL.Vazia() == false)
                            {
                                ListSQL.Inicio();
                                PCusto = PCusto - ListSQL.Valor("Direitos");
                            }
                        }
                        // 3 - comparar as duas variaveis

                        if (PrUnit <= PCusto)
                        {
                            enviarmail = true;
                            ListaPTB = BSO.Consulta("select primundifios.dbo.VMP_IEXF_DaPrecoTabela ('" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "','" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "','3') as 'PrecoTab'");
                            ListaPTB.Inicio();
                            VarLinhas = VarLinhas + "Linha " + i + ": " + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "-" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + " - Desc:" + this.DocumentoVenda.Linhas.GetEdita(i).Descricao + " - Qtd:" + this.DocumentoVenda.Linhas.GetEdita(i).Quantidade + "  - Pre�o Unit:" + PrUnit + "  - Pre�o Custo:" + PCusto + "  - Pre�o Tabela:" + ListaPTB.Valor("PrecoTab") + "  - Tipo de qualidade:" + qualidade + Strings.Chr(13) + "";

                            Table = Table + "<tr>" + "<td>" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "</td>" + "<td>" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "</td>" + "<td>" + qualidade + "</td>" + "<td>" + this.DocumentoVenda.Linhas.GetEdita(i).Descricao + "</td>" + "<td>" + this.DocumentoVenda.Linhas.GetEdita(i).Quantidade + this.DocumentoVenda.Linhas.GetEdita(i).Unidade + "</td>" + "<td>" + PrUnit + "</td>" + "<td>" + PCusto + "</td>" + "<td>" + ListaPTB.Valor("PrecoTab") + "</td></tr>";
                        }
                    }
                }
            }

            Footer = "</table><br><br></body></html>";

            VarMensagem = Header + Table + Footer;

            // 'Envia emailcom indicacao de pre�o custo
            if (enviarmail == true)
                BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
        }

        // *******************************************************************************************************************************************
        // #### JFC - Verifica se n�o h� pre�o de custo e envia email #### - 10/05/2021
        // *******************************************************************************************************************************************
        private void VerificaPrecoCustoEEnviaEmail()
        {
            int i;
            long ln;
            bool enviarmail;
            string qualidade;
            double PrUnit, PCusto;
            string Header, Footer, Table, Nome;
            VarCliente = this.DocumentoVenda.Entidade;
            Nome = this.DocumentoVenda.Nome;
            if (this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupoNome"].Valor + "" != "")
            {
                VarCliente = this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor.ToString();
                Nome = this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupoNome"].Valor.ToString();
            }

            Header = "<html><head><style>" + "td {border: solid black;border-width: 1px;padding-left:5px;padding-right:5px;padding-top:1px;padding-bottom:1px;font: 11px arial} " + "</style></head><body><b>Mundifios, S.A.</b> <br>";

            VarFrom = "";

            VarTo = "mafaldamachado@mundifios.pt; suporte@mundifios.pt; informatica@mundifios.pt";

            if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
            {
                VarTextoInicialMsg = "Bom dia,";
                Header = Header + "<br>Bom dia,<br><br>Foi lan�ado um documento no Primavera onde o pre�o custo � 0<br>";
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
            {
                VarTextoInicialMsg = "Boa tarde,";
                Header = Header + "<br>Boa tarde,<br><br>Foi lan�ado um documento no Primavera onde o pre�o custo � 0<br>";
            }
            else
            {
                VarTextoInicialMsg = "Boa noite,";
                Header = Header + "<br>Boa noite,<br><br>Foi lan�ado um documento no Primavera onde o pre�o custo � 0<br>";
            }

            VarAssunto = "[PCusto 0]Documento: " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "####") + "/" + this.DocumentoVenda.Serie + " (" + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + ")";

            VarUtilizador = Aplicacao.Utilizador.Utilizador;

            VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Foi lan�ado um documento no Primavera onde o pre�o unit�rio � inferior ao pre�o de custo" + Strings.Chr(13) + Strings.Chr(13) + ""
                          + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + Strings.Chr(13) + ""
                          + "Utilizador:                      " + VarUtilizador + Strings.Chr(13) + Strings.Chr(13) + ""
                          + "Cliente:                         " + VarCliente + " - " + Strings.Replace(Nome, "'", "") + Strings.Chr(13) + "";

            Header = Header + "<br>" + "<b>Empresa:</b>                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + "<br>" + "<b>Utilizador:</b>                      " + VarUtilizador + "<br><br>" + "<b>Cliente:</b>                         " + VarCliente + " - " + Strings.Replace(Nome, "'", "") + "<br>";

            VarLinhas = "";
            Table = "<table cellpadding=0 cellspacing=0 border=0>" + "<td bgcolor=#72C6FF><b>Artigo</b></td>" + "<td bgcolor=#72C6FF><b>Lote</b></td>" + "<td bgcolor=#72C6FF><b>T.Qual</b></td>" + "<td bgcolor=#72C6FF><b>Descri��o</b></td>" + "<td bgcolor=#72C6FF><b>Qtd</b></td>" + "<td bgcolor=#72C6FF><b>Pre�o</b></td>" + "<td bgcolor=#FF5019><b>PCusto</b></td>" + "<td bgcolor=#FF5019><b>PrTab</b></td></tr>";

            enviarmail = false;
            // 'Verifica se o pre�o de custo � superior ao Unitario
            for (i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
            {
                qualidade = "--";
                if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor.ToString() == "002")
                    qualidade = "c/Gar. p/Branco";
                if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor.ToString() == "003")
                    qualidade = "Baixa Contamin.";

                if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "")
                {
                    SqlStringPCU = "select primundifios.dbo.VMP_IEXF_DaPrecoCusto ('" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "','" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "','3') as 'PCusto'";
                    ListaPCU = BSO.Consulta(SqlStringPCU);
                    if (ListaPCU.Vazia() == false)
                    {
                        ListaPCU.Inicio();
                        PCusto = ListaPCU.Valor("PCusto");
                        // 1 - definir pre�ounit�rio (prunit+margempassagem ou precobase, / cambio)
                        PrUnit = 0;
                        if ((this.DocumentoVenda.Pais != "PT" & (Information.IsNothing(this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor) | this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor.ToString() != "PT")))
                            PrUnit = double.Parse(DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_PrecoBase"].Valor.ToString()) / (double)this.DocumentoVenda.Cambio;
                        else
                            PrUnit = (this.DocumentoVenda.Linhas.GetEdita(i).PrecUnit + double.Parse(DocumentoVenda.CamposUtil["CDU_MargemPassagemArtigo"].Valor.ToString())) / (double)this.DocumentoVenda.Cambio;

                        // 2 - verifica se o PCusto � 0
                        if (PCusto == 0)
                        {
                            enviarmail = true;
                            ListaPTB = BSO.Consulta("select primundifios.dbo.VMP_IEXF_DaPrecoTabela ('" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "','" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "','3') as 'PrecoTab'");
                            ListaPTB.Inicio();
                            VarLinhas = VarLinhas + "Linha " + i + ": " + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "-" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + " - Desc:" + this.DocumentoVenda.Linhas.GetEdita(i).Descricao + " - Qtd:" + this.DocumentoVenda.Linhas.GetEdita(i).Quantidade + "  - Pre�o Unit:" + PrUnit + "  - Pre�o Custo:" + PCusto + "  - Pre�o Tabela:" + ListaPTB.Valor("PrecoTab") + "  - Tipo de qualidade:" + qualidade + Strings.Chr(13) + "";

                            Table = Table + "<tr>" + "<td>" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "</td>" + "<td>" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "</td>" + "<td>" + qualidade + "</td>" + "<td>" + this.DocumentoVenda.Linhas.GetEdita(i).Descricao + "</td>" + "<td>" + this.DocumentoVenda.Linhas.GetEdita(i).Quantidade + this.DocumentoVenda.Linhas.GetEdita(i).Unidade + "</td>" + "<td>" + PrUnit + "</td>" + "<td>" + PCusto + "</td>" + "<td>" + ListaPTB.Valor("PrecoTab") + "</td></tr>";
                        }
                    }
                }
            }

            Footer = "</table><br><br></body></html>";

            VarMensagem = Header + Table + Footer;

            // 'Envia email com indicacao de pre�o custo
            if (enviarmail == true)
                BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
        }
    }
}