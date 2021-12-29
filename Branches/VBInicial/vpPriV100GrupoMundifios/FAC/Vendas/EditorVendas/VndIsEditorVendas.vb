'Imports System
'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Text
'Imports System.Threading.Tasks
'Imports Primavera.Extensibility.Sales.Editors
'Imports Primavera.Extensibility.BusinessEntities
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
'Imports StdBE100
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
'Imports InvBE100

'Namespace FAC
'    Public Class VndIsEditorVendas
'        Inherits EditorVendas

'        Dim FACAcabadaDeCriar As Boolean
'        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
'            MyBase.AntesDeGravar(Cancel, e)

'            If Module1.VerificaToken("FAC") = 1 Then

'                'JFC Valida de o documento FAC foi criado pela primeira vez.
'                FACAcabadaDeCriar = False
'                If Me.DocumentoVenda.Tipodoc = "FAC" Then
'                    Dim ListFAC As StdBELista

'                    ListFAC = BSO.Consulta("SELECT distinct ln.Artigo FROM CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id Where cd.Id=" & "'" & Me.DocumentoVenda.ID & "'")

'                    If ListFAC.Vazia Then
'                        FACAcabadaDeCriar = True
'                    Else
'                        FACAcabadaDeCriar = False
'                    End If
'                End If

'            End If


'        End Sub

'        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
'            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

'            If Module1.VerificaToken("FAC") = 1 Then

'                '*******************************************************************************************************************************************
'                '#### Criar Transferencia de Armazém Cliente (FAC) - JFC 08/07/2019 ####
'                '*******************************************************************************************************************************************
'                If Me.DocumentoVenda.Tipodoc = "FAC" Then
'                    If FACAcabadaDeCriar Then
'                        Dim ListArm As StdBELista
'                        Dim ListLinhas As StdBELista
'                        ListArm = BSO.Consulta("SELECT distinct ln.Armazem FROM CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id Where ln.Armazem is not null and cd.Id=" & "'" & Me.DocumentoVenda.ID & "'")
'                        ListArm.Inicio()

'                        For j = 1 To ListArm.NumLinhas

'                            ListLinhas = BSO.Consulta("SELECT ln.Artigo, ln.Quantidade, ln.Armazem, ln.PrecUnit, ln.Lote, ln.Localizacao  FROM CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id Where cd.Id=" & "'" & Me.DocumentoVenda.ID & "' and ln.Armazem='" & ListArm.Valor(0) & "'")
'                            CriaTransArmCliente(ListArm.Valor(0), Me.DocumentoVenda.Serie, ListLinhas, Me.DocumentoVenda.DataDoc)

'                            ListArm.Seguinte()

'                        Next j
'                    End If
'                End If
'                '*******************************************************************************************************************************************
'                '#### Criar Transferencia de Armazém Cliente (FAC) - JFC 08/07/2019 ####
'                '*******************************************************************************************************************************************

'            End If

'        End Sub


'        Private Sub CriaTransArmCliente(ByVal TRA_Arm As String, ByVal TRA_Serie As String, TRA_Linhas As StdBELista, TRA_Data As Date)

'            Dim DocStocks As InvBEDocumentoTransf

'            Dim strDetalhe As String

'            On Error GoTo Erro

'            'Inicia uma transação
'            BSO.IniciaTransaccao()

'            DocStocks = New InvBEDocumentoTransf

'            With DocStocks

'                'Muito importante que o identificador do documento esteja já preenchido
'                .ID = PSO.FuncoesGlobais.CriaGuid(True)

'                .Tipodoc = "TRA"
'                .Serie = TRA_Serie
'                .ArmazemOrigem = TRA_Arm



'            End With

'            'Preenche a restante informação no documento
'            BSO.Comercial.Stocks.PreencheDadosRelacionados(DocStocks)
'            DocStocks.Data = TRA_Data
'            'Adiciona um artigo

'            TRA_Linhas.Inicio()
'            Dim j As Long
'            For j = 1 To TRA_Linhas.NumLinhas
'                'ln.Artigo, ln.Quantidade, ln.Armazem, ln.PrecUni, ln.Lote, ln.Localizacao
'                BSO.Comercial.Stocks.AdicionaLinha(DocStocks, TRA_Linhas.Valor(0), , TRA_Linhas.Valor(1), "FC", TRA_Linhas.Valor(3), , TRA_Linhas.Valor(4), "FC")
'                'BSO.Comercial.Stocks.AdicionaLinha DocStocks, Artigo, EntradaSaida, Quantidade, Armazem, PrecUnit, Desconto, Lote, Localizacao, QntVA, QntdVb, QntVc
'                TRA_Linhas.Seguinte()
'            Next j



'            '----------------------------------
'            '   GRAVAÇÃO DO DOCUMENTO
'            BSO.Comercial.Stocks.actualiza DocStocks
'            '   GRAVAÇÃO DO DOCUMENTO
'            '----------------------------------

'            'Termina a transação
'            BSO.TerminaTransaccao()
'            '----------------------------------
'            '   MENSAGEM FINAL


'            strDetalhe = vbNullString

'            strDetalhe = strDetalhe & "Documento de Stock: " & DocStocks.Tipodoc & " Nº " & CStr(DocStocks.NumDoc) & "/" & DocStocks.Serie & vbCrLf

'            MsgBox("Documento gerado com sucesso." & vbCrLf & strDetalhe, vbInformation, "Documento de Stock")

'            '   MENSAGEM FINAL
'            '----------------------------------

'            DocStocks = Nothing


'            Exit Sub

'Erro:
'            'Desfaz a transação
'            BSO.DesfazTransaccao()

'            DocStocks = Nothing


'            MsgBox("Erro ao gerar o documento." & vbCrLf & Err.Description, vbCritical, "Erro")


'        End Sub

'    End Class
'End Namespace
