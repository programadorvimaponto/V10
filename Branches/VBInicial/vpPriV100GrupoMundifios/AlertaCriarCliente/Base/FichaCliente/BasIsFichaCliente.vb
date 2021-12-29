Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace AlertaCriarCliente
    Public Class BasIsFichaCliente
        Inherits FichaClientes

        Public Overrides Sub DepoisDeGravar(Cliente As String, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Cliente, e)

            If Module1.VerificaToken("AlertaCriarCliente") = 1 Then


                Dim i As Long
                Dim listEnt As StdBELista
                Dim VarAssunto As String
                Dim VarFrom As String
                Dim VarTo As String
                Dim VarTextoInicialMsg As String
                Dim VarMensagem As String
                Dim VarUtilizador As String


                listEnt = BSO.Consulta("select f.Cliente, f.Nome,  f.Fac_Mor as 'Morada', f.Fac_Local as 'Local',  f.Fac_Cp as 'Cp', f.Fac_Cploc as 'CpLoc',  f.Distrito,  f.TipoTerceiro, f.Pais, f.Idioma, f.NumContrib, f.CondPag, f.ModoPag, f.Moeda, f.CDU_EntidadeInterna from PRIMUNDITALIA.dbo.Clientes f where f.CDU_EntidadeInterna not in (select CDU_EntidadeInterna from primundifios.dbo.Clientes)")

                If listEnt.Vazia = False Then
                    listEnt.Inicio()

                    VarFrom = ""

                    VarTo = "informatica@mundifios.pt; mafaldamachado@mundifios.pt"


                    If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                        VarTextoInicialMsg = "Bom dia,"
                    ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                        VarTextoInicialMsg = "Boa tarde,"
                    Else
                        VarTextoInicialMsg = "Boa noite,"
                    End If

                    VarAssunto = "Munditalia - Clientes: Lista de Clientes a Criar na Mundifios"
                    VarUtilizador = Aplicacao.Utilizador.Utilizador
                    VarMensagem = ""

                    For i = 1 To listEnt.NumLinhas






                        VarMensagem = VarMensagem & Chr(13) & Chr(13) & "" _
                                    & "Cliente:         " & listEnt.Valor("Cliente") & Chr(13) & "" _
                                    & "Nome:            " & Replace(listEnt.Valor("Nome"), "'", "") & Chr(13) & "" _
                                    & "Morada:          " & Replace(listEnt.Valor("Morada"), "'", "") & Chr(13) & "" _
                                    & "Local:           " & listEnt.Valor("Local") & Chr(13) & "" _
                                    & "CodigoPostal:    " & listEnt.Valor("Cp") & Chr(13) & "" _
                                    & "Localidade:      " & listEnt.Valor("CpLoc") & Chr(13) & "" _
                                    & "Distrito:        " & listEnt.Valor("Distrito") & Chr(13) & "" _
                                    & "TipoTerceiro:    " & listEnt.Valor("TipoTerceiro") & Chr(13) & "" _
                                    & "Pais:            " & listEnt.Valor("Pais") & Chr(13) & "" _
                                    & "Idioma:          " & listEnt.Valor("Idioma") & Chr(13) & "" _
                                    & "NIF:             " & listEnt.Valor("NumContrib") & Chr(13) & "" _
                                    & "CondPag:         " & listEnt.Valor("CondPag") & Chr(13) & "" _
                                    & "ModoPag:          " & listEnt.Valor("ModoPag") & Chr(13) & "" _
                                    & "Moeda:           " & listEnt.Valor("Moeda") & Chr(13) & "" _
                                    & "EntidadeInterna: " & listEnt.Valor("CDU_EntidadeInterna") & Chr(13) & ""


                        listEnt.Seguinte()
                    Next i
                    VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Os seguintes Clientes não estão criados na Mundifios:" & Chr(13) & Chr(13) & "" _
                                & VarMensagem & "" _
                                & "Cumprimentos"



                    BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")

                End If
            End If
        End Sub


    End Class
End Namespace
