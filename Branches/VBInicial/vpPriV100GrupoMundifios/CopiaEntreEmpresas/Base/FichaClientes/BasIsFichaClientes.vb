Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace CopiaEntreEmpresas
    Public Class BasIsFichaClientes
        Inherits FichaClientes


        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then

                If Not ValidacoesCamposUtilizador() Then Cancel = True

            End If

        End Sub

        '#edusamp
        Public Function ValidacoesCamposUtilizador() As Boolean

            If Len(Me.Cliente.CamposUtil("CDU_NomeEmpresaGrupo").Valor & "") = 0 Then

                'Se não tiver empresa definida, garante que o campo de utilizador de fornecedor está vazio
                Me.Cliente.CamposUtil("CDU_CodigoFornecedorGrupo").Valor = ""

                ValidacoesCamposUtilizador = True : Exit Function

            Else
                'Se a empresa de grupo estiver preenchida..

                'Colocar a empresa em maísculas
                Me.Cliente.CamposUtil("CDU_NomeEmpresaGrupo").Valor = UCase(Me.Cliente.CamposUtil("CDU_NomeEmpresaGrupo").Valor & "")

                If Not GetTrueIfEmpresaValida(Me.Cliente.CamposUtil("CDU_NomeEmpresaGrupo").Valor & "") Then
                    'Se a empresa não for válida, aviso (na outra função) e retorno falso
                    ValidacoesCamposUtilizador = False : Exit Function

                Else

                    If GetTrueIfFornecedorValido(Me.Cliente.CamposUtil("CDU_NomeEmpresaGrupo").Valor & "", Me.Cliente.CamposUtil("CDU_CodigoFornecedorGrupo").Valor & "") = False Then

                        ValidacoesCamposUtilizador = False : Exit Function

                    Else

                        If GetTrueIfArmazemValido(Me.Cliente.CamposUtil("CDU_NomeEmpresaGrupo").Valor & "", Me.Cliente.CamposUtil("CDU_ArmazemGrupo").Valor & "") = False Then
                            ValidacoesCamposUtilizador = False : Exit Function
                        Else
                            ValidacoesCamposUtilizador = True : Exit Function
                        End If

                    End If

                End If

            End If

        End Function

        '#edusamp
        Public Function GetTrueIfArmazemValido(ByVal Empresa As String, ByVal Armazem As String) As Boolean



            Dim stdBE_ListaArmazem As StdBELista
            stdBE_ListaArmazem = BSO.Consulta("SELECT Armazem " &
                                                        "  FROM PRI" & Empresa & ".dbo.Armazens " &
                                                        "  WHERE Armazem = '" & Armazem & "'")

            If Not stdBE_ListaArmazem.Vazia Then

                GetTrueIfArmazemValido = True
            Else
                MsgBox("O Armazém '" & Armazem & "' não existe ou não é válido.", vbCritical + vbOKOnly, "Campo de Utilizador:  'Armazém Grupo'")
                GetTrueIfArmazemValido = False
            End If

        End Function

        '#edusamp
        Public Function GetTrueIfFornecedorValido(ByVal Empresa As String, ByVal Fornecedor As String) As Boolean



            Dim stdBE_ListaFornecedor As StdBELista
            stdBE_ListaFornecedor = BSO.Consulta("SELECT Fornecedor " &
                                                        "  FROM PRI" & Empresa & ".dbo.Fornecedores " &
                                                        "  WHERE Fornecedor = '" & Fornecedor & "' and FornecedorAnulado = 'false'")

            If Not stdBE_ListaFornecedor.Vazia Then

                GetTrueIfFornecedorValido = True
            Else
                MsgBox("O Fornecedor '" & Fornecedor & "' não é válido ou está anulado.", vbCritical + vbOKOnly, "Campo de Utilizador:  'Cód. Fornecedor Grupo'")
                GetTrueIfFornecedorValido = False
            End If

        End Function


        '#edusamp
        Public Function GetTrueIfEmpresaValida(ByVal Empresa As String) As Boolean

            'Verificar se o Nome de Empresa de Destino pertence à empresa de grupo. Não basta apenas estar preenchido!
            Dim stdBE_ListaEmpresasGrupo As StdBELista
            stdBE_ListaEmpresasGrupo = BSO.Consulta("SELECT CDU_Empresa, CDU_Nome, CDU_Instancia " &
                                                        "  FROM PRIEMPRE.dbo.TDU_EmpresasGrupo " &
                                                        "  WHERE CDU_Empresa = '" & Empresa & "' ")

            If Not stdBE_ListaEmpresasGrupo.Vazia Then

                GetTrueIfEmpresaValida = True
            Else
                MsgBox("A Empresa '" & Empresa & "' não pertence às empresas do grupo." & Chr(13) & "Isto é, não consta na tabela TDU_EmpresasGrupo.", vbCritical + vbOKOnly, "Campo de Utilizador: 'Nome Emp. Destino'")
                GetTrueIfEmpresaValida = False
            End If

        End Function


    End Class
End Namespace
