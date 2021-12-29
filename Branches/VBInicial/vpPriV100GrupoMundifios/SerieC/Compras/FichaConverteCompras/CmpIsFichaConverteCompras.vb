Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace SerieC
    Public Class CmpIsFichaConverteCompras
        Inherits FichaConverteCompras

        Public Overrides Sub AntesDeConverter(NumDoc As Integer, Tipodoc As String, Serie As String, Filial As String, TipodocDestino As String, SerieDestino As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeConverter(NumDoc, Tipodoc, Serie, Filial, TipodocDestino, SerieDestino, Cancel, e)

            If Module1.VerificaToken("SerieC") = 1 Then

                If Right(Serie, 1) = "C" And Right(SerieDestino, 1) <> "C" Then
                    MsgBox("Não é permitida conversões de documentos da serie C para outras series." & Chr(13) & Chr(13) & "Está a converter da serie " & Serie & " para a serie " & SerieDestino & Chr(13) & "Tera de convertar para uma serie destino " & Serie, vbCritical)
                    Cancel = True
                End If

            End If
        End Sub


    End Class
End Namespace