using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Documentos
{

    public partial class DocumentosFrm : Form
    {

        private Gestion _controlador;


        
        public DocumentosFrm()
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
            c4.MinimumWidth= 90;
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

        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void CompraArticulosFrm_Load(object sender, EventArgs e)
        {
            L_CLIENTE.Text = _controlador.Cliente;
            DTP_DESDE.Value = _controlador.Desde;
            DTP_HASTA.Value = _controlador.Hasta;
            DGV.DataSource = _controlador.Source;
            L_ITEMS_CNT.Text = _controlador.ItemsCnt.ToString(); ;
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            this.Close();
        }

        private void BT_BUSCAR_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            _controlador.Buscar();
            L_ITEMS_CNT.Text = _controlador.ItemsCnt.ToString(); ;
        }

        private void DTP_DESDE_ValueChanged(object sender, EventArgs e)
        {
            _controlador.setDesde(DTP_DESDE.Value);
        }

        private void DTP_HASTA_ValueChanged(object sender, EventArgs e)
        {
            _controlador.setHasta(DTP_HASTA.Value);
        }

        private void DGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in DGV.Rows)
            {
                if (row.Cells["Estatus"].Value.ToString() == "ANULADO")
                {
                    row.Cells["Estatus"].Style.BackColor = Color.Red;
                    row.Cells["Estatus"].Style.ForeColor = Color.White;
                }
            }
        }

        private void BT_LIMPIAR_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            _controlador.Limpiar();
            DTP_DESDE.Value = _controlador.Desde;
            DTP_HASTA.Value = _controlador.Hasta;
            L_ITEMS_CNT.Text = _controlador.ItemsCnt.ToString(); ;
        }

        private void BT_IMPRIMIR_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void Imprimir()
        {
            this.TopMost = false;
            _controlador.Imprimir();
            this.TopMost = true;
        }

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                VisualizarDocumento();
                SeleccionarDocumento();
            }
        }

        private void SeleccionarDocumento()
        {
            _controlador.SeleccionarDocumento();
            if (_controlador.SeleccionarDocumentoIsOk) 
            {
                Salir();
            }
        }

        private void VisualizarDocumento()
        {
            this.TopMost = false;
            _controlador.VisualizarDocumento();
            this.TopMost = true;
        }

    }

}