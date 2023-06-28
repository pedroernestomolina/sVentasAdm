using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Aliado
{
    public partial class Frm : Form
    {
        private IAliado _controlador;


        public Frm()
        {
            InitializeComponent();
            InicializaGrid();
            InicializaCB();
        }
        private void InicializaGrid()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);
            var f2 = new Font("Serif", 10, FontStyle.Bold);

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
            c1.Width = 100;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "Desc";
            c2.HeaderText = "Descripción";
            c2.Visible = true;
            c2.MinimumWidth = 150;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "ciRif";
            c3.HeaderText = "CiRif";
            c3.Visible = true;
            c3.Width = 100;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
        }
        private void InicializaCB()
        {
            CB_METODO_BUSQ.ValueMember = "id";
            CB_METODO_BUSQ.DisplayMember = "desc";
        }

        private bool _modoInicio;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicio = true;
            DGV.DataSource = _controlador.Source_GetData;
            CB_METODO_BUSQ.DataSource = _controlador.CompBusqueda.MetodoBusqueda_GetSource;
            CB_METODO_BUSQ.SelectedValue = _controlador.CompBusqueda.MetodoBusqueda_GetId;
            TB_CADENA_BUSQ.Text = _controlador.CompBusqueda.GetCadena;
            L_ITEMS.Text = "Items Encontrados: " + _controlador.CntItem_Get.ToString("n0");
            _modoInicio = false;
        }
        private void CTRL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                ItemSeleccionado();
            }
        }
        public void setControlador(IAliado ctr)
        {
            _controlador = ctr;
        }


        private void CB_METODO_BUSQ_Leave(object sender, EventArgs e)
        {
            if (_modoInicio) return;
            _controlador.CompBusqueda.setMetodo("");
            if (CB_METODO_BUSQ.SelectedIndex != -1) 
            {
                _controlador.CompBusqueda.setMetodo(CB_METODO_BUSQ.SelectedValue.ToString());
            }
        }
        private void TB_CADENA_BUSQ_Leave(object sender, EventArgs e)
        {
            _controlador.CompBusqueda.setCadenaBuscar(TB_CADENA_BUSQ.Text.Trim());
        }
        private void BT_BUSCAR_ALIADO_Click(object sender, EventArgs e)
        {
            _controlador.BuscarAliados();
            L_ITEMS.Text = "Items Encontrados: " + _controlador.CntItem_Get.ToString("n0");
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }


        private void ItemSeleccionado()
        {
            _controlador.ItemSeleccionado();
            if (_controlador.ItemSeleccionadoIsOk)
            {
                Salir();
            }
        }
        private void Salir()
        {
            this.Close();
        }
    }
}