using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;

namespace Intrastat
{
    public class VndIsEditorVendas : EditorVendas
    {
        private int i;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("Intrastat") == 1)
            {
                if (BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.Tipodoc, "IntrastatDoc") == true)
                {
                    i = 0;

                    for (i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                    {
                        if (int.Parse(this.DocumentoVenda.Linhas.GetEdita(i).TipoLinha.ToString()) >= 10 & int.Parse(this.DocumentoVenda.Linhas.GetEdita(i).TipoLinha.ToString()) <= 29 & this.DocumentoVenda.Linhas.GetEdita(i).Unidade == "KG")
                        {
                            this.DocumentoVenda.Linhas.GetEdita(i).IntrastatRegiao = "80";
                            this.DocumentoVenda.Linhas.GetEdita(i).IntrastatMassaLiq = 1;
                        }
                    }
                }
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("Intrastat") == 1)
            {
                if (BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.Tipodoc, "IntrastatDoc") == true)
                    BSO.DSO.ExecuteSQL("UPDATE CABECDOC SET CDU_IntrastatNatA='1', CDU_INTRASTATNATB='1',CDU_IntrastatCondEnt='CIP',CDU_IntrastatModoTransp='3' WHERE ID='" + this.DocumentoVenda.ID + "'");
            }
        }
    }
}