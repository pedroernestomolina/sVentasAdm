using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.Presupuesto
{
    
    public class Gestion: IDocGestion
    {

        private datosDoc _datosDoc;
        private decimal _tasaDivisa;
        private AgregarEditarItem.IGestion _itemGestion;
        private OOB.Sistema.TipoDocumento.Entidad.Ficha _sistTipoDoc;
        private List<Remision.tipoDoc> _lstDocRemision;


        public string TipoDocumento { get { return "PRESUPUESTO"; } }
        public IDatosDocumento HabilitarDatosDoc { get { return _datosDoc; } }
        public decimal TasaDivisa { get { return _tasaDivisa; } }
        public AgregarEditarItem.IGestion ItemGestion { get { return _itemGestion; } }
        public OOB.Sistema.TipoDocumento.Entidad.Ficha SistTipoDocumento { get { return _sistTipoDoc; } }
        public int CantDocPend { get { return CantidadDocPendiente(); } }
        public int CantDocRecuperar { get { return CantidadDocRecuperar(); } }
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
            var r01 = Sistema.MyData.Configuracion_FactorDivisa();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _tasaDivisa = r01.Entidad;

            var r02 = Sistema.MyData.Sistema_TipoDocumento_GetFichaById(Sistema.Id_SistDocumento_Presupuesto);
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
            if (r01.Result== OOB.Resultado.Enumerados.EnumResult.isError)
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

        public OOB.Venta.Temporal.Remision.Registrar.Ficha CargaRemision(OOB.Documento.Entidad.Ficha ficha, int _idVentaTemporal)
        {
            var lst = new List<remision>();
            foreach (var it in ficha.items) 
            {
                var dt = new remision(it);
                lst.Add(dt);
            }
            var t= lst.Sum(s=>s.total);

            var fichaOOB = new OOB.Venta.Temporal.Remision.Registrar.Ficha()
            {
                autoDoc= ficha.Auto,
                numeroDoc= ficha.DocumentoNro,
                codigoDoc= ficha.Tipo,
                fechaDoc=ficha.Fecha,
                nombreDoc=ficha.DocumentoNombre,
                idTemporal = _idVentaTemporal,
                renglones = ficha.items.Count,
                monto = t,
                montoDivisa = t / TasaDivisa,
                items = lst.Select(s =>
                {
                    var nr = new OOB.Venta.Temporal.Item.Registrar.ItemDetalle()
                    {
                        idVenta = _idVentaTemporal,
                        autoDepartamento = s.autoDepartamento,
                        autoGrupo = s.autoGrupo,
                        autoProducto = s.autoProducto,
                        autoSubGrupo = s.autoSubGrupo,
                        autoTasaIva = s.autoTasaIva,
                        codigoProducto = s.codigoProducto,
                        nombreProducto = s.nombreProducto,
                        cantidad = s.cantidad,
                        precioNeto = s.precioNeto,
                        precioNetoDivisa = Math.Round(s.precioNeto / _tasaDivisa, 2, MidpointRounding.AwayFromZero),
                        tarifaPrecio = s.tarifaPrecio,
                        tasaIva = s.tasaIva,
                        tipoIva = s.tipoIva,
                        categroiaProducto = s.categroiaProducto,
                        decimalesProducto = s.decimalesProducto,
                        empaqueCont = s.empaqueCont,
                        empaqueDesc = s.empaqueDesc,
                        estatusPesadoProducto = s.estatusPesadoProducto,
                        estatusReservaMerc = "",
                        costo = s.costo,
                        costoPromd = s.costoPromd,
                        costoPromdUnd = s.costoPromdUnd,
                        costoUnd = s.costoUnd,
                        dsctoPorct = s.dsctoPorct,
                        notas = s.notas,
                        autoDeposito = s.autoDeposito,
                        cantidadUnd = s.cantidadUnd,
                        total = s.total,
                        totalDivisa = Math.Round(s.total / _tasaDivisa, 2, MidpointRounding.AwayFromZero),
                        estatusRemision = "1",
                    };
                    return nr;
                }).ToList(),
            };

            return fichaOOB;
        }

        public void setCambioTasaDivisa(decimal tasa)
        {
            _tasaDivisa = tasa;
        }

        public void ActualizarTasaDivisaSistema()
        {
            var r01 = Sistema.MyData.Configuracion_FactorDivisa();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }
            _tasaDivisa = r01.Entidad;
        }

    }

}