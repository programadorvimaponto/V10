using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.Windows.Forms;

namespace CopiaEntreEmpresas
{
    public class VndIsEditorVendas : EditorVendas
    {
        private String NomeEmpresaDestino;
        private string InstanciaEmpresaDestino;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);
            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                // #EduSamp
                if (!BSO.Vendas.Documentos.Existe(this.DocumentoVenda.Filial, this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc))
                {
                    this.DocumentoVenda.CamposUtil["CDU_DocumentoVendaDestino"].Valor = "";
                    this.DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor = "";
                }
            }
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);
            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                // Edusamp
                IdentificarClienteEmpresaGrupo(Cliente);
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);
            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                // EduSamp
                RegistarDocumentosEmpresaGrupo(Filial, Tipo, Serie, NumDoc);
            }
        }

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);
            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                // Edusamp F3
                // Quando carregar no F3 exibir o formulário de escolha de entidade final, apenas se a entidade do documento for empresa do grupo
                if (KeyCode == 114 & Strings.Len(this.DocumentoVenda.Entidade) > 0)
                {
                    IdentificarClienteEmpresaGrupo(this.DocumentoVenda.Entidade);
                    if (this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "" != "" & this.DocumentoVenda.Tipodoc == "ECL")
                    {
                        StdBELista listEntidadeFinal;
                        listEntidadeFinal = BSO.Consulta("select c2.Cliente from pri" + BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + ".dbo.Clientes c  inner join Clientes c2 on c2.CDU_EntidadeInterna=c.CDU_EntidadeInterna where c.Cliente='" + this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"] + "'");
                        listEntidadeFinal.Inicio();
                        if (listEntidadeFinal.Vazia() == false)
                        {
                            if (BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).CamposUtil["CDU_ObsEncomenda"].Valor + "" != "")
                            {
                                MessageBox.Show(BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).CamposUtil["CDU_ObsEncomenda"].Valor, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        private bool RegistarDocumentosEmpresaGrupo(string Filial_Atual, string TipoDoc_Atual, string Serie_Atual, int NumDoc_Atual)
        {
            try
            {
                string Mensagem;

                // Se algum dos documentos de destino já tiverem sido gerados, não faz qualquer alteração na empresa de destino!
                if (Strings.Len(this.DocumentoVenda.CamposUtil["CDU_DocumentoVendaDestino"].Valor + "") > 0 | Strings.Len(this.DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor + "") > 0)
                {
                    Mensagem = "O Documento atual já tinha dado origem ao(s) seguinte(s) documento(s) na empresa de Grupo: " + '\r' + '\r' + "" + this.DocumentoVenda.CamposUtil["CDU_DocumentoVendaDestino"].Valor + "" + '\r' + "" + this.DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor + "" + '\r' + '\r' + "Caso tenha efetuado altearções, deverá replicar manualmente na empresa de Grupo.";
                    Mensagem = Strings.Replace(Mensagem, "''", "");
                    MessageBox.Show(Mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }

                if (Strings.Len(this.DocumentoVenda.CamposUtil["CDU_DocumentoOrigem"].Valor + "") > 0)
                {
                    Mensagem = "O Documento atual já tinha sido gerado através do seguinte documento na empresa de Grupo: " + '\r' + '\r' + "" + this.DocumentoVenda.CamposUtil["CDU_DocumentoOrigem"].Valor + "" + '\r' + "Não irá gerar nenhum documento na empresa do Grupo.";
                    Mensagem = Strings.Replace(Mensagem, "''", "");
                    MessageBox.Show(Mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }

                // Validação pedida pelo eng. Joaquim Costa
                if (Strings.Right(this.DocumentoVenda.Serie, 1) == "x" | Strings.Right(this.DocumentoVenda.Serie, 1) == "X")
                {
                    return true;
                }

                NomeEmpresaDestino = BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "";
                string EntidadeDestino;
                string ArmazemDestino;
                if (Strings.Len(NomeEmpresaDestino) == 0)
                {
                    return true;
                }
                else
                {
                    // Se Entidade Empresa Grupo Associada à entidade definida...

                    // Gerar Encomenda de Fornecedor
                    string TipoDocComprasDestino;
                    TipoDocComprasDestino = Strings.UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil["CDU_TipoDocComprasDestino"].Valor + "");
                    if (Strings.Len(TipoDocComprasDestino) == 0)
                    {
                        // MsgBox "O campo de utilizador 'TipoDoc Compras Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Fornecedor na empresa de Grupo", vbInformation + vbOKOnly
                        return true;
                    }

                    string SerieComprasDestino;
                    SerieComprasDestino = Strings.UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil["CDU_SerieComprasDestino"].Valor + "");
                    if (Strings.Len(SerieComprasDestino) == 0)
                    {
                        // Avisa por que se tem tipo de documento tem de ter serie também

                        MessageBox.Show("O campo de utilizador 'Serie Compras Destino' do Documento '" + TipoDoc_Atual + "' não está preenchido, pelo que não será possível gerar a Encomenda de Fornecedor na empresa de Grupo", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    // Fornecedor colocado no campo de utilizador da entidade colocada no documento
                    EntidadeDestino = BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_CodigoFornecedorGrupo"].Valor + "";
                    if (Strings.Len(EntidadeDestino) == 0)
                    {
                        // MsgBox "O campo de utilizador 'Codigo Fornecedor Grupo' da Entidade '" & Me.DocumentoVenda.Entidade & "' não está preenchido, pelo que não será possível gerar a Encomenda de Fornecedor na empresa de Grupo", vbInformation + vbOKOnly
                        return true;
                    }

                    // Armazem que será usado para criar os documento de compra e venda (caso preenchido)
                    // ArmazemDestino = BSO.Comercial.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil["CDU_ArmazemGrupo").Valor & ""
                    if (Strings.Len(this.DocumentoVenda.CamposUtil["CDU_ArmazemGrupo"].Valor + "") > 0)
                    {
                        ArmazemDestino = this.DocumentoVenda.CamposUtil["CDU_ArmazemGrupo"].Valor.ToString();
                    }
                    else
                    {
                        ArmazemDestino = BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_ArmazemGrupo"].Valor + "";
                    }

                    // Se o armazém do parametro não esstiver definido,
                    // Identifico o armazem da Encomenda a fornecedor, através da rastreabilidade
                    if (Strings.Len(ArmazemDestino) == 0)
                        ArmazemDestino = IdentificarArmazemDaEncomendaFornecedor(Filial_Atual, Serie_Atual, TipoDoc_Atual, NumDoc_Atual, NomeEmpresaDestino);
                    var DocumentoModelo = new VndBE100.VndBEDocumentoVenda();
                    DocumentoModelo = new VndBE100.VndBEDocumentoVenda();
                    DocumentoModelo = BSO.Vendas.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual);

                    // Cliente final inserido
                    if (Strings.Len(this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "") > 0)
                    {
                        // Gerar Encomenda de Cliente

                        // #2018.03.08
                        // 2018.03.06 Email dia 2018.03.01 do Eng.º Joaquima onde foi solicitado que ou gera os 2 documentos nas empresas de grupo ou não gera nenhum.
                        if (MessageBox.Show("Pretende gerar documentos na empresa do Grupo?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            return true;
                        }

                        // ********************************************************************************************************************************************
                        // Bruno - Verifica se existe rastreabilidade em todas as linhas da GR na empresa destino
                        // ********************************************************************************************************************************************
                        if (this.DocumentoVenda.Tipodoc == "GR")
                        {
                            StdBELista ListSQL;
                            StdBELista ListSQL1;
                            bool Sair;
                            // Dim i As Long
                            string msg;
                            msg = "O(s) artigo(s) não têm rastreabilidade na empresa destino: " + '\r';
                            Sair = false;
                            for (int i = 1, loopTo = this.DocumentoVenda.Linhas.NumItens; i <= loopTo; i++)
                            {
                                if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "")
                                {
                                    ListSQL = BSO.Consulta("select * from pri" + NomeEmpresaDestino + ".dbo.cabecdoc cd inner join pri" + NomeEmpresaDestino + ".dbo.linhasdoc ln on ln.IdCabecDoc=cd.Id where cd.TipoDoc='ECL' and ln.CDU_IDLinhaOriginalGrupo= (select top 1 concat('{',lt.IdLinhasDocOrigem,'}') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinha + "')");
                                    if (ListSQL.Vazia() == true)
                                    {
                                        Sair = true;
                                        ListSQL1 = BSO.Consulta("select concat (cd.CDU_DocumentoCompraDestino, ' e ', cd.CDU_DocumentoVendaDestino) as 'Doc' from LinhasDocTrans lt inner join LinhasDoc ln on lt.IdLinhasDocOrigem=ln.Id inner join CabecDoc cd on cd.Id=ln.IdCabecDoc where lt.IdLinhasDoc='" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinha + "'");
                                        ListSQL1.Inicio();
                                        msg = msg + Constants.vbCrLf + " Artigo:  " + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + " Lote: " + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" + Constants.vbCrLf + "Documentos: " + ListSQL1.Valor("Doc") + "";
                                    }
                                }
                            }

                            if (Sair == true)
                            {
                                MessageBox.Show(msg + Constants.vbCrLf + Constants.vbCrLf + Constants.vbCrLf + "Tem de criar as guias manualmente na empresa " + NomeEmpresaDestino, "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                        // ********************************************************************************************************************************************
                        // Bruno - Verifica se existe rastreabilidade em todas as linhas da GR na empresa destino
                        // ********************************************************************************************************************************************

                        // Gerar encomenda a fornecedor
                        Mdi_GeraDocumentoCompra.GerarDocumento_BaseVendas(DocumentoModelo, NomeEmpresaDestino, TipoDocComprasDestino, SerieComprasDestino, EntidadeDestino, ArmazemDestino);
                        // mdl_GeraDocumentoCompra.GerarDocumento Filial_Atual, Serie_Atual, TipoDoc_Atual, NumDoc_Atual, NomeEmpresaDestino, TipoDocComprasDestino, SerieComprasDestino, EntidadeDestino, ArmazemDestino

                        double ValorASomarArtigo;
                        // ValorASomarArtigo = BSO.Comercial.TabVendas.Edita(TipoDoc_Atual).CamposUtil["CDU_MargemPassagemArtigo").Valor & ""

                        // Se estiver definido no documento atual, significa que já invocou o formulário (pop up) do cliente final
                        if (Strings.Len(this.DocumentoVenda.CamposUtil["CDU_MargemPassagemArtigo"].Valor + "") > 0)
                        {
                            ValorASomarArtigo = Double.Parse(this.DocumentoVenda.CamposUtil["CDU_MargemPassagemArtigo"].Valor.ToString());
                        }
                        else
                        {
                            ValorASomarArtigo = Double.Parse(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil["CDU_MargemPassagemArtigo"].Valor.ToString());
                        }

                        string TipoDocVendasDestino;
                        TipoDocVendasDestino = Strings.UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil["CDU_TipoDocVendasDestino"].Valor + "");
                        if (Strings.Len(TipoDocVendasDestino) == 0)
                        {
                            // MsgBox "O campo de utilizador 'TipoDoc Vendas Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", vbInformation + vbOKOnly
                            return true;
                        }

                        string SerieVendasDestino;
                        string SQLstr;
                        StdBELista PaisClienteFinal;
                        SQLstr = "select c.Pais from pri" + NomeEmpresaDestino + ".dbo.Clientes c where c.Cliente='" + this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "'";
                        PaisClienteFinal = BSO.Consulta(SQLstr);
                        PaisClienteFinal.Inicio();
                        if (this.DocumentoVenda.Tipodoc == "GR" & PaisClienteFinal.Valor("Pais") != "PT")
                        {
                            SerieVendasDestino = Strings.UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil["CDU_SerieMEVendasDestino"].Valor + "");
                        }
                        else
                        {
                            SerieVendasDestino = Strings.UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil["CDU_SerieVendasDestino"].Valor + "");
                        }

                        if (Strings.Len(SerieVendasDestino) == 0)
                        {
                            // MsgBox "O campo de utilizador 'Serie Vendas Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", vbInformation + vbOKOnly
                            return true;
                        }

                        // Entidade colocada no formulário que é exibido (o formulário só é mostrado se o cliente colocado no documento pertencer à empresa de Grupo (so pertence à empresa de grupo se a entidade tiver o campo de utilizador CDU_NomeEmpresaGrupo preenchido))
                        EntidadeDestino = this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "";
                        if (this.DocumentoVenda.Tipodoc == "GR" & TipoDocVendasDestino == "GR")
                        {
                            if (MessageBox.Show("Pretende gerar o Documento 'GR' no Cliente Final '" + NomeEmpresaDestino + "'?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                DocumentoModelo = new VndBE100.VndBEDocumentoVenda();
                                DocumentoModelo = BSO.Vendas.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual);
                                Mdi_GeraDocumentoVenda.GerarDocumento_BaseVendas(DocumentoModelo, NomeEmpresaDestino, TipoDocVendasDestino, SerieVendasDestino, EntidadeDestino, ArmazemDestino, ValorASomarArtigo);
                            }
                        }
                        else
                        {
                            DocumentoModelo = new VndBE100.VndBEDocumentoVenda();
                            DocumentoModelo = BSO.Vendas.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual);
                            Mdi_GeraDocumentoVenda.GerarDocumento_BaseVendas(DocumentoModelo, NomeEmpresaDestino, TipoDocVendasDestino, SerieVendasDestino, EntidadeDestino, ArmazemDestino, ValorASomarArtigo);
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private void IdentificarClienteEmpresaGrupo(string Cliente)
        {
            // ********************************************************************************************************************************************
            // Eduardo Sampaio 2016.12.21 (inicio) 'Edusamp
            // ********************************************************************************************************************************************

            // Sempre que o cliente é identificado, apago a entidade final
            // Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor = ""

            // Se o cliente não tiver empres do Grupo Preenchido, sai da função
            NomeEmpresaDestino = BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "";
            if (Strings.Len(NomeEmpresaDestino) == 0)
                return;

            // Se o documento não tiver parameterizado documentos de Venda (Encomenda de Cliente) não vai mostrar o formulário
            string TipoDocVendasDestino;
            TipoDocVendasDestino = Strings.UCase(BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).CamposUtil["CDU_TipoDocVendasDestino"].Valor + "");
            if (Strings.Len(TipoDocVendasDestino) == 0)
                return;

            // Se o documento não tiver parameterizado Serie para os documentos de Venda (Encomenda de Cliente) não vai mostrar o formulário
            string SerieVendasDestino;
            SerieVendasDestino = Strings.UCase(BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).CamposUtil["CDU_SerieVendasDestino"].Valor + "");
            if (Strings.Len(SerieVendasDestino) == 0)
                return;

            // Verificar se o cliente associado realmente pertence à empresa de grupo. Não basta apenas estar preenchido!
            // Aproveito tambem para identificar a instancia...
            StdBELista stdBE_ListaEmpresasGrupo;
            stdBE_ListaEmpresasGrupo = BSO.Consulta("SELECT CDU_Empresa, CDU_Nome, CDU_Instancia " + "  FROM PRIEMPRE.dbo.TDU_EmpresasGrupo " + "  WHERE CDU_Empresa = '" + NomeEmpresaDestino + "' ");

            if (!stdBE_ListaEmpresasGrupo.Vazia())
            {
                stdBE_ListaEmpresasGrupo.Inicio();
                NomeEmpresaDestino = stdBE_ListaEmpresasGrupo.Valor("CDU_Empresa");
                InstanciaEmpresaDestino = stdBE_ListaEmpresasGrupo.Valor("CDU_Instancia");

                // Exibir o formulário para escolha do cliente final
                FrmClientesView FormularioClientes = new FrmClientesView();
                FormularioClientes.EmpresaDestino = NomeEmpresaDestino;
                FormularioClientes.PRIEmpresaDestino = "PRI" + NomeEmpresaDestino;

                FormularioClientes.TextEditCodigoCliente.EditValue = this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "";
                FormularioClientes.TextEditDescricaoCliente.EditValue = this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupoNome"].Valor + ""; // #20180219

                // Se o campo de utilizador "CDU_ArmazemGrupo" do documento estiver preenchido, carrego o pop up com ele
                if (Strings.Len(this.DocumentoVenda.CamposUtil["CDU_ArmazemGrupo"].Valor + "") > 0)
                    FormularioClientes.TextEditCodigoArmazem.EditValue = this.DocumentoVenda.CamposUtil["CDU_ArmazemGrupo"].Valor + "";
                else
                    FormularioClientes.TextEditCodigoArmazem.EditValue = BSO.Base.Clientes.DaValorAtributo(this.DocumentoVenda.Entidade, "CDU_ArmazemGrupo");

                // Se o campo de utilizador "CDU_MoradaAlternativa" do documento estiver preenchido, carrego o pop up com ele
                if (Strings.Len(this.DocumentoVenda.CamposUtil["CDU_MoradaAlternativa"].Valor + "") > 0)
                    FormularioClientes.TextEditCodigoLocalDescarga.EditValue = this.DocumentoVenda.CamposUtil["CDU_MoradaAlternativa"].Valor + "";
                else
                    FormularioClientes.TextEditCodigoLocalDescarga.EditValue = this.DocumentoVenda.MoradaAlternativaEntrega;

                if (Strings.Len(this.DocumentoVenda.CamposUtil["CDU_MargemPassagemArtigo"].Valor + "") != 1)
                    FormularioClientes.TextEditCodigoMargem.EditValue = this.DocumentoVenda.CamposUtil["CDU_MargemPassagemArtigo"].Valor + "";
                else
                    // Sugere sempre a do parametro!
                    FormularioClientes.TextEditCodigoMargem.EditValue = BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).CamposUtil["CDU_MargemPassagemArtigo"].Valor + "";

                FormularioClientes.TextEditCodigoMargem.EditValue = Strings.Replace(FormularioClientes.TextEditCodigoMargem.EditValue.ToString(), ".", ""); // #2018.03.08

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmClientesView));

                if (result.ResultCode == ExtensibilityResultCode.Ok)
                {
                    FrmClientesView frm = result.Result;
                    frm.ShowDialog();
                }

                if (result.ResultCode == ExtensibilityResultCode.Ok)
                {
                    this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor = FormularioClientes.TextEditCodigoCliente.EditValue;
                    this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupoNome"].Valor = FormularioClientes.TextEditDescricaoCliente.EditValue; // #20180219
                    this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor = FormularioClientes.TextEditCodigoIdioma.EditValue;
                    this.DocumentoVenda.CamposUtil["CDU_MargemPassagemArtigo"].Valor = Strings.Replace(FormularioClientes.TextEditCodigoMargem.EditValue.ToString(), ".", ""); // #2018.03.08
                    this.DocumentoVenda.CamposUtil["CDU_ArmazemGrupo"].Valor = FormularioClientes.TextEditCodigoArmazem.EditValue;
                    this.DocumentoVenda.CamposUtil["CDU_MoradaAlternativa"].Valor = FormularioClientes.TextEditCodigoLocalDescarga.EditValue;

                    // #20180219 (inicio)
                    // Se cliente final definido e local de descarga não, atribuir a morada de faturação!
                    if (Strings.Len(FormularioClientes.TextEditDescricaoCliente.EditValue) > 0 & FormularioClientes.TextEditCodigoLocalDescarga.EditValue.ToString() == "")
                    {
                        // Identificar a morada Faturação!
                        this.DocumentoVenda.MoradaAlternativaEntrega = "";
                        // #2018.03.08
                        this.DocumentoVenda.MoradaEntrega = Strings.Left(FormularioClientes.TextEditDescricaoCliente.EditValue.ToString(), 50);
                        // Me.DocumentoVenda.Morada2Entrega = Left(FormularioClientes.txtDescricaoCliente.Text, 50)
                        // Me.DocumentoVenda.MoradaEntrega = Left(FormularioClientes.txtDescricaoCliente.Text, 10) & " - " & Left(BSO.Comercial.Clientes.DaValorAtributo(FormularioClientes.txtCodigoCliente.Text, "Fac_Mor"), 37)
                        this.DocumentoVenda.Morada2Entrega = Strings.Left(BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue.ToString(), "Fac_Mor"), 50);
                        this.DocumentoVenda.LocalidadeEntrega = BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue.ToString(), "Fac_Local"); ;
                        this.DocumentoVenda.CargaDescarga.CodPostalEntrega = BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue.ToString(), "Fac_cp");
                        this.DocumentoVenda.CargaDescarga.CodPostalLocalidadeEntrega = BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue.ToString(), "Fac_CpLoc");
                        this.DocumentoVenda.CargaDescarga.DistritoEntrega = BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue.ToString(), "Distrito");
                    }
                    else
                    {
                        // Esta tem de ser vazia porque não existe. A morada que estamos a falar é da empresa do Grupo.
                        this.DocumentoVenda.MoradaAlternativaEntrega = "";
                        // #2018.03.08
                        this.DocumentoVenda.MoradaEntrega = Strings.Left(FormularioClientes.TextEditDescricaoCliente.EditValue.ToString(), 50);
                        // Me.DocumentoVenda.Morada2Entrega = Left(FormularioClientes.txtDescricaoCliente.Text, 50)
                        // Me.DocumentoVenda.MoradaEntrega = Left(FormularioClientes.txtDescricaoCliente.Text, 10) & " - " & Left(FormularioClientes.txtCodigoMoradaEntrega.Text, 37)
                        this.DocumentoVenda.Morada2Entrega = Strings.Left(FormularioClientes.TextEditCodigoMoradaEntrega2.EditValue.ToString(), 50);
                        this.DocumentoVenda.LocalidadeEntrega = FormularioClientes.TextEditCodigoLocalidadeEntrega.EditValue.ToString();
                        this.DocumentoVenda.CargaDescarga.CodPostalEntrega = FormularioClientes.TextEditCodigoPostalEntrega.EditValue.ToString();
                        this.DocumentoVenda.CargaDescarga.CodPostalLocalidadeEntrega = FormularioClientes.TextEditCodPostalLocalidadeEntrega.EditValue.ToString();
                        this.DocumentoVenda.CargaDescarga.DistritoEntrega = FormularioClientes.TextEditDistritoEntrega.EditValue.ToString();
                    }
                }

                // JFC 05/08/2020 - Identifica Observações na ficha do cliente quando a venda é feita entre empresas. Pedido de Ze Luis.
                if (this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "" != "" & this.DocumentoVenda.Tipodoc == "ECL")
                {
                    StdBELista listEntidadeFinal;

                    listEntidadeFinal = BSO.Consulta("select c2.Cliente from pri" + BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + ".dbo.Clientes c  inner join Clientes c2 on c2.CDU_EntidadeInterna=c.CDU_EntidadeInterna where c.Cliente='" + this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"] + "'");
                    listEntidadeFinal.Inicio();

                    if (listEntidadeFinal.Vazia() == false)
                    {
                        if (BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).CamposUtil["CDU_ObsEncomenda"].Valor + "" != "")
                            MessageBox.Show(BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).CamposUtil["CDU_ObsEncomenda"].Valor, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // JFC 23/09/2020 - Colocar a Moeda do Cliente final na Encomenda. Pedido de Mafalda após surgir faturas em euros para o cliente sallis (libras)
                        this.DocumentoVenda.Moeda = BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).Moeda;
                        // JFC 03/08/2021 Colocado o Cambio após ser detectado que uma ECL da Sallis (Libras) não estava assumir o cambio.
                        this.DocumentoVenda.Cambio = BSO.Base.Moedas.DaCambioCompra(this.DocumentoVenda.Moeda, DateTime.Now);
                    }
                }
            }
            else
            {
                MessageBox.Show("O cliente " + this.DocumentoVenda.Entidade + " - " + this.DocumentoVenda.DescEntidade + " tem empresa de grupo definida mas esta não consta na tabela TDU_EmpresasGrupo!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor = "";
            }
        }

        private string IdentificarArmazemDaEncomendaFornecedor(string Filial_FaturaCliente, string Serie_FaturaCliente, string TipoDoc_FaturaCliente, long NumDoc_FaturaCliente, string BaseDadosDestino)
        {
            string Str_Rastreabilidade;
            StdBELista Lst_StrRastreabilidade;

            Str_Rastreabilidade = "  SELECT  COALESCE(LDGrupo.Armazem,'') as Armazem " + " FROM CabecDoc  CD " + " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60" + " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " + " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " + " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.LinhasCompras LDGrupo ON '{' + convert(nvarchar(50),  LDGrupo.id) + '}'  =  LD2.CDU_IDLinhaOriginalGrupo " + " INNER JOIN PRI" + BaseDadosDestino + ".dbo.CabecCompras CDGrupo ON CDGrupo.id = LDGrupo.IdCabecCompras " + " WHERE  CD.Filial = '" + Filial_FaturaCliente + "' AND CD.TipoDoc = '" + TipoDoc_FaturaCliente + "' AND CD.serie = '" + Serie_FaturaCliente + "' AND CD.NumDoc = " + NumDoc_FaturaCliente + " " + " ORDER BY LD.NumLinha ";
            Lst_StrRastreabilidade = BSO.Consulta(Str_Rastreabilidade);

            if (Lst_StrRastreabilidade.Vazia() == false)
            {
                Lst_StrRastreabilidade.Inicio();

                return Lst_StrRastreabilidade.Valor("Armazem");
            }
            else
                return "";
        }
    }
}