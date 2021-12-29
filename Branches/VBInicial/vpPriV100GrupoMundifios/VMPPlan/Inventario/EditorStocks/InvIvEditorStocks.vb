Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Production.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100

Namespace VMPPlan

    Public Class InvIvEditorStocks
        Inherits EditorStocksProducao

        Dim ListaDocPlan As StdBELista
        Dim SqlStringDocPlan As String
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("VMPPlan") = 1 Then

                'Código para verificar se o documento foi gerado no VMP Plan
                '******************************************************************************************************************************************************************
                SqlStringDocPlan = "SELECT * FROM TDU_SecRastreabilidadeDocs WHERE CDU_DocPRI = '" & Me.DocumentoStock.ID & "'"

                ListaDocPlan = BSO.Consulta(SqlStringDocPlan)

                If ListaDocPlan.Vazia = False And UCase(BSO.Contexto.UtilizadorActual) <> "FINANCE" Then

                    MsgBox("O Documento seleccionado foi gerado a partir do VMP Plan, logo não permite alterações a partir do ERP Primavera.", vbInformation + vbOKOnly)
                    Cancel = True
                    Exit Sub

                End If
                '******************************************************************************************************************************************************************
            End If
        End Sub

        Public Overrides Sub DepoisDeEditar(e As ExtensibilityEventArgs)
            MyBase.DepoisDeEditar(e)

            If Module1.VerificaToken("VMPPlan") = 1 Then
                'Código para verificar se o documento foi gerado no VMP Plan
                '******************************************************************************************************************************************************************
                SqlStringDocPlan = "SELECT * FROM TDU_SecRastreabilidadeDocs WHERE CDU_DocPRI = '" & Me.DocumentoStock.ID & "'"

                ListaDocPlan = BSO.Consulta(SqlStringDocPlan)

                If ListaDocPlan.Vazia = False Then

                    MsgBox("O Documento seleccionado foi gerado a partir do VMP Plan, logo não permite alterações a partir do ERP Primavera.", vbInformation + vbOKOnly)
                    Exit Sub

                End If
                '******************************************************************************************************************************************************************
            End If
        End Sub

    End Class
End Namespace
