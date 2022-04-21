using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.ReportesCliente.Filtro
{

    public partial class FiltroFrm : Form
    {

        private Gestion _controlador;


        public FiltroFrm()
        {
            InitializeComponent();
            InicializaControles();
        }


        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void InicializaControles()
        {
            CB_GRUPO.ValueMember = "Id";
            CB_GRUPO.DisplayMember = "Descripcion";
            CB_ESTADO.ValueMember = "Id";
            CB_ESTADO.DisplayMember = "Descripcion";
            CB_ZONA.ValueMember = "Id";
            CB_ZONA.DisplayMember = "Descripcion";
            CB_VENDEDOR.ValueMember = "Id";
            CB_VENDEDOR.DisplayMember = "Descripcion";
            CB_COBRADOR.ValueMember = "Id";
            CB_COBRADOR.DisplayMember = "Descripcion";
            CB_CATEGORIA.ValueMember = "Id";
            CB_CATEGORIA.DisplayMember = "Descripcion";
            CB_NIVEL.ValueMember = "Id";
            CB_NIVEL.DisplayMember = "Descripcion";
            CB_TARIFA.ValueMember = "Id";
            CB_TARIFA.DisplayMember = "Descripcion";
            CB_ESTATUS.ValueMember = "Id";
            CB_ESTATUS.DisplayMember = "Descripcion";
            CB_CREDITO.ValueMember = "Id";
            CB_CREDITO.DisplayMember = "Descripcion";
        }

        private bool _modoInicializar;
        private void FiltrosFrm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            CB_GRUPO.DataSource = _controlador.SourceGrupo;
            CB_ESTADO.DataSource = _controlador.SourceEstado;
            CB_ZONA.DataSource = _controlador.SourceZona;
            CB_VENDEDOR.DataSource = _controlador.SourceVendedor;
            CB_COBRADOR.DataSource = _controlador.SourceCobrador;
            CB_CATEGORIA.DataSource = _controlador.SourceCategoria;
            CB_NIVEL.DataSource = _controlador.SourceNivel;
            CB_TARIFA.DataSource = _controlador.SourceTarifa;
            CB_ESTATUS.DataSource = _controlador.SourceEstatus;
            CB_CREDITO.DataSource = _controlador.SourceCredito;

            L_GRUPO.Enabled = _controlador.ActivarGrupo;
            CB_GRUPO.Enabled = _controlador.ActivarGrupo;
            L_ESTADO.Enabled = _controlador.ActivarEstado;
            CB_ESTADO.Enabled = _controlador.ActivarEstado;
            L_ZONA.Enabled = _controlador.ActivarZona;
            CB_ZONA.Enabled = _controlador.ActivarZona;
            L_VENDEDOR.Enabled = _controlador.ActivarVendedor;
            CB_VENDEDOR.Enabled = _controlador.ActivarVendedor;
            L_COBRADOR.Enabled = _controlador.ActivarCobrador;
            CB_COBRADOR.Enabled = _controlador.ActivarCobrador;
            L_CATEGORIA.Enabled = _controlador.ActivarCategoria;
            CB_CATEGORIA.Enabled = _controlador.ActivarCategoria;
            L_NIVEL.Enabled = _controlador.ActivarNivel;
            CB_NIVEL.Enabled = _controlador.ActivarNivel;
            L_TARIFA.Enabled = _controlador.ActivarTarifa;
            CB_TARIFA.Enabled = _controlador.ActivarTarifa;
            L_ESTATUS.Enabled = _controlador.ActivarEstatus;
            CB_ESTATUS.Enabled= _controlador.ActivarEstatus;
            L_CREDITO.Enabled = _controlador.ActivarCredito;
            CB_CREDITO.Enabled = _controlador.ActivarCredito;
            _modoInicializar = false;

            LimpiarFiltros();
        }

        private void BT_LIMPIAR_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            LimpiarGrupo();
            LimpiarEstado();
            LimpiarZona();
            LimpiarVendedor();
            LimpiarCobrador();
            LimpiarCategoria();
            LimpiarNivel();
            LimpiarTarifa();
            LimpiarEstatus();
            LimpiarCredito();
        }

         private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            _controlador.Salir();
            this.Close();
        }

        private void BT_FILTRAR_Click(object sender, EventArgs e)
        {
            _controlador.Filtrar();
            if (_controlador.IsOk)
            {
                Salir();
            }
        }

        private void FiltroFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_controlador.IsOk)
            {
                e.Cancel = true;
            }
        }

        private void L_GRUPO_Click(object sender, EventArgs e)
        {
            LimpiarGrupo();
        }

        private void LimpiarGrupo()
        {
            CB_GRUPO.SelectedIndex = -1;
        }

        private void L_ESTADO_Click(object sender, EventArgs e)
        {
            LimpiarEstado();
        }

        private void LimpiarEstado()
        {
            CB_ESTADO.SelectedIndex = -1;
        }

        private void L_ZONA_Click(object sender, EventArgs e)
        {
            LimpiarZona();
        }

        private void LimpiarZona()
        {
            CB_ZONA.SelectedIndex = -1;
        }

        private void L_VENDEDOR_Click(object sender, EventArgs e)
        {
            LimpiarVendedor();
        }

        private void LimpiarVendedor()
        {
            CB_VENDEDOR.SelectedIndex = -1;
        }

        private void L_COBRADOR_Click(object sender, EventArgs e)
        {
            LimpiarCobrador();
        }

        private void LimpiarCobrador()
        {
            CB_COBRADOR.SelectedIndex = -1;
        }

        private void L_CATEGORIA_Click(object sender, EventArgs e)
        {
            LimpiarCategoria();
        }

        private void LimpiarCategoria()
        {
            CB_CATEGORIA.SelectedIndex = -1;
        }

        private void L_NIVEL_Click(object sender, EventArgs e)
        {
            LimpiarNivel();
        }

        private void LimpiarNivel()
        {
            CB_NIVEL.SelectedIndex = -1;
        }

        private void L_TARIFA_Click(object sender, EventArgs e)
        {
            LimpiarTarifa();
        }

        private void LimpiarTarifa()
        {
            CB_TARIFA.SelectedIndex = -1;
        }

        private void L_ESTATUS_Click(object sender, EventArgs e)
        {
            LimpiarEstatus();
        }

        private void LimpiarEstatus()
        {
            CB_ESTATUS.SelectedIndex = -1;
        }

        private void L_CREDITO_Click(object sender, EventArgs e)
        {
            LimpiarCredito();
        }

        private void LimpiarCredito()
        {
            CB_CREDITO.SelectedIndex = -1;
        }


        private void CB_GRUPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setGrupo("");
            if (CB_GRUPO.SelectedIndex != -1) 
            {
                _controlador.setGrupo(CB_GRUPO.SelectedValue.ToString());
            }
        }

        private void CB_ESTADO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setEstado("");
            if (CB_ESTADO.SelectedIndex != -1)
            {
                _controlador.setEstado(CB_ESTADO.SelectedValue.ToString());
            }
        }

        private void CB_ZONA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setZona("");
            if (CB_ZONA.SelectedIndex != -1)
            {
                _controlador.setZona(CB_ZONA.SelectedValue.ToString());
            }
        }

        private void CB_VENDEDOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setVendedor("");
            if (CB_VENDEDOR.SelectedIndex != -1)
            {
                _controlador.setVendedor(CB_VENDEDOR.SelectedValue.ToString());
            }
        }

        private void CB_COBRADOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setCobrador("");
            if (CB_COBRADOR.SelectedIndex != -1)
            {
                _controlador.setCobrador(CB_COBRADOR.SelectedValue.ToString());
            }
        }

        private void CB_CATEGORIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setCategoria("");
            if (CB_CATEGORIA.SelectedIndex != -1)
            {
                _controlador.setCategoria(CB_CATEGORIA.SelectedValue.ToString());
            }
        }

        private void CB_NIVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setNivel("");
            if (CB_NIVEL.SelectedIndex != -1)
            {
                _controlador.setNivel(CB_NIVEL.SelectedValue.ToString());
            }
        }

        private void CB_TARIFA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setTarifa("");
            if (CB_TARIFA.SelectedIndex != -1)
            {
                _controlador.setTarifa(CB_TARIFA.SelectedValue.ToString());
            }
        }

        private void CB_ESTATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setEstatus("");
            if (CB_ESTATUS.SelectedIndex != -1)
            {
                _controlador.setEstatus(CB_ESTATUS.SelectedValue.ToString());
            }
        }

        private void CB_CREDITO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar)
                return;
            _controlador.setCredito("");
            if (CB_CREDITO.SelectedIndex != -1)
            {
                _controlador.setCredito(CB_CREDITO.SelectedValue.ToString());
            }
        }

    }

}