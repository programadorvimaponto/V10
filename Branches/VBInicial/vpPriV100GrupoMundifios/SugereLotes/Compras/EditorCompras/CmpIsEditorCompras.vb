Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace SugereLotes
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub ArtigoIdentificado(Artigo As String, NumLinha As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ArtigoIdentificado(Artigo, NumLinha, Cancel, e)


            If Module1.VerificaToken("SugereLotes") = 1 Then

                '############################################################################################
                '####              JFC 21/10/2019 Sugestão de Lotes                            ##############
                '############################################################################################

                If Me.DocumentoCompra.Entidade & "" <> "" And BSO.Compras.TabCompras.Edita(Me.DocumentoCompra.Tipodoc).TipoDocumento = 2 And (BSO.Base.Artigos.Edita(Artigo).Descricao Like "Fio*" Or BSO.Base.Artigos.Edita(Artigo).Descricao Like "Rama*") Then
                    'Sugestão de lote
                    Dim i As Long
                    Dim ent As String
                    Dim lote As Integer
                    Dim loteAux As Integer
                    Dim listLote As StdBELista

                    ent = Me.DocumentoCompra.Entidade
                    loteAux = 0
                    'Consulta à função dbo.fnProximoLote de qual o proximo lote a utilizar.
                    listLote = BSO.Consulta("select PRIMUNDIFIOS.dbo.fnProximoLote('" & BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).CamposUtil("CDU_EntidadeInterna").Valor & "','" & Artigo & "') as 'Lote'")

                    listLote.Inicio()

                    'Primeira validação, ver se já existe nas outras linhas algum lote já inserido
                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens

                        If Me.DocumentoCompra.Linhas.GetEdita(i).Artigo = Artigo And i <> NumLinha And Len(Me.DocumentoCompra.Linhas.GetEdita(i).Lote) = 8 Then

                            lote = CInt(Right(Me.DocumentoCompra.Linhas.GetEdita(i).Lote, 4))
                            If lote > loteAux Then
                                loteAux = lote
                            End If

                        End If
                    Next i

                    'Se já existir o lote noutra linha, então lote nove = lote +1
                    If loteAux <> 0 Then
                        loteAux = loteAux + 1

                        'Validar se o lote novo é igual ou superior ao lote sugerido pela funcção dbo.fnProximoLote
                        If listLote.Vazia Or listLote.Valor("Lote") = "" Then
                            i = 4 - Len(CStr(loteAux))
                            Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = "" & ent & Left("0000", i) & loteAux
                        Else
                            If loteAux <= CInt(Right(listLote.Valor("Lote"), 4)) Then
                                Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = listLote.Valor("Lote")
                            Else
                                i = 4 - Len(CStr(loteAux))
                                Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = "" & ent & Left("0000", i) & loteAux
                            End If
                        End If

                        'Não foi detectado nenhum lote inserido
                    Else

                        'Se não existir nenhum lote sugerido pela funcção dbo.fnProximoLote, então é o primeiro lote
                        If listLote.Vazia Or listLote.Valor("Lote") = "" Then
                            Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = "" & ent & "0001"
                            'Lote sugerido pela função
                        Else
                            Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = listLote.Valor("Lote")
                        End If
                    End If
                End If
                '############################################################################################
                '####              JFC 21/10/2019 Sugestão de Lotes                            ##############
                '############################################################################################


            End If

        End Sub


    End Class
End Namespace
