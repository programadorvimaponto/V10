using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System.Windows.Forms;

namespace ValidaStockGc
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("ValidaStockGc") == 1)
            {
                // JFC 31/01/2020 - Validação de Stock disponivel em Armazém. Reportado por Pedro Carteado, situação recorrente no mercado externo onde o armazém indicado na GC não está correcto.
                if (this.DocumentoVenda.Tipodoc == "GC")
                {
                    StdBELista listStk;
                    for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).IdLinhaOrigemCopia + "" != "")
                        {
                            StdBELista listEstadoECL;
                            listEstadoECL = BSO.Consulta("select * from linhasdoc ln inner join cabecdoc cd on cd.id=ln.idcabecdoc inner join cabecdocstatus cds on cds.Idcabecdoc=cd.id inner join linhasdocstatus lns on lns.IdLinhasDoc=ln.id where lns.EstadoTrans='P' and lns.Fechado='0' and cds.Fechado='0' and cds.Anulado='0' and cds.Estado='P' and ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinhaOrigemCopia + "'");
                            if (listEstadoECL.Vazia() == false)
                            {
                                if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                                {
                                    listStk = BSO.Consulta("select aa.StkActual from ArtigoArmazem aa where aa.StkActual>0 and aa.Artigo='" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' and aa.Lote='" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' and aa.Armazem='" + this.DocumentoVenda.Linhas.GetEdita(i).Armazem + "'");
                                    if (listStk.Vazia())
                                    {
                                        listStk = BSO.Consulta("select top 1 aa.Armazem, aa.StkActual from ArtigoArmazem aa where aa.StkActual>0 and aa.Artigo='" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' and aa.Lote='" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' order by aa.StkActual desc");
                                        if (listStk.Vazia())
                                            MessageBox.Show("Atenção Artigo/Lote sem stock: " + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + " - " + this.DocumentoVenda.Linhas.GetEdita(i).Lote, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        else
                                        {
                                            listStk.Inicio();

                                            if (MessageBox.Show("Atenção " + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + " - " + this.DocumentoVenda.Linhas.GetEdita(i).Lote + " sem stock no armazem " + this.DocumentoVenda.Linhas.GetEdita(i).Armazem + Strings.Chr(13) + "Artigo/Lote com " + listStk.Valor("StkActual") + "Kg no armazem " + listStk.Valor("Armazem") + Strings.Chr(13) + "Deseja atualizar a ECL e GC para o armazem " + listStk.Valor("Armazem") + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                            {
                                                this.DocumentoVenda.Linhas.GetEdita(i).Armazem = listStk.Valor("Armazem");
                                                this.DocumentoVenda.Linhas.GetEdita(i).Localizacao = listStk.Valor("Armazem");
                                                BSO.DSO.ExecuteSQL("update ln set ln.Armazem='" + listStk.Valor("Armazem") + "', ln.Localizacao='" + listStk.Valor("Armazem") + "' from LinhasDoc ln where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinhaOrigemCopia + "'");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}