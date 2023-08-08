using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Maestro
{
    public partial class Frm : Form
    {
        private Utils.Maestro.IMaestro _controlador;


        private void InicializaGrid()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 10, FontStyle.Regular);

            DGV.RowHeadersVisible = false;
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
            c1.DataPropertyName = "Descripcion";
            c1.HeaderText = "Nombre";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.MinimumWidth = 180;
            c1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "Codigo";
            c2.HeaderText = "Codigo";
            c2.Visible = true;
            c2.Width = 120;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;

            DGV.Columns.Add(c2);
            DGV.Columns.Add(c1);
        }


        public Frm()
        {
            InitializeComponent();
            InicializaGrid();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            DGV.DataSource = _controlador.DataSource_Get;
            L_MAESTRO.Text = _controlador.TituloMaestro_Get;
            ActualizarItems();
        }
        public void setControlador(Utils.Maestro.IMaestro ctr)
        {
            _controlador = ctr;
        }


        private void BT_AGREGAR_Click(object sender, EventArgs e)
        {
            AgregarItem();
        }
        private void BT_EDITAR_Click(object sender, EventArgs e)
        {
            EditarItem();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }
        private void TSM_ARCHIVO_Salir_Click(object sender, EventArgs e)
        {
            Salir();
        }


        private void ActualizarItems()
        {
            L_ITEMS.Text = _controlador.CntItems_Get.ToString("n0");
        }
        private void AgregarItem()
        {
            _controlador.AgregarItem();
            ActualizarItems();
        }
        private void EditarItem()
        {
            _controlador.EditarItem();
            ActualizarItems();
        }
        private void Salir()
        {
            this.Close();
        }
    }
}