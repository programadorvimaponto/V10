using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.PayablesReceivables.Editors;
using System.Windows.Forms;

namespace CompraFio
{
    public class CctIsEditorPendentes : EditorPendentes
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("CompraFio") == 1)
            {
                // Codigo alterado a 22/03/2019 a pedido de Elisabet. Alteração para listar todos os documentos onde o contentor é referido e não apenas um - JFC
                long j;
                string msg;
                if (this.DocumentoPendente.Tipodoc == "FAF" | this.DocumentoPendente.Tipodoc == "FAI")
                {
                    for (var i = 1; i <= this.DocumentoPendente.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoPendente.Linhas.GetEdita(i).CamposUtil["CDU_NumContentor"].Valor + "" != "")
                        {
                            string SqlStringNumContentor;
                            SqlStringNumContentor = "SELECT dbo.Historico.Modulo, dbo.Historico.TipoEntidade, dbo.Historico.Entidade, dbo.Historico.TipoDoc, dbo.Historico.Serie, dbo.Historico.NumDoc, dbo.Historico.NumDocInt , dbo.Historico.DataDoc, dbo.LinhasPendentes.CDU_NumContentor "
                                        + "FROM dbo.LinhasPendentes INNER JOIN dbo.Historico ON dbo.LinhasPendentes.IdHistorico = dbo.Historico.Id "
                                        + "WHERE (dbo.Historico.Modulo = 'M') AND (dbo.Historico.TipoDoc in ('FAF','FAI','FAO','NCO','NCF', '" + this.DocumentoPendente.Tipodoc + "')) AND (dbo.LinhasPendentes.CDU_NumContentor = '" + this.DocumentoPendente.Linhas.GetEdita(i).CamposUtil["CDU_NumContentor"].Valor + "') AND (dbo.Historico.Id <> '" + this.DocumentoPendente.IDHistorico + "') "
                                        + "ORDER BY dbo.Historico.DataDoc DESC";

                            var ListaNumContentor = BSO.Consulta(SqlStringNumContentor);

                            if (ListaNumContentor.Vazia() == false)
                            {
                                msg = "Já existe um documento lançado com o mesmo Nº Contentor:" + Strings.Chr(13) + Strings.Chr(13);
                                ListaNumContentor.Inicio();

                                for (j = 1; j <= ListaNumContentor.NumLinhas(); j++)
                                {
                                    string NomeEntidade = "";

                                    if (ListaNumContentor.Valor("TipoEntidade") == "F")
                                        NomeEntidade = BSO.Base.Fornecedores.Edita(ListaNumContentor.Valor("Entidade")).Nome;
                                    else if (ListaNumContentor.Valor("TipoEntidade") == "R")
                                        NomeEntidade = BSO.Base.OutrosTerceiros.Edita(ListaNumContentor.Valor("Entidade")).Nome;
                                    msg = "Documento:      " + ListaNumContentor.Valor("TipoDoc") + " Nº " + ListaNumContentor.Valor("NumDocInt") + "/" + ListaNumContentor.Valor("Serie") + " de " + ListaNumContentor.Valor("DataDoc") + ", Nº externo: " + ListaNumContentor.Valor("NumDoc") + Strings.Chr(13) + "Entidade:            " + ListaNumContentor.Valor("Entidade") + " - " + NomeEntidade + Strings.Chr(13) + "Nº Contentor:   " + ListaNumContentor.Valor("CDU_NumContentor") + Strings.Chr(13) + Strings.Chr(13);
                                    ListaNumContentor.Seguinte();
                                }

                                if (MessageBox.Show(msg + "Deseja mesmo assim gravar o documento?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    Cancel = true;
                            }
                        }
                    }
                }
            }
        }
    }
}