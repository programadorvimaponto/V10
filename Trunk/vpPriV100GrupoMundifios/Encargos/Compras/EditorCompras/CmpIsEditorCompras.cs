//using Generico;
//using Microsoft.VisualBasic;
//using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
//using Primavera.Extensibility.Purchases.Editors;
//using StdBE100;
//using System;
//using System.Windows.Forms;
//using IntBE100;

//namespace Encargos
//{
//    public class CmpIsEditorCompras : EditorCompras
//    {
//        private bool NaoGravar;

//        public override void AntesDeEditar(string Filial, string Tipo, string Serie, int NumDoc, ref bool Cancel, ExtensibilityEventArgs e)
//        {
//            base.AntesDeEditar(Filial, Tipo, Serie, NumDoc, ref Cancel, e);

//            if (Module1.VerificaToken("Encargos") == 1)
//                NaoGravar = false;
//        }

//        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
//        {
//            base.DepoisDeTransformar(e);

//            if (Module1.VerificaToken("Encargos") == 1)
//                NaoGravar = false;
//        }

//        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
//        {
//            base.AntesDeGravar(ref Cancel, e);

//            if (Module1.VerificaToken("Encargos") == 1)
//            {
//                if (NaoGravar)
//                    Cancel = true;

//                // ################################################################################################################################################################
//                // # Verificar se a Data de Introdução é igual à Data Movimento das Linhas   'JFC - 09/04/2021                                                                    #
//                // # (verificou-se situações onde os ENCG estavam lançados antes da entrade de stock, motivo foram algumas VF's com datas diferentes na Intodução vs LinhaEntrada #
//                // ################################################################################################################################################################

//                if (this.DocumentoCompra.Tipodoc == "VFA" | this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "VIT" | this.DocumentoCompra.Tipodoc == "VFE" | this.DocumentoCompra.Tipodoc == "WE" | this.DocumentoCompra.Tipodoc == "WEI" | this.DocumentoCompra.Tipodoc == "WEO")
//                {
//                    for (var i = 1; i <= this.DocumentoCompra.Linhas.NumItens; i++)
//                    {
//                        if (this.DocumentoCompra.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoCompra.Linhas.GetEdita(i).Lote + "" != "")
//                        {
//                            if ((DateTime)Conversion.Int(this.DocumentoCompra.Linhas.GetEdita(i).DataStock) != this.DocumentoCompra.DataIntroducao)
//                            {
//                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "Não foi possivel gravar o documento.A Linha " + i + " está com Data de Entrada(F10) diferente da Data de Introdução!" + Strings.Chr(13) + Strings.Chr(13) + "Linha " + i + ": " + (DateTime)Conversion.Int(this.DocumentoCompra.Linhas.GetEdita(i).DataStock) + Strings.Chr(13) + "DataIntrodução: " + this.DocumentoCompra.DataIntroducao + Strings.Chr(13) + Strings.Chr(13) + "Por favor corrija a data na Linha (usando a tecla F10) ou a Data de Introdução no cabeçalho do documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                                Cancel = true;
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        public override void AntesDeRemoverLinha(ref bool Cancel, ExtensibilityEventArgs e)
//        {
//            base.AntesDeRemoverLinha(ref Cancel, e);
//            if (Module1.VerificaToken("Encargos") == 1)
//            {
//                // JFC 13/12/2019 - Encargos Automáticos, devido à complexidade em identificar remoções efectivas, decidiu-se colocar apenas um alerta quando a linha é removida.
//                if (this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_DocEncargo"].Valor + "" != "")
//                {
//                    NaoGravar = true;
//                    MessageBox.Show("Atenção!" + Strings.Chr(13) + "Linha com Encargo associado, não vai ser possivel gravar o documento!" + Strings.Chr(13) + "Encargo: " + this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_DocEncargo"].Valor, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                }
//            }
//        }

//        public override void DepoisDeAnular(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
//        {
//            base.DepoisDeAnular(Filial, Tipo, Serie, NumDoc, e);

//            if (Module1.VerificaToken("Encargos") == 1)
//            {
//                // JFC 13/12/2019 - Encargos Automáticos: remove as linhas do Encargo gerado.
//                int j;

//                for (j = 1; j <= this.DocumentoCompra.Linhas.NumItens; j++)
//                {
//                    if (this.DocumentoCompra.Linhas.GetEdita(j).CamposUtil["CDU_DocEncargo"].Valor + "" != "")
//                    {
//                        // JFC 03/09/2019
//                        string TipoDocFinal;
//                        string SerieFinal;
//                        int NumDocFinal;

//                        int PosBarra;
//                        int PosEsp;

//                        // JFC 04/09/2019 - Identificar a posição da barra e do espaço (TipoDoc NumDoc/Serie)
//                        PosBarra = Strings.InStr(1, this.DocumentoCompra.Linhas.GetEdita(j).CamposUtil["CDU_DocEncargo"].Valor.ToString(), "/", Constants.vbTextCompare);
//                        PosEsp = Strings.InStr(1, this.DocumentoCompra.Linhas.GetEdita(j).CamposUtil["CDU_DocEncargo"].Valor.ToString(), " ", Constants.vbTextCompare);

//                        TipoDocFinal = Strings.Left(this.DocumentoCompra.Linhas.GetEdita(j).CamposUtil["CDU_DocEncargo"].Valor.ToString(), PosEsp - 1);
//                        NumDocFinal = Convert.ToInt32(Strings.Mid(this.DocumentoCompra.Linhas.GetEdita(j).CamposUtil["CDU_DocEncargo"].Valor.ToString(), PosEsp + 1, PosBarra - PosEsp - 1));
//                        SerieFinal = Strings.Mid(this.DocumentoCompra.Linhas.GetEdita(j).CamposUtil["CDU_DocEncargo"].Valor.ToString(), PosBarra + 1);

//                        StdBELista listDocEnc;

//                        listDocEnc = BSO.Consulta("select lk.NumLinha from LinhasSTK lk where lk.Modulo='S' and lk.TipoDoc='" + TipoDocFinal + "' and lk.NumDoc='" + NumDocFinal + "' and lk.Serie='" + SerieFinal + "' and lk.Artigo='" + this.DocumentoCompra.Linhas.GetEdita(j).Artigo + "' and lk.Lote='" + this.DocumentoCompra.Linhas.GetEdita(j).Lote + "'");
//                        listDocEnc.Inicio();

//                        if (listDocEnc.Vazia() == true)
//                            MessageBox.Show("Não foi possivel remover a linha do documento: " + this.DocumentoCompra.Linhas.GetEdita(j).CamposUtil["CDU_DocEncargo"].Valor, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        else
//                        {
//                            long i;
//                            for (i = 1; i <= listDocEnc.NumLinhas(); i++)
//                            {
//                                BSO.Comercial.Stocks.Remove("000", "S", TipoDocFinal, SerieFinal, NumDocFinal, listDocEnc.Valor("NumLinha"));
//                                listDocEnc.Seguinte();
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
//        {
//            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

//            if (Module1.VerificaToken("Encargos") == 1)
//            {
//                // ####################################################################################################################################
//                // # Encargos Automáticos 'JFC - 13/12/2019                                                                                           #
//                // ####################################################################################################################################

//                if (this.DocumentoCompra.Tipodoc == "VFA" | this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "VIT" | this.DocumentoCompra.Tipodoc == "VFE" | this.DocumentoCompra.Tipodoc == "WE" | this.DocumentoCompra.Tipodoc == "WEI" | this.DocumentoCompra.Tipodoc == "WEO")
//                {
//                    if (DateTime.Now.Year >= 2020)
//                    {
//                        StdBELista listEnc;
//                        // Todo o desenvolvimento dos Encargos Automáticos depende da query abaixo. Qualquer alteração na mesma obrigará a validar todo o desenvolvimento.
//                        // Armazem AEP não considera Direitos. Utilizado VFE para o efeito. Pedido de Mafalda. - JFC 23/04/2020
//                        if (this.DocumentoCompra.Tipodoc == "VFE")
//                            listEnc = BSO.Consulta("select lc.Id, lc.NumLinha, lc.Artigo,  lc.Lote, lc.Armazem, lce.CDU_Descricao, (-1*lc.Quantidade* lce.CDU_CustoValor) as 'Custo' from "
//    + "CabecCompras cc "
//    + "inner join LinhasCompras lc on lc.IdCabecCompras=cc.Id "
//    + "inner join LinhasComprasTrans lt on lt.IdLinhasCompras=lc.Id "
//    + "inner join linhascompras lc2 on lc2.Id=lt.IdLinhasComprasOrigem "
//    + "inner join CabecCompras cc2 on cc2.Id=lc2.IdCabecCompras "
//    + "inner join TDU_CabecCustosEncomendas cce on cce.CDU_TipoDoc=cc2.TipoDoc and cce.CDU_NumDoc=cc2.NumDoc and cce.CDU_Serie=cc2.Serie and cce.CDU_NumLinha=lc2.NumLinha "
//    + "inner join TDU_LinhasCustosEncomenda lce on lce.CDU_NumDoc=cc2.NumDoc and lce.CDU_Serie=cc2.Serie and lce.CDU_NumLinha=lc2.NumLinha "
//    + "where (lc.CDU_DocEncargo is null or lc.CDU_DocEncargo='') and cc.Id='" + this.DocumentoCompra.ID + "' "
//    + "and lce.CDU_Descricao in ('Custos Companhia Maritima') "
//    + "order by lc.NumLinha");
//                        else
//                            listEnc = BSO.Consulta("select lc.Id, lc.NumLinha, lc.Artigo,  lc.Lote, lc.Armazem, lce.CDU_Descricao, (-1*lc.Quantidade* lce.CDU_CustoValor) as 'Custo' from "
//                        + "CabecCompras cc "
//                    + "inner join LinhasCompras lc on lc.IdCabecCompras=cc.Id "
//                    + "inner join LinhasComprasTrans lt on lt.IdLinhasCompras=lc.Id "
//                    + "inner join linhascompras lc2 on lc2.Id=lt.IdLinhasComprasOrigem "
//                    + "inner join CabecCompras cc2 on cc2.Id=lc2.IdCabecCompras "
//                    + "inner join TDU_CabecCustosEncomendas cce on cce.CDU_TipoDoc=cc2.TipoDoc and cce.CDU_NumDoc=cc2.NumDoc and cce.CDU_Serie=cc2.Serie and cce.CDU_NumLinha=lc2.NumLinha "
//                    + "inner join TDU_LinhasCustosEncomenda lce on lce.CDU_NumDoc=cc2.NumDoc and lce.CDU_Serie=cc2.Serie and lce.CDU_NumLinha=lc2.NumLinha "
//                    + "where (lc.CDU_DocEncargo is null or lc.CDU_DocEncargo='') and cc.Id='" + this.DocumentoCompra.ID + "' "
//                    + "and lce.CDU_Descricao in ('Custos Companhia Maritima','Custos Alfandegarios') "
//                    + "order by lc.NumLinha");
//                        // Se a query devolver resultados, então cria o Encargo
//                        if (listEnc.Vazia() == false)
//                            CriarDocEncargo(this.DocumentoCompra.Serie, listEnc, DateAndTime.DateAdd("n", 1, (DateAndTime.DateAdd("n", DateAndTime.Minute(DateTime.Now), DateAndTime.DateAdd("h", DateAndTime.Hour(DateTime.Now), this.DocumentoCompra.DataIntroducao)))));
//                    }
//                }
//            }
//        }

//        private void CriarDocEncargo(string VFA_Serie, StdBELista VFA_Linhas, DateTime VFA_Data)
//        {
//            IntBEDocumentoInterno dockStocks = new IntBEDocumentoInterno();
//            string strDetalhe;

//            try
//            {
//                BSO.IniciaTransaccao();

//                dockStocks.ID = Guid.NewGuid().ToString();
//                dockStocks.Tipodoc = "ENCG";
//                dockStocks.Serie = VFA_Serie;

//                BSO.Internos.Documentos.PreencheDadosRelacionados(dockStocks);

//                dockStocks.Data = VFA_Data;

//                VFA_Linhas.Inicio();
//                for (int i = 1; i <= VFA_Linhas.NumLinhas(); i++)
//                {
//                    BSO.Internos.Documentos.AdicionaLinha(dockStocks, VFA_Linhas.Valor("Artigo"), , 0, VFA_Linhas.Valor("Armazem"), VFA_Linhas.Valor("Custo"), , VFA_Linhas.Valor("Lote"), VFA_Linhas.Valor("Armazem"));
//                    VFA_Linhas.Seguinte();
//                }

//                // ----------------------------------
//                // GRAVAÇÃO DO DOCUMENTO
//                 BSO.Internos.Documentos.Actualiza(dockStocks);
//                // GRAVAÇÃO DO DOCUMENTO
//                // ----------------------------------

//                // Termina a transação
//                BSO.TerminaTransaccao();
//                // ----------------------------------
//                // MENSAGEM FINAL

//                // Preencher as Descrições no Documento de Stock e preencher o CDU_DocEncargo no Documento de Compra
//                VFA_Linhas.Inicio();

//                for (int i = 1; i <= VFA_Linhas.NumLinhas(); i++)
//                {
//                    BSO.DSO.ExecuteSQL("update LinhasSTK set Descricao='" + VFA_Linhas.Valor("CDU_Descricao") + "' where NumLinha='" + i + "' and Modulo='S' and TipoDoc='" + dockStocks.Tipodoc + "' and NumDoc='" + dockStocks.NumDoc + "' and Serie='" + dockStocks.Serie + "'");
//                    BSO.DSO.ExecuteSQL("update LinhasCompras set CDU_DocEncargo='" + dockStocks.Tipodoc + " " + System.Convert.ToString(dockStocks.NumDoc) + "/" + dockStocks.Serie + "' where Id='" + VFA_Linhas.Valor("Id") + "'");
//                    VFA_Linhas.Seguinte();
//                }

//                strDetalhe = Constants.vbNullString;

//                strDetalhe = strDetalhe + "Documento de Stock: " + dockStocks.Tipodoc + " Nº " + System.Convert.ToString(dockStocks.NumDoc) + "/" + dockStocks.Serie + Constants.vbCrLf;

//                MessageBox.Show("Documento gerado com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                // MENSAGEM FINAL
//                // ----------------------------------

//                dockStocks = null;
//            }
//            catch
//            {
//                // Desfaz a transação
//                BSO.DesfazTransaccao();

//                dockStocks = null;

//                MessageBox.Show("Erro ao gerar o documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        //#####################################################################################################################################
//        // # Encargos Automáticos 'JFC - 13/12/2019                                                                                           #
//        // ####################################################################################################################################

//        public override void TipoDocumentoIdentificado(string Tipo, ref bool Cancel, ExtensibilityEventArgs e)
//        {
//            base.TipoDocumentoIdentificado(Tipo, ref Cancel, e);

//            if (Module1.VerificaToken("Encargos") == 1)
//                NaoGravar = false;
//        }
//    }
//}