using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.BuscarProducto
{

    public class Gestion: IBuscarProducto
    {


        private Items.Gestion _items;
        private bool _seleccionatItemIsActivo;
        private Items.data _itemSeleccionado;
        private bool _itemSeleccionadoIsOk;
        private string _cadenaBusq;
        private string _metodoBusq;
        private string _depositoFiltrar;
        private decimal _factorDivisa;

        
        public int CntItem { get { return _items.Cnt; } }
        public BindingSource ItemsSource { get { return _items.ItemSource; } }
        public string IdItemSeleccionado { get { return _itemSeleccionado.Id; } }

        public bool ItemSeleccionadoIsOk { get { return _itemSeleccionadoIsOk; } }
        public string Inf_Producto { get { return _items.Inf_Producto; } }
        public decimal Inf_ExistenciaActual { get { return _items.Inf_ExistenciaActual; } }
        public decimal Inf_ExistenciaDisponible { get { return _items.Inf_ExistenciaDisponible; } }
        //
        public string Inf_EmpqCont_1 { get { return _items.Inf_EmpqCont_1; } }
        public decimal Inf_PNeto_1 { get { return _items.Inf_PNeto_1; } }
        public decimal Inf_PFull_1 { get { return _items.Inf_PFull_1; } }
        public decimal Inf_PDivisa_1 { get { return calculaPrecioDivisa(Inf_PFull_1); } }
        //
        public string Inf_EmpqCont_2 { get { return _items.Inf_EmpqCont_2; } }
        public decimal Inf_PNeto_2 { get { return _items.Inf_PNeto_2; } }
        public decimal Inf_PFull_2 { get { return _items.Inf_PFull_2; } }
        public decimal Inf_PDivisa_2 { get { return calculaPrecioDivisa(Inf_PFull_2); } }
        //        
        public string Inf_EmpqCont_3 { get { return _items.Inf_EmpqCont_3; } }
        public decimal Inf_PNeto_3 { get { return _items.Inf_PNeto_3; } }
        public decimal Inf_PFull_3 { get { return _items.Inf_PFull_3; } }
        public decimal Inf_PDivisa_3 { get { return calculaPrecioDivisa(Inf_PFull_3); } }
        //        
        public string Inf_EmpqCont_4 { get { return _items.Inf_EmpqCont_4; } }
        public decimal Inf_PNeto_4 { get { return _items.Inf_PNeto_4; } }
        public decimal Inf_PFull_4 { get { return _items.Inf_PFull_4; } }
        public decimal Inf_PDivisa_4 { get { return calculaPrecioDivisa(Inf_PFull_4); } }
        //        
        public string Inf_EmpqCont_5 { get { return _items.Inf_EmpqCont_5; } }
        public decimal Inf_PNeto_5 { get { return _items.Inf_PNeto_5; } }
        public decimal Inf_PFull_5 { get { return _items.Inf_PFull_5; } }
        public decimal Inf_PDivisa_5 { get { return calculaPrecioDivisa(Inf_PFull_5); } }
        //        
        public string Inf_EmpqCont_6 { get { return _items.Inf_EmpqCont_6; } }
        public decimal Inf_PNeto_6 { get { return _items.Inf_PNeto_6; } }
        public decimal Inf_PFull_6 { get { return _items.Inf_PFull_6; } }
        public decimal Inf_PDivisa_6 { get { return calculaPrecioDivisa(Inf_PFull_6); } }


        public Gestion()
        {
            _depositoFiltrar = "";
            _metodoBusq = "";
            _cadenaBusq = "";
            _itemSeleccionado = null;
            _items = new Items.Gestion();
            _seleccionatItemIsActivo = false;
            _itemSeleccionadoIsOk = false;
        }


        public void Inicializa() 
        {
            _factorDivisa = 0m;
            _depositoFiltrar = "";
            _metodoBusq="";
            _cadenaBusq = "";
            _itemSeleccionado = null;
            _itemSeleccionadoIsOk = false;
            _seleccionatItemIsActivo = false;
            _items.Inicializa();
        }

        BuscarProductoFrm _frm;
        public void Inicia() 
        {
            if (_frm == null)
            {
                _frm = new BuscarProductoFrm();
                _frm.setControlador(this);
            }
            _frm.ShowDialog();
        }

        public void SeleccionarItem()
        {
            if (_seleccionatItemIsActivo)
            {
                if (_items.Item.Id !="")
                {
                    if (!_items.Item.IsActivo) 
                    {
                        Helpers.Msg.Error("PRODUCTO SELECCIONADO EN ESTADO INACTIVO");
                        return;
                    }
                    _itemSeleccionado = _items.Item;
                    _itemSeleccionadoIsOk = true;
                }
            }
        }

        public void setActivarSeleccionItem(bool p)
        {
            _seleccionatItemIsActivo = p;
        }

        public void setCadenaBusq(string cad)
        {
            _cadenaBusq = cad;
        }

        public void Buscar()
        {
            _itemSeleccionado = null;
            _itemSeleccionadoIsOk = false;

            if (_cadenaBusq == "") 
            {
                return;
            }

            var mb = OOB.Producto.Lista.Enumerados.EnumMetodoBusqueda.SinDefinir;
            switch (_metodoBusq)
            {
                case "01":
                    mb = OOB.Producto.Lista.Enumerados.EnumMetodoBusqueda.PorCodigo;
                    break;
                case "02":
                    mb = OOB.Producto.Lista.Enumerados.EnumMetodoBusqueda.PorDescripcion;
                    break;
                case "03":
                    mb = OOB.Producto.Lista.Enumerados.EnumMetodoBusqueda.PorReferencia;
                    break;
            }
            var filtro = new OOB.Producto.Lista.Filtro();
            filtro.AutoDeposito = _depositoFiltrar;
            filtro.Cadena = _cadenaBusq;
            filtro.MetodoBusqueda = mb;
            var r01 = Sistema.MyData.Producto_GetLista(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            _items.setLista(r01.ListaD);

            Inicia();
        }

        public void setActivarBusPorCodigo()
        {
            _metodoBusq = "01";
        }

        public void setActivarBusPorDescripcion()
        {
            _metodoBusq = "02";
        }

        public void setActivarBusPorReferencia()
        {
            _metodoBusq = "03";
        }

        public void setDepositoBuscar(string id)
        {
            _depositoFiltrar= id;
        }

        public void setFactorCambio(decimal tasaDivisa)
        {
            _factorDivisa = tasaDivisa;
        }

        private decimal calculaPrecioDivisa(decimal p)
        {
            if (p != 0m)
                return p / _factorDivisa;
            else
                return 0m;
        }

    }

}