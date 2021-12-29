using System;
using System.Data;
using VndBE100;
using TesBE100;
using CblBE100;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Linq;
using System.Collections;
using Vimaponto.PrimaveraV100.Clientes.Filopa.EditorVendasDetalhe.DataSets.DsEditorVendasDetalheTableAdapters;
using EditorVendasDetalhe;

namespace Vimaponto.PrimaveraV100.Clientes.Filopa.EditorVendasDetalhe.DataSets
{
    partial class DsEditorVendasDetalhe
    {

        public VndBEDocumentoVenda DocumentoVenda = new VndBEDocumentoVenda();
        public TesBEDocumentoTesouraria DocumentoTesouraria = new TesBEDocumentoTesouraria();
        public CblBEDocumento DocumentoCBL = new CblBEDocumento();
        public int LinhaActual;
        public string Nome;

        public void SetContexto(VndBEDocumentoVenda DocumentoVenda, TesBEDocumentoTesouraria DocumentoTesouraria, CblBEDocumento DocumentoCBL, int LinhaActual, string Nome)
        {
            this.DocumentoVenda = DocumentoVenda;
            this.DocumentoTesouraria = DocumentoTesouraria;
            this.DocumentoCBL = DocumentoCBL;
            this.LinhaActual = LinhaActual;
            this.Nome = Nome;
        }

        public void GetContexto(object DocumentoVenda, object DocumentoTesouraria, object DocumentoCBL, int LinhaActual, string Nome)
        {
            DocumentoVenda = this.DocumentoVenda;
            DocumentoTesouraria = this.DocumentoTesouraria;
            DocumentoCBL = this.DocumentoCBL;
            LinhaActual = this.LinhaActual;
            Nome = this.Nome;
        }

        public void LimparTabelas()
        {
            CabecDoc.Clear();
            LinhasDoc.Clear();
        }

        public void VerificaCamposObrigatorios()
        {

            switch (DocumentoVenda.Tipodoc)
            {
                case Enums.TiposDocumento.Encomenda:
                    if (DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()) == "" )
                    {
                        throw new Exception("O campo Cliente do cabeçalho do documento não está preenchido.");
                    }

                    if ( DocumentoVenda.CamposUtil["CDU_Agente"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Agente"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo Agente do cabeçalho do documento não está preenchido.");
                        }

                    if (DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor == null  || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo Incoterms do cabeçalho do documento não está preenchido.");
                        }

                    if (DocumentoVenda.CamposUtil["CDU_Comissao"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Comissao"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo Comissão do cabeçalho do documento não está preenchido.");
                        }


                    for (int NLinha = 1; NLinha < DocumentoVenda.Linhas.NumItens; NLinha++)
                    {

                        if (DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "60" && DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "65")
                        {
                            if (Convert.IsDBNull(DocumentoVenda.Linhas.GetEdita(NLinha).PrecUnit))
                                throw new Exception("O campo Preço Unitário da linha " + NLinha + " do documento não está preenchido.");
                        }




                        //'If DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil("CDU_Comissao"].Valor = 0 
                        //'    MsgBox("O campo Comissão da linha '" & NLinha & "' do documento tem o valor 0", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                        //'

                    }
                    break;

                case Enums.TiposDocumento.Contrato:

                    if (DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()) == "" )
                        throw new Exception("O campo Cliente do cabeçalho do documento não está preenchido.");



                    if (DocumentoVenda.CamposUtil["CDU_Agente"].Valor == null  || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Agente"].Valor.ToString()) == "" )
                        throw new Exception("O campo Agente do cabeçalho do documento não está preenchido.");



                    if (DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString()) == "")
                        throw new Exception("O campo Incoterms do cabeçalho do documento não está preenchido.");



                    if (DocumentoVenda.CamposUtil["CDU_Comissao"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Comissao"].Valor.ToString()) == "")
                        throw new Exception("O campo Comissão do cabeçalho do documento não está preenchido.");



                    if ( DocumentoVenda.CamposUtil["CDU_Localidade"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Localidade"].Valor.ToString()) == "" )
                        throw new Exception("O campo Localidade do cabeçalho do documento não está preenchido. ");



                    if (DocumentoVenda.Referencia == "")
                        throw new Exception("O campo Número do cabeçalho (V/Refer.) do documento não está preenchido.");

                    for (int NLinha = 1; NLinha < DocumentoVenda.Linhas.NumItens; NLinha++)
                    {

                        if (DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "60" && DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "65")
                        {
                            if (Convert.IsDBNull(DocumentoVenda.Linhas.GetEdita(NLinha).PrecUnit))
                                throw new Exception("O campo Preço Unitário da linha " + NLinha + " do documento não está preenchido.");

                        }
                        //'If DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil("CDU_Comissao"].Valor = 0 Then
                        //'    MsgBox("O campo Comissão da linha '" & NLinha & "' do documento tem o valor 0", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                        //'End If
                    }

                    break;

                //Embarque
                case Enums.TiposDocumento.Embarque:

                    if (DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo Cliente do cabeçalho do documento não está preenchido.");
                    }

                    if (DocumentoVenda.CamposUtil["CDU_Agente"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Agente"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo Agente do cabeçalho do documento não está preenchido.");
                    }

                    if (DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo Incoterms do cabeçalho do documento não está preenchido.");
                    }

                    if (DocumentoVenda.CamposUtil["CDU_Comissao"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Comissao"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo Comissão do cabeçalho do documento não está preenchido.");
                    }

                    if (DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo Companhia Marítima do cabeçalho do documento não está preenchido.");
                    }

                    if (DocumentoVenda.CamposUtil["CDU_NBL"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_NBL"].Valor.ToString()) == "")
                    {
                        throw new Exception("O campo NBL do cabeçalho do documento não está preenchido.");
                    }

                    for (int NLinha = 1; NLinha < DocumentoVenda.Linhas.NumItens; NLinha++)
                    {
                        if (DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "60" && DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "65")
                        {
                            if (Convert.IsDBNull(DocumentoVenda.Linhas.GetEdita(NLinha).PrecUnit))
                            {
                                throw new Exception("O campo Preço Unitário da linha " + NLinha + " do documento não está preenchido.");
                            }

                            if (DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_NVolumes"].Valor==null || DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_NVolumes"].Valor.ToString()=="")
                            {
                                throw new Exception("O campo Nº Volumes da linha " + NLinha + " do documento não está preenchido.");
                            }

                            if (DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_NContentor"].Valor == null || DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_NContentor"].Valor.ToString() == "")
                            {
                                throw new Exception("O campo Nº Contentor da linha " + NLinha + " do documento não está preenchido.");
                            }

                            if (DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_NFatura"].Valor == null || DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_NFatura"].Valor.ToString() == "")
                            {
                                throw new Exception("O campo Nº Fatura da linha " + NLinha + " do documento não está preenchido.");
                            }

                            if (DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_LoteFornecedor"].Valor == null || DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_LoteFornecedor"].Valor.ToString() == "")
                            {
                                throw new Exception("O campo Lote Fornecedor da linha " + NLinha + " do coumento não está preenchido");
                            }
                        }


                        // If DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil("CDU_Comissao"].Valor = 0 Then
                        // MsgBox("O campo Comissão da linha '" & NLinha & "' do documento tem o valor 0", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                        // End If
                    }
                    break;

                case Enums.TiposDocumento.Proforma:
                    {
                        if (DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()) == "")
                        {
                            throw new Exception("O campo Cliente do cabeçalho do documento não está preenchido.");
                        }

                        if (DocumentoVenda.CamposUtil["CDU_Agente"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Agente"].Valor.ToString()) == "")
                        {
                            throw new Exception("O campo Agente do cabeçalho do documento não está preenchido.");
                        }

                        if (DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString()) == "")
                        {
                            throw new Exception("O campo Incoterms do cabeçalho do documento não está preenchido.");
                        }

                        if (DocumentoVenda.CamposUtil["CDU_Comissao"].Valor == null || Strings.Trim(DocumentoVenda.CamposUtil["CDU_Comissao"].Valor.ToString()) == "")
                        {
                            throw new Exception("O campo Comissão do cabeçalho do documento não está preenchido.");
                        }


                        // AndAlso Me.DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha <> "51" : ignora linha artigo especial se for necessário
                        for (int NLinha = 1; NLinha < DocumentoVenda.Linhas.NumItens; NLinha++)
                        {
                            {
                                if (DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "60" && DocumentoVenda.Linhas.GetEdita(NLinha).IDLinhaOriginal == "" && DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "65")
                                {
                                    if (Convert.IsDBNull(DocumentoVenda.Linhas.GetEdita(NLinha).PrecUnit))
                                    {
                                        throw new Exception("O campo Preço Unitário da linha " + NLinha + " do documento não está preenchido.");
                                    }

                                    if (DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_NFatura"].Valor == null || DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil["CDU_NFatura"].Valor.ToString() == "")
                                    {
                                        throw new Exception("O campo Nº Fatura da linha " + NLinha + " do documento não está preenchido.");
                                    }

                                    // If DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil("CDU_Comissao"].Valor = 0 Then
                                    // MsgBox("O campo Comissão da linha '" & NLinha & "' do documento tem o valor 0", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                                    // End If

                                }
                            }

                        }
                    }

                    break;

                //'Fatura
                case Enums.TiposDocumento.Fatura:

                    //'If trim(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor"].Valor.tostring) = "" Then
                    //'    Throw New Exception("O campo Cliente do cabeçalho do documento não está preenchido.")
                    //'End If

                    //'If trim(Me.DocumentoVenda.CamposUtil("CDU_Agente"].Valor.tostring) = "" Then
                    //'    Throw New Exception("O campo Agente do cabeçalho do documento não está preenchido.")
                    //'End If

                    //'If trim(Me.DocumentoVenda.CamposUtil("CDU_Incoterms"].Valor.tostring) = "" Then
                    //'    Throw New Exception("O campo Incoterms do cabeçalho do documento não está preenchido.")
                    //'End If

                    //'If trim(Me.DocumentoVenda.CamposUtil("CDU_Comissao"].Valor.tostring) = "" Then
                    //'    Throw New Exception("O campo Comissão do cabeçalho do documento não está preenchido.")
                    //'End If


                    for (int NLinha = 1; NLinha < DocumentoVenda.Linhas.NumItens; NLinha++)

                        if (DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "60" && DocumentoVenda.Linhas.GetEdita(NLinha).IDLinhaOriginal == "" && DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "65")
                        {
                            if (Convert.IsDBNull(DocumentoVenda.Linhas.GetEdita(NLinha).PrecUnit))
                                throw new Exception("O campo Preço Unitário da linha " + NLinha + " do documento não está preenchido.");
                        }


                    //'If DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil("CDU_NFatura"].Valor = "" Then
                    //'    Throw New Exception("O campo Nº Fatura da linha '" & NLinha & "' do documento não está preenchido.")
                    //'End If

                    //'If DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil("CDU_Comissao"].Valor = 0 Then
                    //'    MsgBox("O campo Comissão da linha '" & NLinha & "' do documento tem o valor 0", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                    //'End If



                    //'Case Enums.TiposDocumento.FaturaO

                    //'    For NLinha As Integer = 1 To Me.DocumentoVenda.Linhas.NumItens
                    //'        If Me.DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha <> "60" AndAlso Me.DocumentoVenda.Linhas.GetEdita(NLinha).IDLinhaOriginal = "" AndAlso Me.DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha <> "65" Then

                    //'            'If DocumentoVenda.Linhas.GetEdita(NLinha).CamposUtil("CDU_Comissao"].Valor = 0 Then
                    //'            '    MsgBox("O campo Comissão da linha '" & NLinha & "' do documento tem o valor 0", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                    //'            'End If

                    //'        End If

                    //'    Next

                    break;
            }
        }
        public void VerificaFluxoDocumental()
        {
            switch (DocumentoVenda.Tipodoc)
            {
                case Enums.TiposDocumento.Embarque:
                    {
                        if (DocumentoVenda.EmModoEdicao == false)
                        {
                            for (int NLinha = 1; NLinha <= DocumentoVenda.Linhas.NumItens; NLinha++)
                            {
                                if (DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "60" && DocumentoVenda.Linhas.GetEdita(NLinha).TipoLinha != "65")
                                {
                                    if (DocumentoVenda.Linhas.GetEdita(NLinha).IDLinhaOriginal == "")
                                    {
                                        DocumentoVenda.Linhas.RemoveTodos();
                                        throw new Exception("Não é possível criar um Embarque sem seguir o fluxo documental Contrato -> Embarque.");
                                    }
                                }
                            }
                        }

                        break;
                    }
            }
        }

        public void VerificaFluxosDocumentos()
        {

            // Todas as linhas que estão a ser transformadas pertencem a um documento válido para o fluxo (pelo idLinhaOriginal)

            for (int Linha = 1; Linha <= this.DocumentoVenda.Linhas.NumItens; Linha++)
            {
                if (this.DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & this.DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                {
                    DataTable DTTipoDocLinhaOriginal = new DataTable();
                    DTTipoDocLinhaOriginal = PriV100Api.BSO.DSO.ConsultaDataTable("SELECT cd.TipoDoc, cd.Serie, cd.NumDoc, ld.NumLinha FROM LinhasDoc ld INNER JOIN CabecDoc cd ON cd.Id = ld.IdCabecDoc WHERE CONVERT(VARCHAR(255), ld.Id)   = '" + DocumentoVenda.Linhas.GetEdita(Linha).IDLinhaOriginal.ToString() + "' ");

                    string TipoDocOriginal = DTTipoDocLinhaOriginal.Rows[0]["TipoDoc"].ToString();
                    string DocumentoOriginal = DTTipoDocLinhaOriginal.Rows[0]["TipoDoc"].ToString() + "." + DTTipoDocLinhaOriginal.Rows[0]["Serie"].ToString() + "." + DTTipoDocLinhaOriginal.Rows[0]["NumDoc"].ToString();

                    int NumLinhaOriginal = Convert.ToInt32(DTTipoDocLinhaOriginal.Rows[0]["NumLinha"]);

                    if (TipoDocOriginal.ToString() == Enums.TiposDocumento.Encomenda)
                    {
                        if (this.DocumentoVenda.Tipodoc.ToString() != Enums.TiposDocumento.Contrato)
                        {
                            MessageBox.Show("A linha " + NumLinhaOriginal + " do documento " + DocumentoOriginal + " não segue o fluxo documental  Encomenda -> Contrato", "Editor Vendas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.DocumentoVenda.Linhas.RemoveTodos();
                        }
                    }

                    if (TipoDocOriginal.ToString() == Enums.TiposDocumento.Contrato)
                    {
                        if (this.DocumentoVenda.Tipodoc.ToString() != Enums.TiposDocumento.Embarque)
                        {
                            MessageBox.Show("A linha " + NumLinhaOriginal + " do documento " + DocumentoOriginal + " não segue o fluxo documental  Contrato -> Embarque", "Editor Vendas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.DocumentoVenda.Linhas.RemoveTodos();
                        }
                    }

                    if (TipoDocOriginal.ToString() == Enums.TiposDocumento.Embarque)
                    {
                        if (this.DocumentoVenda.Tipodoc.ToString() != Enums.TiposDocumento.Proforma)
                        {
                            MessageBox.Show("A linha " + NumLinhaOriginal + " do documento " + DocumentoOriginal + " não segue o fluxo documental  Embarque -> Fatura Proforma", "Editor Vendas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.DocumentoVenda.Linhas.RemoveTodos();
                        }
                    }

                    if (TipoDocOriginal.ToString() == Enums.TiposDocumento.Proforma)
                    {
                        if (this.DocumentoVenda.Tipodoc.ToString() != Enums.TiposDocumento.Fatura & this.DocumentoVenda.Tipodoc.ToString() != Enums.TiposDocumento.FaturaO & this.DocumentoVenda.Tipodoc.ToString() != Enums.TiposDocumento.FaturaI)
                        {
                            MessageBox.Show("A linha " + NumLinhaOriginal + " do documento " + DocumentoOriginal + " não segue o fluxo documental  Fatura Proforma -> Fatura", "Editor Vendas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.DocumentoVenda.Linhas.RemoveTodos();
                        }
                    }
                }
            }
        }


        public void CondPagLinhas()
        {
            try
            {
                for (int linha = 1; linha <= DocumentoVenda.Linhas.NumItens; linha++)
                {
                    if (DocumentoVenda.Linhas.GetEdita(linha).TipoLinha != "65" & DocumentoVenda.Linhas.GetEdita(linha).TipoLinha != "60")
                    {
                        if (DocumentoVenda.CondPag == "")
                        {
                        }
                        else
                            DocumentoVenda.Linhas.GetEdita(linha).CamposUtil["CDU_CondPag"].Valor = DocumentoVenda.CondPag;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PreencheCDUs(int NumLinha = -1)
        {
            // If Me.DocumentoVenda.EmModoEdicao = False Then
            if (NumLinha > 0)
            {
                // verifica se não tem rasteabilidade, se não é linha de comentário e se está em modo de edição
                if (DocumentoVenda.Linhas.GetEdita(NumLinha).IDLinhaOriginal == "" && DocumentoVenda.Linhas.GetEdita(NumLinha).TipoLinha != "60" && DocumentoVenda.Linhas.GetEdita(NumLinha).TipoLinha != "65" && DocumentoVenda.EmModoEdicao == false)
                {
                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Agente"].Valor.ToString() == "" && Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Agente"].Valor) && DocumentoVenda.CamposUtil["CDU_Agente"].Valor.ToString() != "")
                        DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Agente"].Valor = DocumentoVenda.CamposUtil["CDU_Agente"].Valor;
                    if ((Convert.ToInt32(DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Comissao"].Valor) == 0) && Convert.ToInt32(DocumentoVenda.CamposUtil["CDU_Comissao"].Valor) != 0)
                        DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Comissao"].Valor = DocumentoVenda.CamposUtil["CDU_Comissao"].Valor;
                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Incoterms"].Valor.ToString() == "" && DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString() != "")
                        DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_Incoterms"].Valor = DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor;

                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).Vendedor == "" && Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor) && DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor.ToString() != "")
                        DocumentoVenda.Linhas.GetEdita(NumLinha).Vendedor = DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor.ToString();
                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).Comissao == 0 && Convert.ToInt32(DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor) != 0)
                        DocumentoVenda.Linhas.GetEdita(NumLinha).Comissao = Convert.ToDouble(DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor);
                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_CondPag"].Valor.ToString() == "" && DocumentoVenda.CondPag != "")
                        DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_CondPag"].Valor = DocumentoVenda.CondPag;
                }
            }
            else

                // Chamada recursiva
                // Para todas as linhas do documento com artigo definido, copia os valores do cabeçalho
                for (int Lin = 1; Lin <= DocumentoVenda.Linhas.NumItens; Lin++)
                    PreencheCDUs(Lin);
        }

        public void CarregaAgentes()
        {
            TDU_AgentesTableAdapter AdpAgentes = new TDU_AgentesTableAdapter();
            TDU_Agentes.Clear();
            AdpAgentes.Fill(TDU_Agentes);
        }

        public void CarregaCompanhiasMaritimas()
        {
            TDU_CompanhiasMaritimasTableAdapter AdpCompanhiasMaritimas = new TDU_CompanhiasMaritimasTableAdapter();
            TDU_CompanhiasMaritimas.Clear();
            AdpCompanhiasMaritimas.Fill(TDU_CompanhiasMaritimas);
        }

        public void CarregaEstadoPagamento()
        {
            TDU_EstadoPagamentoTableAdapter AdpEstadoPagamento = new TDU_EstadoPagamentoTableAdapter();

            TDU_EstadoPagamento.Clear();
            AdpEstadoPagamento.Fill(TDU_EstadoPagamento);
        }

        public void CarregaCertificados()
        {
            TDU_CertificadosTableAdapter AdpCertificados = new TDU_CertificadosTableAdapter();

            TDU_Certificados.Clear();
            AdpCertificados.Fill(TDU_Certificados);
        }

        public void CarregaBancos()
        {
            TDU_BancosTableAdapter AdpBancos = new TDU_BancosTableAdapter();

            TDU_Bancos.Clear();
            AdpBancos.Fill(TDU_Bancos);
        }

        public void CarregaIntrastatCondEntrega()
        {
            IntrastatCondEntregaTableAdapter AdpIntrastatCondEntrega = new IntrastatCondEntregaTableAdapter();

            IntrastatCondEntrega.Clear();
            AdpIntrastatCondEntrega.Fill(IntrastatCondEntrega);

        }

        public void CarregaFornecedores()
        {
            FornecedoresTableAdapter AdpFornecedores = new FornecedoresTableAdapter();

            Fornecedores.Clear();
            AdpFornecedores.Fill(Fornecedores);
        }

        public void CarregaVendedores()
        {
            VendedoresTableAdapter AdpVendedores = new VendedoresTableAdapter();

            Vendedores.Clear();
            AdpVendedores.Fill(Vendedores);
        }

        public void CarregaLocais()
        {
            TDU_LocaisTableAdapter AdpLocais = new TDU_LocaisTableAdapter();

            TDU_Locais.Clear();
            AdpLocais.Fill(TDU_Locais);
        }

        public void CarregaSituacao()
        {
            TDU_SituacoesLoteTableAdapter AdpSituacao = new TDU_SituacoesLoteTableAdapter();

            TDU_SituacoesLote.Clear();
            AdpSituacao.Fill(TDU_SituacoesLote);
        }

        public void CarregaTipoQualidade()
        {
            TDU_TipoQualidadeTableAdapter AdpTipoQualidade = new TDU_TipoQualidadeTableAdapter();

            TDU_TipoQualidade.Clear();
            AdpTipoQualidade.Fill(TDU_TipoQualidade);
        }

        public void CarregaPais()
        {
            PaisesTableAdapter AdpPais = new PaisesTableAdapter();

            Paises.Clear();
            AdpPais.Fill(Paises);
        }

        public void CarregaParques()
        {
            TDU_ParquesTableAdapter AdpParques = new TDU_ParquesTableAdapter();

            TDU_Parques.Clear();
            AdpParques.Fill(TDU_Parques);
        }

        public void CopiaCampoParaLinhas(string Campo)
        {
            try
            {
                foreach (LinhasDocRow Linha in LinhasDoc.Select("TipoLinha <> '60' AND IDLinhaOriginal = '' AND TipoLinha <> '65'"))
                    Linha[Campo] = CabecDoc[0][Campo];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CopiaAgenteParaLinhas(string agente, string descricao)
        {
            try
            {
                foreach (LinhasDocRow linha in LinhasDoc.Select("TipoLinha <> '60' AND IDLinhaOriginal = '' AND TipoLinha <> '65'"))
                {
                    linha[agente] = CabecDoc[0][agente];
                    linha["AgenteNome"] = CabecDoc[0][descricao];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CopiaFOBParaLinhas(string Campo, string NFatura)
        {
            try
            {
                foreach (LinhasDocRow Linha in LinhasDoc.Select("TipoLinha <> '60' AND IDLinhaOriginal = '' AND TipoLinha <> '65' AND CDU_NFatura='" + NFatura + "'"))
                    Linha[Campo] = CabecDoc[0][Campo];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CopiaVendedorParaLinhas(string Fornecedor)
        {
            try
            {
                foreach (LinhasDocRow Linha in LinhasDoc.Select("TipoLinha <> '60' AND IDLinhaOriginal = '' AND TipoLinha <> '65'"))
                {
                    DataTable DTFornecedor = new DataTable();
                    DTFornecedor = PriV100Api.BSO.DSO.ConsultaDataTable("Select TOP 1 ISNULL(CDU_Vendedor, '') AS CDU_Vendedor, ISNULL(CDU_Comissao,0) AS CDU_Comissao From Fornecedores Where Fornecedor= '" + Fornecedor + "' ");
                    if (DTFornecedor.Rows.Count <= 0)
                    {
                        Linha.Vendedor = "";
                        Linha.VendedorNome = "";
                        Linha.ComissaoVendedor = 0;
                    }
                    else
                    {
                        Linha.Vendedor = DTFornecedor.Rows[0]["CDU_Vendedor"].ToString();
                        Linha.ComissaoVendedor = Convert.ToDouble(DTFornecedor.Rows[0]["CDU_Comissao"]);

                        if (PriV100Api.BSO.DSO.DaValorUnico("Select TOP 1 Nome From Vendedores Where Vendedor = '" + Linha.Vendedor + "' ") is string vendedor)
                            Linha.VendedorNome = vendedor;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AlteraPrcUnitCopiaLinhas(ref VndBEDocumentoVenda ObjectoDestino, ref bool Cancel)
        {
            Cancel = false;

            DataTable FornecedorGrupo = new DataTable();
            FornecedorGrupo = PriV100Api.BSO.DSO.ConsultaDataTable("SELECT CDU_NomeEmpresaGrupo FROM Fornecedores WHERE Fornecedor = '" + ObjectoDestino.CamposUtil["CDU_Fornecedor"].Valor + "' ");
            string NomeEmpresaGrupo = FornecedorGrupo.Rows[0]["CDU_NomeEmpresaGrupo"].ToString();

            if (Strings.Trim(NomeEmpresaGrupo) != "")
            {
                for (var nlinha = 1; nlinha <= ObjectoDestino.Linhas.NumItens; nlinha++)
                {
                    if (ObjectoDestino.Linhas.GetEdita(nlinha).TipoLinha != "60" & ObjectoDestino.Linhas.GetEdita(nlinha).TipoLinha != "65")
                    {

                        // a comissão do agente tem de ser = 0 para fazer a copia
                        if (Convert.ToInt32(ObjectoDestino.Linhas.GetEdita(nlinha).CamposUtil["CDU_Comissao"].Valor) != 0)
                        {
                            MessageBox.Show("A comissão do agente da linha " + nlinha + " é diferente de zero, a cópia não foi concluida", "Editor de Cópia de Linhas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Cancel = true;
                            return;
                        }
                        else
                            // a comissão do vendedor tem de ser > 0
                            if (ObjectoDestino.Linhas.GetEdita(nlinha).Comissao > 0)
                        {
                            if (ObjectoDestino.Linhas.GetEdita(nlinha).Quantidade == 0)
                                MessageBox.Show("A Quantidade da linha " + nlinha + " é igual a zero o preço unitário não foi alterado", "Editor de Cópia de Linhas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            else
                                // mudar preço unitário para ComissãoVendedor * PrecoLiquido / Quantidade
                                ObjectoDestino.Linhas.GetEdita(nlinha).PrecUnit = (ObjectoDestino.Linhas.GetEdita(nlinha).Comissao / Convert.ToDouble(100) * ObjectoDestino.Linhas.GetEdita(nlinha).PrecoLiquido / Convert.ToDouble(ObjectoDestino.Linhas.GetEdita(nlinha).Quantidade));
                        }
                        else
                        {
                            // aviso caso a comissão do vendedor seja 0
                            MessageBox.Show("A comissão do vendedor da linha " + nlinha + " é menor ou igual a zero, a cópia não foi concluida", "Editor de Cópia de Linhas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        public static void AtualizaIvaNasLinhasDoc(VndBEDocumentoVenda pDoc)
        {
            int i;
            string vCodIva = "";
            double vTaxaIva = 0;
            string vTipoOperacao = "";

            if (PriV100Api.BSO.Contexto.LocalizacaoSede == StdBE100.StdBETipos.EnumLocalizacaoSede.lsPortugalCont)
            {

                // 0 - Nacional - Normal
                if (pDoc.EspacoFiscal == 0)
                {

                    // 0 - Normal
                    if (pDoc.RegimeIva == "0")
                    {
                        vCodIva = "";
                        vTaxaIva = 0;
                        vTipoOperacao = Constants.vbNullString;
                    }
                    else if (pDoc.RegimeIva == "3")
                    {
                        vCodIva = PriV100Api.BSO.Base.Parametros.DaValorAtributo("IvaReverseCharge");
                        vTaxaIva = PriV100Api.BSO.Base.Iva.DaValorAtributo(vCodIva, "Taxa");
                        vTipoOperacao = Constants.vbNullString;
                    }
                    else if (pDoc.RegimeIva == "2")
                    {
                        vCodIva = PriV100Api.BSO.Base.Parametros.DaValorAtributo("IvaIsento");
                        vTaxaIva = PriV100Api.BSO.Base.Iva.DaValorAtributo(vCodIva, "Taxa");
                        vTipoOperacao = Constants.vbNullString;
                    }
                }
                else if (pDoc.EspacoFiscal == 1)
                {

                    // 0 - Isento
                    if (pDoc.RegimeIva == "2")
                    {
                        vCodIva = PriV100Api.BSO.Base.Parametros.DaValorAtributo("IvaIsento");
                        vTaxaIva = PriV100Api.BSO.Base.Iva.DaValorAtributo(vCodIva, "Taxa");
                        vTipoOperacao = Constants.vbNullString;
                    }
                }
                else if (pDoc.EspacoFiscal == 2)
                {

                    // 0 - Normal
                    if (pDoc.RegimeIva == "0")
                    {
                        vCodIva = "";
                        vTaxaIva = 0;
                        vTipoOperacao = Constants.vbNullString;
                    }
                    else if (pDoc.RegimeIva == "3")
                    {
                        vCodIva = PriV100Api.BSO.Base.Parametros.DaValorAtributo("IvaIntracom");
                        vTaxaIva = PriV100Api.BSO.Base.Iva.DaValorAtributo(vCodIva, "Taxa");
                        vTipoOperacao = "1";
                    }
                }
                else if (pDoc.EspacoFiscal == 3)
                {

                    // 0 - Normal
                    if (pDoc.RegimeIva == "0")
                    {
                        vCodIva = "";
                        vTaxaIva = 0;
                        vTipoOperacao = Constants.vbNullString;
                    }
                    else if (pDoc.RegimeIva == "4")
                    {
                        vCodIva = PriV100Api.BSO.Base.Parametros.DaValorAtributo("IvaExterno");
                        vTaxaIva = PriV100Api.BSO.Base.Iva.DaValorAtributo(vCodIva, "Taxa");
                        vTipoOperacao = Constants.vbNullString;
                    }
                }
            }

            for (i = 1; i <= pDoc.Linhas.NumItens; i++)
            {
                if (pDoc.Linhas.GetEdita(i).Artigo + "" != "")
                {
                    if (vCodIva == "")
                    {
                        pDoc.Linhas.GetEdita(i).CodIva = PriV100Api.BSO.Base.Artigos.DaValorAtributo(pDoc.Linhas.GetEdita(i).Artigo, "Iva");
                        pDoc.Linhas.GetEdita(i).TaxaIva = PriV100Api.BSO.Base.Iva.DaValorAtributo(pDoc.Linhas.GetEdita(i).CodIva, "Taxa");
                    }
                    else
                    {
                        pDoc.Linhas.GetEdita(i).CodIva = vCodIva;
                        pDoc.Linhas.GetEdita(i).TaxaIva = float.Parse(vTaxaIva.ToString());
                    }
                    if (vTipoOperacao == "1")
                    {
                        if (pDoc.Linhas.GetEdita(i).TipoLinha == "20" | pDoc.Linhas.GetEdita(i).TipoLinha == "21" | pDoc.Linhas.GetEdita(i).TipoLinha == "22")
                            pDoc.Linhas.GetEdita(i).TipoOperacao = "5";
                        else
                            pDoc.Linhas.GetEdita(i).TipoOperacao = "1";
                    }
                    else
                        pDoc.Linhas.GetEdita(i).TipoOperacao = Constants.vbNullString;
                }
            }
        }

        public static bool ValidaCopiaLinhas(string ModuloOrigem, VndBEDocumentoVenda ObjectoOrigem, string ModuloDestino, VndBEDocumentoVenda ObjectoDestino)
        {
            if (ModuloOrigem == "V" & ModuloDestino == "V")
            {
                if (ObjectoOrigem.Tipodoc != "EMB" | ObjectoDestino.Tipodoc != "PF")
                    return false;
            }
            else
                return false;

            return true;
        }

        public void InsereComissoes()
        {
            if (DocumentoVenda.Tipodoc == Enums.TiposDocumento.Proforma && DocumentoVenda.Linhas.NumItens > 0)
            {

                // ciclo para calcular sum de prec.liquido / Total

                var Valorliq = 0;

                for (int Linha = 1; Linha <= DocumentoVenda.Linhas.NumItens; Linha++)
                {
                    if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                    {
                        Valorliq = Convert.ToInt32(DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit * DocumentoVenda.Linhas.GetEdita(Linha).Quantidade);
                        DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorLiquidoOrig"].Valor = Valorliq;
                    }
                }


                // consideraValorFOB
                bool consideraValorFOB = new bool();
                if (Convert.IsDBNull(PriV100Api.BSO.DSO.DaValorUnico("Select TOP 1 CDU_ConsideraValorFOB From Clientes Where Cliente = '" + DocumentoVenda.Entidade + "' ")))
                    consideraValorFOB = false;
                else
                    consideraValorFOB = bool.Parse(PriV100Api.BSO.DSO.DaValorUnico("Select TOP 1 CDU_ConsideraValorFOB From Clientes Where Cliente = '" + DocumentoVenda.Entidade + "' ").ToString());


                for (int Linha = 1; Linha <= DocumentoVenda.Linhas.NumItens; Linha++)
                {
                    if (DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString() != "FOB" & consideraValorFOB == true & Convert.ToInt32(DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorFOB"].Valor) != 0)
                    {
                        // nova transf aqui
                        if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                        {
                            var PrecUnit = DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit;
                            var Quantidade = DocumentoVenda.Linhas.GetEdita(Linha).Quantidade;
                            var Comissao = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_Comissao"].Valor;
                            var ValorFOB = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorFOB"].Valor;
                            var NFatura = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor;

                            // calcular sumvalorliq
                            var Sumvalorliq = 0;
                            for (int Linhas = 1; Linhas <= DocumentoVenda.Linhas.NumItens; Linhas++)
                            {
                                if (DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString() != "FOB" & consideraValorFOB == true & Convert.ToInt32(DocumentoVenda.Linhas.GetEdita(Linhas).CamposUtil["CDU_ValorFOB"].Valor) != 0)
                                {
                                    if (DocumentoVenda.Linhas.GetEdita(Linhas).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linhas).TipoLinha != "65")
                                    {
                                        if (NFatura == DocumentoVenda.Linhas.GetEdita(Linhas).CamposUtil["CDU_NFatura"].Valor)
                                            Sumvalorliq = Sumvalorliq + Convert.ToInt32(DocumentoVenda.Linhas.GetEdita(Linhas).CamposUtil["CDU_ValorLiquidoOrig"].Valor);
                                    }
                                }
                            }

                            double NovoPrecUnit = 0;
                            if (Quantidade <= 0 | Sumvalorliq <= 0)
                            {
                                NovoPrecUnit = 0;
                                MessageBox.Show("valor liquido ou Quantidade na Linha " + Linha + " não pode ser 0 ou negativo, PrecUnit foi colocado a 0", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                                NovoPrecUnit = Convert.ToDouble(((PrecUnit * Quantidade) / Sumvalorliq) * Convert.ToDouble(ValorFOB) * ((Convert.ToDouble(Comissao) / 100) / Quantidade));

                            DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit = NovoPrecUnit;
                            DocumentoVenda.Linhas.GetEdita(Linha).Descricao = "Comission Against Invoice - " + NFatura + "";
                        }
                    }
                    else if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                    {
                        var PrecUnit = DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit;
                        var Quantidade = DocumentoVenda.Linhas.GetEdita(Linha).Quantidade;
                        var Comissao = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_Comissao"].Valor;

                        double NovoPrecUnit = (PrecUnit * Quantidade) * (Convert.ToDouble(Comissao) / 100);

                        var NFatura = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor;
                        var Agente = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_Agente"].Valor;
                        var Vendedor = DocumentoVenda.Linhas.GetEdita(Linha).Vendedor;
                        var ComissaoVendedor = DocumentoVenda.Linhas.GetEdita(Linha).Comissao;

                        if (Quantidade <= 0)
                            DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit = 0;
                        else
                            DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit = NovoPrecUnit / Quantidade;

                        DocumentoVenda.Linhas.GetEdita(Linha).Descricao = "Comission Against Invoice - " + NFatura + "";
                    }
                }
            }
        }

        public void calculoTotal(double ValorTotal, string NFatura)
        {
            if (DocumentoVenda.Tipodoc == Enums.TiposDocumento.Proforma | DocumentoVenda.Tipodoc == Enums.TiposDocumento.Fatura | DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaO | DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaI)
            {

                // calculo do valor total para a fatura escolhida
                var total = 0;
                for (int Linha = 1; Linha <= DocumentoVenda.Linhas.NumItens; Linha++)
                {
                    if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                    {
                        if (DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor.ToString() == NFatura)
                            total = total + Convert.ToInt32(DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorLiquidoOrig"].Valor);
                    }
                }



                for (int Linha = 1; Linha <= DocumentoVenda.Linhas.NumItens; Linha++)
                {
                    if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                    {
                        if (DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor.ToString() == NFatura)
                        {
                            var PrecUnit = DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit;
                            var Quantidade = DocumentoVenda.Linhas.GetEdita(Linha).Quantidade;
                            var Comissao = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_Comissao"].Valor;

                            if (Quantidade <= 0 | Convert.ToDouble(Comissao) <= 0)
                            {
                                // Me.DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit = 0
                                double NovoPrecUnit = 0;
                                MessageBox.Show("Comissão ou Quantidade na Linha " + Linha + " não pode ser 0 ou negativa, PrecUnit foi colocado a 0", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                var NovoPrecUnit = ((((PrecUnit * Quantidade) / (Convert.ToDouble(Comissao) / 100)) / Convert.ToDouble(total) * ValorTotal) * ((Convert.ToDouble(Comissao) / 100) / Quantidade));
                                // Me.DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit = NovoPrecUnit

                                // modificar o valor liquido original da linha de acordo com o novo total
                                foreach (LinhasDocRow NumLinha in LinhasDoc)
                                {
                                    if (NumLinha.NumLinha == Linha)
                                    {
                                        NumLinha.PrecUnit = NovoPrecUnit;
                                        NumLinha.CDU_ValorLiquidoOrig = (ValorTotal * ((PrecUnit / (Convert.ToDouble(Comissao) / 100)) * Quantidade)) / total;
                                    }
                                }
                            }
                        }
                    }
                }

                AtualizaTabelaTotalLinhasTotal(ValorTotal, NFatura);
            }
        }
        public void AtualizaTabelaTotalLinhas()
        {
            if (DocumentoVenda.Tipodoc == Enums.TiposDocumento.Proforma | DocumentoVenda.Tipodoc == Enums.TiposDocumento.Fatura | DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaO | DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaI)
            {
                TotalLinhas.Clear();

                ArrayList lstTotalLinhas = new ArrayList();

                for (int Linha = 1; Linha <= DocumentoVenda.Linhas.NumItens; Linha++)
                {
                    if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                    {
                        if (DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor.ToString() != "")
                        {
                            var NFatura = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor;
                            var FOB = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorFOB"].Valor;
                            var ValorLiq = DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit * DocumentoVenda.Linhas.GetEdita(Linha).Quantidade;
                            var TotalEmbarque = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorLiquidoOrig"].Valor;

                            if (lstTotalLinhas.Contains(NFatura))
                            {
                                if (Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 1]) < Convert.ToDouble(FOB))
                                    lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 1] = Convert.ToInt32(FOB);
                                lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 2] = Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 2]) + ValorLiq;
                                lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 3] = Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 3]) + Convert.ToInt32(TotalEmbarque);
                            }
                            else
                            {
                                lstTotalLinhas.Add(NFatura);
                                lstTotalLinhas.Add(FOB);
                                lstTotalLinhas.Add(ValorLiq);
                                lstTotalLinhas.Add(TotalEmbarque);
                            }
                        }
                    }
                }

                double total = 0;
                for (int fa = 0; fa <= lstTotalLinhas.Count - 1; fa += 4)
                {
                    TotalLinhasRow TotalLinhasReg = (TotalLinhasRow)TotalLinhas.NewRow();

                    TotalLinhasReg.NFatura = lstTotalLinhas[fa].ToString();
                    TotalLinhasReg.ValorFOB = float.Parse(lstTotalLinhas[fa + 1].ToString());
                    TotalLinhasReg.Total = float.Parse(lstTotalLinhas[fa + 2].ToString());
                    total = total + Double.Parse(lstTotalLinhas[fa + 2].ToString());
                    TotalLinhasReg.TotalEmb = float.Parse(lstTotalLinhas[fa + 3].ToString());
                    TotalLinhas.Rows.Add(TotalLinhasReg);
                }
            }
            else
            {
                TotalLinhas.Clear();

                ArrayList lstTotalLinhas = new ArrayList();

                for (int Linha = 1; Linha <= DocumentoVenda.Linhas.NumItens; Linha++)
                {
                    if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                    {
                        if (DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor.ToString() != "")
                        {
                            var NFatura = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor;
                            var FOB = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorFOB"].Valor;
                            var ValorLiq = DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit * DocumentoVenda.Linhas.GetEdita(Linha).Quantidade;

                            if (lstTotalLinhas.Contains(NFatura))
                            {
                                if (Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 1]) < Convert.ToDouble(FOB))
                                    lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 1] = FOB;
                                lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 2] = Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 2]) + ValorLiq;
                            }
                            else
                            {
                                lstTotalLinhas.Add(NFatura);
                                lstTotalLinhas.Add(FOB);
                                lstTotalLinhas.Add(ValorLiq);
                            }
                        }
                    }
                }


                double total = 0;
                for (int fa = 0; fa <= lstTotalLinhas.Count - 1; fa += 4)
                {
                    TotalLinhasRow TotalLinhasReg = (TotalLinhasRow)TotalLinhas.NewRow();

                    TotalLinhasReg.NFatura = lstTotalLinhas[fa].ToString();
                    TotalLinhasReg.ValorFOB = float.Parse(lstTotalLinhas[fa + 1].ToString());
                    TotalLinhasReg.Total = float.Parse(lstTotalLinhas[fa + 2].ToString());
                    total = total + Double.Parse(lstTotalLinhas[fa + 2].ToString());
                    TotalLinhas.Rows.Add(TotalLinhasReg);
                }
            }
        }

        public void AtualizaTabelaTotalLinhasMudaFatura(string NFaturaNova, double NumLinha)
        {
            TotalLinhas.Clear();
            ArrayList lstTotalLinhas = new ArrayList();

            for (int Linha = 1; Linha <= DocumentoVenda.Linhas.NumItens; Linha++)
            {
                if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                {
                    if (DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor.ToString() != "")
                    {
                        string NumFatura;
                        if (NumLinha == Linha)
                            NumFatura = NFaturaNova;
                        else
                            NumFatura = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor.ToString();

                        var FOB = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorFOB"].Valor;
                        var ValorLiq = DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit * DocumentoVenda.Linhas.GetEdita(Linha).Quantidade;

                        if (lstTotalLinhas.Contains(NumFatura))
                        {
                            if (Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 1]) < Convert.ToDouble(FOB))
                                lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 1] = FOB;
                            lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 2] = Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 2]) + ValorLiq;
                        }
                        else
                        {
                            lstTotalLinhas.Add(NumFatura);
                            lstTotalLinhas.Add(FOB);
                            lstTotalLinhas.Add(ValorLiq);
                        }
                    }
                }
            }

            double total = 0;
            for (int fa = 0; fa <= lstTotalLinhas.Count - 1; fa += 3)
            {
                TotalLinhasRow TotalLinhasReg = (TotalLinhasRow)TotalLinhas.NewRow();

                TotalLinhasReg.NFatura = lstTotalLinhas[fa].ToString();
                TotalLinhasReg.ValorFOB = float.Parse(lstTotalLinhas[fa + 1].ToString());
                TotalLinhasReg.Total = float.Parse(lstTotalLinhas[fa + 2].ToString());
                total = total + Double.Parse(lstTotalLinhas[fa + 2].ToString());
                TotalLinhas.Rows.Add(TotalLinhasReg);
            }
        }

        public void AtualizaTabelaTotalLinhasMudaFOB(double ValorFOB, double NumLinha)
        {
            TotalLinhas.Clear();
            ArrayList lstTotalLinhas = new ArrayList();

            for (int Linha = 1; Linha <= DocumentoVenda.Linhas.NumItens; Linha++)
            {
                if (DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "60" & DocumentoVenda.Linhas.GetEdita(Linha).TipoLinha != "65")
                {
                    if (DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor.ToString() != "")
                    {
                        var NumFatura = DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_NFatura"].Valor;
                        double FOB;
                        if (NumLinha == Linha)
                            FOB = ValorFOB;
                        else
                            FOB = Convert.ToDouble(DocumentoVenda.Linhas.GetEdita(Linha).CamposUtil["CDU_ValorFOB"].Valor);
                        var ValorLiq = DocumentoVenda.Linhas.GetEdita(Linha).PrecUnit * DocumentoVenda.Linhas.GetEdita(Linha).Quantidade;

                        if (lstTotalLinhas.Contains(NumFatura))
                        {
                            if (Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 1]) < Convert.ToDouble(FOB))
                                lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 1] = FOB;
                            lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 2] = Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 2]) + ValorLiq;
                        }
                        else
                        {
                            lstTotalLinhas.Add(NumFatura);
                            lstTotalLinhas.Add(FOB);
                            lstTotalLinhas.Add(ValorLiq);
                        }
                    }
                }
            }

            double total = 0;
            for (int fa = 0; fa <= lstTotalLinhas.Count - 1; fa += 3)
            {
                TotalLinhasRow TotalLinhasReg = (TotalLinhasRow)TotalLinhas.NewRow();

                TotalLinhasReg.NFatura = lstTotalLinhas[fa].ToString();
                TotalLinhasReg.ValorFOB = float.Parse(lstTotalLinhas[fa + 1].ToString());
                TotalLinhasReg.Total = float.Parse(lstTotalLinhas[fa + 2].ToString());
                total = total + Double.Parse(lstTotalLinhas[fa + 2].ToString());
                TotalLinhas.Rows.Add(TotalLinhasReg);
            }
        }

        public void AtualizaTabelaTotalLinhasTotal(double TotalInserido, string NFatura)
        {
            if (DocumentoVenda.Tipodoc == Enums.TiposDocumento.Proforma | DocumentoVenda.Tipodoc == Enums.TiposDocumento.Fatura | DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaO | DocumentoVenda.Tipodoc == Enums.TiposDocumento.FaturaI)
            {
                ArrayList lstTotalLinhas = new ArrayList();

                foreach (LinhasDocRow Linha in LinhasDoc)
                {
                    if (Linha.CDU_NFatura != "")
                    {
                        var NumFatura = Linha.CDU_NFatura;
                        var Fob = Linha.CDU_ValorFOB;
                        var ValorLiq = Linha.PrecUnit * Linha.Quantidade;
                        var TotalEmbarque = TotalInserido;

                        if (lstTotalLinhas.Contains(NumFatura))
                        {
                            if (Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 1]) < Convert.ToDouble(Fob))
                                lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 1] = Fob;
                            lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 2] = Convert.ToDouble(lstTotalLinhas[lstTotalLinhas.IndexOf(NumFatura) + 2]) + ValorLiq;
                        }
                        else
                        {
                            lstTotalLinhas.Add(NumFatura);
                            lstTotalLinhas.Add(Fob);
                            lstTotalLinhas.Add(ValorLiq);
                            lstTotalLinhas.Add(TotalEmbarque);
                        }
                    }
                }

                foreach (TotalLinhasRow Linha in TotalLinhas)
                {
                    if (Linha.NFatura == NFatura)
                    {
                        Linha.Total = float.Parse(lstTotalLinhas[lstTotalLinhas.IndexOf(NFatura) + 2].ToString());
                        Linha.TotalEmb = float.Parse(TotalInserido.ToString());
                    }
                }
            }
        }

        /// <summary>

        ///     ''' Atualiza os datatables CabecDoc e LinhasDoc com os dados recebidos do EditorVendas via SetContexto.

        ///     ''' </summary>

        ///     ''' <remarks></remarks>
        public void AtualizaCabecDoc_LinhasDoc()
        {
            try
            {

                // Limpa Tabelas em memoria
                CabecDoc.Clear();
                LinhasDoc.Clear();

                // Atualiza CabecDoc com os dados do DocumentoVenda

                CabecDocRow CabecReg = (CabecDocRow)CabecDoc.NewRow();


                CabecReg.TipoDoc = DocumentoVenda.Tipodoc;
                CabecReg.Serie = DocumentoVenda.Serie;
                if (DocumentoVenda.Referencia == null | Convert.IsDBNull(DocumentoVenda.Referencia))
                    CabecReg.NumDoc = "";
                else
                    CabecReg.NumDoc = DocumentoVenda.Referencia;

                // CabecReg.CDU_Data = Me.DocumentoVenda.DataDoc
                CabecReg.Entidade = DocumentoVenda.Entidade;
                CabecReg.EntidadeNome = PriV100Api.BSO.Base.Clientes.DaValorAtributo(DocumentoVenda.Entidade, "Nome");
                CabecReg.Id = DocumentoVenda.ID;

                CarregaFornecedores();
                CarregaBancos();
                CarregaIntrastatCondEntrega();
                CarregaAgentes();
                CarregaCompanhiasMaritimas();
                CarregaVendedores();
                CarregaLocais();


                if (DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor))
                    CabecReg.CDU_Fornecedor = "";
                else
                    CabecReg.CDU_Fornecedor = DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString();


                if (CabecReg.CDU_Fornecedor != "")
                {
                    FornecedoresRow Fornecedor = Fornecedores.FindByCodigo(CabecReg.CDU_Fornecedor);
                    if (Fornecedor != null)
                        CabecReg.CDU_FornecedorNome = Fornecedor.Descricao;
                    else
                        CabecReg.CDU_FornecedorNome = "";
                }
                else
                    CabecReg.CDU_FornecedorNome = "";

                if (DocumentoVenda.CamposUtil["CDU_Agente"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Agente"].Valor))
                    CabecReg.CDU_Agente = "";
                else
                    CabecReg.CDU_Agente = DocumentoVenda.CamposUtil["CDU_Agente"].Valor.ToString();

                if (CabecReg.CDU_Agente != "")
                {
                    TDU_AgentesRow Agente = TDU_Agentes.FindByCodigo(CabecReg.CDU_Agente);
                    if (Agente != null)
                        CabecReg.CDU_AgenteNome = Agente.Descricao;
                    else
                        CabecReg.CDU_AgenteNome = "";
                }
                else
                    CabecReg.CDU_AgenteNome = "";


                if (DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor))
                    CabecReg.CDU_Incoterms = "";
                else
                    CabecReg.CDU_Incoterms = DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString();


                if (CabecReg.CDU_Incoterms != "")
                {
                    IntrastatCondEntregaRow Incoterms = IntrastatCondEntrega.FindByCodigo(CabecReg.CDU_Incoterms);
                    if (Incoterms != null)
                        CabecReg.CDU_IncotermsDesc = Incoterms.Descricao;
                    else
                        CabecReg.CDU_IncotermsDesc = "";
                }
                else
                    CabecReg.CDU_IncotermsDesc = "";


                if (Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_NCartaCredito"].Valor) | DocumentoVenda.CamposUtil["CDU_NCartaCredito"].Valor == null)
                    CabecReg.CDU_NCartaCredito = "";
                else
                    CabecReg.CDU_NCartaCredito = DocumentoVenda.CamposUtil["CDU_NCartaCredito"].Valor.ToString();


                if (DocumentoVenda.CamposUtil["CDU_DataLimiteEmissaoCC"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_DataLimiteEmissaoCC"].Valor))
                    CabecReg.CDU_DataLimiteEmissaoCC = DateTime.Parse("1999-01-01");
                else
                    CabecReg.CDU_DataLimiteEmissaoCC = DateTime.Parse(DocumentoVenda.CamposUtil["CDU_DataLimiteEmissaoCC"].Valor.ToString());

                if (DocumentoVenda.CamposUtil["CDU_DataPrevistaChegada"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_DataPrevistaChegada"].Valor))
                    CabecReg.CDU_DataPrevistaChegada = DateTime.Parse("1999-01-01");
                else
                    CabecReg.CDU_DataPrevistaChegada = DateTime.Parse(DocumentoVenda.CamposUtil["CDU_DataPrevistaChegada"].Valor.ToString());


                if (Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor) | DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor == null)
                    CabecReg.CDU_CompanhiaMaritima = "";
                else
                    CabecReg.CDU_CompanhiaMaritima = DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor.ToString();


                if (CabecReg.CDU_CompanhiaMaritima != "")
                {
                    TDU_CompanhiasMaritimasRow CompanhiaMaritima = TDU_CompanhiasMaritimas.FindByCodigo(CabecReg.CDU_CompanhiaMaritima);
                    if (CompanhiaMaritima != null)
                        CabecReg.CDU_CompanhiaMaritimaNome = CompanhiaMaritima.Descricao;
                    else
                        CabecReg.CDU_CompanhiaMaritimaNome = "";
                }
                else
                    CabecReg.CDU_CompanhiaMaritimaNome = "";

                if (Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Navio"].Valor) | DocumentoVenda.CamposUtil["CDU_Navio"].Valor == null)
                    CabecReg.CDU_Navio = "";
                else
                    CabecReg.CDU_Navio = DocumentoVenda.CamposUtil["CDU_Navio"].Valor.ToString();


                if (DocumentoVenda.CamposUtil["CDU_DataComunicacaoCliente"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_DataComunicacaoCliente"].Valor))
                    CabecReg.CDU_DataComunicacaoCliente = DateTime.Parse("1999-01-01");
                else
                    CabecReg.CDU_DataComunicacaoCliente = DateTime.Parse(DocumentoVenda.CamposUtil["CDU_DataComunicacaoCliente"].Valor.ToString());

                if (DocumentoVenda.CamposUtil["CDU_Banco"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Banco"].Valor))
                    CabecReg.CDU_Banco = "";
                else
                    CabecReg.CDU_Banco = DocumentoVenda.CamposUtil["CDU_Banco"].Valor.ToString();


                if (CabecReg.CDU_Banco != "")
                {


                    TDU_BancosRow Banco = TDU_Bancos.FindByCodigo(DocumentoVenda.CamposUtil["CDU_Banco"].Valor.ToString());
                    if (Banco != null)
                        CabecReg.CDU_BancoNome = Banco.Descricao;
                    else
                        CabecReg.SetCDU_BancoNomeNull();
                }
                else
                    CabecReg.SetCDU_BancoNomeNull();


                if (DocumentoVenda.CamposUtil["CDU_Comissao"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Comissao"].Valor))
                    CabecReg.CDU_Comissao = 0;
                else
                    CabecReg.CDU_Comissao = float.Parse(DocumentoVenda.CamposUtil["CDU_Comissao"].Valor.ToString());


                if (DocumentoVenda.CamposUtil["CDU_NBL"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_NBL"].Valor))
                    CabecReg.CDU_NBL = "";
                else
                    CabecReg.CDU_NBL = DocumentoVenda.CamposUtil["CDU_NBL"].Valor.ToString();


                if (DocumentoVenda.CamposUtil["CDU_CustoFrete"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_CustoFrete"].Valor))
                    CabecReg.CDU_CustoFrete = 0;
                else
                    CabecReg.CDU_CustoFrete = float.Parse(DocumentoVenda.CamposUtil["CDU_CustoFrete"].Valor.ToString());

                if (DocumentoVenda.CamposUtil["CDU_Porto"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Porto"].Valor))
                    CabecReg.CDU_Porto = "";
                else
                    CabecReg.CDU_Porto = DocumentoVenda.CamposUtil["CDU_Porto"].Valor.ToString();


                if (DocumentoVenda.CamposUtil["CDU_Porto"].Valor != null && DocumentoVenda.CamposUtil["CDU_Porto"].Valor.ToString() != "")
                {
                    TDU_LocaisRow Porto = TDU_Locais.FindByCodigo(CabecReg.CDU_Porto);
                    if (Porto != null)
                        CabecReg.DescricaoPorto = Porto.Descricao;
                    else
                        CabecReg.DescricaoPorto = "";
                }
                else
                    CabecReg.DescricaoPorto = "";

                if (DocumentoVenda.CamposUtil["CDU_Destino"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Destino"].Valor))
                    CabecReg.CDU_Destino = "";
                else
                    CabecReg.CDU_Destino = DocumentoVenda.CamposUtil["CDU_Destino"].Valor.ToString();


                if (DocumentoVenda.CamposUtil["CDU_Destino"].Valor != null && DocumentoVenda.CamposUtil["CDU_Destino"].Valor.ToString() != "")
                {
                    TDU_LocaisRow Destino = TDU_Locais.FindByCodigo(CabecReg.CDU_Destino);
                    if (Destino != null)
                        CabecReg.DescricaoDestino = Destino.Descricao;
                    else
                        CabecReg.DescricaoDestino = "";
                }
                else
                    CabecReg.DescricaoDestino = "";

                if (DocumentoVenda.CamposUtil["CDU_Localidade"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Localidade"].Valor))
                    CabecReg.CDU_Localidade = "";
                else
                    CabecReg.CDU_Localidade = DocumentoVenda.CamposUtil["CDU_Localidade"].Valor.ToString();


                if (DocumentoVenda.CamposUtil["CDU_Localidade"].Valor != null && DocumentoVenda.CamposUtil["CDU_Localidade"].Valor.ToString() != "")
                {
                    TDU_LocaisRow Regiao = TDU_Locais.FindByCodigo(CabecReg.CDU_Localidade);
                    if (Regiao != null)
                        CabecReg.DescricaoLocalidade = Regiao.Descricao;
                    else
                        CabecReg.DescricaoLocalidade = "";
                }
                else
                    CabecReg.DescricaoLocalidade = "";

                if (Convert.IsDBNull(PriV100Api.BSO.DSO.DaValorUnico("Select TOP 1 CDU_ConsideraValorFOB From Clientes Where Cliente = '" + DocumentoVenda.Entidade + "' ")))
                    CabecReg.CDU_ConsideraValorFOB = false;
                else
                    CabecReg.CDU_ConsideraValorFOB = Convert.ToBoolean(PriV100Api.BSO.DSO.DaValorUnico("Select TOP 1 CDU_ConsideraValorFOB From Clientes Where Cliente = '" + DocumentoVenda.Entidade + "' "));

                if (DocumentoVenda.CamposUtil["CDU_DataEmissaoCC"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_DataEmissaoCC"].Valor))
                    CabecReg.CDU_DataEmissaoCC = DateTime.Parse("1999-01-01");
                else
                    CabecReg.CDU_DataEmissaoCC = DateTime.Parse(DocumentoVenda.CamposUtil["CDU_DataEmissaoCC"].Valor.ToString());



                if (Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor) | DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor == null)
                    CabecReg.CDU_Vendedor = "";
                else
                    CabecReg.CDU_Vendedor = DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor.ToString();

                if (CabecReg.CDU_Vendedor != "")
                {
                    VendedoresRow Vendedor = Vendedores.FindByCodigo(CabecReg.CDU_Vendedor);

                    if (Vendedor != null)
                        CabecReg.VendedorNome = Vendedor.Descricao;
                    else
                        CabecReg.SetVendedorNomeNull();
                }
                else
                    CabecReg.SetVendedorNomeNull();

                if (Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor))
                    CabecReg.CDU_ComissaoVendedor = 0;
                else
                    CabecReg.CDU_ComissaoVendedor = Convert.ToDouble(DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor);

                // Codigo de sugestão de vendedor

                // If CabecReg.CDU_Vendedor = "" Then
                // If Me.DocumentoVenda.CamposUtil("CDU_Fornecedor"].Valor = "" Then
                // CabecReg.CDU_Vendedor = ""
                // CabecReg.VendedorNome = ""
                // CabecReg.CDU_ComissaoVendedor = 0
                // Else

                // Dim DTVendedor As New DataTable
                // ClienteSql.ExecutarDataTable("SELECT F.CDU_Vendedor, ISNULL(V.Nome, '') AS VendedorNome, ISNULL(V.Comissao,0) AS VendedorComissao, ISNULL(F.CDU_UsaComissaoVendedor, 1) AS CDU_UsaComissaoVendedor, F.CDU_Comissao " & _
                // "FROM Fornecedores F " & _
                // "LEFT JOIN Vendedores V ON V.Vendedor = F.CDU_Vendedor " & _
                // "WHERE F.Fornecedor = '" & Me.DocumentoVenda.CamposUtil("CDU_Fornecedor"].Valor & "' ", DTVendedor)

                // CabecReg.CDU_Vendedor = DTVendedor.Rows(0)("CDU_Vendedor").ToString()
                // CabecReg.VendedorNome = DTVendedor.Rows(0)("VendedorNome").ToString()

                // If DTVendedor.Rows(0)("CDU_UsaComissaoVendedor") = 0 Then
                // CabecReg.CDU_ComissaoVendedor = DTVendedor.Rows(0)("CDU_Comissao")
                // Else
                // CabecReg.CDU_ComissaoVendedor = DTVendedor.Rows(0)("VendedorComissao")
                // End If

                // End If
                // End If


                CabecDoc.Rows.Add(CabecReg);


                for (int NLinha = 1; NLinha <= DocumentoVenda.Linhas.NumItens; NLinha++)
                {
                    LinhasDocRow LinhaReg = (LinhasDocRow)LinhasDoc.NewRow();

                    {
                        var DocVenda = DocumentoVenda.Linhas.GetEdita(NLinha);
                        CarregaCertificados();
                        CarregaVendedores();
                        CarregaEstadoPagamento();

                        CarregaSituacao();
                        CarregaTipoQualidade();
                        CarregaPais();
                        CarregaParques();

                        LinhaReg.Id = DocVenda.IdLinha;
                        if (DocVenda.Artigo == null | Convert.IsDBNull(DocVenda.Artigo))
                            LinhaReg.Artigo = "";
                        else
                            LinhaReg.Artigo = DocVenda.Artigo;

                        LinhaReg.Descricao = DocVenda.Descricao;

                        LinhaReg.IdCabecDoc = DocumentoVenda.ID;
                        LinhaReg.IdLinhaOriginal = DocVenda.IDLinhaOriginal;
                        LinhaReg.NumLinha = short.Parse(NLinha.ToString());

                        LinhaReg.TipoLinha = DocVenda.TipoLinha;
                        LinhaReg.Quantidade = DocVenda.Quantidade;
                        LinhaReg.PrecUnit = DocVenda.PrecUnit;

                        LinhaReg.DataEntrega = DocVenda.DataEntrega;
                        LinhaReg.Unidade = DocVenda.Unidade;

                        if (DocumentoVenda.CondPag == "")

                            // MsgBox("Condição de Pagamento não definida", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                            LinhaReg.CDU_CondPag = "";
                        else
                        {
                            LinhaReg.CDU_CondPag = DocumentoVenda.CondPag;

                            DataTable DTCondPag = new DataTable();
                            DTCondPag = PriV100Api.BSO.DSO.ConsultaDataTable("SELECT Descricao FROM CondPag WHERE CondPag= '" + LinhaReg.CDU_CondPag + "' ");
                            LinhaReg.CDU_CondPagDesc = DTCondPag.Rows[0][0].ToString();
                        }

                        if (Convert.IsDBNull(DocVenda.Vendedor))
                            LinhaReg.Vendedor = "";
                        else
                            LinhaReg.Vendedor = DocVenda.Vendedor;


                        if (DocVenda.Vendedor != "")
                        {
                            VendedoresRow Vendedor = Vendedores.FindByCodigo(DocVenda.Vendedor);

                            if (Vendedor != null)
                                LinhaReg.VendedorNome = Vendedor.Descricao;
                            else
                                LinhaReg.SetVendedorNomeNull();
                        }
                        else
                            LinhaReg.SetVendedorNomeNull();


                        if (Convert.IsDBNull(DocVenda.Comissao))
                            LinhaReg.ComissaoVendedor = 0;
                        else
                            LinhaReg.ComissaoVendedor = DocVenda.Comissao;

                        // Codigo de sugestão de vendedor

                        // If LinhaReg.Artigo = "" Then
                        // LinhaReg.CDU_CondPag = 0
                        // LinhaReg.CDU_CondPagDesc = ""
                        // LinhaReg.Vendedor = ""
                        // LinhaReg.VendedorNome = ""
                        // LinhaReg.ComissaoVendedor = 0

                        // Else

                        // If LinhaReg.Vendedor = "" Then
                        // If Me.DocumentoVenda.CamposUtil("CDU_Fornecedor"].Valor = "" Then
                        // LinhaReg.Vendedor = ""
                        // LinhaReg.VendedorNome = ""
                        // LinhaReg.ComissaoVendedor = 0
                        // Else

                        // Dim DTVendedor As New DataTable
                        // ClienteSql.ExecutarDataTable("SELECT F.CDU_Vendedor, ISNULL(V.Nome, '') AS VendedorNome, ISNULL(V.Comissao,0) AS VendedorComissao, ISNULL(F.CDU_UsaComissaoVendedor, 1) AS CDU_UsaComissaoVendedor, F.CDU_Comissao " & _
                        // "FROM Fornecedores F " & _
                        // "LEFT JOIN Vendedores V ON V.Vendedor = F.CDU_Vendedor " & _
                        // "WHERE F.Fornecedor = '" & Me.DocumentoVenda.CamposUtil("CDU_Fornecedor"].Valor & "' ", DTVendedor)

                        // LinhaReg.Vendedor = DTVendedor.Rows(0)("CDU_Vendedor").ToString()
                        // LinhaReg.VendedorNome = DTVendedor.Rows(0)("VendedorNome").ToString()
                        // If LinhaReg.ComissaoVendedor = 0 Or IsDBNull(LinhaReg.ComissaoVendedor) Then
                        // If DTVendedor.Rows(0)("CDU_UsaComissaoVendedor") = 0 Then
                        // LinhaReg.ComissaoVendedor = DTVendedor.Rows(0)("CDU_Comissao")
                        // Else
                        // LinhaReg.ComissaoVendedor = DTVendedor.Rows(0)("VendedorComissao")
                        // End If
                        // Else
                        // LinhaReg.ComissaoVendedor = .Comissao
                        // End If

                        // End If
                        // End If
                        // End If

                        if (DocumentoVenda.CamposUtil["CDU_Agente"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Agente"].Valor) | DocVenda.CamposUtil["CDU_Agente"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_Agente"].Valor))
                        {
                            LinhaReg.CDU_Agente = "";
                            LinhaReg.AgenteNome = "";
                        }
                        else
                        {
                            LinhaReg.CDU_Agente = DocVenda.CamposUtil["CDU_Agente"].Valor.ToString();
                            CarregaAgentes();
                            TDU_AgentesRow Agente = TDU_Agentes.FindByCodigo(LinhaReg.CDU_Agente);
                            if (Agente != null)
                                LinhaReg.AgenteNome = Agente.Descricao;
                            else
                                LinhaReg.AgenteNome = "";
                        }

                        if (DocVenda.CamposUtil["CDU_DataPrevistaEmbarque"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_DataPrevistaEmbarque"].Valor))
                            LinhaReg.CDU_DataPrevistaEmbarque = DateTime.Parse("1999-01-01");
                        else
                            LinhaReg.CDU_DataPrevistaEmbarque = DateTime.Parse(DocVenda.CamposUtil["CDU_DataPrevistaEmbarque"].Valor.ToString());

                        if (DocVenda.CamposUtil["CDU_Observacoes"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_Observacoes"].Valor))
                            LinhaReg.CDU_Observacoes = "";
                        else
                            LinhaReg.CDU_Observacoes = DocVenda.CamposUtil["CDU_Observacoes"].Valor.ToString();

                        if (DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor == null | Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor))
                            LinhaReg.CDU_Incoterms = "";
                        else if (DocVenda.CamposUtil["CDU_Incoterms"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_Incoterms"].Valor))
                            LinhaReg.CDU_Incoterms = DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor.ToString();
                        else
                            LinhaReg.CDU_Incoterms = DocVenda.CamposUtil["CDU_Incoterms"].Valor.ToString();

                        if (DocVenda.CamposUtil["CDU_DPEAlteradaMotivo"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_DPEAlteradaMotivo"].Valor))
                            LinhaReg.CDU_DPEAlteradaMotivo = "";
                        else
                            LinhaReg.CDU_DPEAlteradaMotivo = DocVenda.CamposUtil["CDU_DPEAlteradaMotivo"].Valor.ToString();

                        if (DocVenda.CamposUtil["CDU_EstadoPagamento"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_EstadoPagamento"].Valor))
                            LinhaReg.CDU_EstadoPagamento = "";
                        else
                            LinhaReg.CDU_EstadoPagamento = DocVenda.CamposUtil["CDU_EstadoPagamento"].Valor.ToString();

                        if (DocVenda.CamposUtil["CDU_EstadoPagamento"].Valor.ToString() != "")
                        {
                            TDU_EstadoPagamentoRow EstadoPagamento = TDU_EstadoPagamento.FindByCodigo(LinhaReg.CDU_EstadoPagamento);
                            if (EstadoPagamento != null)
                                LinhaReg.EstadoPagamentoDesc = EstadoPagamento.Descricao;
                            else
                                LinhaReg.SetEstadoPagamentoDescNull();
                        }
                        else
                            LinhaReg.SetEstadoPagamentoDescNull();


                        if (DocVenda.CamposUtil["CDU_Certificado1"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_Certificado1"].Valor))
                            LinhaReg.CDU_Certificado1 = 0;
                        else
                            LinhaReg.CDU_Certificado1 = short.Parse(DocVenda.CamposUtil["CDU_Certificado1"].Valor.ToString());

                        if (DocVenda.CamposUtil["CDU_Certificado2"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_Certificado2"].Valor))
                            LinhaReg.CDU_Certificado2 = 0;
                        else
                            LinhaReg.CDU_Certificado2 = short.Parse(DocVenda.CamposUtil["CDU_Certificado2"].Valor.ToString());


                        if (DocVenda.CamposUtil["CDU_CertificadoTratado1"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_CertificadoTratado1"].Valor))
                            LinhaReg.CDU_CertificadoTratado1 = "";
                        else
                            LinhaReg.CDU_CertificadoTratado1 = DocVenda.CamposUtil["CDU_CertificadoTratado1"].Valor.ToString();

                        if (DocVenda.CamposUtil["CDU_CertificadoTratado1"].Valor.ToString() != "")
                        {
                            TDU_CertificadosRow CertificadoTratado1 = TDU_Certificados.FindByCodigo(LinhaReg.CDU_CertificadoTratado1);
                            if (CertificadoTratado1 != null)
                                LinhaReg.CDU_CertificadoTratado1Desc = CertificadoTratado1.Descricao;
                            else
                                LinhaReg.SetCDU_CertificadoTratado1DescNull();
                        }
                        else
                            LinhaReg.SetCDU_CertificadoTratado1DescNull();

                        if (DocVenda.CamposUtil["CDU_CertificadoTratado2"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_CertificadoTratado2"].Valor))
                            LinhaReg.CDU_CertificadoTratado2 = "";
                        else
                            LinhaReg.CDU_CertificadoTratado2 = DocVenda.CamposUtil["CDU_CertificadoTratado2"].Valor.ToString();

                        if (DocVenda.CamposUtil["CDU_CertificadoTratado2"].Valor is string Certificado)
                        {
                            TDU_CertificadosRow CertificadoTratado2 = TDU_Certificados.FindByCodigo(LinhaReg.CDU_CertificadoTratado2);
                            if (CertificadoTratado2 != null)
                                LinhaReg.CDU_CertificadoTratado2Desc = CertificadoTratado2.Descricao;
                            else
                                LinhaReg.SetCDU_CertificadoTratado2DescNull();
                        }
                        else
                            LinhaReg.SetCDU_CertificadoTratado2DescNull();


                        if (DocVenda.CamposUtil["CDU_PesoLiquido"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_PesoLiquido"].Valor))
                            LinhaReg.CDU_PesoLiquido = 0;
                        else
                            LinhaReg.CDU_PesoLiquido = Convert.ToDouble(DocVenda.CamposUtil["CDU_PesoLiquido"].Valor);

                        if (DocVenda.CamposUtil["CDU_PesoBruto"].Valor == null | Convert.IsDBNull(DocVenda.CamposUtil["CDU_PesoBruto"].Valor))
                            LinhaReg.CDU_PesoBruto = 0;
                        else
                            LinhaReg.CDU_PesoBruto = Convert.ToDouble(DocVenda.CamposUtil["CDU_PesoBruto"].Valor);

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_Comissao"].Valor) | DocumentoVenda.CamposUtil["CDU_Comissao"].Valor == null)
                            LinhaReg.CDU_Comissao = 0;
                        else
                            // LinhaReg.CDU_Comissao = BSO.Comercial.Clientes.DaValorAtributo(Me.DocumentoVenda.Entidade, "CDU_Comissao")
                            LinhaReg.CDU_Comissao = float.Parse(DocVenda.CamposUtil["CDU_Comissao"].Valor.ToString());


                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_ETRCliente"].Valor) | DocVenda.CamposUtil["CDU_ETRCliente"].Valor == null)
                            LinhaReg.CDU_ETRCliente = 0;
                        else
                            LinhaReg.CDU_ETRCliente = short.Parse(DocVenda.CamposUtil["CDU_ETRCliente"].Valor.ToString());

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_ETRFornecedor"].Valor) | DocVenda.CamposUtil["CDU_ETRFornecedor"].Valor == null)
                            LinhaReg.CDU_ETRFornecedor = 0;
                        else
                            LinhaReg.CDU_ETRFornecedor = short.Parse(DocVenda.CamposUtil["CDU_ETRFornecedor"].Valor.ToString());

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_NVolumes"].Valor) | DocVenda.CamposUtil["CDU_NVolumes"].Valor == null)
                            LinhaReg.CDU_NVolumes = "";
                        else
                            LinhaReg.CDU_NVolumes = DocVenda.CamposUtil["CDU_NVolumes"].Valor.ToString();

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_NContentor"].Valor) | DocVenda.CamposUtil["CDU_NContentor"].Valor == null)
                            LinhaReg.CDU_NContentor = "";
                        else
                            LinhaReg.CDU_NContentor = DocVenda.CamposUtil["CDU_NContentor"].Valor.ToString();

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_NFatura"].Valor) | DocVenda.CamposUtil["CDU_NFatura"].Valor == null)
                            LinhaReg.CDU_NFatura = "";
                        else
                            LinhaReg.CDU_NFatura = DocVenda.CamposUtil["CDU_NFatura"].Valor.ToString();

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_LoteFornecedor"].Valor) | DocVenda.CamposUtil["CDU_LoteFornecedor"].Valor == null)
                            LinhaReg.CDU_LoteFornecedor = "";
                        else
                            LinhaReg.CDU_LoteFornecedor = DocVenda.CamposUtil["CDU_LoteFornecedor"].Valor.ToString();

                        if (Convert.IsDBNull(DocVenda.Lote) | DocVenda.Lote == null)
                            LinhaReg.Lote = "";
                        else
                            LinhaReg.Lote = DocVenda.Lote;

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_ValorFOB"].Valor) | DocVenda.CamposUtil["CDU_ValorFOB"].Valor == null)
                            LinhaReg.CDU_ValorFOB = 0;
                        else
                            LinhaReg.CDU_ValorFOB = Convert.ToDouble(DocVenda.CamposUtil["CDU_ValorFOB"].Valor);

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_ComissaoConsiderada"].Valor) | DocVenda.CamposUtil["CDU_ComissaoConsiderada"].Valor == null)
                            LinhaReg.CDU_ComissaoConsiderada = false;
                        else
                            LinhaReg.CDU_ComissaoConsiderada = (bool)DocVenda.CamposUtil["CDU_ComissaoConsiderada"].Valor;

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_ValorLiquidoOrig"].Valor) | DocVenda.CamposUtil["CDU_ValorLiquidoOrig"].Valor == null)
                            LinhaReg.CDU_ValorLiquidoOrig = 0;
                        else
                            LinhaReg.CDU_ValorLiquidoOrig = Convert.ToDouble(DocVenda.CamposUtil["CDU_ValorLiquidoOrig"].Valor);

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_Situacao"].Valor) | DocVenda.CamposUtil["CDU_Situacao"].Valor == null)
                            LinhaReg.CDU_Situacao = "";
                        else
                            LinhaReg.CDU_Situacao = DocVenda.CamposUtil["CDU_Situacao"].Valor.ToString();

                        // carregar SituacaoDesc

                        if (LinhaReg.CDU_Situacao != "")
                        {
                            TDU_SituacoesLoteRow Situacao = TDU_SituacoesLote.FindByCodigo(LinhaReg.CDU_Situacao);
                            if (Situacao != null)
                                LinhaReg.SituacaoDescricao = Situacao.Descricao;
                            else
                                LinhaReg.SituacaoDescricao = "";
                        }
                        else
                            LinhaReg.SituacaoDescricao = "";

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_Parafinado"].Valor) | DocVenda.CamposUtil["CDU_Parafinado"].Valor == null)
                            LinhaReg.CDU_Parafinado = false;
                        else
                            LinhaReg.CDU_Parafinado = (bool)DocVenda.CamposUtil["CDU_Parafinado"].Valor;

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_TipoQualidade"].Valor) | DocVenda.CamposUtil["CDU_TipoQualidade"].Valor == null)
                            LinhaReg.CDU_TipoQualidade = "";
                        else
                            LinhaReg.CDU_TipoQualidade = DocVenda.CamposUtil["CDU_TipoQualidade"].Valor.ToString();

                        // carregar TipoQualidadeDesc

                        if (LinhaReg.CDU_TipoQualidade != "")
                        {
                            TDU_TipoQualidadeRow TipoQualidade = TDU_TipoQualidade.FindByCodigo(LinhaReg.CDU_TipoQualidade);
                            if (TipoQualidade != null)
                                LinhaReg.TipoQualidadeDescricao = TipoQualidade.Descricao;
                            else
                                LinhaReg.TipoQualidadeDescricao = "";
                        }
                        else
                            LinhaReg.TipoQualidadeDescricao = "";


                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_PaisOrigem"].Valor) | DocVenda.CamposUtil["CDU_PaisOrigem"].Valor == null)
                            LinhaReg.CDU_PaisOrigem = "";
                        else
                            LinhaReg.CDU_PaisOrigem = DocVenda.CamposUtil["CDU_PaisOrigem"].Valor.ToString();

                        // carregar PaisOrigemDesc

                        if (LinhaReg.CDU_PaisOrigem != "")
                        {
                            PaisesRow PaisOrigem = Paises.FindByCodigo(LinhaReg.CDU_PaisOrigem);
                            if (PaisOrigem != null)
                                LinhaReg.PaisOrigemDescricao = PaisOrigem.Descricao;
                            else
                                LinhaReg.PaisOrigemDescricao = "";
                        }
                        else
                            LinhaReg.PaisOrigemDescricao = "";

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_Seguradora"].Valor) | DocVenda.CamposUtil["CDU_Seguradora"].Valor == null)
                            LinhaReg.CDU_Seguradora = "";
                        else
                            LinhaReg.CDU_Seguradora = DocVenda.CamposUtil["CDU_Seguradora"].Valor.ToString();

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_NumCertificado"].Valor) | DocVenda.CamposUtil["CDU_NumCertificado"].Valor == null)
                            LinhaReg.CDU_NumCertificado = "";
                        else
                            LinhaReg.CDU_NumCertificado = DocVenda.CamposUtil["CDU_NumCertificado"].Valor.ToString();

                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_Parque"].Valor) | DocVenda.CamposUtil["CDU_Parque"].Valor == null)
                            LinhaReg.CDU_Parque = "";
                        else
                            LinhaReg.CDU_Parque = DocVenda.CamposUtil["CDU_Parque"].Valor.ToString();

                        // Carregar ParqueDesc

                        if (LinhaReg.CDU_Parque != "")
                        {
                            TDU_ParquesRow Parques = TDU_Parques.FindByCodigo(LinhaReg.CDU_Parque);
                            if (Parques != null)
                                LinhaReg.ParqueDescricao = Parques.Descricao;
                            else
                                LinhaReg.ParqueDescricao = "";
                        }
                        else
                            LinhaReg.ParqueDescricao = "";


                        if (Convert.IsDBNull(DocVenda.CamposUtil["CDU_ObsMdf"].Valor) | DocVenda.CamposUtil["CDU_ObsMdf"].Valor == null)
                            LinhaReg.CDU_ObsMdf = "";
                        else
                            LinhaReg.CDU_ObsMdf = DocVenda.CamposUtil["CDU_ObsMdf"].Valor.ToString();



                        LinhasDoc.Rows.Add(LinhaReg);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreram erros ao carregar o documento para o formulário campos de utilizador." + Constants.vbNewLine + ex.Message, "Editor Vendas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AtualizaDocumentoVenda()
        {
            try
            {
                foreach (CabecDocRow Cabec in CabecDoc)
                {

                    if (Cabec.IsCDU_AgenteNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Agente"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Agente"].Valor = Cabec.CDU_Agente;
                    }

                    if (Cabec.IsCDU_FornecedorNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor = Cabec.CDU_Fornecedor;
                    }

                    if (Cabec.IsCDU_ComissaoNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Comissao"].Valor = 0;
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Comissao"].Valor = Cabec.CDU_Comissao;
                    }

                    if (Cabec.IsCDU_IncotermsNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Incoterms"].Valor = Cabec.CDU_Incoterms;
                    }

                    if (Cabec.IsCDU_CustoFreteNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_CustoFrete"].Valor = 0;
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_CustoFrete"].Valor = Cabec.CDU_CustoFrete;
                    }

                    if (Cabec.IsCDU_NCartaCreditoNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_NCartaCredito"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_NCartaCredito"].Valor = Cabec.CDU_NCartaCredito;
                    }

                    if (Cabec.IsCDU_DataComunicacaoClienteNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_DataLimiteEmissaoCC"].Valor = DateTime.Parse("1999-01-01");
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_DataLimiteEmissaoCC"].Valor = Cabec.CDU_DataLimiteEmissaoCC;
                    }

                    if (Cabec.IsCDU_DataPrevistaChegadaNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_DataPrevistaChegada"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_DataPrevistaChegada"].Valor = Cabec.CDU_DataPrevistaChegada;
                    }

                    if (Cabec.IsCDU_CompanhiaMaritimaNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor = Cabec.CDU_CompanhiaMaritima;
                    }

                    if (Cabec.IsCDU_NavioNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Navio"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Navio"].Valor = Cabec.CDU_Navio;
                    }

                    if (Cabec.IsCDU_NBLNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_NBL"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_NBL"].Valor = Cabec.CDU_NBL;
                    }

                    if (Cabec.IsCDU_DataComunicacaoClienteNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_DataComunicacaoCliente"].Valor = DateTime.Parse("1999-01-01");
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_DataComunicacaoCliente"].Valor = Cabec.CDU_DataComunicacaoCliente;
                    }

                    if (Cabec.IsCDU_BancoNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Banco"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Banco"].Valor = Cabec.CDU_Banco;
                    }

                    if (Cabec.IsCDU_PortoNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Porto"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Porto"].Valor = Cabec.CDU_Porto;
                    }

                    if (Cabec.IsCDU_DestinoNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Destino"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Destino"].Valor = Cabec.CDU_Destino;
                    }

                    if (Cabec.IsCDU_LocalidadeNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Localidade"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Localidade"].Valor = Cabec.CDU_Localidade;
                    }

                    if (Cabec.IsNumDocNull())
                    {
                        DocumentoVenda.Referencia = "";
                        DocumentoVenda.Requisicao = "";
                    }
                    else
                    {
                        DocumentoVenda.Referencia = Cabec.NumDoc;
                        DocumentoVenda.Requisicao = Cabec.NumDoc;
                    }

                    if (Cabec.IsCDU_DataEmissaoCCNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_DataEmissaoCC"].Valor = DateTime.Parse("1999-01-01");
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_DataEmissaoCC"].Valor = Cabec.CDU_DataEmissaoCC;
                    }

                    if (Cabec.IsCDU_VendedorNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor = "";
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_Vendedor"].Valor = Cabec.CDU_Vendedor;
                    }

                    if (Cabec.IsCDU_ComissaoVendedorNull())
                    {
                        DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor = 0;
                    }
                    else
                    {
                        DocumentoVenda.CamposUtil["CDU_ComissaoVendedor"].Valor = Cabec.CDU_ComissaoVendedor;
                    }
                }



                foreach (LinhasDocRow Linha in LinhasDoc)
                {
                    if (Linha.NumLinha == 0 & Linha.Id == "")
                    {
                    }
                    else
                    {
                        if (DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).IdLinha != Linha.Id)
                        {
                            throw new Exception("Dados inconsistentes. Tente novamente.");
                        }

                        if (Linha.IsQuantidadeNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Quantidade = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Quantidade = Linha.Quantidade;
                        }

                        if (Linha.IsPrecUnitNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).PrecUnit = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).PrecUnit = Linha.PrecUnit;
                        }

                        DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Unidade = Linha.Unidade;
                        if (Linha.IsCDU_AgenteNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Agente"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Agente"].Valor = Linha.CDU_Agente;
                        }

                        if (Linha.IsCDU_ComissaoNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Comissao"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Comissao"].Valor = Linha.CDU_Comissao;
                        }

                        if (Linha.IsCDU_IncotermsNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Incoterms"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Incoterms"].Valor = Linha.CDU_Incoterms;
                        }

                        if (Linha.IsCDU_CondPagNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_CondPag"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_CondPag"].Valor = DocumentoVenda.CondPag;
                        }

                        if (Linha.IsVendedorNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Vendedor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Vendedor = Linha.Vendedor;
                        }

                        if (Linha.IsComissaoVendedorNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Comissao = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Comissao = Linha.ComissaoVendedor;
                        }

                        if (Linha.IsCDU_DataPrevistaEmbarqueNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_DataPrevistaEmbarque"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_DataPrevistaEmbarque"].Valor = Linha.CDU_DataPrevistaEmbarque;
                        }

                        if (Linha.IsCDU_DataPrevistaEmbarqueNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Observacoes"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Observacoes"].Valor = Linha.CDU_Observacoes;
                        }

                        if (Linha.IsCDU_DPEAlteradaMotivoNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_DPEAlteradaMotivo"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_DPEAlteradaMotivo"].Valor = Linha.CDU_DPEAlteradaMotivo;
                        }

                        if (Linha.IsCDU_ETRClienteNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ETRCliente"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ETRCliente"].Valor = Linha.CDU_ETRCliente;
                        }

                        if (Linha.IsCDU_ETRFornecedorNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ETRFornecedor"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ETRFornecedor"].Valor = Linha.CDU_ETRFornecedor;
                        }

                        if (Linha.IsCDU_EstadoPagamentoNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_EstadoPagamento"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_EstadoPagamento"].Valor = Linha.CDU_EstadoPagamento;
                        }

                        if (Linha.IsCDU_Certificado1Null())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Certificado1"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Certificado1"].Valor = Linha.CDU_Certificado1;
                        }

                        if (Linha.IsCDU_CertificadoTratado1Null())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_CertificadoTratado1"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_CertificadoTratado1"].Valor = Linha.CDU_CertificadoTratado1;
                        }

                        if (Linha.IsCDU_Certificado2Null())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Certificado2"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Certificado2"].Valor = Linha.CDU_Certificado2;
                        }

                        if (Linha.IsCDU_CertificadoTratado2Null())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_CertificadoTratado2"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_CertificadoTratado2"].Valor = Linha.CDU_CertificadoTratado2;
                        }

                        if (Linha.IsCDU_PesoLiquidoNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_PesoLiquido"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_PesoLiquido"].Valor = Linha.CDU_PesoLiquido;
                        }

                        if (Linha.IsCDU_PesoBrutoNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_PesoBruto"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_PesoBruto"].Valor = Linha.CDU_PesoBruto;
                        }

                        if (Linha.IsCDU_NVolumesNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_NVolumes"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_NVolumes"].Valor = Linha.CDU_NVolumes;
                        }

                        if (Linha.IsCDU_NContentorNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_NContentor"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_NContentor"].Valor = Linha.CDU_NContentor;
                        }

                        if (Linha.IsCDU_NFaturaNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_NFatura"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_NFatura"].Valor = Linha.CDU_NFatura;
                        }

                        if (Linha.IsCDU_LoteFornecedorNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_LoteFornecedor"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_LoteFornecedor"].Valor = Linha.CDU_LoteFornecedor;
                        }

                        if (Linha.IsLoteNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Lote = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).Lote = Linha.Lote;
                        }

                        if (Linha.IsCDU_ValorFOBNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ValorFOB"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ValorFOB"].Valor = Linha.CDU_ValorFOB;
                        }

                        if (Linha.IsCDU_ComissaoConsideradaNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ComissaoConsiderada"].Valor = false;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ComissaoConsiderada"].Valor = Linha.CDU_ComissaoConsiderada;
                        }

                        if (Linha.IsCDU_ValorLiquidoOrigNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ValorLiquidoOrig"].Valor = 0;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ValorLiquidoOrig"].Valor = Linha.CDU_ValorLiquidoOrig;
                        }

                        if (Linha.IsCDU_SituacaoNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Situacao"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Situacao"].Valor = Linha.CDU_Situacao;
                        }

                        if (Linha.IsCDU_ParafinadoNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Parafinado"].Valor = false;
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Parafinado"].Valor = Linha.CDU_Parafinado;
                        }

                        if (Linha.IsCDU_TipoQualidadeNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_TipoQualidade"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_TipoQualidade"].Valor = Linha.CDU_TipoQualidade;
                        }

                        if (Linha.IsCDU_PaisOrigemNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_PaisOrigem"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_PaisOrigem"].Valor = Linha.CDU_PaisOrigem;
                        }

                        if (Linha.IsCDU_SeguradoraNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Seguradora"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Seguradora"].Valor = Linha.CDU_Seguradora;
                        }

                        if (Linha.IsCDU_NumCertificadoNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_NumCertificado"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_NumCertificado"].Valor = Linha.CDU_NumCertificado;
                        }

                        if (Linha.IsCDU_ParqueNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Parque"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_Parque"].Valor = Linha.CDU_Parque;
                        }

                        if (Linha.IsCDU_ObsMdfNull())
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ObsMdf"].Valor = "";
                        }
                        else
                        {
                            DocumentoVenda.Linhas.GetEdita(Linha.NumLinha).CamposUtil["CDU_ObsMdf"].Valor = Linha.CDU_ObsMdf;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

