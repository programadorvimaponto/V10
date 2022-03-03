using CblBE100;
using GrupoMundifios.Formulários;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using StdBE100;
using System;
using System.Windows;
using Vimaponto.PrimaveraV100;

namespace Generico
{
    internal class ImportaDocCBL
    {

            private static ErpBS100.ErpBS ObjEmpresaOrigem = new ErpBS100.ErpBS();
            private static bool ErroCriaDocCBL;
            private static int NumDocCBLIntegrados;

            public static void AbreFrmImportaDocCBL()
            {
                ExtensibilityResult result = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(typeof(FrmImportaDocCBLView));
                FrmImportaDocCBLView frm = result.Result;
                frm.ShowDialog();
            }

            private static bool AbreEmpresa(string Empresa)
            {
                try
                {
                    ObjEmpresaOrigem.AbreEmpresaTrabalho(ObjEmpresaOrigem.Contexto.TipoPlataforma,Empresa, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro ao aceder à empresa origem selecionada. \nExceção: "+ex.ToString(), "Atenção!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }


        public static void Movimentos(string pEmpresa, int pAno, int pPeriodoInicial, int pPeriodoFinal)
        {
            int p;
            string numeradorStr="";
            // Iniciar a variavel a falso quando inicia nova importação. JFC - 11/09/2020
            ErroCriaDocCBL = false;
            //FrmImportaDocCBL.LabelEstado.Caption = "A validar Empresa...";
            //DoEvents();
            if (AbreEmpresa(pEmpresa) == true)
            {
                //FrmImportaDocCBL.LabelEstado.Caption = "A validar Configurações...";
                //DoEvents();
                if (ValidaParametros(pEmpresa, pAno) == true)
                {

                    // JFC 11/05/2020 - Elimina registos já integrados, para garantir novas integrações e atualizações.
                    //FrmImportaDocCBL.LabelEstado.Caption = "A eliminar integrações anteriores...";
                    //DoEvents();
                    PriV100Api.BSO.DSO.ExecuteSQL("delete from Movimentos where Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and Ano='" + pAno + "' and Mes between '" + pPeriodoInicial + "' and '" + pPeriodoFinal + "'");
                    PriV100Api.BSO.DSO.ExecuteSQL("delete from CblDocRascEfectivos where IDDoc in (select Id from CabecMovCBL where Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and Ano='" + pAno + "' and Mes between '" + pPeriodoInicial + "' and '" + pPeriodoFinal + "' )");
                    PriV100Api.BSO.DSO.ExecuteSQL("delete from CabecMovCBL where Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and Ano='" + pAno + "' and Mes between '" + pPeriodoInicial + "' and '" + pPeriodoFinal + "'");

                    // JFC 20/05/2020 - Zerar o numerador dos diários. Caso contrário o motor vai integrar o NumDiario igual ao Numerador.
                    for (p = pPeriodoInicial; p <= pPeriodoFinal; p++)
                    {
                        switch (p)
                        {
                            case 1:
                                {
                                    numeradorStr = "update n set n.Numerador01='10000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 2:
                                {
                                    numeradorStr = "update n set n.Numerador02='20000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 3:
                                {
                                    numeradorStr = "update n set n.Numerador03='30000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 4:
                                {
                                    numeradorStr = "update n set n.Numerador04='40000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 5:
                                {
                                    numeradorStr = "update n set n.Numerador05='50000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 6:
                                {
                                    numeradorStr = "update n set n.Numerador06='60000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 7:
                                {
                                    numeradorStr = "update n set n.Numerador07='70000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 8:
                                {
                                    numeradorStr = "update n set n.Numerador08='80000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 9:
                                {
                                    numeradorStr = "update n set n.Numerador09='90000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 10:
                                {
                                    numeradorStr = "update n set n.Numerador10='100000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 11:
                                {
                                    numeradorStr = "update n set n.Numerador11='110000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }

                            case 12:
                                {
                                    numeradorStr = "update n set n.Numerador12='120000' from NumeradoresDiarios n where n.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and n.Ano='" + pAno + "'";
                                    break;
                                }
                        }

                        PriV100Api.BSO.DSO.ExecuteSQL(numeradorStr);
                    }

                    NumDocCBLIntegrados = 0;

                    //FrmImportaDocCBL.LabelEstado.Caption = "A Iniciar processo de integração...";
                    //DoEvents();
                    IntegraDocCBL(pEmpresa, pAno, pPeriodoInicial, pPeriodoFinal);
                    if (ErroCriaDocCBL == false)
                    {
                        if (NumDocCBLIntegrados == 0)
                            MessageBox.Show("Não existem documentos a integrar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        else
                        {
                            // JFC 20/05/2020 - Atualiza a data de criação para fim do proximo mês da datadoc
                            PriV100Api.BSO.DSO.ExecuteSQL("update c set c.DataCriacao=eomonth(c.DataDoc,1) from CabecMovCBL c where c.Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and c.Ano='" + pAno + "' and c.Mes between '" + pPeriodoInicial + "' and '" + pPeriodoFinal + "'");
                            // JFC 20/05/2020 - Insere centro de Custo.
                            PriV100Api.BSO.DSO.ExecuteSQL("insert into Movimentos (Conta, ContaOrigem, ValorAlt, Mes, Dia, Diario, NumDiario, Documento, NumDoc, Lote, Descricao, Valor, Natureza, TipoConta, Modulo, Moeda, Cambio, Ano, Serie, Linha, Pendente, IdCabec, ValorOrigem, CambioOrigem,CambioMAlt, TipoLancamento)   select '003', Conta, ValorAlt, Mes, Dia, Diario, NumDiario, Documento, NumDoc, Lote, Descricao, Valor,  Natureza, 'O', Modulo, Moeda, Cambio, Ano, Serie, Linha, Pendente, IdCabec, ValorOrigem, CambioOrigem,  CambioMAlt, TipoLancamento   from Movimentos where Diario in (select CDU_DiarioDestino from TDU_CBLImportaMovEmpresaDiario where CDU_Empresa='" + pEmpresa + "') and Ano='" + pAno + "' and Mes between '" + pPeriodoInicial + "' and '" + pPeriodoFinal + "'");
                            MessageBox.Show("Foram integrados " + NumDocCBLIntegrados + " documento(s).", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        }
                    }

                    //FrmImportaDocCBL.LabelEstado.Caption = "";
                    ObjEmpresaOrigem.FechaEmpresaTrabalho();
                }
            }
        }

        private static void IntegraDocCBL(string pEmpresa, int pAno, int pPeriodoInicial, int pPeriodoFinal)
        {
            StdBELista ListaDiarios;
            string SqlStringDiarios;
            StdBELista ListaDiarioMov;
            string SqlStringDiarioMov;
            int i, k;

            SqlStringDiarios = "SELECT ED.CDU_DiarioOrigem, ED.CDU_DiarioDestino FROM TDU_CBLImportaMovEmpresaDiario ED WHERE ED.CDU_Empresa = '" + pEmpresa + "' ORDER BY ED.CDU_DiarioOrigem";


            ListaDiarios = PriV100Api.BSO.Consulta(SqlStringDiarios);


            if (ListaDiarios.Vazia() == false)
            {
                ListaDiarios.Inicio();

                for (i = 1; i <= ListaDiarios.NumLinhas(); i++)
                {
                    SqlStringDiarioMov = "SELECT C.Ano, C.Diario, C.NumDiario, C.TipoLancamento FROM PRI" + pEmpresa + ".dbo.CabecMovCBL C WHERE C.Diario = '" + ListaDiarios.Valor("CDU_DiarioOrigem") + "' AND C.Ano = " + pAno + " AND C.Mes >= " + pPeriodoInicial + " AND C.Mes <= " + pPeriodoFinal + " ORDER BY C.Diario, C.Ano, C.Mes, C.NumDiario";

                    ListaDiarioMov = PriV100Api.BSO.Consulta(SqlStringDiarioMov);


                    if (ListaDiarioMov.Vazia() == false)
                    {
                        ListaDiarioMov.Inicio();

                        for (k = 1; k <= ListaDiarioMov.NumLinhas(); k++)
                        {
                            //FrmImportaDocCBL.LabelEstado.Caption = "A integrar Diário: " + ListaDiarios("CDU_DiarioDestino") + " Nº: " + ListaDiarioMov("NumDiario");
                            //DoEvents();
                            if (CriaDocCBL(ListaDiarios.Valor("CDU_DiarioDestino"), pEmpresa, ListaDiarioMov.Valor("Ano"), ListaDiarioMov.Valor("Diario"), ListaDiarioMov.Valor("NumDiario"), ListaDiarioMov.Valor("TipoLancamento")) == true)
                                // JFC 11/05/2020 - Atualiza a data de criação para fim do proximo mês da datadoc
                                // BSO.DSO.BDAPL.Execute ("update c set c.DataCriacao=eomonth(c.DataDoc,1) from CabecMovCBL c where c.Ano='" & ListaDiarioMov("Ano") & "' and c.Diario='" & ListaDiarios("CDU_DiarioDestino") & "' and c.NumDiario ='" & ListaDiarioMov("NumDiario") & "'")
                                // JFC 18/05/2020 - Insere centro de Custo.
                                // BSO.DSO.BDAPL.Execute ("insert into Movimentos (Conta, ContaOrigem, ValorAlt, Mes, Dia, Diario, NumDiario, Documento, NumDoc, Lote, Descricao, Valor, Natureza, TipoConta, Modulo, Moeda, Cambio, Ano, Serie, Linha, Pendente, IdCabec, ValorOrigem, CambioOrigem,CambioMAlt, TipoLancamento)   select '003', Conta, ValorAlt, Mes, Dia, Diario, NumDiario, Documento, NumDoc, Lote, Descricao, Valor,  Natureza, 'O', Modulo, Moeda, Cambio, Ano, Serie, Linha, Pendente, IdCabec, ValorOrigem, CambioOrigem,  CambioMAlt, TipoLancamento   from Movimentos where Ano='" & ListaDiarioMov("Ano") & "' and Diario='" & ListaDiarios("CDU_DiarioDestino") & "' and NumDiario ='" & ListaDiarioMov("NumDiario") & "'")
                                // Atualizacão da Data Criação e Centro de Custo comentado, pois o NumDiario não é fiavel para comparação. JFC 20/05/2020
                                ListaDiarioMov.Seguinte();
                            else
                                return;
                        }
                    }

                    ListaDiarios.Seguinte();
                }
            }
        }

        public static bool CriaDocCBL(string pDiarioDestino, string pEmpresa, int pAno, string pDiario, int pNumDiario, string pTipoLancamento)
        {
            CblBEDocumento DocOrigem=new CblBEDocumento();
            CblBEDocumento DocCBL= new CblBEDocumento();
            CblBELinhaDocGeral LinhaDocCBL = new CblBELinhaDocGeral();

            string TextoErro = "";
            string TipoEntidadeContaCBL = "";
            string EntidadeContaCBL = "";
            string TipoEntidadeDestinoContaCBL = "";
            string EntidadeContaDestinoCBL = "";
            try
            {
                // Verifica se o documento já existe registado
                if (PriV100Api.BSO.Contabilidade.Documentos.Existe(pAno, pDiarioDestino, pNumDiario, ref pTipoLancamento) == false)
                {

                    int l;


                    //'Carrega documento da base de dados origem
                    DocOrigem = ObjEmpresaOrigem.Contabilidade.Documentos.Edita(pAno, pDiario, pNumDiario, ref pTipoLancamento);




                    //'Cria documento na base de dados destino no módulo "L - Contabilidade" no diário de destino associado
                    DocCBL = new CblBEDocumento();


                    {
                        var withBlock = DocCBL;
                        withBlock.Ano = DocOrigem.Ano;
                        withBlock.DataDoc = DocOrigem.DataDoc;
                        withBlock.DataExpedicao = DocOrigem.DataExpedicao;
                        withBlock.DataOperacao = DocOrigem.DataOperacao;
                        withBlock.DataRecepcao = DocOrigem.DataRecepcao;
                        withBlock.Descricao = DocOrigem.Descricao;
                        withBlock.Dia = DocOrigem.Dia;
                        withBlock.Diario = pDiarioDestino;
                        withBlock.Doc = DocOrigem.Doc;
                        withBlock.Mes = DocOrigem.Mes;
                        withBlock.MesContab = DocOrigem.MesContab;
                        withBlock.Modulo = "L";
                        withBlock.Moeda = DocOrigem.Moeda;
                        withBlock.NaturezaOperacao = DocOrigem.NaturezaOperacao;
                        withBlock.NumDiario = DocOrigem.NumDiario;
                        withBlock.NumDoc = DocOrigem.NumDoc;
                        withBlock.NumeroDocExterno = DocOrigem.NumeroDocExterno;
                        withBlock.Observacoes = DocOrigem.Observacoes;
                        withBlock.Recolha = DocOrigem.Recolha;
                        withBlock.Serie = DocOrigem.Serie;
                        withBlock.TipoLancamento = DocOrigem.TipoLancamento;
                        withBlock.Utilizador = PriV100Api.BSO.Contexto.UtilizadorActual;
                        withBlock.TipoOperacaoPendente = DocOrigem.TipoOperacaoPendente;
                    }

                    TextoErro = "Doc. Origem: " + DocOrigem.Doc + " " + DocOrigem.Serie + "/" + DocOrigem.NumDoc + " Entidade: " + DocOrigem.Terceiro + " " + DocOrigem.NomeFiscalEntidade;

                    for (l = 1; l <= DocOrigem.LinhasGeral.NumItens; l++)
                    {
                        if (DocOrigem.LinhasGeral.GetEdita(l).TipoLinha == "F")
                        {


                            LinhaDocCBL = new CblBELinhaDocGeral();


                            {
                                var withBlock = LinhaDocCBL;
                                withBlock.AnoRecolha = DocOrigem.LinhasGeral.GetEdita(l).AnoRecolha;
                                withBlock.Cambio = DocOrigem.LinhasGeral.GetEdita(l).Cambio;
                                withBlock.CambioMAlt = DocOrigem.LinhasGeral.GetEdita(l).CambioMAlt;
                                withBlock.CambioOrigem = DocOrigem.LinhasGeral.GetEdita(l).CambioOrigem;
                                withBlock.Conta = DaContaDestino(pEmpresa, DocOrigem.LinhasGeral.GetEdita(l).Conta);
                                if (withBlock.Conta == "")
                                {
                                    TextoErro = TextoErro + Constants.vbNewLine + "Verificar configurações na tabela TDU_CBLImportaMovEmpresaDiario." + Constants.vbNewLine + "Não existe configuração da conta " + DocOrigem.LinhasGeral.GetEdita(l).Conta + ".";
                                    throw new Exception();
                                }
                                else if (PriV100Api.BSO.Contabilidade.PlanoContas.Existe(DocCBL.Ano, withBlock.Conta) == false)
                                {
                                    TextoErro = TextoErro + Constants.vbNewLine + "Verificar configurações na tabela TDU_CBLImportaMovEmpresaDiario." + Constants.vbNewLine + "A conta " + withBlock.Conta + " associada à conta " + DocOrigem.LinhasGeral.GetEdita(l).Conta + " não existe no plano de contas da " + PriV100Api.BSO.Contexto.CodEmp + ".";
                                    throw new Exception();
                                }
                                TipoEntidadeContaCBL = PriV100Api.BSO.Contabilidade.PlanoContas.DaValorAtributo(DocCBL.Ano, withBlock.Conta, "TipoEntidade");
                                EntidadeContaCBL = PriV100Api.BSO.Contabilidade.PlanoContas.DaValorAtributo(DocCBL.Ano, withBlock.Conta, "Entidade");
                                withBlock.Descricao = DocOrigem.LinhasGeral.GetEdita(l).Descricao;
                                withBlock.TipoEntidade = DocOrigem.LinhasGeral.GetEdita(l).TipoEntidade;
                                if (withBlock.TipoEntidade != "")
                                {
                                    withBlock.Entidade = DaEntidadeDestino(pEmpresa, DocOrigem.LinhasGeral.GetEdita(l).TipoEntidade, DocOrigem.LinhasGeral.GetEdita(l).Entidade);
                                    if (withBlock.Entidade == "")
                                    {
                                        TextoErro = TextoErro + Constants.vbNewLine + "Não foi possível determinar a associação entre a entidade origem e a entidade destino." + Constants.vbNewLine + "Entidade Origem: " + DocOrigem.LinhasGeral.GetEdita(l).Entidade + " Tipo Entidade: " + DocOrigem.LinhasGeral.GetEdita(l).TipoEntidade + Constants.vbNewLine + "Deve verificar se o campo de utilizador 'Entidade Interna' está corretamente preenchido na empresa " + pEmpresa + ".";
                                        throw new Exception();
                                    }
                                    if (TipoEntidadeDestinoContaCBL != "" & EntidadeContaDestinoCBL != "")
                                    {
                                        if (TipoEntidadeContaCBL == "" | EntidadeContaCBL == "")
                                        {
                                            TextoErro = TextoErro + Constants.vbNewLine + "A conta " + withBlock.Conta + " na empresa " + PriV100Api.BSO.Contexto.CodEmp + " não tem configurado o tipo ou entidade." + Constants.vbNewLine + "Deve definir na configuração da conta no separador 'Integração com Logística' o 'Tipo' e 'Entidade'.";
                                            throw new Exception();
                                        }
                                        if (TipoEntidadeContaCBL != withBlock.TipoEntidade | EntidadeContaCBL != withBlock.Entidade)
                                        {
                                            TextoErro = TextoErro + Constants.vbNewLine + "A conta " + withBlock.Conta + " na empresa " + PriV100Api.BSO.Contexto.CodEmp + " tem configurado o tipo entidade e/ou entidade diferente da conta origem." + Constants.vbNewLine + "Conta Destino: " + withBlock.Conta + " Tipo Entidade: " + TipoEntidadeContaCBL + " Entidade: " + EntidadeContaCBL + Constants.vbNewLine + "Conta Origem: " + DocOrigem.LinhasGeral.GetEdita(l).Conta + " Tipo Entidade: " + DocOrigem.LinhasGeral.GetEdita(l).TipoEntidade + " Entidade Interna: " + withBlock.Entidade + Constants.vbNewLine + "Deve corrigir na configuração da conta no separador 'Integração com Logística' o 'Tipo' e/ou 'Entidade'.";
                                            throw new Exception();
                                        }
                                    }
                                    withBlock.EntidadeNIF = DocOrigem.LinhasGeral.GetEdita(l).EntidadeNIF;
                                    withBlock.EntidadeNomeFiscal = DocOrigem.LinhasGeral.GetEdita(l).EntidadeNomeFiscal;
                                    withBlock.EntidadePais = DocOrigem.LinhasGeral.GetEdita(l).EntidadePais;
                                    // .Recapitulativo = DocOrigem.LinhasGeral(l).Recapitulativo
                                    withBlock.Recapitulativo = false;
                                }
                                withBlock.LocalOperacao = DocOrigem.LinhasGeral.GetEdita(l).LocalOperacao;
                                withBlock.MesRecolha = DocOrigem.LinhasGeral.GetEdita(l).MesRecolha;
                                withBlock.Moeda = DocOrigem.LinhasGeral.GetEdita(l).Moeda;
                                withBlock.Natureza = DocOrigem.LinhasGeral.GetEdita(l).Natureza;
                                withBlock.PercNDedutivel = DocOrigem.LinhasGeral.GetEdita(l).PercNDedutivel;
                                withBlock.RecolhaTerc = DocOrigem.LinhasGeral.GetEdita(l).RecolhaTerc;
                                withBlock.TaxaIva = DocOrigem.LinhasGeral.GetEdita(l).TaxaIva;
                                withBlock.TipoTerceiro = DocOrigem.LinhasGeral.GetEdita(l).TipoTerceiro;
                                if (withBlock.TipoTerceiro != "")
                                {
                                    withBlock.Terceiro = DaEntidadeDestino(pEmpresa, DocOrigem.LinhasGeral.GetEdita(l).TipoTerceiro, DocOrigem.LinhasGeral.GetEdita(l).Terceiro);
                                    if (withBlock.Terceiro == "")
                                    {
                                        TextoErro = TextoErro + Constants.vbNewLine + "Não foi possível determinar a associação entre a entidade origem e a entidade destino." + Constants.vbNewLine + "Entidade Origem: " + DocOrigem.LinhasGeral.GetEdita(l).Terceiro + " Tipo Entidade: " + DocOrigem.LinhasGeral.GetEdita(l).TipoTerceiro + Constants.vbNewLine + "Deve verificar se o campo de utilizador 'Entidade Interna' está corretamente preenchido na empresa " + pEmpresa + ".";
                                        throw new Exception();
                                    }
                                    if (TipoEntidadeContaCBL == "" | EntidadeContaCBL == "")
                                    {
                                        TextoErro = TextoErro + Constants.vbNewLine + "A conta " + withBlock.Conta + " na empresa " + PriV100Api.BSO.Contexto.CodEmp + " não tem configurado o tipo ou entidade." + Constants.vbNewLine + "Deve definir na configuração da conta no separador 'Integração com Logística' o 'Tipo' e 'Entidade'.";
                                        throw new Exception();
                                    }
                                    if (TipoEntidadeContaCBL != withBlock.TipoEntidade | EntidadeContaCBL != withBlock.Entidade)
                                    {
                                        TextoErro = TextoErro + Constants.vbNewLine + "A conta " + withBlock.Conta + " na empresa " + PriV100Api.BSO.Contexto.CodEmp + " tem configurado o tipo entidade e/ou entidade diferente da conta origem." + Constants.vbNewLine + "Conta Destino: " + withBlock.Conta + " Tipo Entidade: " + TipoEntidadeContaCBL + " Entidade: " + EntidadeContaCBL + Constants.vbNewLine + "Conta Origem: " + DocOrigem.LinhasGeral.GetEdita(l).Conta + " Tipo Entidade: " + DocOrigem.LinhasGeral.GetEdita(l).TipoTerceiro + " Entidade Interna: " + withBlock.Terceiro + Constants.vbNewLine + "Deve corrigir na configuração da conta no separador 'Integração com Logística' o 'Tipo' e/ou 'Entidade'.";
                                        throw new Exception();
                                    }
                                    withBlock.TerceiroNIF = DocOrigem.LinhasGeral.GetEdita(l).TerceiroNIF;
                                    withBlock.TerceiroNomeFiscal = DocOrigem.LinhasGeral.GetEdita(l).TerceiroNomeFiscal;
                                    withBlock.TerceiroPais = DocOrigem.LinhasGeral.GetEdita(l).TerceiroPais;
                                    // .Recapitulativo = DocOrigem.LinhasGeral(l).Recapitulativo
                                    withBlock.Recapitulativo = false;
                                }
                                withBlock.TipoLancamento = DocOrigem.LinhasGeral.GetEdita(l).TipoLancamento;
                                withBlock.TipoLinha = DocOrigem.LinhasGeral.GetEdita(l).TipoLinha;
                                withBlock.Valor = DocOrigem.LinhasGeral.GetEdita(l).Valor;
                                withBlock.ValorAlt = DocOrigem.LinhasGeral.GetEdita(l).ValorAlt;
                                withBlock.ValorIncIVA = DocOrigem.LinhasGeral.GetEdita(l).ValorIncIVA;
                                withBlock.ValorIncIVAAlt = DocOrigem.LinhasGeral.GetEdita(l).ValorIncIVAAlt;
                                withBlock.ValorIncIVAOrigem = DocOrigem.LinhasGeral.GetEdita(l).ValorIncIVAOrigem;
                                withBlock.ValorIVANDedutivel = DocOrigem.LinhasGeral.GetEdita(l).ValorIVANDedutivel;
                                withBlock.ValorIVANDedutivelAlt = DocOrigem.LinhasGeral.GetEdita(l).ValorIVANDedutivelAlt;
                                withBlock.ValorIVANDedutivelOrigem = DocOrigem.LinhasGeral.GetEdita(l).ValorIVANDedutivelOrigem;
                                withBlock.ValorOrigem = DocOrigem.LinhasGeral.GetEdita(l).ValorOrigem;
                            }

                            DocCBL.LinhasGeral.Insere(LinhaDocCBL);


                            LinhaDocCBL = null;


                        }
                    }

                    PriV100Api.BSO.Contabilidade.Documentos.Actualiza(DocCBL);


                    DocCBL = null;

                    NumDocCBLIntegrados = NumDocCBLIntegrados + 1;
                }

                return true;
            }
            catch (Exception ex)
            {
                if (DocCBL == null)
                    MessageBox.Show("Ocorreram os seguintes erros: \n" + ex.ToString(), "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                MessageBox.Show("Ocorreram os seguintes erros ao gravar o documento (diário/nº)" + Constants.vbNewLine + DocCBL.Diario + "/" + DocCBL.NumDiario + "", TextoErro, MessageBoxButton.OK, MessageBoxImage.Error);


                LinhaDocCBL = null;


                DocCBL = null;

                ErroCriaDocCBL = true;
                //FrmImportaDocCBL.LabelEstado.Caption = "";
                return false;

            }
       
        }

        private static string DaContaDestino(string pEmpresa, string pContaOrigem)
        {
            StdBELista ListaConta;
            string SqlStringConta;

            SqlStringConta = "SELECT PAC.CDU_ContaDestino ContaDestino FROM TDU_CBLImportaMovEmpresa E INNER JOIN TDU_CBLPlanoAlternativo PAC ON PAC.CDU_PlanoAlt = E.CDU_PlanoAlt WHERE E.CDU_Empresa = '" + pEmpresa + "' AND PAC.CDU_ContaOrigem = '" + pContaOrigem + "'";


            ListaConta = PriV100Api.BSO.Consulta(SqlStringConta);


            if (ListaConta.Vazia() == false)
            {
                ListaConta.Inicio();

                return ListaConta.Valor("ContaDestino");
            }
            else
                return "";
        }

        private static string DaEntidadeDestino(string pEmpresa, string pTipoEntidadeOrigem, string pEntidadeOrigem)
        {
            StdBELista ListaEntidade;
            string SqlStringEntidade;

            if (pTipoEntidadeOrigem == "C")
                SqlStringEntidade = "SELECT C.Cliente Entidade FROM Clientes C INNER JOIN PRI" + pEmpresa + ".dbo.Clientes CO ON CO.CDU_EntidadeInterna = C.CDU_EntidadeInterna WHERE CO.Cliente = '" + pEntidadeOrigem + "'";
            else if (pTipoEntidadeOrigem == "F")
                SqlStringEntidade = "SELECT F.Fornecedor Entidade FROM Fornecedores F INNER JOIN PRI" + pEmpresa + ".dbo.Fornecedores FO ON FO.CDU_EntidadeInterna = F.CDU_EntidadeInterna  WHERE FO.Fornecedor = '" + pEntidadeOrigem + "'";
            else
                return "";


            ListaEntidade = PriV100Api.BSO.Consulta(SqlStringEntidade);

            if (ListaEntidade.Vazia() == false)
            {
                ListaEntidade.Inicio();

                return ListaEntidade.Valor("Entidade");
            }
            return "";
        }

        private static bool ValidaParametros(string pEmpresa, int pAno)
        {
            StdBELista ListaValidacoes;
            string SqlStringValidacoes = "";
            int k;
            string TextoErro = "";
            string TextoErroDetalhe = "";
            try
                {

                // Verifica na empresa tem configurado os diários
                SqlStringValidacoes = "SELECT E.CDU_Empresa Empresa, (SELECT COUNT(ED.CDU_DiarioOrigem) NumDiarios FROM TDU_CBLImportaMovEmpresaDiario ED WHERE ED.CDU_Empresa = E.CDU_Empresa) NumDiarios FROM TDU_CBLImportaMovEmpresa E WHERE E.CDU_Empresa = '" + pEmpresa + "'";


                ListaValidacoes = PriV100Api.BSO.Consulta(SqlStringValidacoes);


                if (ListaValidacoes.Vazia() == false)
                {
                    ListaValidacoes.Inicio();

                    if (ListaValidacoes.Valor("NumDiarios") == 0)
                    {
                        TextoErro = "Verificar configurações na tabela TDU_CBLImportaMovEmpresaDiario" + Constants.vbNewLine + "Na empresa " + pEmpresa + " não existe configuração dos diários.";

                        TextoErroDetalhe = "Necessário configurar a associação entre os diários da " + pEmpresa + " e os diários da " + PriV100Api.BSO.Contexto.CodEmp + ".";

                        throw new Exception();
                    }
                }

                // Verifica se os diários na empresa origem existem
                SqlStringValidacoes = "SELECT ED.CDU_DiarioOrigem Diario FROM TDU_CBLImportaMovEmpresaDiario ED WHERE ED.CDU_Empresa = '" + pEmpresa + "' AND ED.CDU_DiarioOrigem NOT IN (SELECT DO.Diario FROM PRI" + pEmpresa + ".dbo.Diarios DO)";


                ListaValidacoes = PriV100Api.BSO.Consulta(SqlStringValidacoes);


                if (ListaValidacoes.Vazia() == false)
                {
                    ListaValidacoes.Inicio();

                    TextoErro = "Verificar configurações na tabela TDU_CBLImportaMovEmpresaDiario" + Constants.vbNewLine + "Na empresa " + pEmpresa + " o(s) seguinte(s) diário(s) não existe(m):";

                    for (k = 1; k <= ListaValidacoes.NumLinhas(); k++)
                    {
                        if (TextoErroDetalhe == "")
                            TextoErroDetalhe = ListaValidacoes.Valor("Diario");
                        else
                            TextoErroDetalhe = TextoErroDetalhe + Constants.vbNewLine + ListaValidacoes.Valor("Diario");

                        ListaValidacoes.Seguinte();
                    }

                    throw new Exception();
                }

                // Verifica se os diários na empresa destino existem
                SqlStringValidacoes = "SELECT ED.CDU_DiarioDestino Diario FROM TDU_CBLImportaMovEmpresaDiario ED WHERE ED.CDU_Empresa = '" + pEmpresa + "' AND ED.CDU_DiarioDestino NOT IN (SELECT DD.Diario FROM Diarios DD)";


                ListaValidacoes = PriV100Api.BSO.Consulta(SqlStringValidacoes);


                if (ListaValidacoes.Vazia() == false)
                {
                    ListaValidacoes.Inicio();

                    TextoErro = "Verificar configurações na tabela TDU_CBLImportaMovEmpresaDiario" + Constants.vbNewLine + "Na empresa " + PriV100Api.BSO.Contexto.CodEmp + " o(s) seguinte(s) diário(s) não existe(m):";

                    for (k = 1; k <= ListaValidacoes.NumLinhas(); k++)
                    {
                        if (TextoErroDetalhe == "")
                            TextoErroDetalhe = ListaValidacoes.Valor("Diario");
                        else
                            TextoErroDetalhe = TextoErroDetalhe + Constants.vbNewLine + ListaValidacoes.Valor("Diario");

                        ListaValidacoes.Seguinte();
                    }

                    throw new Exception();
                }

                // Verifica se existem diários na empresa destino repetidos. Só pode existir um diário destino para cada diário de origem.
                SqlStringValidacoes = "SELECT ED.CDU_DiarioDestino DiarioDestino, COUNT(ED.CDU_DiarioDestino) DiarioRepetido FROM TDU_CBLImportaMovEmpresaDiario ED WHERE ED.CDU_Empresa = '" + pEmpresa + "' GROUP BY ED.CDU_DiarioDestino HAVING COUNT(ED.CDU_DiarioDestino) > 1 ORDER BY ED.CDU_DiarioDestino";


                ListaValidacoes = PriV100Api.BSO.Consulta(SqlStringValidacoes);


                if (ListaValidacoes.Vazia() == false)
                {
                    ListaValidacoes.Inicio();

                    TextoErro = "Verificar configurações na tabela TDU_CBLImportaMovEmpresaDiario" + Constants.vbNewLine + "Na empresa " + pEmpresa + " o(s) seguinte(s) diário(s) estão repetidos.";

                    for (k = 1; k <= ListaValidacoes.NumLinhas(); k++)
                    {
                        if (TextoErroDetalhe == "")
                            TextoErroDetalhe = ListaValidacoes.Valor("DiarioDestino");
                        else
                            TextoErroDetalhe = TextoErroDetalhe + Constants.vbNewLine + ListaValidacoes.Valor("DiarioDestino");

                        ListaValidacoes.Seguinte();
                    }

                    throw new Exception();
                }

                // Verifica se o plano alternativo indicado na empresa tem configurado as contas da contabilidade
                SqlStringValidacoes = "SELECT E.CDU_PlanoAlt PlanoAlt, (SELECT COUNT(PA.CDU_ContaOrigem) NumContas FROM TDU_CBLPlanoAlternativo PA WHERE PA.CDU_PlanoAlt = E.CDU_PlanoAlt) NumContas FROM TDU_CBLImportaMovEmpresa E WHERE E.CDU_Empresa = '" + pEmpresa + "'";


                ListaValidacoes = PriV100Api.BSO.Consulta(SqlStringValidacoes);


                if (ListaValidacoes.Vazia() == false)
                {
                    ListaValidacoes.Inicio();

                    if (ListaValidacoes.Valor("NumContas") == 0)
                    {
                        TextoErro = "Verificar configurações na tabela TDU_CBLPlanoAlternativo" + Constants.vbNewLine + "O plano alternativo " + ListaValidacoes.Valor("PlanoAlt") + " para a empresa " + pEmpresa + " não tem as contas da contabilidade configuradas.";

                        TextoErroDetalhe = "Deve configurar o plano alternativo associando as contas do plano de contas da " + pEmpresa + " às contas do plano de contas da " + PriV100Api.BSO.Contexto.CodEmp + ".";

                        throw new Exception(TextoErro, new Exception(TextoErroDetalhe));
                    }
                }

                // Verifica se as contas definidas no plano alternativo existem na empresa origem
                SqlStringValidacoes = "SELECT E.CDU_PlanoAlt PlanoAlt, PA.CDU_ContaOrigem ContaOrigem FROM TDU_CBLImportaMovEmpresa E INNER JOIN TDU_CBLPlanoAlternativo PA ON PA.CDU_PlanoAlt = E.CDU_PlanoAlt WHERE E.CDU_Empresa = '" + pEmpresa + "' AND PA.CDU_ContaOrigem NOT IN (SELECT PC.Conta FROM PRI" + pEmpresa + ".dbo.PlanoContas PC WHERE PC.Ano = " + pAno + ") ORDER BY PA.CDU_ContaOrigem";


                ListaValidacoes = PriV100Api.BSO.Consulta(SqlStringValidacoes);


                if (ListaValidacoes.Vazia() == false)
                {
                    ListaValidacoes.Inicio();

                    TextoErro = "Verificar configurações na tabela TDU_CBLPlanoAlternativo" + Constants.vbNewLine + "No plano alternativo " + ListaValidacoes.Valor("PlanoAlt") + " para a empresa " + pEmpresa + " a(s) seguinte(s) conta(s) não existe(m):";

                    for (k = 1; k <= ListaValidacoes.NumLinhas(); k++)
                    {
                        if (TextoErroDetalhe == "")
                            TextoErroDetalhe = ListaValidacoes.Valor("ContaOrigem");
                        else
                            TextoErroDetalhe = TextoErroDetalhe + Constants.vbNewLine + ListaValidacoes.Valor("ContaOrigem");

                        ListaValidacoes.Seguinte();
                    }

                    throw new Exception();
                }

                // Verifica se as contas definidas no plano alternativo existem na empresa destino
                SqlStringValidacoes = "SELECT E.CDU_PlanoAlt PlanoAlt, PA.CDU_ContaDestino ContaDestino FROM TDU_CBLImportaMovEmpresa E INNER JOIN TDU_CBLPlanoAlternativo PA ON PA.CDU_PlanoAlt = E.CDU_PlanoAlt WHERE E.CDU_Empresa = '" + pEmpresa + "' AND PA.CDU_ContaDestino NOT IN (SELECT PC.Conta FROM PlanoContas PC WHERE PC.Ano = " + pAno + ") ORDER BY PA.CDU_ContaDestino";


                ListaValidacoes = PriV100Api.BSO.Consulta(SqlStringValidacoes);


                if (ListaValidacoes.Vazia() == false)
                {
                    ListaValidacoes.Inicio();

                    TextoErro = "Verificar configurações na tabela TDU_CBLPlanoAlternativo" + Constants.vbNewLine + "No plano alternativo " + ListaValidacoes.Valor("PlanoAlt") + " para a empresa " + PriV100Api.BSO.Contexto.CodEmp + " a(s) seguinte(s) conta(s) não existe(m):";

                    for (k = 1; k <= ListaValidacoes.NumLinhas(); k++)
                    {
                        if (TextoErroDetalhe == "")
                            TextoErroDetalhe = ListaValidacoes.Valor("ContaDestino");
                        else
                            TextoErroDetalhe = TextoErroDetalhe + Constants.vbNewLine + ListaValidacoes.Valor("ContaDestino");

                        ListaValidacoes.Seguinte();
                    }

                    throw new Exception();
                }
                return true;
            }
            catch
            {
                PriV100Api.PSO.Dialogos.MostraMensagem(StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimplesOk, TextoErro, StdPlatBS100.StdBSTipos.IconId.PRI_Critico, TextoErroDetalhe,1, true);
                //FrmImportaDocCBL.LabelEstado.Caption = "";
                return false;
            }
        }
    }
    }