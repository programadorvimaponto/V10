using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;
using StdBE100;
using System;
using System.Windows.Forms;

namespace CondPagRQ
{
    public class PltNsEmpresas : Plataforma
    {
        public override void DepoisDeAbrirEmpresa(ExtensibilityEventArgs e)
        {
            base.DepoisDeAbrirEmpresa(e);

            
            if (Module1.VerificaToken("CondPagRQ") == 1 && System.Reflection.Assembly.GetExecutingAssembly().GetName().Name != "Plan")
            {
                StdBELista listadocs;

                if ((BSO.DSO.ExecuteSQL("SELECT * FROM [PRIEMPRE].[DBO].[MENSAGENSEMAIL] M WHERE M.ENVIADA = 0 AND M.DATA <= CONVERT(DATETIME,'" + Strings.Format(DateTime.Now, "yyyy-MM-dd") + "' ,102)") == 1))
                    MessageBox.Show("Existem mensagens de plafond ultrapassado não enviadas!" + Strings.Chr(13) + "Por favor verificar o serviço Primavera Windows Scheduler que não está a funcionar.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // *************************************************************************************************************************************************

                // Verifica se existem documentos de venda com condição de pagamento RQ ou RM onde a data de vencimento não foi recalculada
                // *************************************************************************************************************************************************
                var SqlStringDocs = "SELECT dbo.CabecDoc.Id, dbo.CabecDoc.Filial, dbo.CabecDoc.TipoDoc, dbo.CabecDoc.Serie, dbo.CabecDoc.NumDoc, dbo.CabecDoc.CondPag, dbo.CondPag.CDU_RQ, dbo.CondPag.CDU_RM, dbo.CabecDoc.CDU_AlteradaDataVenc "
                            + "FROM dbo.CabecDoc INNER JOIN dbo.CondPag ON dbo.CabecDoc.CondPag = dbo.CondPag.CondPag INNER JOIN dbo.DocumentosVenda ON dbo.CabecDoc.TipoDoc = dbo.DocumentosVenda.Documento "
                            + "WHERE (dbo.DocumentosVenda.TipoDocumento = 4) AND (dbo.CabecDoc.CDU_AlteradaDataVenc = 0) AND (dbo.CondPag.CDU_RQ = 1) AND (dbo.CabecDoc.Data <= CONVERT(DATETIME, GETDATE()-1, 102)) OR "
                            + "(dbo.DocumentosVenda.TipoDocumento = 4) AND (dbo.CabecDoc.CDU_AlteradaDataVenc = 0) AND (dbo.CondPag.CDU_RM = 1) AND (dbo.CabecDoc.Data <= CONVERT(DATETIME, GETDATE()-1, 102))";

                listadocs = BSO.Consulta(SqlStringDocs);

                if (listadocs.Vazia() == false)
                    MessageBox.Show("Existem documentos de venda com condição de pagamento resumo quinzenal ou resumo mensal onde a data de vencimento não foi corrigida." + Strings.Chr(13) + Strings.Chr(13) + "Deve por favor executar o quanto antes a função" + Strings.Chr(13) + "'Altera Data Vencimento RQ/RM'.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}