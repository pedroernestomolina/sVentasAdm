using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.BuscarProducto
{

    public partial class BuscarProductoFrm : Form
    {

        private Gestion _controlador;

        
        public BuscarProductoFrm()
        {
            InitializeComponent();
            InicializarDGV();
        }

        private void InicializarDGV()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);
            var f2 = new Font("Serif", 6, FontStyle.Bold);

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

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "Descripcion";
            c3.HeaderText = "Descripcion";
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
            c4.DefaultCellStyle.Font = f2;
            c4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(c4);
        }

        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private BindingSource _bs;
        private void BuscarClienteFrm_Load(object sender, EventArgs e)
        {
            _bs = _controlador.ItemsSource;
            _bs.CurrentChanged +=_bs_CurrentChanged;  
            DGV.DataSource = _controlador.ItemsSource;
            ActualizarData();
        }

        private void _bs_CurrentChanged(object sender, EventArgs e)
        {
            ActualizarData();
        }

        private void ActualizarData()
        {
            L_INFO_PRODUCTO.Text = _controlador.Inf_Producto;
            L_INFO_EX_ACTUAL.Text = _controlador.Inf_ExistenciaActual.ToString("n2");
            L_INFO_EX_DISP.Text = _controlador.Inf_ExistenciaDisponible.ToString("n2");
            L_ITEMS.Text = _controlador.CntItem.ToString();
            //
            L_INFO_EMP_1.Text = _controlador.Inf_EmpqCont_1;
            L_INFO_PNETO_1.Text = _controlador.Inf_PNeto_1.ToString("n2");
            L_INFO_PFULL_1.Text = _controlador.Inf_PFull_1.ToString("n2");
            L_INFO_PDIVISA_1.Text = _controlador.Inf_PDivisa_1.ToString("n2");
            //
            L_INFO_EMP_2.Text = _controlador.Inf_EmpqCont_2;
            L_INFO_PNETO_2.Text = _controlador.Inf_PNeto_2.ToString("n2");
            L_INFO_PFULL_2.Text = _controlador.Inf_PFull_2.ToString("n2");
            L_INFO_PDIVISA_2.Text = _controlador.Inf_PDivisa_2.ToString("n2");
            //
            L_INFO_EMP_3.Text = _controlador.Inf_EmpqCont_3;
            L_INFO_PNETO_3.Text = _controlador.Inf_PNeto_3.ToString("n2");
            L_INFO_PFULL_3.Text = _controlador.Inf_PFull_3.ToString("n2");
            L_INFO_PDIVISA_3.Text = _controlador.Inf_PDivisa_3.ToString("n2");
            //
            L_INFO_EMP_4.Text = _controlador.Inf_EmpqCont_4;
            L_INFO_PNETO_4.Text = _controlador.Inf_PNeto_4.ToString("n2");
            L_INFO_PFULL_4.Text = _controlador.Inf_PFull_4.ToString("n2");
            L_INFO_PDIVISA_4.Text = _controlador.Inf_PDivisa_4.ToString("n2");
            //
            L_INFO_EMP_5.Text = _controlador.Inf_EmpqCont_5;
            L_INFO_PNETO_5.Text = _controlador.Inf_PNeto_5.ToString("n2");
            L_INFO_PFULL_5.Text = _controlador.Inf_PFull_5.ToString("n2");
            L_INFO_PDIVISA_5.Text = _controlador.Inf_PDivisa_5.ToString("n2");
            //
            L_INFO_EMP_6.Text = _controlador.Inf_EmpqCont_6;
            L_INFO_PNETO_6.Text = _controlador.Inf_PNeto_6.ToString("n2");
            L_INFO_PFULL_6.Text = _controlador.Inf_PFull_6.ToString("n2");
            L_INFO_PDIVISA_6.Text = _controlador.Inf_PDivisa_6.ToString("n2");
            //
        }

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                SeleccionarItem();
            }
        }

        private void SeleccionarItem()
        {
            _controlador.SeleccionarItem();
            if (_controlador.ItemSeleccionadoIsOk) 
            {
                Salir();
            }
        }

        private void Salir()
        {
            this.Close();
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void DGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in DGV.Rows)
            {
                if (row.Cells["Estatus"].Value.ToString() != "")
                {
                    row.Cells["Estatus"].Style.BackColor = Color.Red;
                    row.Cells["Estatus"].Style.ForeColor = Color.White;
                }
            }
        }

        private void DGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (DGV.CurrentRow != null)
                {
                    if (DGV.CurrentRow.Index > -1)
                    {
                        SeleccionarItem();
                    }
                }
            }
        }

    }

}