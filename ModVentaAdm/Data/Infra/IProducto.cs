using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface IProducto
    {
        
        OOB.Resultado.Lista<OOB.Producto.Lista.Ficha> Producto_GetLista(OOB.Producto.Lista.Filtro filtro);
        OOB.Resultado.Lista<OOB.Producto.ListaResumen.Ficha> Producto_GetListaResumen(OOB.Producto.ListaResumen.Filtro filtro);
        OOB.Resultado.FichaEntidad<OOB.Producto.Entidad.Ficha> Producto_GetFichaById(string id);
        OOB.Resultado.FichaEntidad<OOB.Producto.Existencia.Ficha> Producto_Existencia_GetFicha(string idPrd, string idDeposito);

    }

}