Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService
Imports Primavera.Extensibility.Constants.ExtensibilityService

Namespace CopiaEntreEmpresas
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then
                '#EduSamp
                If Not BSO.Vendas.Documentos.Existe(Me.DocumentoVenda.Filial, Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc) Then
                    Me.DocumentoVenda.CamposUtil("CDU_DocumentoVendaDestino").Valor = ""
                    Me.DocumentoVenda.CamposUtil("CDU_DocumentoCompraDestino").Valor = ""
                End If

            End If

        End Sub

        Public Overrides Sub ClienteIdentificado(Cliente As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ClienteIdentificado(Cliente, Cancel, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then
                'Edusamp
                IdentificarClienteEmpresaGrupo(Cliente)

            End If
        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then
                'EduSamp
                RegistarDocumentosEmpresaGrupo(Filial, Tipo, Serie, NumDoc)
            End If

        End Sub

        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
            MyBase.TeclaPressionada(KeyCode, Shift, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then

                'Edusamp F3
                ' Quando carregar no F3 exibir o formulário de escolha de entidade final, apenas se a entidade do documento for empresa do grupo
                If KeyCode = 114 And Len(Me.DocumentoVenda.Entidade) > 0 Then
                    IdentificarClienteEmpresaGrupo(Me.DocumentoVenda.Entidade)
                    If Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & "" <> "" And Me.DocumentoVenda.Tipodoc = "ECL" Then
                        Dim listEntidadeFinal As StdBELista

                        listEntidadeFinal = BSO.Consulta("select c2.Cliente from pri" & BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor & ".dbo.Clientes c  inner join Clientes c2 on c2.CDU_EntidadeInterna=c.CDU_EntidadeInterna where c.Cliente='" & Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo") & "'")
                        listEntidadeFinal.Inicio()

                        If listEntidadeFinal.Vazia = False Then
                            If BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).CamposUtil("CDU_ObsEncomenda").Valor & "" <> "" Then
                                MsgBox(BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).CamposUtil("CDU_ObsEncomenda").Valor, vbInformation + vbOKOnly)
                            End If


                        End If
                    End If
                End If

            End If

        End Sub

        'Edusamp
        Private Function RegistarDocumentosEmpresaGrupo(ByVal Filial_Atual As String,
                                                ByVal TipoDoc_Atual As String,
                                                ByVal Serie_Atual As String,
                                                ByVal NumDoc_Atual As Long) As Boolean

            On Error GoTo TrataErro

            Dim Mensagem As String

            'Se algum dos documentos de destino já tiverem sido gerados, não faz qualquer alteração na empresa de destino!
            If Len(Me.DocumentoVenda.CamposUtil("CDU_DocumentoVendaDestino").Valor & "") > 0 _
        Or Len(Me.DocumentoVenda.CamposUtil("CDU_DocumentoCompraDestino").Valor & "") > 0 Then

                Mensagem = "O Documento atual já tinha dado origem ao(s) seguinte(s) documento(s) na empresa de Grupo: " & Chr(13) & Chr(13) & "" & Me.DocumentoVenda.CamposUtil("CDU_DocumentoVendaDestino").Valor & "" & Chr(13) & "" & Me.DocumentoVenda.CamposUtil("CDU_DocumentoCompraDestino").Valor & "" & Chr(13) & Chr(13) & "Caso tenha efetuado altearções, deverá replicar manualmente na empresa de Grupo."
                Mensagem = Replace(Mensagem, "''", "")
                MsgBox(Mensagem, vbInformation + vbOKOnly)

                RegistarDocumentosEmpresaGrupo = True : Exit Function

            End If

            If Len(Me.DocumentoVenda.CamposUtil("CDU_DocumentoOrigem").Valor & "") > 0 Then
                Mensagem = "O Documento atual já tinha sido gerado através do seguinte documento na empresa de Grupo: " & Chr(13) & Chr(13) & "" & Me.DocumentoVenda.CamposUtil("CDU_DocumentoOrigem").Valor & "" & Chr(13) & "Não irá gerar nenhum documento na empresa do Grupo."
                Mensagem = Replace(Mensagem, "''", "")
                MsgBox(Mensagem, vbInformation + vbOKOnly)

                RegistarDocumentosEmpresaGrupo = True : Exit Function
            End If

            'Validação pedida pelo eng. Joaquim Costa
            If Right(Me.DocumentoVenda.Serie, 1) = "x" Or Right(Me.DocumentoVenda.Serie, 1) = "X" Then
                RegistarDocumentosEmpresaGrupo = True : Exit Function
            End If



            NomeEmpresaDestino = BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor & ""

            Dim EntidadeDestino As String
            Dim ArmazemDestino As String

            If Len(NomeEmpresaDestino) = 0 Then

                RegistarDocumentosEmpresaGrupo = True
                Exit Function

            Else
                'Se Entidade Empresa Grupo Associada à entidade definida...

                'Gerar Encomenda de Fornecedor
                Dim TipoDocComprasDestino As String
                TipoDocComprasDestino = UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil("CDU_TipoDocComprasDestino").Valor & "")
                If Len(TipoDocComprasDestino) = 0 Then
                    'MsgBox "O campo de utilizador 'TipoDoc Compras Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Fornecedor na empresa de Grupo", vbInformation + vbOKOnly
                    RegistarDocumentosEmpresaGrupo = True
                    Exit Function
                End If

                Dim SerieComprasDestino As String
                SerieComprasDestino = UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil("CDU_SerieComprasDestino").Valor & "")
                If Len(SerieComprasDestino) = 0 Then
                    'Avisa por que se tem tipo de documento tem de ter serie também
                    MsgBox("O campo de utilizador 'Serie Compras Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Fornecedor na empresa de Grupo", vbInformation + vbOKOnly)
                    RegistarDocumentosEmpresaGrupo = False
                    Exit Function
                End If

                'Fornecedor colocado no campo de utilizador da entidade colocada no documento
                EntidadeDestino = BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_CodigoFornecedorGrupo").Valor & ""
                If Len(EntidadeDestino) = 0 Then
                    'MsgBox "O campo de utilizador 'Codigo Fornecedor Grupo' da Entidade '" & Me.DocumentoVenda.Entidade & "' não está preenchido, pelo que não será possível gerar a Encomenda de Fornecedor na empresa de Grupo", vbInformation + vbOKOnly
                    RegistarDocumentosEmpresaGrupo = True
                    Exit Function
                End If

                'Armazem que será usado para criar os documento de compra e venda (caso preenchido)
                'ArmazemDestino = BSO.Comercial.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_ArmazemGrupo").Valor & ""
                If Len(Me.DocumentoVenda.CamposUtil("CDU_ArmazemGrupo").Valor & "") > 0 Then
                    ArmazemDestino = Me.DocumentoVenda.CamposUtil("CDU_ArmazemGrupo").Valor
                Else
                    ArmazemDestino = BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_ArmazemGrupo").Valor & ""
                End If


                'Se o armazém do parametro não esstiver definido,
                'Identifico o armazem da Encomenda a fornecedor, através da rastreabilidade
                If Len(ArmazemDestino) = 0 Then ArmazemDestino = IdentificarArmazemDaEncomendaFornecedor(Filial_Atual, Serie_Atual, TipoDoc_Atual, NumDoc_Atual, NomeEmpresaDestino)

                Dim DocumentoModelo As New VndBE100.VndBEDocumentoVenda
                DocumentoModelo = New VndBE100.VndBEDocumentoVenda
                DocumentoModelo = BSO.Vendas.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual)


                'Cliente final inserido
                If Len(Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & "") > 0 Then
                    'Gerar Encomenda de Cliente

                    '#2018.03.08
                    '2018.03.06 Email dia 2018.03.01 do Eng.º Joaquima onde foi solicitado que ou gera os 2 documentos nas empresas de grupo ou não gera nenhum.
                    If MsgBox("Pretende gerar documentos na empresa do Grupo?", vbInformation + vbYesNo) = vbNo Then
                        RegistarDocumentosEmpresaGrupo = True
                        Exit Function
                    End If




                    '********************************************************************************************************************************************
                    'Bruno - Verifica se existe rastreabilidade em todas as linhas da GR na empresa destino
                    '********************************************************************************************************************************************
                    If Me.DocumentoVenda.Tipodoc = "GR" Then
                        Dim ListSQL As StdBELista
                        Dim ListSQL1 As StdBELista
                        Dim Sair As Boolean
                        ' Dim i As Long
                        Dim msg As String
                        msg = "O(s) artigo(s) não têm rastreabilidade na empresa destino: " & Chr(13)
                        Sair = False

                        For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                            If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "" Then
                                ListSQL = BSO.Consulta("select * from pri" & NomeEmpresaDestino & ".dbo.cabecdoc cd inner join pri" & NomeEmpresaDestino & ".dbo.linhasdoc ln on ln.IdCabecDoc=cd.Id where cd.TipoDoc='ECL' and ln.CDU_IDLinhaOriginalGrupo= (select top 1 concat('{',lt.IdLinhasDocOrigem,'}') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha & "')")

                                If ListSQL.Vazia = True Then
                                    Sair = True
                                    ListSQL1 = BSO.Consulta("select concat (cd.CDU_DocumentoCompraDestino, ' e ', cd.CDU_DocumentoVendaDestino) as 'Doc' from LinhasDocTrans lt inner join LinhasDoc ln on lt.IdLinhasDocOrigem=ln.Id inner join CabecDoc cd on cd.Id=ln.IdCabecDoc where lt.IdLinhasDoc='" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha & "'")
                                    ListSQL1.Inicio()

                                    msg = msg & vbCrLf & " Artigo:  " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & " Lote: " & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" & vbCrLf & "Documentos: " & ListSQL1.Valor("Doc") & ""



                                End If

                            End If
                        Next i


                        If Sair = True Then

                            MsgBox(msg & vbCrLf & vbCrLf & vbCrLf & "Tem de criar as guias manualmente na empresa " & NomeEmpresaDestino, vbInformation + vbOKOnly, "Rastreabilidade entre empresas")
                            Exit Function
                        End If
                    End If
                    '********************************************************************************************************************************************
                    'Bruno - Verifica se existe rastreabilidade em todas as linhas da GR na empresa destino
                    '********************************************************************************************************************************************



                    'Gerar encomenda a fornecedor
                    Mdi_GeraDocumentoCompra.GerarDocumento_BaseVendas(DocumentoModelo, NomeEmpresaDestino, TipoDocComprasDestino, SerieComprasDestino, EntidadeDestino, ArmazemDestino)
                    'mdl_GeraDocumentoCompra.GerarDocumento Filial_Atual, Serie_Atual, TipoDoc_Atual, NumDoc_Atual, NomeEmpresaDestino, TipoDocComprasDestino, SerieComprasDestino, EntidadeDestino, ArmazemDestino

                    Dim ValorASomarArtigo As Double
                    'ValorASomarArtigo = BSO.Comercial.TabVendas.Edita(TipoDoc_Atual).CamposUtil("CDU_MargemPassagemArtigo").Valor & ""

                    'Se estiver definido no documento atual, significa que já invocou o formulário (pop up) do cliente final
                    If Len(Me.DocumentoVenda.CamposUtil("CDU_MargemPassagemArtigo").Valor & "") > 0 Then
                        ValorASomarArtigo = Me.DocumentoVenda.CamposUtil("CDU_MargemPassagemArtigo").Valor & ""
                    Else
                        ValorASomarArtigo = BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil("CDU_MargemPassagemArtigo").Valor & ""
                    End If

                    Dim TipoDocVendasDestino As String
                    TipoDocVendasDestino = UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil("CDU_TipoDocVendasDestino").Valor & "")

                    If Len(TipoDocVendasDestino) = 0 Then
                        'MsgBox "O campo de utilizador 'TipoDoc Vendas Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", vbInformation + vbOKOnly
                        RegistarDocumentosEmpresaGrupo = True
                        Exit Function
                    End If

                    Dim SerieVendasDestino As String
                    Dim SQLstr As String
                    Dim PaisClienteFinal As StdBELista

                    SQLstr = "select c.Pais from pri" & NomeEmpresaDestino & ".dbo.Clientes c where c.Cliente='" & Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & "'"
                    PaisClienteFinal = BSO.Consulta(SQLstr)

                    PaisClienteFinal.Inicio()

                    If Me.DocumentoVenda.Tipodoc = "GR" And PaisClienteFinal.Valor("Pais") <> "PT" Then
                        SerieVendasDestino = UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil("CDU_SerieMEVendasDestino").Valor & "")
                    Else
                        SerieVendasDestino = UCase(BSO.Vendas.TabVendas.Edita(TipoDoc_Atual).CamposUtil("CDU_SerieVendasDestino").Valor & "")
                    End If

                    If Len(SerieVendasDestino) = 0 Then
                        'MsgBox "O campo de utilizador 'Serie Vendas Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", vbInformation + vbOKOnly
                        RegistarDocumentosEmpresaGrupo = True
                        Exit Function
                    End If

                    'Entidade colocada no formulário que é exibido (o formulário só é mostrado se o cliente colocado no documento pertencer à empresa de Grupo (so pertence à empresa de grupo se a entidade tiver o campo de utilizador CDU_NomeEmpresaGrupo preenchido))
                    EntidadeDestino = Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & ""


                    If Me.DocumentoVenda.Tipodoc = "GR" And TipoDocVendasDestino = "GR" Then
                        If MsgBox("Pretende gerar o Documento 'GR' no Cliente Final '" & NomeEmpresaDestino & "'?", vbQuestion + vbYesNo, "Guia de Remessa") = vbYes Then
                            DocumentoModelo = New VndBE100.VndBEDocumentoVenda
                            DocumentoModelo = BSO.Vendas.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual)
                            Mdi_GeraDocumentoVenda.GerarDocumento_BaseVendas(DocumentoModelo, NomeEmpresaDestino, TipoDocVendasDestino, SerieVendasDestino, EntidadeDestino, ArmazemDestino, ValorASomarArtigo)
                        End If
                    Else
                        DocumentoModelo = New VndBE100.VndBEDocumentoVenda
                        DocumentoModelo = BSO.Vendas.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual)
                        Mdi_GeraDocumentoVenda.GerarDocumento_BaseVendas(DocumentoModelo, NomeEmpresaDestino, TipoDocVendasDestino, SerieVendasDestino, EntidadeDestino, ArmazemDestino, ValorASomarArtigo)
                    End If


                End If

                RegistarDocumentosEmpresaGrupo = True
            End If

            Exit Function

TrataErro:
            MsgBox(Err.Description, vbCritical + vbOKOnly, "Registar Documentos na Empresa do Grupo")
        End Function


        'Edusamp
        Dim NomeEmpresaDestino As String
        Dim InstanciaEmpresaDestino As String
        'Edusamp
        Private Sub IdentificarClienteEmpresaGrupo(ByVal Cliente As String)

            '********************************************************************************************************************************************
            'Eduardo Sampaio 2016.12.21 (inicio) 'Edusamp
            '********************************************************************************************************************************************

            'Sempre que o cliente é identificado, apago a entidade final
            'Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor = ""

            'Se o cliente não tiver empres do Grupo Preenchido, sai da função
            NomeEmpresaDestino = BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor & ""
            If Len(NomeEmpresaDestino) = 0 Then Exit Sub


            'Se o documento não tiver parameterizado documentos de Venda (Encomenda de Cliente) não vai mostrar o formulário
            Dim TipoDocVendasDestino As String
            TipoDocVendasDestino = UCase(BSO.Vendas.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).CamposUtil("CDU_TipoDocVendasDestino").Valor & "")
            If Len(TipoDocVendasDestino) = 0 Then Exit Sub

            'Se o documento não tiver parameterizado Serie para os documentos de Venda (Encomenda de Cliente) não vai mostrar o formulário
            Dim SerieVendasDestino As String
            SerieVendasDestino = UCase(BSO.Vendas.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).CamposUtil("CDU_SerieVendasDestino").Valor & "")
            If Len(SerieVendasDestino) = 0 Then Exit Sub


            'Verificar se o cliente associado realmente pertence à empresa de grupo. Não basta apenas estar preenchido!
            'Aproveito tambem para identificar a instancia...
            Dim stdBE_ListaEmpresasGrupo As StdBELista
            stdBE_ListaEmpresasGrupo = BSO.Consulta("SELECT CDU_Empresa, CDU_Nome, CDU_Instancia " &
                                                        "  FROM PRIEMPRE.dbo.TDU_EmpresasGrupo " &
                                                        "  WHERE CDU_Empresa = '" & NomeEmpresaDestino & "' ")

            If Not stdBE_ListaEmpresasGrupo.Vazia Then

                stdBE_ListaEmpresasGrupo.Inicio()
                NomeEmpresaDestino = stdBE_ListaEmpresasGrupo.Valor("CDU_Empresa")
                InstanciaEmpresaDestino = stdBE_ListaEmpresasGrupo.Valor("CDU_Instancia")

                'Exibir o formulário para escolha do cliente final
                Dim FormularioClientes As New FrmClientesView
                FormularioClientes.EmpresaDestino = NomeEmpresaDestino
                FormularioClientes.PRIEmpresaDestino = "PRI" & NomeEmpresaDestino


                FormularioClientes.TextEditCodigoCliente.EditValue = Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & ""
                FormularioClientes.TextEditDescricaoCliente.EditValue = Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupoNome").Valor & "" '#20180219

                'Se o campo de utilizador "CDU_ArmazemGrupo" do documento estiver preenchido, carrego o pop up com ele
                If Len(Me.DocumentoVenda.CamposUtil("CDU_ArmazemGrupo").Valor & "") > 0 Then
                    FormularioClientes.TextEditCodigoArmazem.EditValue = Me.DocumentoVenda.CamposUtil("CDU_ArmazemGrupo").Valor & ""
                Else
                    FormularioClientes.TextEditCodigoArmazem.EditValue = BSO.Base.Clientes.DaValorAtributo(Me.DocumentoVenda.Entidade, "CDU_ArmazemGrupo")
                End If

                'Se o campo de utilizador "CDU_MoradaAlternativa" do documento estiver preenchido, carrego o pop up com ele
                If Len(Me.DocumentoVenda.CamposUtil("CDU_MoradaAlternativa").Valor & "") > 0 Then
                    FormularioClientes.TextEditCodigoLocalDescarga.EditValue = Me.DocumentoVenda.CamposUtil("CDU_MoradaAlternativa").Valor & ""
                Else
                    FormularioClientes.TextEditCodigoLocalDescarga.EditValue = Me.DocumentoVenda.MoradaAlternativaEntrega
                End If

                If Len(Me.DocumentoVenda.CamposUtil("CDU_MargemPassagemArtigo").Valor & "") <> 1 Then
                    FormularioClientes.TextEditCodigoMargem.EditValue = Me.DocumentoVenda.CamposUtil("CDU_MargemPassagemArtigo").Valor & ""
                Else
                    'Sugere sempre a do parametro!
                    FormularioClientes.TextEditCodigoMargem.EditValue = BSO.Vendas.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).CamposUtil("CDU_MargemPassagemArtigo").Valor & ""
                End If

                FormularioClientes.TextEditCodigoMargem.EditValue = Replace(FormularioClientes.TextEditCodigoMargem.EditValue, ".", "") '#2018.03.08

                Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmClientesView))

                If result.ResultCode = ExtensibilityResultCode.Ok Then

                    Dim frm As FrmClientesView = result.Result
                    frm.ShowDialog()

                End If

                If result.ResultCode = ExtensibilityResultCode.Ok Then

                    Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor = FormularioClientes.TextEditCodigoCliente.EditValue
                    Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupoNome").Valor = FormularioClientes.TextEditDescricaoCliente.EditValue '#20180219
                    Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor = FormularioClientes.TextEditCodigoIdioma.EditValue
                    Me.DocumentoVenda.CamposUtil("CDU_MargemPassagemArtigo").Valor = Replace(FormularioClientes.TextEditCodigoMargem.EditValue, ".", "") '#2018.03.08
                    Me.DocumentoVenda.CamposUtil("CDU_ArmazemGrupo").Valor = FormularioClientes.TextEditCodigoArmazem.EditValue
                    Me.DocumentoVenda.CamposUtil("CDU_MoradaAlternativa").Valor = FormularioClientes.TextEditCodigoLocalDescarga.EditValue


                    '#20180219 (inicio)
                    'Se cliente final definido e local de descarga não, atribuir a morada de faturação!
                    If Len(FormularioClientes.TextEditDescricaoCliente.EditValue) > 0 And FormularioClientes.TextEditCodigoLocalDescarga.EditValue = "" Then
                        'Identificar a morada Faturação!
                        Me.DocumentoVenda.MoradaAlternativaEntrega = ""
                        '#2018.03.08
                        Me.DocumentoVenda.MoradaEntrega = Left(FormularioClientes.TextEditDescricaoCliente.EditValue, 50)
                        'Me.DocumentoVenda.Morada2Entrega = Left(FormularioClientes.txtDescricaoCliente.Text, 50)
                        'Me.DocumentoVenda.MoradaEntrega = Left(FormularioClientes.txtDescricaoCliente.Text, 10) & " - " & Left(BSO.Comercial.Clientes.DaValorAtributo(FormularioClientes.txtCodigoCliente.Text, "Fac_Mor"), 37)
                        Me.DocumentoVenda.Morada2Entrega = Left(BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue, "Fac_Mor"), 50)
                        Me.DocumentoVenda.LocalidadeEntrega = BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue, "Fac_Local")
                        Me.DocumentoVenda.CargaDescarga.CodPostalEntrega = BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue, "Fac_cp")
                        Me.DocumentoVenda.CargaDescarga.CodPostalLocalidadeEntrega = BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue, "Fac_CpLoc")
                        Me.DocumentoVenda.CargaDescarga.DistritoEntrega = BSO.Base.Clientes.DaValorAtributo(FormularioClientes.TextEditCodigoCliente.EditValue, "Distrito")
                    Else

                        'Esta tem de ser vazia porque não existe. A morada que estamos a falar é da empresa do Grupo.
                        Me.DocumentoVenda.MoradaAlternativaEntrega = ""
                        '#2018.03.08
                        Me.DocumentoVenda.MoradaEntrega = Left(FormularioClientes.TextEditDescricaoCliente.EditValue, 50)
                        'Me.DocumentoVenda.Morada2Entrega = Left(FormularioClientes.txtDescricaoCliente.Text, 50)
                        'Me.DocumentoVenda.MoradaEntrega = Left(FormularioClientes.txtDescricaoCliente.Text, 10) & " - " & Left(FormularioClientes.txtCodigoMoradaEntrega.Text, 37)
                        Me.DocumentoVenda.Morada2Entrega = Left(FormularioClientes.TextEditCodigoMoradaEntrega2.EditValue, 50)
                        Me.DocumentoVenda.LocalidadeEntrega = FormularioClientes.TextEditCodigoLocalidadeEntrega.EditValue
                        Me.DocumentoVenda.CargaDescarga.CodPostalEntrega = FormularioClientes.TextEditCodigoPostalEntrega.EditValue
                        Me.DocumentoVenda.CargaDescarga.CodPostalLocalidadeEntrega = FormularioClientes.TextEditCodPostalLocalidadeEntrega.EditValue
                        Me.DocumentoVenda.CargaDescarga.DistritoEntrega = FormularioClientes.TextEditDistritoEntrega.EditValue
                    End If
                    '#20180219 (Fim)


                End If

                'JFC 05/08/2020 - Identifica Observações na ficha do cliente quando a venda é feita entre empresas. Pedido de Ze Luis.
                If Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & "" <> "" And Me.DocumentoVenda.Tipodoc = "ECL" Then
                        Dim listEntidadeFinal As StdBELista

                        listEntidadeFinal = BSO.Consulta("select c2.Cliente from pri" & BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor & ".dbo.Clientes c  inner join Clientes c2 on c2.CDU_EntidadeInterna=c.CDU_EntidadeInterna where c.Cliente='" & Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo") & "'")
                        listEntidadeFinal.Inicio()

                        If listEntidadeFinal.Vazia = False Then
                            If BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).CamposUtil("CDU_ObsEncomenda").Valor & "" <> "" Then
                                MsgBox(BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).CamposUtil("CDU_ObsEncomenda").Valor, vbInformation + vbOKOnly)
                            End If

                            'JFC 23/09/2020 - Colocar a Moeda do Cliente final na Encomenda. Pedido de Mafalda após surgir faturas em euros para o cliente sallis (libras)
                            Me.DocumentoVenda.Moeda = BSO.Base.Clientes.Edita(listEntidadeFinal.Valor("Cliente")).Moeda
                            'JFC 03/08/2021 Colocado o Cambio após ser detectado que uma ECL da Sallis (Libras) não estava assumir o cambio.
                            Me.DocumentoVenda.Cambio = BSO.Base.Moedas.DaCambioCompra(Me.DocumentoVenda.Moeda, Now)
                        End If
                    End If

                Else

                    MsgBox("O cliente " & Me.DocumentoVenda.Entidade & " - " & Me.DocumentoVenda.DescEntidade & " tem empresa de grupo definida mas esta não consta na tabela TDU_EmpresasGrupo!", vbInformation + vbOKOnly)
                Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor = ""
            End If


            '********************************************************************************************************************************************
            'Eduardo Sampaio 2016.12.21 (Fim) 'Edusamp
            '********************************************************************************************************************************************
        End Sub

        Private Function IdentificarArmazemDaEncomendaFornecedor(
                            ByVal Filial_FaturaCliente As String,
                            ByVal Serie_FaturaCliente As String,
                            ByVal TipoDoc_FaturaCliente As String,
                            ByVal NumDoc_FaturaCliente As Long,
                            ByVal BaseDadosDestino As String) As String

            Dim Str_Rastreabilidade As String
            Dim Lst_StrRastreabilidade As StdBELista

            Str_Rastreabilidade = "  SELECT  COALESCE(LDGrupo.Armazem,'') as Armazem " &
                            " FROM CabecDoc  CD " &
                            " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60" &
                            " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " &
                            " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " &
                            " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " &
                            " INNER JOIN PRI" & BaseDadosDestino & ".dbo.LinhasCompras LDGrupo ON '{' + convert(nvarchar(50),  LDGrupo.id) + '}'  =  LD2.CDU_IDLinhaOriginalGrupo " &
                            " INNER JOIN PRI" & BaseDadosDestino & ".dbo.CabecCompras CDGrupo ON CDGrupo.id = LDGrupo.IdCabecCompras " &
                            " WHERE  CD.Filial = '" & Filial_FaturaCliente & "' AND CD.TipoDoc = '" & TipoDoc_FaturaCliente & "' AND CD.serie = '" & Serie_FaturaCliente & "' AND CD.NumDoc = " & NumDoc_FaturaCliente & " " &
                            " ORDER BY LD.NumLinha "
            Lst_StrRastreabilidade = BSO.Consulta(Str_Rastreabilidade)

            If Lst_StrRastreabilidade.Vazia = False Then

                Lst_StrRastreabilidade.Inicio()

                IdentificarArmazemDaEncomendaFornecedor = Lst_StrRastreabilidade.Valor("Armazem")
            Else
                IdentificarArmazemDaEncomendaFornecedor = ""
            End If

        End Function


    End Class
End Namespace
