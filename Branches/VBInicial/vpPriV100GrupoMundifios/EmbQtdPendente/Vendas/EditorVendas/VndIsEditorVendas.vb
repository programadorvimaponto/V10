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

Namespace EmbQtdPendente
    Public Class VndNsVendas
        Inherits EditorVendas
        Dim listQtdPendente As StdBELista
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)


            If Module1.VerificaToken("EmbQtdPendente") = 1 Then


                If Me.DocumentoVenda.Tipodoc = "EMB" And Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor & "" <> "" And BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "" <> "" Then
                    listQtdPendente = BSO.Consulta("select cd.CDU_NBL, ln.* from LinhasDoc ln inner join CabecDoc cd on cd.Id=ln.IdCabecDoc where ln.IdCabecDoc='" & Me.DocumentoVenda.ID & "'")
                End If

            End If

        End Sub


        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("EmbQtdPendente") = 1 Then

                '################################################################################################################################################################
                '############# JFC - 28/10/2019 - Preenchimento do campo CDU_QtdPendenteEmb. Tem como objetivo filtrar Embarques efetivos de Embarques previstos           ######
                '################################################################################################################################################################

                If Me.DocumentoVenda.Tipodoc = "EMB" And Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor & "" <> "" And BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "" <> "" Then

                    'Primeira vez que se grava o EMB e o BL não se encontra preenchido.
                    If listQtdPendente.Vazia And Trim(Me.DocumentoVenda.CamposUtil("CDU_NBL").Valor & "") = "0" Then
                        For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                            If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" Then
                                BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_QtdPendenteEmb=isnull(ln2.CDU_QtdPendenteEmb,0)+replace('" & Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade & "',',','.') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha & "'")
                            End If
                        Next i
                    End If

                    'O EMB já foi gravado pelo menos uma vez,e continua com o BL por preencher. Necessário conferir quantidades.
                    If Not listQtdPendente.Vazia And Trim(Me.DocumentoVenda.CamposUtil("CDU_NBL").Valor & "") = "0" Then
                        For j = 1 To listQtdPendente.NumLinhas
                            listQtdPendente.Inicio()

                            For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                                If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And listQtdPendente.Valor("Id") = Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha Then
                                    If Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade <> listQtdPendente.Valor("Quantidade") Then
                                        BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_QtdPendenteEmb= ln2.CDU_QtdPendenteEmb - '" & listQtdPendente.Valor("Quantidade") & "' + replace('" & Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade & "',',','.') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha & "'")
                                    End If
                                    Exit For
                                End If

                            Next i
                        Next j
                    End If

                    listQtdPendente.Inicio()
                    'Primeira vez que se insere o BL num EMB já lançado. Necessário conferir quantidades.
                    If Not listQtdPendente.Vazia And Trim(Me.DocumentoVenda.CamposUtil("CDU_NBL").Valor & "") <> "0" Then
                        'MsgBox listQtdPendente("CDU_NBL")
                        If Trim(listQtdPendente.Valor("CDU_NBL") & "") = "0" Then
                            listQtdPendente.Inicio()

                            For j = 1 To listQtdPendente.NumLinhas

                                For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                                    If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And listQtdPendente.Valor("Id") = Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha Then
                                        'MsgBox listQtdPendente("Quantidade")
                                        If Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade <> listQtdPendente.Valor("Quantidade") Then
                                            BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_QtdPendenteEmb= ln2.CDU_QtdPendenteEmb - '" & listQtdPendente.Valor("Quantidade") & "' from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha & "'")
                                        Else
                                            BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_QtdPendenteEmb= ln2.CDU_QtdPendenteEmb - replace('" & Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade & "',',','.') from LinhasDoc ln inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha & "'")
                                        End If

                                        Exit For
                                    End If

                                Next i
                                listQtdPendente.Seguinte()
                            Next j
                        End If
                    End If
                End If


                '################################################################################################################################################################
                '############# JFC - 28/10/2019 - Preenchimento do campo CDU_QtdPendenteEmb. Tem como objetivo filtrar Embarques efetivos de Embarques previstos           ######
                '################################################################################################################################################################

            End If

        End Sub
    End Class
End Namespace