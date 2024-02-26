using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Principal
{

    public partial class PrincipalFrm : Form
    {

        private Src.Principal.Gestion _controlador;
        private Timer timer;


        public PrincipalFrm()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var s = DateTime.Now;
            L_FECHA.Text = s.ToLongDateString();
            L_HORA.Text = s.ToLongTimeString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
            L_HERRAMIENTA.Text = _controlador.GetNombreHerramienta;
            L_VERSION.Text = _controlador.Version;
            L_HOST.Text = _controlador.Host;
            L_USUARIO.Text = _controlador.Usuario;
            L_FECHA.Text = "";
            L_HORA.Text = "";
            this.Text = _controlador.GetNombreHerramienta;
        }

        public void setControlador(Src.Principal.Gestion ctr)
        {
            _controlador = ctr;
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            this.Close();
        }

        private void TSM_ARCHIVO_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        public void setVisibilidadOff()
        {
            this.Visible = false;
        }

        public void setVisibilidadOn()
        {
            this.Visible = true;
        }


        private void MENU_MAESTRO_CLIENTES_Click(object sender, EventArgs e)
        {
            MaestroClientes();
        }
        private void MENU_MAESTRO_GRUPO_Click(object sender, EventArgs e)
        {
            MaestroGrupo();
        }
        private void MENU_MAESTRO_ZONA_Click(object sender, EventArgs e)
        {
            MaestroZona();
        }
        private void MENU_MAESTRO_REPORTE_MAESTRO_CLIENTE_Click(object sender, EventArgs e)
        {
            MaestroCliente();
        }

        
        private void MENU_DOCUMENTOS_PRESUPUESTO_Click(object sender, EventArgs e)
        {
            GenerarPresupuesto();
        }
        private void MENU_DOCUMENTOS_FACTURA_Click(object sender, EventArgs e)
        {
            GenerarFactura();
        }
        private void MENU_DOCUMENTOS_PEDIDO_Click(object sender, EventArgs e)
        {
            GenerarPedido();
        }


        private void MENU_DOCUMENTOS_ADMINISTRADOR_Click(object sender, EventArgs e)
        {
            AdministradorDoc();
        }
        private void MENU_CONFIGURACION_SISTEMA_Click(object sender, EventArgs e)
        {
            ConfiguracionSistema();
        }
        private void TSM_CXC_TOOLS_Click(object sender, EventArgs e)
        {
            ToolsCxC();
        }

        private void ToolsCxC()
        {
            _controlador.ToolsCxC();
        }
        private void AdministradorDoc()
        {
            _controlador.AdministradorDoc();
        }
        private void ConfiguracionSistema()
        {
            _controlador.ConfiguracionSistema();
        }


        private void MENU_REPORTES_LIBRO_VENTA_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_LibroVenta();
        }
        private void MENU_REPORTES_CONSOLIDADO_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_Consolidado();
        }
        private void MENU_REPORTES_GENERAL_DOCUMENTOS_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_GeneralDocumentos();
        }
        private void MENU_REPORTES_GENERAL_POR_DEPARTAMENTO_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_GeneralPorDepartamento();
        }
        private void MENU_REPORTES_GENERAL_POR_GRUPO_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_GeneralPorGrupo();
        }
        private void MENU_REPORTES_RESUMEN_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_Resumen();
        }
        private void MENU_REPORTES_GENERAL_POR_PRODUCTO_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_GeneralPorProducto();
        }
        private void MENU_REPORTES_GENERAL_DOCUMENTOS_DETALLE_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_GeneralDocumentoDetalle();
        }
        private void MENU_REPORTES_UTILIDAD_POR_VENTAS_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_UtilidadPorVentas();
        }
        private void MENU_REPORTES_UTILIDAD_POR_UTILIDAD_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_UtilidadPorProducto();
        }
        private void MENU_REPORTES_UTILIDAD_CONSOLIDADO_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_UtilidadConsolidado();
        }
        private void MENU_REPORTES_VENDEDOR_RESUMEN_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_PorVendedorResumen();
        }
        private void MENU_REPORTES_VENDEDOR_DETALLADO_Click(object sender, EventArgs e)
        {
            Reporte_Ventas_PorVendedorDetallado();
        }


        //CLIENTES
        private void MaestroClientes()
        {
            _controlador.MaestroClientes();
        }
        private void MaestroGrupo()
        {
            _controlador.MaestroGrupo();
        }
        private void MaestroZona()
        {
            _controlador.MaestroZona();
        }
        private void MaestroCliente()
        {
            _controlador.RepCliente_Maestro();
        }


        //DOCUMENTOS
        private void GenerarFactura()
        {
            //_controlador.GenerarFactura();
        }
        private void GenerarPresupuesto()
        {
            //_controlador.GenerarPresupuesto();
        }
        private void GenerarPedido()
        {
            //_controlador.GenerarPedido();
        }


        //REPORTE VENTAS
        private void Reporte_Ventas_LibroVenta()
        {
            _controlador.Reporte_LibroVenta();
        }
        private void Reporte_Ventas_Consolidado()
        {
            _controlador.Reporte_Consolidado();
        }
        private void Reporte_Ventas_GeneralDocumentos()
        {
            _controlador.Reporte_GeneralDocumentos();
        }
        private void Reporte_Ventas_UtilidadPorVentas()
        {
            _controlador.UtilidadPorVentas();
        }
        private void Reporte_Ventas_UtilidadPorProducto()
        {
            _controlador.UtilidadPorProducto();
        }
        private void Reporte_Ventas_UtilidadConsolidado()
        {
            _controlador.UtilidadConsolidado();
        }
        private void Reporte_Ventas_GeneralPorProducto()
        {
            _controlador.Reporte_GeneralPorProducto();
        }
        private void Reporte_Ventas_GeneralDocumentoDetalle()
        {
            _controlador.Reporte_GeneralDocumentoDetalle();
        }
        private void Reporte_Ventas_GeneralPorDepartamento()
        {
            _controlador.Reporte_GeneralPorDepartamento();
        }
        private void Reporte_Ventas_GeneralPorGrupo()
        {
            _controlador.Reporte_GeneralPorGrupo();
        }
        private void Reporte_Ventas_Resumen()
        {
            _controlador.Reporte_Resumen();
        }
        private void Reporte_Ventas_PorVendedorResumen()
        {
            _controlador.Reporte_Vendedor_Resumen();
        }
        private void Reporte_Ventas_PorVendedorDetallado()
        {
            _controlador.Reporte_Vendedor_Detallado();
        }



        //
        private void MNU_MAESTRO_ALIADO_Click(object sender, EventArgs e)
        {
            MenuMaestroAliados();
        }
        private void MenuMaestroAliados()
        {
            _controlador.MenuMaestroAliados();
        }
        private void MNU_MAESTRO_SERV_PRESTADOS_Click(object sender, EventArgs e)
        {
            MenuMaestroServiciosPrestados();
        }
        private void MenuMaestroServiciosPrestados()
        {
            _controlador.MenuMaestroServiciosPrestados();
        }


        private void MNU_TRANSPORTE_PRESUPUESTO_GENERAR_Click(object sender, EventArgs e)
        {
            TransportePresupuestoGenerar();
        }
        private void TransportePresupuestoGenerar()
        {
            _controlador.TransportePresupuestoGenerar();
        }

        private void MENU_DOCUMENTOS_TRANS_PRO_FORMA_Click(object sender, EventArgs e)
        {
            TransporteProForma();
        }
        private void TransporteProForma()
        {
            _controlador.TransporteProFormaGenerar();
        }

        private void MNU_TRANSPORTE_FACTURA_Click(object sender, EventArgs e)
        {
            TransporteFactura();
        }
        private void TransporteFactura()
        {
            _controlador.TransporteFacturaGenerar();
        }

        //
        private void REP_TRANS_ALIADO_RESUMEN_Click(object sender, EventArgs e)
        {
            ReporteTransporte_AliadoResumen();
        }
        private void REP_TRANS_ALIADO_DETALLE_Click(object sender, EventArgs e)
        {
            ReporteTransporte_AliadoDetalleDoc();
        }
        private void REP_TRANS_ALIADO_POR_SERVICIO_Click(object sender, EventArgs e)
        {
            ReporteTransporte_AliadoDetalleServ();
        }
        private void REP_TRANS_ALIADO_POR_CLIENTE_Click(object sender, EventArgs e)
        {
            ReporteTransporte_AliadoPorCliente();
        }

        private void ReporteTransporte_AliadoResumen()
        {
            _controlador.ReporteTransporte_AliadoResumen();
        }
        private void ReporteTransporte_AliadoDetalleDoc()
        {
            _controlador.ReporteTransporte_AliadoDetalleDoc();
        }
        private void ReporteTransporte_AliadoDetalleServ()
        {
            _controlador.ReporteTransporte_AliadoDetalleServ();
        }
        private void ReporteTransporte_AliadoPorCliente()
        {
            _controlador.ReporteTransporte_AliadoPorCliente();
        }
        //

        private void MNU_CNF_NOTAS_PRESUP_Click(object sender, EventArgs e)
        {
            Cnf_NotasPresupuesto();
        }
        private void Cnf_NotasPresupuesto()
        {
            _controlador.Cnf_NotasPresupuesto();
        }

        private void MNU_CNF_NOTAS_FACTURA_Click(object sender, EventArgs e)
        {
            Cnf_NotasFactura();
        }
        private void Cnf_NotasFactura()
        {
            _controlador.Cnf_NotasFactura();
        }

        private void REP_CLIENTE_MAESTRO_Click(object sender, EventArgs e)
        {
            MaestroCliente();
        }

        private void MENU_DOCUMENTOS_TRANS_NOTA_CREDITO_Click(object sender, EventArgs e)
        {
            _controlador.NotaCreditoAdm();
        }
    }
}