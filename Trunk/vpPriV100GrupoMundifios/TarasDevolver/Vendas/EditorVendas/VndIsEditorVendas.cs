using Generico;
using IntBE100;
using InvBE100;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System;
using VndBE100;

namespace TarasDevolver
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void DepoisDeDuplicar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeDuplicar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("TarasDevolver") == 1)
            {
                // #### TARAS A DEVOLVER ####
                // *******************************************************************************************************************************************
                this.DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor = 0;
                this.DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor = 0;
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("TarasDevolver") == 1)
            {
                // *******************************************************************************************************************************************
                // #### TARAS A DEVOLVER ####
                // *******************************************************************************************************************************************
                if ((this.DocumentoVenda.Tipodoc == "GR" | this.DocumentoVenda.Tipodoc == "GT") & (this.DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor.ToString() == "0" & this.DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor.ToString() == "0"))

                    // EduSamp
                    GeraDocumentosStock(DocumentoVenda);
            }
        }

        public void GeraDocumentosStock(VndBEDocumentoVenda DocumentoVenda)
        {
            InvBEDocumentoTransf DocStk;
            InvBELinhaOrigemTransf LinhaStk;

            IntBEDocumentoInterno DocStk_Saida; // Documento de saída de stock, para quando a checkbox está a false
            IntBELinhaDocumentoInterno LinhaStk_Saida;

            string v_Artigo="";
            string v_Armazem = "";
            string v_Localizacao = "";
            string v_Lote;
            string v_LoteOrigem = "";
            double v_Quantidade=0;
            double v_PrecoUnit = 0;

            bool Devolver = false;

            if (DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor.ToString() == "0")
            {
                DocStk = new InvBEDocumentoTransf();
            }
            else
            {
                DocStk = BSO.Inventario.Transferencias.Edita("TTR", int.Parse(DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor.ToString()), "000", DateAndTime.Year(DocumentoVenda.DataDoc).ToString());
                DocStk.LinhasOrigem.RemoveTodos();
            }

            if (DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor.ToString() == "0")
            {
                DocStk_Saida = new IntBEDocumentoInterno();
            }
            else
            {
                DocStk_Saida = BSO.Internos.Documentos.Edita("SS", int.Parse(DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor.ToString()), DateAndTime.Year(DocumentoVenda.DataDoc).ToString(), "000");
                DocStk.LinhasOrigem.RemoveTodos();
            }

            
            DocStk.Filial = "000";
            DocStk.Tipodoc = "TTR";
            DocStk.Serie = DateAndTime.Year(DocumentoVenda.DataDoc).ToString();
            DocStk.Data = DocumentoVenda.DataDoc.Date.Add(DocumentoVenda.DataHoraCarga.TimeOfDay);
            DocStk.Moeda = "EUR";
            DocStk.Cambio = 1;
            DocStk.CambioMAlt = 1;
            DocStk.CambioMBase = 1;
            DocStk.Utilizador = BSO.Contexto.UtilizadorActual;
            DocStk.TipoEntidade = "C";
            DocStk.Entidade = DocumentoVenda.Entidade;
            //DocStk.ArmazemOrigem = v_Armazem;
            v_Armazem = "TR";
            v_Localizacao = "TR";
            v_LoteOrigem = "0001";
            v_Lote = DocumentoVenda.Entidade;


            //duas linhasorigem comentario

            //'Linha nº 1 de texto
            LinhaStk = new InvBELinhaOrigemTransf();
            LinhaStk.IdLinha = Guid.NewGuid().ToString();
            LinhaStk.TipoLinha = ConstantesPrimavera100.Documentos.TipoLinComentario;
            LinhaStk.Descricao = "Referente à " + BSO.Vendas.TabVendas.Edita(DocumentoVenda.Tipodoc).Descricao + " " + DocumentoVenda.Tipodoc + " " + DocumentoVenda.Serie + "/" + DocumentoVenda.NumDoc;
            LinhaStk.Lote = "<L01>";
            DocStk.LinhasOrigem.Insere(LinhaStk);

            //'Linha nº 2 de texto
            LinhaStk = new InvBELinhaOrigemTransf();
            LinhaStk.IdLinha = Guid.NewGuid().ToString();
            LinhaStk.TipoLinha = ConstantesPrimavera100.Documentos.TipoLinComentario;
            LinhaStk.Descricao = "";
            LinhaStk.Lote = "<L01>";
            DocStk.LinhasOrigem.Insere(LinhaStk);

            //LinhaStk = new InvBELinhaOrigemTransf();
            ////'Linha nº 1 de texto
            //InvBELinhaDestinoTransf LinhaDestinoStk = new InvBELinhaDestinoTransf();
            //LinhaDestinoStk.TipoLinha = "60";
            //LinhaDestinoStk.Descricao = "Referente à " + BSO.Vendas.TabVendas.Edita(DocumentoVenda.Tipodoc).Descricao + " " + DocumentoVenda.Tipodoc + " " + DocumentoVenda.Serie + "/" + DocumentoVenda.NumDoc;
            //LinhaDestinoStk.Lote = "<L01>";
            //LinhaStk.LinhasDestino.Insere(LinhaDestinoStk);

            ////'Linha nº 2 de texto
            //LinhaDestinoStk = new InvBELinhaDestinoTransf();
            //LinhaStk.TipoLinha = "60";
            //LinhaStk.Descricao = "";
            //LinhaStk.Lote = "<L01>";
            //LinhaStk.LinhasDestino.Insere(LinhaDestinoStk);






            //********************************************************************************************************************

            DocStk_Saida.Filial = "000";
            DocStk_Saida.Tipodoc = "SS";
            DocStk_Saida.Serie = DateAndTime.Year(DocumentoVenda.DataDoc).ToString();
            DocStk_Saida.Data = DocumentoVenda.DataDoc.Date.Add(DocumentoVenda.DataHoraCarga.TimeOfDay);
            DocStk_Saida.DataVencimento = DocStk_Saida.Data;
            DocStk_Saida.Moeda = "EUR";
            DocStk_Saida.Cambio = 1;
            DocStk_Saida.CambioMAlt = 1;
            DocStk_Saida.CambioMBase = 1;
            DocStk_Saida.Utilizador = BSO.Contexto.UtilizadorActual;
            DocStk_Saida.TipoEntidade = "C";
            DocStk_Saida.Entidade = DocumentoVenda.Entidade;
            BSO.Internos.Documentos.PreencheDadosRelacionados(DocStk_Saida);
            //DocStk_Saida.ArmazemOrigem = v_Armazem;

            //'Linha nº 1 de texto
            LinhaStk_Saida = new IntBELinhaDocumentoInterno();
            LinhaStk_Saida.TipoLinha = "60";
            LinhaStk_Saida.Descricao = "Referente à " + BSO.Vendas.TabVendas.Edita(DocumentoVenda.Tipodoc).Descricao + " " + DocumentoVenda.Tipodoc + " " + DocumentoVenda.Serie + "/" + DocumentoVenda.NumDoc;
            LinhaStk_Saida.Lote = "<L01>";
            //LinhaStk_Saida.LoteOrigem = "<L01>";
            DocStk_Saida.Linhas.Insere(LinhaStk_Saida);

            //'Linha nº 2 de texto
            LinhaStk_Saida = new IntBELinhaDocumentoInterno();
            LinhaStk_Saida.TipoLinha = "60";
            LinhaStk_Saida.Descricao = "";
            LinhaStk_Saida.Lote = "<L01>";
            //LinhaStk_Saida.LoteOrigem = "<L01>";
            DocStk_Saida.Linhas.Insere(LinhaStk_Saida);

            for (int i = 1; i <= 7; i++)
            {
                switch (i)
                {
                    case 1:
                        {
                            v_Artigo = "TRCO001";
                            v_Quantidade = double.Parse(this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor.ToString());
                            Devolver = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverConesCartao"].Valor.ToString());
                            break;
                        }

                    case 2:
                        {
                            v_Artigo = "TRCO002";
                            v_Quantidade = double.Parse(this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor.ToString());
                            Devolver = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverConesPlastico"].Valor.ToString());
                            break;
                        }

                    case 3:
                        {
                            v_Artigo = "TRTU001";
                            v_Quantidade = double.Parse(this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor.ToString());
                            Devolver = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverTubosCartao"].Valor.ToString());
                            break;
                        }

                    case 4:
                        {
                            v_Artigo = "TRTU002";
                            v_Quantidade = double.Parse(this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor.ToString());
                            Devolver = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverTubosPlastico"].Valor.ToString());
                            break;
                        }

                    case 5:
                        {
                            v_Artigo = "TRPA001";
                            v_Quantidade = double.Parse(this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor.ToString());
                            Devolver = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesMadeira"].Valor.ToString());
                            break;
                        }

                    case 6:
                        {
                            v_Artigo = "TRPA002";
                            v_Quantidade = double.Parse(this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor.ToString());
                            Devolver = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesPlastico"].Valor.ToString());
                            break;
                        }

                    case 7:
                        {
                            v_Artigo = "TRSE001";
                            v_Quantidade = double.Parse(this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor.ToString());
                            Devolver = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverSeparadoresCartao"].Valor.ToString());
                            break;
                        }
                }

                if (v_Quantidade == 0)
                    continue;

                v_PrecoUnit = 0;

                // Criar lote caso não exista
                if (BSO.Inventario.ArtigosLotes.Existe(v_Artigo, v_Lote) == false)
                {
                    var v_NovoLote = default(InvBEArtigoLote);

                    v_NovoLote = new InvBEArtigoLote();

                    v_NovoLote.Artigo = v_Artigo;
                    v_NovoLote.Lote = v_Lote;
                    v_NovoLote.Descricao = Strings.Left(this.DocumentoVenda.Nome, 30);
                    v_NovoLote.Activo = true;
                    BSO.Inventario.ArtigosLotes.Actualiza(v_NovoLote);
                }

                //destino
                if (Devolver == true)
                {
                    BSO.Inventario.Transferencias.AdicionaLinhaOrigem(DocStk, v_Artigo, v_Armazem, v_Localizacao, "DISP", v_Quantidade, v_LoteOrigem);

                    LinhaStk = DocStk.LinhasOrigem.GetEdita(DocStk.LinhasOrigem.NumItens);
                    LinhaStk.DataStock = DocumentoVenda.DataDoc.Date.Add(DocumentoVenda.DataHoraCarga.TimeOfDay);

                    InvBELinhaDestinoTransf linhaStkDst = LinhaStk.LinhasDestino.GetEdita(1);
                    linhaStkDst.Lote = v_Lote;
                    linhaStkDst.DataStock = DocumentoVenda.DataDoc.Date.Add(DocumentoVenda.DataHoraCarga.TimeOfDay); 
                }
                else
                {
                    BSO.Internos.Documentos.AdicionaLinha(DocStk_Saida, v_Artigo, v_Armazem, v_Localizacao, v_Lote, 0,0,v_Quantidade);
                    // DocStk_Saida.Linhas(DocStk.Linhas.NumItens).LoteOrigem = v_LoteOrigem
                    // DocStk_Saida.Linhas(DocStk.Linhas.NumItens).LocalizacaoOrigem = v_Localizacao
                    DocStk_Saida.Linhas.GetEdita(DocStk.LinhasOrigem.NumItens).DataStock = DocumentoVenda.DataDoc.Date.Add(DocumentoVenda.DataHoraCarga.TimeOfDay);
                }
            }
            string erros = "";
            if (DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor.ToString() == "0" & LinhaStk.LinhasDestino.NumItens > 0)
            {
                BSO.Inventario.Transferencias.Actualiza(DocStk,ref erros);
                BSO.Vendas.Documentos.ActualizaValorAtributo(this.DocumentoVenda.Filial, this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, "CDU_NumDocStock", DocStk.NumDoc);
            }
            else if (DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor.ToString() != "0")
            {
                BSO.Inventario.Transferencias.Actualiza(DocStk, ref erros);
            }

            if (DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor.ToString() == "0" & DocStk_Saida.Linhas.NumItens >= 3)
            {
                BSO.Internos.Documentos.Actualiza(DocStk_Saida);
                BSO.Vendas.Documentos.ActualizaValorAtributo(this.DocumentoVenda.Filial, this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, "CDU_NumDocSaidaStock", DocStk_Saida.NumDoc);
            }
            else if (DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor.ToString() != "0")
            {
                BSO.Internos.Documentos.Actualiza(DocStk_Saida);
            }
        }

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            {
                if (Module1.VerificaToken("TarasDevolver") == 1)
                {
                    // *******************************************************************************************************************************************
                    // #### TARAS A DEVOLVER ####
                    // *******************************************************************************************************************************************
                    // Alt+T
                    if (KeyCode == 84 & (this.DocumentoVenda.Tipodoc == "GR" | this.DocumentoVenda.Tipodoc == "GT"))
                    {
                        Module1.ConesCartao = int.Parse(this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor.ToString());
                        Module1.ConesPlastico = int.Parse(this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor.ToString());
                        Module1.TubosCartao = int.Parse(this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor.ToString());
                        Module1.TubosPlastico = int.Parse(this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor.ToString());
                        Module1.PaletesMadeira = int.Parse(this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor.ToString());
                        Module1.PaletesPlastico = int.Parse(this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor.ToString());
                        Module1.SeparadoresCartao = int.Parse(this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor.ToString());

                        // EduSamp 03/01/2017
                        Module1.Devolver_ConesCartao = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverConesCartao"].Valor.ToString());
                        Module1.Devolver_ConesPlastico = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverConesPlastico"].Valor.ToString());
                        Module1.Devolver_TubosCartao = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverTubosCartao"].Valor.ToString());
                        Module1.Devolver_TubosPlastico = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverTubosPlastico"].Valor.ToString());
                        Module1.Devolver_PaletesMadeira = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesMadeira"].Valor.ToString());
                        Module1.Devolver_PaletesPlastico = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesPlastico"].Valor.ToString());
                        Module1.Devolver_SeparadoresCartao = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverSeparadoresCartao"].Valor.ToString());

                        ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmTarasDevolverView));
                        FrmTarasDevolverView frm = result.Result;
                        frm.ShowDialog();

                        this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor = Module1.ConesCartao;
                        this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor = Module1.ConesPlastico;
                        this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor = Module1.TubosCartao;
                        this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor = Module1.TubosPlastico;
                        this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor = Module1.PaletesMadeira;
                        this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor = Module1.PaletesPlastico;
                        this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor = Module1.SeparadoresCartao;

                        // EduSamp 03/01/2017
                        this.DocumentoVenda.CamposUtil["CDU_DevolverConesCartao"].Valor = Module1.Devolver_ConesCartao;
                        this.DocumentoVenda.CamposUtil["CDU_DevolverConesPlastico"].Valor = Module1.Devolver_ConesPlastico;
                        this.DocumentoVenda.CamposUtil["CDU_DevolverTubosCartao"].Valor = Module1.Devolver_TubosCartao;
                        this.DocumentoVenda.CamposUtil["CDU_DevolverTubosPlastico"].Valor = Module1.Devolver_TubosPlastico;
                        this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesMadeira"].Valor = Module1.Devolver_PaletesMadeira;
                        this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesPlastico"].Valor = Module1.Devolver_PaletesPlastico;
                        this.DocumentoVenda.CamposUtil["CDU_DevolverSeparadoresCartao"].Valor = Module1.Devolver_SeparadoresCartao;
                        for (int i = 1, loopTo = this.DocumentoVenda.Linhas.NumItens; i <= loopTo; i++)
                        {
                            if (i > this.DocumentoVenda.Linhas.NumItens)
                            {
                                break;
                            }

                            if (this.DocumentoVenda.Linhas.GetEdita(i).Descricao == "Taras a Devolver:" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 14) == "Cone(s) Cartão" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 16) == "Cone(s) Plástico" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 14) == "Tubo(s) Cartão" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 16) == "Tubo(s) Plástico" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 17) == "Palete(s) Madeira" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 18) == "Palete(s) Plástico" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 20) == "Separador(es) Cartão")

                            {
                                this.DocumentoVenda.Linhas.Remove(i);
                                i = i - 1;
                            }
                        }

                        if (Module1.TotalTaras > 0 & Module1.TotalTaras_a_Devolver > 0)
                        {
                            for (int i = 0; i <= 7; i++)
                            {
                                var LinhaDoc = new VndBELinhaDocumentoVenda();
                                LinhaDoc = new VndBELinhaDocumentoVenda();
                                LinhaDoc.TipoLinha = "60";
                                LinhaDoc.Lote = "<L01>";
                                switch (i)
                                {
                                    case 0:
                                        {
                                            LinhaDoc.Descricao = "Taras a Devolver:";
                                            break;
                                        }

                                    case 1:
                                        {
                                            if (this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverConesCartao"].Valor == false)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor + " Cone(s) Cartão";
                                            }

                                            break;
                                        }

                                    case 2:
                                        {
                                            if (this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverConesPlastico"].Valor == false)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor + " Cone(s) Plástico";
                                            }

                                            break;
                                        }

                                    case 3:
                                        {
                                            if (this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverTubosCartao"].Valor == false)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor + " Tubo(s) Cartão";
                                            }

                                            break;
                                        }

                                    case 4:
                                        {
                                            if (this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverTubosPlastico"].Valor == false)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor + " Tubo(s) Plástico";
                                            }

                                            break;
                                        }

                                    case 5:
                                        {
                                            if (this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesMadeira"].Valor == false)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor + " Palete(s) Madeira";
                                            }

                                            break;
                                        }

                                    case 6:
                                        {
                                            if (this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesPlastico"].Valor == false)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor + " Palete(s) Plástico";
                                            }

                                            break;
                                        }

                                    case 7:
                                        {
                                            if (this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverSeparadoresCartao"].Valor == false)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor + " Separador(es) Cartão";
                                            }

                                            break;
                                        }
                                }

                                this.DocumentoVenda.Linhas.Insere(LinhaDoc);
                            }
                        }
                    }
                }
            }
        }
    }
}