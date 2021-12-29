Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgsPublic
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdPlatBS100.StdBSTipos
Imports BasBE100.BasBETiposGcp

Namespace InstrucaoAcabamento
    Public Class VndNsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub ArtigoIdentificado(Artigo As String, NumLinha As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ArtigoIdentificado(Artigo, NumLinha, Cancel, e)

            If Module1.VerificaToken("InstrucaoAcabamento") = 1 Then

                If Me.DocumentoVenda.Tipodoc = "ECL" Or Me.DocumentoVenda.Tipodoc = "GR" Then

                    Me.DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil("CDU_DataEntregaCliente").Valor = Me.DocumentoVenda.DataDoc

                    If Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao Like "*Seacell*" Then
                        If Me.DocumentoVenda.Pais = "PT" Then
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por Seacell por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.")
                        Else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by Seacell fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.")
                        End If
                    End If
                    If Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao Like "*Sensitive*" Then
                        If Me.DocumentoVenda.Pais = "PT" Then
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por SmartCel Sensitive por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.")
                        Else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by SmartCel Sensitive fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.")
                        End If
                    End If
                    If Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao Like "*Protection*" Then
                        If Me.DocumentoVenda.Pais = "PT" Then
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por CellSolution Protection por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.")
                        Else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by CellSolution Protection fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.")
                        End If
                    End If
                    If Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao Like "*Clima*" Then
                        If Me.DocumentoVenda.Pais = "PT" Then
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por CellSolution Clima por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.")
                        Else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by CellSolution Clima fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.")
                        End If
                    End If
                    If Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao Like "*Skin Care*" Then
                        If Me.DocumentoVenda.Pais = "PT" Then
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por CellSolution Skin Care por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.")
                        Else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by CellSolution Skin Care fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.")
                        End If
                    End If


                End If
            End If
        End Sub

    End Class
End Namespace