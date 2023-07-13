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
            c1.DataPropertyName = "DocFecha";
            c1.HeaderText = "Fecha";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.Width = 70;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "DocTipo";
            c2.HeaderText = "Tipo";
            c2.Visible = true;
            c2.MinimumWidth = 80;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "DocNumero";
            c3.HeaderText = "Documento";
            c3.Visible = true;
            c3.Width = 90;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;

            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "NombreRazonSocial";
            c4.HeaderText = "Nombre";
            c4.Visible = true;
            c4.MinimumWidth = 220;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f1;
            c4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c5 = new DataGridViewTextBoxColumn();
            c5.DataPropertyName = "CiRif";
            c5.HeaderText = "CiRif";
            c5.Visible = true;
            c5.Width = 100;
            c5.HeaderCell.Style.Font = f;
            c5.DefaultCellStyle.Font = f1;

            var c6 = new DataGridViewTextBoxColumn();
            c6.DataPropertyName = "CntRenglones";
            c6.HeaderText = "Rengl";
            c6.Visible = true;
            c6.Width = 60;
            c6.HeaderCell.Style.Font = f;
            c6.DefaultCellStyle.Font = f1;
            c6.DefaultCellStyle.Format = "n0";
            c6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var c7 = new DataGridViewTextBoxColumn();
            c7.DataPropertyName = "Monto";
            c7.HeaderText = "Monto($)";
            c7.Visible = true;
            c7.Width = 90;
            c7.HeaderCell.Style.Font = f;
            c7.DefaultCellStyle.Font = f1;
            c7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c7.DefaultCellStyle.Format = "n2";

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(c4);
            DGV.Columns.Add(c5);
            DGV.Columns.Add(c6);
            DGV.Columns.Add(c7);
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