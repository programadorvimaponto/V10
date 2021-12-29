using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Vimaponto.PrimaveraV100.Clientes.Filopa.EditorVendasDetalhe.DataSets;

namespace EditorVendasDetalhe
{
    public class VndNsEditorCopiaLinhas : EditorCopiaLinhas
    {
        private DsEditorVendasDetalhe DsEditorVendasDetalhe = new DsEditorVendasDetalhe();

        public override void AntesDeCopiar(string ModuloOrigem, dynamic ObjectoOrigem, string ModuloDestino, dynamic ObjectoDestino, ref bool Cancel, ExtensibilityEventArgs e)
        {
            
            base.AntesDeCopiar(ModuloOrigem, (VndBE100.VndBEDocumentoVenda)ObjectoOrigem, ModuloDestino, (VndBE100.VndBEDocumentoVenda)ObjectoDestino, ref Cancel, e);

            // se o modulo de origem e destino não forem de Vendas e se objecto de origem não for EMB e destino não for PF não faz alterações
            if (DsEditorVendasDetalhe.ValidaCopiaLinhas(ModuloOrigem, ObjectoOrigem, ModuloDestino, ObjectoDestino))
                DsEditorVendasDetalhe.AlteraPrcUnitCopiaLinhas(ObjectoDestino, ref Cancel);

            if (ModuloOrigem == "V" & ModuloDestino == "V")
                DsEditorVendasDetalhe.AtualizaIvaNasLinhasDoc(ObjectoDestino);
        }
    }
}
