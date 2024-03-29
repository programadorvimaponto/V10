using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;
using System;

namespace AlertaCriarFornecedor
{
    public class BasIsFichaFornecedor : FichaFornecedores
    {
        public override void DepoisDeGravar(string Fornecedor, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Fornecedor, e);

            if (Module1.VerificaToken("AlertaCriarFornecedor") == 1)
            {
                int i;
                StdBELista listEnt;
                string VarAssunto;
                string VarFrom;
                string VarTo;
                string VarTextoInicialMsg;
                string VarMensagem;
                string VarUtilizador;

                listEnt = BSO.Consulta("select f.Fornecedor, f.Nome,  f.Morada, f.Local,  f.Cp, f.CpLoc,  f.Distrito,  f.TipoTerceiro, f.Pais, f.Idioma, f.NumContrib, f.CondPag, f.ModoPag, f.Moeda, f.CDU_EntidadeInterna from PRIMUNDITALIA.dbo.Fornecedores f where isnull(f.CDU_EntidadeInterna,'') not in (select isnull(CDU_EntidadeInterna,'') from primundifios.dbo.Fornecedores)");

                if (listEnt.Vazia() == false)
                {
                    listEnt.Inicio();

                    VarFrom = "";

                    VarTo = "informatica@mundifios.pt; mafaldamachado@mundifios.pt";

                    if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
                        VarTextoInicialMsg = "Bom dia,";
                    else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
                        VarTextoInicialMsg = "Boa tarde,";
                    else
                        VarTextoInicialMsg = "Boa noite,";

                    VarAssunto = "Munditalia - Fornecedores: Lista de Fornecedores a Criar na Mundifios";
                    VarUtilizador = Aplicacao.Utilizador.Utilizador;
                    VarMensagem = "";

                    for (i = 1; i <= listEnt.NumLinhas(); i++)
                    {
                        VarMensagem = VarMensagem + Strings.Chr(13) + Strings.Chr(13) + ""
                                    + "Fornecedor:         " + listEnt.Valor("Fornecedor") + Strings.Chr(13) + ""
                                    + "Nome:            " + listEnt.Valor("Nome") + Strings.Chr(13) + ""
                                    + "Morada:          " + listEnt.Valor("Morada") + Strings.Chr(13) + ""
                                    + "Local:           " + listEnt.Valor("Local") + Strings.Chr(13) + ""
                                    + "CodigoPostal:    " + listEnt.Valor("Cp") + Strings.Chr(13) + ""
                                    + "Localidade:      " + listEnt.Valor("CpLoc") + Strings.Chr(13) + ""
                                    + "Distrito:        " + listEnt.Valor("Distrito") + Strings.Chr(13) + ""
                                    + "TipoTerceiro:    " + listEnt.Valor("TipoTerceiro") + Strings.Chr(13) + ""
                                    + "Pais:            " + listEnt.Valor("Pais") + Strings.Chr(13) + ""
                                    + "Idioma:          " + listEnt.Valor("Idioma") + Strings.Chr(13) + ""
                                    + "NIF:             " + listEnt.Valor("NumContrib") + Strings.Chr(13) + ""
                                    + "CondPag:         " + listEnt.Valor("CondPag") + Strings.Chr(13) + ""
                                    + "ModoPag:          " + listEnt.Valor("ModoPag") + Strings.Chr(13) + ""
                                    + "Moeda:           " + listEnt.Valor("Moeda") + Strings.Chr(13) + ""
                                    + "EntidadeInterna: " + listEnt.Valor("CDU_EntidadeInterna") + Strings.Chr(13) + "";

                        listEnt.Seguinte();
                    }
                    VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Os seguintes Fornecedores n�o est�o criados na Mundifios:" + Strings.Chr(13) + Strings.Chr(13) + ""
                                + VarMensagem + ""
                                + "Cumprimentos";

                    BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
                }
            }
        }
    }
}