Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace HistoricoPlafond

    Public Class BasIsFichaCliente
        Inherits FichaClientes

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)


            If Module1.VerificaToken("HistoricoPlafond") = 1 Then

                '#################################################################################################
                '####### Atualiza a tabela TDU_HistoricoPlafond - JFC 12-02-2019                        ##########
                '#################################################################################################



                Dim HistoricoPlafond As StdBELista
                Dim PlafondSeguradora As Long
                Dim PlafondSolicitado As Long
                Dim PlafondAdicional As Long


                PlafondSeguradora = 0
                PlafondSolicitado = 0
                PlafondAdicional = 0


                HistoricoPlafond = BSO.Consulta("select top 1 Data, PlafondSeguradora, PlafondSolicitado, PlafondAdicional from TDU_HistoricoPlafond where Entidade='" & Me.Cliente.Cliente & "' and Empresa='Mundifios' order by Data desc")

                If HistoricoPlafond.Vazia Then


                    BSO.DSO.ExecuteSQL("INSERT INTO [PRIMUNDIFIOS].[DBO].[TDU_HistoricoPlafond] values ('Mundifios', getdate(),'" & Me.Cliente.Cliente & "','" & Me.Cliente.Nome & "', '" & Me.Cliente.CamposUtil("CDU_PlafondSeguradora").Valor & "','" & Me.Cliente.CamposUtil("CDU_PlafondExtra") & "','" & Me.Cliente.CamposUtil("CDU_PlafondAdicional") & "')")

                Else

                    HistoricoPlafond.Inicio()

                    PlafondSeguradora = HistoricoPlafond.Valor("PlafondSeguradora")
                    PlafondSolicitado = HistoricoPlafond.Valor("PlafondSolicitado")
                    PlafondAdicional = HistoricoPlafond.Valor("PlafondAdicional")


                    If Me.Cliente.CamposUtil("CDU_PlafondSeguradora").Valor <> PlafondSeguradora Or Me.Cliente.CamposUtil("CDU_PlafondExtra").Valor <> PlafondSolicitado Or Me.Cliente.CamposUtil("CDU_PlafondAdicional").Valor <> PlafondAdicional Then


                        BSO.DSO.ExecuteSQL("INSERT INTO [PRIMUNDIFIOS].[DBO].[TDU_HistoricoPlafond] values ('Mundifios', getdate(),'" & Me.Cliente.Cliente & "','" & Me.Cliente.Nome & "', '" & Me.Cliente.CamposUtil("CDU_PlafondSeguradora").Valor & "','" & Me.Cliente.CamposUtil("CDU_PlafondExtra") & "','" & Me.Cliente.CamposUtil("CDU_PlafondAdicional") & "')")


                    End If
                End If




                '###################################################################################
                '####### Atualiza a tabela TDU_HistoricoPlafond - JFC 12-02-2019                  ##
                '###################################################################################

            End If

        End Sub

    End Class
End Namespace
