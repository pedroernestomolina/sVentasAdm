using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.__.Cliente.ParaZufu
{
    public class ImpData: IData.IData
    {
        public List<OOB.Maestro.Grupo.Entidad.Ficha> 
            Grupo_GetLista()
        {
            var filtroOOB = new OOB.Maestro.Grupo.Lista.Filtro();
            var r01 =  Sistema.MyData.ClienteGrupo_GetLista(filtroOOB);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                throw new Exception(r01.Mensaje);
            }
            return r01.ListaD.OrderBy(o => o.nombre).ToList();
        }
        public List<OOB.Sistema.Estado.Entidad.Ficha> 
            Estados_GetLista()
        {
            var r01 = Sistema.MyData.Sistema_Estado_GetLista();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return r01.ListaD.OrderBy(o => o.nombre).ToList();
        }
        public List<OOB.Maestro.Zona.Entidad.Ficha> 
            Zonas_GetLista()
        {
            var filtroOOB = new OOB.Maestro.Zona.Lista.Filtro();
            var r01 = Sistema.MyData.ClienteZona_GetLista(filtroOOB);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return r01.ListaD.OrderBy(o => o.nombre).ToList();
        }
        public List<OOB.Sistema.Vendedor.Entidad.Ficha> 
            Vendedores_GetLista()
        {
            var r01 = Sistema.MyData.Sistema_Vendedor_GetLista();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return r01.ListaD.OrderBy(o => o.nombre).ToList();
        }
        public List<OOB.Sistema.Cobrador.Entidad.Ficha> 
            Cobradores_GetLista()
        {
            var r01 = Sistema.MyData.Sistema_Cobrador_GetLista ();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                throw new Exception(r01.Mensaje);
            }
            return r01.ListaD.OrderBy(o => o.nombre).ToList();
        }
        public List<OOB.Sistema.General> 
            Categorias_GetLista()
        {
            var _lst = new List<OOB.Sistema.General>();
            _lst.Clear();
            _lst.Add(new OOB.Sistema.General() { id = "01", desc = "Administrativo", codigo = "" });
            _lst.Add(new OOB.Sistema.General() { id = "02", desc = "Eventual", codigo = "" });
            return _lst;
        }
        public List<OOB.Sistema.General>
            Nivel_GetLista()
        {
            var _lst = new List<OOB.Sistema.General>();
            _lst.Clear();
            _lst.Add(new OOB.Sistema.General() { id = "00", desc = "Sin Definir", codigo = "" });
            _lst.Add(new OOB.Sistema.General() { id = "01", desc = "A", codigo = "" });
            _lst.Add(new OOB.Sistema.General() { id = "02", desc = "B", codigo = "" });
            _lst.Add(new OOB.Sistema.General() { id = "03", desc = "C", codigo = "" });
            return _lst;
        }
        public List<OOB.Sistema.General>
            Estatus_GetLista()
        {
            var _lst = new List<OOB.Sistema.General>();
            _lst.Clear();
            _lst.Add(new OOB.Sistema.General() { id = "01", desc = "Activo", codigo = "" });
            _lst.Add(new OOB.Sistema.General() { id = "02", desc = "Inactivo", codigo = "" });
            return _lst;
        }
        public List<OOB.Sistema.General>
            Credito_GetLista()
        {
            var _lst = new List<OOB.Sistema.General>();
            _lst.Clear();
            _lst.Add(new OOB.Sistema.General() { id = "01", desc = "Activo", codigo = "" });
            _lst.Add(new OOB.Sistema.General() { id = "02", desc = "Inactivo", codigo = "" });
            return _lst;
        }
        public OOB.Maestro.Cliente.Entidad.Ficha 
            ObtenerFicha_Cliente_PorId(string id)
        {
            return null;
        }
    }
}