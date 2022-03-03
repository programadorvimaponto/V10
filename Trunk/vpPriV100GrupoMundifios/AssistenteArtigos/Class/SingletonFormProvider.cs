using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class
{
    public class SingletonFormProvider
    {

        private static Dictionary<Type, Form> mTypeFormLookup = new Dictionary<Type, Form>();

        public static T GetInstance<T>(Form owner) where T : Form
        {
            return GetInstance<T>(owner, true, null/* TODO Change to default(_) if this is not a reference type */);
        }

        public static T GetInstance<T>(Form owner, bool IncludeInMDI) where T : Form
        {
            return GetInstance<T>(owner, IncludeInMDI, null/* TODO Change to default(_) if this is not a reference type */);
        }

        public static T GetNewInstance<T>(Form owner) where T : Form
        {
            return GetNewInstance<T>(owner, true, null/* TODO Change to default(_) if this is not a reference type */);
        }

        public static T GetNewInstance<T>(Form owner, bool IncludeInMDI) where T : Form
        {
            return GetNewInstance<T>(owner, IncludeInMDI, null/* TODO Change to default(_) if this is not a reference type */);
        }

        /// <summary>
        ///         ''' Close any active window of that type and returns a new one
        ///         ''' </summary>
        ///         ''' <typeparam name="T">Type of form</typeparam>
        ///         ''' <param name="owner">Current form</param>
        ///         ''' <param name="IncludeInMDI">If it is associated with MDI Parent. Must be false in case of ShowDialog</param>
        ///         ''' <param name="args">Arguments for construcor</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static T GetNewInstance<T>(Form owner, bool IncludeInMDI, params object[] args) where T : Form
        {
            if (mTypeFormLookup.ContainsKey(typeof(T)))
            {
                mTypeFormLookup[typeof(T)].Close();
                mTypeFormLookup.Remove(typeof(T));
            }
            return GetInstance<T>(owner, IncludeInMDI, args);
        }

        public static T GetInstance<T>(Form owner, bool IncludeInMDI, params object[] args) where T : Form
        {
            if (!mTypeFormLookup.ContainsKey(typeof(T)))
            {
                Form f = (Form)Activator.CreateInstance(typeof(T), args);
                mTypeFormLookup.Add(typeof(T), f);
                f.Owner = owner;
                f.FormClosed += new FormClosedEventHandler(remover);
            }
            return (T)mTypeFormLookup[typeof(T)];
        }

        private static void remover(object sender, FormClosedEventArgs e)
        {
            Form f = sender as Form;
            if (f == null)
                return;

            f.FormClosed -= new FormClosedEventHandler(remover);
            mTypeFormLookup.Remove(f.GetType());
        }

    }
}
