﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vimaponto.Componentes.Sistema;

namespace Vimaponto.PrimaveraV100.Clientes.Filopa
{
    public class DsFacade : SisDsServicoDados
    {
        public override int NivelBaseDados { get { return 12; } }

        public override Assembly AssemblyComScripts { get { return Assembly.GetExecutingAssembly(); } }

        public override string IdentificadorPacote { get { return "Filopa"; } }

        public DsFacade(ref SisNeContexto contexto) : base(ref contexto)
        {

        }
    }
}
