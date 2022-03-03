using CmpBE100;
using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;
using System.Windows.Forms;

namespace Default
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);
            if (Module1.VerificaToken("Default") == 1)
            {
                if (this.DocumentoCompra.Entidade == "")
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor não está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }

                if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "1") & (this.DocumentoCompra.Tipodoc == "VFA" | this.DocumentoCompra.Tipodoc == "VNC"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é intracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "2") & (this.DocumentoCompra.Tipodoc == "VFA" | this.DocumentoCompra.Tipodoc == "VNC"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é extracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "0") & (this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "VCI"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é nacional, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "2") & (this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "VCI"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é extracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "0") & (this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VCO"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é nacional, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "1") & (this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VCO"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é intracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }

                if (this.DocumentoCompra.UtilizaMoradaAlternativaEntrega == false & this.DocumentoCompra.LocalDescarga + "" == "")
                {
                    if (this.DocumentoCompra.Tipodoc == "NGS" | this.DocumentoCompra.Tipodoc == "NGT")
                        this.DocumentoCompra.LocalDescarga = "V/ Morada";
                    else
                        this.DocumentoCompra.LocalDescarga = "N/ Morada";
                }

                // ################################################################################################################################################################
                // # Verificar se o documento a gravar tem moeda diferente de EURO com cambio a 1                          BMP - 06/05/2021                                       #
                // ################################################################################################################################################################

                if (this.DocumentoCompra.Moeda != "EUR" & this.DocumentoCompra.Cambio == 1)
                {
                    MessageBox.Show("Atenção, não foi possivel gravar o documento, corrigir a moeda ou cambio antes de gravar! A moeda " + this.DocumentoCompra.Moeda + " tem o cambio " + this.DocumentoCompra.Cambio + "", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                }
            }
        }

        private CmpBEDocumentoCompra DocCompra = new CmpBEDocumentoCompra();

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (Module1.VerificaToken("Default") == 1)
            {
                // Ctrl+L - Preenche dados dos campos de utilizador
                if (KeyCode == 76)
                {
                    // Verifica se é uma linha que não existe na tabela linhascompras
                    if (this.LinhaActual == -1)
                    {
                        return;
                    }

                    // Verifica se é uma linha de texto, sem artigo
                    if (this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Artigo + "" == "")
                    {
                        return;
                    }

                    Module1.ArtigoEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Artigo;
                    Module1.DescArtEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Descricao;
                    Module1.LoteEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Lote;
                    Module1.LinhaEnc = this.LinhaActual;
                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmOutrosDadosView));
                    FrmOutrosDadosView frm = result.Result;
                    frm.DocumentoCompra = DocumentoCompra;
                    frm.ShowDialog();
                }

                // Alt+Q - Altera Encomenda
                if (KeyCode == 81 & BSO.Compras.TabCompras.Edita(this.DocumentoCompra.Tipodoc).TipoDocumento == 2)
                {
                    // Verifica se é uma linha que não existe na tabela linhascompras
                    if (this.LinhaActual == -1)
                    {
                        return;
                    }

                    // Verifica se é uma linha de texto, sem artigo
                    if (this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Artigo + "" == "")
                    {
                        return;
                    }

                    Module1.NovaQuantidadeEnc = 0;
                    Module1.NovaQtReservadaEnc = 0;
                    Module1.NovoPrecoEnc = 0;
                    Module1.ArtigoEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Artigo;
                    Module1.DescArtEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Descricao;
                    Module1.LoteEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Lote;
                    Module1.QuantidadeEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Quantidade;
                    Module1.QtReservadaEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).QuantReservada;
                    Module1.QtSatisfeitaEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).QuantSatisfeita;
                    Module1.PrecoEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).PrecUnit;
                    Module1.IdLinhaEnc = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).IdLinha;
                    Module1.ObsEnc = DocumentoCompra.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_Observacoes"].Valor.ToString();


                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmAlteraEstadoEncomendaView));

                    if (result.ResultCode == ExtensibilityResultCode.Ok)
                    {
                        FrmAlteraEstadoEncomendaView frm = result.Result;
                        frm.desativarOF = true;
                        frm.ShowDialog();
                    }

                    if (Module1.Opcao == 1)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE CABECCOMPRASSTATUS SET FECHADO = 0 WHERE IDCABECCOMPRAS = '" + this.DocumentoCompra.ID + "'");

                        DocCompra = BSO.Compras.Documentos.Edita(DocumentoCompra.Filial, DocumentoCompra.Tipodoc, DocumentoCompra.Serie, DocumentoCompra.NumDoc);

                        for (int i = 1, loopTo = DocCompra.Linhas.NumItens; i <= loopTo; i++)
                        {
                            if (DocCompra.Linhas.GetEdita(i).Estado == "P")
                            {
                                BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRASSTATUS SET FECHADO = 0 WHERE IDLINHASCOMPRAS = '" + Module1.IdLinhaEnc + "'");
                            }
                        }
                    }
                    else if (Module1.Opcao == 2)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRASSTATUS SET FECHADO = 0 WHERE IDLINHASCOMPRAS = '" + Module1.IdLinhaEnc + "'");
                    }
                    else if (Module1.Opcao == 3)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRASSTATUS SET FECHADO = 1 WHERE IDLINHASCOMPRAS = '" + Module1.IdLinhaEnc + "'");
                    }
                    else if (Module1.Opcao == 4)
                    {
                        if (Module1.NovaQuantidadeEnc != 0)
                        {
                            this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Quantidade = Module1.NovaQuantidadeEnc;
                            BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRAS SET QUANTIDADE = " + Strings.Replace(Module1.NovaQuantidadeEnc.ToString(), ",", ".") + " WHERE ID = '" + Module1.IdLinhaEnc + "'");
                            BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRASSTATUS SET FECHADO = 0, QUANTIDADE = " + Strings.Replace(Module1.NovaQuantidadeEnc.ToString(), ",", ".") + " WHERE IDLINHASCOMPRAS = '" + Module1.IdLinhaEnc + "'");

                            DocCompra = BSO.Compras.Documentos.Edita(DocumentoCompra.Filial, DocumentoCompra.Tipodoc, DocumentoCompra.Serie, DocumentoCompra.NumDoc);

                            BSO.Compras.Documentos.Actualiza(DocCompra);

                            DocCompra = BSO.Compras.Documentos.Edita(DocumentoCompra.Filial, DocumentoCompra.Tipodoc, DocumentoCompra.Serie, DocumentoCompra.NumDoc);

                            for (int i = 1, loopTo1 = DocCompra.Linhas.NumItens; i <= loopTo1; i++)
                            {
                                if (DocCompra.Linhas.GetEdita(i).Estado == "P")
                                {
                                    BSO.DSO.ExecuteSQL("UPDATE CABECCOMPRASSTATUS SET ESTADO = 'P', ANULADO = 0, FECHADO = 0 WHERE IDCABECCOMPRAS = '" + DocCompra.ID + "'");
                                    break;
                                }

                                BSO.DSO.ExecuteSQL("UPDATE CABECCOMPRASSTATUS SET ESTADO = 'T' WHERE IDCABECDOC = '" + DocCompra.ID + "'");
                            }
                        }

                        if (Module1.NovaQtReservadaEnc != 0)
                        {
                            this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Quantidade = Module1.NovaQtReservadaEnc;
                            BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRASSTATUS SET QUANTRESERV = " + Strings.Replace(Module1.NovaQtReservadaEnc.ToString(), ",", ".") + " WHERE IDLINHASCOMPRAS = '" + Module1.IdLinhaEnc + "'");

                            DocCompra = BSO.Compras.Documentos.Edita(DocumentoCompra.Filial, DocumentoCompra.Tipodoc, DocumentoCompra.Serie, DocumentoCompra.NumDoc);

                            BSO.Compras.Documentos.Actualiza(DocCompra);
                        }

                        if (Module1.NovoPrecoEnc != 0)
                        {
                            this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).PrecUnit = Module1.NovoPrecoEnc;
                            BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRAS SET PRECUNIT = " + Strings.Replace(Module1.NovoPrecoEnc.ToString(), ",", ".") + " WHERE ID = '" + Module1.IdLinhaEnc + "'");

                            DocCompra = BSO.Compras.Documentos.Edita(DocumentoCompra.Filial, DocumentoCompra.Tipodoc, DocumentoCompra.Serie, DocumentoCompra.NumDoc);

                            BSO.Compras.Documentos.Actualiza(DocCompra);
                        }
                    }
                    else if (Module1.Opcao == 7)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE CABECCOMPRASSTATUS SET FECHADO = 1 WHERE IDCABECCOMPRAS = '" + this.DocumentoCompra.ID + "'");

                        DocCompra = BSO.Compras.Documentos.Edita(DocumentoCompra.Filial, DocumentoCompra.Tipodoc, DocumentoCompra.Serie, DocumentoCompra.NumDoc);

                        for (int i = 1, loopTo2 = DocCompra.Linhas.NumItens; i <= loopTo2; i++)
                        {
                            if (DocCompra.Linhas.GetEdita(i).Estado == "P")
                            {
                                BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRASSTATUS SET FECHADO = 1 WHERE IDLINHASCOMPRAS = '" + Module1.IdLinhaEnc + "'");
                            }
                        }
                    }
                    DocumentoCompra.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_Observacoes"].Valor = Module1.ObsEnc;
                    BSO.DSO.ExecuteSQL("UPDATE LinhasCompras SET CDU_Observacoes = '" + Module1.ObsEnc + "' WHERE Id = '" + Module1.IdLinhaEnc + "'");
                }
            }
        }
        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            if (Module1.VerificaToken("Default") == 1)
            {
                base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

                if (BSO.Base.Artigos.DaValorAtributo(this.DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra") + "" != "")
                {
                    this.DocumentoCompra.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") + " " + BSO.Base.Artigos.DaValorAtributo(this.DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra");
                }
            }

        }


        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            if (Module1.VerificaToken("Default") == 1)
            {
                base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

                ActualizaQtdSatisfeitaContrato();

            }
        }

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            if (Module1.VerificaToken("Default") == 1)
            {
                base.DepoisDeTransformar(e);

                // ##############################################################################################
                // ###Validar criação de documentos de Compras se foi transformado de ECF --- Bruno 18/12/2019###
                // ##############################################################################################
                if (this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "WE" | this.DocumentoCompra.Tipodoc == "WEI" | this.DocumentoCompra.Tipodoc == "WEO" | this.DocumentoCompra.Tipodoc == "VIT")
                {
                    int i;
                    StdBELista list = new StdBELista();
                    var loopTo = this.DocumentoCompra.Linhas.NumItens;
                    for (i = 1; i <= loopTo; i++)
                    {
                        if (this.DocumentoCompra.Linhas.GetEdita(i).IDLinhaOriginal != string.Empty)
                        {

                            list = BSO.Consulta("select cc.TipoDoc from LinhasCompras lc inner join CabecCompras cc on cc.Id=lc.IdCabecCompras where lc.Id='" + DocumentoCompra.Linhas.GetEdita(i).IDLinhaOriginal + "'");
                            list.Inicio();
                            if (list.Valor("TipoDoc") != "ECF")
                            {
                                Interaction.MsgBox("Não vai ser possivel gravar o documento! Este documento não foi transformado por uma ECF! ", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly));
                                NaoGravar = true;
                            }
                        }
                    }
                }

                // ###############################################################################################
                // ###############################################################################################
                // ###############################################################################################
            }
        }


        public override void FornecedorIdentificado(string Fornecedor, ref bool Cancel, ExtensibilityEventArgs e)
        {
            if (Module1.VerificaToken("Default") == 1)
            {
                base.FornecedorIdentificado(Fornecedor, ref Cancel, e);

                if (this.DocumentoCompra.Tipodoc == "NGS" | this.DocumentoCompra.Tipodoc == "NGT")
                {
                    this.DocumentoCompra.LocalCarga = "N/ Morada";
                    this.DocumentoCompra.LocalDescarga = "V/ Morada";
                }


                if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "1") & (this.DocumentoCompra.Tipodoc == "VFA" | this.DocumentoCompra.Tipodoc == "VNC"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é intracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "2") & (this.DocumentoCompra.Tipodoc == "VFA" | this.DocumentoCompra.Tipodoc == "VNC"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é extracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "0") & (this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "VCI"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é nacional, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "2") & (this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "VCI"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é extracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "0") & (this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VCO"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é nacional, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "1") & (this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VCO"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é intracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
            }

            }
            bool NaoGravar;
            StdBELista ListaIdCopia = new StdBELista();
            StdBELista ListaQtdIdCopia = new StdBELista();
            string SqlStringIdCopia;
            string SqlStringQtdIdCopia;
            private void ActualizaQtdSatisfeitaContrato()
            {
                if (this.DocumentoCompra.Tipodoc == "ECF")
                {
                    SqlStringIdCopia = "SELECT dbo.LinhasCompras.IdLinhaOrigemCopia "
                                       + "FROM dbo.CabecCompras INNER JOIN dbo.LinhasCompras ON dbo.CabecCompras.Id = dbo.LinhasCompras.IdCabecCompras "
                                       + "WHERE (dbo.CabecCompras.TipoDoc = '" + this.DocumentoCompra.Tipodoc + "') AND (dbo.CabecCompras.Serie = '" + this.DocumentoCompra.Serie + "') AND (dbo.CabecCompras.NumDoc = " + this.DocumentoCompra.NumDoc + ") AND (dbo.LinhasCompras.IdLinhaOrigemCopia IS NOT NULL) "
                                       + "GROUP BY dbo.LinhasCompras.IdLinhaOrigemCopia";

                    ListaIdCopia = BSO.Consulta(SqlStringIdCopia);


                    if (ListaIdCopia.Vazia() == false)
                    {
                        ListaIdCopia.Inicio();

                        for (var i = 1; i <= ListaIdCopia.NumLinhas(); i++)
                        {
                            SqlStringQtdIdCopia = "SELECT SUM(Quantidade) AS TOTAL "
                                                  + "FROM dbo.LinhasCompras "
                                                  + "WHERE (IdLinhaOrigemCopia = '" + ListaIdCopia.Valor("IdLinhaOrigemCopia") + "')";


                            ListaQtdIdCopia = BSO.Consulta(SqlStringQtdIdCopia);


                            if (ListaQtdIdCopia.Vazia() == false)
                            {
                                ListaQtdIdCopia.Inicio();

                                BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRASSTATUS SET QUANTTRANS = " + Strings.Replace(ListaQtdIdCopia.Valor("TOTAL").ToString(), ",", ".") + " WHERE IDLINHASCOMPRAS = '" + ListaIdCopia.Valor("IdLinhaOrigemCopia") + "'");
                            }

                            ListaIdCopia.Seguinte();
                        }
                    }
                }
            }
        }
    }