using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.IO;
using System.Windows.Forms;
using BasBE100;
using static BasBE100.BasBETiposGcp;
using static StdPlatBS100.StdBSTipos;

namespace Filopa
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeImprimir(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeImprimir(ref Cancel, e);

            if (this.DocumentoVenda.Tipodoc == "NEC")
            {
                ImprimeNEC();
                Cancel = true;
            }

            if (this.DocumentoVenda.Tipodoc == "CNT")
            {
                ImprimeCNT();
                Cancel = true;
            }
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);
            // #################################################################################################################################
            // ####### Verifica se o artigo existe na TDU_ArtigosNoSplice, se existir aparece comentario na linha abaixo - BRUNO 27/11/2020   ##
            // #################################################################################################################################

            if (this.DocumentoVenda.Tipodoc == "NEC")
            {
                StdBELista sqlArtigoNoSplice;

                sqlArtigoNoSplice = BSO.Consulta("select CDU_Artigo from TDU_ArtigosNoSplice where CDU_Artigo='" + Artigo + "'");
                if (sqlArtigoNoSplice.Vazia() == false)
                    BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "TFO Knotted Yarn: No Splice should be used in the double yarn");
            }

            // #################################################################################################################################
            // ####### Verifica se o artigo existe na TDU_ArtigosNoSplice, se existir aparece comentario na linha abaixo - BRUNO 27/11/2020   ##
            // #################################################################################################################################

            // ########################################################################################################################################
            // ####### Verifica se o artigo existe na pesquisa pedida pelo Jorge, se existir aparece comentario na linha abaixo - BRUNO 09/04/2021   ##
            // ########################################################################################################################################
            if (this.DocumentoVenda.Tipodoc == "NEC")
            {
                StdBELista sqlArtigosDescExtraDY;

                sqlArtigosDescExtraDY = BSO.Consulta("select distinct a.Artigo, a.Descricao, a.CDU_DescricaoExtra from Artigo a inner join VMP_ART_TipoArtigo on VMP_ART_TipoArtigo.CodigoArtigo=a.Artigo inner join VMP_ART_Tipo on VMP_ART_Tipo.Id=VMP_ART_TipoArtigo.IdTipo inner join VMP_ART_NE ne on ne.Codigo=a.CDU_NE where a.ArtigoAnulado='0'and a.Familia='F01'and a.SubFamilia='01' and VMP_ART_Tipo.Codigo='006'and  (a.CDU_OrdenacaoDescricaoExtra not like '%C018%' and  a.CDU_OrdenacaoDescricaoExtra not like '%C011%' )and a.CDU_DescricaoTorcao2='L'and a.CDU_DescricaoComponentes='CO' and a.CDU_DescricaoComponentesPerc='100'and ne.Cabos='2'and ne.NE between '20' and '40'  and a.Artigo='" + Artigo + "'");
                if (sqlArtigosDescExtraDY.Vazia() == false)
                    BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "The Double Yarn TPI should be Half of the Single Yarn TPI");
            }

            // ########################################################################################################################################
            // ####### Verifica se o artigo existe na pesquisa pedida pelo Jorge, se existir aparece comentario na linha abaixo - BRUNO 09/04/2021   ##
            // ########################################################################################################################################

            // JFC Copiar Vendedor e Comissão do Cabeçalho do Documento para a Linha assim que o artigo é identificado 24/01/2019
            if (!Convert.IsDBNull(this.DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor) && this.DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor != null)
                this.DocumentoVenda.Linhas.GetEdita(NumLinha).Vendedor = this.DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor.ToString();
            if (!Convert.IsDBNull(this.DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor) && this.DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor != null)
                this.DocumentoVenda.Linhas.GetEdita(NumLinha).Comissao = Double.Parse(this.DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor.ToString());
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);
            if (this.DocumentoVenda.CamposUtil["CDU_Remarks"].Valor + "" == "")
                this.DocumentoVenda.CamposUtil["CDU_Remarks"].Valor = "- 'A' GRADE QUALITY WITH ALL DYEING GUARANTEES" + Strings.Chr(13) + "- CONES WITH YARN RESERVE (TAIL)" + Strings.Chr(13) + "- PACKING IN NEUTRAL PALLETS/CONES / LENGTH MEASURED CONES" + Strings.Chr(13) + "- COVERING LETTER MUST MENTION CLEARLY AGENT COMMISSION." + Strings.Chr(13) + "- SPECS SHOULD BE SENT FOR APPROVAL BEFORE SHIPMENT" + Strings.Chr(13) + "- NO HUMIDITY ADJUSTMENTS ARE ALLOWED" + Strings.Chr(13) + "- THE PRODUCT IN THIS ORDER CONFIRMATION MUST BE IN ACCORDANCE WITH THE RESPECTIVE OEKO TEX STANDARD 100 CERTIFICATES";
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            // ###################################################################################################################################################################################################
            // ###### Copiar o CDU_NFatura, CDU_NContentor, CDU_CNT_DataEmbarque do Embarque para o respetivo Contrato transformado. JFC - 24/01/2019 - Pedido de Pedro Lobrato para aparecer na lista Contratos##
            // ###################################################################################################################################################################################################
            if (this.DocumentoVenda.Tipodoc == "EMB")
                BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_NFatura=ln.CDU_NFatura, ln2.CDU_NContentor=ln.CDU_NContentor, ln2.CDU_CNT_DataEmbarque=cd.Data from CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem inner join CabecDoc cd2 on cd2.Id=ln2.IdCabecDoc where cd.TipoDoc='EMB' and cd2.TipoDoc='CNT' and cd.Id='" + this.DocumentoVenda.ID + "'");
        }

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            base.DepoisDeTransformar(e);

            // JFC Quando documento é Pre-Invoice, Condição de Pagamento deixa de ser a que estava no Embarque
            // e passa a ser a que está definida na ficha do Cliente.
            if (this.DocumentoVenda.Tipodoc == "PF")
            {
                // Me.DocumentoVenda.CondPag = BSO.Comercial.Clientes.Consulta(Me.DocumentoVenda.Entidade).CondPag
                if (BSO.Base.Clientes.DaValorAtributo(this.DocumentoVenda.Entidade, "CondPag") != "")
                {
                    this.DocumentoVenda.CondPag = BSO.Base.Clientes.DaValorAtributo(this.DocumentoVenda.Entidade, "CondPag");
                    int preenchedados = int.Parse(PreencheDados.enuDadosCondPag.ToString());
                    BSO.Vendas.Documentos.PreencheDadosRelacionados(this.DocumentoVenda, ref preenchedados);
                }
            }
        }

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            // #################################################################################################
            // # Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (BRUNO 15/10/2020) #
            // #################################################################################################
            // Crtl+R- AlteraCertificadoTransacao

            if (this.DocumentoVenda.Tipodoc == "EMB")
            {
                if (this.LinhaActual > 0)
                {
                    if (KeyCode == 82)
                    {
                        Module1.certArtigo = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Artigo;
                        Module1.certDocumento = this.DocumentoVenda.Tipodoc + " " + this.DocumentoVenda.NumDoc + "/" + this.DocumentoVenda.Serie;
                        // certLote = Me.DocumentoVenda.Linhas(Me.LinhaActual).lote
                        Module1.certIDlinha = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).IdLinha;
                        Module1.certEmitido = bool.Parse(this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_CertificadoRecebido"].Valor.ToString());
                        Module1.certDescricao = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Descricao;

                        // Acrescentado dia 27/01/2021 - Bruno
                        Module1.certCancelado = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_CertificadoCancelado"].Valor.ToString();

                        ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmAlteraCertificadoTransacaoFilopaView));

                        if (result.ResultCode == ExtensibilityResultCode.Ok)
                        {
                            FrmAlteraCertificadoTransacaoFilopaView.DocumentoVenda = DocumentoVenda;
                            FrmAlteraCertificadoTransacaoFilopaView.LinhaActual = LinhaActual;

                            FrmAlteraCertificadoTransacaoFilopaView frm = result.Result;
                            frm.ShowDialog();
                        }
                    }
                }
            }
        }

        public void ImprimeNEC()
        {
            string CaminhoFicheiro;
            string NomeFicheiro;

            CaminhoFicheiro = @"C:\NEC\";

            if (Directory.Exists(CaminhoFicheiro) == false)
                Directory.CreateDirectory(CaminhoFicheiro);

            NomeFicheiro = this.DocumentoVenda.Serie + "_" + this.DocumentoVenda.NumDoc + "_" + this.DocumentoVenda.Tipodoc + ".pdf";

            if (File.Exists(CaminhoFicheiro + @"\" + NomeFicheiro) == true)
                File.Delete(CaminhoFicheiro + @"\" + NomeFicheiro);

            try
            {
                PSO.Mapas.Inicializar("VND");
                PSO.Mapas.Destino = CRPEExportDestino.edFicheiro;
                PSO.Mapas.SetFileProp(CRPEExportFormat.efPdf, CaminhoFicheiro + NomeFicheiro);

                PSO.Mapas.AddFormula("Nome", "'" + BSO.Contexto.IDNome + "'");
                PSO.Mapas.AddFormula("Contribuinte", "'" + "Contribuinte N.º: " + BSO.Contexto.IFNIF + "'");

                if (BSO.Contexto.IDNumPorta + "" != "")
                    PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + ", " + BSO.Contexto.IDNumPorta + "'");
                else
                    PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + "'");

                PSO.Mapas.AddFormula("Localidade", "'" + BSO.Contexto.IDLocalidade + "'");
                PSO.Mapas.AddFormula("CodPostal", "'" + BSO.Contexto.IDCodPostal + " " + BSO.Contexto.IDCodPostalLocal + "'");
                PSO.Mapas.AddFormula("Telefone", "'" + "Telef. " + BSO.Contexto.IDIndicativoTelefone + "  " + BSO.Contexto.IDTelefone + "  Fax. " + BSO.Contexto.IDIndicativoFax + "  " + BSO.Contexto.IDFax + "'");

                PSO.Mapas.AddFormula("CapitalSocial", "'" + "Capital Social  " + Strings.Format(BSO.Contexto.ICCapitalSocial, "#,###.00") + " " + BSO.Contexto.ICMoedaCapSocial + "'");
                PSO.Mapas.AddFormula("Conservatoria", "'" + "Cons. Reg. Com. " + BSO.Contexto.ICConservatoria + "'");
                PSO.Mapas.AddFormula("Matricula", "'" + "Matricula N.º " + BSO.Contexto.ICMatricula + "'");
                PSO.Mapas.AddFormula("EMailEmpresa", "'" + BSO.Contexto.IDEmail + "'");
                PSO.Mapas.AddFormula("WebEmpresa", "'" + BSO.Contexto.IDWeb + "'");

                PSO.Mapas.AddFormula("NumVia", "'" + BSO.Base.Series.Edita("V", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie).DescricaoVia01 + "'");

                PSO.Mapas.AddFormula("lbl_Text23", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, "000") + "'");

                PSO.Mapas.AddFormula("NomeLicenca", "''");

                PSO.Mapas.AddFormula("Documento", "'" + BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.Tipodoc, "Descricao") + " " + this.DocumentoVenda.Tipodoc + " " + this.DocumentoVenda.Serie + "/" + Strings.Format(this.DocumentoVenda.NumDoc, "0") + "'");
                PSO.Mapas.SelectionFormula = "{CabecDoc.Filial} = '000' AND {CabecDoc.TipoDoc} = '" + DocumentoVenda.Tipodoc + "' AND {CabecDoc.Serie} = '" + DocumentoVenda.Serie + "' AND {CabecDoc.NumDoc} = " + DocumentoVenda.NumDoc + "";
                PSO.Mapas.ImprimeListagem("NEC", "Order Confirmation", "P", 1, bMapaSistema: false);
            }
            catch
            {
                MessageBox.Show("Erro ao imprimir o mapa seleccionado.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ImprimeCNT()
        {
            string CaminhoFicheiro;
            string NomeFicheiro;

            CaminhoFicheiro = @"C:\CNT\";

            if (Directory.Exists(CaminhoFicheiro) == false)
                Directory.CreateDirectory(CaminhoFicheiro);

            NomeFicheiro = this.DocumentoVenda.Serie + "_" + this.DocumentoVenda.NumDoc + "_" + this.DocumentoVenda.Tipodoc + ".pdf";

            if (File.Exists(CaminhoFicheiro + @"\" + NomeFicheiro) == true)
                File.Delete(CaminhoFicheiro + @"\" + NomeFicheiro);

            try
            {
                PSO.Mapas.Inicializar("VND");
                PSO.Mapas.Destino = CRPEExportDestino.edFicheiro;
                PSO.Mapas.SetFileProp(CRPEExportFormat.efPdf, CaminhoFicheiro + NomeFicheiro);

                PSO.Mapas.AddFormula("Nome", "'" + BSO.Contexto.IDNome + "'");
                PSO.Mapas.AddFormula("Contribuinte", "'" + "Contribuinte N.º: " + BSO.Contexto.IFNIF + "'");

                if (BSO.Contexto.IDNumPorta + "" != "")
                    PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + ", " + BSO.Contexto.IDNumPorta + "'");
                else
                    PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + "'");

                PSO.Mapas.AddFormula("Localidade", "'" + BSO.Contexto.IDLocalidade + "'");
                PSO.Mapas.AddFormula("CodPostal", "'" + BSO.Contexto.IDCodPostal + " " + BSO.Contexto.IDCodPostalLocal + "'");
                PSO.Mapas.AddFormula("Telefone", "'" + "Telef. " + BSO.Contexto.IDIndicativoTelefone + "  " + BSO.Contexto.IDTelefone + "  Fax. " + BSO.Contexto.IDIndicativoFax + "  " + BSO.Contexto.IDFax + "'");

                PSO.Mapas.AddFormula("CapitalSocial", "'" + "Capital Social  " + Strings.Format(BSO.Contexto.ICCapitalSocial, "#,###.00") + " " + BSO.Contexto.ICMoedaCapSocial + "'");
                PSO.Mapas.AddFormula("Conservatoria", "'" + "Cons. Reg. Com. " + BSO.Contexto.ICConservatoria + "'");
                PSO.Mapas.AddFormula("Matricula", "'" + "Matricula N.º " + BSO.Contexto.ICMatricula + "'");
                PSO.Mapas.AddFormula("EMailEmpresa", "'" + BSO.Contexto.IDEmail + "'");
                PSO.Mapas.AddFormula("WebEmpresa", "'" + BSO.Contexto.IDWeb + "'");

                PSO.Mapas.AddFormula("NumVia", "'" + BSO.Base.Series.Edita("V", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie).DescricaoVia01 + "'");

                PSO.Mapas.AddFormula("lbl_Text23", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, "000") + "'");

                PSO.Mapas.AddFormula("NomeLicenca", "''");

                PSO.Mapas.AddFormula("Documento", "'" + BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.Tipodoc, "Descricao") + " " + this.DocumentoVenda.Tipodoc + " " + this.DocumentoVenda.Serie + "/" + Strings.Format(this.DocumentoVenda.NumDoc, "0") + "'");
                PSO.Mapas.SelectionFormula = "{CabecDoc.Filial} = '000' AND {CabecDoc.TipoDoc} = '" + DocumentoVenda.Tipodoc + "' AND {CabecDoc.Serie} = '" + DocumentoVenda.Serie + "' AND {CabecDoc.NumDoc} = " + DocumentoVenda.NumDoc + "";
                PSO.Mapas.ImprimeListagem("NEC", "Order Confirmation", "P", 1, bMapaSistema: false);
            }
            catch
            {
                MessageBox.Show("Erro ao imprimir o mapa seleccionado.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            /*#################################################################
              ####### Verifica se CDU_Fornecedor é válido - JFC 24/01/2019   ##
              #################################################################*/

            if (DocumentoVenda.Tipodoc == "NEC" || DocumentoVenda.Tipodoc == "CNT" || DocumentoVenda.Tipodoc == "EMB")
            {
                if (BSO.Base.Fornecedores.Existe(DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()) == false)
                {
                    MessageBox.Show("O numero de Cliente não é válido: " + DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                }
            }
            /*#################################################################
              ####### Verifica se CDU_Fornecedor é válido - JFC 24/01/2019   ##
              #################################################################*/

            if (DocumentoVenda.Tipodoc == "CNT")
            {
                for (int j = 1; j <= DocumentoVenda.Linhas.NumItens; j++)
                {
                    if (DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_DataEntregaOriginal"].Valor + "" == "" && DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "")
                        DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_DataEntregaOriginal"].Valor = DocumentoVenda.Linhas.GetEdita(j).DataEntrega;
                }
            }

            if (DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor + "" != "")
                DocumentoVenda.LocalDescarga = BSO.Base.Fornecedores.DaNome(DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString());

            if (DocumentoVenda.Tipodoc == "NEC")
            {
                StdBELista TransValida;
                bool TransDiff;
                string TransString;
                bool TransMsg;

                TransMsg = false;
                TransString = "Atenção, valores diferentes da linha original!";

                for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
                {
                    TransDiff = false;

                    //if(DocumentoVenda.Linhas.GetEdita(i).IDLinhaOriginal!= "")
                    TransValida = BSO.Consulta("select concat(cd.TipoDoc, ' ', cd.NumDoc,'/', cd.Serie) as 'Doc',  ln.Artigo, ln.CDU_Comissao, ln.Comissao, ln.Vendedor from linhasdoc ln inner join cabecdoc cd on cd.id=ln.idcabecdoc where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(i).IDLinhaOriginal + "'");
                    //TransValida = BSO.Consulta("select concat(cd.TipoDoc, ' ', cd.NumDoc,'/', cd.Serie) as 'Doc',  ln.Artigo, ln.CDU_Comissao, ln.Comissao, ln.Vendedor from linhasdoc ln inner join cabecdoc cd on cd.id=ln.idcabecdoc where ln.Id='" + Guid.NewGuid() + "'");
                        
                    if (DocumentoVenda.Linhas.GetEdita(i).Armazem + "" != "" && DocumentoVenda.Linhas.GetEdita(i).IDLinhaOriginal + "" != "")
                    {
                        TransValida.Inicio();

                        if (DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Comissao"] != TransValida.Valor("CDU_Comissao"))
                            TransDiff = true;
                        if (DocumentoVenda.Linhas.GetEdita(i).Comissao != TransValida.Valor("Comissao"))
                            TransDiff = true;
                        if (DocumentoVenda.Linhas.GetEdita(i).Vendedor != TransValida.Valor("Vendedor"))
                            TransDiff = true;
                    }

                    if (TransDiff)
                    {
                        TransString = "" + TransString + "" + Strings.Chr(13) + Strings.Chr(13) + TransValida.Valor("Doc") + "    -->  " + " Linha: " + i + " - Artigo: " + DocumentoVenda.Linhas.GetEdita(i).Artigo + Strings.Chr(13) + "Comissao: " + TransValida.Valor("CDU_Comissao") + "% --> " + DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Comissao"] + "%" + Strings.Chr(13) + "Vendedor: " + TransValida.Valor("Vendedor") + " --> " + DocumentoVenda.Linhas.GetEdita(i).Vendedor + Strings.Chr(13) + "Com.Vend: " + TransValida.Valor("Comissao") + "% --> " + DocumentoVenda.Linhas.GetEdita(i).Comissao + "%";
                        TransMsg = true;
                    }
                }

                if (TransMsg)
                {
                    if (MessageBox.Show("" + TransString + Strings.Chr(13) + Strings.Chr(13) + "Deseja continuar com a gravação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        Cancel = true;
                }
            }

            /*   ' * ******************************************************************************************************************************************
            '    ''#### Bruno Peixoto 18/02/2020 ### Validar se em NEC/CNT/EMB tem produtos certificados e se o fornecedor é Certificado ####
            '    ' * *******************************************************************************************************************************************/

            if (DocumentoVenda.Tipodoc == "CNT" || DocumentoVenda.Tipodoc == "EMB" || DocumentoVenda.Tipodoc == "NEC")
            {
                int l;
                StdBELista listFornCert;

                listFornCert = BSO.Consulta("select *, getdate() as 'Hoje' from Clientes where Cliente='" + DocumentoVenda.Entidade + "'");
                listFornCert.Inicio();

                for (l = 1; l <= DocumentoVenda.Linhas.NumItens; l++)
                {
                    if (DocumentoVenda.Linhas.GetEdita(l).Artigo + "" != "")
                    {
                        if (Strings.UCase(BSO.Base.Artigos.Edita(DocumentoVenda.Linhas.GetEdita(l).Artigo).CamposUtil["CDU_DescricaoExtra"].Valor + BSO.Base.Artigos.Edita(DocumentoVenda.Linhas.GetEdita(l).Artigo).Descricao).Contains("GOTS"))
                        {

                            if (listFornCert.Valor("CDU_Gots") != true)
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor não está identificado como certificado Gots.",
                                   "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (listFornCert.Valor("CDU_GotsData") < listFornCert.Valor("Hoje"))
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor tem o certificado Gots expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        if (Strings.UCase(BSO.Base.Artigos.Edita(DocumentoVenda.Linhas.GetEdita(l).Artigo).CamposUtil["CDU_DescricaoExtra"].Valor + BSO.Base.Artigos.Edita(DocumentoVenda.Linhas.GetEdita(l).Artigo).Descricao).Contains("GRS"))
                        {

                            if (listFornCert.Valor("CDU_Grs") != true)
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor não está identificado como certificado GRS.",
                                   "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (listFornCert.Valor("CDU_GrsData") < listFornCert.Valor("Hoje"))
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor tem o certificado GRS expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        if (Strings.UCase(BSO.Base.Artigos.Edita(DocumentoVenda.Linhas.GetEdita(l).Artigo).CamposUtil["CDU_DescricaoExtra"].Valor + BSO.Base.Artigos.Edita(DocumentoVenda.Linhas.GetEdita(l).Artigo).Descricao).Contains("OCS"))
                        {

                            if (listFornCert.Valor("CDU_Ocs") != true)
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor não está identificado como certificado OCS.",
                                   "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (listFornCert.Valor("CDU_OcsData") < listFornCert.Valor("Hoje"))
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor tem o certificado OCS expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }




                        if (Strings.UCase(DocumentoVenda.Linhas.GetEdita(l).CamposUtil["CDU_ObsLote"].Valor.ToString()).Contains("BCI"))
                        {

                            if (listFornCert.Valor("CDU_Bci") != true)
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor não está identificado como certificado BCI.",
                                   "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (listFornCert.Valor("CDU_BciData") < listFornCert.Valor("Hoje"))
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor tem o certificado BCI expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }



                        if (Strings.UCase(DocumentoVenda.Linhas.GetEdita(l).Descricao).Contains("BCI"))
                        {

                            if (listFornCert.Valor("CDU_Bci") != true)
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor não está identificado como certificado BCI.",
                                   "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (listFornCert.Valor("CDU_BciData") < listFornCert.Valor("Hoje"))
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor tem o certificado BCI expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        if (Strings.UCase(BSO.Base.Artigos.Edita(DocumentoVenda.Linhas.GetEdita(l).Artigo).CamposUtil["CDU_DescricaoExtra"].Valor + BSO.Base.Artigos.Edita(DocumentoVenda.Linhas.GetEdita(l).Artigo).Descricao).Contains("FSC"))
                        {

                            if (listFornCert.Valor("CDU_Fsc") != true)
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor não está identificado como certificado FSC.",
                                   "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (listFornCert.Valor("CDU_FscData") < listFornCert.Valor("Hoje"))
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor tem o certificado FSC expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}