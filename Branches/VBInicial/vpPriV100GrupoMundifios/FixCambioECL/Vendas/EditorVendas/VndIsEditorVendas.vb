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

Namespace FixCambioECL
    Public Class VndNsEditorVendas
        Inherits EditorVendas
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            '#################################################################################################
            '####### Coloca ultimo Cambio na Linha da ECL. Pedido de Goreti - JFC 28-02-2019      ############
            '#################################################################################################

            If Module1.VerificaToken("FixCambioECL") = 1 Then

                If Me.DocumentoVenda.Tipodoc = "ECL" Then
                    Dim cambio As StdBELista
                    Dim Moeda As StdBELista
                    Dim loteChange As StdBELista

                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens

                        If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "" <> "" Then


                            'Identifica a moeda de compra
                            Moeda = BSO.Consulta("select top 1 cc.Moeda from CabecCompras cc inner join LinhasCompras lc on lc.IdCabecCompras=cc.Id inner join Fornecedores f on f.Fornecedor=cc.Entidade where f.CDU_EntidadeInterna not in ('0001','0002','0003','0004','0005','0006') and lc.Armazem!='PLA' and cc.TipoDoc in ('CNT','ECF') and  lc.Artigo='" & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "' and lc.Lote='" & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "'")
                            Moeda.Inicio()

                            'Se a linha não tiver cambio atribuido, então atribui um consoante a Moeda
                            If IsNothing(Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Cambio").Valor) Or Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Cambio").Valor = 0 Then


                                If Moeda.Vazia = False Then
                                    If Moeda.Valor("Moeda") = "EUR" Then
                                        Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Cambio").Valor = 1
                                    Else
                                        cambio = BSO.Consulta("select top 1 m.Venda from MoedasHistorico m where m.Moeda='" & Moeda.Valor("Moeda") & "' order by m.Data desc")
                                        cambio.Inicio()
                                        Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Cambio").Valor = cambio.Valor("Venda")
                                    End If
                                End If

                            Else
                                'JFC - 25/05/2020
                                'Caso tenha uma cambio atribuido, valida se o lote foi alterado.
                                loteChange = BSO.Consulta("select top 1 ln.Lote from LinhasDoc ln where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(j).IdLinha & "'")
                                loteChange.Inicio()

                                If loteChange.Vazia = False Then
                                    If loteChange.Valor("Lote") <> Me.DocumentoVenda.Linhas.GetEdita(j).Lote Then
                                        'Caso o lote tenha sido alterado é necessário validar se a moeda de commpra se altera, e associar o cambio correto.
                                        'Esta parte do codigo considera apenas trocas de EUR e DOLLARS, caso haja mais moedas envolvida, por exemplo trocas de LIBRAS para DOLLARS, terá que se corrigido.
                                        If Moeda.Valor("Moeda") = "EUR" Then
                                            Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Cambio").Valor = 1

                                        ElseIf Moeda.Valor("Moeda") <> "EUR" And Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Cambio").Valor = "1" Then
                                            cambio = BSO.Consulta("select top 1 m.Venda from MoedasHistorico m where m.Moeda='" & Moeda.Valor("Moeda") & "' order by m.Data desc")
                                            cambio.Inicio()
                                            Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Cambio").Valor = cambio.Valor("Venda")
                                        End If
                                    End If

                                End If
                            End If
                        End If

                    Next j
                End If
                '#################################################################################################
                '####### Coloca ultimo Cambio na Linha da ECL. Pedido de Goreti - JFC 28-02-2019      ############
                '#################################################################################################

            End If

        End Sub


    End Class
End Namespace
