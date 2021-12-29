using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;
using Vimaponto.Componentes.Sistema;

namespace Vimaponto.PrimaveraV100.Clientes.Inovafil
{
    public class NsPlataforma : Plataforma
    {
        public override void DepoisDeAbrirEmpresa(ExtensibilityEventArgs e)
        {
            PriV100Api.AtualizaContexto(PSO, BSO, Aplicacao);
            AtualizaSchemaBaseDados(PriV100Api.VSO.Contexto);
        }

        private void AtualizaSchemaBaseDados(SisNeContexto contexto)
        {
            DsFacade inovafilDs = new DsFacade(ref contexto);
        }
    }
}
