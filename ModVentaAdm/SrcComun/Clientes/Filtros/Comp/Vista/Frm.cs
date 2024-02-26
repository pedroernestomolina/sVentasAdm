using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcComun.Clientes.Filtros.Comp.Vista
{
    public partial class Frm : Form
    {
        private IVista _controlador;
        //
        public Frm()
        {
            InitializeComponent();
            InicializaControles();
        }
        private void InicializaControles()
        {
            CB_GRUPO.ValueMember = "id";
            CB_GRUPO.DisplayMember = "desc";
            CB_ESTADO.ValueMember = "id";
            CB_ESTADO.DisplayMember = "desc";
            CB_ZONA.ValueMember = "id";
            CB_ZONA.DisplayMember = "desc";
            CB_VENDEDOR.ValueMember = "id";
            CB_VENDEDOR.DisplayMember = "desc";
            CB_COBRADOR.ValueMember = "id";
            CB_COBRADOR.DisplayMember = "desc";
            CB_CATEGORIA.ValueMember = "id";
            CB_CATEGORIA.DisplayMember = "desc";
            CB_NIVEL.ValueMember = "id";
            CB_NIVEL.DisplayMember = "desc";
            CB_ESTATUS.ValueMember = "id";
            CB_ESTATUS.DisplayMember = "desc";
            CB_CREDITO.ValueMember = "id";
            CB_CREDITO.DisplayMember = "desc";
        }
        private bool _modoInicializar;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            P_GRUPO.Enabled = _controlador.OpcPorGrupo.Opcion.GetHabilitarOpcion;
            CB_GRUPO.DataSource = _controlador.OpcPorGrupo.Opcion.GetSource;
            CB_GRUPO.SelectedValue = _controlador.OpcPorGrupo.Opcion.GetId;
            //
            P_ESTADO.Enabled = _controlador.OpcPorEstado.Opcion.GetHabilitarOpcion;
            CB_ESTADO.DataSource = _controlador.OpcPorEstado.Opcion.GetSource;
            CB_ESTADO.SelectedValue = _controlador.OpcPorEstado.Opcion.GetId;
            //
            P_ZONA.Enabled = _controlador.OpcPorZona.Opcion.GetHabilitarOpcion;
            CB_ZONA.DataSource = _controlador.OpcPorZona.Opcion.GetSource;
            CB_ZONA.SelectedValue = _controlador.OpcPorZona.Opcion.GetId;
            //
            P_VENDEDOR.Enabled = _controlador.OpcPorVendedor.Opcion.GetHabilitarOpcion;
            CB_VENDEDOR.DataSource = _controlador.OpcPorVendedor.Opcion.GetSource;
            CB_VENDEDOR.SelectedValue = _controlador.OpcPorVendedor.Opcion.GetId;
            //
            P_COBRADOR.Enabled = _controlador.OpcPorCobrador.Opcion.GetHabilitarOpcion;
            CB_COBRADOR.DataSource = _controlador.OpcPorCobrador.Opcion.GetSource;
            CB_COBRADOR.SelectedValue = _controlador.OpcPorCobrador.Opcion.GetSource;
            //
            P_CATEGORIA.Enabled = _controlador.OpcPorCategoria.Opcion.GetHabilitarOpcion;
            CB_CATEGORIA.DataSource = _controlador.OpcPorCategoria.Opcion.GetSource;
            CB_CATEGORIA.SelectedValue = _controlador.OpcPorCategoria.Opcion.GetId;
            //
            P_NIVEL.Enabled = _controlador.OpcPorNivel.Opcion.GetHabilitarOpcion;
            CB_NIVEL.DataSource = _controlador.OpcPorNivel.Opcion.GetSource;
            CB_NIVEL.SelectedValue = _controlador.OpcPorNivel.Opcion.GetId;
            //
            P_ESTATUS.Enabled = _controlador.OpcPorEstatus.Opcion.GetHabilitarOpcion;
            CB_ESTATUS.DataSource = _controlador.OpcPorEstatus.Opcion.GetSource;
            CB_ESTATUS.SelectedValue = _controlador.OpcPorEstatus.Opcion.GetId;
            //
            P_CREDITO.Enabled = _controlador.OpcPorCredito.Opcion.GetHabilitarOpcion;
            CB_CREDITO.DataSource = _controlador.OpcPorCredito.Opcion.GetSource;
            CB_CREDITO.SelectedValue = _controlador.OpcPorCredito.Opcion.GetId;
            //
            _modoInicializar = false;
        }
        public void setControlador(IVista ctr)
        {
            _controlador = ctr;
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.BtSalida.OpcionIsOK || _controlador.BtAceptar.OpcionIsOK)
            {
                e.Cancel = false;
            }
        }

        private void L_GRUPO_Click(object sender, EventArgs e)
        {
            limpiarGrupo();
        }
        private void L_ESTADO_Click(object sender, EventArgs e)
        {
            limpiarEstado();
        }
        private void L_ZONA_Click(object sender, EventArgs e)
        {
            limpiarZona();
        }
        private void L_VENDEDOR_Click(object sender, EventArgs e)
        {
            limpiarVendedor();
        }
        private void L_COBRADOR_Click(object sender, EventArgs e)
        {
            limpiarCobrador();
        }
        private void L_CATEGORIA_Click(object sender, EventArgs e)
        {
            limpiarCategoria();
        }
        private void L_NIVEL_Click(object sender, EventArgs e)
        {
            limpiarNivel();
        }
        private void L_ESTATUS_Click(object sender, EventArgs e)
        {
            limpiarEstatus();
        }
        private void L_CREDITO_Click(object sender, EventArgs e)
        {
            limpiarCredito();
        }

        private void CB_GRUPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorGrupo.Opcion.setFichaById("");
            if (CB_GRUPO.SelectedIndex != -1)
            {
                _controlador.OpcPorGrupo.Opcion.setFichaById(CB_GRUPO.SelectedValue.ToString());
            }
        }
        private void CB_ESTADO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorEstado.Opcion.setFichaById("");
            if (CB_ESTADO.SelectedIndex != -1)
            {
                _controlador.OpcPorEstado.Opcion.setFichaById(CB_ESTADO.SelectedValue.ToString());
            }
        }
        private void CB_ZONA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorZona.Opcion.setFichaById("");
            if (CB_ZONA.SelectedIndex != -1)
            {
                _controlador.OpcPorZona.Opcion.setFichaById(CB_ZONA.SelectedValue.ToString());
            }
        }
        private void CB_VENDEDOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorVendedor.Opcion.setFichaById("");
            if (CB_VENDEDOR.SelectedIndex != -1)
            {
                _controlador.OpcPorVendedor.Opcion.setFichaById(CB_VENDEDOR.SelectedValue.ToString());
            }
        }
        private void CB_COBRADOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorCobrador.Opcion.setFichaById("");
            if (CB_COBRADOR.SelectedIndex != -1)
            {
                _controlador.OpcPorCobrador.Opcion.setFichaById(CB_COBRADOR.SelectedValue.ToString());
            }
        }
        private void CB_CATEGORIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorCategoria.Opcion.setFichaById("");
            if (CB_CATEGORIA.SelectedIndex != -1)
            {
                _controlador.OpcPorCategoria.Opcion.setFichaById(CB_CATEGORIA.SelectedValue.ToString());
            }
        }
        private void CB_NIVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorNivel.Opcion.setFichaById("");
            if (CB_NIVEL.SelectedIndex != -1)
            {
                _controlador.OpcPorNivel.Opcion.setFichaById(CB_NIVEL.SelectedValue.ToString());
            }
        }
        private void CB_ESTATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorEstatus.Opcion.setFichaById("");
            if (CB_ESTATUS.SelectedIndex != -1)
            {
                _controlador.OpcPorEstatus.Opcion.setFichaById(CB_ESTATUS.SelectedValue.ToString());
            }
        }
        private void CB_CREDITO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.OpcPorCredito.Opcion.setFichaById("");
            if (CB_CREDITO.SelectedIndex != -1)
            {
                _controlador.OpcPorCredito.Opcion.setFichaById(CB_CREDITO.SelectedValue.ToString());
            }
        }

        private void BT_LIMPIAR_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }
        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            AceptarFiltros();
        }
        private void AceptarFiltros()
        {
            _controlador.BtAceptar.Opcion();
            if (_controlador.BtAceptar.OpcionIsOK) 
            {
                salir();
            }
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
             AbandonarFicha();
        }
        private void AbandonarFicha()
        {
             _controlador.BtSalida.Opcion();
             if (_controlador.BtSalida.OpcionIsOK) 
             {
                 salir();
             }
        }
        private void LimpiarFiltros()
        {
            limpiarGrupo();
            limpiarEstado();
            limpiarZona();
            limpiarVendedor();
            limpiarCobrador();
            limpiarCategoria();
            limpiarNivel();
            limpiarEstatus();
            limpiarCredito();
        }
        private void limpiarGrupo()
        {
            CB_GRUPO.SelectedIndex = -1;
        }
        private void limpiarEstado()
        {
            CB_ESTADO.SelectedIndex = -1;
        }
        private void limpiarZona()
        {
            CB_ZONA.SelectedIndex = -1;
        }
        private void limpiarVendedor()
        {
            CB_VENDEDOR.SelectedIndex = -1;
        }
        private void limpiarCobrador()
        {
            CB_COBRADOR.SelectedIndex = -1;
        }
        private void limpiarCategoria()
        {
            CB_CATEGORIA.SelectedIndex = -1;
        }
        private void limpiarNivel()
        {
            CB_NIVEL.SelectedIndex = -1;
        }
        private void limpiarEstatus()
        {
            CB_ESTATUS.SelectedIndex = -1;
        }
        private void limpiarCredito()
        {
            CB_CREDITO.SelectedIndex = -1;
        }
        private void salir()
        {
            this.Close();
        }
    }
}