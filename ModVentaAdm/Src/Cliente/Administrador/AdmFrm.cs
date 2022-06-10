using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Administrador
{

    public partial class AdmFrm : Form
    {


        private Gestion _controlador;


        public AdmFrm()
        {
            InitializeComponent();
            InicializarDGV();
        }

        private void InicializarDGV()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);

            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.AutoGenerateColumns = false;
            DGV.AllowUserToResizeRows = false;
            DGV.AllowUserToResizeColumns = false;
            DGV.AllowUserToOrderColumns = false;
            DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV.MultiSelect = false;
            DGV.ReadOnly = true;

            var c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "Codigo";
            c1.HeaderText = "Codigo";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.Width = 100;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "CiRif";
            c2.HeaderText = "CiRif";
            c2.Visible = true;
            c2.Width = 110;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "NombreRazonSocial";
            c3.HeaderText = "Nombre/Razón Social";
            c3.Visible = true;
            c3.MinimumWidth = 220;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;
            c3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "Estatus";
            c4.HeaderText = "Estatus";
            c4.Name = "Estatus";
            c4.Visible = true;
            c4.Width = 60;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f;
            c4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(c4);
        }

        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void BT_BUSCAR_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        private void Buscar()
        {
            _controlador.ActivarBusqueda();
            ActualizarData();
            ActualizarFicha();
        }

        private void ActualizarData()
        {
            TB_CADENA.Text = "";
            L_ITEMS.Text = _controlador.cntItem.ToString("n0");
            switch (_controlador.MetodoBusqueda)
            {
                case Enumerados.enumMetodoBusqueda.PorCodigo:
                    RB_BUSCAR_POR_CODIGO.Checked = true;
                    break;
                case Enumerados.enumMetodoBusqueda.PorNombre:
                    RB_BUSCAR_POR_NOMBRE.Checked = true;
                    break;
                case Enumerados.enumMetodoBusqueda.PorRif:
                    RB_BUSCAR_POR_RIF.Checked = true;
                    break;
            }
        }

        private void AdmFrm_Load(object sender, EventArgs e)
        {
            DGV.DataSource = _controlador.Source;
            ActualizarData();
            L_PROVEEDOR.Text = _controlador.Cliente;
        }

        private void TB_CADENA_Leave(object sender, EventArgs e)
        {
            _controlador.setCadena(TB_CADENA.Text);
        }

        private void RB_BUSCAR_POR_CODIGO_CheckedChanged(object sender, EventArgs e)
        {
            _controlador.setMetodoPorCodigo();
            GoInicio();
        }

        private void RB_BUSCAR_POR_NOMBRE_CheckedChanged(object sender, EventArgs e)
        {
            _controlador.setMetodoPorNombre();
            GoInicio();
        }

        private void RB_BUSCAR_POR_RIF_CheckedChanged(object sender, EventArgs e)
        {
            _controlador.setMetodoPorCiRif();
            GoInicio();
        }

        private void BT_LIMPIAR_Click(object sender, EventArgs e)
        {
            LimpiarBusqueda();
            ActualizarData();
            ActualizarFicha();
            GoInicio();
        }

        private void GoInicio()
        {
            TB_CADENA.Focus();
        }

        private void LimpiarBusqueda()
        {
            _controlador.LimpiarBusqueda();
        }

        private void AdmFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                GoInicio();
            }
        }

        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void BT_AGREGAR_FICHA_Click(object sender, EventArgs e)
        {
            AgregarFicha();
        }

        private void AgregarFicha()
        {
            _controlador.AgregarFicha();
            ActualizarData();
            ActualizarFicha();
        }

        private void BT_EDITAR_FICHA_Click(object sender, EventArgs e)
        {
            EditarFicha();
        }

        private void EditarFicha()
        {
            _controlador.EditarFicha();
        }

        private void BT_ARTICULOS_COMPRA_Click(object sender, EventArgs e)
        {
            CompraArticulos();
        }

        private void CompraArticulos()
        {
            _controlador.CompraArticulos();
        }

        private void BT_DOCUMENTOS_Click(object sender, EventArgs e)
        {
            Documentos();
        }

        private void Documentos()
        {
            _controlador.Documentos();
        }

        public void ActualizarFicha()
        {
            L_PROVEEDOR.Text = _controlador.Cliente;
        }

        private void BT_ESTATUS_Click(object sender, EventArgs e)
        {
            ActualizarEstatus();
        }

        private void ActualizarEstatus()
        {
            _controlador.ActualizarEstatus();
        }

        private void DGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in DGV.Rows)
            {
                if (row.Cells["Estatus"].Value.ToString() == "Inactivo")
                {
                    row.Cells["Estatus"].Style.BackColor = Color.Red;
                    row.Cells["Estatus"].Style.ForeColor = Color.White;
                }
            }
        }

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                VisualizarFicha();
            }
        }

        private void VisualizarFicha()
        {
            _controlador.VisualizarFicha();
        }

    }

}