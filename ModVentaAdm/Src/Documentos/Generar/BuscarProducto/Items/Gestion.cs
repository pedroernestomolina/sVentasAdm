using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.BuscarProducto.Items
{
    
    public class Gestion
    {

       
        private List<data> _lst;
        private BindingList<data> _bl;
        private BindingSource _bs;


        public BindingSource ItemSource { get { return _bs; } }
        public int Cnt { get { return _bs.Count; } }
        public data Item 
        {
            get 
            {
                if (_bs.Current == null) 
                {
                    return new data();
                }
                return (data)_bs.Current; 
            } 
        }
        public string Inf_Producto { get { return Item.Codigo + Environment.NewLine + Item.Descripcion; } }
        public decimal Inf_ExistenciaActual { get { return Item.ExActual; } }
        public decimal Inf_ExistenciaDisponible { get { return Item.ExDisponible; } }
        //
        public string Inf_EmpqCont_1 { get { return Item.EmpqCont_1; } }
        public decimal Inf_PNeto_1 { get { return Item.PNeto_1; } }
        public decimal Inf_PFull_1 { get { return Item.PFull_1; } }
        //
        public string Inf_EmpqCont_2 { get { return Item.EmpqCont_2; } }
        public decimal Inf_PNeto_2 { get { return Item.PNeto_2; } }
        public decimal Inf_PFull_2 { get { return Item.PFull_2; } }
        //
        public string Inf_EmpqCont_3 { get { return Item.EmpqCont_3; } }
        public decimal Inf_PNeto_3 { get { return Item.PNeto_3; } }
        public decimal Inf_PFull_3 { get { return Item.PFull_3; } }
        //
        public string Inf_EmpqCont_4 { get { return Item.EmpqCont_4; } }
        public decimal Inf_PNeto_4 { get { return Item.PNeto_4; } }
        public decimal Inf_PFull_4 { get { return Item.PFull_4; } }
        //
        public string Inf_EmpqCont_5 { get { return Item.EmpqCont_5; } }
        public decimal Inf_PNeto_5 { get { return Item.PNeto_5; } }
        public decimal Inf_PFull_5 { get { return Item.PFull_5; } }
        //
        public string Inf_EmpqCont_6 { get { return Item.EmpqCont_6; } }
        public decimal Inf_PNeto_6 { get { return Item.PNeto_6; } }
        public decimal Inf_PFull_6 { get { return Item.PFull_6; } }


        public Gestion()
        {
            _lst = new List<data>();
            _bl = new BindingList<data>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
        }


        public void Inicializa()
        {
            LimpiarLista();
        }

        internal void setLista(List<OOB.Producto.Lista.Ficha> list)
        {
            _lst.Clear();
            foreach (var it in list.OrderBy(o => o.Nombre).ToList())
            {
                _lst.Add(new data(it));
            }
            _bs.CurrencyManager.Refresh();
        }

        public void LimpiarLista()
        {
            _lst.Clear();
            _bs.CurrencyManager.Refresh();
        }

    }

}