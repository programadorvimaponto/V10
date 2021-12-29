Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.CustomForm
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Public Class FrmCustoTransportesView
    Inherits CustomForm

    Dim c As Double
    Dim c2 As Double
    '    Private Sub BarButtonItemRemover_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemRemover.ItemClick

    '        Me.TextEditCodigoFornecedor.EditValue = String.Empty
    '        Me.TextEditIdFatura.EditValue = String.Empty
    '        Me.TextEditFatura.EditValue = String.Empty
    '        Me.TextEditCusto.EditValue = "0"

    '        Me.TextEditFatura2.EditValue = String.Empty
    '        Me.TextEditCusto2.EditValue = "0"
    '        Me.TextEditTotal.EditValue = "0"
    '        Me.TextEditIdFatura2.EditValue = String.Empty
    '        Me.MemoEditObsTransporte.EditValue = String.Empty



    '        BSO.DSO.ExecuteSQL("update CabecDoc set CDU_EntidadeTransporte='" & Me.TextEditCodigoFornecedor.EditValue & "', CDU_IdFaturaTransporte='" & Me.TextEditIdFatura.EditValue & "', CDU_IdFaturaTransporte2='" & Me.TextEditIdFatura2.EditValue & "', CDU_FaturaTransporte='" & Me.TextEditFatura.EditValue & "', CDU_FaturaTransporte2='" & Me.TextEditFatura2.EditValue & "', CDU_CustoTransporte='" & Me.TextEditCusto.EditValue & "', CDU_CustoTransporte2='" & Me.TextEditCusto2.EditValue & "', CDU_CustoTransporteTotal='" & Me.TextEditTotal.EditValue & "', CDU_Obstransporte='" & Me.MemoEditObsTransporte.EditValue & "' where Id='" & EditorVendas.DocumentoVenda.ID & "'")
    '        BSO.DSO.ExecuteSQL("update ln set ln.CDU_CustoTransporte=ln.Quantidade/(select sum(ln2.Quantidade) from LinhasDoc ln2 inner join Artigo a2 on a2.Artigo=ln2.Artigo where ln2.IdCabecDoc=cd.Id)*cd.CDU_CustoTransporte  from CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id inner join Artigo a on a.Artigo=ln.Artigo where cd.Id='" & EditorVendas.DocumentoVenda.ID & "'")


    '        Me.Hide()
    '        Unload Me

    '                End Sub

    '    Private Sub BarButtonItemGravar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemGravar.ItemClick

    '        If Me.TextEditCodigoFornecedor.EditValue <> "" Then
    '            Gravar()

    '        Else

    '            MsgBox("O Fornecedor não está identificado.", vbInformation + vbOKOnly)
    '        End If


    '    End Sub

    '    Private Sub CarregaDados()

    '        If Me.TextEditFornecedor.EditValue = String.Empty Then
    '            Me.TextEditCodigoFornecedor.EditValue = String.Empty
    '            Exit Sub
    '        End If



    '    End Sub

    '    Function Gravar()


    '        On Error GoTo Erro

    '        BSO.DSO.ExecuteSQL("update CabecDoc set CDU_EntidadeTransporte='" & Me.TextEditCodigoFornecedor.EditValue & "', CDU_IdFaturaTransporte='" & Me.TextEditIdFatura.EditValue & "', CDU_FaturaTransporte='" & Me.TextEditFatura.EditValue & "', CDU_FaturaTransporte2='" & Me.TextEditFatura2.EditValue & "', CDU_CustoTransporteTotal=replace('" & Me.TextEditTotal.EditValue & "',',','.'), CDU_CustoTransporte=replace('" & Me.TextEditCusto.EditValue & "',',','.'), CDU_CustoTransporte2=replace('" & Me.TextEditCusto2.EditValue & "',',','.'), CDU_Obstransporte='" & Me.MemoEditObsTransporte.EditValue & "' where Id='" & EditorVendas.DocumentoVenda.ID & "'")

    '        BSO.DSO.ExecuteSQL("update ln set ln.CDU_CustoTransporte=ln.Quantidade/(select sum(ln2.Quantidade) from LinhasDoc ln2 inner join Artigo a2 on a2.Artigo=ln2.Artigo where ln2.IdCabecDoc=cd.Id)*cd.CDU_CustoTransporteTotal/ln.Quantidade  from CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id inner join Artigo a on a.Artigo=ln.Artigo where cd.Id='" & EditorVendas.DocumentoVenda.ID & "'")




    '        Me.Hide()
    '        Unload Me

    '                  Exit Function

    'Erro:
    '        MsgBox("Erro ao gravar: " & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)




    '    End Function

    '    Private Sub TextEditCusto_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditCusto.EditValueChanged

    '        If Not IsNumeric(Me.TextEditCusto.EditValue) Then
    '            MsgBox("Só são permitidos numeros!")
    '            Me.TextEditCusto.EditValue = "0"

    '        End If
    '        If Me.TextEditTotal.EditValue <> "" Then
    '            c = Replace(Me.TextEditCusto.EditValue, ".", ",")
    '            c2 = Replace(Me.TextEditCusto.EditValue, ".", ",")
    '            Me.TextEditTotal.EditValue = c + c2
    '        End If


    '    End Sub

    '    Private Sub TextEditCusto2_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditCusto2.EditValueChanged

    '        If Not IsNumeric(Me.TextEditCusto2.EditValue) Then
    '            MsgBox("Só são permitidos numeros!")
    '            Me.TextEditCusto2.EditValue = "0"

    '        End If
    '        If Me.TextEditTotal.EditValue <> "" Then
    '            c = Replace(Me.TextEditCusto.EditValue, ".", ",")
    '            c2 = Replace(Me.TextEditCusto2.EditValue, ".", ",")
    '            Me.TextEditTotal.EditValue = c + c2
    '        End If

    '    End Sub

    '    Private Sub TextEditFornecedor_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditFornecedor.EditValueChanged

    '        CarregaDados()

    '    End Sub

    '    Private Sub TextEditCodigoFornecedor_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditCodigoFornecedor.EditValueChanged

    '        Me.TextEditFornecedor.EditValue = BSO.Base.Fornecedores.DaValorAtributo(Me.TextEditCodigoFornecedor.EditValue, "Nome")

    '    End Sub

    '    Private Sub TextEditCodigoFornecedor_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles TextEditCodigoFornecedor.KeyDown

    '        If e.KeyCode = Windows.Forms.Keys.F4 Then

    '            Me.TextEditCodigoFornecedor.EditValue = PSO.Listas.GetF4SQL("Fornecedores", "select fornecedor, nome as 'Nome Fornecedor' from fornecedores where CDU_TipoFornecedor='001'", "Fornecedor")

    '        End If

    '    End Sub

    '    Private Sub TextEditFatura_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles TextEditFatura.KeyDown

    '        If e.KeyCode = Windows.Forms.Keys.E.F4 Then

    '            If Me.TextEditCodigoFornecedor.EditValue = String.Empty Then
    '                MsgBox("O Fornecedor não está identificado.", vbInformation + vbOKOnly)
    '            Else
    '                Me.TextEditIdFatura.EditValue = PSO.Listas.GetF4SQL("Lista Faturas Pendentes", "select h.DataDoc as 'Data', concat(h.TipoDoc,' ', h.NumDocint,'/', h.Serie) as 'N/Doc', h.NumDoc as 'V/Doc',abs(h.ValorTotal)-abs(h.TotalIva) as 'SemIVA', h.ValorTotal*-1 as 'ValorTotal', h.Id from Historico h inner join DocumentosCCT ct on ct.Documento=h.TipoDoc where ct.Natureza='D' and h.Entidade='" & Me.TextEditCodigoFornecedor.EditValue & "' and h.DataLiq is null and h.TipoEntidade='F'", "Id")
    '                If Me.TextEditIdFatura.EditValue <> String.Empty Then
    '                    Dim listFaf As StdBELista
    '                    listFaf = BSO.Consulta("select h.NumDoc as 'Doc',abs(h.ValorTotal)-abs(h.TotalIva) as 'SemIVA', h.ValorTotal*-1 as 'ValorTotal'  from Historico h where h.Id='" & Me.TextEditFatura.EditValue & "'")
    '                    listFaf.Inicio()
    '                    Me.TextEditFatura.EditValue = listFaf.Valor("Doc")
    '                    Me.TextEditCusto.EditValue = listFaf.Valor("SemIVA")
    '                End If
    '            End If
    '        End If

    '    End Sub

    '    Private Sub TextEditFatura2_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles TextEditFatura2.KeyDown


    '        If e.KeyCode = Windows.Forms.Keys.E.F4 Then

    '            If Me.TextEditCodigoFornecedor.EditValue = String.Empty Then
    '                MsgBox("O Fornecedor não está identificado.", vbInformation + vbOKOnly)
    '            Else
    '                Me.TextEditIdFatura2.EditValue = PSO.Listas.GetF4SQL("Lista Faturas Pendentes", "select h.DataDoc as 'Data', concat(h.TipoDoc,' ', h.NumDocint,'/', h.Serie) as 'N/Doc', h.NumDoc as 'V/Doc',abs(h.ValorTotal)-abs(h.TotalIva) as 'SemIVA', h.ValorTotal*-1 as 'ValorTotal', h.Id from Historico h inner join DocumentosCCT ct on ct.Documento=h.TipoDoc where ct.Natureza='D' and h.Entidade='" & Me.TextEditCodigoFornecedor.EditValue & "' and h.DataLiq is null and h.TipoEntidade='F'", "Id")
    '                If Me.TextEditIdFatura2.EditValue <> String.Empty Then
    '                    Dim listFaf As StdBELista
    '                    listFaf = BSO.Consulta("select h.NumDoc as 'Doc',abs(h.ValorTotal)-abs(h.TotalIva) as 'SemIVA', h.ValorTotal*-1 as 'ValorTotal'  from Historico h where h.Id='" & Me.TextEditFatura2.EditValue & "'")
    '                    listFaf.Inicio()
    '                    Me.TextEditFatura2.EditValue = listFaf.Valor("Doc")
    '                    Me.TextEditCusto2.EditValue = listFaf.Valor("SemIVA")
    '                End If
    '            End If
    '        End If

    '    End Sub

    '    Private Sub TextEditIdFatura_EditValueChanged(sender As Object, e As EventArgs) Handles TextEditIdFatura.EditValueChanged

    '        Dim listGRs As StdBELista
    '        If Me.TextEditIdFatura.EditValue <> String.Empty Then
    '            listGRs = BSO.Consulta("select  concat(h.TipoDoc,' ', h.NumDoc,'/', h.Serie) as 'Doc', h.Data, h.Nome  from CabecDoc h where h.CDU_IdFaturaTransporte='" & Me.TextEditIdFatura.EditValue & "' and h.Id!='" & EditorVendas.DocumentoVenda.ID & "'")
    '            listGRs.Inicio()

    '            If listGRs.Vazia = False Then
    '                Dim msg As String
    '                Dim i As Long
    '                msg = "Já existem os seguintes documentos associados a esta fatura:" & Chr(13) & Chr(13)
    '                For i = 1 To listGRs.NumLinhas
    '                    msg = msg & listGRs.Valor("Data") & " " & listGRs.Valor("Doc") & " - " & listGRs.Valor("Nome") & Chr(13)
    '                    listGRs.Seguinte()
    '                Next i
    '                MsgBox(msg, vbOKOnly, "Fatura Associada")
    '            End If
    '        End If

    '    End Sub

    '    Private Sub New()

    '        InitializeComponent()

    '        Dim listGR As StdBELista

    '        listGR = BSO.Consulta("select  h.CDU_EntidadeTransporte, h.CDU_IdFaturaTransporte, h.CDU_FaturaTransporte, isnull(h.CDU_CustoTransporte,0) + 0 as 'CDU_CustoTransporte' , h.CDU_IdFaturaTransporte2, h.CDU_FaturaTransporte2, isnull(h.CDU_CustoTransporte2,0) + 0 as 'CDU_CustoTransporte2', isnull(h.cdu_custotransportetotal,0) + 0 as 'cdu_custotransportetotal' , h.cdu_obstransporte  from CabecDoc h where h.Id='" & EditorVendas.DocumentoVenda.ID & "'")
    '        listGR.Inicio()

    '        If listGR.Vazia = False Then
    '            Me.TextEditCodigoFornecedor.EditValue = listGR.Valor("CDU_EntidadeTransporte")
    '            Me.TextEditIdFatura.EditValue = listGR.Valor("CDU_IdFaturaTransporte")
    '            Me.TextEditFatura.EditValue = listGR.Valor("CDU_FaturaTransporte")
    '            Me.TextEditCusto.EditValue = listGR.Valor("CDU_CustoTransporte")



    '            Me.TextEditIdFatura2.EditValue = listGR.Valor("CDU_IdFaturaTransporte2")
    '            Me.TextEditIdFatura2.EditValue = listGR.Valor("CDU_FaturaTransporte2")
    '            Me.TextEditCusto2.EditValue = listGR.Valor("CDU_CustoTransporte2")
    '            Me.TextEditTotal.EditValue = listGR.Valor("cdu_custotransportetotal")
    '            Me.MemoEditObsTransporte.EditValue = listGR.Valor("CDU_Obstransporte")

    '        End If
    '    End Sub

    '    Private Sub BarButtonItemFechar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemFechar.ItemClick

    '        Me.DialogResult = DialogResult.Cancel
    '        Me.Close()

    '    End Sub
End Class