using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ToolsCxC.Administrador.Handler
{
    public class hndBusqueda: Utils.Componente.Busqueda.Vistas.IBusqueda
    {
        private OOB.Transporte.CxcMovCobro.ListaMov.Filtro _filtro;


        public hndBusqueda()
        {
            _filtro = new OOB.Transporte.CxcMovCobro.ListaMov.Filtro();
        }
        public void Inicializa()
        {
        }
        public void setFiltros(object filtros)
        {
            var filt= (Filtro.Vistas.IdataFiltrar)filtros;
            var _estatus = "";
            var _idCliente = filt.IdCliente;
            if (filt.EstatusDoc != Filtro.Vistas.Enumerados.EstatusDoc.SinDefinir)
            {
                _estatus = "A";
                if (filt.EstatusDoc == Filtro.Vistas.Enumerados.EstatusDoc.Anulado)
                {
                    _estatus = "I";
                }
            }
            _filtro = new OOB.Transporte.CxcMovCobro.ListaMov.Filtro()
            {
                Desde = filt.Desde,
                Hasta = filt.Hasta,
                Estatus = _estatus,
                IdCliente = _idCliente,
            };
        }
        public IEnumerable<object>Buscar()
        {
            try
            {
                var r01 = Sistema.MyData.Transporte_CxcMovCobro_GetLista (_filtro);
                return (IEnumerable<object>)r01.ListaD.OrderByDescending(o => o.idMov);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return null;
            }
        }
    }
}