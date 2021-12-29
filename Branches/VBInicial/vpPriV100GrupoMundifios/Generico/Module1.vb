Imports StdBE100
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports Primavera.Extensibility.Integration

Namespace Generico
    Public Class Module1


        'Utilizado no frmFornecedoresCerts JFC 04/11/2019
        Public Shared certEntidade As String

        'Utilizado no frmInditex Bruno Peixoto 02/09/2020
        Public Shared certFiacoes As String

        'Compras
        Public Shared i As Long
        Public Shared ListaIdCopia As StdBELista
        Public Shared SqlStringIdCopia As String
        Public Shared ListaQtdIdCopia As StdBELista
        Public Shared SqlStringQtdIdCopia As String

        '###############################################################
        '# Declaração Variaveis para Formulário FrmAlteraGuiaAEP (JFC) #
        '###############################################################
        Public Shared aepArtigo As String
        Public Shared aepDocumento As String
        Public Shared aepLote As String
        Public Shared aepArmazem As String
        Public Shared aepDespDAU As String
        Public Shared aepRegime As String
        Public Shared aepIDlinha
        '###############################################################
        '# Declaração Variaveis para Formulário FrmAlteraGuiaAEP (JFC) #
        '###############################################################

        '*******************************************************************************************************************************************
        '#### ARMAZEM ENTREPOSTO ####
        '*******************************************************************************************************************************************
        Public Shared ArmEntreposto As String
        '*******************************************************************************************************************************************
        '#### ARMAZEM ENTREPOSTO ####
        '*******************************************************************************************************************************************


        '*******************************************************************************************************************************************
        '#### TARAS A DEVOLVER ####
        '*******************************************************************************************************************************************
        Public Shared ConesCartao As Integer
        Public Shared ConesPlastico As Integer
        Public Shared TubosCartao As Integer
        Public Shared TubosPlastico As Integer
        Public Shared PaletesMadeira As Integer
        Public Shared PaletesPlastico As Integer
        Public Shared SeparadoresCartao As Integer
        Public Shared TotalTaras As Integer


        Public Shared Devolver_ConesCartao As Boolean
        Public Shared Devolver_ConesPlastico As Boolean
        Public Shared Devolver_TubosCartao As Boolean
        Public Shared Devolver_TubosPlastico As Boolean
        Public Shared Devolver_PaletesMadeira As Boolean
        Public Shared Devolver_PaletesPlastico As Boolean
        Public Shared Devolver_SeparadoresCartao As Boolean
        Public Shared TotalTaras_a_Devolver As Integer

        '*******************************************************************************************************************************************
        '#### TARAS A DEVOLVER ####
        '*******************************************************************************************************************************************




        Public Shared EntidadeVerifica As String
        Public Shared ArtigoVerifica As String
        Public Shared LoteVerifica As String
        Public Shared ArmazemVerifica As String
        Public Shared PrecoUnitVerifica As Double


        '############################################################################
        '# Declaração Variaveis para Formulário FrmAlteraCertificadoTransacao (JFC) #
        '############################################################################
        Public Shared certArtigo As String
        Public Shared certDocumento As String
        Public Shared certLote As String
        Public Shared certArmazem As String
        Public Shared certCertificadoTransacao As String
        Public Shared certCertificadoTransacao2 As String
        Public Shared certCertificadoTransacao3 As String
        Public Shared certQtdTransacao As Double
        Public Shared certQtdTransacao2 As Double
        Public Shared certQtdTransacao3 As Double
        Public Shared certDataCertificado As Date
        Public Shared certIDlinha
        Public Shared certProgramLabel As Integer
        Public Shared certEmitido As Boolean
        Public Shared certEmitir As Boolean
        Public Shared certDescricao As String
        Public Shared certObs As String
        Public Shared certBCI As Boolean
        Public Shared certBCIEmitido As Boolean
        '############################################################################
        '# Declaração Variaveis para Formulário FrmAlteraCertificadoTransacao (JFC) #
        '############################################################################

        'Acrescentado dia 27/01/2021 - Bruno
        Public Shared certCancelado As String


        '###############################################################
        '# Declaração Variaveis para Formulário FrmDisputa(JFC) #
        '###############################################################
        Public Shared dspModulo As String
        Public Shared dsptipoDoc As String
        Public Shared dspNumDoc As String
        Public Shared dspSerie As String
        Public Shared dspDisputa As Boolean
        '###############################################################
        '# Declaração Variaveis para Formulário FrmDisputa (JFC) #
        '###############################################################

        '        Public Shared Function AbreObjEmpresa(ByVal Empresa As String) As Boolean


        '            On Error GoTo TrataErro

        '            PriV100Api.PSO.AbreEmpresaTrabalho(TipoEmpresa, Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual)
        '            AbreObjEmpresa = True
        '            Exit Function

        'TrataErro:
        '            AbreObjEmpresa = False
        '        End Function

        '        Public Shared Sub FechaObjEmpresa()
        '            PriV100Api.FechaEmpresaTrabalho
        '        End Sub



        Shared Function NovaDataVencimento(ByVal vDataDoc As Date, ByVal vCondPag As String, ByVal vTipoEntidade As String, ByVal vEntidade As String) As Date

            Dim vDataVenc As Date
            Dim DataDocRQRM As Date
            If PriV100Api.BSO.Base.CondsPagamento.Edita(vCondPag).CamposUtil("CDU_RM").Valor = True Then
                vDataVenc = PriV100Api.BSO.Vendas.Documentos.CalculaDataVencimento(vDataDoc, vCondPag, PriV100Api.BSO.Base.CondsPagamento.Edita(vCondPag).DiasVencimento, vTipoEntidade, vEntidade)
                DataDocRQRM = Func_Ultimo_Dia_Mes(vDataVenc)

            ElseIf PriV100Api.BSO.Base.CondsPagamento.Edita(vCondPag).CamposUtil("CDU_RQ").Valor = True Then

                vDataVenc = PriV100Api.BSO.Vendas.Documentos.CalculaDataVencimento(vDataDoc, vCondPag, PriV100Api.BSO.Base.CondsPagamento.Edita(vCondPag).DiasVencimento, vTipoEntidade, vEntidade)

                If Day(vDataVenc) <= 15 Then
                    DataDocRQRM = "15/" & Month(vDataVenc) & "/" & Year(vDataVenc)
                Else
                    DataDocRQRM = Func_Ultimo_Dia_Mes(vDataVenc)
                End If

            End If

            NovaDataVencimento = DataDocRQRM

        End Function

        Public Shared Function Func_Ultimo_Dia_Mes(paramDataX As Date) As Date

            Func_Ultimo_Dia_Mes = DateAdd("m", 1, DateSerial(Year(paramDataX), Month(paramDataX), 1))
            Func_Ultimo_Dia_Mes = DateAdd("d", -1, Func_Ultimo_Dia_Mes)

        End Function

        ''' <summary>
        ''' Verifica o token da funcionalidade na base de dados. 1 se estiver ativo, 0 se não estiver.
        ''' </summary>
        Public Shared Function VerificaToken(token As String) As Integer

            If PriV100Api.BSO.DSO.DaValorUnico("select CDU_AplicaFuncionalidade from TDU_FuncionalidadesExt where CDU_TokenFuncionalidade = '" + token + "'") Then
                Return 1

            Else
                Return 0
            End If

        End Function




    End Class
End Namespace