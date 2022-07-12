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

        public OOB.Resultado.FichaEntidad<OOB.Sistema.Empresa.Entidad.Ficha> 
            Sistema_Empresa_GetFicha()
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Sistema.Empresa.Entidad.Ficha>();

            var r01 = MyData.Sistema_Empresa_GetFicha();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var s = r01.Entidad;
            result.Entidad = new OOB.Sistema.Empresa.Entidad.Ficha()
            {
                CiRif = s.CiRif,
                Direccion = s.Direccion,
                Nombre = s.Nombre,
                Telefono = s.Telefono,
            };

            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Sistema.TipoDocumento.Entidad.Ficha>
            Sistema_TipoDocumento_GetFichaById(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Sistema.TipoDocumento.Entidad.Ficha>();

            var r01 = MyData.Sistema_TipoDocumento_GetFichaById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var ent = r01.Entidad;
            result.Entidad = new OOB.Sistema.TipoDocumento.Entidad.Ficha()
            {
                id = ent.autoId,
                codigo = ent.codigo,
                descripcion = ent.nombre,
                siglas = ent.siglas,
                signo = ent.signo,
                tipo = ent.tipo,
            };

            return result;
        }
        

        public OOB.Resultado.Lista<OOB.Sistema.Vendedor.Entidad.Ficha> 
            Sistema_Vendedor_GetLista()
        {
            var result = new OOB.Resultado.Lista<OOB.Sistema.Vendedor.Entidad.Ficha>();

            var filtroDTO = new DtoLibPos.Vendedor.Lista.Filtro();
            var r01 = MyData.Vendedor_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Sistema.Vendedor.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Sistema.Vendedor.Entidad.Ficha()
                        {
                            id = s.id,
                            codigo = s.codigo,
                            nombre = s.nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }


        public OOB.Resultado.Lista<OOB.Sistema.Cobrador.Entidad.Ficha> 
            Sistema_Cobrador_GetLista()
        {
            var result = new OOB.Resultado.Lista<OOB.Sistema.Cobrador.Entidad.Ficha>();

            var filtroDTO = new DtoLibPos.Cobrador.Lista.Filtro();
            var r01 = MyData.Cobrador_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Sistema.Cobrador.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Sistema.Cobrador.Entidad.Ficha()
                        {
                            id = s.id,
                            codigo = s.codigo,
                            nombre = s.nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Sistema.Cobrador.Entidad.Ficha> 
            Sistema_Cobrador_GetFicha_ById(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Sistema.Cobrador.Entidad.Ficha>();

            var r01 = MyData.Cobrador_GetFichaById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var s=r01.Entidad;
            var nr = new OOB.Sistema.Cobrador.Entidad.Ficha()
            {
                id = s.id,
                codigo = s.codigo,
                nombre = s.nombre,
            };
            result.Entidad = nr;

            return result;
        }


        public OOB.Resultado.Lista<OOB.Sistema.Estado.Entidad.Ficha> 
            Sistema_Estado_GetLista()
        {
            var result = new OOB.Resultado.Lista<OOB.Sistema.Estado.Entidad.Ficha>();

            var r01 = MyData.Sistema_Estado_GetLista();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Sistema.Estado.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Sistema.Estado.Entidad.Ficha()
                        {
                            auto = s.auto,
                            nombre = s.nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }
        public OOB.Resultado.Lista<OOB.Sistema.Transporte.Entidad.Ficha> 
            Sistema_Transporte_GetLista()
        {
            var result = new OOB.Resultado.Lista<OOB.Sistema.Transporte.Entidad.Ficha>();

            var filtroDTO = new DtoLibPos.Transporte.Lista.Filtro();
            var r01 = MyData.Transporte_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Sistema.Transporte.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Sistema.Transporte.Entidad.Ficha()
                        {
                            id = s.id,
                            codigo = s.codigo,
                            nombre = s.nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }
        public OOB.Resultado.Lista<OOB.Sistema.Deposito.Entidad.Ficha> 
            Deposito_GetLista(OOB.Sistema.Deposito.Lista.Filtro filtro)
        {
            var result = new OOB.Resultado.Lista<OOB.Sistema.Deposito.Entidad.Ficha>();

            var filtroDTO = new DtoLibPos.Deposito.Lista.Filtro();
            filtroDTO.PorCodigoSuc = filtro.PorCodigoSuc;
            var r01 = MyData.Deposito_GetLista(filtroDTO);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Sistema.Deposito.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Sistema.Deposito.Entidad.Ficha()
                        {
                            id = s.id,
                            codigo = s.codigo,
                            nombre = s.nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }
        public OOB.Resultado.Lista<OOB.Sistema.Fiscal.Entidad.Ficha>
            Sistema_TasaFiscal_GetLista()
        {
            var result = new OOB.Resultado.Lista<OOB.Sistema.Fiscal.Entidad.Ficha>();

            var r01 = MyData.Fiscal_GetTasas(new DtoLibPos.Fiscal.Lista.Filtro());
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }

            var lst = new List<OOB.Sistema.Fiscal.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Sistema.Fiscal.Entidad.Ficha()
                        {
                            id = s.id,
                            descripcion = s.descripcion,
                            tasa = s.tasa,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }


        public OOB.Resultado.FichaEntidad<string> 
            Sistema_GetCodigoSucursal()
        {
            var rt = new OOB.Resultado.FichaEntidad<string>();

            var r01 = MyData.Sistema_GetCodigoSucursal();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            rt.Entidad = r01.Entidad;

            return rt;
        }


        public OOB.Resultado.Lista<OOB.Sistema.MedioCobro.Entidad.Ficha> 
            Sistema_MedioCobro_GetLista()
        {
            var result = new OOB.Resultado.Lista<OOB.Sistema.MedioCobro.Entidad.Ficha>();

            var r01 = MyData.MedioPago_GetLista(new DtoLibPos.MedioPago.Lista.Filtro());
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var lst = new List<OOB.Sistema.MedioCobro.Entidad.Ficha>();
            if (r01.Lista != null)
            {
                if (r01.Lista.Count > 0)
                {
                    lst = r01.Lista.Select(s =>
                    {
                        var nr = new OOB.Sistema.MedioCobro.Entidad.Ficha()
                        {
                            id = s.id,
                            codigo = s.codigo,
                            nombre = s.nombre,
                        };
                        return nr;
                    }).ToList();
                }
            }
            result.ListaD = lst;

            return result;
        }
        public OOB.Resultado.FichaEntidad<OOB.Sistema.MedioCobro.Entidad.Ficha> 
            Sistema_MedioCobro_GetFicha_ById(string id)
        {
            var result = new OOB.Resultado.FichaEntidad<OOB.Sistema.MedioCobro.Entidad.Ficha>();

            var r01 = MyData.MedioPago_GetFichaById(id);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return result;
            }
            var s = r01.Entidad;
            result.Entidad = new OOB.Sistema.MedioCobro.Entidad.Ficha()
            {
                id = s.id,
                codigo = s.codigo,
                nombre = s.nombre,
            };

            return result;
        }

    }

}