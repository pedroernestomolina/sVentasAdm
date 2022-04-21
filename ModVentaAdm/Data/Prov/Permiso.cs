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

        public OOB.Resultado.FichaEntidad<string> Permiso_PedirClaveAcceso_NivelMaximo()
        {
            var rt = new OOB.Resultado.FichaEntidad<string>();

            var r01 = MyData.Permiso_PedirClaveAcceso_NivelMaximo();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            rt.Entidad = r01.Entidad;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<string> Permiso_PedirClaveAcceso_NivelMedio()
        {
            var rt = new OOB.Resultado.FichaEntidad<string>();

            var r01 = MyData.Permiso_PedirClaveAcceso_NivelMedio();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            rt.Entidad = r01.Entidad;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<string> Permiso_PedirClaveAcceso_NivelMinimo()
        {
            var rt = new OOB.Resultado.FichaEntidad<string>();

            var r01 = MyData.Permiso_PedirClaveAcceso_NivelMinimo();
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }
            rt.Entidad = r01.Entidad;

            return rt;
        }
        //

        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Reportes(string autoGrupoUsuario)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_VentaAdm_Reportes(autoGrupoUsuario);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        //

        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteGrupo(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_ClienteGrupo(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Agregar(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_ClienteGrupo_Agregar(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Editar(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_ClienteGrupo_Editar(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        //

        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteZona(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_ClienteZona(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteZona_Agregar(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_ClienteZona_Agregar(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_ClienteZona_Editar(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_ClienteZona_Editar(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        //

        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_Cliente(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente_Agregar(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_Cliente_Agregar(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente_Editar(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_Cliente_Editar(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente_Reportes(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_Cliente_Reportes(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Cliente_ActivarInactivar(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_Cliente_ActivarInactivar(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        //

        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Adm_AnularDocumento(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_VentaAdm_AnularDocumento(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        //

        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_AnularItem(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_VentaAdm_AnularItem(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_DarDsctoItem(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_VentaAdm_DarDsctoItem(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_VisualizarCosto(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_VentaAdm_VisualizarCosto(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_PrecioLibre(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_VentaAdm_PrecioLibre(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_GenerarDoc_DsctoGlobal(string idGrupoUsu)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_VentaAdm_DsctoGlobal(idGrupoUsu);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }
        //

        public OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha> Permiso_Configuracion(string autoGrupoUsuario)
        {
            var rt = new OOB.Resultado.FichaEntidad<OOB.Permiso.Entidad.Ficha>();

            var r01 = MyData.Permiso_VentaAdm_Configuracion(autoGrupoUsuario);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r01.Mensaje;
                rt.Result = OOB.Resultado.Enumerados.EnumResult.isError;
                return rt;
            }

            var s = r01.Entidad;
            var nr = new OOB.Permiso.Entidad.Ficha()
            {
                estatus = s.estatus,
                seguridad = s.seguridad,
            };
            rt.Entidad = nr;

            return rt;
        }

    }

}