using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Administrador.Handler
{
    public class Imp: Vistas.IAdm
    {
        private bool _abandonarIsOK;
        private Vistas.IListaAdm _lista;
        private Vistas.IBusqDocAdm _busqDoc;
        private SrcTransporte.Filtro.Vistas.IFiltro _ctrFiltro;


        public Utils.Componente.Administrador.Vistas.ILista data { get { return _lista; } }
        public Vistas.IBusqDocAdm BusqDoc { get { return _busqDoc; } }
        public string Get_TituloAdm { get { return "Administrador Documentos: ANTICIPOS DE CLIENTES"; } }
        public int Get_CntItem { get { return _lista.Get_CntItem; } }
        public Filtro.Vistas.IFiltro CtrlFiltro { get { return _ctrFiltro; } }
        

        public Imp()
        {
            _abandonarIsOK = false;
            _lista = new HndLista();
            _busqDoc = new HndBusqDoc();
            _ctrFiltro = new SrcTransporte.Filtro.Reportes.Imp();
            _ctrFiltro.setActivarFiltrosPor(new FiltrarPor());
        }
        public void Inicializa()
        {
            _abandonarIsOK = false;
            _lista.Inicializa();
            _busqDoc.Inicializa();
            _ctrFiltro.Inicializa();
        }
        Vistas.Frm frm;
        public void Inicia()
        {
            if (cargarData()) 
            {
                if (frm == null) 
                {
                    frm = new Vistas.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void FitrosBusqueda()
        {
            _ctrFiltro.Inicia();
        }
        public void FiltrosLimpiar()
        {
            _ctrFiltro.Limpiar();
        }
        public void Buscar()
        {
            if (_ctrFiltro.HndFiltro.VerificarFiltros())
            {
                _busqDoc.setFiltros(_ctrFiltro.HndFiltro.Get_Filtros);
                var r01 = _busqDoc.Buscar();
                if (r01 != null)
                {
                    var lst = new List<Vistas.IdataItem>();
                    foreach (var rg in r01)
                    {
                        var nr = new dataItem((OOB.Transporte.ClienteAnticipo.ListaMov.Ficha)rg);
                        lst.Add(nr);
                    }
                    _lista.setDataCargar(lst);
                }
            }
        }
        public void AnularItem()
        {
            if (_lista.ItemActual != null) 
            {
                var it = (dataItem)_lista.ItemActual;
                if (it.isAnulado) 
                {
                    Helpers.Msg.Alerta("ITEM YA SE ENCUENTRA ANULADO");
                    return;
                }
                anulaItem(it);
                _lista.Refresca();
            }
        }
        public void VisualizarDocumento()
        {
            if (_lista.ItemActual != null)
            {
                var it = (dataItem)_lista.ItemActual;
                visualizarItem(it);
                _lista.Refresca();
            }
        }
        public void Imprimir()
        {
            if (_lista.Get_CntItem > 0) 
            {
                imprimirItems();
            }
        }


        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }


        private bool cargarData()
        {
            try
            {
                var r01 = Sistema.MyData.FechaServidor();
                var _ano = r01.Entidad.Year;
                var _mes = r01.Entidad.Month;
                var _dia = DateTime.DaysInMonth(_ano, _mes);
                _ctrFiltro.HndFiltro.setDesde(new DateTime(_ano, _mes, 01));
                _ctrFiltro.HndFiltro.setHasta(new DateTime(_ano, _mes, _dia));
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        private void anulaItem(dataItem it)
        {
            var seg = Helpers.Msg.ProcesarGuardar("Anular Movimiento de Anticipo ?");
            if (seg)
            {
                try
                {
                    var r01 = Sistema.MyData.Transporte_Cliente_Anticipo_Anular(it.idMov);
                    it.setEstatusAnulado();
                    Helpers.Msg.EliminarOk();
                }
                catch (Exception e)
                {
                    Helpers.Msg.Error(e.Message);
                }
            }
        }
        private void visualizarItem(dataItem it)
        {
            IRepPlanilla _rep = new Reportes.Planilla.Imp();
            _rep.setItemCargar(it.idMov);
            _rep.Generar();
        }
        private void imprimirItems()
        {
            IRepAdm _rep = new Reportes.ListaAdm.Imp();
            _rep.setFiltrosBusq("");
            _rep.setDataCargar(_lista.Get_Items);
            _rep.Generar();
        }
    }
}