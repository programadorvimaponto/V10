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
Imports BasBE100.BasBETiposGcp

Namespace AvisoCompraTaras
    Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("AvisoCompraTaras") = 1 Then

                'JFC a pedido do Jafernandes. Comprar as taras aos clientes. Sair nas Guias.
                If Me.DocumentoVenda.Tipodoc = "GR" And Me.DocumentoVenda.Pais = "PT" And Me.DocumentoVenda.Entidade <> "1207" And Me.DocumentoVenda.Entidade <> "0580" And Me.DocumentoVenda.Entidade <> "0707" Then
                    Dim lista As StdBELista
                    Dim Escreve As Boolean
                    lista = BSO.Consulta("SELECT Entidade FROM CabecDoc Where Tipodoc='GR' and Serie=" & "'" & Me.DocumentoVenda.Serie & "' and Numdoc=" & "'" & Me.DocumentoVenda.NumDoc & "'")
                    Escreve = False



                    If (lista.Vazia = True) Then
                        For i = 1 To Me.DocumentoVenda.Linhas.NumItens

                            If Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade > 10000 Then
                                'Este if tem que ser igual ao do Corpo
                                If Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0817" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1132" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1387" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1338" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1560" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0218" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0331" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0922" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0262" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0459" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1865" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1317" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1219" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1069" Then
                                    Escreve = True
                                End If


                            End If
                        Next i
                        'CABECALHO
                        If Escreve Then
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "INFORMAMOS QUE A MUNDIFIOS COMPRA A 0.05€ CADA CARTÃO E 0.50€ CADA PALETE. ")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "DO(s) SEGUINTE(s) ARTIGO(s)/LOTE(s):")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "")

                        End If

                        For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                            'CORPO
                            If Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade > 10000 Then
                                If Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0817" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1132" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1387" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1338" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1560" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0218" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0331" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0922" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0262" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "0459" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1865" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1317" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1219" Or Left(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) = "1069" Then
                                    BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, Me.DocumentoVenda.Linhas.GetEdita(i).Descricao & "/" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote)
                                End If


                            End If
                        Next i

                        'RODAPE
                        If Escreve Then
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "NA RECOLHA AS TARAS DEVERÃO ESTAR DEVIDAMENTE SEPARADAS E ORGANIZADAS.")
                        End If
                    End If

                End If

            End If
        End Sub

    End Class
End Namespace
