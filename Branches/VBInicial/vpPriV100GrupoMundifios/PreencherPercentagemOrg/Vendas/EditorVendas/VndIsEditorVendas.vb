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

Namespace PreencherPercentagemOrg
    Public Class VndIsEditorVendas
        Inherits EditorVendas


        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("PreencherPercentagemOrg") = 1 Then

                '#################################################################################################
                '####### Percentagens de Gots, OCS e GRS. Pedido de Dr. Rita - JFC 20/03/2019         ############
                '#################################################################################################

                If Me.DocumentoVenda.Tipodoc = "ECL" Then
                    Dim LinhaNaoExiste As StdBELista
                    Dim ReplaceGots As Boolean
                    Dim ReplaceOCS As Boolean
                    Dim ReplaceGRS As Boolean

                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens
                        ReplaceGots = False
                        ReplaceOCS = False
                        ReplaceGRS = False

                        If Me.DocumentoVenda.Linhas.GetEdita(j).Armazem <> "PLA" Then

                            Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "GOTS", "Gots")
                            LinhaNaoExiste = BSO.Consulta("select top 1 ln.CDU_Gots, ln.CDU_OCS, ln.CDU_GRS, ln.Artigo from LinhasDoc ln where ln.Artigo='" & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "' and ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(j).IdLinha & "'")
                            If LinhaNaoExiste.Vazia = True Then

                                If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_ReplacePercCert").Valor = False Then
                                    If Me.DocumentoVenda.Linhas.GetEdita(j).Descricao Like "*Gots*" Then
                                        If Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor = "0" Or IsNothing(Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots")) Then
                                            MsgBox("Atenção preencher o campo de utilizador Perc. Gots antes de gravar!", vbCritical + vbOKOnly)
                                            Cancel = True
                                            Exit Sub
                                        Else
                                            ReplaceGots = True
                                        End If

                                    End If

                                    If Me.DocumentoVenda.Linhas.GetEdita(j).Descricao Like "*OCS*" Then
                                        If Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS").Valor = "0" Or IsNothing(Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS")) Then
                                            MsgBox("Atenção preencher o campo de utilizador Perc. OCS antes de gravar!", vbCritical + vbOKOnly)
                                            Cancel = True
                                            Exit Sub
                                        Else
                                            ReplaceOCS = True
                                        End If

                                    End If

                                    If Me.DocumentoVenda.Linhas.GetEdita(j).Descricao Like "*GRS*" Then
                                        If Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_GRS").Valor = "0" Or IsNothing(Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_GRS")) Then
                                            MsgBox("Atenção preencher o campo de utilizador Perc. GRS antes de gravar!", vbCritical + vbOKOnly)
                                            Cancel = True
                                            Exit Sub
                                        Else
                                            ReplaceGRS = True
                                        End If

                                    End If

                                    If ReplaceGots = True And Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor <> 100 Then
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Org. Gots", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor & "% Org. Gots")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Organico Gots", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor & "% Org. Gots")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Org.  Gots", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor & "% Org. Gots")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Org.Gots", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor & "% Org. Gots")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Org Gots", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor & "% Org. Gots")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_ReplacePercCert").Valor = True
                                    End If
                                    If ReplaceOCS = True And Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS").Valor <> 100 Then
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Org. OCS", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS").Valor & "% Org. OCS")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Org OCS", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS").Valor & "% Org. OCS")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Organico OCS", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS").Valor & "% Org. OCS")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_ReplacePercCert").Valor = True
                                    End If
                                    If ReplaceGRS = True And Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_GRS").Valor <> 100 Then
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "GRS", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_GRS").Valor & "% GRS")
                                        Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_ReplacePercCert").Valor = True
                                    End If

                                End If


                            Else
                                LinhaNaoExiste.Inicio()
                                'If Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_ReplacePercCert") = True Then
                                If LinhaNaoExiste.Valor("CDU_Gots") <> Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots") Then
                                    If LinhaNaoExiste.Valor("CDU_Gots") = 0 Or LinhaNaoExiste.Valor("CDU_Gots") = 100 Then
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Org. Gots", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor & "% Org. Gots")
                                    Else
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, LinhaNaoExiste.Valor("CDU_Gots") & "% Org. Gots", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_Gots").Valor & "% Org. Gots")
                                    End If
                                End If

                                If LinhaNaoExiste.Valor("CDU_OCS") <> Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS") Then
                                    If LinhaNaoExiste.Valor("CDU_OCS") = 0 Or LinhaNaoExiste.Valor("CDU_OCS") = 100 Then
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "Org. OCS", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS").Valor & "% Org. OCS")
                                    Else
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, LinhaNaoExiste.Valor("CDU_OCS") & "% Org. OCS", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_OCS").Valor & "% Org. OCS")
                                    End If
                                End If

                                If LinhaNaoExiste.Valor("CDU_GRS") <> Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_GRS") Then
                                    If LinhaNaoExiste.Valor("CDU_GRS") = 0 Or LinhaNaoExiste.Valor("CDU_GRS") = 100 Then
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, "GRS", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_GRS").Valor & "% GRS")
                                    Else
                                        Me.DocumentoVenda.Linhas.GetEdita(j).Descricao = Replace(Me.DocumentoVenda.Linhas.GetEdita(j).Descricao, LinhaNaoExiste.Valor("CDU_GRS") & "% GRS", Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_GRS").Valor & "% GRS")
                                    End If
                                End If
                                'End If

                            End If


                        End If


                    Next j
                End If

                '#################################################################################################
                '####### Percentagens de Gots, OCS e GRS. Pedido de Dr. Rita - JFC 20/03/2019         ############
                '#################################################################################################

            End If

        End Sub

    End Class
End Namespace
