using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Utils.DocLista.Remision
{
    public partial class Frm : Form
    {
        private IRemision _controlador;


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
            c1.DataPropertyName = "Fecha";
            c1.HeaderText = "Fecha";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.Width = 80;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "Tipo";
            c2.HeaderText = "Tipo";
            c2.Visible = true;
            c2.MinimumWidth = 90;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "Serie";
            c3.HeaderText = "Serie";
            c3.Visible = true;
            c3.Width = 40;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;

            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "Documento";
            c4.HeaderText = "Documento";
            c4.Visible = true;
            c4.MinimumWidth = 90;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f1;
            c4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c9 = new DataGridViewTextBoxColumn();
            c9.DataPropertyName = "ImporteDivisa";
            c9.HeaderText = "Importe $";
            c9.Visible = true;
            c9.MinimumWidth = 100;
            c9.HeaderCell.Style.Font = f;
            c9.DefaultCellStyle.Font = f1;
            c9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c9.DefaultCellStyle.Format = "n2";
            c9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c0a = new DataGridViewTextBoxColumn();
            c0a.DataPropertyName = "Estatus";
            c0a.Name = "Estatus";
            c0a.HeaderText = "*";
            c0a.Visible = true;
            c0a.Width = 100;
            c0a.HeaderCell.Style.Font = f;
            c0a.DefaultCellStyle.Font = f1;
            c0a.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(c4);
            DGV.Columns.Add(c9);
            DGV.Columns.Add(c0a);
        }
        public Frm()
        {
            InitializeComponent();
            InicializarDGV();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            DGV.DataSource = _controlador.Items.Source_Get;
            L_ITEMS_CNT.Text = _controlador.Items.Cnt.ToString(); ;
        }
        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                SeleccionarDocumento();
            }
        }
        public void setControlador(IRemision ctr)
        {
            _controlador = ctr;
        }


        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }


        private void SeleccionarDocumento()
        {
            _controlador.SeleccionarItem();
            if (_controlador.ItemSeleccionadoIsOk)
            {
                Salir();
            }
        }
        private void AbandonarFicha()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOK) 
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