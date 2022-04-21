using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Administrador.Documentos
{

    public partial class AdmDocFrm : Form
    {


        private IGestion _controlador;


        public AdmDocFrm()
        {
            InitializeComponent();
            InicializarCombos();
        }


        public void setControlador(IGestion ctr)
        {
            _controlador = ctr;
        }

        private void InicializarCombos()
        {
            CB_SUCURSAL.DisplayMember = "descripcion";
            CB_SUCURSAL.ValueMember = "auto";
            CB_TIPO_DOC.DisplayMember = "descripcion";
            CB_TIPO_DOC.ValueMember = "auto";
        }

        private void InicializarGrid()
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
            c1.DataPropertyName = "FechaHora";
            c1.HeaderText = "Fecha";
            c1.Visible = true;
            c1.Width = 100;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "DocNombre";
            c2.HeaderText = "Tipo";
            c2.Visible = true;
            c2.Width = 100;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "Documento";
            c3.HeaderText = "Documento";
            c3.Visible = true;
            c3.Width = 80;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var cA = new DataGridViewTextBoxColumn();
            cA.DataPropertyName = "Aplica";
            cA.HeaderText = "Aplica";
            cA.Visible = true;
            cA.Width = 100;
            cA.HeaderCell.Style.Font = f;
            cA.DefaultCellStyle.Font = f1;

            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "SucursalCod";
            c4.HeaderText = "Sucursal";
            c4.Name = "Sucursal";
            c4.Visible = true;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f1;
            c4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c4.Width = 60;

            var c5 = new DataGridViewTextBoxColumn();
            c5.DataPropertyName = "ClienteNombre";
            c5.HeaderText = "Cliente";
            c5.Visible = true;
            c5.MinimumWidth = 220;
            c5.HeaderCell.Style.Font = f;
            c5.DefaultCellStyle.Font = f1;
            c5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c5A = new DataGridViewTextBoxColumn();
            c5A.DataPropertyName = "ClienteCiRif";
            c5A.HeaderText = "Ci/Rif";
            c5A.Visible = true;
            c5A.Width = 90;
            c5A.HeaderCell.Style.Font = f;
            c5A.DefaultCellStyle.Font = f1;
            
            var c9 = new DataGridViewTextBoxColumn();
            c9.DataPropertyName = "Signo";
            c9.HeaderText = "+/-";
            c9.Visible = true;
            c9.Width = 40;
            c9.HeaderCell.Style.Font = f;
            c9.DefaultCellStyle.Font = f2;
            c9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var c5B = new DataGridViewTextBoxColumn();
            c5B.DataPropertyName = "Importe";
            c5B.HeaderText = "Importe";
            c5B.Visible = true;
            c5B.Width = 120;
            c5B.HeaderCell.Style.Font = f;
            c5B.DefaultCellStyle.Font = f1;
            c5B.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c5B.DefaultCellStyle.Format = "n2";

            var c6 = new DataGridViewTextBoxColumn();
            c6.DataPropertyName = "ImporteDivisa";
            c6.HeaderText = "Importe $";
            c6.Visible = true;
            c6.Width = 90;
            c6.HeaderCell.Style.Font = f;
            c6.DefaultCellStyle.Font = f1;
            c6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c6.DefaultCellStyle.Format = "n2";

            var c7 = new DataGridViewTextBoxColumn();
            c7.DataPropertyName = "Situacion";
            c7.Name = "Situacion";
            c7.HeaderText = "Situación";
            c7.Visible = true;
            c7.Width = 80;
            c7.HeaderCell.Style.Font = f;
            c7.DefaultCellStyle.Font = f1;
            c7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var c8 = new DataGridViewTextBoxColumn();
            c8.DataPropertyName = "IsAnulado";
            c8.Name = "IsAnulado";
            c8.Visible = false;
            c8.Width = 0;
            c8.HeaderCell.Style.Font = f;
            c8.DefaultCellStyle.Font = f1;

            var c8A = new DataGridViewTextBoxColumn();
            c8A.DataPropertyName = "Estatus";
            c8A.HeaderText= "Estatus";
            c8A.Name = "Estatus";
            c8A.Visible = true;
            c8A.Width = 80;
            c8A.HeaderCell.Style.Font = f;
            c8A.DefaultCellStyle.Font = f1;
            c8A.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var cX = new DataGridViewTextBoxColumn();
            cX.DataPropertyName = "SucursalDesc";
            cX.Name = "SucursalDesc";
            cX.Visible = false;


            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(cA);
            DGV.Columns.Add(c4);
            DGV.Columns.Add(c5);
            DGV.Columns.Add(c5A);
            DGV.Columns.Add(c9);
            DGV.Columns.Add(c5B);
            DGV.Columns.Add(c6);
            DGV.Columns.Add(c7);
            DGV.Columns.Add(c8);
            DGV.Columns.Add(c8A);
            DGV.Columns.Add(cX);
        }

        private void DGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in DGV.Rows)
            {
                if ((bool)row.Cells["IsAnulado"].Value == true)
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                    row.Cells["Estatus"].Style.BackColor = Color.Red;
                    row.Cells["Estatus"].Style.ForeColor = Color.White;
                }
            }
        }

        private bool _modoInicializa;
        private void AdministradorFrm_Load(object sender, EventArgs e)
        {
            InicializarGrid();

            _modoInicializa = true;
            CB_SUCURSAL.DataSource = _controlador.SucursalSource;
            CB_TIPO_DOC.DataSource = _controlador.TipoDocSource;
            DGV.DataSource = _controlador.ItemsSource;
            DGV.Refresh();
            DGV.Columns[0].Frozen = true;
            DGV.Columns[1].Frozen = true;
            DGV.Columns[2].Frozen = true;
            DGV.Columns[3].Frozen = true;
            _modoInicializa = false;

            Actualizar();
            ActualizarControles();
        }

        private void ActualizarControles()
        {
            DTP_DESDE.Value = _controlador.GetDesde;
            DTP_HASTA.Value = _controlador.GetHasta;
            CB_SUCURSAL.SelectedValue = _controlador.GetIdSucursal;
            CB_TIPO_DOC.SelectedValue = _controlador.GetIdTipoDoc;
        }

        private void Actualizar()
        {
            L_ITEMS.Text = _controlador.ItemsEncontrados;
        }

        private void BT_BUSCAR_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            _controlador.Buscar();
            Actualizar();
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            this.Close();
        }

        private void BT_ANULAR_Click(object sender, EventArgs e)
        {
            AnularItem();
        }

        private void AnularItem()
        {
            _controlador.AnularItem();
            DGV.Refresh();
        }

        private void DTP_DESDE_ValueChanged(object sender, EventArgs e)
        {
            _controlador.setFechaDesde(DTP_DESDE.Value);
        }

        private void DTP_HASTA_ValueChanged(object sender, EventArgs e)
        {
            _controlador.setFechaHasta(DTP_HASTA.Value);
        }

        private void CB_TIPO_DOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializa)
                return;

            var id = "";
            if (CB_TIPO_DOC.SelectedIndex != -1)
                id = CB_TIPO_DOC.SelectedValue.ToString();
            _controlador.setTipoDoc(id);
        }

        private void CB_SUCURSAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializa)
                return;

            var id = "";
            if (CB_SUCURSAL.SelectedIndex != -1)
                id = CB_SUCURSAL.SelectedValue.ToString();
            _controlador.setSucursal(id);
        }

        private void L_TIPO_DOC_Click(object sender, EventArgs e)
        {
            CB_TIPO_DOC.SelectedIndex = -1;
        }

        private void L_SUCURSAL_Click(object sender, EventArgs e)
        {
            CB_SUCURSAL.SelectedIndex = -1;
        }

        private void BT_LIMPIAR_FILTROS_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            _controlador.LimpiarFiltros();

            DTP_DESDE.Value = DateTime.Now;
            DTP_HASTA.Value = DateTime.Now;
            CB_SUCURSAL.SelectedIndex = -1;
            CB_TIPO_DOC.SelectedIndex = -1;
        }

        private void BT_LIMPIAR_DATA_Click(object sender, EventArgs e)
        {
            LimpiarData();
        }

        private void LimpiarData()
        {
            _controlador.LimpiarData();
            Actualizar();
        }

        private void BT_VISUALIZAR_Click(object sender, EventArgs e)
        {
            VisualizarDocumento();
        }

        private void VisualizarDocumento()
        {
            _controlador.VisualizarDocumento();
        }

        private void BT_IMPRIMIR_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void Imprimir()
        {
            _controlador.Imprimir();
        }

        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void BT_CORRECTOR_Click(object sender, EventArgs e)
        {
            CorrectorDocumentos();
        }

        private void CorrectorDocumentos()
        {
            _controlador.CorrectorDocumento();
        }

        private void DGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == DGV.Columns["Sucursal"].Index) && e.Value != null)
            { 
                DataGridViewCell cell =DGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = DGV.Rows[e.RowIndex].Cells["SucursalDesc"].Value.ToString();
            }
        }

        private void BT_FILTROS_Click(object sender, EventArgs e)
        {
            Filtros();
        }

        private void Filtros()
        {
            _controlador.Filtros();
            ActualizarControles();
        }

        private void DGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                if (e.ColumnIndex >= 0)
                {
                    VerAnulacion();
                }
            }
        }

        private void VerAnulacion()
        {
            _controlador.VerAnulacion();
        }

    }

}