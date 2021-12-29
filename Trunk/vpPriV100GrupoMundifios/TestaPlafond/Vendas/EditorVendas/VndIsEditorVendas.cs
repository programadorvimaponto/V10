using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.Windows.Forms;

namespace TestaPlafond
{
    public class VndIsEditorVendas : EditorVendas
    {
        // Variáveis para o Plafond
        private StdBELista ListaTotalDeb;

        private string SqlStringTotalDeb;
        private string VarCliente;
        private string VarClienteNome;
        private double VarPlafondCliente;
        private double VarPlafondCred;
        private double VarPlafondExtra;
        private double VarTotRespLetras;
        private double VarTotValorCC;
        private double VarTotGuias;
        private double VarTotCheques;
        private double VarTotEncomendas;
        private double VarTotParaEncomenda;
        private double VarTotParaGuia;
        private double VarTotalDoc;
        private double VarTotalDocGravado;
        private string VarFrom;
        private string VarTo;
        private string VarAssunto;
        private string VarTextoInicialMsg;
        private string VarMensagem;
        private string VarUtilizador;

        private bool VarCancelaDoc;
        private int VarLocalTeste; // 0 - ao seleccionar o cliente; 1 - antes de gravar o documento(ECL ou GR)

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("TestaPlafond") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "ECL" | this.DocumentoVenda.Tipodoc == "GC" | (this.DocumentoVenda.Tipodoc == "GR" & Strings.Right(this.DocumentoVenda.Serie, 1) == "E"))
                {
                    VarCancelaDoc = false;
                    VarLocalTeste = 1;
                    if (this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "" != "")
                        TestaPlafondCopiaEntreEmpresas();
                    else
                        TestaPlafond();

                    if (VarCancelaDoc == true)
                    {
                        Cancel = true;
                        return;
                    }
                }
            }
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            if (Module1.VerificaToken("TestaPlafond") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "ECL" | this.DocumentoVenda.Tipodoc == "GC" | (this.DocumentoVenda.Tipodoc == "GR" & Strings.Right(this.DocumentoVenda.Serie, 1) == "E"))
                {
                    VarLocalTeste = 0;
                    TestaPlafond();
                }
            }
        }

        public override void AntesDeEditar(string Filial, string Tipo, string Serie, int NumDoc, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeEditar(Filial, Tipo, Serie, NumDoc, ref Cancel, e);

            if (Module1.VerificaToken("TestaPlafond") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "ECL" | this.DocumentoVenda.Tipodoc == "GC")
                    VarTotalDocGravado = 0;
            }
        }

        private void TestaPlafondCopiaEntreEmpresas()
        {
            if (this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "" != "")
            {
                VarCliente = this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor.ToString();

                Module1.AbreEmpresa(BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor.ToString());
                VarClienteNome = BSO.Base.Clientes.Edita(VarCliente).Nome;
                if (BSO.Base.Clientes.Edita(VarCliente).Limitecredito + "" == "")
                    VarPlafondCred = 0;
                else
                    VarPlafondCred = BSO.Base.Clientes.Edita(VarCliente).Limitecredito;

                // ######  Retirada a soma do VarPlafondExtra no dia 04/11/2016 a pedido da Dona Goretti. #######
                // If BSO.Comercial.Clientes.Edita(VarCliente).CamposUtil("CDU_PlafondExtra").Valor & "" = "" Then
                // VarPlafondExtra = 0
                // Else
                // VarPlafondExtra = BSO.Comercial.Clientes.Edita(VarCliente).CamposUtil("CDU_PlafondExtra").Valor
                // End If

                // VarPlafondCliente = VarPlafondCred + VarPlafondExtra

                VarPlafondCliente = VarPlafondCred;

                // 1. Responsabilidade em Letras
                // 2. Valor em  Conta Corrente
                // 3. Guias de Remessa Por Facturar
                // 4. Cheques Pré-Datados
                // 5. Encomendas em Carteira

                SqlStringTotalDeb = "SELECT TOP 1 (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente "
                                + "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CCC') AND estado = 'PEN' AND Entidade = '" + VarCliente + "' "
                                + "AND DataVenc < '2099-12-31') AS ValorContaCorrente "
                                + ", (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente "
                                + "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CCC') AND estado = 'CPD' AND Entidade = '" + VarCliente + "' "
                                + "AND DataVenc < '2099-12-31') AS ValorCPD "
                                + ", (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente "
                                + "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CLR') AND Entidade = '" + VarCliente + "' "
                                + "AND DataVenc < '2099-12-31') AS ValorLetras "
                                + "FROM Pendentes AS Pendentes_1";

                ListaTotalDeb = BSO.Consulta(SqlStringTotalDeb);

                if (ListaTotalDeb.Vazia() == true)
                {
                    VarTotValorCC = 0;
                    VarTotCheques = 0;
                    VarTotRespLetras = 0;
                }
                else
                {
                    VarTotValorCC = Math.Round(ListaTotalDeb.Valor("ValorContaCorrente"), 2);
                    VarTotCheques = Math.Round(ListaTotalDeb.Valor("ValorCPD"), 2);
                    VarTotRespLetras = Math.Round(ListaTotalDeb.Valor("ValorLetras"), 2);
                }

                SqlStringTotalDeb = "SELECT TOP 1 (SELECT ISNULL(SUM(CASE moeda WHEN 'EUR' THEN ((LinhasDoc.TaxaIva / 100) + 1) * (LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) "
                                + "ELSE ((LinhasDoc.TaxaIva / 100) + 1) * ((LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit))) / cambio END), 0) AS ValorGuiasRemessa "
                                + "FROM CabecDoc INNER JOIN LinhasDoc ON CabecDoc.Id = LinhasDoc.IdCabecDoc INNER JOIN LinhasDocStatus ON LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc INNER JOIN DocumentosVenda ON CabecDoc.TipoDoc = DocumentosVenda.Documento INNER JOIN CabecDocStatus ON LinhasDoc.IdCabecDoc = CabecDocStatus.IdCabecDoc "
                                + "WHERE (DocumentosVenda.TipoDocumento = 3) AND (CabecDocStatus.Estado = 'P') AND (LinhasDocStatus.EstadoTrans = 'P') AND (CabecDoc.Entidade = '" + VarCliente + "') "
                                + "AND CabecDoc.DataVencimento < '2099-12-31' AND (CabecDocStatus.Fechado = 0) AND (LinhasDocStatus.Fechado = 0) AND (CabecDocStatus.Anulado = 0) "
                                + "AND cabecdoc.tipodoc = 'GR') AS ValorGuiasRemessa "
                                + ", (SELECT ISNULL(SUM(CASE moeda WHEN 'EUR' THEN ((LinhasDoc.TaxaIva / 100) + 1) * (LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) "
                                + "ELSE ((LinhasDoc.TaxaIva / 100) + 1) * ((LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) / cambio) END), 0) AS ValorEncomedasCarteira "
                                + "FROM CabecDoc INNER JOIN LinhasDoc ON CabecDoc.Id = LinhasDoc.IdCabecDoc INNER JOIN LinhasDocStatus ON LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc INNER JOIN DocumentosVenda ON CabecDoc.TipoDoc = DocumentosVenda.Documento INNER JOIN CabecDocStatus ON LinhasDoc.IdCabecDoc = CabecDocStatus.IdCabecDoc "
                                + "WHERE (DocumentosVenda.TipoDocumento = 2) AND (CabecDocStatus.Estado = 'P') AND (LinhasDocStatus.EstadoTrans = 'P') AND (CabecDoc.Entidade = '" + VarCliente + "') "
                                + "AND (CabecDocStatus.Fechado = 0) AND (LinhasDocStatus.Fechado = 0) AND (CabecDocStatus.Anulado = 0) "
                                + "AND CabecDoc.DataVencimento < '2099-12-31') AS ValorEncomendasCarteira "
                                + "FROM CabecDoc AS CabecDoc_1";

                ListaTotalDeb = BSO.Consulta(SqlStringTotalDeb);

                if (ListaTotalDeb.Vazia() == true)
                {
                    VarTotGuias = 0;
                    VarTotEncomendas = 0;
                }
                else
                {
                    VarTotGuias = Math.Round(ListaTotalDeb.Valor("ValorGuiasRemessa"), 2);
                    VarTotEncomendas = Math.Round(ListaTotalDeb.Valor("ValorEncomendasCarteira"), 2);
                }

                VarTotalDoc = this.DocumentoVenda.TotalDocumento;

                VarTotParaEncomenda = VarTotRespLetras + VarTotValorCC + VarTotGuias + VarTotCheques + VarTotEncomendas + VarTotalDoc;
                VarTotParaGuia = VarTotRespLetras + VarTotValorCC + VarTotGuias + VarTotCheques + VarTotalDoc;

                if (this.DocumentoVenda.Tipodoc == "ECL" & VarTotalDoc != VarTotalDocGravado)
                {
                    if (VarTotParaEncomenda > VarPlafondCliente)
                    {
                        if (VarLocalTeste == 0)
                            MessageBox.Show("Cliente com plafond ultrapassado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (VarLocalTeste == 1)
                        {
                            if (MessageBox.Show("Cliente com plafond ultrapassado!" + Strings.Chr(13) + "Deseja mesmo assim gravar o documento ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)

                                EnvioEmailPlafond();
                            else
                                VarCancelaDoc = true;
                        }
                    }
                }
                else if ((this.DocumentoVenda.Tipodoc == "GC" | this.DocumentoVenda.Tipodoc == "GR") & VarTotalDoc != VarTotalDocGravado)
                {
                    if (VarTotParaGuia > VarPlafondCliente)
                    {
                        if (VarLocalTeste == 0)
                            MessageBox.Show("Cliente com plafond ultrapassado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (VarLocalTeste == 1)
                        {
                            if (MessageBox.Show("Cliente com plafond ultrapassado!" + Strings.Chr(13) + "Deseja mesmo assim gravar o documento ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                EnvioEmailPlafond();
                            else
                                VarCancelaDoc = true;
                        }
                    }
                }

                VarTotalDocGravado = 0;

                Module1.FechaEmpresa();
            }
        }

        private void EnvioEmailPlafond()
        {
            VarFrom = "";
            // VarTo = "mgoretti@mundifios.pt"
            VarTo = "tesouraria.clientes@mundifios.pt;";
            // VarTo = "jafernandes@mundifios.pt; mgoretti@mundifios.pt;"

            if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
                VarTextoInicialMsg = "Bom dia,";
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
                VarTextoInicialMsg = "Boa tarde,";
            else
                VarTextoInicialMsg = "Boa noite,";

            VarAssunto = "Plafond Ultrapassado: (" + VarCliente + ") - " + Strings.Replace(VarClienteNome, "'", "");

            VarUtilizador = Aplicacao.Utilizador.Utilizador;
            int i;
            for (i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
            {
                if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote != "")
                    break;
            }

            if (this.DocumentoVenda.Tipodoc == "ECL")
                VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Foi emitido um documento no Primavera para um cliente com o plafond ultrapassado:" + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + Strings.Chr(13) + ""
                            + "Empresa Destino:                 " + BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + Strings.Chr(13) + ""
                            + "Utilizador:                      " + VarUtilizador + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Cliente:                         " + VarCliente + " - " + Strings.Replace(VarClienteNome, "'", "") + Strings.Chr(13) + ""
                            + "Palfond:                         " + Interaction.IIf(VarPlafondCliente == 0, "0,00", Strings.Format(VarPlafondCliente, "#,###.00")) + Strings.Chr(13) + ""
                            + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + Strings.Chr(13) + ""
                            + "Valor Documento:                 " + Interaction.IIf(VarTotalDoc == 0, "0,00", Strings.Format(VarTotalDoc, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Responsabilidade em Letras:      " + Interaction.IIf(VarTotRespLetras == 0, "0,00", Strings.Format(VarTotRespLetras, "#,###.00")) + Strings.Chr(13) + ""
                            + "Valor em  Conta Corrente:        " + Interaction.IIf(VarTotValorCC == 0, "0,00", Strings.Format(VarTotValorCC, "#,###.00")) + Strings.Chr(13) + ""
                            + "Guias de Remessa Por Facturar:   " + Interaction.IIf(VarTotGuias == 0, "0,00", Strings.Format(VarTotGuias, "#,###.00")) + Strings.Chr(13) + ""
                            + "Cheques Pré-Datados:             " + Interaction.IIf(VarTotCheques == 0, "0,00", Strings.Format(VarTotCheques, "#,###.00")) + Strings.Chr(13) + ""
                            + "Encomendas em Carteira:          " + Interaction.IIf(VarTotEncomendas == 0, "0,00", Strings.Format(VarTotEncomendas, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Totais + Valor Documento:        " + Interaction.IIf(VarTotParaEncomenda == 0, "0,00", Strings.Format(VarTotParaEncomenda, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Cumprimentos";
            else if (this.DocumentoVenda.Tipodoc == "GC")
                VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Foi emitido um documento no Primavera para um cliente com o plafond ultrapassado:" + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + Strings.Chr(13) + ""
                            + "Empresa Destino:                 " + BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + Strings.Chr(13) + ""
                            + "Utilizador:                      " + VarUtilizador + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Cliente:                         " + VarCliente + " - " + Strings.Replace(VarClienteNome, "'", "") + Strings.Chr(13) + ""
                            + "Palfond:                         " + Interaction.IIf(VarPlafondCliente == 0, "0,00", Strings.Format(VarPlafondCliente, "#,###.00")) + Strings.Chr(13) + ""
                            + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + Strings.Chr(13) + ""
                            + "Data Expedição:                  " + this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_DataExp"] + ""
                            + "Valor Documento:                 " + Interaction.IIf(VarTotalDoc == 0, "0,00", Strings.Format(VarTotalDoc, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Responsabilidade em Letras:      " + Interaction.IIf(VarTotRespLetras == 0, "0,00", Strings.Format(VarTotRespLetras, "#,###.00")) + Strings.Chr(13) + ""
                            + "Valor em  Conta Corrente:        " + Interaction.IIf(VarTotValorCC == 0, "0,00", Strings.Format(VarTotValorCC, "#,###.00")) + Strings.Chr(13) + ""
                            + "Guias de Remessa Por Facturar:   " + Interaction.IIf(VarTotGuias == 0, "0,00", Strings.Format(VarTotGuias, "#,###.00")) + Strings.Chr(13) + ""
                            + "Cheques Pré-Datados:             " + Interaction.IIf(VarTotCheques == 0, "0,00", Strings.Format(VarTotCheques, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Totais + Valor Documento:        " + Interaction.IIf(VarTotParaGuia == 0, "0,00", Strings.Format(VarTotParaGuia, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Cumprimentos";
            else if (this.DocumentoVenda.Tipodoc == "GR")
                VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Foi emitido um documento no Primavera para um cliente com o plafond ultrapassado:" + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + Strings.Chr(13) + ""
                            + "Empresa Destino:                 " + BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + Strings.Chr(13) + ""
                            + "Utilizador:                      " + VarUtilizador + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Cliente:                         " + VarCliente + " - " + Strings.Replace(VarClienteNome, "'", "") + Strings.Chr(13) + ""
                            + "Palfond:                         " + Interaction.IIf(VarPlafondCliente == 0, "0,00", Strings.Format(VarPlafondCliente, "#,###.00")) + Strings.Chr(13) + ""
                            + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + Strings.Chr(13) + ""
                            + "Valor Documento:                 " + Interaction.IIf(VarTotalDoc == 0, "0,00", Strings.Format(VarTotalDoc, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Responsabilidade em Letras:      " + Interaction.IIf(VarTotRespLetras == 0, "0,00", Strings.Format(VarTotRespLetras, "#,###.00")) + Strings.Chr(13) + ""
                            + "Valor em  Conta Corrente:        " + Interaction.IIf(VarTotValorCC == 0, "0,00", Strings.Format(VarTotValorCC, "#,###.00")) + Strings.Chr(13) + ""
                            + "Guias de Remessa Por Facturar:   " + Interaction.IIf(VarTotGuias == 0, "0,00", Strings.Format(VarTotGuias, "#,###.00")) + Strings.Chr(13) + ""
                            + "Cheques Pré-Datados:             " + Interaction.IIf(VarTotCheques == 0, "0,00", Strings.Format(VarTotCheques, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Totais + Valor Documento:        " + Interaction.IIf(VarTotParaGuia == 0, "0,00", Strings.Format(VarTotParaGuia, "#,###.00")) + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + ""
                            + "Cumprimentos";

            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
        }

        private void TestaPlafond()
        {
            if (this.DocumentoVenda.Entidade + "" != "")
            {
                VarCliente = this.DocumentoVenda.Entidade;
                VarClienteNome = BSO.Base.Clientes.Edita(VarCliente).Nome;
                if (BSO.Base.Clientes.Edita(VarCliente).Limitecredito + "" == "")
                    VarPlafondCred = 0;
                else
                    VarPlafondCred = BSO.Base.Clientes.Edita(VarCliente).Limitecredito;

                // ######  Retirada a soma do VarPlafondExtra no dia 04/11/2016 a pedido da Dona Goretti. #######
                // If BSO.Comercial.Clientes.Edita(VarCliente).CamposUtil("CDU_PlafondExtra").Valor & "" = "" Then
                // VarPlafondExtra = 0
                // Else
                // VarPlafondExtra = BSO.Comercial.Clientes.Edita(VarCliente).CamposUtil("CDU_PlafondExtra").Valor
                // End If

                // VarPlafondCliente = VarPlafondCred + VarPlafondExtra

                VarPlafondCliente = VarPlafondCred;

                // 1. Responsabilidade em Letras
                // 2. Valor em  Conta Corrente
                // 3. Guias de Remessa Por Facturar
                // 4. Cheques Pré-Datados
                // 5. Encomendas em Carteira

                SqlStringTotalDeb = "SELECT TOP 1 (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente "
                                + "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CCC') AND estado = 'PEN' AND Entidade = '" + VarCliente + "' "
                                + "AND DataVenc < '2099-12-31') AS ValorContaCorrente "
                                + ", (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente "
                                + "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CCC') AND estado = 'CPD' AND Entidade = '" + VarCliente + "' "
                                + "AND DataVenc < '2099-12-31') AS ValorCPD "
                                + ", (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente "
                                + "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CLR') AND Entidade = '" + VarCliente + "' "
                                + "AND DataVenc < '2099-12-31') AS ValorLetras "
                                + "FROM Pendentes AS Pendentes_1";

                ListaTotalDeb = BSO.Consulta(SqlStringTotalDeb);

                if (ListaTotalDeb.Vazia() == true)
                {
                    VarTotValorCC = 0;
                    VarTotCheques = 0;
                    VarTotRespLetras = 0;
                }
                else
                {
                    VarTotValorCC = Math.Round(ListaTotalDeb.Valor("ValorContaCorrente"), 2);
                    VarTotCheques = Math.Round(ListaTotalDeb.Valor("ValorCPD"), 2);
                    VarTotRespLetras = Math.Round(ListaTotalDeb.Valor("ValorLetras"), 2);
                }

                SqlStringTotalDeb = "SELECT TOP 1 (SELECT ISNULL(SUM(CASE moeda WHEN 'EUR' THEN ((LinhasDoc.TaxaIva / 100) + 1) * (LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) "
                                + "ELSE ((LinhasDoc.TaxaIva / 100) + 1) * ((LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit))) / cambio END), 0) AS ValorGuiasRemessa "
                                + "FROM CabecDoc INNER JOIN LinhasDoc ON CabecDoc.Id = LinhasDoc.IdCabecDoc INNER JOIN LinhasDocStatus ON LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc INNER JOIN DocumentosVenda ON CabecDoc.TipoDoc = DocumentosVenda.Documento INNER JOIN CabecDocStatus ON LinhasDoc.IdCabecDoc = CabecDocStatus.IdCabecDoc "
                                + "WHERE (DocumentosVenda.TipoDocumento = 3) AND (CabecDocStatus.Estado = 'P') AND (LinhasDocStatus.EstadoTrans = 'P') AND (CabecDoc.Entidade = '" + VarCliente + "') "
                                + "AND CabecDoc.DataVencimento < '2099-12-31' AND (CabecDocStatus.Fechado = 0) AND (LinhasDocStatus.Fechado = 0) AND (CabecDocStatus.Anulado = 0) "
                                + "AND cabecdoc.tipodoc = 'GR') AS ValorGuiasRemessa "
                                + ", (SELECT ISNULL(SUM(CASE moeda WHEN 'EUR' THEN ((LinhasDoc.TaxaIva / 100) + 1) * (LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) "
                                + "ELSE ((LinhasDoc.TaxaIva / 100) + 1) * ((LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) / cambio) END), 0) AS ValorEncomedasCarteira "
                                + "FROM CabecDoc INNER JOIN LinhasDoc ON CabecDoc.Id = LinhasDoc.IdCabecDoc INNER JOIN LinhasDocStatus ON LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc INNER JOIN DocumentosVenda ON CabecDoc.TipoDoc = DocumentosVenda.Documento INNER JOIN CabecDocStatus ON LinhasDoc.IdCabecDoc = CabecDocStatus.IdCabecDoc "
                                + "WHERE (DocumentosVenda.TipoDocumento = 2) AND (CabecDocStatus.Estado = 'P') AND (LinhasDocStatus.EstadoTrans = 'P') AND (CabecDoc.Entidade = '" + VarCliente + "') "
                                + "AND (CabecDocStatus.Fechado = 0) AND (LinhasDocStatus.Fechado = 0) AND (CabecDocStatus.Anulado = 0) "
                                + "AND CabecDoc.DataVencimento < '2099-12-31') AS ValorEncomendasCarteira "
                                + "FROM CabecDoc AS CabecDoc_1";

                ListaTotalDeb = BSO.Consulta(SqlStringTotalDeb);

                if (ListaTotalDeb.Vazia() == true)
                {
                    VarTotGuias = 0;
                    VarTotEncomendas = 0;
                }
                else
                {
                    VarTotGuias = Math.Round(ListaTotalDeb.Valor("ValorGuiasRemessa"), 2);
                    VarTotEncomendas = Math.Round(ListaTotalDeb.Valor("ValorEncomendasCarteira"), 2);
                }

                VarTotalDoc = this.DocumentoVenda.TotalDocumento;

                VarTotParaEncomenda = VarTotRespLetras + VarTotValorCC + VarTotGuias + VarTotCheques + VarTotEncomendas + VarTotalDoc;
                VarTotParaGuia = VarTotRespLetras + VarTotValorCC + VarTotGuias + VarTotCheques + VarTotalDoc;

                if (this.DocumentoVenda.Tipodoc == "ECL" & VarTotalDoc != VarTotalDocGravado)
                {
                    if (VarTotParaEncomenda > VarPlafondCliente)
                    {
                        if (VarLocalTeste == 0)
                            MessageBox.Show("Cliente com plafond ultrapassado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (VarLocalTeste == 1)
                        {
                            if (MessageBox.Show("Cliente com plafond ultrapassado!" + Strings.Chr(13) + "Deseja mesmo assim gravar o documento ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                EnvioEmailPlafond();
                            else
                                VarCancelaDoc = true;
                        }
                    }
                }
                else if ((this.DocumentoVenda.Tipodoc == "GC" | this.DocumentoVenda.Tipodoc == "GR") & VarTotalDoc != VarTotalDocGravado)
                {
                    if (VarTotParaGuia > VarPlafondCliente)
                    {
                        if (VarLocalTeste == 0)
                            MessageBox.Show("Cliente com plafond ultrapassado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (VarLocalTeste == 1)
                        {
                            if (MessageBox.Show("Cliente com plafond ultrapassado!" + Strings.Chr(13) + "Deseja mesmo assim gravar o documento ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                EnvioEmailPlafond();
                            else
                                VarCancelaDoc = true;
                        }
                    }
                }

                VarTotalDocGravado = 0;
            }
        }
    }
}