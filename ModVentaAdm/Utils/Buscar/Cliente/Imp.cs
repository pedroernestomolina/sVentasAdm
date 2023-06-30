using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.Buscar.Cliente
{
    public class Imp: ICliente
    {
        private Busqueda.IComp _comp;
        private IItem _items;


        public Busqueda.IComp CompBusqueda { get { return _comp; } }
        public IItem Items { get { return _items; } }
        public object ItemSeleccionadoGetId
        {
            get
            {
                var _it = "";
                if (_items.ItemActual != null) 
                {
                    _it =((dataCli)_items.ItemActual).auto;
                }
                return _it;
            }
        }


        public Imp()
        {
            _itemSeleccionadoIsOk = false;
            _comp = new ImpCompBusqueda();
            _items = new ImpItems();
        }


        public void Inicializa() 
        {
            _itemSeleccionadoIsOk = false;
            _comp.Inicializa();
            _items.Inicializa();
        }
        Frm frm;
        public void Inicia() 
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void Limpiar()
        {
            _comp.Limpiar();
            _items.Limpiar();
        }
        public void Buscar()
        {
            try
            {
                if (!_comp.HayParametrosBusqueda)
                {
                    throw new Exception("NO EXISTEN PARAMETROS DE BUSQUEDA");
                }
                var filtroOOB = (OOB.Maestro.Cliente.Lista.Filtro)_comp.DataExportar();
                var r01 = Sistema.MyData.Cliente_GetLista(filtroOOB);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                var _lst = new List<dataCli>();
                foreach (var rg in r01.ListaD.OrderBy(o=>o.razonSocial).ToList())
                {
                    var nr = new dataCli() { auto = rg.id, codigo = rg.codigo, cirif=rg.ciRif, nombre= rg.razonSocial, isActivo = rg.estatus.Trim().ToUpper() == "ACTIVO" };
                    _lst.Add(nr);
                }
                _items.setDataCargar(_lst);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return;
            }
        }


        private bool _itemSeleccionadoIsOk;
        public bool ItemSeleccionadoIsOk { get { return _itemSeleccionadoIsOk; } }
        public void ItemSeleccionado()
        {
            _itemSeleccionadoIsOk = false;
            if (_items.ItemActual != null) 
            {
                var _it = (dataCli)_items.ItemActual;
                if (!_it.isActivo) 
                {
                    Helpers.Msg.Alerta("VERIFICAR ESTATUS DEL CLIENTE");
                    return;
                }
                _itemSeleccionadoIsOk = true;
            }
        }


        private bool CargarData()
        {
            try
            {
                _comp.CargarData();
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                throw;
            }
        }
    }
}