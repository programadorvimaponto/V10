Imports Primavera.Extensibility.CustomForm
Imports Primavera.Extensibility.Purchases.Editors
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Public Class Form2
    Inherits CustomForm



    Dim k As Long
    Dim j As Long
    Dim LinhaActualizar As Long
    Dim Verifica As Integer
    Dim ListaSituacao As StdBELista
    Dim SqlStringSituacao As String
    Dim ListaTipoQualidade As StdBELista
    Dim SqlStringTipoQualidade As String
    Dim ListaPais As StdBELista
    Dim SqlStringPais As String
    Dim ListaCompMaritima As StdBELista
    Dim SqlStringCompMaritima As String
    Dim ListaPorto As StdBELista
    Dim SqlStringPorto As String
    'Rui Fernandes: 2019/08/06
    Dim ListaDestino As StdBELista
    Dim SqlStringDestino As String
    Dim Situacao As String
    Dim TipoQualidade As String
    Dim Pais As String
    Dim CompMaritima As String
    Dim Porto As String
    'Rui Fernandes: 2019/08/06
    Dim Destino As String
    Dim TestaStr As String
    Dim ValidaStr As String
    Dim Companhia As String
    Dim CompanhiaStr As String

    Dim SituacaoStr As String
    Dim TipoQualidadeStr As String
    Dim PaisStr As String
    Dim CompMaritimaStr As String
    Dim PortoStr As String
    Private WithEvents groupControl1 As DevExpress.XtraEditors.GroupControl
    Private WithEvents simpleButtonSeguinte As DevExpress.XtraEditors.SimpleButton
    Private WithEvents simpleButtonAnterior As DevExpress.XtraEditors.SimpleButton
    Private WithEvents textEditLinha As DevExpress.XtraEditors.TextEdit
    Private WithEvents label3 As Windows.Forms.Label
    Private WithEvents textEditLote As DevExpress.XtraEditors.TextEdit
    Private WithEvents label2 As Windows.Forms.Label
    Private WithEvents textEditDescArt As DevExpress.XtraEditors.TextEdit
    Private WithEvents textEditArtigo As DevExpress.XtraEditors.TextEdit
    Private WithEvents label1 As Windows.Forms.Label
    Private WithEvents groupControl2 As DevExpress.XtraEditors.GroupControl
    Private WithEvents simpleButtonTodas As DevExpress.XtraEditors.SimpleButton
    Private WithEvents simpleButtonInverteSel As DevExpress.XtraEditors.SimpleButton
    Private WithEvents simpleButtonAnulaSel As DevExpress.XtraEditors.SimpleButton
    Private WithEvents simpleButtonSelTodos As DevExpress.XtraEditors.SimpleButton
    Private WithEvents textEditNumCertificado As DevExpress.XtraEditors.TextEdit
    Private WithEvents textEditSeguradora As DevExpress.XtraEditors.TextEdit
    Private WithEvents lookUpEditCompMaritima As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents lookUpEditPorto As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents lookUpEditDestino As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents lookUpEditPais As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents checkEditDestino As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEditNumCertificado As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEditSeguradora As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEditPorto As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEditCompMaritima As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEditPais As DevExpress.XtraEditors.CheckEdit
    Private WithEvents memoEditObservacoes As DevExpress.XtraEditors.MemoEdit
    Private WithEvents textEditNumBL As DevExpress.XtraEditors.TextEdit
    Private WithEvents textEditNumContentor As DevExpress.XtraEditors.TextEdit
    Private WithEvents dateEditLimiteEmbarque As DevExpress.XtraEditors.DateEdit
    Private WithEvents dateEditEmbarque As DevExpress.XtraEditors.DateEdit
    Private WithEvents lookUpEditTipoQualidade As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents textEditLoteForn As DevExpress.XtraEditors.TextEdit
    Private WithEvents lookUpEditStiuacao As DevExpress.XtraEditors.LookUpEdit
    Private WithEvents checkEditLoteForn As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEditTipoQualidade As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEditEmbarque As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEdit4 As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEdit3 As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEdit2 As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEdit1 As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkEditSituacao As DevExpress.XtraEditors.CheckEdit
    'Rui Fernandes: 2019/08/06
    Dim DestinoStr As String

    Private Sub checkEditCompMaritima_EditValueChanged(sender As Object, e As EventArgs) Handles checkEditCompMaritima.EditValueChanged

        If Verifica = 0 Then
            If Me.checkEditCompMaritima.EditValue = False Then
                Me.checkEditPorto.EditValue = False
            End If
        End If

    End Sub

    Private Sub checkEditPorto_EditValueChanged(sender As Object, e As EventArgs) Handles checkEditPorto.EditValueChanged

        If Verifica = 0 Then
            If Me.checkEditPorto.EditValue = True Then
                Me.checkEditCompMaritima.EditValue = True
            End If
        End If

    End Sub

    Private Sub simpleButtonTodas_Click(sender As Object, e As EventArgs) Handles simpleButtonTodas.Click

        For k = 1 To EditorCompras.DocumentoCompra.Linhas.NumItens

            If EditorCompras.DocumentoCompra.Linhas.GetEdita(k).Artigo & "" <> "" Then

                'Rui Fernandes: 2019/08/06
                'If Me.ChkBoxDestino.Value = True Then
                '    EditorCompras.DocumentoCompra.Linhas(k).CamposUtil("CDU_Destino").Valor = Me.TxtDestino.Text
                'End If
                If Me.checkEditLoteForn.EditValue = True Then
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_LoteForn"].Valor = Me.TxtLoteForn.Text
                End If
                If Me.checkEditNumBL.EditValue = True Then
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_NumBL"].Valor = Me.TxtNumBL.Text
                End If
                If Me.checkEditNumCertificado.EditValue = True Then
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_NumCertificado"].Valor = Me.TxtNumCertificado.Text
                End If
                If Me.checkEditNumContentor.EditValue = True Then
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_NumContentor"].Valor = Me.TxtNumContentor.Text
                End If
                If Me.checkEditSeguradora.EditValue = True Then
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_Seguradora"].Valor = Me.TxtSeguradora.Text
                End If
                If Me.checkEditObservacoes.EditValue = True Then
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_Observacoes"].Valor = Me.TxtObservacoes.Text
                End If

                If Me.checkEditSituacao.EditValue = True Then
                    VerificaSituacao()
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_Situacao"].Valor = SituacaoStr
                End If
                If Me.checkEditTipoQualidade.EditValue = True Then
                    VerificaTipoQualidade()
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_TipoQualidade"].Valor = TipoQualidadeStr
                End If
                If Me.checkEditPais.EditValue = True Then
                    VerificaPais()
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_PaisOrigem"].Valor = PaisStr
                End If
                If Me.checkEditCompMaritima.EditValue = True Then
                    VerificaCompMaritima()
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_CompMaritima"].Valor = CompMaritimaStr
                End If
                If Me.checkEditPorto.EditValue = True Then
                    VerificaPorto()
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_Porto"].Valor = PortoStr
                End If
                'Rui Fernandes: 2019/08/06
                If Me.checkEditDestino.EditValue = True Then
                    VerificaDestino()
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_Destino"].Valor = DestinoStr
                End If

                If Me.checkEditEmbarque.EditValue = True Then
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_Embarque"].Valor = Format(Me.DTPickerEmbarque.Value, "yyyy-MM-d")
                End If
                If Me.checkEditLimiteEmbarque.EditValue = True Then
                    EditorCompras.DocumentoCompra.Linhas.GetEdita(k).CamposUtil["CDU_LimiteEmbarque"].Valor = Format(Me.DTPickerLimiteEmbarque.Value, "yyyy-MM-dd")
                End If

            End If

        Next k

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LimpaCampos()

        CarregaCombos()

        Me.textEditArtigo.EditValue = ""
        Me.textEditDescArt.EditValue = ""
        Me.textEditLote.EditValue = ""
        Me.textEditLinha.EditValue = ""

        Me.textEditArtigo.EditValue = ArtigoEnc
        Me.textEditDescArt.EditValue = DescArtEnc
        Me.textEditLote.EditValue = LoteEnc
        Me.textEditLinha.EditValue = LinhaEnc

        Me.textEditLoteForn.EditValue = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_LoteForn"].Valor & ""
        Me.textEditNumContentor.EditValue = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_NumContentor"].Valor & ""
        Me.textEditNumBL.EditValue = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_NumBL"].Valor & ""
        'Rui Fernandes: 2019/08/06
        'Me.TxtDestino.Text = EditorCompras.DocumentoCompra.Linhas(LinhaEnc).CamposUtil("CDU_Destino").Valor & ""
        Me.textEditSeguradora.EditValue = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_Seguradora"].Valor & ""
        Me.textEditNumCertificado.EditValue = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_NumCertificado"].Valor & ""
        Me.memoEditObservacoes.EditValue = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_Observacoes"].Valor & ""

        Situacao = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_Situacao"].Valor & ""
        TipoQualidade = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_TipoQualidade"].Valor & ""
        Pais = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_PaisOrigem"].Valor & ""
        CompMaritima = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_CompMaritima"].Valor & ""
        Porto = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_Porto"].Valor & ""
        'Rui Fernandes: 2019/08/06
        Destino = EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_Destino"].Valor & ""
        ColocaDadosCombo()

        If EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_Embarque"].Valor & "" = "" Then
            Me.dateEditEmbarque.EditValue = Now
        Else
            Me.dateEditEmbarque.EditValue = Format(EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_Embarque"].Valor, "yyyy-MM-dd")
        End If
        If EditorCompras.DocumentoCompra.Linhas(LinhaEnc).CamposUtil["CDU_LimiteEmbarque"].Valor & "" = "" Then
            Me.dateEditLimiteEmbarque.EditValue = Now
        Else
            Me.dateEditLimiteEmbarque.EditValue = Format(EditorCompras.DocumentoCompra.Linhas.GetEdita(LinhaEnc).CamposUtil["CDU_LimiteEmbarque"].Valor, "yyyy-MM-dd")
        End If

        Me.CmdBtnTodasasLinhas.Caption = "Todas as" & Chr(13) & "Linhas"

        Me.CmbSituacao.SetFocus

    End Sub

    Private Function ColocaDadosCombo()

        'Preenche dados na combo da situação
        SqlStringSituacao = "SELECT * FROM TDU_SITUACOESLOTE WHERE CDU_SITUACAO = '" & Situacao & "' ORDER BY CDU_SITUACAO DESC"

        ListaSituacao = BSO.Consulta(SqlStringSituacao)

        If ListaSituacao.Vazia = False Then

            ListaSituacao.Inicio()

            Me.lookUpEditStiuacao.EditValue = ListaSituacao.Valor("CDU_Situacao") & " - " & ListaSituacao.Valor("CDU_Descricao")

        End If





        'Preenche dados na combo tipo de qualidade
        SqlStringTipoQualidade = "SELECT * FROM TDU_TIPOQUALIDADE WHERE CDU_TIPOQUALIDADE = '" & TipoQualidade & "' ORDER BY CDU_TIPOQUALIDADE DESC"

        ListaTipoQualidade = BSO.Consulta(SqlStringTipoQualidade)

        If ListaTipoQualidade.Vazia = False Then

            ListaTipoQualidade.Inicio()

            Me.lookUpEditTipoQualidade.EditValue = ListaTipoQualidade.Valor("CDU_TipoQualidade") & " - " & ListaTipoQualidade.Valor("CDU_Descricao")

        End If

        'Preenche dados na combo do país
        SqlStringPais = "SELECT * FROM PAISES WHERE PAIS = '" & Pais & "' ORDER BY PAIS DESC"

        ListaPais = BSO.Consulta(SqlStringPais)

        If ListaPais.Vazia = False Then

            ListaPais.Inicio()

            Me.lookUpEditPais.EditValue = ListaPais.Valor("Pais") & " - " & ListaPais.Valor("Descricao")

        End If

        'Preenche dados na combo companhias maritimas
        SqlStringCompMaritima = "SELECT * FROM TDU_COMPMARITIMAS WHERE CDU_COMPANHIA = '" & CompMaritima & "' ORDER BY CDU_COMPANHIA DESC"

        ListaCompMaritima = BSO.Consulta(SqlStringCompMaritima)

        If ListaCompMaritima.Vazia = False Then

            ListaCompMaritima.Inicio()

            Me.lookUpEditCompMaritima.EditValue = ListaCompMaritima.Valor("CDU_Companhia") & " - " & ListaCompMaritima.Valor("CDU_Descricao")

        End If

        'Preenche dados na combo dos portos
        'Rui Fernandes: 2019/08/06
        'SqlStringPorto = "SELECT * FROM TDU_PORTO WHERE CDU_COMPANHIA = '" & CompMaritima & "' ORDER BY CDU_PORTO DESC"
        SqlStringPorto = "SELECT * FROM TDU_Locais WHERE CDU_Local = '" & Porto & "' ORDER BY CDU_Local DESC"

        ListaPorto = BSO.Consulta(SqlStringPorto)

        If ListaPorto.Vazia = False Then

            ListaPorto.Inicio()

            'Rui Fernandes: 2019/08/06
            'Me.CmbPorto.Text = ListaPorto("CDU_Porto") & " - " & ListaPorto("CDU_Descricao")
            Me.lookUpEditPorto.EditValue = ListaPorto.Valor("CDU_Local") & " - " & ListaPorto.Valor("CDU_Descricao")

        End If

        'Rui Fernandes: 2019/08/06
        'Preenche dados na combo dos destinos
        SqlStringDestino = "SELECT * FROM TDU_Locais WHERE CDU_Local = '" & Destino & "' ORDER BY CDU_Local DESC"

        ListaDestino = BSO.Consulta(SqlStringDestino)

        If ListaDestino.Vazia = False Then

            ListaDestino.Inicio()

            Me.lookUpEditDestino.EditValue = ListaDestino.Valor("CDU_Local") & " - " & ListaDestino.Valor("CDU_Descricao")

        End If

    End Function



    Private Function CarregaCombos()

        'Preenche combo das situações
        SqlStringSituacao = "SELECT * FROM TDU_SITUACOESLOTE ORDER BY CDU_SITUACAO DESC"

        ListaSituacao = BSO.Consulta(SqlStringSituacao)

        If ListaSituacao.Vazia = False Then

            ListaSituacao.Inicio()

            For k = 1 To ListaSituacao.NumLinhas

                Me.CmbSituacao.AddItem ListaSituacao("CDU_Situacao") & " - " & ListaSituacao("CDU_Descricao"), 0
            ListaSituacao.Seguinte()

            Next k

        End If

        'Preenche combo tipo de qualidade
        SqlStringTipoQualidade = "SELECT * FROM TDU_TIPOQUALIDADE ORDER BY CDU_TIPOQUALIDADE DESC"

        ListaTipoQualidade = BSO.Consulta(SqlStringTipoQualidade)

        If ListaTipoQualidade.Vazia = False Then

            ListaTipoQualidade.Inicio()

            For k = 1 To ListaTipoQualidade.NumLinhas

                Me.CmbTipoQualidade.AddItem ListaTipoQualidade("CDU_TipoQualidade") & " - " & ListaTipoQualidade("CDU_Descricao"), 0
            ListaTipoQualidade.Seguinte()

            Next k

        End If

        'Preenche combo dos países
        SqlStringPais = "SELECT * FROM PAISES ORDER BY PAIS DESC"

        ListaPais = BSO.Consulta(SqlStringPais)

        If ListaPais.Vazia = False Then

            ListaPais.Inicio()

            For k = 1 To ListaPais.NumLinhas

                Me.CmbPais.AddItem ListaPais("Pais") & " - " & ListaPais("Descricao"), 0
            ListaPais.Seguinte()

            Next k

        End If

        'Preenche combo companhias maritimas
        SqlStringCompMaritima = "SELECT * FROM TDU_COMPMARITIMAS ORDER BY CDU_COMPANHIA DESC"

        ListaCompMaritima = BSO.Consulta(SqlStringCompMaritima)

        If ListaCompMaritima.Vazia = False Then

            ListaCompMaritima.Inicio()

            For k = 1 To ListaCompMaritima.NumLinhas

                Me.CmbCompMaritima.AddItem ListaCompMaritima("CDU_Companhia") & " - " & ListaCompMaritima("CDU_Descricao"), 0
            ListaCompMaritima.Seguinte()

            Next k

        End If

        'Rui Fernandes: 2019/08/06
        'Preenche combo dos portos
        SqlStringPorto = "SELECT * FROM TDU_Locais ORDER BY CDU_Local DESC"

        ListaPorto = BSO.Consulta(SqlStringPorto)

        If ListaPorto.Vazia = False Then

            ListaPorto.Inicio()

            For k = 1 To ListaPorto.NumLinhas

                Me.CmbPorto.AddItem ListaPorto("CDU_Local") & " - " & ListaPorto("CDU_Descricao"), 0
            ListaPorto.Seguinte()

            Next k

        End If

        'Rui Fernandes: 2019/08/06
        'Preenche combo dos destinos
        SqlStringDestino = "SELECT * FROM TDU_Locais ORDER BY CDU_Local DESC"

        ListaDestino = BSO.Consulta(SqlStringDestino)

        If ListaDestino.Vazia = False Then

            ListaDestino.Inicio()

            For k = 1 To ListaDestino.NumLinhas

                Me.CmbDestino.AddItem ListaDestino("CDU_Local") & " - " & ListaDestino("CDU_Descricao"), 0
            ListaDestino.Seguinte()

            Next k

        End If

    End Function


    Private Sub LimpaCampos()

        Me.textEditLoteForn.EditValue = ""
        Me.textEditNumContentor.EditValue = ""
        Me.textEditNumBL.EditValue = ""
        Me.textEditSeguradora.EditValue = ""
        Me.textEditNumCertificado.EditValue = ""
        Me.memoEditObservacoes.EditValue = ""

        Me.lookUpEditStiuacao.EditValue = ""
        Me.lookUpEditTipoQualidade.EditValue = ""
        Me.lookUpEditPais.EditValue = ""
        Me.lookUpEditCompMaritima.EditValue = ""
        Me.lookUpEditPorto.EditValue = ""
        'Rui Fernandes: 2019/08/06
        Me.lookUpEditDestino.EditValue = ""

    End Sub

    Private Sub simpleButtonAnterior_Click(sender As Object, e As EventArgs) Handles simpleButtonAnterior.Click


        LinhaActualizar = Me.textEditLinha.EditValue
        GravarDados()

        For i = CInt(Me.textEditLinha.EditValue) - 1 To 1 Step -1

            LimpaCampos()

            If EditorCompras.DocumentoCompra.Linhas(i).Artigo & "" <> "" Then
                Me.TxtArtigo.Text = EditorCompras.DocumentoCompra.Linhas(i).Artigo
                Me.TxtDescArt.Text = EditorCompras.DocumentoCompra.Linhas(i).Descricao
                Me.TxtLote.Text = EditorCompras.DocumentoCompra.Linhas(i).lote
                Me.TxtLinha.Text = i

                Me.TxtLoteForn.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_LoteForn").Valor & ""
                Me.TxtNumContentor.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_NumContentor").Valor & ""
                Me.TxtNumBL.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_NumBL").Valor & ""
                'Rui Fernandes: 2019/08/06
                'Me.TxtDestino.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Destino").Valor & ""
                Me.TxtSeguradora.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Seguradora").Valor & ""
                Me.TxtNumCertificado.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_NumCertificado").Valor & ""
                Me.TxtObservacoes.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Observacoes").Valor & ""

                Situacao = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Situacao").Valor & ""
                TipoQualidade = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_TipoQualidade").Valor & ""
                Pais = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_PaisOrigem").Valor & ""
                CompMaritima = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_CompMaritima").Valor & ""
                Porto = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Porto").Valor & ""
                'Rui Fernandes: 2019/08/06
                Destino = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Destino").Valor & ""
                ColocaDadosCombo()

                If EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Embarque").Valor & "" = "" Then
                    Me.DTPickerEmbarque.Value = Now
                Else
                    Me.DTPickerEmbarque.Value = Format(EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Embarque").Valor, "yyyy-MM-dd")
                End If
                If EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_LimiteEmbarque").Valor & "" = "" Then
                    Me.DTPickerLimiteEmbarque.Value = Now
                Else
                    Me.DTPickerLimiteEmbarque.Value = Format(EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_LimiteEmbarque").Valor, "yyyy-MM-dd")
                End If

                Exit Sub

            End If

        Next

    End Sub

    Private Sub simpleButtonSeguinte_Click(sender As Object, e As EventArgs) Handles simpleButtonSeguinte.Click

        LinhaActualizar = Me.TxtLinha.Text
        GravarDados()

        For i = CInt(Me.TxtLinha.Text) + 1 To EditorCompras.DocumentoCompra.Linhas.NumItens

            LimpaCampos()

            If EditorCompras.DocumentoCompra.Linhas(i).Artigo & "" <> "" Then
                Me.TxtArtigo.Text = EditorCompras.DocumentoCompra.Linhas(i).Artigo
                Me.TxtDescArt.Text = EditorCompras.DocumentoCompra.Linhas(i).Descricao
                Me.TxtLote.Text = EditorCompras.DocumentoCompra.Linhas(i).lote
                Me.TxtLinha.Text = i

                Me.TxtLoteForn.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_LoteForn").Valor & ""
                Me.TxtNumContentor.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_NumContentor").Valor & ""
                Me.TxtNumBL.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_NumBL").Valor & ""
                'Rui Fernandes: 2019/08/06
                'Me.TxtDestino.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Destino").Valor & ""
                Me.TxtSeguradora.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Seguradora").Valor & ""
                Me.TxtNumCertificado.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_NumCertificado").Valor & ""
                Me.TxtObservacoes.Text = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Observacoes").Valor & ""

                Situacao = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Situacao").Valor & ""
                TipoQualidade = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_TipoQualidade").Valor & ""
                Pais = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_PaisOrigem").Valor & ""
                CompMaritima = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_CompMaritima").Valor & ""
                Porto = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Porto").Valor & ""
                'Rui Fernandes: 2019/08/06
                Destino = EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Destino").Valor & ""
                ColocaDadosCombo()

                If EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Embarque").Valor & "" = "" Then
                    Me.DTPickerEmbarque.Value = Now
                Else
                    Me.DTPickerEmbarque.Value = Format(EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_Embarque").Valor, "yyyy-MM-dd")
                End If
                If EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_LimiteEmbarque").Valor & "" = "" Then
                    Me.DTPickerLimiteEmbarque.Value = Now
                Else
                    Me.DTPickerLimiteEmbarque.Value = Format(EditorCompras.DocumentoCompra.Linhas(i).CamposUtil("CDU_LimiteEmbarque").Valor, "yyyy-MM-dd")
                End If

                Exit Sub

            End If

        Next i


    End Sub

    Private Function VerificaSituacao()

        TestaStr = ""
        ValidaStr = ""

        TestaStr = Me.CmbSituacao.Text

        For j = 1 To Len(Me.CmbSituacao.Text)

            ValidaStr = Left(TestaStr, 1)

            If ValidaStr = " " Then
                SituacaoStr = Left(Me.CmbSituacao.Text, j - 1)
                Exit For
            Else
                TestaStr = Right(TestaStr, Len(Me.CmbSituacao.Text) - j)
            End If

        Next j

    End Function

    Private Function VerificaTipoQualidade()

        TestaStr = ""
        ValidaStr = ""

        TestaStr = Me.CmbTipoQualidade.Text

        For j = 1 To Len(Me.CmbTipoQualidade.Text)

            ValidaStr = Left(TestaStr, 1)

            If ValidaStr = " " Then
                TipoQualidadeStr = Left(Me.CmbTipoQualidade.Text, j - 1)
                Exit For
            Else
                TestaStr = Right(TestaStr, Len(Me.CmbTipoQualidade.Text) - j)
            End If

        Next j

    End Function

    Private Function VerificaPais()

        TestaStr = ""
        ValidaStr = ""

        TestaStr = Me.CmbPais.Text

        For j = 1 To Len(Me.CmbPais.Text)

            ValidaStr = Left(TestaStr, 1)

            If ValidaStr = " " Then
                PaisStr = Left(Me.CmbPais.Text, j - 1)
                Exit For
            Else
                TestaStr = Right(TestaStr, Len(Me.CmbPais.Text) - j)
            End If

        Next j

    End Function

    Private Function VerificaCompMaritima()

        TestaStr = ""
        ValidaStr = ""

        TestaStr = Me.CmbCompMaritima.Text

        For j = 1 To Len(Me.CmbCompMaritima.Text)

            ValidaStr = Left(TestaStr, 1)

            If ValidaStr = " " Then
                CompMaritimaStr = Left(Me.CmbCompMaritima.Text, j - 1)
                Exit For
            Else
                TestaStr = Right(TestaStr, Len(Me.CmbCompMaritima.Text) - j)
            End If

        Next j

    End Function

    Private Function VerificaPorto()

        TestaStr = ""
        ValidaStr = ""

        TestaStr = Me.CmbPorto.Text

        For j = 1 To Len(Me.CmbPorto.Text)

            ValidaStr = Left(TestaStr, 1)

            If ValidaStr = " " Then
                PortoStr = Left(Me.CmbPorto.Text, j - 1)
                Exit For
            Else
                TestaStr = Right(TestaStr, Len(Me.CmbPorto.Text) - j)
            End If

        Next j

    End Function

    'Rui Fernandes: 2019/08/06
    Private Function VerificaDestino()

        TestaStr = ""
        ValidaStr = ""

        TestaStr = Me.CmbDestino.Text

        For j = 1 To Len(Me.CmbDestino.Text)

            ValidaStr = Left(TestaStr, 1)

            If ValidaStr = " " Then
                DestinoStr = Left(Me.CmbDestino.Text, j - 1)
                Exit For
            Else
                TestaStr = Right(TestaStr, Len(Me.CmbDestino.Text) - j)
            End If

        Next j

    End Function


    Private Sub GravarDados()

        'Rui Fernandes: 2019/08/06
        'EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_Destino").Valor = Me.TxtDestino.Text
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_LoteForn").Valor = Me.TxtLoteForn.Text
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_NumBL").Valor = Me.TxtNumBL.Text
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_NumCertificado").Valor = Me.TxtNumCertificado.Text
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_NumContentor").Valor = Me.TxtNumContentor.Text
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_Seguradora").Valor = Me.TxtSeguradora.Text
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_Observacoes").Valor = Me.TxtObservacoes.Text

        VerificaSituacao()
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_Situacao").Valor = SituacaoStr
        VerificaTipoQualidade()
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_TipoQualidade").Valor = TipoQualidadeStr
        VerificaPais()
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_PaisOrigem").Valor = PaisStr
        VerificaCompMaritima()
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_CompMaritima").Valor = CompMaritimaStr
        VerificaPorto()
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_Porto").Valor = PortoStr
        'Rui Fernandes: 2019/08/06
        VerificaDestino()
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_Destino").Valor = DestinoStr

        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_Embarque").Valor = Format(Me.DTPickerEmbarque.Value, "yyyy-MM-d")
        EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_LimiteEmbarque").Valor = Format(Me.DTPickerLimiteEmbarque.Value, "yyyy-MM-dd")

    End Sub

    Private Sub simpleButtonSelTodos_Click(sender As Object, e As EventArgs) Handles simpleButtonSelTodos.Click

        Verifica = 1

        Me.ChkBoxCompMaritima.Value = True
        Me.ChkBoxDestino.Value = True
        Me.ChkBoxEmbarque.Value = True
        Me.ChkBoxLimiteEmbarque.Value = True
        Me.ChkBoxLoteForn.Value = True
        Me.ChkBoxNumBL.Value = True
        Me.ChkBoxNumCertificado.Value = True
        Me.ChkBoxNumContentor.Value = True
        Me.ChkBoxObservacoes.Value = True
        Me.ChkBoxPais.Value = True
        Me.ChkBoxPorto.Value = True
        Me.ChkBoxSeguradora.Value = True
        Me.ChkBoxSituacao.Value = True
        Me.ChkBoxTipoQualidade.Value = True

        Verifica = 0

    End Sub

    Private Sub simpleButtonAnulaSel_Click(sender As Object, e As EventArgs) Handles simpleButtonAnulaSel.Click

        Verifica = 1

        Me.ChkBoxCompMaritima.Value = False
        Me.ChkBoxDestino.Value = False
        Me.ChkBoxEmbarque.Value = False
        Me.ChkBoxLimiteEmbarque.Value = False
        Me.ChkBoxLoteForn.Value = False
        Me.ChkBoxNumBL.Value = False
        Me.ChkBoxNumCertificado.Value = False
        Me.ChkBoxNumContentor.Value = False
        Me.ChkBoxObservacoes.Value = False
        Me.ChkBoxPais.Value = False
        Me.ChkBoxPorto.Value = False
        Me.ChkBoxSeguradora.Value = False
        Me.ChkBoxSituacao.Value = False
        Me.ChkBoxTipoQualidade.Value = False

        Verifica = 0

    End Sub

    Private Sub simpleButtonInverteSel_Click(sender As Object, e As EventArgs) Handles simpleButtonInverteSel.Click

        Verifica = 1

        If Me.ChkBoxCompMaritima.Value = True Then
            Me.ChkBoxCompMaritima.Value = False
            Me.ChkBoxPorto.Value = False
        Else
            Me.ChkBoxCompMaritima.Value = True
            Me.ChkBoxPorto.Value = True
        End If

        If Me.ChkBoxDestino.Value = True Then
            Me.ChkBoxDestino.Value = False
        Else
            Me.ChkBoxDestino.Value = True
        End If

        If Me.ChkBoxEmbarque.Value = True Then
            Me.ChkBoxEmbarque.Value = False
        Else
            Me.ChkBoxEmbarque.Value = True
        End If

        If Me.ChkBoxLimiteEmbarque.Value = True Then
            Me.ChkBoxLimiteEmbarque.Value = False
        Else
            Me.ChkBoxLimiteEmbarque.Value = True
        End If

        If Me.ChkBoxLoteForn.Value = True Then
            Me.ChkBoxLoteForn.Value = False
        Else
            Me.ChkBoxLoteForn.Value = True
        End If

        If Me.ChkBoxNumBL.Value = True Then
            Me.ChkBoxNumBL.Value = False
        Else
            Me.ChkBoxNumBL.Value = True
        End If

        If Me.ChkBoxNumCertificado.Value = True Then
            Me.ChkBoxNumCertificado.Value = False
        Else
            Me.ChkBoxNumCertificado.Value = True
        End If

        If Me.ChkBoxNumContentor.Value = True Then
            Me.ChkBoxNumContentor.Value = False
        Else
            Me.ChkBoxNumContentor.Value = True
        End If

        If Me.ChkBoxObservacoes.Value = True Then
            Me.ChkBoxObservacoes.Value = False
        Else
            Me.ChkBoxObservacoes.Value = True
        End If

        If Me.ChkBoxPais.Value = True Then
            Me.ChkBoxPais.Value = False
        Else
            Me.ChkBoxPais.Value = True
        End If

        If Me.ChkBoxSeguradora.Value = True Then
            Me.ChkBoxSeguradora.Value = False
        Else
            Me.ChkBoxSeguradora.Value = True
        End If

        If Me.ChkBoxSituacao.Value = True Then
            Me.ChkBoxSituacao.Value = False
        Else
            Me.ChkBoxSituacao.Value = True
        End If

        If Me.ChkBoxTipoQualidade.Value = True Then
            Me.ChkBoxTipoQualidade.Value = False
        Else
            Me.ChkBoxTipoQualidade.Value = True
        End If

        Verifica = 0

    End Sub

    Private Sub InitializeComponent()
        Me.groupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.simpleButtonSeguinte = New DevExpress.XtraEditors.SimpleButton()
        Me.simpleButtonAnterior = New DevExpress.XtraEditors.SimpleButton()
        Me.textEditLinha = New DevExpress.XtraEditors.TextEdit()
        Me.label3 = New System.Windows.Forms.Label()
        Me.textEditLote = New DevExpress.XtraEditors.TextEdit()
        Me.label2 = New System.Windows.Forms.Label()
        Me.textEditDescArt = New DevExpress.XtraEditors.TextEdit()
        Me.textEditArtigo = New DevExpress.XtraEditors.TextEdit()
        Me.label1 = New System.Windows.Forms.Label()
        Me.groupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.simpleButtonTodas = New DevExpress.XtraEditors.SimpleButton()
        Me.simpleButtonInverteSel = New DevExpress.XtraEditors.SimpleButton()
        Me.simpleButtonAnulaSel = New DevExpress.XtraEditors.SimpleButton()
        Me.simpleButtonSelTodos = New DevExpress.XtraEditors.SimpleButton()
        Me.textEditNumCertificado = New DevExpress.XtraEditors.TextEdit()
        Me.textEditSeguradora = New DevExpress.XtraEditors.TextEdit()
        Me.lookUpEditCompMaritima = New DevExpress.XtraEditors.LookUpEdit()
        Me.lookUpEditPorto = New DevExpress.XtraEditors.LookUpEdit()
        Me.lookUpEditDestino = New DevExpress.XtraEditors.LookUpEdit()
        Me.lookUpEditPais = New DevExpress.XtraEditors.LookUpEdit()
        Me.checkEditDestino = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEditNumCertificado = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEditSeguradora = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEditPorto = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEditCompMaritima = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEditPais = New DevExpress.XtraEditors.CheckEdit()
        Me.memoEditObservacoes = New DevExpress.XtraEditors.MemoEdit()
        Me.textEditNumBL = New DevExpress.XtraEditors.TextEdit()
        Me.textEditNumContentor = New DevExpress.XtraEditors.TextEdit()
        Me.dateEditLimiteEmbarque = New DevExpress.XtraEditors.DateEdit()
        Me.dateEditEmbarque = New DevExpress.XtraEditors.DateEdit()
        Me.lookUpEditTipoQualidade = New DevExpress.XtraEditors.LookUpEdit()
        Me.textEditLoteForn = New DevExpress.XtraEditors.TextEdit()
        Me.lookUpEditStiuacao = New DevExpress.XtraEditors.LookUpEdit()
        Me.checkEditLoteForn = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEditTipoQualidade = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEditEmbarque = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEdit4 = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEdit3 = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEdit2 = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEdit1 = New DevExpress.XtraEditors.CheckEdit()
        Me.checkEditSituacao = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.groupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupControl1.SuspendLayout()
        CType(Me.textEditLinha.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditDescArt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditArtigo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupControl2.SuspendLayout()
        CType(Me.textEditNumCertificado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditSeguradora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditCompMaritima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditPorto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditPais.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditDestino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditNumCertificado.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditSeguradora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditPorto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditCompMaritima.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditPais.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.memoEditObservacoes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditNumBL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditNumContentor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateEditLimiteEmbarque.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateEditLimiteEmbarque.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateEditEmbarque.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateEditEmbarque.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditTipoQualidade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.textEditLoteForn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lookUpEditStiuacao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditLoteForn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditTipoQualidade.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditEmbarque.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEdit4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditSituacao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'groupControl1
        '
        Me.groupControl1.Controls.Add(Me.simpleButtonSeguinte)
        Me.groupControl1.Controls.Add(Me.simpleButtonAnterior)
        Me.groupControl1.Controls.Add(Me.textEditLinha)
        Me.groupControl1.Controls.Add(Me.label3)
        Me.groupControl1.Controls.Add(Me.textEditLote)
        Me.groupControl1.Controls.Add(Me.label2)
        Me.groupControl1.Controls.Add(Me.textEditDescArt)
        Me.groupControl1.Controls.Add(Me.textEditArtigo)
        Me.groupControl1.Controls.Add(Me.label1)
        Me.groupControl1.Location = New System.Drawing.Point(3, 3)
        Me.groupControl1.Name = "groupControl1"
        Me.groupControl1.Size = New System.Drawing.Size(526, 90)
        Me.groupControl1.TabIndex = 1
        '
        'simpleButtonSeguinte
        '
        Me.simpleButtonSeguinte.Location = New System.Drawing.Point(326, 59)
        Me.simpleButtonSeguinte.Name = "simpleButtonSeguinte"
        Me.simpleButtonSeguinte.Size = New System.Drawing.Size(38, 23)
        Me.simpleButtonSeguinte.TabIndex = 8
        Me.simpleButtonSeguinte.Text = ">"
        '
        'simpleButtonAnterior
        '
        Me.simpleButtonAnterior.Location = New System.Drawing.Point(282, 59)
        Me.simpleButtonAnterior.Name = "simpleButtonAnterior"
        Me.simpleButtonAnterior.Size = New System.Drawing.Size(38, 23)
        Me.simpleButtonAnterior.TabIndex = 7
        Me.simpleButtonAnterior.Text = "<"
        '
        'textEditLinha
        '
        Me.textEditLinha.EditValue = ""
        Me.textEditLinha.Location = New System.Drawing.Point(221, 62)
        Me.textEditLinha.Name = "textEditLinha"
        Me.textEditLinha.Size = New System.Drawing.Size(43, 20)
        Me.textEditLinha.TabIndex = 6
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(183, 65)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(32, 13)
        Me.label3.TabIndex = 5
        Me.label3.Text = "Linha"
        '
        'textEditLote
        '
        Me.textEditLote.EditValue = ""
        Me.textEditLote.Location = New System.Drawing.Point(56, 62)
        Me.textEditLote.Name = "textEditLote"
        Me.textEditLote.Size = New System.Drawing.Size(121, 20)
        Me.textEditLote.TabIndex = 4
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(14, 65)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(28, 13)
        Me.label2.TabIndex = 3
        Me.label2.Text = "Lote"
        '
        'textEditDescArt
        '
        Me.textEditDescArt.Location = New System.Drawing.Point(183, 28)
        Me.textEditDescArt.Name = "textEditDescArt"
        Me.textEditDescArt.Size = New System.Drawing.Size(336, 20)
        Me.textEditDescArt.TabIndex = 2
        '
        'textEditArtigo
        '
        Me.textEditArtigo.EditValue = ""
        Me.textEditArtigo.Location = New System.Drawing.Point(56, 28)
        Me.textEditArtigo.Name = "textEditArtigo"
        Me.textEditArtigo.Size = New System.Drawing.Size(121, 20)
        Me.textEditArtigo.TabIndex = 1
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(14, 31)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(36, 13)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Artigo"
        '
        'groupControl2
        '
        Me.groupControl2.Controls.Add(Me.simpleButtonTodas)
        Me.groupControl2.Controls.Add(Me.simpleButtonInverteSel)
        Me.groupControl2.Controls.Add(Me.simpleButtonAnulaSel)
        Me.groupControl2.Controls.Add(Me.simpleButtonSelTodos)
        Me.groupControl2.Controls.Add(Me.textEditNumCertificado)
        Me.groupControl2.Controls.Add(Me.textEditSeguradora)
        Me.groupControl2.Controls.Add(Me.lookUpEditCompMaritima)
        Me.groupControl2.Controls.Add(Me.lookUpEditPorto)
        Me.groupControl2.Controls.Add(Me.lookUpEditDestino)
        Me.groupControl2.Controls.Add(Me.lookUpEditPais)
        Me.groupControl2.Controls.Add(Me.checkEditDestino)
        Me.groupControl2.Controls.Add(Me.checkEditNumCertificado)
        Me.groupControl2.Controls.Add(Me.checkEditSeguradora)
        Me.groupControl2.Controls.Add(Me.checkEditPorto)
        Me.groupControl2.Controls.Add(Me.checkEditCompMaritima)
        Me.groupControl2.Controls.Add(Me.checkEditPais)
        Me.groupControl2.Controls.Add(Me.memoEditObservacoes)
        Me.groupControl2.Controls.Add(Me.textEditNumBL)
        Me.groupControl2.Controls.Add(Me.textEditNumContentor)
        Me.groupControl2.Controls.Add(Me.dateEditLimiteEmbarque)
        Me.groupControl2.Controls.Add(Me.dateEditEmbarque)
        Me.groupControl2.Controls.Add(Me.lookUpEditTipoQualidade)
        Me.groupControl2.Controls.Add(Me.textEditLoteForn)
        Me.groupControl2.Controls.Add(Me.lookUpEditStiuacao)
        Me.groupControl2.Controls.Add(Me.checkEditLoteForn)
        Me.groupControl2.Controls.Add(Me.checkEditTipoQualidade)
        Me.groupControl2.Controls.Add(Me.checkEditEmbarque)
        Me.groupControl2.Controls.Add(Me.checkEdit4)
        Me.groupControl2.Controls.Add(Me.checkEdit3)
        Me.groupControl2.Controls.Add(Me.checkEdit2)
        Me.groupControl2.Controls.Add(Me.checkEdit1)
        Me.groupControl2.Controls.Add(Me.checkEditSituacao)
        Me.groupControl2.Location = New System.Drawing.Point(3, 99)
        Me.groupControl2.Name = "groupControl2"
        Me.groupControl2.Size = New System.Drawing.Size(526, 369)
        Me.groupControl2.TabIndex = 2
        Me.groupControl2.Text = "Informação Adicional"
        '
        'simpleButtonTodas
        '
        Me.simpleButtonTodas.Location = New System.Drawing.Point(451, 211)
        Me.simpleButtonTodas.Name = "simpleButtonTodas"
        Me.simpleButtonTodas.Size = New System.Drawing.Size(68, 51)
        Me.simpleButtonTodas.TabIndex = 31
        Me.simpleButtonTodas.Text = "Todas"
        '
        'simpleButtonInverteSel
        '
        Me.simpleButtonInverteSel.Location = New System.Drawing.Point(370, 225)
        Me.simpleButtonInverteSel.Name = "simpleButtonInverteSel"
        Me.simpleButtonInverteSel.Size = New System.Drawing.Size(75, 23)
        Me.simpleButtonInverteSel.TabIndex = 30
        Me.simpleButtonInverteSel.Text = "Inverte Sel."
        '
        'simpleButtonAnulaSel
        '
        Me.simpleButtonAnulaSel.Location = New System.Drawing.Point(289, 239)
        Me.simpleButtonAnulaSel.Name = "simpleButtonAnulaSel"
        Me.simpleButtonAnulaSel.Size = New System.Drawing.Size(75, 23)
        Me.simpleButtonAnulaSel.TabIndex = 29
        Me.simpleButtonAnulaSel.Text = "Anula Sel."
        '
        'simpleButtonSelTodos
        '
        Me.simpleButtonSelTodos.Location = New System.Drawing.Point(289, 211)
        Me.simpleButtonSelTodos.Name = "simpleButtonSelTodos"
        Me.simpleButtonSelTodos.Size = New System.Drawing.Size(75, 23)
        Me.simpleButtonSelTodos.TabIndex = 28
        Me.simpleButtonSelTodos.Text = "Sel. Todos"
        '
        'textEditNumCertificado
        '
        Me.textEditNumCertificado.EditValue = ""
        Me.textEditNumCertificado.Location = New System.Drawing.Point(370, 183)
        Me.textEditNumCertificado.Name = "textEditNumCertificado"
        Me.textEditNumCertificado.Size = New System.Drawing.Size(149, 20)
        Me.textEditNumCertificado.TabIndex = 27
        '
        'textEditSeguradora
        '
        Me.textEditSeguradora.EditValue = ""
        Me.textEditSeguradora.Location = New System.Drawing.Point(370, 153)
        Me.textEditSeguradora.Name = "textEditSeguradora"
        Me.textEditSeguradora.Size = New System.Drawing.Size(149, 20)
        Me.textEditSeguradora.TabIndex = 26
        '
        'lookUpEditCompMaritima
        '
        Me.lookUpEditCompMaritima.Location = New System.Drawing.Point(370, 63)
        Me.lookUpEditCompMaritima.Name = "lookUpEditCompMaritima"
        Me.lookUpEditCompMaritima.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditCompMaritima.Size = New System.Drawing.Size(149, 20)
        Me.lookUpEditCompMaritima.TabIndex = 25
        '
        'lookUpEditPorto
        '
        Me.lookUpEditPorto.Location = New System.Drawing.Point(370, 93)
        Me.lookUpEditPorto.Name = "lookUpEditPorto"
        Me.lookUpEditPorto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditPorto.Size = New System.Drawing.Size(149, 20)
        Me.lookUpEditPorto.TabIndex = 24
        '
        'lookUpEditDestino
        '
        Me.lookUpEditDestino.Location = New System.Drawing.Point(370, 123)
        Me.lookUpEditDestino.Name = "lookUpEditDestino"
        Me.lookUpEditDestino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditDestino.Size = New System.Drawing.Size(149, 20)
        Me.lookUpEditDestino.TabIndex = 23
        '
        'lookUpEditPais
        '
        Me.lookUpEditPais.Location = New System.Drawing.Point(370, 33)
        Me.lookUpEditPais.Name = "lookUpEditPais"
        Me.lookUpEditPais.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditPais.Size = New System.Drawing.Size(149, 20)
        Me.lookUpEditPais.TabIndex = 22
        '
        'checkEditDestino
        '
        Me.checkEditDestino.Location = New System.Drawing.Point(270, 123)
        Me.checkEditDestino.Name = "checkEditDestino"
        Me.checkEditDestino.Properties.Caption = "Destino"
        Me.checkEditDestino.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditDestino.Size = New System.Drawing.Size(94, 19)
        Me.checkEditDestino.TabIndex = 21
        '
        'checkEditNumCertificado
        '
        Me.checkEditNumCertificado.Location = New System.Drawing.Point(270, 183)
        Me.checkEditNumCertificado.Name = "checkEditNumCertificado"
        Me.checkEditNumCertificado.Properties.Caption = "Nº Certificado"
        Me.checkEditNumCertificado.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditNumCertificado.Size = New System.Drawing.Size(94, 19)
        Me.checkEditNumCertificado.TabIndex = 20
        '
        'checkEditSeguradora
        '
        Me.checkEditSeguradora.Location = New System.Drawing.Point(270, 153)
        Me.checkEditSeguradora.Name = "checkEditSeguradora"
        Me.checkEditSeguradora.Properties.Caption = "Seguradora"
        Me.checkEditSeguradora.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditSeguradora.Size = New System.Drawing.Size(94, 19)
        Me.checkEditSeguradora.TabIndex = 19
        '
        'checkEditPorto
        '
        Me.checkEditPorto.Location = New System.Drawing.Point(270, 93)
        Me.checkEditPorto.Name = "checkEditPorto"
        Me.checkEditPorto.Properties.Caption = "Porto"
        Me.checkEditPorto.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditPorto.Size = New System.Drawing.Size(94, 19)
        Me.checkEditPorto.TabIndex = 18
        '
        'checkEditCompMaritima
        '
        Me.checkEditCompMaritima.Location = New System.Drawing.Point(270, 63)
        Me.checkEditCompMaritima.Name = "checkEditCompMaritima"
        Me.checkEditCompMaritima.Properties.Caption = "Comp. Marítima"
        Me.checkEditCompMaritima.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditCompMaritima.Size = New System.Drawing.Size(94, 19)
        Me.checkEditCompMaritima.TabIndex = 17
        '
        'checkEditPais
        '
        Me.checkEditPais.Location = New System.Drawing.Point(270, 33)
        Me.checkEditPais.Name = "checkEditPais"
        Me.checkEditPais.Properties.Caption = "País Origem"
        Me.checkEditPais.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditPais.Size = New System.Drawing.Size(94, 19)
        Me.checkEditPais.TabIndex = 16
        '
        'memoEditObservacoes
        '
        Me.memoEditObservacoes.Location = New System.Drawing.Point(5, 268)
        Me.memoEditObservacoes.Name = "memoEditObservacoes"
        Me.memoEditObservacoes.Size = New System.Drawing.Size(514, 96)
        Me.memoEditObservacoes.TabIndex = 15
        '
        'textEditNumBL
        '
        Me.textEditNumBL.EditValue = ""
        Me.textEditNumBL.Location = New System.Drawing.Point(115, 213)
        Me.textEditNumBL.Name = "textEditNumBL"
        Me.textEditNumBL.Size = New System.Drawing.Size(149, 20)
        Me.textEditNumBL.TabIndex = 14
        '
        'textEditNumContentor
        '
        Me.textEditNumContentor.EditValue = ""
        Me.textEditNumContentor.Location = New System.Drawing.Point(115, 183)
        Me.textEditNumContentor.Name = "textEditNumContentor"
        Me.textEditNumContentor.Size = New System.Drawing.Size(149, 20)
        Me.textEditNumContentor.TabIndex = 13
        '
        'dateEditLimiteEmbarque
        '
        Me.dateEditLimiteEmbarque.EditValue = Nothing
        Me.dateEditLimiteEmbarque.Location = New System.Drawing.Point(115, 153)
        Me.dateEditLimiteEmbarque.Name = "dateEditLimiteEmbarque"
        Me.dateEditLimiteEmbarque.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateEditLimiteEmbarque.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateEditLimiteEmbarque.Size = New System.Drawing.Size(149, 20)
        Me.dateEditLimiteEmbarque.TabIndex = 12
        '
        'dateEditEmbarque
        '
        Me.dateEditEmbarque.EditValue = Nothing
        Me.dateEditEmbarque.Location = New System.Drawing.Point(115, 123)
        Me.dateEditEmbarque.Name = "dateEditEmbarque"
        Me.dateEditEmbarque.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateEditEmbarque.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dateEditEmbarque.Size = New System.Drawing.Size(149, 20)
        Me.dateEditEmbarque.TabIndex = 11
        '
        'lookUpEditTipoQualidade
        '
        Me.lookUpEditTipoQualidade.Location = New System.Drawing.Point(115, 93)
        Me.lookUpEditTipoQualidade.Name = "lookUpEditTipoQualidade"
        Me.lookUpEditTipoQualidade.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditTipoQualidade.Size = New System.Drawing.Size(149, 20)
        Me.lookUpEditTipoQualidade.TabIndex = 10
        '
        'textEditLoteForn
        '
        Me.textEditLoteForn.EditValue = ""
        Me.textEditLoteForn.Location = New System.Drawing.Point(115, 63)
        Me.textEditLoteForn.Name = "textEditLoteForn"
        Me.textEditLoteForn.Size = New System.Drawing.Size(149, 20)
        Me.textEditLoteForn.TabIndex = 9
        '
        'lookUpEditStiuacao
        '
        Me.lookUpEditStiuacao.Location = New System.Drawing.Point(115, 33)
        Me.lookUpEditStiuacao.Name = "lookUpEditStiuacao"
        Me.lookUpEditStiuacao.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lookUpEditStiuacao.Size = New System.Drawing.Size(149, 20)
        Me.lookUpEditStiuacao.TabIndex = 8
        '
        'checkEditLoteForn
        '
        Me.checkEditLoteForn.Location = New System.Drawing.Point(6, 63)
        Me.checkEditLoteForn.Name = "checkEditLoteForn"
        Me.checkEditLoteForn.Properties.Caption = "Lote Forn"
        Me.checkEditLoteForn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditLoteForn.Size = New System.Drawing.Size(99, 19)
        Me.checkEditLoteForn.TabIndex = 7
        '
        'checkEditTipoQualidade
        '
        Me.checkEditTipoQualidade.Location = New System.Drawing.Point(6, 93)
        Me.checkEditTipoQualidade.Name = "checkEditTipoQualidade"
        Me.checkEditTipoQualidade.Properties.Caption = "Tipo Qualidade"
        Me.checkEditTipoQualidade.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditTipoQualidade.Size = New System.Drawing.Size(99, 19)
        Me.checkEditTipoQualidade.TabIndex = 6
        '
        'checkEditEmbarque
        '
        Me.checkEditEmbarque.Location = New System.Drawing.Point(6, 123)
        Me.checkEditEmbarque.Name = "checkEditEmbarque"
        Me.checkEditEmbarque.Properties.Caption = "Embarque"
        Me.checkEditEmbarque.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditEmbarque.Size = New System.Drawing.Size(99, 19)
        Me.checkEditEmbarque.TabIndex = 5
        '
        'checkEdit4
        '
        Me.checkEdit4.Location = New System.Drawing.Point(6, 153)
        Me.checkEdit4.Name = "checkEdit4"
        Me.checkEdit4.Properties.Caption = "Limite Embarque"
        Me.checkEdit4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEdit4.Size = New System.Drawing.Size(99, 19)
        Me.checkEdit4.TabIndex = 4
        '
        'checkEdit3
        '
        Me.checkEdit3.Location = New System.Drawing.Point(6, 183)
        Me.checkEdit3.Name = "checkEdit3"
        Me.checkEdit3.Properties.Caption = "Nª Contentor"
        Me.checkEdit3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEdit3.Size = New System.Drawing.Size(99, 19)
        Me.checkEdit3.TabIndex = 3
        '
        'checkEdit2
        '
        Me.checkEdit2.Location = New System.Drawing.Point(6, 213)
        Me.checkEdit2.Name = "checkEdit2"
        Me.checkEdit2.Properties.Caption = "Nº BL"
        Me.checkEdit2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEdit2.Size = New System.Drawing.Size(99, 19)
        Me.checkEdit2.TabIndex = 2
        '
        'checkEdit1
        '
        Me.checkEdit1.Location = New System.Drawing.Point(6, 243)
        Me.checkEdit1.Name = "checkEdit1"
        Me.checkEdit1.Properties.Caption = "Observações"
        Me.checkEdit1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEdit1.Size = New System.Drawing.Size(99, 19)
        Me.checkEdit1.TabIndex = 1
        '
        'checkEditSituacao
        '
        Me.checkEditSituacao.Location = New System.Drawing.Point(6, 33)
        Me.checkEditSituacao.Name = "checkEditSituacao"
        Me.checkEditSituacao.Properties.Caption = "Situação"
        Me.checkEditSituacao.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.checkEditSituacao.Size = New System.Drawing.Size(99, 19)
        Me.checkEditSituacao.TabIndex = 0
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.groupControl2)
        Me.Controls.Add(Me.groupControl1)
        Me.Name = "Form2"
        Me.Size = New System.Drawing.Size(532, 470)
        Me.Text = "OverlayControl"
        CType(Me.groupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupControl1.ResumeLayout(False)
        Me.groupControl1.PerformLayout()
        CType(Me.textEditLinha.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditDescArt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditArtigo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupControl2.ResumeLayout(False)
        CType(Me.textEditNumCertificado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditSeguradora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditCompMaritima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditPorto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditPais.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditDestino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditNumCertificado.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditSeguradora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditPorto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditCompMaritima.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditPais.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.memoEditObservacoes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditNumBL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditNumContentor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateEditLimiteEmbarque.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateEditLimiteEmbarque.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateEditEmbarque.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateEditEmbarque.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditTipoQualidade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.textEditLoteForn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lookUpEditStiuacao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditLoteForn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditTipoQualidade.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditEmbarque.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEdit4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditSituacao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class