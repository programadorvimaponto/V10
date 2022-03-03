using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class
{
    public partial class ListParamTextBox : ListParameter
    {

        public ListParamTextBox(DevExpress.XtraEditors.TextEdit Codigo, DevExpress.XtraEditors.TextEdit Descricao, object IdRegisto) : base(Codigo, Descricao, IdRegisto)
        {

        }

        protected override void SetString(ref object O, string Value)
        {
            if (O != null)
            {
                TextEdit txt = (TextEdit)O;
                string Tag = "";
                txt.Tag = "F4";
                txt.Text = Value.ToString();
                txt.Tag = Tag;
            }

            //throw new NotImplementedException();
        }

        protected override string GetString(object O)
        {
            if (O != null)
            {
                TextBox txt = (TextBox)O;
                return txt.Tag.ToString();
            }
            else return "";
            //throw new NotImplementedException();
        }
    }
}
