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
Imports StdPlatBS100.StdBSTipos

Namespace IdiomaArtigo
    Public Class VndNsVendas
        Inherits EditorVendas

        Public Overrides Sub ArtigoIdentificado(Artigo As String, NumLinha As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ArtigoIdentificado(Artigo, NumLinha, Cancel, e)


            If Module1.VerificaToken("IdiomaArtigo") = 1 Then


                'Se    Tiver entidade final
                '      Idioma do cliente Final <> Idioma do cliente Principal
                '      Se existir tradução na tabela de idiomas
                If Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & "" <> "" And
               Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor & "" <> "" And
               Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor & "" <> BSO.Base.Clientes.DaValorAtributo(Me.DocumentoVenda.Entidade, "Idioma") And
               BSO.Base.ArtigosIdiomas.DaValorAtributo(Artigo, BSO.Base.Clientes.DaValorAtributo(Me.DocumentoVenda.Entidade, "Idioma"), "Descricao") & "" <> "" Then


                    If BSO.Base.ArtigosIdiomas.DaValorAtributo(Artigo, Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor & "", "Descricao") <> "" Then
                        Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.ArtigosIdiomas.DaValorAtributo(Artigo, Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor & "", "Descricao")
                    Else
                        Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") & " " & BSO.Base.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna")

                    End If


                Else


                    If BSO.Base.Clientes.DaValorAtributo(Me.DocumentoVenda.Entidade, "Idioma") <> "PT" And
                    BSO.Base.ArtigosIdiomas.DaValorAtributo(Artigo, BSO.Base.Clientes.DaValorAtributo(Me.DocumentoVenda.Entidade, "Idioma"), "Descricao") & "" <> "" Then
                        'Se o idioma não for PT e se existir tradução, não acrescento descrição Extra
                    Else


                        If BSO.Base.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna") & "" <> "" Then
                            Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") & " " & BSO.Base.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna")
                            Me.DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil("CDU_ReferenciaCliente").Valor = BSO.Base.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna")

                            'Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao = BSO.Comercial.Artigos.DaValorAtributo(Artigo, "Descricao") & " " & BSO.Comercial.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra")
                            ' Me.DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil("CDU_ReferenciaCliente").Valor = BSO.Comercial.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra")
                        End If
                    End If

                End If
            End If

        End Sub

    End Class
End Namespace
