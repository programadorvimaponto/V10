using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class
{
    public abstract class ListParameter
    {

        private object _codigo;
        private object _descricao;
        private object _IdRegisto;

        public ListParameter( object Codigo,  object Descricao, object IdRegisto = null)
        {
            this._codigo = Codigo;
            this._descricao = Descricao;
            // If Id <> Nothing Then
            this._IdRegisto = IdRegisto;
        }

        public object Codigo
        {
            get
            {
                return GetString(_codigo);
            }
        }

        public object Descricao
        {
            get
            {
                return GetString(_descricao);
            }
        }

        public object Id
        {
            get
            {
                return GetString(_IdRegisto);
            }
        }


        public void SetData(string Codigo, string Descricao, string IdRegisto = null)
        {
            SetString(ref _codigo, Codigo);
            SetString(ref _descricao, Descricao);
            if (IdRegisto != "")
                SetString(ref _IdRegisto, IdRegisto);
        }

        

        protected abstract void SetString(ref object O, string Value);

        protected abstract string GetString(object O);

    }
}
