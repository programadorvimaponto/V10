using System;
using Primavera.Extensibility.CustomForm;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StdBE100;
using Vimaponto.PrimaveraV100.Clientes.Filopa;
using Vimaponto.PrimaveraV100;
using VndBE100;
using Vimaponto.PrimaveraV100.Clientes.Filopa.EditorVendasDetalhe.DataSets;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;

namespace EditorVendasDetalhe
{
    public partial class FrmEditorVendasDetalheView : CustomForm
    {
        public FrmEditorVendasDetalheView()
        {
            InitializeComponent();
        }

        public void IniciaBs(DsEditorVendasDetalhe dsEditorVendasDetalhe)
        {
            this.dsEditorVendasDetalhe = dsEditorVendasDetalhe;

            BsCabecDoc.DataSource = dsEditorVendasDetalhe;
            BsCabecDoc.DataMember = "CabecDoc";

            BsLinhasDoc.DataSource = dsEditorVendasDetalhe;
            BsLinhasDoc.DataMember = "LinhasDoc";

            BsTotalLinhas.DataSource = dsEditorVendasDetalhe;
            BsTotalLinhas.DataMember = "TotalLinhas";

            barHeaderItemVersao.Caption = "Versão: " + typeof(string).Assembly.GetName().Version;
        }

        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                vmpGridViewLinhasDoc.RefreshData();

                dsEditorVendasDetalhe.AtualizaDocumentoVenda();

                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro na gravação do documento", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void FrmEditorVendasDetalheView_Load(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.PreencheCDUs();

            dsEditorVendasDetalhe.AtualizaCabecDoc_LinhasDoc();

            dsEditorVendasDetalhe.AtualizaTabelaTotalLinhas();


            FormatarGrelhasLinhasDoc();
            FormatarTotalLinhas();

            textEditIncoterms.TextChanged -= textEditIncoterms_TextChanged;
            spinEditComissaoAgente.ValueChanged -= spinEditComissaoAgente_ValueChanged;
            textEditAgente.TextChanged -= textEditAgente_TextChanged;
            textEditAgenteG.TextChanged -= textEditAgenteG_TextChanged;
            textEditCliente.TextChanged -= textEditCliente_TextChanged;
            textEditLocalidade.TextChanged -= textEditLocalidade_TextChanged;
            textEditPorto.TextChanged -= textEditPorto_TextChanged;
            textEditCompanhia.TextChanged -= textEditCompanhia_TextChanged;
            textEditDestino.TextChanged -= textEditDestino_TextChanged;
            textEditEstadoPagamento.TextChanged -= textEditEstadoPagamento_TextChanged;
            textEditCertificado1.TextChanged -= textEditCertificado1_TextChanged;
            textEditCertificado2.TextChanged -= textEditCertificado2_TextChanged;
            textEditBanco.TextChanged -= textEditBanco_TextChanged;
            textEditSituacao.TextChanged -= textEditSituacao_TextChanged;
            textEditTipoQualidade.TextChanged -= textEditTipoQualidade_TextChanged;
            textEditPaisOrigem.TextChanged -= textEditPaisOrigem_TextChanged;
            textEditParque.TextChanged -= textEditParque_TextChanged;
            textEditVendedor.TextChanged -= textEditVendedor_TextChanged;
            textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;
            spinEditComissaoVendedor.ValueChanged -= spinEditComissaoVendedor_ValueChanged;

            //'CabecDoc
            CarregaDadosCabec();

            //'LinhasDoc
            CarregaDadosLinhas();

            textEditIncoterms.TextChanged += textEditIncoterms_TextChanged;
            spinEditComissaoAgente.ValueChanged += spinEditComissaoAgente_ValueChanged;
            textEditAgente.TextChanged += textEditAgente_TextChanged;
            textEditAgenteG.TextChanged += textEditAgenteG_TextChanged;
            textEditCliente.TextChanged += textEditCliente_TextChanged;
            textEditLocalidade.TextChanged += textEditLocalidade_TextChanged;
            textEditPorto.TextChanged += textEditPorto_TextChanged;
            textEditCompanhia.TextChanged += textEditCompanhia_TextChanged;
            textEditDestino.TextChanged += textEditDestino_TextChanged;
            textEditEstadoPagamento.TextChanged += textEditEstadoPagamento_TextChanged;
            textEditCertificado1.TextChanged += textEditCertificado1_TextChanged;
            textEditCertificado2.TextChanged += textEditCertificado2_TextChanged;
            textEditBanco.TextChanged += textEditBanco_TextChanged;
            textEditSituacao.TextChanged += textEditSituacao_TextChanged;
            textEditTipoQualidade.TextChanged += textEditTipoQualidade_TextChanged;
            textEditPaisOrigem.TextChanged += textEditPaisOrigem_TextChanged;
            textEditParque.TextChanged += textEditParque_TextChanged;
            textEditVendedor.TextChanged += textEditVendedor_TextChanged;
            textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
            spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;

            //falta por o length para comparar
            if ((Strings.Len(PriV100Api.BSO.Vendas.Documentos.DaValorAtributo(dsEditorVendasDetalhe.DocumentoVenda.Filial, dsEditorVendasDetalhe.DocumentoVenda.Tipodoc, dsEditorVendasDetalhe.DocumentoVenda.Serie, dsEditorVendasDetalhe.DocumentoVenda.NumDoc, "CDU_DocumentoCompraDestino")) > 1) || (Strings.Len(PriV100Api.BSO.Vendas.Documentos.DaValorAtributo(dsEditorVendasDetalhe.DocumentoVenda.Filial, dsEditorVendasDetalhe.DocumentoVenda.Tipodoc, dsEditorVendasDetalhe.DocumentoVenda.Serie, dsEditorVendasDetalhe.DocumentoVenda.NumDoc, "CDU_DocumentoVendaDestino")) > 1))
            {
                textEditCliente.Enabled = false;
                buttonCliente.Enabled = false;
            }
            else
            {
                textEditCliente.Enabled = true;
                buttonCliente.Enabled = true;
            }
        }

        private void CarregaDadosCabec()
        {

            // geral
            textEditAgente.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Agente", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditAgenteDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_AgenteNome", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditCliente.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Fornecedor", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditClienteDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_FornecedorNome", true, DataSourceUpdateMode.OnPropertyChanged));

            spinEditComissaoAgente.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Comissao", true, DataSourceUpdateMode.OnPropertyChanged));

            textEditIncoterms.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Incoterms", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditIncotermsDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_IncotermsDesc", true, DataSourceUpdateMode.OnPropertyChanged));

            // contrato
            textEditLocalidade.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Localidade", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditLocalidadeDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "DescricaoLocalidade", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditNumero.DataBindings.Add(new Binding("Text", BsCabecDoc, "NumDoc", true, DataSourceUpdateMode.OnPropertyChanged));

            textEditCartaCredito.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_NCartaCredito", true, DataSourceUpdateMode.OnPropertyChanged));

            dateEditLimEmissao.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_DataLimiteEmissaoCC", true, DataSourceUpdateMode.OnPropertyChanged));

            textEditPorto.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Porto", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditPortoDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "DescricaoPorto", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditDestino.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Destino", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditDestinoDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "DescricaoDestino", true, DataSourceUpdateMode.OnPropertyChanged));

            // embarque
            textEditCompanhia.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_CompanhiaMaritima", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditCompanhiaDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_CompanhiaMaritimaNome", true, DataSourceUpdateMode.OnPropertyChanged));

            dateEditChegadaPrevista.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_DataPrevistaChegada", true, DataSourceUpdateMode.OnPropertyChanged));

            dateEditComunicacaoCliente.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_DataComunicacaoCliente", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditNBL.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_NBL", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditNavio.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Navio", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditBanco.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Banco", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditBancoDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_BancoNome", true, DataSourceUpdateMode.OnPropertyChanged));
            dateEditDataEmissao.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_DataEmissaoCC", true, DataSourceUpdateMode.OnPropertyChanged));

            textEditVendedor.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_Vendedor", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditVendedorDesc.DataBindings.Add(new Binding("Text", BsCabecDoc, "VendedorNome", true, DataSourceUpdateMode.OnPropertyChanged));
            spinEditComissaoVendedor.DataBindings.Add(new Binding("Text", BsCabecDoc, "CDU_ComissaoVendedor", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void CarregaDadosLinhas()
        {

            // geral
            textEditAgenteG.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_Agente", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditAgenteGDesc.DataBindings.Add(new Binding("Text", BsLinhasDoc, "AgenteNome", true, DataSourceUpdateMode.OnPropertyChanged));
            spinEditComissaoAgenteG.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_Comissao", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditVendedorG.DataBindings.Add(new Binding("Text", BsLinhasDoc, "Vendedor", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditVendedorGDesc.DataBindings.Add(new Binding("Text", BsLinhasDoc, "VendedorNome", true, DataSourceUpdateMode.OnPropertyChanged));
            spinEditComissaoVendedorG.DataBindings.Add(new Binding("Text", BsLinhasDoc, "ComissaoVendedor", true, DataSourceUpdateMode.OnPropertyChanged));
            memoEditObservacoes.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_Observacoes", true, DataSourceUpdateMode.OnPropertyChanged));

            // contrato
            textEditEstadoPagamento.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_EstadoPagamento", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditEstadoPagamentoDesc.DataBindings.Add(new Binding("Text", BsLinhasDoc, "EstadoPagamentoDesc", true, DataSourceUpdateMode.OnPropertyChanged));

            dateEditEmbarque.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_DataPrevistaEmbarque", true, DataSourceUpdateMode.OnPropertyChanged));
            memoEditEmbarque.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_DPEAlteradaMotivo", true, DataSourceUpdateMode.OnPropertyChanged));

            checkEditCliente.DataBindings.Add(new Binding("Checked", BsLinhasDoc, "CDU_ETRCliente", true, DataSourceUpdateMode.OnPropertyChanged));

            checkEditFornecedor.DataBindings.Add(new Binding("Checked", BsLinhasDoc, "CDU_ETRFornecedor", true, DataSourceUpdateMode.OnPropertyChanged));

            checkEditCertificado1.DataBindings.Add(new Binding("Checked", BsLinhasDoc, "CDU_Certificado1", true, DataSourceUpdateMode.OnPropertyChanged));

            textEditCertificado1.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_CertificadoTratado1", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditCertificado1Desc.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_CertificadoTratado1Desc", true, DataSourceUpdateMode.OnPropertyChanged));


            checkEditCertificado2.DataBindings.Add(new Binding("Checked", BsLinhasDoc, "CDU_Certificado2", true, DataSourceUpdateMode.OnPropertyChanged));

            textEditCertificado2.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_CertificadoTratado2", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditCertificado2Desc.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_CertificadoTratado2Desc", true, DataSourceUpdateMode.OnPropertyChanged));

            // embarque

            textEditNFatura.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_NFatura", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditLoteFornecedor.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_LoteFornecedor", true, DataSourceUpdateMode.OnPropertyChanged));
            spinEditPesoLiquido.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_PesoLiquido", true, DataSourceUpdateMode.OnPropertyChanged));
            spinEditPesoBruto.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_PesoBruto", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditNContentor.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_NContentor", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditNVolumes.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_NVolumes", true, DataSourceUpdateMode.OnPropertyChanged));
            spinEditFOB.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_ValorFOB", true, DataSourceUpdateMode.OnPropertyChanged));
            checkEditComissaoConsiderada.DataBindings.Add(new Binding("Checked", BsLinhasDoc, "CDU_ComissaoConsiderada", true, DataSourceUpdateMode.OnPropertyChanged));

            // Campos Auxiliares
            textEditSituacao.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_Situacao", true, DataSourceUpdateMode.OnPropertyChanged));
            checkEditParafinado.DataBindings.Add(new Binding("Checked", BsLinhasDoc, "CDU_Parafinado", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditTipoQualidade.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_TipoQualidade", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditPaisOrigem.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_PaisOrigem", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditSeguradora.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_Seguradora", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditNCertificado.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_NumCertificado", true, DataSourceUpdateMode.OnPropertyChanged));
            textEditParque.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_Parque", true, DataSourceUpdateMode.OnPropertyChanged));
            memoEditObservacaoDadosA.DataBindings.Add(new Binding("Text", BsLinhasDoc, "CDU_ObsMdf", true, DataSourceUpdateMode.OnPropertyChanged));
        }




        private void FormatarGrelhasLinhasDoc()
        {
            {
                // # Locked individual
                vmpGridViewLinhasDoc.IniciarFormatacao(true);

                vmpGridViewLinhasDoc.FormatarColuna("Artigo", true, Caption: "Artigo", LarguraFixa: 150);

                vmpGridViewLinhasDoc.FormatarColuna("Lote", true, Caption: "Lote", LarguraFixa: 70);

                vmpGridViewLinhasDoc.FormatarColuna("Descricao", true, Caption: "Descrição", LarguraFixa: 300);

                vmpGridViewLinhasDoc.FormatarColuna("Quantidade", true, Caption: "Quantidade", TipoFormato: DevExpress.Utils.FormatType.Numeric, FormatoTexto: "N2", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("Unidade", true, Caption: "Unidade", LarguraFixa: 70);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_Comissao", true, Caption: "Comissão", TipoFormato: DevExpress.Utils.FormatType.Numeric, FormatoTexto: "N2", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("PrecUnit", true, Caption: "PrecUnit", LarguraFixa: 100, TipoFormato: DevExpress.Utils.FormatType.Numeric, FormatoTexto: "N5");

                vmpGridViewLinhasDoc.FormatarColuna("Id", false, Caption: "Id", LarguraFixa: 60);

                vmpGridViewLinhasDoc.FormatarColuna("IdCabecDoc", false, Caption: "IdCabecDoc", LarguraFixa: 60);

                vmpGridViewLinhasDoc.FormatarColuna("NumLinha", false, Caption: "NumLinha", LarguraFixa: 60);

                vmpGridViewLinhasDoc.FormatarColuna("TipoLinha", false, Caption: "TipoLinha", LarguraFixa: 60);

                vmpGridViewLinhasDoc.FormatarColuna("Vendedor", false, Caption: "Vendedor", LarguraFixa: 80);

                vmpGridViewLinhasDoc.FormatarColuna("VendedorNome", false, Caption: "Vendedor Nome", LarguraFixa: 120);

                vmpGridViewLinhasDoc.FormatarColuna("ComissaoVendedor", false, Caption: "Comissão Vendedor", LarguraFixa: 140);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_CondPag", false, Caption: "Condição Pagamento", LarguraFixa: 140);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_CondPagDesc", false, Caption: "Condição Pagamento Desc", LarguraFixa: 160);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_Agente", false, Caption: "Agente", LarguraFixa: 80);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_DataPrevistaEmbarque", false, Caption: "Data Prevista Embarque", TipoFormato: DevExpress.Utils.FormatType.DateTime, LarguraFixa: 170);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_Incoterms", false, Caption: "Incoterms", LarguraFixa: 80);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_DPEAlteradaMotivo", false, Caption: "DPE Alterada Motivo", LarguraFixa: 140);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_ETRCliente", false, Caption: "ETR Cliente", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_ETRFornecedor", false, Caption: "ETR Fornecedor", LarguraFixa: 120);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_EstadoPagamento", false, Caption: "Estado Pagamento", LarguraFixa: 140);

                vmpGridViewLinhasDoc.FormatarColuna("EstadoPagamentoDesc", false, Caption: "EP Descrição", LarguraFixa: 120);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_Certificado1", false, Caption: "Certificado 1", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_CertificadoTratado1", false, Caption: "Certificado Tratado 1", LarguraFixa: 180);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_CertificadoTratado1Desc", false, Caption: "CT1 Descrição", LarguraFixa: 180);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_Certificado2", false, Caption: "Certificado 2", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_CertificadoTratado2", false, Caption: "Certificado Tratado 2", LarguraFixa: 180);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_CertificadoTratado2Desc", false, Caption: "CT2 Descrição", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_PesoLiquido", false, Caption: "Peso Líquido", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_PesoBruto", false, Caption: "Peso Bruto", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_NVolumes", false, Caption: "Nº Volumes", LarguraFixa: 80);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_NContentor", false, Caption: "Nº Contentor", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_NFatura", false, Caption: "Nº Fatura", LarguraFixa: 80);

                vmpGridViewLinhasDoc.FormatarColuna("DataEntrega", false, Caption: "Data Entrega", TipoFormato: DevExpress.Utils.FormatType.DateTime, LarguraFixa: 80);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_LoteFornecedor", false, Caption: "Lote Fornecedor", LarguraFixa: 100);

                vmpGridViewLinhasDoc.FormatarColuna("CDU_ValorFOB", false, Caption: "Valor FOB", LarguraFixa: 100);


                // Formatação
                // .Splits(0).DisplayColumns("Campo1").Style.VerticalAlignment = C1.Win.C1TrueDBGrid.AlignVertEnum.Center

                // #Formatação Quantidades
                // .Columns("QuantidadeTotal").NumberFormat = "N2"

                // # gerais
                vmpGridViewLinhasDoc.AutoFillColumn = vmpGridViewLinhasDoc.Columns["Descricao"];
                vmpGridViewLinhasDoc.OptionsView.ColumnAutoWidth = true;
                vmpGridViewLinhasDoc.FinalizarFormatacao();
            }
        }

        private void FormatarTotalLinhas()
        {
            {

                // # bloquear 
                // # Locked individual
                vmpGridViewTotalLinhas.IniciarFormatacao(true);

                vmpGridViewTotalLinhas.FormatarColuna("NFatura", Visivel: true, Caption: "Nº Fatura", LarguraFixa: 80);

                vmpGridViewTotalLinhas.FormatarColuna("ValorFOB", Editavel: true);

                vmpGridViewTotalLinhas.FormatarColuna("Total", Visivel: true, Caption: "Total", LarguraFixa: 140, TipoFormato: DevExpress.Utils.FormatType.Numeric, FormatoTexto: "N2");

                vmpGridViewTotalLinhas.FormatarColuna("TotalEmb", Editavel: true);

                vmpGridViewTotalLinhas.FormatarColuna("ValorFOB", Visivel: true, Editavel: true, Caption: "ValorFOB", LarguraFixa: 140, TipoFormato: DevExpress.Utils.FormatType.Numeric, FormatoTexto: "N2");

                vmpGridViewTotalLinhas.FormatarColuna("TotalEmb", Visivel: true, Caption: "Total Embarque", LarguraFixa: 140, TipoFormato: DevExpress.Utils.FormatType.Numeric, FormatoTexto: "N2");

                // Formatação
                // .Splits(0).DisplayColumns("Campo1").Style.VerticalAlignment = C1.Win.C1TrueDBGrid.AlignVertEnum.Center

                // #Formatação Quantidades
                // .Columns("QuantidadeTotal").NumberFormat = "N2"

                // # gerais
                vmpGridViewTotalLinhas.AutoFillColumn = vmpGridViewTotalLinhas.Columns["NFatura"];
                vmpGridViewTotalLinhas.OptionsView.ColumnAutoWidth = true;
                vmpGridViewTotalLinhas.FinalizarFormatacao();
            }
        }

        private void FormatarGrelhaLListas(ref FrmListaBsView Formulario)
        {
            {
                var GridListBs = Formulario.vmpGridViewListaBs;

                GridListBs.PopulateColumns();

                GridListBs.IniciarFormatacao(true);
                Formulario.Width = 750;
                GridListBs.FormatarColuna("Descricao", true, Caption: "Descrição");

                // # gerais

                GridListBs.AutoFillColumn = GridListBs.Columns["Descricao"];
                GridListBs.OptionsView.ColumnAutoWidth = true;
                GridListBs.FinalizarFormatacao();
            }
        }


        private void CopiaVendedorLinhas(object Fornecedor)
        {
            foreach (DsEditorVendasDetalhe.LinhasDocRow _reg in dsEditorVendasDetalhe.LinhasDoc)
            {
                var DTFornecedor = new DataTable();
                DTFornecedor = PriV100Api.BSO.DSO.ConsultaDataTable("Select TOP 1 ISNULL(CDU_Vendedor, '') AS CDU_Vendedor, ISNULL(CDU_Comissao,0) AS CDU_Comissao From Fornecedores Where Fornecedor= '" + Fornecedor + "' ");
                var DTVendedor = new DataTable();
                DTVendedor = PriV100Api.BSO.DSO.ConsultaDataTable("SELECT F.CDU_Vendedor, ISNULL(V.Nome, '') AS VendedorNome, ISNULL(V.Comissao,0) AS VendedorComissao, ISNULL(F.CDU_UsaComissaoVendedor, 1) AS CDU_UsaComissaoVendedor, F.CDU_Comissao " +
                             "FROM Fornecedores F " +
                             "LEFT JOIN Vendedores V ON V.Vendedor = F.CDU_Vendedor " +
                             "WHERE F.Fornecedor = '" + Fornecedor + "' ");

                if (DTFornecedor.Rows.Count <= 0)
                {
                    _reg.Vendedor = "";
                    _reg.VendedorNome = "";
                    _reg.ComissaoVendedor = 0;
                }
                else
                {
                    _reg.Vendedor = DTFornecedor.Rows[0]["CDU_Vendedor"].ToString();
                    _reg.VendedorNome = PriV100Api.BSO.DSO.DaValorUnico("Select TOP 1 Nome From Vendedores Where Vendedor = '" + _reg.Vendedor + "' ").ToString();
                    if (Convert.ToInt32(DTVendedor.Rows[0]["CDU_UsaComissaoVendedor"]) == 0)
                    {
                        _reg.ComissaoVendedor = Convert.ToDouble(DTFornecedor.Rows[0]["CDU_Comissao"]);
                    }
                    else
                    {
                        _reg.ComissaoVendedor = Convert.ToDouble(DTVendedor.Rows[0]["VendedorComissao"]);
                    }
                }
            }

            DataTable DTFornecedor2 = new DataTable();
            DTFornecedor2 = PriV100Api.BSO.DSO.ConsultaDataTable("Select TOP 1 ISNULL(CDU_Vendedor, '') AS CDU_Vendedor, ISNULL(CDU_Comissao,0) AS CDU_Comissao From Fornecedores Where Fornecedor= '" + Fornecedor + "' ");
            DataTable DTVendedor2 = new DataTable();
            DTVendedor2 = PriV100Api.BSO.DSO.ConsultaDataTable("SELECT F.CDU_Vendedor, ISNULL(V.Nome, '') AS VendedorNome, ISNULL(V.Comissao,0) AS VendedorComissao, ISNULL(F.CDU_UsaComissaoVendedor, 1) AS CDU_UsaComissaoVendedor, F.CDU_Comissao " +
                                     "FROM Fornecedores F " +
                                     "LEFT JOIN Vendedores V ON V.Vendedor = F.CDU_Vendedor " +
                                     "WHERE F.Fornecedor = '" + Fornecedor + "' ");


            if (DTFornecedor2.Rows.Count <= 0)
            {
                textEditVendedor.Text = "";
                textEditVendedorDesc.Text = "";
                spinEditComissaoVendedor.Value = 0;
            }
            else
            {
                textEditVendedor.Text = DTFornecedor2.Rows[0]["CDU_Vendedor"].ToString();
                textEditVendedorDesc.Text = PriV100Api.BSO.DSO.DaValorUnico("Select TOP 1 Nome From Vendedores Where Vendedor = '" + textEditVendedor.Text + "' ").ToString();
                if (Convert.ToInt32(DTVendedor2.Rows[0]["CDU_UsaComissaoVendedor"]) == 0)
                {
                    spinEditComissaoVendedor.Value = (decimal)DTFornecedor2.Rows[0]["CDU_Comissao"];
                }
                else
                {
                    spinEditComissaoVendedor.Value = Convert.ToDecimal(DTVendedor2.Rows[0]["VendedorComissao"]);
                }
            }
        }

        private void CopiaComissaoLinhas()
        {
            foreach (DsEditorVendasDetalhe.LinhasDocRow _reg in dsEditorVendasDetalhe.LinhasDoc)
                _reg.CDU_Comissao = float.Parse(spinEditComissaoAgente.Value.ToString());
        }

        private void CopiaVendedorCabecLinhas()
        {
            foreach (DsEditorVendasDetalhe.LinhasDocRow _reg in dsEditorVendasDetalhe.LinhasDoc)
            {
                _reg.Vendedor = textEditVendedor.Text;
                _reg.VendedorNome = textEditVendedorDesc.Text;
                _reg.ComissaoVendedor = Convert.ToDouble(spinEditComissaoVendedor.Value);
            }
        }

        private void CopiaComissaoVendedorLinhas()
        {
            foreach (DsEditorVendasDetalhe.LinhasDocRow _reg in dsEditorVendasDetalhe.LinhasDoc)
                _reg.ComissaoVendedor = Convert.ToDouble(spinEditComissaoVendedor.Value);
        }


        private void textEditCliente_TextChanged(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaFornecedores();
            DsEditorVendasDetalhe.FornecedoresRow reg = dsEditorVendasDetalhe.Fornecedores.FindByCodigo(textEditCliente.Text);
            if (reg != null)
            {
                textEditVendedor.TextChanged -= textEditVendedor_TextChanged;
                spinEditComissaoVendedor.ValueChanged -= spinEditComissaoVendedor_ValueChanged;
                textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;


                textEditCliente.Text = reg.Codigo;
                textEditClienteDesc.Text = reg.Descricao;
                CopiaVendedorLinhas(reg.Codigo);

                textEditVendedor.TextChanged += textEditVendedor_TextChanged;
                spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;
                textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
            }
            else
            {
                spinEditComissaoVendedor.ValueChanged -= spinEditComissaoVendedor_ValueChanged;
                textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;
                textEditVendedor.TextChanged -= textEditVendedor_TextChanged;

                textEditClienteDesc.Text = "";

                textEditVendedor.Text = "";
                textEditVendedorDesc.Text = "";
                spinEditComissaoAgenteG.Value = 0;

                // txtVendedor.Text = ""
                // txtVendedorDesc.Text = ""
                // txtComissao3.Value = 0

                spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;
                textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
                textEditVendedor.TextChanged += textEditVendedor_TextChanged;
            }
        }

        private void textEditAgente_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaAgentes();
            DsEditorVendasDetalhe.TDU_AgentesRow reg = dsEditorVendasDetalhe.TDU_Agentes.FindByCodigo(textEditAgente.Text);
            if (reg != null)
            {
                textEditAgenteG.TextChanged += textEditAgenteG_TextChanged;

                textEditAgente.Text = reg.Codigo;
                textEditAgenteDesc.Text = reg.Descricao;

                // CopiaAgenteLinhas()
                dsEditorVendasDetalhe.CopiaAgenteParaLinhas("CDU_Agente", "CDU_AgenteNome");

                textEditAgenteG.TextChanged -= textEditAgenteG_TextChanged;
            }
            else
                textEditAgenteDesc.Text = "";
        }

        private void textEditVendedor_TextChanged(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaVendedores();
            DsEditorVendasDetalhe.VendedoresRow reg = dsEditorVendasDetalhe.Vendedores.FindByCodigo(textEditVendedor.Text);
            if (reg != null)
            {
                textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;
                spinEditComissaoVendedor.ValueChanged -= spinEditComissaoVendedor_ValueChanged;

                textEditVendedor.Text = reg.Codigo;
                textEditVendedorDesc.Text = reg.Descricao;
                spinEditComissaoVendedor.Value = (decimal)reg.Comissao;
                CopiaVendedorCabecLinhas();

                spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;
                textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
            }
            else
            {
                textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;
                spinEditComissaoVendedor.ValueChanged -= spinEditComissaoVendedor_ValueChanged;

                textEditVendedorDesc.Text = "";
                spinEditComissaoVendedor.Value = 0;

                // txtVendedor.Text = ""
                // txtVendedorDesc.Text = ""
                // txtComissao3.Value = 0

                spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;
                textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
            }


        }


        private void textEditLocalidade_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaLocais();
            DsEditorVendasDetalhe.TDU_LocaisRow reg = dsEditorVendasDetalhe.TDU_Locais.FindByCodigo(textEditLocalidade.Text);
            if (reg != null)
            {
                textEditLocalidade.Text = reg.Codigo;
                textEditLocalidadeDesc.Text = reg.Descricao;
            }
            else
                textEditLocalidadeDesc.Text = "";
        }

        private void textEditBanco_TextChanged(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaBancos();
            DsEditorVendasDetalhe.TDU_BancosRow reg = dsEditorVendasDetalhe.TDU_Bancos.FindByCodigo(textEditBanco.Text);
            if (reg != null)
            {
                textEditBanco.Text = reg.Codigo;
                textEditBancoDesc.Text = reg.Descricao;
            }
            else
                textEditBancoDesc.Text = "";
        }

        private void textEditPorto_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaLocais();
            DsEditorVendasDetalhe.TDU_LocaisRow reg = dsEditorVendasDetalhe.TDU_Locais.FindByCodigo(textEditPorto.Text);
            if (reg != null)
            {
                textEditPorto.Text = reg.Codigo;
                textEditPortoDesc.Text = reg.Descricao;
            }
            else
                textEditPortoDesc.Text = "";
        }

        private void textEditDestino_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaLocais();
            DsEditorVendasDetalhe.TDU_LocaisRow reg = dsEditorVendasDetalhe.TDU_Locais.FindByCodigo(textEditDestino.Text);
            if (reg != null)
            {
                textEditDestino.Text = reg.Codigo;
                textEditDestinoDesc.Text = reg.Descricao;
            }
            else
                textEditDestinoDesc.Text = "";
        }

        private void textEditEstadoPagamento_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaEstadoPagamento();
            DsEditorVendasDetalhe.TDU_EstadoPagamentoRow reg = dsEditorVendasDetalhe.TDU_EstadoPagamento.FindByCodigo(textEditEstadoPagamento.Text);
            if (reg != null)
            {
                textEditEstadoPagamento.Text = reg.Codigo;
                textEditEstadoPagamentoDesc.Text = reg.Descricao;
            }
            else
                textEditEstadoPagamentoDesc.Text = "";
        }

        private void textEditAgenteG_TextChanged(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaAgentes();
            DsEditorVendasDetalhe.TDU_AgentesRow reg = dsEditorVendasDetalhe.TDU_Agentes.FindByCodigo(textEditAgenteG.Text);
            if (reg != null)
            {
                textEditAgenteG.Text = reg.Codigo;
                textEditAgenteGDesc.Text = reg.Descricao;
            }
            else
                textEditAgenteGDesc.Text = "";

        }

        private void textEditCompanhia_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaCompanhiasMaritimas();
            DsEditorVendasDetalhe.TDU_CompanhiasMaritimasRow reg = dsEditorVendasDetalhe.TDU_CompanhiasMaritimas.FindByCodigo(textEditCompanhia.Text);
            if (reg != null)
            {
                textEditCompanhia.Text = reg.Codigo;
                textEditCompanhiaDesc.Text = reg.Descricao;
            }
            else
                textEditCompanhiaDesc.Text = "";
        }

        private void textEditCertificado1_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaCertificados();
            DsEditorVendasDetalhe.TDU_CertificadosRow reg = dsEditorVendasDetalhe.TDU_Certificados.FindByCodigo(textEditCertificado1.Text);
            if (reg != null)
            {
                textEditCertificado1.Text = reg.Codigo;
                textEditCertificado1Desc.Text = reg.Descricao;
            }
            else
                textEditCertificado1Desc.Text = "";
        }

        private void textEditCertificado2_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaCertificados();
            DsEditorVendasDetalhe.TDU_CertificadosRow reg = dsEditorVendasDetalhe.TDU_Certificados.FindByCodigo(textEditCertificado2.Text);
            if (reg != null)
            {
                textEditCertificado2.Text = reg.Codigo;
                textEditCertificado2Desc.Text = reg.Descricao;
            }
            else
                textEditCertificado2Desc.Text = "";
        }

        private void textEditSituacao_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaSituacao();
            DsEditorVendasDetalhe.TDU_SituacoesLoteRow reg = dsEditorVendasDetalhe.TDU_SituacoesLote.FindByCodigo(textEditSituacao.Text);
            if (reg != null)
            {
                textEditSituacao.Text = reg.Codigo;
                textEditSituacaoDesc.Text = reg.Descricao;
            }
            else
                textEditSituacaoDesc.Text = "";
        }

        private void textEditTipoQualidade_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaTipoQualidade();
            DsEditorVendasDetalhe.TDU_TipoQualidadeRow reg = dsEditorVendasDetalhe.TDU_TipoQualidade.FindByCodigo(textEditTipoQualidade.Text);
            if (reg != null)
            {
                textEditTipoQualidade.Text = reg.Codigo;
                textEditTipoQualidadeDesc.Text = reg.Descricao;
            }
            else
                textEditTipoQualidadeDesc.Text = "";
        }

        private void textEditPaisOrigem_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaPais();
            DsEditorVendasDetalhe.PaisesRow reg = dsEditorVendasDetalhe.Paises.FindByCodigo(textEditPaisOrigem.Text);
            if (reg != null)
            {
                textEditPaisOrigem.Text = reg.Codigo;
                textEditPaisOrigemDesc.Text = reg.Descricao;
            }
            else
                textEditPaisOrigemDesc.Text = "";
        }

        private void textEditParque_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaParques();
            DsEditorVendasDetalhe.TDU_ParquesRow reg = dsEditorVendasDetalhe.TDU_Parques.FindByCodigo(textEditParque.Text);
            if (reg != null)
            {
                textEditParque.Text = reg.Codigo;
                textEditParqueDesc.Text = reg.Descricao;
            }
            else
                textEditParqueDesc.Text = "";
        }

        private void buttonAgente_Click(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaAgentes();

            BindingSource BsAgentes = new BindingSource();
            BsAgentes.DataSource = dsEditorVendasDetalhe;
            BsAgentes.DataMember = "TDU_Agentes";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Agentes", BsAgentes);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_AgentesRow _r;
                _r = (DsEditorVendasDetalhe.TDU_AgentesRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditAgenteG.TextChanged -= textEditAgenteG_TextChanged;
                textEditAgente.TextChanged -= textEditAgente_TextChanged;

                textEditAgente.Text = _r.Codigo;
                textEditAgenteDesc.Text = _r.Descricao;
                // CopiaAgenteLinhas()
                dsEditorVendasDetalhe.CopiaAgenteParaLinhas("CDU_Agente", "CDU_AgenteNome");

                textEditAgente.TextChanged += textEditAgente_TextChanged;
                textEditAgenteG.TextChanged += textEditAgenteG_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }


        private void buttonCliente_Click(object sender, EventArgs e)
        {
            // NOTA : Este botão chama-se cliente mas lista fornecedores
            // GMC - 2019.06.13 
            // Se documento venda = "EMB" + gerado por transformacao de documentos + de um cliente do grupo Mundifios, não deixa alterar o fornecedor

            if (Strings.Trim(dsEditorVendasDetalhe.DocumentoVenda.Tipodoc.ToString()) == "EMB" & (Verifica_Se_Fornecedor_Seleccionado_E_Empresa_Grupo_Mundifios(dsEditorVendasDetalhe.DocumentoVenda.Entidade) == true & Verifica_Se_Documento_Venda_Atual_Foi_Gerado_Por_Transformacao_Documentos(dsEditorVendasDetalhe.DocumentoVenda.Tipodoc, dsEditorVendasDetalhe.DocumentoVenda.Serie, dsEditorVendasDetalhe.DocumentoVenda.NumDoc.ToString()) == true))
                MessageBox.Show("Não é possível alterar o cliente");
            dsEditorVendasDetalhe.CarregaFornecedores();

            BindingSource BsFornecedores = new BindingSource();
            BsFornecedores.DataSource = dsEditorVendasDetalhe;
            BsFornecedores.DataMember = "Fornecedores";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Fornecedores", BsFornecedores);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.FornecedoresRow _r;
                _r = (DsEditorVendasDetalhe.FornecedoresRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditCliente.TextChanged -= textEditCliente_TextChanged;
                textEditVendedor.TextChanged -= textEditVendedor_TextChanged;
                spinEditComissaoVendedor.ValueChanged -= spinEditComissaoAgente_ValueChanged;
                textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;

                textEditCliente.Text = _r.Codigo;
                textEditClienteDesc.Text = _r.Descricao;

                CopiaVendedorLinhas(_r.Codigo);

                textEditCliente.TextChanged += textEditCliente_TextChanged;
                textEditVendedor.TextChanged += textEditVendedor_TextChanged;
                spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;
                textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }
        private bool Verifica_Se_Fornecedor_Seleccionado_E_Empresa_Grupo_Mundifios(string Codigo)
        {
            // ----------------------------------------------------------------
            // --- 2019.06.13 - VIMAPONTO - Gualter Costa  - Redmine # 1558 ---
            // ----------------------------------------------------------------
            // NOTA : Todos os fornecedores que são empresas do Grupo Mundifios têm o campo CDU_NomeEmpresaGrupo preenchido (esse campo indica ainda qual a base de dados dessa empresa) 


            //Verifica_Se_Fornecedor_Seleccionado_E_Empresa_Grupo_Mundifios = false; // Inicializa a false

            if (Strings.Trim(BSO.Base.Fornecedores.DaValorAtributo(dsEditorVendasDetalhe.DocumentoVenda.Entidade, "CDU_NomeEmpresaGrupo")) == "")
                return false;
            else
                return true;
        }

        private void spinEditComissaoVendedor_ValueChanged(object sender, EventArgs e)
        {
            CopiaComissaoVendedorLinhas();
        }

        private void spinEditComissaoAgente_ValueChanged(object sender, EventArgs e)
        {
            CopiaComissaoLinhas();
        }

        private void buttonIncoterms_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaIntrastatCondEntrega();

            BindingSource BsIntrastatCondEntrega = new BindingSource();
            BsIntrastatCondEntrega.DataSource = dsEditorVendasDetalhe;
            BsIntrastatCondEntrega.DataMember = "IntrastatCondEntrega";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("IntrastatCondEntrega", BsIntrastatCondEntrega);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.IntrastatCondEntregaRow _r;
                _r = (DsEditorVendasDetalhe.IntrastatCondEntregaRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditIncoterms.TextChanged -= textEditIncoterms_TextChanged;

                textEditIncoterms.Text = _r.Codigo;
                textEditIncotermsDesc.Text = _r.Descricao;

                dsEditorVendasDetalhe.CopiaCampoParaLinhas("CDU_Incoterms");

                textEditIncoterms.TextChanged += textEditIncoterms_TextChanged;


                frm.Dispose();
                frm = null;
            }
        }

        private void textEditIncoterms_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaIntrastatCondEntrega();
            DsEditorVendasDetalhe.IntrastatCondEntregaRow reg = dsEditorVendasDetalhe.IntrastatCondEntrega.FindByCodigo(textEditIncoterms.Text);
            if (reg != null)
            {
                textEditIncoterms.Text = reg.Codigo;
                textEditIncotermsDesc.Text = reg.Descricao;
                dsEditorVendasDetalhe.CopiaCampoParaLinhas("CDU_Incoterms");
            }
            else
                textEditIncotermsDesc.Text = "";
        }

        private void buttonLocalidade_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaLocais();

            BindingSource BsRegioes = new BindingSource();
            BsRegioes.DataSource = dsEditorVendasDetalhe;
            BsRegioes.DataMember = "TDU_Locais";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("TDU_Locais", BsRegioes);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_LocaisRow _r;
                _r = (DsEditorVendasDetalhe.TDU_LocaisRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditLocalidade.TextChanged -= textEditLocalidade_TextChanged;

                textEditLocalidade.Text = _r.Codigo;
                textEditLocalidadeDesc.Text = _r.Descricao;

                textEditLocalidade.TextChanged += textEditLocalidade_TextChanged;
            }
            frm.Dispose();
            frm = null;


        }

        private void buttonPorto_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaLocais();

            BindingSource BsPortos = new BindingSource();
            BsPortos.DataSource = dsEditorVendasDetalhe;
            BsPortos.DataMember = "TDU_Locais";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("TDU_Locais", BsPortos);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_LocaisRow _r;
                _r = (DsEditorVendasDetalhe.TDU_LocaisRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditPorto.TextChanged -= textEditPorto_TextChanged;

                textEditPorto.Text = _r.Codigo;
                textEditPortoDesc.Text = _r.Descricao;

                textEditPorto.TextChanged += textEditPorto_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }

        private void buttonDestino_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaLocais();

            BindingSource BsDestinos = new BindingSource();
            BsDestinos.DataSource = dsEditorVendasDetalhe;
            BsDestinos.DataMember = "TDU_Locais";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("TDU_Locais", BsDestinos);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_LocaisRow _r;
                _r = (DsEditorVendasDetalhe.TDU_LocaisRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditDestino.TextChanged -= textEditDestino_TextChanged;

                textEditDestino.Text = _r.Codigo;
                textEditDestinoDesc.Text = _r.Descricao;

                textEditDestino.TextChanged += textEditDestino_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }

        private void buttonCompanhia_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaCompanhiasMaritimas();

            BindingSource BsCompanhiasMaritimas = new BindingSource();
            BsCompanhiasMaritimas.DataSource = dsEditorVendasDetalhe;
            BsCompanhiasMaritimas.DataMember = "TDU_CompanhiasMaritimas";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("CompanhiasMaritimas", BsCompanhiasMaritimas);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_CompanhiasMaritimasRow _r;
                _r = (DsEditorVendasDetalhe.TDU_CompanhiasMaritimasRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditCompanhia.TextChanged -= textEditCompanhia_TextChanged;

                textEditCompanhia.Text = _r.Codigo;
                textEditCompanhiaDesc.Text = _r.Descricao;

                textEditCompanhia.TextChanged += textEditCompanhia_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }

        private void buttonEstadoPagamento_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaEstadoPagamento();

            BindingSource BsEstadoPagamento = new BindingSource();
            BsEstadoPagamento.DataSource = dsEditorVendasDetalhe;
            BsEstadoPagamento.DataMember = "TDU_EstadoPagamento";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("EstadoPagamento", BsEstadoPagamento);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_EstadoPagamentoRow _r;
                _r = (DsEditorVendasDetalhe.TDU_EstadoPagamentoRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditEstadoPagamento.TextChanged -= textEditEstadoPagamento_TextChanged;

                textEditEstadoPagamento.Text = _r.Codigo;
                textEditEstadoPagamentoDesc.Text = _r.Descricao;

                textEditEstadoPagamento.TextChanged += textEditEstadoPagamento_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }

        private void buttonCertificado1_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaCertificados();

            BindingSource BsCertificados = new BindingSource();
            BsCertificados.DataSource = dsEditorVendasDetalhe;
            BsCertificados.DataMember = "TDU_Certificados";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Certificados", BsCertificados);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_CertificadosRow _r;
                _r = (DsEditorVendasDetalhe.TDU_CertificadosRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditCertificado1.TextChanged -= textEditCertificado1_TextChanged;

                textEditCertificado1.Text = _r.Codigo;
                textEditCertificado1Desc.Text = _r.Descricao;

                textEditCertificado1.TextChanged += textEditCertificado1_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }

        private void buttonCertificado2_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaCertificados();

            BindingSource BsCertificados = new BindingSource();
            BsCertificados.DataSource = dsEditorVendasDetalhe;
            BsCertificados.DataMember = "TDU_Certificados";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Certificados", BsCertificados);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_CertificadosRow _r;
                _r = (DsEditorVendasDetalhe.TDU_CertificadosRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditCertificado2.TextChanged -= textEditCertificado2_TextChanged;

                textEditCertificado2.Text = _r.Codigo;
                textEditCertificado2Desc.Text = _r.Descricao;

                textEditCertificado2.TextChanged += textEditCertificado2_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }

        private void textEditCliente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaFornecedores();

                BindingSource BsFornecedores = new BindingSource();
                BsFornecedores.DataSource = dsEditorVendasDetalhe;
                BsFornecedores.DataMember = "Fornecedores";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Fornecedores", BsFornecedores);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.FornecedoresRow _r;
                    _r = (DsEditorVendasDetalhe.FornecedoresRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);
                    textEditCliente.TextChanged -= textEditCliente_TextChanged;
                    textEditVendedor.TextChanged -= textEditVendedor_TextChanged;
                    spinEditComissaoVendedor.ValueChanged -= spinEditComissaoVendedor_ValueChanged;
                    textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;


                    textEditCliente.Text = _r.Codigo;
                    textEditClienteDesc.Text = _r.Descricao;
                    CopiaVendedorLinhas(_r.Codigo);

                    textEditCliente.TextChanged += textEditCliente_TextChanged;
                    textEditVendedor.TextChanged += textEditVendedor_TextChanged;
                    spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;
                    textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }
        }

        private void textEditAgente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaAgentes();

                BindingSource BsAgentes = new BindingSource();
                BsAgentes.DataSource = dsEditorVendasDetalhe;
                BsAgentes.DataMember = "TDU_Agentes";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Agentes", BsAgentes);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_AgentesRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_AgentesRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditAgente.TextChanged -= textEditAgente_TextChanged;
                    textEditAgenteG.TextChanged -= textEditAgenteG_TextChanged;

                    textEditAgente.Text = _r.Codigo;
                    textEditAgenteDesc.Text = _r.Descricao;
                    // CopiaAgenteLinhas()
                    dsEditorVendasDetalhe.CopiaAgenteParaLinhas("CDU_Agente", "CDU_AgenteNome");

                    textEditAgente.TextChanged += textEditAgente_TextChanged;
                    textEditAgenteG.TextChanged += textEditAgenteG_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }


        }

        private void textEditIncoterms_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaIntrastatCondEntrega();

                BindingSource BsIntrastatCondEntrega = new BindingSource();
                BsIntrastatCondEntrega.DataSource = dsEditorVendasDetalhe;
                BsIntrastatCondEntrega.DataMember = "IntrastatCondEntrega";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("IntrastatCondEntrega", BsIntrastatCondEntrega);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.IntrastatCondEntregaRow _r;
                    _r = (DsEditorVendasDetalhe.IntrastatCondEntregaRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditIncoterms.TextChanged -= textEditIncoterms_TextChanged;

                    textEditIncoterms.Text = _r.Codigo;
                    textEditIncotermsDesc.Text = _r.Descricao;

                    dsEditorVendasDetalhe.CopiaCampoParaLinhas("CDU_Incoterms");

                    textEditIncoterms.TextChanged += textEditIncoterms_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }
        }

        private void textEditLocalidade_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaLocais();

                BindingSource BsRegioes = new BindingSource();
                BsRegioes.DataSource = dsEditorVendasDetalhe;
                BsRegioes.DataMember = "TDU_Locais";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("TDU_Locais", BsRegioes);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_LocaisRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_LocaisRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditLocalidade.TextChanged -= textEditLocalidade_TextChanged;

                    textEditLocalidade.Text = _r.Codigo;
                    textEditLocalidadeDesc.Text = _r.Descricao;

                    textEditLocalidade.TextChanged += textEditLocalidade_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }


        }


        private void textEditPorto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaLocais();

                BindingSource BsPortos = new BindingSource();
                BsPortos.DataSource = dsEditorVendasDetalhe;
                BsPortos.DataMember = "TDU_Locais";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("TDU_Locais", BsPortos);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_LocaisRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_LocaisRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditPorto.TextChanged -= textEditPorto_TextChanged;

                    textEditPorto.Text = _r.Codigo;
                    textEditPortoDesc.Text = _r.Descricao;

                    textEditPorto.TextChanged += textEditPorto_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }
        }

        private void textEditDestino_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaLocais();

                BindingSource BsDestinos = new BindingSource();
                BsDestinos.DataSource = dsEditorVendasDetalhe;
                BsDestinos.DataMember = "TDU_Locais";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("TDU_Locais", BsDestinos);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_LocaisRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_LocaisRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditDestino.TextChanged -= textEditDestino_TextChanged;

                    textEditDestino.Text = _r.Codigo;
                    textEditDestinoDesc.Text = _r.Descricao;

                    textEditDestino.TextChanged += textEditDestino_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }
        }

        private void textEditCompanhia_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaCompanhiasMaritimas();

                BindingSource BsCompanhiasMaritimas = new BindingSource();
                BsCompanhiasMaritimas.DataSource = dsEditorVendasDetalhe;
                BsCompanhiasMaritimas.DataMember = "TDU_CompanhiasMaritimas";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("CompanhiasMaritimas", BsCompanhiasMaritimas);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_CompanhiasMaritimasRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_CompanhiasMaritimasRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditCompanhia.TextChanged -= textEditCompanhia_TextChanged;

                    textEditCompanhia.Text = _r.Codigo;
                    textEditCompanhiaDesc.Text = _r.Descricao;

                    textEditCompanhia.TextChanged += textEditCompanhia_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }
        }

        private void textEditEstadoPagamento_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaEstadoPagamento();

                BindingSource BsEstadoPagamento = new BindingSource();
                BsEstadoPagamento.DataSource = dsEditorVendasDetalhe;
                BsEstadoPagamento.DataMember = "TDU_EstadoPagamento";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("EstadoPagamento", BsEstadoPagamento);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_EstadoPagamentoRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_EstadoPagamentoRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditEstadoPagamento.TextChanged -= textEditEstadoPagamento_TextChanged;

                    textEditEstadoPagamento.Text = _r.Codigo;
                    textEditEstadoPagamentoDesc.Text = _r.Descricao;

                    textEditEstadoPagamento.TextChanged += textEditEstadoPagamento_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }
        }

        private void textEditCertificado1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaCertificados();

                BindingSource BsCertificados = new BindingSource();
                BsCertificados.DataSource = dsEditorVendasDetalhe;
                BsCertificados.DataMember = "TDU_Certificados";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Certificados", BsCertificados);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_CertificadosRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_CertificadosRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditCertificado1.TextChanged -= textEditCertificado1_TextChanged;

                    textEditCertificado1.Text = _r.Codigo;
                    textEditCertificado1Desc.Text = _r.Descricao;

                    textEditCertificado1.TextChanged += textEditCertificado1_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }
        }

        private void textEditCertificado2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaCertificados();

                BindingSource BsCertificados = new BindingSource();
                BsCertificados.DataSource = dsEditorVendasDetalhe;
                BsCertificados.DataMember = "TDU_Certificados";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Certificados", BsCertificados);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_CertificadosRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_CertificadosRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditCertificado2.TextChanged -= textEditCertificado2_TextChanged;

                    textEditCertificado2.Text = _r.Codigo;
                    textEditCertificado2Desc.Text = _r.Descricao;

                    textEditCertificado2.TextChanged += textEditCertificado2_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }


        }

        private void textEditSituacao_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaSituacao();

                BindingSource BsSituacaoes = new BindingSource();
                BsSituacaoes.DataSource = dsEditorVendasDetalhe;
                BsSituacaoes.DataMember = "TDU_SituacoesLote";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Situações", BsSituacaoes);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_SituacoesLoteRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_SituacoesLoteRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditSituacao.TextChanged -= textEditSituacao_TextChanged;

                    textEditSituacao.Text = _r.Codigo;
                    textEditSituacaoDesc.Text = _r.Descricao;

                    textEditSituacao.TextChanged += textEditSituacao_TextChanged;
                }
                frm.Dispose();
                frm = null;
            }


        }

        private void textEditTipoQualidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaTipoQualidade();

                BindingSource BsTipoQualidade = new BindingSource();
                BsTipoQualidade.DataSource = dsEditorVendasDetalhe;
                BsTipoQualidade.DataMember = "TDU_TipoQualidade";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Tipo Qualidade", BsTipoQualidade);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_TipoQualidadeRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_TipoQualidadeRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditTipoQualidade.TextChanged -= textEditTipoQualidade_TextChanged;

                    textEditTipoQualidade.Text = _r.Codigo;
                    textEditTipoQualidadeDesc.Text = _r.Descricao;

                    textEditTipoQualidade.TextChanged += textEditTipoQualidade_TextChanged;
                }
                frm.Dispose();
                frm = null;
            }
        }

        private void textEditPaisOrigem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaPais();

                BindingSource BsPaises = new BindingSource();
                BsPaises.DataSource = dsEditorVendasDetalhe;
                BsPaises.DataMember = "Paises";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Paises", BsPaises);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.PaisesRow _r;
                    _r = (DsEditorVendasDetalhe.PaisesRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditPaisOrigem.TextChanged -= textEditPaisOrigem_TextChanged;

                    textEditPaisOrigem.Text = _r.Codigo;
                    textEditPaisOrigemDesc.Text = _r.Descricao;

                    textEditPaisOrigem.TextChanged += textEditPaisOrigem_TextChanged;
                }
                frm.Dispose();
                frm = null;
            }
        }

        private void textEditParque_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaParques();

                BindingSource BsParques = new BindingSource();
                BsParques.DataSource = dsEditorVendasDetalhe;
                BsParques.DataMember = "TDU_Parques";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Parques", BsParques);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_ParquesRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_ParquesRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditParque.TextChanged -= textEditParque_TextChanged;

                    textEditParque.Text = _r.Codigo;
                    textEditParqueDesc.Text = _r.Descricao;

                    textEditParque.TextChanged += textEditParque_TextChanged;
                }
                frm.Dispose();
                frm = null;
            }
        }

        private void textEditVendedorG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaVendedores();

                BindingSource BsVendedores = new BindingSource();
                BsVendedores.DataSource = dsEditorVendasDetalhe;
                BsVendedores.DataMember = "Vendedores";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Vendedores", BsVendedores);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.VendedoresRow _r;
                    _r = (DsEditorVendasDetalhe.VendedoresRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;

                    textEditVendedorG.Text = _r.Codigo;
                    textEditVendedorDesc.Text = _r.Descricao;
                    spinEditComissaoVendedorG.Value = (decimal)_r.Comissao;

                    textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }


        }

        private void buttonSituacao_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaSituacao();

            BindingSource BsSituacaoes = new BindingSource();
            BsSituacaoes.DataSource = dsEditorVendasDetalhe;
            BsSituacaoes.DataMember = "TDU_SituacoesLote";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Situações", BsSituacaoes);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_SituacoesLoteRow _r;
                _r = (DsEditorVendasDetalhe.TDU_SituacoesLoteRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditSituacao.TextChanged -= textEditSituacao_TextChanged;

                textEditSituacao.Text = _r.Codigo;
                textEditSituacaoDesc.Text = _r.Descricao;

                textEditSituacao.TextChanged += textEditSituacao_TextChanged;
            }
            frm.Dispose();
            frm = null;
        }

        private void buttonTipoQualidade_Click(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaTipoQualidade();

            BindingSource BsTipoQualidade = new BindingSource();
            BsTipoQualidade.DataSource = dsEditorVendasDetalhe;
            BsTipoQualidade.DataMember = "TDU_TipoQualidade";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Tipo Qualidade", BsTipoQualidade);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_TipoQualidadeRow _r;
                _r = (DsEditorVendasDetalhe.TDU_TipoQualidadeRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditTipoQualidade.TextChanged -= textEditTipoQualidade_TextChanged;

                textEditTipoQualidade.Text = _r.Codigo;
                textEditTipoQualidadeDesc.Text = _r.Descricao;

                textEditTipoQualidade.TextChanged += textEditTipoQualidade_TextChanged;
            }
            frm.Dispose();
            frm = null;
        }

        private void buttonPaisOrigem_Click(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaPais();

            BindingSource BsPaises = new BindingSource();
            BsPaises.DataSource = dsEditorVendasDetalhe;
            BsPaises.DataMember = "Paises";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Paises", BsPaises);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.PaisesRow _r;
                _r = (DsEditorVendasDetalhe.PaisesRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditPaisOrigem.TextChanged -= textEditPaisOrigem_TextChanged;

                textEditPaisOrigem.Text = _r.Codigo;
                textEditPaisOrigemDesc.Text = _r.Descricao;

                textEditPaisOrigem.TextChanged += textEditPaisOrigem_TextChanged;
            }
            frm.Dispose();
            frm = null;

        }

        private void buttonParque_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaParques();

            BindingSource BsParques = new BindingSource();
            BsParques.DataSource = dsEditorVendasDetalhe;
            BsParques.DataMember = "TDU_Parques";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Parques", BsParques);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_ParquesRow _r;
                _r = (DsEditorVendasDetalhe.TDU_ParquesRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditParque.TextChanged -= textEditParque_TextChanged;

                textEditParque.Text = _r.Codigo;
                textEditParqueDesc.Text = _r.Descricao;

                textEditParque.TextChanged += textEditParque_TextChanged;
            }
            frm.Dispose();
            frm = null;

        }

        private void buttonVendedorG_Click(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaVendedores();

            BindingSource BsVendedores = new BindingSource();
            BsVendedores.DataSource = dsEditorVendasDetalhe;
            BsVendedores.DataMember = "Vendedores";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Vendedores", BsVendedores);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.VendedoresRow _r;
                _r = (DsEditorVendasDetalhe.VendedoresRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;

                textEditVendedorG.Text = _r.Codigo;
                textEditVendedorGDesc.Text = _r.Descricao;
                spinEditComissaoVendedorG.Value = (decimal)_r.Comissao;

                textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
            }

            frm.Dispose();
            frm = null;

        }

        private void buttonAgenteG_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaAgentes();

            BindingSource BsAgentes = new BindingSource();
            BsAgentes.DataSource = dsEditorVendasDetalhe;
            BsAgentes.DataMember = "TDU_Agentes";
            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Agentes", BsAgentes);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_AgentesRow _r;
                _r = (DsEditorVendasDetalhe.TDU_AgentesRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditAgenteG.TextChanged -= textEditAgenteG_TextChanged;

                textEditAgenteG.Text = _r.Codigo;
                textEditAgenteGDesc.Text = _r.Descricao;
                textEditAgenteG.TextChanged += textEditAgenteG_TextChanged;
            }

            frm.Dispose();
            frm = null;
        }

        private void textEditAgenteG_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaAgentes();

                BindingSource BsAgentes = new BindingSource();
                BsAgentes.DataSource = dsEditorVendasDetalhe;
                BsAgentes.DataMember = "TDU_Agentes";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Agentes", BsAgentes);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_AgentesRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_AgentesRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditAgenteG.TextChanged -= textEditAgenteG_TextChanged;

                    textEditAgenteG.Text = _r.Codigo;
                    textEditAgenteGDesc.Text = _r.Descricao;

                    textEditAgenteG.TextChanged += textEditAgenteG_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }


        }

        private void spinEditComissaoAgente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.KeyChar = ',';
        }

        private void vmpGridControlTotalLInhas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaBancos();

                BindingSource BsBancos = new BindingSource();
                BsBancos.DataSource = dsEditorVendasDetalhe;
                BsBancos.DataMember = "TDU_Bancos";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Bancos", BsBancos);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_BancosRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_BancosRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditBanco.TextChanged -= textEditBanco_TextChanged;

                    textEditBanco.Text = _r.Codigo;
                    textEditBancoDesc.Text = _r.Descricao;

                    textEditBanco.TextChanged += textEditBanco_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }


        }

        private void buttonBanco_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaBancos();

            BindingSource BsBancos = new BindingSource();
            BsBancos.DataSource = dsEditorVendasDetalhe;
            BsBancos.DataMember = "TDU_Bancos";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Bancos", BsBancos);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.TDU_BancosRow _r;
                _r = (DsEditorVendasDetalhe.TDU_BancosRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditBanco.TextChanged -= textEditBanco_TextChanged;

                textEditBanco.Text = _r.Codigo;
                textEditBancoDesc.Text = _r.Descricao;

                textEditBanco.TextChanged += textEditBanco_TextChanged;
            }

            frm.Dispose();
            frm = null;


        }

        private void textEditVendedor_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaVendedores();

                BindingSource BsVendedores = new BindingSource();
                BsVendedores.DataSource = dsEditorVendasDetalhe;
                BsVendedores.DataMember = "Vendedores";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Vendedores", BsVendedores);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();


                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.VendedoresRow _r;
                    _r = (DsEditorVendasDetalhe.VendedoresRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;
                    textEditVendedor.TextChanged -= textEditVendedor_TextChanged;
                    spinEditComissaoVendedor.ValueChanged -= spinEditComissaoVendedor_ValueChanged;

                    textEditVendedor.Text = _r.Codigo;
                    textEditVendedorDesc.Text = _r.Descricao;
                    spinEditComissaoVendedor.Value = (decimal)_r.Comissao;
                    CopiaVendedorCabecLinhas();

                    textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
                    textEditVendedor.TextChanged += textEditVendedor_TextChanged;
                    spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;
                }

                frm.Dispose();
                frm = null;
            }


        }

        private void vmpGridViewLinhasDoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (vmpGridViewLinhasDoc.FocusedRowHandle < 0)
                return;

            // '# apanhar linha do dataset
            DsEditorVendasDetalhe.LinhasDocRow activeRow;

            activeRow = (DsEditorVendasDetalhe.LinhasDocRow)vmpGridViewLinhasDoc.GetFocusedDataRow();//  (DataRowView)GridLinhasDoc(GridLinhasDoc.RowBookmark(e.Row)).Row;

            if (activeRow.TipoLinha == "60" | activeRow.TipoLinha == "65" | activeRow.Artigo == "")
                vmpGridViewLinhasDoc.MakeRowVisible(e.FocusedRowHandle);
            //vmpGridViewLinhasDoc.Columns[0] Rows(e.row).Visible = false;
            vmpGridViewLinhasDoc.GetSelectedRows();
        }

        private void buttonVendedor_Click(object sender, EventArgs e)
        {

            dsEditorVendasDetalhe.CarregaVendedores();

            BindingSource BsVendedores = new BindingSource();
            BsVendedores.DataSource = dsEditorVendasDetalhe;
            BsVendedores.DataMember = "Vendedores";

            ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
            FrmListaBsView frm = result.Result;
            frm.IniciaListaBs("Vendedores", BsVendedores);
            FormatarGrelhaLListas(ref frm);
            frm.ShowDialog();


            if (frm.DialogResult == DialogResult.OK)
            {

                DsEditorVendasDetalhe.VendedoresRow _r;
                _r = (DsEditorVendasDetalhe.VendedoresRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                textEditVendedorG.TextChanged -= textEditVendedorG_TextChanged;
                textEditVendedor.TextChanged -= textEditVendedor_TextChanged;
                spinEditComissaoVendedor.ValueChanged -= spinEditComissaoVendedor_ValueChanged;

                textEditVendedor.Text = _r.Codigo;
                textEditVendedorDesc.Text = _r.Descricao;
                spinEditComissaoVendedor.Value = (decimal)_r.Comissao;
                CopiaVendedorCabecLinhas();

                spinEditComissaoVendedor.ValueChanged += spinEditComissaoVendedor_ValueChanged;
                textEditVendedorG.TextChanged += textEditVendedorG_TextChanged;
                textEditVendedor.TextChanged += textEditVendedor_TextChanged;
            }

            frm.Dispose();
            frm = null;


        }

        private void textEditNFatura_Validated(object sender, EventArgs e)
        {

            DsEditorVendasDetalhe.LinhasDocRow activeRow;
            activeRow = (DsEditorVendasDetalhe.LinhasDocRow)vmpGridViewLinhasDoc.GetDataRow(vmpGridViewLinhasDoc.FocusedRowHandle); //(DataRowView)GridLinhasDoc(GridLinhasDoc.RowBookmark(GridLinhasDoc.Row)).Row;

            if (dsEditorVendasDetalhe.DocumentoVenda.Tipodoc == Enums.TiposDocumento.Proforma | dsEditorVendasDetalhe.DocumentoVenda.Tipodoc == Enums.TiposDocumento.Fatura | dsEditorVendasDetalhe.DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaO | dsEditorVendasDetalhe.DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaI)
                return;

            if (Convert.IsDBNull(textEditNFatura.Text) | textEditNFatura.Text == null | textEditNFatura.Text == "")
                return;

            string NFaturaNova = textEditNFatura.Text;
            double NumLinha = activeRow.NumLinha;
            dsEditorVendasDetalhe.AtualizaTabelaTotalLinhasMudaFatura(NFaturaNova, NumLinha);
        }

        private void spinEditFOB_Validated(object sender, EventArgs e)
        {
            if (dsEditorVendasDetalhe.DocumentoVenda.Tipodoc == Enums.TiposDocumento.Proforma | dsEditorVendasDetalhe.DocumentoVenda.Tipodoc == Enums.TiposDocumento.Fatura | dsEditorVendasDetalhe.DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaO | dsEditorVendasDetalhe.DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaI)
                return;

            if (Convert.IsDBNull(spinEditFOB.Value))
                return;

            double ValorFOB = Convert.ToDouble(spinEditFOB.Value);

            DsEditorVendasDetalhe.LinhasDocRow activeRow;
            activeRow = (DsEditorVendasDetalhe.LinhasDocRow)vmpGridViewLinhasDoc.GetDataRow(vmpGridViewLinhasDoc.FocusedRowHandle);  // (DataRowView)GridLinhasDoc(GridLinhasDoc.RowBookmark(GridLinhasDoc.Row)).Row;

            double NumLinha = activeRow.NumLinha;

            dsEditorVendasDetalhe.AtualizaTabelaTotalLinhasMudaFOB(ValorFOB, NumLinha);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Help.ShowHelp(this, helpProviderEditorVendas.HelpNamespace);
        }

        private bool Verifica_Se_Documento_Venda_Atual_Foi_Gerado_Por_Transformacao_Documentos(string Tipo_Doc, string Serie, string Num_Doc)
        {


            // ----------------------------------------------------------------
            // --- 2019.06.13 - VIMAPONTO - Gualter Costa  - Redmine # 1558 ---
            // ----------------------------------------------------------------

            // Verifica se o documento de compra atual tem as linhas do documento registadas na tabela LinhasDocTrans. 
            // Se sim, indica que o mesmo foi criado por transformação de documento


            string Sql_Query;
            StdBE100.StdBELista Lista_Primavera = new StdBE100.StdBELista();


            //Verifica_Se_Documento_Venda_Atual_Foi_Gerado_Por_Transformacao_Documentos = false; // Inicializa a false


            Sql_Query = "select * from LinhasDocTrans where IdLinhasDoc in ( " + "Select LinhasDoc.id " + "from CabecDoc inner join LinhasDoc on LinhasDoc.IdCabecDoc = CabecDoc.Id " + "where CabecDoc.TipoDoc = '" + Strings.Trim(Tipo_Doc) + "' and CabecDoc.Serie = '" + Strings.Trim(Serie) + "' and CabecDoc.NumDoc = '" + Strings.Trim(Num_Doc) + "')";


            Lista_Primavera = BSO.Consulta(Sql_Query);


            // Se detectou id's de linhas do documento atual na tabela LinhasDocTrans, retorna true
            if (Lista_Primavera.NumLinhas() > 0)
                return true;

            Lista_Primavera.Vazia();
            return false;
        }


        private void vmpGridViewTotalLinhas_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            string CampoGrelha = vmpGridViewTotalLinhas.FocusedColumn.FieldName;//    vmpGridViewTotalLinhas.GetDisplayTextByColumnValue(e.Column, e.Value);//TotalLinhas.Splits(0).DisplayColumns(TotalLinhas.Col).DataColumn.DataField;

            DsEditorVendasDetalhe.TotalLinhasRow activeRow;
            activeRow = (DsEditorVendasDetalhe.TotalLinhasRow)vmpGridViewTotalLinhas.GetDataRow(e.RowHandle);// (DataRowView)TotalLinhas(TotalLinhas.RowBookmark(TotalLinhas.Row)).Row;

            switch (CampoGrelha)
            {
                case "ValorFOB":
                    {
                        string NFatura = activeRow.NFatura;

                        foreach (DsEditorVendasDetalhe.LinhasDocRow _reg in dsEditorVendasDetalhe.LinhasDoc)
                        {
                            if (_reg.CDU_NFatura == NFatura)
                                _reg.CDU_ValorFOB = activeRow.ValorFOB;
                        }

                        break;
                    }

                case "TotalEmb":
                    {
                        if (dsEditorVendasDetalhe.DocumentoVenda.Tipodoc != Enums.TiposDocumento.Fatura & dsEditorVendasDetalhe.DocumentoVenda.Tipodoc != Enums.TiposDocumento.FaturaO & dsEditorVendasDetalhe.DocumentoVenda.Tipodoc != Enums.TiposDocumento.FaturaI)
                            return;

                        if (Convert.IsDBNull(activeRow.TotalEmb))
                            return;

                        dsEditorVendasDetalhe.calculoTotal(activeRow.TotalEmb, activeRow.NFatura);
                        vmpGridViewLinhasDoc.RefreshData();
                        break;
                    }
            }


        }

        private void textEditBanco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                dsEditorVendasDetalhe.CarregaBancos();

                BindingSource BsBancos = new BindingSource();
                BsBancos.DataSource = dsEditorVendasDetalhe;
                BsBancos.DataMember = "TDU_Bancos";

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmListaBsView));
                FrmListaBsView frm = result.Result;
                frm.IniciaListaBs("Bancos", BsBancos);
                FormatarGrelhaLListas(ref frm);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {

                    DsEditorVendasDetalhe.TDU_BancosRow _r;
                    _r = (DsEditorVendasDetalhe.TDU_BancosRow)frm.vmpGridViewListaBs.GetDataRow(frm.vmpGridViewListaBs.FocusedRowHandle);

                    textEditBanco.TextChanged -= textEditBanco_TextChanged;

                    textEditBanco.Text = _r.Codigo;
                    textEditBancoDesc.Text = _r.Descricao;

                    textEditBanco.TextChanged += textEditBanco_TextChanged;
                }

                frm.Dispose();
                frm = null;
            }

        }

        private void spinEditComissaoVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.KeyChar = ',';
        }

        private void spinEditComissaoAgenteG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.KeyChar = ',';
        }

        private void spinEditComissaoVendedorG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
                e.KeyChar = ',';
        }

        private void textEditVendedorG_TextChanged(object sender, EventArgs e)
        {
            dsEditorVendasDetalhe.CarregaVendedores();
            DsEditorVendasDetalhe.VendedoresRow reg = dsEditorVendasDetalhe.Vendedores.FindByCodigo(textEditVendedorG.Text);
            if (reg != null)
            {
                textEditVendedorG.Text = reg.Codigo;
                textEditVendedorGDesc.Text = reg.Descricao;
                spinEditComissaoVendedorG.Value = Convert.ToDecimal(reg.Comissao);
            }
            else
            {
                textEditVendedorGDesc.Text = "";
                spinEditComissaoVendedorG.Value = 0;
            }
        }
    }
}

