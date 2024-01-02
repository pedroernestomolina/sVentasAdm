using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra.Transporte
{
    public interface ITransporteClienteAnticipo
    {
        OOB.Resultado.FichaEntidad<int>
            Transporte_Cliente_Anticipo_Agregar(OOB.Transporte.ClienteAnticipo.Agregar.Ficha ficha);
        OOB.Resultado.FichaEntidad<OOB.Transporte.ClienteAnticipo.Obtener.Ficha>
            Transporte_Cliente_Anticipo_Obtener_ById(string id);
        OOB.Resultado.Lista<OOB.Transporte.ClienteAnticipo.ListaMov.Ficha>
            Transporte_Cliente_Anticipo_GetLista(OOB.Transporte.ClienteAnticipo.ListaMov.Filtro filtro);
        //
        OOB.Resultado.Ficha
            Transporte_Cliente_Anticipo_Anular(int idMov);
        //
        OOB.Resultado.FichaEntidad<OOB.Transporte.ClienteAnticipo.Entidad.Ficha>
            Transporte_Cliente_Anticipo_Movimiento_GetById(int idMov);
    }
}