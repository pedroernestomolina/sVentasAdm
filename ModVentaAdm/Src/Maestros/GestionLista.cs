using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Maestros
{
    
    public class GestionLista: IGestionLista
    {

        private List<data> lLista;
        private BindingList<data> blLista;
        private BindingSource bsLista;


        public BindingSource Source { get { return bsLista; } }
        public data ItemActual { get { return (data)bsLista.Current; } }


        public GestionLista()
        {
            lLista = new List<data>();
            blLista = new BindingList<data>(lLista);
            bsLista = new BindingSource();
            bsLista.DataSource = blLista;
        }


        public int TotalItems
        {
            get { return blLista.Count; }
        }

        public void setLista(List<data> list)
        {
            blLista.Clear();
            foreach (var it in list.OrderBy(o => o.descripcion).ToList())
            {
                blLista.Add(it);
            }
            bsLista.CurrencyManager.Refresh();
        }

        public void Agregar(data dat)
        {
            blLista.Add(dat);
            var l = blLista.ToList();
            setLista(l);

            var ind = blLista.IndexOf(blLista.FirstOrDefault(f => f.id == dat.id));
            bsLista.Position = ind;
        }

        public void Actualizar(data data)
        {
            var it = blLista.FirstOrDefault(f => f.id == data.id);
            if (it != null)
            {
                blLista.Remove(it);
            }
            Agregar(data);
        }

    }

}