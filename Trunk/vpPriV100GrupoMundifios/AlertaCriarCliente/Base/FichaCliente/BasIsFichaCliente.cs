using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;
using System;

namespace AlertaCriarCliente
{
    public class BasIsFichaCliente : FichaClientes
    {
        public override void DepoisDeGravar(string Cliente, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Cliente, e);

            if (Module1.VerificaToken("AlertaCriarCliente") == 1)
            {
                long i;
                StdBELista listEnt;
                string VarAssunto;
                string VarFrom;
                string VarTo;
                string VarTextoInicialMsg;
                string VarMensagem;
                string VarUtilizador;

                listEnt = BSO.Consulta("select f.Cliente, f.Nome,  f.Fac_Mor as 'Morada', f.Fac_Local as 'Local',  f.Fac_Cp as 'Cp', f.Fac_Cploc as 'CpLoc',  f.Distrito,  f.TipoTerceiro, f.Pais, f.Idioma, f.NumContrib, f.CondPag, f.ModoPag, f.Moeda, f.CDU_EntidadeInterna from PRIMUNDITALIA.dbo.Clientes f where f.CDU_EntidadeInterna not in (select CDU_EntidadeInterna from primundifios.dbo.Clientes)");

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

                    VarAssunto = "Munditalia - Clientes: Lista de Clientes a Criar na Mundifios";
                    VarUtilizador = Aplicacao.Utilizador.Utilizador;
                    VarMensagem = "";

                    for (i = 1; i <= listEnt.NumLinhas(); i++)
                    {
                        VarMensagem = VarMensagem + Strings.Chr(13) + Strings.Chr(13) + ""
                                    + "Cliente:         " + listEnt.Valor("Cliente") + Strings.Chr(13) + ""
                                    + "Nome:            " + Strings.Replace(listEnt.Valor("Nome"), "'", "") + Strings.Chr(13) + ""
                                    + "Morada:          " + Strings.Replace(listEnt.Valor("Morada"), "'", "") + Strings.Chr(13) + ""
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
                    VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Os seguintes Clientes não estão criados na Mundifios:" + Strings.Chr(13) + Strings.Chr(13) + ""
                                + VarMensagem + ""
                                + "Cumprimentos";

                    BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
                }
            }
        }
    }
}