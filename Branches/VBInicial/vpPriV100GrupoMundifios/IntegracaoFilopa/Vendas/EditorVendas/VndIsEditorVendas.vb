Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports InvBE100

Public Class VndIsEditorVendas
    Inherits EditorVendas

    Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
        MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)


        '#############################################################################################################
        '############# JFC - 21/10/2019 - Copia da Lotes para a Mundifios.                                      ######
        '#############                   (Primeiro grava o lote na Filopa depois copia o lote para a Mundifios  ######
        '#############################################################################################################
        If (Me.DocumentoVenda.Tipodoc = "CNT" Or Me.DocumentoVenda.Tipodoc = "EMB") And Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor + "" <> "" And BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor.ToString()).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "" <> "" And BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_EntidadeInterna").Valor & "" <> "" Then


            Dim listForn As StdBELista
            listForn = BSO.Consulta("select top 1 Fornecedor  from PRIMUNDIFIOS.dbo.Fornecedores where CDU_EntidadeInterna='" & BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_EntidadeInterna").Valor & "' and FornecedorAnulado='0'")
            listForn.Inicio()

            For i = 1 To Me.DocumentoVenda.Linhas.NumItens

                If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "<L01>" And Me.DocumentoVenda.Linhas.GetEdita(i).Estado = "P" And Me.DocumentoVenda.Linhas.GetEdita(i).Fechado = False Then
                    'Cria lotes na Filopa
                    Dim ArtigoLote As New InvBEArtigoLote

                    If BSO.Inventario.ArtigosLotes.Existe(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo, Me.DocumentoVenda.Linhas.GetEdita(i).Lote) = False Then


                        ArtigoLote.Artigo = Me.DocumentoVenda.Linhas.GetEdita(i).Artigo
                        ArtigoLote.Lote = Me.DocumentoVenda.Linhas.GetEdita(i).Lote
                        ArtigoLote.Descricao = BSO.Contexto.CodEmp & ", " & BSO.Contexto.UtilizadorActual
                        ArtigoLote.DataFabrico = Now()
                        ArtigoLote.Validade = Now()
                        ArtigoLote.Activo = True
                        ArtigoLote.Observacoes = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_ObsLote").Valor
                        ArtigoLote.CamposUtil("CDU_TipoQualidade").Valor = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_TipoQualidade").Valor
                        ArtigoLote.CamposUtil("CDU_Parafinado").Valor = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Parafinado").Valor
                        ArtigoLote.CamposUtil("CDU_LoteForn").Valor = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_LoteFornecedor").Valor
                        If listForn.Vazia = False Then
                            ArtigoLote.CamposUtil("CDU_Fornecedor").Valor = listForn.Valor("Fornecedor")
                        End If
                        BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote)

                    Else

                        'Atualiza Lotes
                        Dim Campos As New StdBECampos
                        Campos = BSO.Inventario.ArtigosLotes.DaValorAtributos(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo, Me.DocumentoVenda.Linhas.GetEdita(i).Lote, "Observacoes", "CDU_TipoQualidade", "CDU_Parafinado", "CDU_LoteForn", "CDU_Fornecedor")
                        Campos("Observacoes") = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_ObsLote")
                        Campos("CDU_TipoQualidade") = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_TipoQualidade")
                        Campos("CDU_Parafinado") = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Parafinado")
                        Campos("CDU_LoteForn") = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_LoteFornecedor")
                        If listForn.Vazia = False Then
                            Campos("CDU_Fornecedor") = listForn.Valor("Fornecedor")
                        End If
                        BSO.Inventario.ArtigosLotes.ActualizaValorAtributos(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo, Me.DocumentoVenda.Linhas.GetEdita(i).Lote, Campos)

                    End If
                End If

            Next i

            'Cria lotes na Mundifios
            If AbreObjEmpresa("MUNDIFIOS") Then
                For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                    If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "<L01>" Then
                        If BSO.Base.Artigos.Existe(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo) = True And (BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao Like "Fio*" Or BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao Like "Rama*") Then
                            CopiaLote(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo, Me.DocumentoVenda.Linhas.GetEdita(i).Lote)
                        End If
                    End If
                Next i
                FechaObjEmpresa
            End If

            'JFC 14/07/2021
            'Cria lotes na Empresa Destino (Ignora a Mundifios porque já copiou em cima)
            If BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "" <> "Mundifios" And AbreObjEmpresa("" & BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "") Then
                For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                    If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "<L01>" Then
                        If BSO.Base.Artigos.Existe(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo) = True And (BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao Like "Fio*" Or BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao Like "Rama*") Then
                            CopiaLote(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo, Me.DocumentoVenda.Linhas.GetEdita(i).Lote)
                        End If

                    End If
                Next i
                FechaObjEmpresa
            End If

        End If

        '#############################################################################################################
        '############# JFC - 21/10/2019 - Copia da Lotes para a Mundifios.                                      ######
        '#############                   (Primeiro grava o lote na Filopa depois copia o lote para a Mundifios  ######
        '#############################################################################################################


        'Sempre que se grava um CNT/EMB ele altera a serie a copiar para a serie atual. JFC - 04/01/2021
        'Ao passar de ano, há sempre contratos e embarques a registar do ano transato, mas o desenvolvimento dava erro na data px: data 2020 e serie 2021 não compativeis.
        If Me.DocumentoVenda.Tipodoc = "CNT" Or Me.DocumentoVenda.Tipodoc = "EMB" Then
            ' BSO.Comercial.TabVendas.Edita(Me.DocumentoVenda.TipoDoc).CamposUtil("CDU_SerieComprasDestino") = Me.DocumentoVenda.Serie
            BSO.DSO.ExecuteSQL("update DocumentosVenda set CDU_SerieComprasDestino='" & Me.DocumentoVenda.Serie & "' where Documento='" & Me.DocumentoVenda.Tipodoc & "'")
        End If



        '#############################################################################################################################################
        '############# JFC - 25/10/2019 - Copiar campo CDU_DataPrevistaChegada para LinhasCompras.DataEntrega                                   ######
        '#############################################################################################################################################
        If (Me.DocumentoVenda.Tipodoc = "CNT" Or Me.DocumentoVenda.Tipodoc = "EMB") And Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor & "" <> "" And BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "" <> "" Then
            If AbreObjEmpresa(BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor) Then
                Dim DataChegada As Date
                Dim listDocDestino As StdBELista
                listDocDestino = BSO.Consulta("select cd.CDU_DocumentoCompraDestino from  CabecDoc cd where cd.Id='" & Me.DocumentoVenda.ID & "'")
                If listDocDestino.Vazia = False Then
                    listDocDestino.Inicio()

                    If listDocDestino.Valor("CDU_DocumentoCompraDestino") & "" <> "" Then

                        For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                            If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" Then
                                'DataChegada = ObjEmpresa.Comercial.Compras.Edita("000", "CNT", "2019X", 6).Linhas.Edita(i).DataEntrega
                                DataChegada = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_DataPrevistaChegada").Valor
                                'JFC 20/10/2020 - Copiar CDU_NVolumes para LinhasCompras.CDU_Volumes
                                BSO.DSO.ExecuteSQL("update LinhasCompras set DataEntrega='" & Format(DataChegada, "yyyy-MM-dd") & "', CDU_Volumes='" & Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_NVolumes").Valor & "'  where CDU_IDLinhaOriginalGrupo = '" & Me.DocumentoVenda.Linhas.GetEdita(i).IdLinha & "'")
                            End If
                        Next i

                        'JFC 28/11/2019 Copiar as faturas para a ECF NumDocExterno.
                        If Me.DocumentoVenda.Tipodoc = "EMB" Then
                            Dim listNFatura As StdBELista
                            Dim NFatura As String
                            NFatura = ""
                            listNFatura = BSO.Consulta("select distinct ln.CDU_NFatura from LinhasDoc ln inner join Artigo a on a.Artigo=ln.Artigo where ln.IdCabecDoc='" & Me.DocumentoVenda.ID & "'")
                            listNFatura.Inicio()

                            For i = 1 To listNFatura.NumLinhas
                                NFatura = NFatura & listNFatura.Valor("CDU_NFatura") & "-"
                                listNFatura.Seguinte()
                            Next i
                            NFatura = Mid(NFatura, 1, Len(NFatura) - 1)
                            If Len(NFatura) > 18 Then
                                MsgBox("Atenção! A conctenação das Faturas de fornecedor ultrapassa os 18 caracteres permitidos: " & Len(NFatura) & " - " & NFatura & Chr(13) & "O resultado a integrar será: " & Mid(NFatura, 1, 18), vbInformation)
                                NFatura = Mid(NFatura, 1, 18)
                            End If

                            'JFC 04/09/2019 - Identificar a posição da barra e do espaço (TipoDoc Serie/NumDoc)
                            Dim PosBarra As Long
                            Dim PosEsp As Long
                            Dim TipoDocFinal As String
                            Dim SerieFinal As String
                            Dim NumDocFinal As String

                            PosBarra = InStr(1, listDocDestino.Valor("CDU_DocumentoCompraDestino"), "/", vbTextCompare)
                            PosEsp = InStr(1, listDocDestino.Valor("CDU_DocumentoCompraDestino"), " ", vbTextCompare)

                            TipoDocFinal = Left(listDocDestino.Valor("CDU_DocumentoCompraDestino"), PosEsp - 1)
                            SerieFinal = Mid(listDocDestino.Valor("CDU_DocumentoCompraDestino"), PosEsp + 1, PosBarra - PosEsp - 1)
                            NumDocFinal = Mid(listDocDestino.Valor("CDU_DocumentoCompraDestino"), PosBarra + 1)


                            BSO.DSO.ExecuteSQL("update cc set cc.NumDocExterno='" & NFatura & "' from pri" & BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & ".dbo.CabecCompras cc where cc.TipoDoc='" & TipoDocFinal & "' and cc.NumDoc='" & NumDocFinal & "' and cc.Serie='" & SerieFinal & "'")
                        End If

                    End If
                End If
                FechaObjEmpresa
            End If
        End If
        '#############################################################################################################################################
        '############# JFC - 25/10/2019 - Copiar campo CDU_DataPrevistaChegada para LinhasCompras.DataEntrega                                   ######
        '#############################################################################################################################################

    End Sub


    Public Function CopiaLote(ByVal str_Artigo As String, ByVal str_Lote As String)



        If str_Lote = "" Then Exit Function

        If BSO.Inventario.ArtigosLotes.Existe(str_Artigo, str_Lote) = False Then

            Dim ArtigoLote As New InvBEArtigoLote

            Dim stdBE_ListaLote As StdBELista
            stdBE_ListaLote = BSO.Consulta(" SELECT * FROM ArtigoLote " &
                                                     " WHERE Artigo = '" & str_Artigo & "' " &
                                                     " AND Lote = '" & str_Lote & "'")

            If Not stdBE_ListaLote.Vazia Then

                stdBE_ListaLote.Inicio()

                ArtigoLote.Artigo = stdBE_ListaLote.Valor("Artigo")
                ArtigoLote.Lote = stdBE_ListaLote.Valor("Lote")
                ArtigoLote.Descricao = BSO.Contexto.CodEmp & ", " & BSO.Contexto.UtilizadorActual
                If Len(stdBE_ListaLote.Valor("DataFabrico")) > 0 Then ArtigoLote.DataFabrico = stdBE_ListaLote.Valor("DataFabrico")
                If Len(stdBE_ListaLote.Valor("Validade")) > 0 Then ArtigoLote.Validade = stdBE_ListaLote.Valor("Validade")
                ArtigoLote.Controlador = stdBE_ListaLote.Valor("Controlador")
                ArtigoLote.Activo = stdBE_ListaLote.Valor("Activo")
                ArtigoLote.Observacoes = stdBE_ListaLote.Valor("Observacoes")
                ArtigoLote.CamposUtil("CDU_TipoQualidade").Valor = stdBE_ListaLote.Valor("CDU_TipoQualidade")
                ArtigoLote.CamposUtil("CDU_Parafinado").Valor = stdBE_ListaLote.Valor("CDU_Parafinado")
                ArtigoLote.CamposUtil("CDU_LoteForn").Valor = stdBE_ListaLote.Valor("CDU_LoteForn")
                ArtigoLote.CamposUtil("CDU_Fornecedor").Valor = stdBE_ListaLote.Valor("CDU_Fornecedor")
                BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote)


            End If
        Else

            'Atualiza Lotes
            Dim Campos As New StdBECampos
            Campos = BSO.Inventario.ArtigosLotes.DaValorAtributos(str_Artigo, str_Lote, "Observacoes", "CDU_TipoQualidade", "CDU_Parafinado", "CDU_LoteForn", "CDU_Fornecedor")
            BSO.Inventario.ArtigosLotes.ActualizaValorAtributos(str_Artigo, str_Lote, Campos)

        End If
    End Function

    '####################################################################################################
    '######## JFC 21/10/2019 Funções adicionadas para copia de lotes para a Mundifios           #########
    '####################################################################################################


    Public Overrides Sub DepoisDeTransformar(e As ExtensibilityEventArgs)
        MyBase.DepoisDeTransformar(e)

        '###################################################################
        '###### Coloca a descrição original do produto Bruno - 21/10/2019 ##
        '###################################################################
        Dim i As Long
        If Me.DocumentoVenda.Tipodoc = "CNT" And BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "" <> "" Then

            For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" Then
                    'JFC 23/10/2019 Apos reunião com a Filopa foi pedido para manter guardada a descrição original da NEC
                    Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_LinhaDescricaoNEC").Valor = Me.DocumentoVenda.Linhas.GetEdita(i).Descricao
                    Me.DocumentoVenda.Linhas.GetEdita(i).Descricao = BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao & " " & BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(i).Artigo).CamposUtil("CDU_Descricaoextra").Valor
                End If
            Next i

        End If


        '###################################################################
        '###### Coloca a descrição original do produto Bruno - 21/10/2019 ##
        '###################################################################


        '###############################################################################################
        '###### Peso Liquido = Quantidade e Situação = Embarque (Pedido Ana Castro JFC - 23/10/2019   ##
        '###############################################################################################

        If (Me.DocumentoVenda.Tipodoc = "EMB" Or Me.DocumentoVenda.Tipodoc = "CNT") And BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "" <> "" Then

            For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" Then
                    If Me.DocumentoVenda.Tipodoc = "EMB" Then
                        Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_PesoLiquido").Valor = Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade
                        Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Situacao").Valor = "004"
                        Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_LoteFornecedor").Valor = "0"
                        Me.DocumentoVenda.CamposUtil("CDU_NBL").Valor = "0"
                    Else
                        Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Situacao").Valor = "001"
                    End If
                End If
            Next i
        End If

        '###############################################################################################
        '###### Peso Liquido = Quantidade e Situação = Embarque (Pedido Ana Castro JFC - 23/10/2019   ##
        '###############################################################################################

    End Sub

    Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
        MyBase.TeclaPressionada(KeyCode, Shift, e)

        '############################################################################################
        '####              JFC 21/10/2019 Sugestão de Lotes                            ##############
        '############################################################################################


        'Alt+F - Sugere Lotes
        If KeyCode = 70 And (Me.DocumentoVenda.Tipodoc = "CNT" Or Me.DocumentoVenda.Tipodoc = "EMB") Then

            If Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor & "" = "" Then
                MsgBox("Atenção: Cliente não está preenchido, não é possivel sugerir lotes", vbInformation, "Artigo/Lote")
                Exit Sub
            End If

            If BSO.Base.Fornecedores.Edita(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor).CamposUtil("CDU_NomeEmpresaGrupo").Valor & "" = "" Then
                MsgBox("Atenção: Cliente não faz parte do Grupo Mundifios (CDU_NomeEmpresaGrupo), não é possivel sugerir lotes", vbInformation, "Artigo/Lote")
                Exit Sub
            End If

            'Primeira validação - Campo EntidadeInterna na ficha do Fornecedor
            If BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_EntidadeInterna").Valor & "" = "" Then
                MsgBox("Atenção: Fornecedor sem EntidadeInterna", vbInformation, "Artigo/Lote")
                Exit Sub
            Else

                Dim ent As String
                Dim listForn As StdBELista
                listForn = BSO.Consulta("select top 1 Fornecedor  from PRIMUNDIFIOS.dbo.Fornecedores where CDU_EntidadeInterna='" & BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_EntidadeInterna").Valor & "' and FornecedorAnulado='0'")
                listForn.Inicio()

                'Segunda validação - Se o Fornecedor existe na Mundifios

                If listForn.Vazia Then
                    'Me.DocumentoCompra.Linhas(NumLinha).lote = "0000"
                    MsgBox("Atenção: Fornecedor inexistente na Mundifios", vbInformation, "Artigo/Lote")
                    Exit Sub
                Else
                    'Se as duas validações acima estiverem ok, então guarda a Entidade do Fornecedor na Mundifios
                    ent = listForn.Valor("Fornecedor")
                End If

            End If

            'Segunda parte, atibuição do lote
            Dim fj As Long
            Dim lote As Integer
            Dim loteAux As Integer
            Dim listLote As StdBELista



            For fj = 1 To Me.DocumentoVenda.Linhas.NumItens

                If Me.DocumentoVenda.Linhas.GetEdita(fj).Lote & "" = "" Or Me.DocumentoVenda.Linhas.GetEdita(fj).Lote & "" = "<L01>" And Me.DocumentoVenda.Linhas.GetEdita(fj).Artigo & "" <> "" Then

                    'Consulta qual o proximo lote a ser utilizado. Função dbo.fnProximoLote
                    listLote = BSO.Consulta("select PRIMUNDIFIOS.dbo.fnProximoLote('" & BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_EntidadeInterna").Valor & "','" & Me.DocumentoVenda.Linhas.GetEdita(fj).Artigo & "') as 'Lote'")
                    loteAux = 0
                    listLote.Inicio()

                    'Primeira validação, ver se já existe nas outras linhas algum lote inserido e guarda o valor do maior lote.
                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens

                        If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo = Me.DocumentoVenda.Linhas.GetEdita(fj).Artigo And i <> fj And Len(Me.DocumentoVenda.Linhas.GetEdita(i).Lote) = 8 Then

                            lote = CInt(Right(Me.DocumentoVenda.Linhas.GetEdita(i).Lote, 4))
                            If lote > loteAux Then
                                loteAux = lote
                            End If

                        End If
                    Next i

                    'Se já existir o lote noutra linha, então o novo lote deverá ser = lote + 1
                    If loteAux <> 0 Then
                        loteAux = loteAux + 1

                        'Conjunto de validações para garantir que o lote+1 é superior ou igual ao lote sugerido pela função dbo.fnProximoLote
                        If listLote.Vazia Or listLote.Valor("Lote") = "" Then
                            i = 4 - Len(CStr(loteAux))
                            Me.DocumentoVenda.Linhas.GetEdita(fj).Lote = "" & ent & Left("0000", i) & loteAux
                        Else
                            If loteAux <= CInt(Right(listLote.Valor("Lote"), 4)) Then
                                Me.DocumentoVenda.Linhas.GetEdita(fj).Lote = listLote.Valor("Lote")
                            Else
                                i = 4 - Len(CStr(loteAux))
                                Me.DocumentoVenda.Linhas.GetEdita(fj).Lote = "" & ent & Left("0000", i) & loteAux
                            End If
                        End If

                        'Caso não exista nenhum lote inserido nas outras linhas, então sugere o lote devolvido pela função dbo.fnProximoLote
                    Else

                        'Se não existir nenhum lote, então é o primeiro lote
                        If listLote.Vazia Or listLote.Valor("Lote") = "" Then
                            Me.DocumentoVenda.Linhas.GetEdita(fj).Lote = "" & ent & "0001"
                        Else
                            Me.DocumentoVenda.Linhas.GetEdita(fj).Lote = listLote.Valor("Lote")
                        End If
                    End If
                End If
            Next fj
        End If

        '############################################################################################
        '####              JFC 21/10/2019 Sugestão de Lotes                            ##############
        '############################################################################################


        'Ctrl+F1 - Preenche dados dos campos de utilizador
        If KeyCode = 112 Then

            '        Verifica se é uma linha que não existe na tabela linhascompras
            If Me.LinhaActual = -1 Then
                Exit Sub
            End If

            'Verifica se é uma linha de texto, sem artigo
            If Me.DocumentoVenda.Linhas(Me.LinhaActual).Artigo & "" = "" Then
                Exit Sub
            End If

            ArtigoEnc = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Artigo
            DescArtEnc = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Descricao
            LoteEnc = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Lote
            LinhaEnc = Me.LinhaActual

            Load FrmOutrosDados
        FrmOutrosDados.Show

        End If



    End Sub

End Class