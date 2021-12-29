//using Generico;
//using Microsoft.VisualBasic;
//using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
//using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
//using Primavera.Extensibility.Sales.Editors;
//using VndBE100;
//using IntBE100;
//using InvBE100;
//using System;

//namespace TarasDevolver
//{
//    public class VndIsEditorVendas : EditorVendas
//    {
//        public override void DepoisDeDuplicar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
//        {
//            base.DepoisDeDuplicar(Filial, Tipo, Serie, NumDoc, e);

//            if (Module1.VerificaToken("TarasDevolver") == 1)
//            {
//                // #### TARAS A DEVOLVER ####
//                // *******************************************************************************************************************************************
//                this.DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor = 0;
//                this.DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor = 0;
//            }
//        }

//        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
//        {
//            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

//            if (Module1.VerificaToken("TarasDevolver") == 1)
//            {
//                // *******************************************************************************************************************************************
//                // #### TARAS A DEVOLVER ####
//                // *******************************************************************************************************************************************
//                if ((this.DocumentoVenda.Tipodoc == "GR" | this.DocumentoVenda.Tipodoc == "GT") & (this.DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor.ToString() == "0" & this.DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor.ToString() == "0"))

//                    // EduSamp
//                    GeraDocumentosStock(int.Parse(this.DocumentoVenda.CamposUtil["CDU_NumDocStock"].Valor.ToString()), int.Parse(this.DocumentoVenda.CamposUtil["CDU_NumDocSaidaStock"].Valor.ToString()), int.Parse(this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor.ToString()), int.Parse(this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor.ToString()), int.Parse(this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor.ToString()), int.Parse(this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor.ToString()), int.Parse(this.DocumentoVenda.CamposUtil["CDU_PaletesCartao"].Valor.ToString()), int.Parse(this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor.ToString()), int.Parse(this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor.ToString()));
//            }
//        }

//        public void GeraDocumentosStock(int v_NumDocStk, int v_NumDocSaidaStk, int v_ConesCartao, int v_ConesPlastico, int v_TubosCartao, int v_TubosPlastico, int v_PaletesMadeira, int v_PaletesPlastico, int v_SeparadoresCartao)
//        {
//            int i;
//            int k;
//            InvBEDocumentoTransf DocStk;
//            InvBEDocumentoTransf DocStk_Saida; // Documento de saída de stock, para quando a checkbox está a false

//            InvBELinhaDestinoTransf LinhaStk;
//            InvBELinhaOrigemTransf LinhaStk_Saida;

//            string v_Artigo;
//            string v_Armazem;
//            string v_Localizacao;
//            string v_Lote;
//            string v_LoteOrigem;
//            double v_Quantidade;
//            double v_PrecoUnit;

//            bool Devolver;

//            if (v_NumDocStk == 0)
//                DocStk = new InvBEDocumentoTransf();
//            else
//            {
//                DocStk = BSO.Inventario.Transferencias.Edita("000", "S", "TTR", DateAndTime.Year(DocumentoVenda.DataDoc), v_NumDocStk);
//                DocStk.LinhasOrigem.RemoveTodos();
//            }

//            if (v_NumDocSaidaStk == 0)
//                DocStk_Saida = new InvBEDocumentoTransf();
//            else
//            {
//                DocStk_Saida = BSO.Inventario.Transferencias.Edita("000", "S", "SS", DateAndTime.Year(DocumentoVenda.DataDoc), v_NumDocSaidaStk);
//                DocStk.LinhasOrigem.RemoveTodos();
//            }

//            LinhaStk = new InvBELinhaDestinoTransf();

//            DocStk.Filial = "000";
//            DocStk.Tipodoc = "TTR";
//            DocStk.Serie = DateAndTime.Year(DocumentoVenda.DataDoc).ToString();
//            DocStk.Data = DateTime.Parse(Strings.Format(DocumentoVenda.DataDoc + " " + DocumentoVenda.DataHoraCarga, "dd-MM-yyyy hh:mm:ss").ToString());
//            DocStk.Modulo = "S";
//            DocStk.Moeda = "EUR";
//            DocStk.Cambio = 1;
//            DocStk.CambioMAlt = 1;
//            DocStk.CambioMBase = 1;
//            DocStk.Utilizador = BSO.Contexto.UtilizadorActual;
//            DocStk.TipoEntidade = "C";
//            DocStk.Entidade = DocumentoVenda.Entidade;

//            v_Armazem = "TR";
//            v_Localizacao = "TR";
//            v_LoteOrigem = "0001";
//            v_Lote = DocumentoVenda.Entidade;

//            DocStk.ArmazemOrigem = v_Armazem;
//            //'Linha nº 1 de texto
//            LinhaStk = new InvBELinhaDestinoTransf();
//            LinhaStk.TipoLinha = "60";
//            LinhaStk.Descricao = "Referente à " + BSO.Vendas.TabVendas.Edita(DocumentoVenda.Tipodoc).Descricao + " " + DocumentoVenda.Tipodoc + " " + DocumentoVenda.Serie + "/" + DocumentoVenda.NumDoc;
//            LinhaStk.Lote = "<L01>";
//            LinhaStk.LoteOrigem = "<L01>";
//            DocStk.LinhasOrigem.Insere(LinhaStk);

//            //'Linha nº 2 de texto
//            LinhaStk = new InvBELinhaDestinoTransf();
//            LinhaStk.TipoLinha = "60";
//            LinhaStk.Descricao = "";
//            LinhaStk.Lote = "<L01>";
//            LinhaStk.LoteOrigem = "<L01>";
//            DocStk_Saida.LinhasOrigem.Insere(LinhaStk);
//            //'********************************************************************************************************************************************************************************************************
//            //'Se o total de devoluções de taras for diferente de 7
//            DocStk_Saida.Filial = "000";
//            DocStk_Saida.Tipodoc = "SS";
//            DocStk_Saida.Serie = DateAndTime.Year(DocumentoVenda.DataDoc).ToString();
//            DocStk_Saida.Data = Strings.Format(DocumentoVenda.DataDoc + " " + DocumentoVenda.DataHoraCarga, "dd-MM-yyyy hh:mm:ss");
//            DocStk_Saida.Modulo = "S";
//            DocStk_Saida.Moeda = "EUR";
//            DocStk_Saida.Cambio = 1;
//            DocStk_Saida.CambioMAlt = 1;
//            DocStk_Saida.CambioMBase = 1;
//            DocStk_Saida.Utilizador = BSO.Contexto.UtilizadorActual;
//            DocStk_Saida.TipoEntidade = "C";
//            DocStk_Saida.Entidade = DocumentoVenda.Entidade;

//            DocStk_Saida.ArmazemOrigem = v_Armazem;
//            //'Linha nº 1 de texto
//            LinhaStk_Saida = new InvBELinhaOrigemTransf();
//            LinhaStk_Saida.TipoLinha = "60";
//            LinhaStk_Saida.Descricao = "Referente à " + BSO.Vendas.TabVendas.Edita(DocumentoVenda.Tipodoc).Descricao + " " + DocumentoVenda.Tipodoc + " " + DocumentoVenda.Serie + "/" + DocumentoVenda.NumDoc;
//            LinhaStk_Saida.Lote = "<L01>";
//            LinhaStk_Saida.LoteOrigem = "<L01>";
//            DocStk_Saida.Linhas.Insere(LinhaStk_Saida);

//            //'Linha nº 2 de texto
//            LinhaStk_Saida = new InvBELinhaOrigemTransf();
//            LinhaStk_Saida.TipoLinha = "60";
//            LinhaStk_Saida.Descricao = "";
//            LinhaStk_Saida.Lote = "<L01>";
//            LinhaStk_Saida.LoteOrigem = "<L01>";
//            DocStk_Saida.Linhas.Insere(LinhaStk_Saida);

//            //'********************************************************************************************************************************************************************************************************
//        }

//        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
//        {
//            base.TeclaPressionada(KeyCode, Shift, e);

//            {
//                if (Module1.VerificaToken("TarasDevolver") == 1)
//                {
//                    // *******************************************************************************************************************************************
//                    // #### TARAS A DEVOLVER ####
//                    // *******************************************************************************************************************************************
//                    // Alt+T
//                    if (KeyCode == 84 & (this.DocumentoVenda.Tipodoc == "GR" | this.DocumentoVenda.Tipodoc == "GT"))
//                    {
//                        Module1.ConesCartao = int.Parse(this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor.ToString());
//                        Module1.ConesPlastico = int.Parse(this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor.ToString());
//                        Module1.TubosCartao = int.Parse(this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor.ToString());
//                        Module1.TubosPlastico = int.Parse(this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor.ToString());
//                        Module1.PaletesMadeira = int.Parse(this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor.ToString());
//                        Module1.PaletesPlastico = int.Parse(this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor.ToString());
//                        Module1.SeparadoresCartao = int.Parse(this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor.ToString());

//                        // EduSamp 03/01/2017
//                        Module1.Devolver_ConesCartao = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverConesCartao"].Valor.ToString());
//                        Module1.Devolver_ConesPlastico = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverConesPlastico"].Valor.ToString());
//                        Module1.Devolver_TubosCartao = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverTubosCartao"].Valor.ToString());
//                        Module1.Devolver_TubosPlastico = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverTubosPlastico"].Valor.ToString());
//                        Module1.Devolver_PaletesMadeira = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesMadeira"].Valor.ToString());
//                        Module1.Devolver_PaletesPlastico = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesPlastico"].Valor.ToString());
//                        Module1.Devolver_SeparadoresCartao = bool.Parse(this.DocumentoVenda.CamposUtil["CDU_DevolverSeparadoresCartao"].Valor.ToString());

//                        ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmTarasDevolverView));
//                        FrmTarasDevolverView frm = result.Result;
//                        frm.ShowDialog();

//                        this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor = Module1.ConesCartao;
//                        this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor = Module1.ConesPlastico;
//                        this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor = Module1.TubosCartao;
//                        this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor = Module1.TubosPlastico;
//                        this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor = Module1.PaletesMadeira;
//                        this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor = Module1.PaletesPlastico;
//                        this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor = Module1.SeparadoresCartao;

//                        // EduSamp 03/01/2017
//                        this.DocumentoVenda.CamposUtil["CDU_DevolverConesCartao"].Valor = Module1.Devolver_ConesCartao;
//                        this.DocumentoVenda.CamposUtil["CDU_DevolverConesPlastico"].Valor = Module1.Devolver_ConesPlastico;
//                        this.DocumentoVenda.CamposUtil["CDU_DevolverTubosCartao"].Valor = Module1.Devolver_TubosCartao;
//                        this.DocumentoVenda.CamposUtil["CDU_DevolverTubosPlastico"].Valor = Module1.Devolver_TubosPlastico;
//                        this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesMadeira"].Valor = Module1.Devolver_PaletesMadeira;
//                        this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesPlastico"].Valor = Module1.Devolver_PaletesPlastico;
//                        this.DocumentoVenda.CamposUtil["CDU_DevolverSeparadoresCartao"].Valor = Module1.Devolver_SeparadoresCartao;
//                        for (int i = 1, loopTo = this.DocumentoVenda.Linhas.NumItens; i <= loopTo; i++)
//                        {
//                            if (i > this.DocumentoVenda.Linhas.NumItens)
//                            {
//                                break;
//                            }

//                            if (this.DocumentoVenda.Linhas.GetEdita(i).Descricao == "Taras a Devolver:" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 14) == "Cone(s) Cartão" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 16) == "Cone(s) Plástico" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 14) == "Tubo(s) Cartão" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 16) == "Tubo(s) Plástico" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 17) == "Palete(s) Madeira" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 18) == "Palete(s) Plástico" | Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, 20) == "Separador(es) Cartão")

//                            {
//                                this.DocumentoVenda.Linhas.Remove(i);
//                                i = i - 1;
//                            }
//                        }

//                        if (Module1.TotalTaras > 0 & Module1.TotalTaras_a_Devolver > 0)
//                        {
//                            for (int i = 0; i <= 7; i++)
//                            {
//                                var LinhaDoc = new VndBELinhaDocumentoVenda();
//                                LinhaDoc = new VndBELinhaDocumentoVenda();
//                                LinhaDoc.TipoLinha = "60";
//                                LinhaDoc.Lote = "<L01>";
//                                switch (i)
//                                {
//                                    case 0:
//                                        {
//                                            LinhaDoc.Descricao = "Taras a Devolver:";
//                                            break;
//                                        }

//                                    case 1:
//                                        {
//                                            if (this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverConesCartao"].Valor == false)
//                                            {
//                                                continue;
//                                            }
//                                            else
//                                            {
//                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_ConesCartao"].Valor + " Cone(s) Cartão";
//                                            }

//                                            break;
//                                        }

//                                    case 2:
//                                        {
//                                            if (this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverConesPlastico"].Valor == false)
//                                            {
//                                                continue;
//                                            }
//                                            else
//                                            {
//                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_ConesPlastico"].Valor + " Cone(s) Plástico";
//                                            }

//                                            break;
//                                        }

//                                    case 3:
//                                        {
//                                            if (this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverTubosCartao"].Valor == false)
//                                            {
//                                                continue;
//                                            }
//                                            else
//                                            {
//                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_TubosCartao"].Valor + " Tubo(s) Cartão";
//                                            }

//                                            break;
//                                        }

//                                    case 4:
//                                        {
//                                            if (this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverTubosPlastico"].Valor == false)
//                                            {
//                                                continue;
//                                            }
//                                            else
//                                            {
//                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_TubosPlastico"].Valor + " Tubo(s) Plástico";
//                                            }

//                                            break;
//                                        }

//                                    case 5:
//                                        {
//                                            if (this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesMadeira"].Valor == false)
//                                            {
//                                                continue;
//                                            }
//                                            else
//                                            {
//                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_PaletesMadeira"].Valor + " Palete(s) Madeira";
//                                            }

//                                            break;
//                                        }

//                                    case 6:
//                                        {
//                                            if (this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverPaletesPlastico"].Valor == false)
//                                            {
//                                                continue;
//                                            }
//                                            else
//                                            {
//                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_PaletesPlastico"].Valor + " Palete(s) Plástico";
//                                            }

//                                            break;
//                                        }

//                                    case 7:
//                                        {
//                                            if (this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor.ToString() == "0" | (bool)this.DocumentoVenda.CamposUtil["CDU_DevolverSeparadoresCartao"].Valor == false)
//                                            {
//                                                continue;
//                                            }
//                                            else
//                                            {
//                                                LinhaDoc.Descricao = this.DocumentoVenda.CamposUtil["CDU_SeparadoresCartao"].Valor + " Separador(es) Cartão";
//                                            }

//                                            break;
//                                        }
//                                }

//                                this.DocumentoVenda.Linhas.Insere(LinhaDoc);
//                            }
//                        }
//                    }
//                }
//            }

//        }
//    }
//}