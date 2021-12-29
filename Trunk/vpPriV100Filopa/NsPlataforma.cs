using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Platform.Services;
using Vimaponto.Componentes.Sistema;

namespace Vimaponto.PrimaveraV100.Clientes.Filopa
{
    public class NsPlataforma : Plataforma
    {
        public override void DepoisDeAbrirEmpresa(ExtensibilityEventArgs e)
        {
            PriV100Api.AtualizaContexto(PSO, BSO, Aplicacao);

            AtualizaConnectionString(PriV100Api.VSO.Contexto);
            AtualizaSchemaBaseDados(PriV100Api.VSO.Contexto);
        }

        private void AtualizaConnectionString(SisNeContexto contexto)
        {
            Properties.Settings.Default["PRIFILOPAConnectionString"] = contexto.Pacotes[SisNeIdentificadorPacote.Vendas].ConnectionString;
            Properties.Settings.Default.Save();
        }

        private void AtualizaSchemaBaseDados(SisNeContexto contexto)
        {
            DsFacade filopaDs = new DsFacade(ref contexto);
        }
    }
}
