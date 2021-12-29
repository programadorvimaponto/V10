Imports Vimaponto.PrimaveraV100
Imports BasBE100.BasBETiposGcp
Imports StdBE100
Imports System.Windows.Forms

Public Class Mdi_GeraDocumentoVenda

    Shared IDLinhaDocOriginal(999) As String
    Public Shared DocumentoNovo_Venda As New VndBE100.VndBEDocumentoVenda 'encomenda a Cliente
    Private Const TipoEmpresa As Integer = 0

    Private Shared Sub FechaEmpresa()
        PriV100Api.BSO.FechaEmpresaTrabalho()
    End Sub
    Private Shared Function AbreEmpresa(ByVal Empresa As String) As Boolean

        On Error GoTo TrataErro
        PriV100Api.BSO.AbreEmpresaTrabalho(TipoEmpresa, Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual)
        AbreEmpresa = True
        Exit Function

TrataErro:
        AbreEmpresa = False
    End Function


    Public Shared Function GerarDocumento_BaseVendas(ByVal DocumentoBase As VndBE100.VndBEDocumentoVenda,
                                            ByVal BaseDadosDestino As String,
                                            ByVal TipoDoc_Destino As String,
                                            ByVal Serie_Destino As String,
                                            ByVal Entidade_Destino As String,
                                            ByVal Armazem_Destino As String,
                                            ByVal ValorASomarArtigo As Double) As Boolean

        On Error GoTo TrataErro
        'Identifica o Documento acabado de Criar (Encomenda a Cliente)
        'IdentificarDocumento Filial_Origem, Serie_Origem, TipoDoc_Origem, NumDoc_Origem

        'Gera Documento de Venda (encomenda a Cliente) à PERCATO
        If Not AbreEmpresa(BaseDadosDestino) Then GerarDocumento_BaseVendas = False : Exit Function

        Dim DocumentoNovo_Venda = New VndBE100.VndBEDocumentoVenda

        DocumentoNovo_Venda.Filial = "000"

        'Caso a empresa de destino seja a Mixyarn, a serie é a 2020Z pois a 2020X estava a ser utilizada como Emissivel. JFC - 02/06/2020
        'If DocumentoBase.Entidade = "2492" And DocumentoBase.Tipodoc = "ECL" Then
        'DocumentoNovo_Venda.Serie = "2020Z"
        'Else
        DocumentoNovo_Venda.Serie = Serie_Destino
        'End If

        'DocCompra consta nos Campos de utilizador
        DocumentoNovo_Venda.Tipodoc = TipoDoc_Destino
        'Fornecedor
        DocumentoNovo_Venda.TipoEntidade = "C"
        'CodFornecedor consta nos Campos de utilizador
        DocumentoNovo_Venda.Entidade = Entidade_Destino
        '-----> Falta passar o documento original
        'DocumentoCompra.doc = DocumentoBase.Serie & "/" & DocumentoBase.NumDoc

        DocumentoNovo_Venda.Referencia = DocumentoBase.Referencia
        DocumentoNovo_Venda.LocalCarga = DocumentoBase.LocalCarga
        DocumentoNovo_Venda.LocalDescarga = DocumentoBase.LocalDescarga
        DocumentoNovo_Venda.CamposUtil("CDU_DocumentoOrigem").Valor = DocumentoBase.Tipodoc & " " & DocumentoBase.Serie & "/" & DocumentoBase.NumDoc

        'Condições, comentado por JFC 28/03/2019 - As condições devem ser sugeridas pela ficha de cliente.
        '    DocumentoNovo_Venda.ModoPag = DocumentoBase.ModoPag
        '    DocumentoNovo_Venda.CondPag = DocumentoBase.CondPag
        '    DocumentoNovo_Venda.moeda = DocumentoBase.moeda
        '    DocumentoNovo_Venda.ContaDomiciliacao = DocumentoBase.ContaDomiciliacao
        '    DocumentoNovo_Venda.Responsavel = DocumentoBase.Responsavel



        PriV100Api.BSO.Vendas.Documentos.PreencheDadosRelacionados(DocumentoNovo_Venda, 5) 'GcpBE800.PreencheRelacaoVendas.vdDadosTodos


        'JFC 02/10/2019 - Após consulta com M.Goreti a Moeda deve ser copiada da ECL original. Atualiza depois dos Dados Relacionados.
        DocumentoNovo_Venda.Moeda = DocumentoBase.Moeda

        'Carga
        If DocumentoBase.Entidade = "1207" Then
            DocumentoNovo_Venda.CargaDescarga.MoradaCarga = "Rua Comendador Manuel Gonçalves nº 25"
            DocumentoNovo_Venda.CargaDescarga.Morada2Carga = ""
            DocumentoNovo_Venda.CargaDescarga.LocalidadeCarga = "S Cosme do Vale"
            DocumentoNovo_Venda.CargaDescarga.DistritoCarga = "03"
            DocumentoNovo_Venda.CargaDescarga.PaisCarga = "PT"
            DocumentoNovo_Venda.CargaDescarga.CodPostalCarga = "4770-583"
            DocumentoNovo_Venda.CargaDescarga.CodPostalLocalidadeCarga = "V N Famalicão"
        Else
            DocumentoNovo_Venda.CargaDescarga.MoradaCarga = DocumentoBase.CargaDescarga.MoradaCarga
            DocumentoNovo_Venda.CargaDescarga.Morada2Carga = DocumentoBase.CargaDescarga.Morada2Carga
            DocumentoNovo_Venda.CargaDescarga.LocalidadeCarga = DocumentoBase.CargaDescarga.LocalidadeCarga
            DocumentoNovo_Venda.CargaDescarga.DistritoCarga = DocumentoBase.CargaDescarga.DistritoCarga
            DocumentoNovo_Venda.CargaDescarga.PaisCarga = DocumentoBase.CargaDescarga.PaisCarga
            DocumentoNovo_Venda.CargaDescarga.CodPostalCarga = DocumentoBase.CargaDescarga.CodPostalCarga
            DocumentoNovo_Venda.CargaDescarga.CodPostalLocalidadeCarga = DocumentoBase.CargaDescarga.CodPostalLocalidadeCarga
        End If
        'Descarga
        DocumentoNovo_Venda.CargaDescarga.MoradaEntrega = DocumentoBase.CargaDescarga.MoradaEntrega
        DocumentoNovo_Venda.CargaDescarga.Morada2Entrega = DocumentoBase.CargaDescarga.Morada2Entrega
        DocumentoNovo_Venda.CargaDescarga.LocalidadeEntrega = DocumentoBase.CargaDescarga.LocalidadeEntrega
        DocumentoNovo_Venda.CargaDescarga.DistritoEntrega = DocumentoBase.CargaDescarga.DistritoEntrega
        'DocumentoNovo_Venda.CargaDescarga.PaisEntrega = DocumentoBase.CargaDescarga.PaisEntrega
        DocumentoNovo_Venda.CargaDescarga.CodPostalEntrega = DocumentoBase.CargaDescarga.CodPostalEntrega
        DocumentoNovo_Venda.CargaDescarga.CodPostalLocalidadeEntrega = DocumentoBase.CargaDescarga.CodPostalLocalidadeEntrega


        '20170921
        If TipoDoc_Destino = "GR" And DocumentoBase.Tipodoc = "GR" Then
            Dim TipoDocOrigem As String
            Dim SerieOrigem As String
            Dim NumDocOrigem As Long

            'JFC 03/09/2019
            Dim TipoDocFinal As String
            Dim SerieFinal As String
            Dim NumDocFinal As Long


            Dim Str_MoradaDescarga As String
            Dim Lst_MoradaDescarga As StdBELista

            'Identifica qual a ECL inicial
            Str_MoradaDescarga = "SELECT top 1 CD2.TipoDoc as TipoDocEncomenda,  " &
                                "       CD2.NumDoc as NumDocEncomenda, " &
                                "       CD2.serie as SerieEncomenda " &
                                " FROM CabecDoc  CD " &
                                " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60 " &
                                " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " &
                                " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " &
                                " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " &
                                " WHERE  CD.Filial = '000' AND CD.TipoDoc = '" & DocumentoBase.Tipodoc & "' AND CD.serie = '" & DocumentoBase.Serie & "' AND CD.NumDoc = " & DocumentoBase.NumDoc & " " &
                                " ORDER BY LD.NumLinha"

            Lst_MoradaDescarga = PriV100Api.BSO.Consulta(Str_MoradaDescarga)

            Lst_MoradaDescarga.Inicio()

            Do While Not Lst_MoradaDescarga.NoFim
                Application.DoEvents()

                TipoDocOrigem = Lst_MoradaDescarga.Valor("TipoDocEncomenda")
                SerieOrigem = Lst_MoradaDescarga.Valor("SerieEncomenda")
                NumDocOrigem = Lst_MoradaDescarga.Valor("NumDocEncomenda")

                Lst_MoradaDescarga.Seguinte()

            Loop

            'Acrescentado por JFC a 03/09/2019 - Identifica a ECL final (para facilitar utilizo a mesma lógica de cima e reutilizo a lista)
            '        Str_MoradaDescarga = "select left(cd.CDU_DocumentoVendaDestino,3) as 'TipoDoc', substring(cd.CDU_DocumentoVendaDestino,5,5) as 'Serie', SUBSTRING(cd.CDU_DocumentoVendaDestino,11, len(cd.CDU_DocumentoVendaDestino)-10) as 'NumDoc' " & _
            '                             "from CabecDoc cd " & _
            '                             "where cd.TipoDoc='" & TipoDocOrigem & "' and cd.NumDoc='" & NumDocOrigem & "' and cd.Serie='" & SerieOrigem & "'"
            '
            Str_MoradaDescarga = "select cd.CDU_DocumentoVendaDestino as 'Str' " &
                                 "from CabecDoc cd " &
                                 "where cd.TipoDoc='" & TipoDocOrigem & "' and cd.NumDoc='" & NumDocOrigem & "' and cd.Serie='" & SerieOrigem & "'"



            Lst_MoradaDescarga = PriV100Api.BSO.Consulta(Str_MoradaDescarga)

            Lst_MoradaDescarga.Inicio()

            Do While Not Lst_MoradaDescarga.NoFim
                Application.DoEvents()

                Dim PosBarra As Long
                Dim PosEsp As Long

                'JFC 04/09/2019 - Identificar a posição da barra e do espaço (TipoDoc Serie/NumDoc)
                PosBarra = InStr(1, Lst_MoradaDescarga.Valor("Str"), "/", vbTextCompare)
                PosEsp = InStr(1, Lst_MoradaDescarga.Valor("Str"), " ", vbTextCompare)

                TipoDocFinal = Left(Lst_MoradaDescarga.Valor("Str"), PosEsp - 1)
                SerieFinal = Mid(Lst_MoradaDescarga.Valor("Str"), PosEsp + 1, PosBarra - PosEsp - 1)
                NumDocFinal = Mid(Lst_MoradaDescarga.Valor("Str"), PosBarra + 1)

                Lst_MoradaDescarga.Seguinte()

            Loop



            Dim DocumentoBase_Encomenda As New VndBE100.VndBEDocumentoVenda

            'Comentado por JFC a 03/09/2019 - Este objeto identificava a ECL inicial.
            'Set DocumentoBase_Encomenda = BSO.Comercial.Vendas.Edita("000", TipoDocOrigem, SerieOrigem, NumDocOrigem)

            'Por JFC a 03/09/2019 - ECL Final
            DocumentoBase_Encomenda = PriV100Api.BSO.Vendas.Documentos.Edita("000", TipoDocFinal, SerieFinal, NumDocFinal)

            'Atualizo a morada de descarga
            'Descarga
            DocumentoNovo_Venda.CargaDescarga.MoradaEntrega = IIf(Len(DocumentoBase_Encomenda.CargaDescarga.MoradaEntrega) > 0, DocumentoBase_Encomenda.CargaDescarga.MoradaEntrega, " . ")
            DocumentoNovo_Venda.CargaDescarga.Morada2Entrega = DocumentoBase_Encomenda.CargaDescarga.Morada2Entrega
            DocumentoNovo_Venda.CargaDescarga.LocalidadeEntrega = IIf(Len(DocumentoBase_Encomenda.CargaDescarga.LocalidadeEntrega) > 0, DocumentoBase_Encomenda.CargaDescarga.LocalidadeEntrega, " . ")
            DocumentoNovo_Venda.CargaDescarga.DistritoEntrega = DocumentoBase_Encomenda.CargaDescarga.DistritoEntrega
            DocumentoNovo_Venda.CargaDescarga.PaisEntrega = DocumentoBase_Encomenda.CargaDescarga.PaisEntrega
            DocumentoNovo_Venda.CargaDescarga.CodPostalEntrega = IIf(Len(DocumentoBase_Encomenda.CargaDescarga.CodPostalEntrega) > 0, DocumentoBase_Encomenda.CargaDescarga.CodPostalEntrega, "0000-000")
            DocumentoNovo_Venda.CargaDescarga.CodPostalLocalidadeEntrega = IIf(Len(DocumentoBase_Encomenda.CargaDescarga.CodPostalLocalidadeEntrega) > 0, DocumentoBase_Encomenda.CargaDescarga.CodPostalLocalidadeEntrega, " . ")

            DocumentoNovo_Venda.ModoExp = DocumentoBase.ModoExp
            DocumentoNovo_Venda.Matricula = DocumentoBase.Matricula
            DocumentoNovo_Venda.CamposUtil("CDU_NumCarga") = DocumentoBase.CamposUtil("CDU_NumCarga")
            DocumentoNovo_Venda.LocalDescarga = IIf(Len(DocumentoBase.LocalDescarga) > 0, DocumentoBase.LocalDescarga, " . ")


            'Condições adicionadas por JFC 28/03/2019 - As mesmas devem ser lidas da ECL final.
            DocumentoNovo_Venda.ModoPag = DocumentoBase_Encomenda.ModoPag
            DocumentoNovo_Venda.CondPag = DocumentoBase_Encomenda.CondPag
            DocumentoNovo_Venda.Moeda = DocumentoBase_Encomenda.Moeda
            DocumentoNovo_Venda.ContaDomiciliacao = DocumentoBase_Encomenda.ContaDomiciliacao
            DocumentoNovo_Venda.Responsavel = DocumentoBase_Encomenda.Responsavel




            'Ir à encomenda, identificar a morada alternativa colocada no F3 e colocar no documento GR ao cliente Final
            If DocumentoBase_Encomenda.MoradaAlternativaEntrega & "" <> "" Then
                DocumentoNovo_Venda.MoradaAlternativaEntrega = DocumentoBase_Encomenda.MoradaAlternativaEntrega
                DocumentoNovo_Venda.UtilizaMoradaAlternativaEntreg = True
            End If

        Else

            'Como não é GR, provavelmetne ECL, vou ao documento que deu origem a este e verifico a morada alternativa colocada no F3 e coloco-a no documento ECL ao cliente final
            If DocumentoBase.CamposUtil("CDU_MoradaAlternativa").Valor & "" <> "" Then
                DocumentoNovo_Venda.MoradaAlternativaEntrega = DocumentoBase.CamposUtil("CDU_MoradaAlternativa").Valor & ""
                '#13/11/2020 - JFC Pais de descarga não estava assumir correctamente nas Encomendas (principalmente quando o clientes era estrangeiro e o local de descarga era PT)
                DocumentoNovo_Venda.CargaDescarga.PaisEntrega = PriV100Api.BSO.Base.MoradasAlternativas.DaValorAtributo("C", DocumentoNovo_Venda.Entidade, DocumentoBase.CamposUtil("CDU_MoradaAlternativa").Valor, "Pais")
                DocumentoNovo_Venda.CargaDescarga.MoradaEntrega = PriV100Api.BSO.Base.MoradasAlternativas.Edita("C", DocumentoNovo_Venda.Entidade, DocumentoBase.CamposUtil("CDU_MoradaAlternativa").Valor).Morada

                DocumentoNovo_Venda.UtilizaMoradaAlternativaEntreg = True
            End If

        End If

        'Tem de estar depois dos dados relazionados!! porque senao sugere a data do sistema e altera a data do documento
        DocumentoNovo_Venda.DataDoc = DocumentoBase.DataDoc
        'se não colocar isto, nao consigo gravar documentos de uma série diferente à serie actual, praticada na data do sistema
        'DocumentoCompra.DataIntroducao = DocumentoCompra.DataDoc




        Dim i As Long
        Dim j As Long

        Dim Artigo As String
        Dim Quantidade As Double
        Dim Armazem As String
        Dim Localizacao As String


        '20170921
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '@1@ @2@/N.º@3@ de @4@
        PriV100Api.BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoNovo_Venda, compTipoLinhaEspecial.compLinha_Comentario, Descricao:="@1@ @2@/N.º@3@ de @4@")
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'j = 1
        j = 2



        For i = 1 To DocumentoBase.Linhas.NumItens
            'Adicionar a linha ao documento

            If DocumentoBase.Linhas.GetEdita(i).TipoLinha = 60 Then


                If InStr(1, DocumentoBase.Linhas.GetEdita(i).Descricao, "/N.º") = 0 Then

                    PriV100Api.BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoNovo_Venda, compTipoLinhaEspecial.compLinha_Comentario, Descricao:=DocumentoBase.Linhas.GetEdita(i).Descricao)
                    IDLinhaDocOriginal(j) = DocumentoBase.Linhas.GetEdita(i).IdLinha & ";" & DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha
                    j = j + 1

                End If

            Else

                Artigo = DocumentoBase.Linhas.GetEdita(i).Artigo
                If Not PriV100Api.BSO.Base.Artigos.Existe(Artigo) Then
                    MsgBox("O Artigo " & Artigo & " não existe na Empresa " & BaseDadosDestino, vbCritical, "Artigo não existente")
                    GerarDocumento_BaseVendas = False
                    Exit Function
                End If

                VerificaLote(Artigo, DocumentoBase.Linhas.GetEdita(i).Lote)

                Quantidade = DocumentoBase.Linhas.GetEdita(i).Quantidade

                'Se o armazem dos parametros preenchido, sua esse. Caso contrário, usa o das linhas
                Armazem = IIf(Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Armazem)
                Localizacao = IIf(Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Localizacao)

                'Armazem = DocumentoBase.Linhas(i).Armazem
                'Localizacao = DocumentoBase.Linhas(i).Localizacao

                PriV100Api.BSO.Vendas.Documentos.AdicionaLinha(DocumentoNovo_Venda, Artigo, Quantidade, Armazem, Localizacao)

                DocumentoNovo_Venda.Linhas.GetEdita(j).Descricao = DocumentoBase.Linhas.GetEdita(i).Descricao
                DocumentoNovo_Venda.Linhas.GetEdita(j).Lote = DocumentoBase.Linhas.GetEdita(i).Lote
                DocumentoNovo_Venda.Linhas.GetEdita(j).Unidade = DocumentoBase.Linhas.GetEdita(i).Unidade
                'DocumentoNovo_Venda.Linhas(j).Armazem = DocumentoBase.Linhas(i).Armazem
                'DocumentoNovo_Venda.Linhas(j).Localizacao = DocumentoBase.Linhas(i).Localizacao

                'Deixar a gestão do iva ao encargo do Primavera
                'DocumentoNovo_Venda.Linhas(j).CodIva = DocumentoBase.Linhas(i).CodIva
                'DocumentoNovo_Venda.Linhas(j).TaxaIva = DocumentoBase.Linhas(i).TaxaIva

                DocumentoNovo_Venda.Linhas.GetEdita(j).PrecUnit = DocumentoBase.Linhas.GetEdita(i).PrecUnit + ValorASomarArtigo
                'DocumentoNovo_Venda.Linhas(j).DescontoComercial = DocumentoBase.Linhas(i).DescontoComercial
                'DocumentoNovo_Venda.Linhas(j).Desconto1 = DocumentoBase.Linhas(i).Desconto1
                'DocumentoNovo_Venda.Linhas(j).Desconto2 = DocumentoBase.Linhas(i).Desconto2
                'DocumentoNovo_Venda.Linhas(j).Desconto3 = DocumentoBase.Linhas(i).Desconto3
                DocumentoNovo_Venda.Linhas.GetEdita(j).MovStock = DocumentoBase.Linhas.GetEdita(i).MovStock
                DocumentoNovo_Venda.Linhas.GetEdita(j).DataStock = DocumentoBase.Linhas.GetEdita(i).DataStock
                DocumentoNovo_Venda.Linhas.GetEdita(j).DataEntrega = DocumentoBase.Linhas.GetEdita(i).DataEntrega

                'JFC 06/11/2017 Preço Base para analise de comições mercado externo
                DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil("CDU_PrecoBase").Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil("CDU_PrecoBase").Valor

                'JFC 07/11/2017 Não estava a sair nas Guias o Tipo Qualidade. Acrescentei esta linha.
                DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil("CDU_TipoQualidade").Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil("CDU_TipoQualidade").Valor & ""

                'JFC 28/02/2019 CDU_Cambio nas Encomendas a Cliente.
                DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil("CDU_Cambio").Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil("CDU_Cambio").Valor

                'JFC 11/09/2020 CDU_PrecTab nas Encomendas a Cliente.
                DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil("CDU_PrecTab").Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil("CDU_PrecTab").Valor


                'Email JC sex 12/05/2017 09:38
                If UCase(BaseDadosDestino) = "INOVAFIL" Then
                    DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil("CDU_DataEntregaCliente").Valor = DocumentoBase.Linhas.GetEdita(i).DataEntrega
                End If

                '20170921
                If TipoDoc_Destino = "GR" And DocumentoBase.Tipodoc = "GR" Then
                    DocumentoNovo_Venda.Linhas.GetEdita(j).CamposUtil("CDU_Observacoes").Valor = DocumentoBase.Linhas.GetEdita(i).CamposUtil("CDU_Observacoes").Valor & ""
                End If

                'Para garantir a rastreabilidade
                'DocumentoNovo_Venda.Linhas(j).CamposUtil("CDU_IDLinhaOriginalPercato").Valor = DocumentoCompra.Linhas(j).IDLinha
                IDLinhaDocOriginal(j) = DocumentoBase.Linhas.GetEdita(i).IdLinha & ";" & DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha
                j = j + 1
            End If
        Next

        PriV100Api.BSO.Vendas.Documentos.Actualiza(DocumentoNovo_Venda)


        'Valida prazo de pagamento para alerta. JFC 27/04/2020
        If DocumentoNovo_Venda.Tipodoc = "GR" And DateDiff("d", DocumentoNovo_Venda.DataDoc, DocumentoNovo_Venda.DataVenc) < 9 Then
            Dim VarCliente As String
            Dim VarFrom As String
            Dim VarTo As String
            Dim VarAssunto As String
            Dim VarTextoInicialMsg As String
            Dim VarMensagem As String
            Dim VarUtilizador As String


            VarCliente = DocumentoNovo_Venda.Entidade
            VarFrom = ""

            VarTo = "informatica@mundifios.pt; tesouraria.clientes@mundifios.pt; faturacao@mundifios.pt"

            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                VarTextoInicialMsg = "Bom dia,"
            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                VarTextoInicialMsg = "Boa tarde,"
            Else
                VarTextoInicialMsg = "Boa noite,"
            End If
            AbreEmpresa(BaseDadosDestino)
            VarAssunto = "Emitir Fatura: " & DocumentoNovo_Venda.Tipodoc & " " & Format(DocumentoNovo_Venda.NumDoc, "####") & "/" & DocumentoNovo_Venda.Serie & " (" & Replace(PriV100Api.BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & ")"

            VarUtilizador = PriV100Api.APL.Utilizador.Utilizador




            VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido uma Guia com prazo de pagamento inferior ou igual a 8 dias, por favor emita a respetiva fatura:" & Chr(13) & Chr(13) & "" _
                        & "Empresa:                         " & BaseDadosDestino & Chr(13) & "" _
                        & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
                        & "Cliente:                         " & VarCliente & " - " & Replace(PriV100Api.BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & Chr(13) & "" _
                        & "Documento:                       " & DocumentoNovo_Venda.Tipodoc & " " & Format(DocumentoNovo_Venda.NumDoc, "#,###") & "/" & DocumentoNovo_Venda.Serie & Chr(13) & Chr(13) & "" _
                        & "Data Vencimento:                 " & DocumentoNovo_Venda.DataVenc & Chr(13) & Chr(13) & "" _
                        & "Local Descarga:                  " & DocumentoNovo_Venda.LocalDescarga & Chr(13) & "" _
                        & "Morada Entrega:                  " & Replace(DocumentoNovo_Venda.MoradaEntrega, "'", "") & Chr(13) & Chr(13) & "" _
                        & "Cumprimentos"


            FechaEmpresa()



            PriV100Api.BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")

        End If




        Dim k As Long
        For k = 1 To DocumentoNovo_Venda.Linhas.NumItens

            '        If DocumentoNovo_Venda.Linhas(k).TipoLinha = 60 And InStr(1, DocumentoNovo_Venda.Linhas(k).Descricao, "/N.º") > 0 Then
            '            ObjMotor.DSO.BDAPL.Execute "update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" & IDLinhaDocOriginal(k) & "' where id = '" & DocumentoNovo_Venda.Linhas(k).IdLinha & "' "
            '        End If
            '
            If InStr(1, IDLinhaDocOriginal(k), ";") > 0 And Len(Mid(IDLinhaDocOriginal(k), InStr(1, IDLinhaDocOriginal(k), ";") + 1, Len(IDLinhaDocOriginal(k)))) > 0 Then

                PriV100Api.BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" & Mid(IDLinhaDocOriginal(k), 1, InStr(1, IDLinhaDocOriginal(k), ";") - 1) & "' where  id = '" & Mid(IDLinhaDocOriginal(k), InStr(1, IDLinhaDocOriginal(k), ";") + 1, Len(IDLinhaDocOriginal(k))) & "' ")

            End If

        Next k

        On Error GoTo TrataErroUpdate

        PriV100Api.BSO.DSO.ExecuteSQL(" UPDATE CabecDoc " &
                                " SET CDU_DocumentoVendaDestino = '" & DocumentoNovo_Venda.Tipodoc & " " & DocumentoNovo_Venda.Serie & "/" & DocumentoNovo_Venda.NumDoc & "' " &
                                " where filial = '" & DocumentoBase.Filial & "' and  TipoDoc = '" & DocumentoBase.Tipodoc & "' and Serie = '" & DocumentoBase.Serie & "' and NumDoc = " & DocumentoBase.NumDoc & " ")


        '20170921
        Rastreabilidade(DocumentoBase.Filial, DocumentoBase.Serie, DocumentoBase.Tipodoc, DocumentoBase.NumDoc, BaseDadosDestino, DocumentoNovo_Venda)

        MsgBox("Foi gerado o documento de Venda " & DocumentoNovo_Venda.Tipodoc & " " & DocumentoNovo_Venda.Serie & "/" & DocumentoNovo_Venda.NumDoc & " na empresa " & BaseDadosDestino & " com a Entidade " & DocumentoNovo_Venda.Entidade, vbInformation + vbOKOnly)

        GerarDocumento_BaseVendas = True

        Exit Function

TrataErro:
        MsgBox("Erro: " & Err.Description, vbCritical, "Encomenda de Cliente ")
        GerarDocumento_BaseVendas = False
        Exit Function

TrataErroUpdate:
        MsgBox("Erro: " & Err.Description, vbCritical, "Gera Documento Base Vendas - TrataErroUpdate")
        MsgBox("Problemas na actualização do Num. documento de destino", vbCritical, "Gera Documento Base Vendas - TrataErroUpdate")
        GerarDocumento_BaseVendas = False
        Exit Function

    End Function


    Private Shared Function VerificaLote(ByVal str_Artigo As String, ByVal str_Lote As String)

        If str_Lote = "" Then Exit Function

        If PriV100Api.BSO.Inventario.ArtigosLotes.Existe(str_Artigo, str_Lote) = False Then

            Dim ArtigoLote As New InvBE100.InvBEArtigoLote

            Dim stdBE_ListaLote As StdBELista
            stdBE_ListaLote = PriV100Api.BSO.Consulta(" SELECT * FROM ArtigoLote " &
                                                     " WHERE Artigo = '" & str_Artigo & "' " &
                                                     " AND Lote = '" & str_Lote & "' ")

            If Not stdBE_ListaLote.Vazia Then

                stdBE_ListaLote.Inicio()

                ArtigoLote.Artigo = stdBE_ListaLote.Valor("Artigo")
                ArtigoLote.Lote = stdBE_ListaLote.Valor("Lote")
                ArtigoLote.Descricao = stdBE_ListaLote.Valor("Descricao")
                If Len(stdBE_ListaLote.Valor("DataFabrico")) > 0 Then ArtigoLote.DataFabrico = stdBE_ListaLote.Valor("DataFabrico")
                If Len(stdBE_ListaLote.Valor("Validade")) > 0 Then ArtigoLote.Validade = stdBE_ListaLote.Valor("Validade")
                ArtigoLote.Controlador = stdBE_ListaLote.Valor("Controlador")
                ArtigoLote.Activo = stdBE_ListaLote.Valor("Activo")
                ArtigoLote.Observacoes = stdBE_ListaLote.Valor("Observacoes")
                '2017-04-14
                ArtigoLote.CamposUtil("CDU_TipoQualidade").Valor = stdBE_ListaLote.Valor("CDU_TipoQualidade")
                ArtigoLote.CamposUtil("CDU_Parafinado").Valor = stdBE_ListaLote.Valor("CDU_Parafinado")
                PriV100Api.BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote)

            End If

        End If



    End Function


    '20170921
    Private Shared Function Rastreabilidade(ByVal Filial_FaturaCliente As String,
                                ByVal Serie_FaturaCliente As String,
                                ByVal TipoDoc_FaturaCliente As String,
                                ByVal NumDoc_FaturaCliente As Long,
                                ByVal BaseDadosDestino As String,
                                ByVal FaturaClienteFinal_AcabadoGerar As VndBE100.VndBEDocumentoVenda) As Boolean



        Dim Str_Rastreabilidade As String
        Dim Lst_StrRastreabilidade As StdBELista
        Dim ErroComunica As String
        Str_Rastreabilidade = "  SELECT CD2.TipoDoc as TipoDocEncomenda, CD2.NumDoc as NumDocEncomenda, CD2.Id as IdCabecDocEncomenda, LD2.Id as IdLinhasDocEncomenda, " &
                        " CD.TipoDoc as TipoDocFatura, CD.NumDoc as NumDocFatura , CD.Id as IdCabecDocFatura,  LD.Id as IdLinhasDocFatura , " &
                        " CDGrupo.TipoDoc as TipoDocEncomendaGrupo, CDGrupo.NumDoc as NumDocEncomendaGrupo ,  CDGrupo.Serie as SerieEncomendaGrupo , replace(CONVERT(VARCHAR(10),   CDGrupo.Data, 102),'.','/') as DataDocEncomendaGrupo ,   CDGrupo.Id as IdCabecDocEncomendaGrupo,  LDGrupo.Id as IdLinhasDocEncomendaGrupo , LDGrupo.Quantidade  as QuantidadeEncomendaGrupo ," &
                        " CDGrupoVFA.TipoDoc as TipoDocFaturaGrupo, CDGrupoVFA.NumDoc as NumDocFaturaGrupo, CDGrupoVFA.Id as IdCabecDocFaturaGrupo, LDGrupoVFA.Id as IdLinhasDocFaturaGrupo " &
                        " FROM CabecDoc  CD " &
                        " INNER JOIN LinhasDoc LD on LD.IdCabecDoc = CD.Id AND LD.TipoLinha <> 60" &
                        " INNER JOIN LinhasDocTrans LDS ON LDS.IdLinhasDoc = LD.id " &
                        " INNER JOIN LinhasDoc LD2 on LD2.id = LDS.IdLinhasDocOrigem " &
                        " INNER JOIN CabecDoc  CD2 on CD2.id = LD2.IdCabecDoc " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.LinhasDoc LDGrupo ON LDGrupo.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD2.id) + '}' " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.CabecDoc CDGrupo ON CDGrupo.id = LDGrupo.IdCabecDoc " &
                        " " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.LinhasDoc LDGrupoVFA ON LDGrupoVFA.CDU_IDLinhaOriginalGrupo = '{' + convert(nvarchar(50), LD.id) + '}' " &
                        " INNER JOIN PRI" & BaseDadosDestino & ".dbo.CabecDoc CDGrupoVFA ON CDGrupoVFA.id = LDGrupoVFA.IdCabecDoc  " &
                        " " &
                        " WHERE CDGrupo.TipoDoc not in ('FP') and CD.Filial = '" & Filial_FaturaCliente & "' AND CD.TipoDoc = '" & TipoDoc_FaturaCliente & "' AND CD.serie = '" & Serie_FaturaCliente & "' AND CD.NumDoc = " & NumDoc_FaturaCliente & " " &
                        " ORDER BY LD.NumLinha "
        'WHERE CDGrupoVFA.TipoDoc='ECL' filtro incluido a 13/07/2020 JFC
        'Motivo: Ao fazer Copia de Linhas da ECL para FP, ficava com rastreabilidade CDU_IDLinhaOriginalGrupo.
        'Ao executar esta query devolvia a ECL e FP, e tentava fazer duplo insert no LinhasDocTrans

        'CD = A
        'CD2 = B
        'CDGrupo = D

        Lst_StrRastreabilidade = PriV100Api.BSO.Consulta(Str_Rastreabilidade)

        If Lst_StrRastreabilidade.Vazia = False Then

            Lst_StrRastreabilidade.Inicio()

            CompletarComentarioRastreabilidade(False, FaturaClienteFinal_AcabadoGerar, Lst_StrRastreabilidade.Valor("TipoDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("SerieEncomendaGrupo"), Lst_StrRastreabilidade.Valor("NumDocEncomendaGrupo"), Lst_StrRastreabilidade.Valor("DataDocEncomendaGrupo"))

            Do While Not Lst_StrRastreabilidade.NoFim

                Application.DoEvents()
                'IdLinhasDoc -> Fatura de Fornecedor do Grupo
                'IdLinhasDocOrigem -> Encomenda de Fornecedor

                PriV100Api.BSO.DSO.ExecuteSQL(" INSERT INTO LinhasDocTrans " &
                             " (IdLinhasDoc,IdLinhasDocOrigem,QuantTrans) " &
                              " VALUES( '" & Lst_StrRastreabilidade.Valor("IdLinhasDocFaturaGrupo") & "','" & Lst_StrRastreabilidade.Valor("IdLinhasDocEncomendaGrupo") & "'," & Replace(Lst_StrRastreabilidade.Valor("QuantidadeEncomendaGrupo"), ",", ".") & " ) ")

                Lst_StrRastreabilidade.Seguinte()
            Loop

            'If MsgBox("Deseja comunicar o documento às Finanças?", vbQuestion + vbYesNo) = vbYes Then

            If PriV100Api.BSO.Base.Series.Edita("V", DocumentoNovo_Venda.Tipodoc, DocumentoNovo_Venda.Serie).TipoComunicacao = "2" Then
                PriV100Api.BSO.Internos.Documentos.ATComunicaDocumentoId(DocumentoNovo_Venda.ID, "V", ErroComunica)
            End If

            If ErroComunica <> "" Then
                MsgBox(ErroComunica, vbCritical, "")
            End If
            'End If


        Else
            'Se não tiver rastreabilidade em cima, NÂO FAZ NADA
            CompletarComentarioRastreabilidade(True, FaturaClienteFinal_AcabadoGerar, "", "", 0, Now)
        End If


    End Function


    '20170921
    Private Shared Sub CompletarComentarioRastreabilidade(ByVal Apagar As Boolean,
                                                    ByVal FaturaClienteFinal_AcabadoGerar As VndBE100.VndBEDocumentoVenda,
                                                    ByVal Tipodoc As String,
                                                    ByVal Serie As String,
                                                    ByVal NumDoc As Long,
                                                    ByVal Data As Date)


        FaturaClienteFinal_AcabadoGerar = PriV100Api.BSO.Vendas.Documentos.Edita(FaturaClienteFinal_AcabadoGerar.Filial, FaturaClienteFinal_AcabadoGerar.Tipodoc, FaturaClienteFinal_AcabadoGerar.Serie, FaturaClienteFinal_AcabadoGerar.NumDoc)

        If Apagar = False Then
            If FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).TipoLinha = "60" Then
                If FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = "@1@ @2@/N.º@3@ de @4@" Then
                    FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = Replace(FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@1@", Tipodoc)
                    FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = Replace(FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@2@", Serie)
                    FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = Replace(FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@3@", NumDoc)
                    FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = Replace(FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao, "@4@", Data)
                End If
            End If
        Else
            If FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).TipoLinha = "60" Then
                If FaturaClienteFinal_AcabadoGerar.Linhas.GetEdita(1).Descricao = "@1@ @2@/N.º@3@ de @4@" Then
                    FaturaClienteFinal_AcabadoGerar.Linhas.Remove(1)
                End If
            End If
        End If

        PriV100Api.BSO.Vendas.Documentos.Actualiza(FaturaClienteFinal_AcabadoGerar)

    End Sub
    Public Shared Function GerarDocumento_BaseCompras(ByVal DocumentoBase As CmpBE100.CmpBEDocumentoCompra,
                                            ByVal BaseDadosDestino As String,
                                            ByVal TipoDoc_Destino As String,
                                            ByVal Serie_Destino As String,
                                            ByVal Entidade_Destino As String,
                                            ByVal Armazem_Destino As String) As Boolean

        On Error GoTo TrataErro



        'Identifica o Documento acabado de Criar (Encomenda a Cliente)
        'IdentificarDocumento Filial_Origem, Serie_Origem, TipoDoc_Origem, NumDoc_Origem

        'Gera Documento de Venda (encomenda a Cliente) à PERCATO
        If Not AbreEmpresa(BaseDadosDestino) Then GerarDocumento_BaseCompras = False : Exit Function

        DocumentoNovo_Venda = New VndBE100.VndBEDocumentoVenda

        DocumentoNovo_Venda.Filial = "000"
        DocumentoNovo_Venda.Serie = Serie_Destino
        'DocCompra consta nos Campos de utilizador
        DocumentoNovo_Venda.Tipodoc = TipoDoc_Destino
        'Fornecedor
        DocumentoNovo_Venda.TipoEntidade = "C"
        'CodFornecedor consta nos Campos de utilizador
        DocumentoNovo_Venda.Entidade = Entidade_Destino
        '-----> Falta passar o documento original
        'DocumentoCompra.doc = DocumentoBase.Serie & "/" & DocumentoBase.NumDoc

        'DocumentoNovo_Venda.Referencia = DocumentoBase.tipoDoc & " " & DocumentoBase.Serie & "/" & DocumentoBase.NumDoc


        DocumentoNovo_Venda.Referencia = DocumentoBase.NumDocExterno
        DocumentoNovo_Venda.LocalCarga = DocumentoBase.LocalCarga
        DocumentoNovo_Venda.LocalDescarga = DocumentoBase.LocalDescarga
        DocumentoNovo_Venda.CamposUtil("CDU_DocumentoOrigem").Valor = DocumentoBase.Tipodoc & " " & DocumentoBase.Serie & "/" & DocumentoBase.NumDoc

        'Condições
        DocumentoNovo_Venda.ModoPag = DocumentoBase.ModoPag
        DocumentoNovo_Venda.CondPag = DocumentoBase.CondPag
        DocumentoNovo_Venda.Moeda = DocumentoBase.Moeda
        DocumentoNovo_Venda.ContaDomiciliacao = DocumentoBase.ContaDomiciliacao
        DocumentoNovo_Venda.Responsavel = DocumentoBase.Responsavel


        PriV100Api.BSO.Vendas.Documentos.PreencheDadosRelacionados(DocumentoNovo_Venda, 5) 'GcpBE800.PreencheRelacaoVendas.vdDadosTodos
        'Tem de estar depois dos dados relazionados!! porque senao sugere a data do sistema e altera a data do documento
        DocumentoNovo_Venda.DataDoc = DocumentoBase.DataDoc
        'se não colocar isto, nao consigo gravar documentos de uma série diferente à serie actual, praticada na data do sistema
        'DocumentoCompra.DataIntroducao = DocumentoCompra.DataDoc

        Dim i As Long
        Dim j As Long
        j = 1
        Dim Artigo As String
        Dim Quantidade As Double
        Dim Armazem As String
        Dim Localizacao As String

        For i = 1 To DocumentoBase.Linhas.NumItens
            'Adicionar a linha ao documento

            If DocumentoBase.Linhas.GetEdita(i).TipoLinha = 60 Then

                If InStr(1, DocumentoBase.Linhas.GetEdita(i).Descricao, "/N.º") = 0 Then

                    PriV100Api.BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoNovo_Venda, vdTipoLinhaEspecial.vdLinha_Comentario, Descricao:=DocumentoBase.Linhas.GetEdita(i).Descricao)
                    IDLinhaDocOriginal(j) = DocumentoBase.Linhas.GetEdita(i).IdLinha & ";" & DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha
                    j = j + 1

                End If

            Else


                Artigo = DocumentoBase.Linhas.GetEdita(i).Artigo
                If Not PriV100Api.BSO.Base.Artigos.Existe(Artigo) Then
                    MsgBox("O Artigo " & Artigo & " não existe na Empresa " & BaseDadosDestino, vbCritical, "Artigo não existente")
                    GerarDocumento_BaseCompras = False
                    Exit Function
                End If

                VerificaLote(Artigo, DocumentoBase.Linhas.GetEdita(i).Lote)

                Quantidade = DocumentoBase.Linhas.GetEdita(i).Quantidade

                'Se o armazem dos parametros preenchido, sua esse. Caso contrário, usa o das linhas
                Armazem = IIf(Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Armazem)
                Localizacao = IIf(Len(Armazem_Destino) > 0, Armazem_Destino, DocumentoBase.Linhas.GetEdita(i).Localizacao)

                'Armazem = DocumentoBase.Linhas(i).Armazem
                'Localizacao = DocumentoBase.Linhas(i).Localizacao

                PriV100Api.BSO.Vendas.Documentos.AdicionaLinha(DocumentoNovo_Venda, Artigo, Quantidade, Armazem, Localizacao)

                DocumentoNovo_Venda.Linhas.GetEdita(j).Descricao = DocumentoBase.Linhas.GetEdita(i).Descricao
                DocumentoNovo_Venda.Linhas.GetEdita(j).Lote = DocumentoBase.Linhas.GetEdita(i).Lote
                DocumentoNovo_Venda.Linhas.GetEdita(j).Unidade = DocumentoBase.Linhas.GetEdita(i).Unidade
                'DocumentoNovo_Venda.Linhas(j).Armazem = DocumentoBase.Linhas(i).Armazem
                'DocumentoNovo_Venda.Linhas(j).Localizacao = DocumentoBase.Linhas(i).Localizacao
                DocumentoNovo_Venda.Linhas.GetEdita(j).CodIva = DocumentoBase.Linhas.GetEdita(i).CodIva
                DocumentoNovo_Venda.Linhas.GetEdita(j).TaxaIva = DocumentoBase.Linhas.GetEdita(i).TaxaIva
                DocumentoNovo_Venda.Linhas.GetEdita(j).PrecUnit = DocumentoBase.Linhas.GetEdita(i).PrecUnit
                'DocumentoNovo_Venda.Linhas(j).DescontoComercial = DocumentoBase.Linhas(i).DescontoComercial
                'DocumentoNovo_Venda.Linhas(j).Desconto1 = DocumentoBase.Linhas(i).Desconto1
                'DocumentoNovo_Venda.Linhas(j).Desconto2 = DocumentoBase.Linhas(i).Desconto2
                'DocumentoNovo_Venda.Linhas(j).Desconto3 = DocumentoBase.Linhas(i).Desconto3
                DocumentoNovo_Venda.Linhas.GetEdita(j).MovStock = DocumentoBase.Linhas.GetEdita(i).MovStock
                DocumentoNovo_Venda.Linhas.GetEdita(j).DataStock = DocumentoBase.Linhas.GetEdita(i).DataStock

                DocumentoNovo_Venda.Linhas.GetEdita(j).DataEntrega = DocumentoBase.Linhas.GetEdita(i).DataEntrega

                'Para garantir a rastreabilidade
                'DocumentoNovo_Venda.Linhas(j).CamposUtil("CDU_IDLinhaOriginalPercato").Valor = DocumentoCompra.Linhas(j).IDLinha
                IDLinhaDocOriginal(j) = DocumentoBase.Linhas.GetEdita(i).IdLinha & ";" & DocumentoNovo_Venda.Linhas.GetEdita(j).IdLinha
                j = j + 1
            End If
        Next

        PriV100Api.BSO.Vendas.Documentos.Actualiza(DocumentoNovo_Venda)

        Dim k As Long
        For k = 1 To DocumentoNovo_Venda.Linhas.NumItens
            '        If DocumentoNovo_Venda.Linhas(k).TipoLinha = 60 And InStr(1, DocumentoNovo_Venda.Linhas(k).Descricao, "/N.º") > 0 Then
            '            ObjMotor.DSO.BDAPL.Execute "update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" & IDLinhaDocOriginal(k) & "' where id = '" & DocumentoNovo_Venda.Linhas(k).IdLinha & "' "
            '        Else
            '
            '        End If

            If InStr(1, IDLinhaDocOriginal(k), ";") > 0 And Len(Mid(IDLinhaDocOriginal(k), InStr(1, IDLinhaDocOriginal(k), ";") + 1, Len(IDLinhaDocOriginal(k)))) > 0 Then

                PriV100Api.BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_IDLinhaOriginalGrupo = '" & Mid(IDLinhaDocOriginal(k), 1, InStr(1, IDLinhaDocOriginal(k), ";") - 1) & "' where  id = '" & Mid(IDLinhaDocOriginal(k), InStr(1, IDLinhaDocOriginal(k), ";") + 1, Len(IDLinhaDocOriginal(k))) & "' ")

            End If

        Next k

        On Error GoTo TrataErroUpdate

        PriV100Api.BSO.DSO.ExecuteSQL(" UPDATE CabecCompras " &
                                " SET CDU_DocumentoVendaDestino = '" & DocumentoNovo_Venda.Tipodoc & " " & DocumentoNovo_Venda.Serie & "/" & DocumentoNovo_Venda.NumDoc & "' " &
                                " where filial = '" & DocumentoBase.Filial & "' and  TipoDoc = '" & DocumentoBase.Tipodoc & "' and Serie = '" & DocumentoBase.Serie & "' and NumDoc = " & DocumentoBase.NumDoc & " ")

        MsgBox("Foi gerado o documento de Venda " & DocumentoNovo_Venda.Tipodoc & " " & DocumentoNovo_Venda.Serie & "/" & DocumentoNovo_Venda.NumDoc & " na empresa " & BaseDadosDestino & " com a Entidade " & DocumentoNovo_Venda.Entidade, vbInformation + vbOKOnly)

        GerarDocumento_BaseCompras = True

        Exit Function

TrataErro:
        MsgBox("Erro: " & Err.Description, vbCritical, "Encomenda de Cliente ")
        GerarDocumento_BaseCompras = False
        Exit Function

TrataErroUpdate:
        MsgBox("Erro: " & Err.Description, vbCritical, "Gera Documento Base Vendas - TrataErroUpdate")
        MsgBox("Problemas na actualização do Num. documento de destino", vbCritical, "Gera Documento Base Vendas - TrataErroUpdate")
        GerarDocumento_BaseCompras = False
        Exit Function

    End Function

End Class
