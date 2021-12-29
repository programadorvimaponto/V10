Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace IntegracaoClientes
    Public Class BasIsFichaCliente
        Inherits FichaClientes

        Dim actualiza As Boolean
        Dim clienteCriadoAgora As Boolean
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("IntegracaoClientes") = 1 Then

                '########################################################################################################################
                '####### Verifica se existe alguma alteração no Anulado ou Tipo de Crédito e pergunta se é para atualizar nas empresas ##
                '#######    JFC 11/12/2020 - Pedido de Sofia Mendes                                                                    ##
                '########################################################################################################################

                Dim listCriterios As StdBELista
                Dim strCredito As String
                actualiza = False
                clienteCriadoAgora = False
                strCredito = ""
                listCriterios = BSO.Consulta("select top 1 ClienteAnulado, TipoCred from Clientes where Cliente='" & Me.Cliente.Cliente & "'")
                listCriterios.Inicio()

                If listCriterios.Vazia = False Then
                    If Me.Cliente.Inactivo <> listCriterios.Valor("ClienteAnulado") Or Me.Cliente.TipoCredito <> listCriterios.Valor("TipoCred") Then
                        Select Case Me.Cliente.TipoCredito
                            Case 1
                                strCredito = "Por Limite"
                            Case 2
                                strCredito = "Suspenso"
                        End Select
                        If MsgBox("Deseja atualizar os seguintes parâmetros em todas as empresas?, " & Chr(13) & Chr(13) & "Anulado: " & Me.Cliente.Inactivo & Chr(13) & "Crédito: " & strCredito & Chr(13) & "", vbYesNo) = vbYes Then
                            actualiza = True
                        End If
                    End If
                Else
                    clienteCriadoAgora = True
                End If
                '########################################################################################################################
                '####### Verifica se existe alguma alteração no Anulado ou Tipo de Crédito e pergunta se é para atualizar nas empresas ##
                '#######    JFC 11/12/2020 - Pedido de Sofia Mendes                                                                    ##
                '########################################################################################################################

            End If

        End Sub

        Dim listEmpresas As StdBELista

        Public Overrides Sub DepoisDeGravar(Cliente As String, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Cliente, e)

            If Module1.VerificaToken("IntegracaoClientes") = 1 Then

                If actualiza Then
                    'JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                    listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" & Aplicacao.Empresa.CodEmp & "' and PRI_FichaClientes='1'")
                    listEmpresas.Inicio()

                    For i = 1 To listEmpresas.NumLinhas
                        BSO.DSO.ExecuteSQL("update PRI" & listEmpresas.Valor("Empresa") & ".DBO.Clientes set ClienteAnulado='" & Me.Cliente.Inactivo & "', TipoCred='" & Me.Cliente.TipoCredito & "' where CDU_EntidadeInterna='" & Me.Cliente.CamposUtil("CDU_EntidadeInterna").Valor & "'")
                        listEmpresas.Seguinte()
                    Next i
                End If

                'JFC 04/03/2021 Valida Cliente criado, e atualiza CDU_PrintLab
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" & Aplicacao.Empresa.CodEmp & "' and PRI_FichaClientes='1'")
                listEmpresas.Inicio()

                If clienteCriadoAgora Then
                    For i = 1 To listEmpresas.NumLinhas
                        BSO.DSO.ExecuteSQL("update c set c.CDU_PrintLab=c2.CDU_PrintLab from dbo.Clientes c inner join PRI" & listEmpresas.Valor("Empresa") & ".dbo.Clientes c2  on c2.CDU_EntidadeInterna=c.CDU_EntidadeInterna where c.CDU_EntidadeInterna='" & Me.Cliente.CamposUtil("CDU_EntidadeInterna").Valor & "'")
                    Next i

                Else

                    For i = 1 To listEmpresas.NumLinhas
                        BSO.DSO.ExecuteSQL("update PRI" & listEmpresas.Valor("Empresa") & ".DBO.Clientes set CDU_PrintLab='" & Me.Cliente.CamposUtil("CDU_PrintLab").Valor & "' where CDU_EntidadeInterna='" & Me.Cliente.CamposUtil("CDU_EntidadeInterna").Valor & "'")
                        listEmpresas.Seguinte()
                    Next i


                End If


            End If

        End Sub

    End Class
End Namespace