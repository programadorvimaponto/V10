Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace IntegracaoCondPag
    Public Class BasIsFichaCondsPag
        Inherits FichaCondsPag

        Dim UpdtDescricao As Boolean
        Dim UpdtCondPag As Boolean
        Dim listEmpresas As StdBELista
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("IntegracaoCondPag") = 1 Then

                'Valida se a condição de pagamento ja existe
                If BSO.Base.CondsPagamento.Existe(Me.CondPag.CondPag) Then
                    UpdtCondPag = True
                    'Valida se foi alterada a descrição
                    If BSO.Base.CondsPagamento.DaValorAtributo(Me.CondPag.CondPag, "Descricao") = Me.CondPag.Descricao Then
                        UpdtDescricao = False
                    Else
                        UpdtDescricao = MsgBox("Deseja atualizar a descrição nas restantes empresas? ", vbYesNo)
                    End If

                Else
                    UpdtCondPag = False

                End If
            End If

        End Sub


        Public Overrides Sub DepoisDeAnular(CondPag As String, e As ExtensibilityEventArgs)
            MyBase.DepoisDeAnular(CondPag, e)

            If Module1.VerificaToken("IntegracaoCondPag") = 1 Then

                'JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" & Aplicacao.Empresa.CodEmp & "' and PRI_FichaCondsPag='1'")
                listEmpresas.Inicio

                For i = 1 To listEmpresas.NumLinhas
                    BSO.DSO.ExecuteSQL("delete from pri" & listEmpresas.Valor("Empresa") & ".dbo.CondPag where CondPag ='" & Me.CondPag.CondPag & "'")
                    listEmpresas.Seguinte
                Next i

            End If
        End Sub

        Public Overrides Sub DepoisDeGravar(CondPag As String, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(CondPag, e)

            If Module1.VerificaToken("IntegracaoCondPag") = 1 Then

                'JFC  05/06/2020 Tabela DEV_Empresas deverá conter todas empresas onde este desenvolvimento é aplicavel.
                listEmpresas = BSO.Consulta("select Empresa from PRIEMPRE.dbo.DEV_Empresas where Empresa != '" & Aplicacao.Empresa.CodEmp & "' and PRI_FichaCondsPag='1'")
                listEmpresas.Inicio()

                If UpdtCondPag Then

                    'Validade se existe e se quer alterar a Descriçaão
                    If UpdtDescricao Then

                        listEmpresas.Inicio()

                        For i = 1 To listEmpresas.NumLinhas
                            BSO.DSO.ExecuteSQL("update pri" & listEmpresas.Valor("Empresa") & ".dbo.CondPag set Descricao = '" & Me.CondPag.Descricao & "', Dias='" & Me.CondPag.DiasVencimento & "', Desconto= '" & Me.CondPag.Desconto & "',EntradaInicial= '" & Me.CondPag.EntradaInicial & "', DiasEntrada= '" & Me.CondPag.DiasVencimentoEntradaInicial & "', NumPrestacoes= '" & Me.CondPag.NumeroPrestacoes & "', PeriodoPrestacoes= '" & Me.CondPag.PeriodicidadePrestacoes & "',TipoCondicao= '" & Me.CondPag.TipoCondicao & "', Clientes='" & Me.CondPag.DescLiqClientes & "', Fornecedores ='" & Me.CondPag.DescLiqFornecedores & "', OutrosCredores ='" & Me.CondPag.DescLiqOutrosCredores & "',OutrosDevedores ='" & Me.CondPag.DescLiqOutrosDevedores & "',  DescontoIncluiIVA ='" & Me.CondPag.DescontoIncluiIVA & "', CDU_RQ ='" & Me.CondPag.CamposUtil("CDU_RQ").Valor & "',CDU_RM ='" & Me.CondPag.CamposUtil("CDU_RM").Valor & "' where CondPag= '" & Me.CondPag.CondPag & "'")
                            listEmpresas.Seguinte()
                        Next i

                    Else

                        For i = 1 To listEmpresas.NumLinhas
                            BSO.DSO.ExecuteSQL("update pri" & listEmpresas.Valor("Empresa") & ".dbo.CondPag set Dias='" & Me.CondPag.DiasVencimento & "', Desconto= '" & Me.CondPag.Desconto & "',EntradaInicial= '" & Me.CondPag.EntradaInicial & "', DiasEntrada= '" & Me.CondPag.DiasVencimentoEntradaInicial & "', NumPrestacoes= '" & Me.CondPag.NumeroPrestacoes & "', PeriodoPrestacoes= '" & Me.CondPag.PeriodicidadePrestacoes & "',TipoCondicao= '" & Me.CondPag.TipoCondicao & "', Clientes='" & Me.CondPag.DescLiqClientes & "', Fornecedores ='" & Me.CondPag.DescLiqFornecedores & "', OutrosCredores ='" & Me.CondPag.DescLiqOutrosCredores & "',OutrosDevedores ='" & Me.CondPag.DescLiqOutrosDevedores & "', DescontoIncluiIVA ='" & Me.CondPag.DescontoIncluiIVA & "',CDU_RQ ='" & Me.CondPag.CamposUtil("CDU_RQ").Valor & "',CDU_RM ='" & Me.CondPag.CamposUtil("CDU_RM").Valor & "' where CondPag= '" & Me.CondPag.CondPag & "'")
                            listEmpresas.Seguinte()
                        Next i

                    End If

                    'Se a condição de Pagamento não existe, então cria.
                Else

                    For i = 1 To listEmpresas.NumLinhas
                        BSO.DSO.ExecuteSQL("insert into " _
                        & "PRI" & listEmpresas.Valor("Empresa") & ".dbo.CondPag(CondPag,Descricao,Dias,Desconto,EntradaInicial,DiasEntrada,NumPrestacoes,PeriodoPrestacoes,TipoCondicao,SugereDescontosLiquidacao,Clientes,Fornecedores,OutrosCredores,OutrosDevedores,Meses30Dias,DescontoIncluiIVA,CDU_RQ,CDU_RM)values('" & Me.CondPag.CondPag & "','" & Me.CondPag.Descricao & "','" & Me.CondPag.DiasVencimento & "', '" & Me.CondPag.Desconto & "', '" & Me.CondPag.EntradaInicial & "','" & Me.CondPag.DiasVencimentoEntradaInicial & "','" & Me.CondPag.NumeroPrestacoes & "','" & Me.CondPag.PeriodicidadePrestacoes & "','" & Me.CondPag.TipoCondicao & "','" & Me.CondPag.SugereDescontosLiquidacao & "','" & Me.CondPag.DescLiqClientes & "','" & Me.CondPag.DescLiqFornecedores & "','" & Me.CondPag.DescLiqOutrosCredores & "','" & Me.CondPag.DescLiqOutrosDevedores & "','" & Me.CondPag.Meses30Dias & "','" & Me.CondPag.DescontoIncluiIVA & "','" & Me.CondPag.CamposUtil("CDU_RQ").Valor & "','" & Me.CondPag.CamposUtil("CDU_RM").Valor & "')")
                        listEmpresas.Seguinte()
                    Next i

                End If
            End If

        End Sub

    End Class
End Namespace
