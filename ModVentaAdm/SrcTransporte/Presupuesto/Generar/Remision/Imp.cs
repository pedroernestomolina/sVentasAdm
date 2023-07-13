using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Remision
{
    public class Imp: IRemision
    {
        private LibUtilitis.CtrlCB.ICtrl _ctrl;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;
        private List<IObservador> _observadores;
        private bool _habilitarCargarDocRemision;
        private data _docRemision;


        public BindingSource SourceItems_Get { get { return _ctrl.GetSource; } }
        public string ItemId_Get { get { return _ctrl.GetId; } }
        //
        public data DocRemision { get { return _docRemision; } }
        public string DocNombre_Get { get { return _docRemision.docNombre; } }
        public string DocNumero_Get { get { return _docRemision.docNumero; } }
        public string DocFecha_Get { get { return _docRemision.docFecha; } }


        public Imp()
        {
            _remisionBusquedaIsOk = false;
            _habilitarCargarDocRemision = false;
            _cliente = null;
            _docRemision = new data();
            _ctrl = new LibUtilitis.CtrlCB.ImpCB();
            _observadores = new List<IObservador>();
        }


        public void setFichaId(string id)        
        {
            _ctrl.setFichaById(id);
        }
        public void setClienteBuscar(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            _cliente = ficha;
        }
        public void setHabilitarCargarDocRemision(bool hab)
        {
            _habilitarCargarDocRemision = hab;
        }


        public void Inicializa()
        {
            _cliente = null;
            _docRemision.Inicializa();
            _ctrl.Inicializa();
        }
        public void CargarData()
        {
            var _lst = new List<Utils.dataFiltro>();
            _lst.Add(new Utils.dataFiltro() { codigo = "", desc = "PRESUPUESTO", id = "1" });
            _ctrl.CargarData(_lst);
        }
        public void Limpiar()
        {
            _docRemision.Limpiar();
            _ctrl.LimpiarOpcion();
        }

        private bool _remisionBusquedaIsOk;
        public bool RemisionIsOK { get { return _remisionBusquedaIsOk; } }
        public void Buscar()
        {
            _remisionBusquedaIsOk = false;
            if (_ctrl.GetItem == null) 
            {
                Helpers.Msg.Alerta("DEBES SELECCIONAR UN TIPO DE DOCUMENTO PARA LA REMISION");
                return;
            }
            var rt = BuscarDocumentos();
            if (rt != null && rt.Count > 0)
            {
                MostrarSeleccionarDocumentos(rt);
            }
            else 
            {
                Helpers.Msg.Alerta("NO HAY DOCUMENTOS A MOSTRAR");
            }
        }


        private List<OOB.Transporte.Documento.Remision.Lista.Ficha> BuscarDocumentos()
        {
            var _idSistemaDocumento = "";
            switch (_ctrl.GetId) 
            {
                case "1": //PRESUPPUESTO
                    _idSistemaDocumento = Sistema.Id_SistDocumento_Presupuesto;
                    break;
            }
            try
            {
                var s01 = Sistema.MyData.Sistema_TipoDocumento_GetFichaById(_idSistemaDocumento);
                if (s01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(s01.Mensaje);
                }
                var filtroOOB = new OOB.Transporte.Documento.Remision.Lista.Filtro()
                {
                    codTipoDoc = s01.Entidad.codigo,
                    idCliente = _cliente.id,
                };
                var r01 =Sistema.MyData.TransporteDocumento_Remision_ListaBy(filtroOOB);
                return r01.ListaD.Where(w=>!w.isAnulado).ToList();
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return null;
            }
        }
        private Utils.DocLista.IDocLista _listDoc;
        private void MostrarSeleccionarDocumentos(List<OOB.Transporte.Documento.Remision.Lista.Ficha> list)
        {
            _listDoc = new Utils.DocLista.Remision.Imp();
            _listDoc.Inicializa();
            _listDoc.setDataCargar(list.OrderByDescending(o => o.docId).ToList());
            _listDoc.Inicia();
            if (_listDoc.ItemSeleccionadoIsOk) 
            {
                if (!_habilitarCargarDocRemision) 
                {
                    Helpers.Msg.Alerta("CARGA DE DOCUMENTO DE REMISION NO HABILITADA");
                    return;
                }
                CargarDocumentoRemision(((Utils.DocLista.Remision.data)_listDoc.ItemSeleccionado).Ficha);
            }
        }

        private void CargarDocumentoRemision(OOB.Transporte.Documento.Remision.Lista.Ficha ficha)
        {
            switch (ficha.docCodigo)
            {
                case "05": //PRESUPUESTO
                    CargarDocumentoPresupuesto(ficha.docId);
                    break;
            }
        }

        private void CargarDocumentoPresupuesto(string idDoc)
        {
            try
            {
                var r01 = Sistema.MyData.TransporteDocumento_EntidadPresupuesto_GetById(idDoc);
                _docRemision.setId(r01.Entidad.encabezado.idDoc);
                _docRemision.setNombre(r01.Entidad.encabezado.docNombre);
                _docRemision.setNumero(r01.Entidad.encabezado.docNumero);
                _docRemision.setFecha(r01.Entidad.encabezado.docFechaEmision);
                _docRemision.setTipo(r01.Entidad.encabezado.docCodigoTipo);
                _remisionBusquedaIsOk = true;
                foreach (var obs in _observadores)
                {
                    obs.NotificarRemisionDocPresupuesto(r01.Entidad);
                }
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }


        public void AgregarObservador(IObservador obs) 
        {
            _observadores.Add(obs);
        }
    }
}