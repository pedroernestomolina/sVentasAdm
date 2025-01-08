using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir
{
    public partial class Frm : Form
    {
        private IHnd _controlador;
        //
        private void InicializarGrid()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);
            var f2 = new Font("Serif", 10, FontStyle.Bold);
            //
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
            //
            var c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "Descripcion";
            c1.HeaderText = "Detalle";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.MinimumWidth = 220;
            c1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //
            DGV.Columns.Add(c1);
        }
        public Frm()
        {
            InitializeComponent();
            InicializarGrid();
        }
        public void setControlador(HndGestion ctr)
        {
            _controlador = ctr;
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            DGV.DataSource = _controlador.DataSource;
        }
        private void DGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 0)
                {
                    EditarItems();
                }
            }
        }
        private void BT_ACEPTAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }
        //
        private void EditarItems()
        {
            _controlador.EditarItem();
        }
        private void Procesar()
        {
            _controlador.BtProcesar.Opcion();
            if (_controlador.BtProcesar.OpcionIsOK)
            {
                cerrar();
            }
        }
        private void Salir()
        {
            _controlador.BtAbandonar.Opcion();
            if (_controlador.BtAbandonar.OpcionIsOK) 
            {
                cerrar();
            }
        }
        private void cerrar()
        {
            this.Close();
        }
    }
}