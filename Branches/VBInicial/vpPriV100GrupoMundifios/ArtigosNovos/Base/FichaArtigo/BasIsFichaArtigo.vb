Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace ArtigosNovos
    Public Class BasIsFichaArtigo
        Inherits FichaArtigos

        Dim ArtigoCopiar As String
        Dim ArtigoNovo As Boolean

        Public Overrides Sub AntesDeCriar(e As ExtensibilityEventArgs)
            MyBase.AntesDeCriar(e)

            If Module1.VerificaToken("ArtigosNovos") = 1 Then
                ArtigoNovo = True
            End If

        End Sub

        Public Overrides Sub AntesDeEditar(Artigo As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeEditar(Artigo, Cancel, e)

            If Module1.VerificaToken("ArtigosNovos") = 1 Then
                ArtigoCopiar = Artigo

                ArtigoNovo = False



            End If

        End Sub

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("ArtigosNovos") = 1 Then

                If Me.Artigo.CamposUtil("CDU_DescricaoExtra").Valor & "" = "" Then
                    Me.Artigo.CamposUtil("CDU_DescricaoExtra").Valor = " "
                End If


                If Me.Artigo.Artigo <> ArtigoCopiar Then

                    If ArtigoNovo = False Then
                        Me.ArtigoIdiomas.RemoveTodos()
                    End If
                    ArtigoCopiar = ""

                End If

                If ArtigoNovo = True Then

                    Me.Artigo.CamposUtil("CDU_DataCriacao").Valor = Now


                End If

                If Me.Artigo.TipoArtigo = 3 And (Me.Artigo.IntrastatCodigoPautal & "" = "" Or Me.Artigo.IntrastatPesoLiquido & "" = "") Then

                    MsgBox("Atenção:" & Chr(13) & "É obrigatório o preenchimento do código pautal (intrastat) e do respetivo peso líquido (1) no caso dos artigos do tipo mercadoria", vbCritical + vbOKOnly)
                End If

                If Me.Artigo.CamposUtil("CDU_DescricaoInterna").Valor & "" = "" Or ArtigoNovo = True Then
                    Me.Artigo.CamposUtil("CDU_DescricaoInterna").Valor = Me.Artigo.Descricao
                End If
            End If
        End Sub

    End Class
End Namespace