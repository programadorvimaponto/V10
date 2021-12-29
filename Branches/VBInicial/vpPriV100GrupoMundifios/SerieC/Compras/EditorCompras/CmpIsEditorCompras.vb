Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace SerieC
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("SerieC") = 1 Then

                'JFC - Antes de gravar garantir que um documento da serie C é transformado para outro documento da serie C - Pedido de Mafalda 15/10/2018
                If Me.DocumentoCompra.Tipodoc = "VGR" Or Me.DocumentoCompra.Tipodoc = "VFA" Then
                    Dim j As Long
                    Dim SerieC As StdBELista

                    If Right(Me.DocumentoCompra.Serie, 1) <> "C" Then
                        For j = 1 To Me.DocumentoCompra.Linhas.NumItens
                            If Me.DocumentoCompra.Linhas.GetEdita(j).IDLinhaOriginal & "" <> "" And Me.DocumentoCompra.Linhas.GetEdita(j).Artigo & "" <> "" Then


                                SerieC = BSO.Consulta("select top 1 right(cd.serie,1) as Serie from cabeccompras cd inner join linhascompras ln on ln.idcabeccompras=cd.id where ln.id='" & Me.DocumentoCompra.Linhas.GetEdita(j).IDLinhaOriginal & "'")
                                SerieC.Inicio()

                                If SerieC.Valor("Serie") = "C" Then
                                    MsgBox("Atenção está a transformar um documento da Serie C para outra Serie: " & Me.DocumentoCompra.Linhas.GetEdita(j).Artigo & " - " & Me.DocumentoCompra.Linhas.GetEdita(j).Lote, vbCritical + vbOKOnly)
                                    Cancel = True
                                End If
                            End If
                        Next j
                    End If

                End If

            End If

        End Sub

    End Class
End Namespace