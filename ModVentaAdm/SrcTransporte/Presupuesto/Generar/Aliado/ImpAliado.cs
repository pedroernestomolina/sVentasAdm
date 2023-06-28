using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Aliado
{
    public class ImpAliado: IAliado
    {
        private Lista.IAliadoLista _lista;
        private Utils.Busqueda.IComp _compBusqueda;
        private bool _itemSeleccionadoIsOk;
        private int _idAliadoSeleccionado;


        public BindingSource Source_GetData { get { return _lista.Source_GetData; } }
        public int CntItem_Get { get { return _lista.CntItem_Get; } }
        public Utils.Busqueda.IComp CompBusqueda { get { return _compBusqueda; } }
        public bool ItemSeleccionadoIsOk { get { return _itemSeleccionadoIsOk; } }
        public int AliadoSeleccionado_GetId { get { return _idAliadoSeleccionado; } }


        public ImpAliado()
        {
            _idAliadoSeleccionado = -1;
            _itemSeleccionadoIsOk = false;
            _lista = new Lista.ImpAliadoLista();
            _compBusqueda = new ImpBusqueda();
        }
        public void Inicializa()
        {
            _idAliadoSeleccionado = -1;
            _itemSeleccionadoIsOk = false;
            _lista.Inicializa();
            _compBusqueda.Inicializa();
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


        public void BuscarAliados()
        {
            if (!_compBusqueda.HayParametrosBusqueda) 
            {
                Helpers.Msg.Alerta("NO HAY PARAMETROS DE BUSQUEDA DEFINIDOS");
                return;
            }
            try
            {
                var r01 = Sistema.MyData.TransporteAliado_GetLista((OOB.Transporte.Aliado.Busqueda.Filtro)_compBusqueda.DataExportar());
                var _lst = new List<Lista.data>();
                foreach (var rg in r01.ListaD.OrderBy(o => o.nombreRazonSocial).ToList())
                {
                    var nr = new Lista.data()
                    {
                        codigo = rg.codigo,
                        desc = rg.nombreRazonSocial,
                        id = rg.id,
                        ciRif = rg.ciRif,
                    };
                    _lst.Add(nr);
                }
                _lista.setData(_lst);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        public void ItemSeleccionado()
        {
            _itemSeleccionadoIsOk = false;
            _idAliadoSeleccionado = -1;
            if (_lista.ItemActual != null) 
            {
                _idAliadoSeleccionado = _lista.ItemActual.id;
                _itemSeleccionadoIsOk = true;
            }
        }


        private bool CargarData()
        {
            try
            {
                _compBusqueda.CargarData();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}