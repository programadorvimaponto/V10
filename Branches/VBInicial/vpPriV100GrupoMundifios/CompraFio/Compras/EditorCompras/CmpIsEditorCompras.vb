Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace CompraFio
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CompraFio") = 1 Then

                If BSO.Compras.TabCompras.Edita(Me.DocumentoCompra.Tipodoc).TipoDocumento = 2 Then

                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens

                        If BSO.Inventario.ArtigosLotes.Existe(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote) = True And Me.DocumentoCompra.Linhas.GetEdita(i).Estado = "P" And Me.DocumentoCompra.Linhas.GetEdita(i).Fechado = False Then

                            BSO.DSO.ExecuteSQL("UPDATE ARTIGOLOTE SET CDU_TIPOQUALIDADE = '" & Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_TIPOQUALIDADE").Valor & "', " _
                            & " CDU_Parafinado = '" & Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_Parafinado").Valor & "' WHERE ARTIGO = '" & Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "' AND LOTE = '" & Me.DocumentoCompra.Linhas.GetEdita(i).Lote & "'")

                            If BSO.Inventario.ArtigosLotes.Edita(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote).CamposUtil("CDU_LOTEFORN").Valor & "" = "" Then
                                BSO.DSO.ExecuteSQL("UPDATE ARTIGOLOTE SET CDU_LOTEFORN = '" & Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_LOTEFORN").Valor & "' " _
                                & "WHERE ARTIGO = '" & Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "' AND LOTE = '" & Me.DocumentoCompra.Linhas.GetEdita(i).Lote & "'")

                            End If

                        End If

                    Next i

                End If



                If BSO.Compras.TabCompras.Edita(Me.DocumentoCompra.Tipodoc).TipoDocumento = 4 Then

                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens

                        If BSO.Inventario.ArtigosLotes.Existe(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote) = True Then

                            If BSO.Inventario.ArtigosLotes.Edita(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote).CamposUtil("CDU_LOTEFORN").Valor & "" = "" Then

                                BSO.DSO.ExecuteSQL("UPDATE ARTIGOLOTE SET CDU_LOTEFORN = '" & Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_LOTEFORN").Valor & "' " _
                                & "WHERE ARTIGO = '" & Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "' AND LOTE = '" & Me.DocumentoCompra.Linhas.GetEdita(i).Lote & "'")

                            End If

                        End If

                    Next i

                End If
            End If

        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("CompraFio") = 1 Then

                If BSO.Compras.TabCompras.Edita(Me.DocumentoCompra.Tipodoc).TipoDocumento = 2 Then
                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens
                        If Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoCompra.Linhas.GetEdita(i).Lote <> "" Then
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_Fornecedor", Me.DocumentoCompra.Entidade)
                        End If
                    Next i
                End If
            End If

        End Sub

    End Class
End Namespace
