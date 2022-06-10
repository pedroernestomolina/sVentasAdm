using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Principal
{
    
    public class Gestion
    {


        private Administrador.Gestion _gestionAdm;
        private Reportes.Gestion _gestionRep;
        private Maestros.Gestion _gestionMaestro;
        private Cliente.Administrador.Gestion _gestionAdmCliente;
        private ReportesCliente.Gestion _gestionRepCli;
        private Documentos.Generar.Gestion _gGenDoc;
        private Documentos.Generar.Presupuesto.Gestion _gestionPresup;
        private Documentos.Generar.Factura.Gestion _gestionFact;
        private Documentos.Generar.Pedido.Gestion _gestionPedido;
        private Configuracion.Gestion _gConfiguracion;


        public string BD_Ruta { get { return Sistema.Instancia; } }
        public string BD_Nombre { get { return Sistema.BaseDatos; } }
        public string Version { get { return "Ver. " + Application.ProductVersion; } }
        public string Host { get { return Sistema.Instancia + "/" + Sistema.BaseDatos; } }
        public string Usuario { get { return Sistema.Usuario.codigo + Environment.NewLine + Sistema.Usuario.nombre; } }


        public Gestion()
        {
            _gestionAdm = new Administrador.Gestion();
            _gestionRep = new Reportes.Gestion();
            _gestionMaestro = new Maestros.Gestion();
            _gestionAdmCliente = new Cliente.Administrador.Gestion();
            _gestionRepCli = new ReportesCliente.Gestion();
            _gGenDoc = new Documentos.Generar.Gestion();
            _gGenDoc.setGestionDsctoCargoFinal(new Documentos.Generar.DsctoCargoFinal.Gestion());
            _gGenDoc.setGestionRemision(new Documentos.Generar.Remision.Gestion());
            _gGenDoc.setGestionPendiente(new Documentos.Generar.Pendiente.Gestion());
            _gGenDoc.setGestionCambioTasa(new Documentos.Generar.CambioTasa.Gestion());
            _gGenDoc.setGestionDatosDoc(new Documentos.Generar.DatosDocumento.Gestion());
            _gGenDoc.setGestionBuscarProducto(new Documentos.Generar.BuscarProducto.Gestion());
            _gGenDoc.setGestionItems(new Documentos.Generar.Items.Gestion());
            _gConfiguracion = new Configuracion.Gestion();
        }


        public void Inicializa()
        {
        }

        PrincipalFrm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                frm = new PrincipalFrm();
                frm.setControlador(this);
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            var rt = true;

            var r01 = Sistema.MyData.Sistema_Empresa_GetFicha();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            Sistema.DatosEmpresa = r01.Entidad;
            var r02 = Sistema.MyData.Sistema_GetCodigoSucursal();
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r02.Mensaje);
                return false;
            }
            var r03 = Sistema.MyData.Sucursal_GetId_ByCodigo(r02.Entidad);
            if (r03.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r03.Mensaje);
                return false;
            }
            var r04= Sistema.MyData.Sucursal_GetFichaById(r03.Entidad);
            if (r04.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r04.Mensaje);
                return false;
            }
            Sistema.Sucursal = r04.Entidad;

            return rt;
        }


        public void AdministradorDoc()
        {
            _gestionAdm.setGestion(new Administrador.Documentos.Gestion());
            _gestionAdm.Inicializa();
            _gestionAdm.Inicia();
        }

        public void Reporte_GeneralDocumentos()
        {
            Reporte(new Reportes.Modo.GeneralDocumento.Gestion());
        }

        public void Reporte_GeneralPorDepartamento()
        {
            Reporte(new Reportes.Modo.GeneralPorDepartamento.Gestion());
        }

        public void Reporte_GeneralPorGrupo()
        {
            Reporte(new Reportes.Modo.GeneralPorGrupo.Gestion());
        }

        private void Reporte(Reportes.IGestion gestion)
        {
            var r00 = Sistema.MyData.Permiso_Reportes(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gestionRep.setGestion(gestion);
                _gestionRep.Inicializa();
                _gestionRep.Inicia();
            }
        }

        public void MaestroGrupo()
        {
            var r00 = Sistema.MyData.Permiso_ClienteGrupo(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gestionMaestro.setGestion(new Maestros.Grupo.Gestion());
                _gestionMaestro.Inicializa();
                _gestionMaestro.Inicia();
            }
        }

        public void MaestroZona()
        {
            var r00 = Sistema.MyData.Permiso_ClienteZona(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gestionMaestro.setGestion(new Maestros.Zona.Gestion());
                _gestionMaestro.Inicializa();
                _gestionMaestro.Inicia();
            }
        }

        public void MaestroClientes()
        {
            var r00 = Sistema.MyData.Permiso_Cliente (Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gestionAdmCliente.Inicializa();
                _gestionAdmCliente.Inicia();
            }
        }

        public void Reporte_Resumen()
        {
            Reporte(new Reportes.Modo.Resumen.Gestion());
        }

        public void Reporte_GeneralPorProducto()
        {
            Reporte(new Reportes.Modo.GeneralPorProducto.Gestion());
        }

        public void Reporte_GeneralDocumentoDetalle()
        {
            Reporte(new Reportes.Modo.GeneralDocumentoDetalle.Gestion());
        }

        public void Reporte_Consolidado()
        {
            Reporte(new Reportes.Modo.Consolidado.Gestion());
        }

        public void Reporte_Cliente_Maestro()
        {
            ReporteCliente(new ReportesCliente.Modo.Maestro.Gestion());
        }

        private void ReporteCliente(ReportesCliente.IGestion gestion)
        {
            var r00 = Sistema.MyData.Permiso_Cliente_Reportes(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gestionRepCli.setGestion(gestion);
                _gestionRepCli.Inicializa();
                _gestionRepCli.Inicia();
            }
        }

        public void GenerarPresupuesto()
        {
            if (_gestionPresup == null)
            {
                _gestionPresup = new Documentos.Generar.Presupuesto.Gestion();
            }
            GenerarDoc(_gestionPresup);
        }

        public void GenerarFactura()
        {
            if (_gestionFact == null)
            {
                _gestionFact = new Documentos.Generar.Factura.Gestion();
            }
            GenerarDoc(_gestionFact);
        }

        private void GenerarDoc(Documentos.Generar.IDocGestion _doc)
        {
            _gGenDoc.setDocGestion(_doc);
            _gGenDoc.Inicializa();
            _gGenDoc.Inicia();
        }

        public void GenerarPedido()
        {
            if (_gestionPedido== null)
            {
                _gestionPedido= new Documentos.Generar.Pedido.Gestion();
            }
            GenerarDoc(_gestionPedido);
        }

        public void Reporte_LibroVenta()
        {
            Reporte(new Reportes.Modo.LibroVenta.Gestion());
        }

        public void ConfiguracionSistema()
        {
            var r00 = Sistema.MyData.Permiso_Configuracion(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _gConfiguracion.Inicializa();
                _gConfiguracion.Inica();
            }
        }

        public void UtilidadConsolidado()
        {
            Reporte(new Reportes.Modo.Utilidad.Consolidado.Gestion());
        }
        public void UtilidadPorVentas()
        {
            Reporte(new Reportes.Modo.Utilidad.Ventas.Gestion());
        }
        public void UtilidadPorProducto()
        {
            Reporte(new Reportes.Modo.Utilidad.Producto.Gestion());
        }

    }

}