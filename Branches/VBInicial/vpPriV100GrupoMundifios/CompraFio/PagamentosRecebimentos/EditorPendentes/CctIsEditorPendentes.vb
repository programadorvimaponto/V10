Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.PayablesReceivables.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace CompraFio
    Public Class CctIsEditorPendentes
        Inherits EditorPendentes

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CompraFio") = 1 Then

                'Codigo alterado a 22/03/2019 a pedido de Elisabet. Alteração para listar todos os documentos onde o contentor é referido e não apenas um - JFC
                Dim j As Long
                Dim msg As String
                If Me.DocumentoPendente.Tipodoc = "FAF" Or Me.DocumentoPendente.Tipodoc = "FAI" Then

                    For i = 1 To Me.DocumentoPendente.Linhas.NumItens

                        If Me.DocumentoPendente.Linhas.GetEdita(i).CamposUtil("CDU_NumContentor").Valor & "" <> "" Then

                            Dim SqlStringNumContentor As String
                            SqlStringNumContentor = "SELECT dbo.Historico.Modulo, dbo.Historico.TipoEntidade, dbo.Historico.Entidade, dbo.Historico.TipoDoc, dbo.Historico.Serie, dbo.Historico.NumDoc, dbo.Historico.NumDocInt , dbo.Historico.DataDoc, dbo.LinhasPendentes.CDU_NumContentor " _
                                        & "FROM dbo.LinhasPendentes INNER JOIN dbo.Historico ON dbo.LinhasPendentes.IdHistorico = dbo.Historico.Id " _
                                        & "WHERE (dbo.Historico.Modulo = 'M') AND (dbo.Historico.TipoDoc in ('FAF','FAI','FAO','NCO','NCF', '" & Me.DocumentoPendente.Tipodoc & "')) AND (dbo.LinhasPendentes.CDU_NumContentor = '" & Me.DocumentoPendente.Linhas.GetEdita(i).CamposUtil("CDU_NumContentor").Valor & "') AND (dbo.Historico.Id <> '" & Me.DocumentoPendente.IDHistorico & "') " _
                                        & "ORDER BY dbo.Historico.DataDoc DESC"

                            Dim ListaNumContentor = BSO.Consulta(SqlStringNumContentor)

                            If ListaNumContentor.Vazia = False Then
                                msg = "Já existe um documento lançado com o mesmo Nº Contentor:" & Chr(13) & Chr(13)
                                ListaNumContentor.Inicio()

                                For j = 1 To ListaNumContentor.NumLinhas

                                    Dim NomeEntidade As String = ""

                                    If ListaNumContentor.Valor("TipoEntidade") = "F" Then
                                        NomeEntidade = BSO.Base.Fornecedores.Edita(ListaNumContentor.Valor("Entidade")).Nome
                                    ElseIf ListaNumContentor.Valor("TipoEntidade") = "R" Then
                                        NomeEntidade = BSO.Base.OutrosTerceiros.Edita(ListaNumContentor.Valor("Entidade")).Nome
                                    End If
                                    msg = "Documento:      " & ListaNumContentor.Valor("TipoDoc") & " Nº " & ListaNumContentor.Valor("NumDocInt") & "/" & ListaNumContentor.Valor("Serie") & " de " & ListaNumContentor.Valor("DataDoc") & ", Nº externo: " & ListaNumContentor.Valor("NumDoc") & Chr(13) & "Entidade:            " & ListaNumContentor.Valor("Entidade") & " - " & NomeEntidade & Chr(13) & "Nº Contentor:   " & ListaNumContentor.Valor("CDU_NumContentor") & Chr(13) & Chr(13)
                                    ListaNumContentor.Seguinte()
                                Next j
                                If MsgBox(msg & "Deseja mesmo assim gravar o documento?", vbQuestion + vbYesNo) = vbNo Then

                                    Cancel = True

                                End If

                            End If

                        End If

                    Next i

                End If
            End If

        End Sub


    End Class
End Namespace