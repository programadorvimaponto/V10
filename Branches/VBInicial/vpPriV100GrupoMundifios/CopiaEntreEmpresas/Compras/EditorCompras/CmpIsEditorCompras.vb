Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace CopiaEntreEmpresas
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then

                'EduSamp
                If Not BSO.Compras.Documentos.Existe(Me.DocumentoCompra.Filial, Me.DocumentoCompra.Tipodoc, Me.DocumentoCompra.Serie, Me.DocumentoCompra.NumDoc) Then
                    Me.DocumentoCompra.CamposUtil("CDU_DocumentoVendaDestino").Valor = ""
                    Me.DocumentoCompra.CamposUtil("CDU_DocumentoCompraDestino").Valor = ""
                End If

            End If
        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then
                'EduSamp  
                RegistarDocumentosEmpresaGrupo(Filial, Tipo, Serie, NumDoc)

            End If

        End Sub

        Dim NomeEmpresaDestino As String
        Private Function RegistarDocumentosEmpresaGrupo(ByVal Filial_Atual As String,
                                                ByVal TipoDoc_Atual As String,
                                                ByVal Serie_Atual As String,
                                                ByVal NumDoc_Atual As Long) As Boolean

            On Error GoTo TrataErro

            Dim Mensagem As String

            'Se algum dos documentos de destino já tiverem sido gerados, não faz qualquer alteração na empresa de destino!
            If Len(Me.DocumentoCompra.CamposUtil("CDU_DocumentoVendaDestino").Valor & "") > 0 Then
                Mensagem = "O Documento atual já tinha dado origem ao(s) seguinte(s) documento(s) na empresa de Grupo: " & Chr(13) & Chr(13) & "" & Me.DocumentoCompra.CamposUtil("CDU_DocumentoVendaDestino").Valor & "" & Chr(13) & "" & Chr(13) & Chr(13) & "Caso tenha efetuado altearções, deverá replicar manualmente na empresa de Grupo."
                MsgBox(Mensagem, vbInformation + vbOKOnly)
                RegistarDocumentosEmpresaGrupo = True : Exit Function

            End If

            If Len(Me.DocumentoCompra.CamposUtil("CDU_DocumentoOrigem").Valor & "") > 0 Then

                Mensagem = "O Documento atual já tinha sido gerado através do seguinte documento na empresa de Grupo: " & Chr(13) & Chr(13) & "" & Me.DocumentoCompra.CamposUtil("CDU_DocumentoOrigem").Valor & "" & Chr(13) & "" & Chr(13) & Chr(13) & "Não irá gerar nenhum documento na empresa do Grupo."
                MsgBox(Mensagem, vbInformation + vbOKOnly)
                RegistarDocumentosEmpresaGrupo = True : Exit Function

            End If

            'Validação pedida pelo eng. Joaquim Costa
            If Right(Me.DocumentoCompra.Serie, 1) = "x" Or Right(Me.DocumentoCompra.Serie, 1) = "X" Then
                RegistarDocumentosEmpresaGrupo = True : Exit Function
            End If

            'Associado ao fornecedor
            NomeEmpresaDestino = UCase(BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "")

            If Len(NomeEmpresaDestino) = 0 Then

                RegistarDocumentosEmpresaGrupo = True
                Exit Function

            End If

            'Se entidade for empres do Grupo..
            'Gerar Encomenda de Cliente
            Dim EntidadeDestino As String
            EntidadeDestino = BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).CamposUtil("CDU_CodigoFornecedorGrupo").Valor & ""

            If Len(EntidadeDestino) = 0 Then
                'Tem de avisar porque se tem o nome da empresa, tem de ter codigo!..
                MsgBox("O campo de utilizador 'Cód. Fornecedor Grupo' da entidade do Grupo '" & Me.DocumentoCompra.Entidade & "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", vbInformation + vbOKOnly)
                RegistarDocumentosEmpresaGrupo = False
                Exit Function

            End If

            Dim TipoDocVendasDestino As String
            TipoDocVendasDestino = UCase(BSO.Compras.TabCompras.Edita(TipoDoc_Atual).CamposUtil("CDU_TipoDocVendasDestino").Valor & "")

            If Len(TipoDocVendasDestino) = 0 Then
                'Não avisa porque o documento pode não estar parametrizado para fazer documentos nas empresas do grupo
                'MsgBox "O campo de utilizador 'Tipo Doc Vendas Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", vbInformation + vbOKOnly
                RegistarDocumentosEmpresaGrupo = True
                Exit Function

            End If


            Dim SerieVendasDestino As String
            SerieVendasDestino = UCase(BSO.Compras.TabCompras.Edita(TipoDoc_Atual).CamposUtil("CDU_SerieVendasDestino").Valor & "")

            If Len(SerieVendasDestino) = 0 Then
                'Avisa por que se tem tipo de documento tem de ter serie também
                MsgBox("O campo de utilizador 'Serie Vendas Destino' do Documento '" & TipoDoc_Atual & "' não está preenchido, pelo que não será possível gerar a Encomenda de Cliente na empresa de Grupo", vbInformation + vbOKOnly)
                RegistarDocumentosEmpresaGrupo = False
                Exit Function

            End If

            'Entidade colocada no formulário que é exibido (o formulário só é mostrado se o cliente colocado no documento pertencer à empresa de Grupo (so pertence à empresa de grupo se a entidade tiver o campo de utilizador CDU_NomeEmpresaGrupo preenchido))

            Dim ArmazemDestino As String

            'Identificar o armazem do parametro
            ArmazemDestino = BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).CamposUtil("CDU_ArmazemGrupo").Valor & ""

            Dim DocumentoModelo As New CmpBE100.CmpBEDocumentoCompra
            DocumentoModelo = New CmpBE100.CmpBEDocumentoCompra
            DocumentoModelo = BSO.Compras.Documentos.Edita(Filial_Atual, TipoDoc_Atual, Serie_Atual, NumDoc_Atual)

            If MsgBox("Pretende gerar documento na empresa do Grupo?", vbInformation + vbYesNo) = vbNo Then
                RegistarDocumentosEmpresaGrupo = True
                Exit Function
            End If

            Mdi_GeraDocumentoVenda.GerarDocumento_BaseCompras(DocumentoModelo, NomeEmpresaDestino, TipoDocVendasDestino, SerieVendasDestino, EntidadeDestino, ArmazemDestino)
            'mdl_GeraDocumentoVenda.GerarDocumento Filial_Atual, Serie_Atual, TipoDoc_Atual, NumDoc_Atual, NomeEmpresaDestino, TipoDocVendasDestino, SerieVendasDestino, EntidadeDestino, ArmazemDestino

            RegistarDocumentosEmpresaGrupo = True

            Exit Function

TrataErro:
            MsgBox(Err.Description, vbCritical + vbOKOnly, "Registar Documentos na Empresa do Grupo")
        End Function


    End Class
End Namespace