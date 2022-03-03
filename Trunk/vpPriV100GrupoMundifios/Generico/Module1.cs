using Microsoft.VisualBasic;
using System;
using Vimaponto.PrimaveraV100;
using ErpBS100;

namespace Generico
{
    public static class Module1
    {
        public static int VerificaToken(string token)
        {
            if (PriV100Api.BSO.DSO.DaValorUnico("SELECT CDU_AplicaFuncionalidade FROM TDU_FuncionalidadesExt WHERE CDU_TokenFuncionalidade = '" + token + "'") is bool aplica && aplica)
            {
                return 1;
            }

            return 0;
        }

        // *******************************************************************************************************************************************
        // #### ARMAZEM ENTREPOSTO ####
        // *******************************************************************************************************************************************
        public static string ArmEntreposto;

        // *******************************************************************************************************************************************
        // #### ARMAZEM ENTREPOSTO ####
        // *******************************************************************************************************************************************

        // ###############################################################
        // # Declaração Variaveis para Formulário FrmAlteraGuiaAEP (JFC) #
        // ###############################################################
        public static string aepArtigo;

        public static string aepDocumento;
        public static string aepLote;
        public static string aepArmazem;
        public static string aepDespDAU;
        public static string aepRegime;
        public static object aepIDlinha;

        // Utilizado no frmInditex Bruno Peixoto 02/09/2020
        public static string certFiacoes;

        // ###############################################################
        // # Declaração Variaveis para Formulário FrmDisputa(JFC) #
        // ###############################################################
        public static string dspModulo;

        public static string dsptipoDoc;
        public static string dspNumDoc;
        public static string dspSerie;
        public static bool dspDisputa;
        // ###############################################################
        // # Declaração Variaveis para Formulário FrmDisputa(JFC) #
        // ###############################################################

        // ############################################################################
        // # Declaração Variaveis para Formulário FrmAlteraCertificadoTransacao (JFC) #
        // ############################################################################
        public static string certArtigo;

        public static string certDocumento;
        public static string certLote;
        public static string certArmazem;
        public static string certCertificadoTransacao;
        public static string certCertificadoTransacao2;
        public static string certCertificadoTransacao3;
        public static double certQtdTransacao;
        public static double certQtdTransacao2;
        public static double certQtdTransacao3;
        public static DateTime certDataCertificado;
        public static object certIDlinha;
        public static int certProgramLabel;
        public static bool certEmitido;
        public static bool certEmitir;
        public static string certDescricao;
        public static string certObs;
        public static bool certBCI;
        public static bool certBCIEmitido;
        // ############################################################################
        // # Declaração Variaveis para Formulário FrmAlteraCertificadoTransacao (JFC) #
        // ############################################################################

        // Acrescentado dia 27/01/2021 - Bruno
        public static string certCancelado;

        // Utilizado no frmFornecedoresCerts JFC 04/11/2019
        public static string certEntidade;

        public static string EntidadeVerifica;
        public static string ArtigoVerifica;
        public static string LoteVerifica;
        public static string ArmazemVerifica;
        public static double PrecoUnitVerifica;

        private const int TipoEmpresa = 0;

        public static ErpBS emp = new ErpBS();

        public static bool AbreEmpresa(string Empresa)
        {
            try
            {
                emp.AbreEmpresaTrabalho(TipoEmpresa, Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void FechaEmpresa()
        {
            emp.FechaEmpresaTrabalho();
        }

        public static string ArtigoEnc;
        public static string DescArtEnc;
        public static string LoteEnc;
        public static int LinhaEnc;

        // *******************************************************************************************************************************************
        // #### TARAS A DEVOLVER ####
        // *******************************************************************************************************************************************
        public static int ConesCartao;

        public static int ConesPlastico;
        public static int TubosCartao;
        public static int TubosPlastico;
        public static int PaletesMadeira;
        public static int PaletesPlastico;
        public static int SeparadoresCartao;
        public static int TotalTaras;

        public static bool Devolver_ConesCartao;
        public static bool Devolver_ConesPlastico;
        public static bool Devolver_TubosCartao;
        public static bool Devolver_TubosPlastico;
        public static bool Devolver_PaletesMadeira;
        public static bool Devolver_PaletesPlastico;
        public static bool Devolver_SeparadoresCartao;
        public static int TotalTaras_a_Devolver;
        // *******************************************************************************************************************************************
        // #### TARAS A DEVOLVER ####
        // *******************************************************************************************************************************************

        public static DateTime NovaDataVencimento(DateTime vDataDoc, string vCondPag, string vTipoEntidade, string vEntidade)
        {
            DateTime NovaDataVencimentoRet = default;
            DateTime vDataVenc;
            var DataDocRQRM = default(DateTime);
            if ((bool)PriV100Api.BSO.Base.CondsPagamento.Edita(vCondPag).CamposUtil["CDU_RM"].Valor == true)
            {
                vDataVenc = PriV100Api.BSO.Vendas.Documentos.CalculaDataVencimento(vDataDoc, vCondPag, PriV100Api.BSO.Base.CondsPagamento.Edita(vCondPag).DiasVencimento, vTipoEntidade, vEntidade);
                DataDocRQRM = Func_Ultimo_Dia_Mes(vDataVenc);
            }
            else if ((bool)PriV100Api.BSO.Base.CondsPagamento.Edita(vCondPag).CamposUtil["CDU_RQ"].Valor == true)
            {
                vDataVenc = PriV100Api.BSO.Vendas.Documentos.CalculaDataVencimento(vDataDoc, vCondPag, PriV100Api.BSO.Base.CondsPagamento.Edita(vCondPag).DiasVencimento, vTipoEntidade, vEntidade);
                if (DateAndTime.Day(vDataVenc) <= 15)
                {
                    DataDocRQRM = Convert.ToDateTime("15/" + DateAndTime.Month(vDataVenc) + "/" + DateAndTime.Year(vDataVenc));
                }
                else
                {
                    DataDocRQRM = Func_Ultimo_Dia_Mes(vDataVenc);
                }
            }

            NovaDataVencimentoRet = DataDocRQRM;
            return NovaDataVencimentoRet;
        }

        public static DateTime Func_Ultimo_Dia_Mes(DateTime paramDataX)
        {
            DateTime Func_Ultimo_Dia_MesRet = default;
            Func_Ultimo_Dia_MesRet = DateAndTime.DateAdd("m", 1d, DateAndTime.DateSerial(DateAndTime.Year(paramDataX), DateAndTime.Month(paramDataX), 1));
            Func_Ultimo_Dia_MesRet = DateAndTime.DateAdd("d", -1, Func_Ultimo_Dia_MesRet);
            return Func_Ultimo_Dia_MesRet;
        }

        public static T HandleNullValue<T>(object valor, T default_if_null)
        {
            T valor_output;

            if (valor == null || Information.IsDBNull(valor))
                return default_if_null;

            // # se o tipo passado for o tipo de dados de output, não haverá problema!
            // # contudo, se não for irá disparar a excepção e aí considera o valor default!
            try
            {
                valor_output = (T)valor;
            }
            catch (Exception ex)
            {
                valor_output = default_if_null;
            }

            return valor_output;
        }

        public static T DaValor<T>(this StdBE100.StdBELista lista, String campo)
        {
            if (TemValor<T>(lista, campo, out T valor))
                return valor;
            return default(T);
        }

        public static bool TemValor<T>(this StdBE100.StdBELista lista, String campo)
        {
            return TemValor<T>(lista, campo, out T valor);
        }

        public static bool TemValor<T>(this StdBE100.StdBELista lista, String campo, out T valor)
        {
            valor = default(T);

            if (lista is null)
                return false;

            if (lista.Valor(campo) == System.DBNull.Value)
                return false;

            if (lista.Valor(campo) is null)
                return false;

            valor = (T)lista.Valor(campo);

            return true;
        }

        public static double QuantidadeEnc;
        public static double QtReservadaEnc;
        public static double QtSatisfeitaEnc;
        public static double PrecoEnc;
        public static double NovaQuantidadeEnc;
        public static double NovaQtReservadaEnc;
        public static double NovoPrecoEnc;
        public static object IdLinhaEnc;
        public static int Opcao;
        public static string ObsEnc;

    }
}