Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace ArmazemEntreposto
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("ArmazemEntreposto") = 1 Then


                If BSO.Compras.TabCompras.Edita(Me.DocumentoCompra.Tipodoc).TipoDocumento = 4 _
        And BSO.Compras.TabCompras.Edita(Me.DocumentoCompra.Tipodoc).PagarReceber = "P" Then

                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens

                        If Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoCompra.Linhas.GetEdita(i).Armazem = "AEP" Then

                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_DespDAU", Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_DespDAU").Valor)
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_Regime", Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_Regime").Valor)
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_CodMerc", Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_CODMERC").Valor)
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_Contramarca", Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_Contramarca").Valor)
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_ContramarcaData", Format(Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_ContramarcaData").Valor, "yyyy-MM-dd"))

                        End If

                    Next i

                End If

            End If
        End Sub

    End Class
End Namespace