using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.__.Cliente.ParaTranspRivas
{
    public class ImpData: IData.IData
    {
        public List<OOB.Maestro.Grupo.Entidad.Ficha>
            Grupo_GetLista()
        {
            return null;
        }
        public List<OOB.Sistema.Estado.Entidad.Ficha>
            Estados_GetLista()
        {
            return null;
        }
        public List<OOB.Maestro.Zona.Entidad.Ficha>
            Zonas_GetLista()
        {
            return null;
        }
        public List<OOB.Sistema.Vendedor.Entidad.Ficha>
            Vendedores_GetLista()
        {
            return null;
        }
        public List<OOB.Sistema.Cobrador.Entidad.Ficha>
            Cobradores_GetLista()
        {
            return null;
        }
        public List<OOB.Sistema.General>
            Categorias_GetLista()
        {
            return null;
        }
        public List<OOB.Sistema.General>
            Nivel_GetLista()
        {
            return null;
        }
        public List<OOB.Sistema.General>
            Estatus_GetLista()
        {
            return null;
        }
        public List<OOB.Sistema.General>
            Credito_GetLista()
        {
            return null;
        }
        public OOB.Maestro.Cliente.Entidad.Ficha 
            ObtenerFicha_Cliente_PorId(string id)
        {
            var r01 = Sistema.MyData.Cliente_GetFicha(id);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return r01.Entidad;
        }
    }
}