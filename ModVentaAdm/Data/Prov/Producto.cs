using ModVentaAdm.Data.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Prov
{

    public partial class DataPrv : IData
    {

        public OOB.Resultado.Lista<OOB.Producto.Lista.Ficha> Producto_GetLista(OOB.Producto.Lista.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Producto.Lista.Ficha>();

            var filtroDto = new DtoLibPos.ProductoAdm.Lista.Filtro()
            {
                AutoDeposito = filtro.AutoDeposito,
                Cadena = filtro.Cadena,
                MetodoBusqueda = (DtoLibPos.ProductoAdm.Lista.Enumerados.EnumMetodoBusqueda)filtro.MetodoBusqueda,
            };
            var r01 = MyData.ProductoAdm_GetLista(filtroDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Producto.Lista.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    var fechaNula = new DateTime(2000, 1, 1);
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Producto.Lista.Ficha()
                        {
                            Codigo = s.Codigo,
                            Cont_1 = s.Cont_1,
                            Cont_2 = s.Cont_2,
                            Cont_3 = s.Cont_3,
                            Cont_4 = s.Cont_4,
                            Cont_5 = s.Cont_5,
                            ContMayor1 = s.ContMayor1,
                            ContMayor2 = s.ContMayor2,
                            Departamento = s.Departamento,
                            Empq_1 = s.Empq_1,
                            Empq_2 = s.Empq_2,
                            Empq_3 = s.Empq_3,
                            Empq_4 = s.Empq_4,
                            Empq_5 = s.Empq_5,
                            EmpqMayor1 = s.EmpqMayor1,
                            EmpqMayor2 = s.EmpqMayor2,
                            Estatus = s.Estatus,
                            EstatusDivisa = s.EstatusDivisa,
                            EstatusPesado = s.EstatusPesado,
                            ExDisponible = s.ExDisponible,
                            ExFisica = s.ExFisica,
                            FechaUltActCosto = s.FechaUltActCosto == fechaNula ? "" : s.FechaUltActCosto.ToShortDateString(),
                            FechaUltVenta = s.FechaUltVenta == fechaNula ? "" : s.FechaUltVenta.ToShortDateString(),
                            Grupo = s.Grupo,
                            Id = s.Id,
                            Modelo = s.Modelo,
                            Nombre = s.Nombre,
                            PNeto1 = s.PNeto1,
                            PNeto2 = s.PNeto2,
                            PNeto3 = s.PNeto3,
                            PNeto4 = s.PNeto4,
                            PNeto5 = s.PNeto5,
                            PNetoMayor1 = s.PNetoMayor1,
                            PNetoMayor2 = s.PNetoMayor2,
                            Referencia = s.Referencia,
                            TasaIva = s.TasaIva,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }

        public OOB.Resultado.FichaEntidad<OOB.Producto.Entidad.Ficha> Producto_GetFichaById(string id)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Producto.Entidad.Ficha>();

            var r01 = MyData.ProductoAdm_GetFichaById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var ent= r01.Entidad;
            rt.Entidad = new OOB.Producto.Entidad.Ficha()
            {
                Auto = ent.Auto,
                AutoDepartamento = ent.AutoDepartamento,
                AutoGrupo = ent.AutoGrupo,
                AutoSubGrupo=ent.AutoSubGrupo,
                AutoTasaIva=ent.AutoTasaIva,
                Categoria = ent.Categoria,
                CodDepartamento = ent.CodDepartamento,
                CodGrupo = ent.CodGrupo,
                CodigoPrd = ent.CodigoPrd,
                contenido_1 = ent.contenido_1,
                contenido_2 = ent.contenido_2,
                contenido_3 = ent.contenido_3,
                contenido_4 = ent.contenido_4,
                contenido_5 = ent.contenido_5,
                contenidoMay_1 = ent.contenidoMay_1,
                contenidoMay_2 = ent.contenidoMay_2,
                Costo = ent.Costo,
                CostoProm = ent.CostoProm,
                CostoPromUnd = ent.CostoPromUnd,
                CostoUnd = ent.CostoUnd,
                decimales=ent.decimales,
                decimales_1 = ent.decimales_1,
                decimales_2 = ent.decimales_2,
                decimales_3 = ent.decimales_3,
                decimales_4 = ent.decimales_4,
                decimales_5 = ent.decimales_5,
                decimalesMay_1 = ent.decimalesMay_1,
                decimalesMay_2 = ent.decimalesMay_2,
                empaque_1 = ent.empaque_1,
                empaque_2 = ent.empaque_2,
                empaque_3 = ent.empaque_3,
                empaque_4 = ent.empaque_4,
                empaque_5 = ent.empaque_5,
                empaqueMay_1 = ent.empaqueMay_1,
                empaqueMay_2 = ent.empaqueMay_2,
                EstatusPesado = ent.EstatusPesado,
                Modelo = ent.Modelo,
                NombreDepartamento = ent.NombreDepartamento,
                NombreGrupo = ent.NombreGrupo,
                NombrePrd = ent.NombrePrd,
                pneto_1 = ent.pneto_1,
                pneto_2 = ent.pneto_2,
                pneto_3 = ent.pneto_3,
                pneto_4 = ent.pneto_4,
                pneto_5 = ent.pneto_5,
                pnetoMay_1 = ent.pnetoMay_1,
                pnetoMay_2 = ent.pnetoMay_2,
                Referencia = ent.Referencia,
                TasaImpuesto = ent.TasaImpuesto
            };

            return rt;
        }

        public OOB.Resultado.FichaEntidad<OOB.Producto.Existencia.Ficha> Producto_Existencia_GetFicha(string idPrd, string idDeposito)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Producto.Existencia.Ficha>();

            var r01 = MyData.ProductoAdm_Existencia_GetFichaByDeposito(idPrd, idDeposito);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            
            var ent= r01.Entidad;
            rt.Entidad = new OOB.Producto.Existencia.Ficha()
            {
                disponible = ent.disponible,
                real = ent.real,
            };

            return rt;
        }

        public OOB.Resultado.Lista<OOB.Producto.ListaResumen.Ficha> Producto_GetListaResumen(OOB.Producto.ListaResumen.Filtro filtro)
        {
            var rt = new OOB.Resultado.Lista<OOB.Producto.ListaResumen.Ficha>();

            var filtroDto = new DtoLibPos.ProductoAdm.ListaResumen.Filtro()
            {
                Cadena = filtro.Cadena,
                MetodoBusqueda = (DtoLibPos.ProductoAdm.ListaResumen.Enumerados.EnumMetodoBusqueda)filtro.MetodoBusqueda,
            };
            var r01 = MyData.ProductoAdm_GetListaResumen(filtroDto);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var list = new List<OOB.Producto.ListaResumen.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    list = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Producto.ListaResumen.Ficha()
                        {
                            Codigo = s.Codigo,
                            Estatus = s.Estatus,
                            Id = s.Id,
                            Nombre = s.Nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            rt.ListaD = list;

            return rt;
        }

    }

}