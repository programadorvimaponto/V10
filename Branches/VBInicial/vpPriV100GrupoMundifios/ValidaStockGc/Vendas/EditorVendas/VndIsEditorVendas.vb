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

Namespace ValidaStockGc
    Public Class VndIsEditorVendas
        Inherits EditorVendas
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("ValidaStockGc") = 1 Then

                'JFC 31/01/2020 - Validação de Stock disponivel em Armazém. Reportado por Pedro Carteado, situação recorrente no mercado externo onde o armazém indicado na GC não está correcto.
                If Me.DocumentoVenda.Tipodoc = "GC" Then
                    Dim listStk As StdBELista
                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(i).IdLinhaOrigemCopia & "" <> "" Then
                            Dim listEstadoECL As StdBELista
                            listEstadoECL = BSO.Consulta("select * from linhasdoc ln inner join cabecdoc cd on cd.id=ln.idcabecdoc inner join cabecdocstatus cds on cds.Idcabecdoc=cd.id inner join linhasdocstatus lns on lns.IdLinhasDoc=ln.id where lns.EstadoTrans='P' and lns.Fechado='0' and cds.Fechado='0' and cds.Anulado='0' and cds.Estado='P' and ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinhaOrigemCopia & "'")
                            If listEstadoECL.Vazia = False Then
                                If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" Then
                                    listStk = BSO.Consulta("select aa.StkActual from ArtigoArmazem aa where aa.StkActual>0 and aa.Artigo='" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' and aa.Lote='" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' and aa.Armazem='" & Me.DocumentoVenda.Linhas.GetEdita(i).Armazem & "'")
                                    If listStk.Vazia Then
                                        listStk = BSO.Consulta("select top 1 aa.Armazem, aa.StkActual from ArtigoArmazem aa where aa.StkActual>0 and aa.Artigo='" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' and aa.Lote='" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' order by aa.StkActual desc")
                                        If listStk.Vazia Then
                                            MsgBox("Atenção Artigo/Lote sem stock: " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & " - " & Me.DocumentoVenda.Linhas.GetEdita(i).Lote, vbInformation)
                                        Else
                                            listStk.Inicio()

                                            If MsgBox("Atenção " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & " - " & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & " sem stock no armazem " & Me.DocumentoVenda.Linhas.GetEdita(i).Armazem & Chr(13) & "Artigo/Lote com " & listStk.Valor("StkActual") & "Kg no armazem " & listStk.Valor("Armazem") & Chr(13) & "Deseja atualizar a ECL e GC para o armazem " & listStk.Valor("Armazem") & "?", vbYesNo) = vbYes Then
                                                Me.DocumentoVenda.Linhas.GetEdita(i).Armazem = listStk.Valor("Armazem")
                                                Me.DocumentoVenda.Linhas.GetEdita(i).Localizacao = listStk.Valor("Armazem")
                                                BSO.DSO.ExecuteSQL("update ln set ln.Armazem='" & listStk.Valor("Armazem") & "', ln.Localizacao='" & listStk.Valor("Armazem") & "' from LinhasDoc ln where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinhaOrigemCopia & "'")
                                            End If

                                        End If

                                    End If
                                End If
                            End If
                        End If
                    Next i

                End If
            End If

        End Sub

    End Class
End Namespace
