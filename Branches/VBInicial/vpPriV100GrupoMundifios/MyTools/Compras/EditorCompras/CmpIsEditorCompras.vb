Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace MyTools
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("MyTools") = 1 Then


                '####################################################################################################################################
                '#Atualizar o CDU_DataUltimaAtualizacao apos gravaçao de uma ECFBRUNO - 04/03/2020                                                  #
                '####################################################################################################################################
                If Me.DocumentoCompra.Tipodoc = "ECF" Then


                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens
                        If Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "" <> "" Then
                            Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_DataUltimaAtualizacao").Valor = Format(Now, "yyyy-MM-dd HH:mm:ss")
                        End If
                    Next i


                End If
                '####################################################################################################################################
                '#Atualizar o CDU_DataUltimaAtualizacao apos gravaçao de uma ECF BRUNO - 04/03/2020                                                 #
                '####################################################################################################################################

            End If
        End Sub

    End Class
End Namespace