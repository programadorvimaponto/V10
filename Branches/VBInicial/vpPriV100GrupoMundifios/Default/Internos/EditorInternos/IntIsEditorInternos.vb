Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Internal.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace DefaultToken
    Public Class IntIsEditorInternos
        Inherits EditorInternos

        Public Overrides Sub ArtigoIdentificado(Artigo As String, NumLinha As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ArtigoIdentificado(Artigo, NumLinha, Cancel, e)

            If Module1.VerificaToken("Default") = 1 Then



                If BSO.Base.Artigos.DaValorAtributo(Me.DocumentoInterno.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra") & "" <> "" Then
                    Me.DocumentoInterno.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") & " " & BSO.Base.Artigos.DaValorAtributo(Me.DocumentoInterno.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra")
                    Me.DocumentoInterno.Linhas.GetEdita(NumLinha).CamposUtil("CDU_ReferenciaCliente").Valor = BSO.Base.Artigos.DaValorAtributo(Me.DocumentoInterno.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra")


                End If

            End If

        End Sub

    End Class
End Namespace