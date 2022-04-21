using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.Factura
{
    
    public class Gestion: IDocGestion
    {

        private datosDoc _datosDoc;
        private decimal _tasaDivisa;
        private AgregarEditarItem.IGestion _itemGestion;
        private OOB.Sistema.TipoDocumento.Entidad.Ficha _sistTipoDoc;
        private List<Remision.tipoDoc> _lstDocRemision;


        public string TipoDocumento { get { return "FACTURA"; } }
        public IDatosDocumento HabilitarDatosDoc { get { return _datosDoc; } }
        public decimal TasaDivisa { get { return _tasaDivisa; ; } }
        public AgregarEditarItem.IGestion ItemGestion { get { return _itemGestion; } }
        public OOB.Sistema.TipoDocumento.Entidad.Ficha SistTipoDocumento { get { return _sistTipoDoc; } }
        public int CantDocPend { get { return CantidadDocPendiente(); } }
        public int CantDocRecuperar{ get { return CantidadDocRecuperar(); } }
        public List<Remision.tipoDoc> TipoDocRemision { get { return _lstDocRemision; } }


        public Gestion() 
        {
            _tasaDivisa = 0m;
            _datosDoc = new datosDoc();
            _itemGestion = new GestionItem();
            _sistTipoDoc = null;
            _lstDocRemision = new List<Remision.tipoDoc>(); 
        }


        public void Inicializa()
        {
            _tasaDivisa = 0m;
            _sistTipoDoc = null;
        }

        public bool CargarData()
        {
            var r01 = Sistema.MyData.Configuracion_FactorDivisa ();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _tasaDivisa = r01.Entidad;

            var r02 = Sistema.MyData.Sistema_TipoDocumento_GetFichaById("0000000001");
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _sistTipoDoc = r02.Entidad;

            _lstDocRemision.Clear();
            _lstDocRemision.Add(new Remision.tipoDoc("01", "FACTURA"));
            _lstDocRemision.Add(new Remision.tipoDoc("04", "NOTA ENTREGA"));
            _lstDocRemision.Add(new Remision.tipoDoc("05", "PRESUPUESTO"));
            _lstDocRemision.Add(new Remision.tipoDoc("06", "PEDIDO"));

            return true;
        }

        private int CantidadDocPendiente()
        {
            var rt = 0;

            var ficha = new OOB.Venta.Temporal.Pendiente.Cantidad.Ficha()
            {
                autoSistDocumento = _sistTipoDoc.id,
                autoUsuario = Sistema.Usuario.id,
                idEquipo = Sistema.IdEquipo,
            };
            var r01 = Sistema.MyData.VentaAdm_Temporal_Pendiente_GetCantidadDoc(ficha);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return rt;
            }
            rt = r01.Entidad;

            return rt;
        }

        private int CantidadDocRecuperar()
        {
            var rt = 0;

            var ficha = new OOB.Venta.Temporal.Recuperar.Ficha()
            {
                autoSistDocumento = _sistTipoDoc.id,
                autoUsuario = Sistema.Usuario.id,
                idEquipo = Sistema.IdEquipo,
            };
            var r01 = Sistema.MyData.VentaAdm_Temporal_Recuperar_GetCantidadDoc(ficha);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return rt;
            }
            rt = r01.Entidad;

            return rt;
        }

        OOB.Venta.Temporal.Remision.Registrar.Ficha IDocGestion.CargaRemision(OOB.Documento.Entidad.Ficha ficha, int _idVentaTemporal)
        {
            throw new NotImplementedException();
        }

        public void setCambioTasaDivisa(decimal tasa)
        {
            throw new NotImplementedException();
        }

        public void ActualizarTasaDivisaSistema()
        {
            throw new NotImplementedException();
        }

    }

}