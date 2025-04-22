using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal
{
    public partial class ToolsFrm : Form
    {
        private ITools _controlador;
        private bool _modoInicializar;
        //
        private void InicializaDGV_1()
        {
            var f = new Font("Serif", 7, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);
            //
            DGV_1.RowHeadersVisible = false;
            DGV_1.AllowUserToAddRows = false;
            DGV_1.AllowUserToDeleteRows = false;
            DGV_1.AutoGenerateColumns = false;
            DGV_1.AllowUserToResizeRows = false;
            DGV_1.AllowUserToResizeColumns = false;
            DGV_1.AllowUserToOrderColumns = false;
            DGV_1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_1.MultiSelect = false;
            DGV_1.ReadOnly = true;
            //
            var c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "CiRif";
            c1.HeaderText = "CI/RIF";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.Width = 100;
            //
            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "NombreRazonSocial";
            c2.HeaderText = "Nombre/Razón Social";
            c2.Visible = true;
            c2.MinimumWidth = 200;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //
            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "MontoImporte";
            c3.HeaderText = "Importe";
            c3.Visible = true;
            c3.Width = 75;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c3.DefaultCellStyle.Format = "n2";
            //
            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "MontoAcumulado";
            c4.HeaderText = "Acumulado";
            c4.Visible = true;
            c4.Width = 75;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f1;
            c4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c4.DefaultCellStyle.Format = "n2";
            //
            var c8 = new DataGridViewTextBoxColumn();
            c8.DataPropertyName = "MontoResta";
            c8.HeaderText = "Resta";
            c8.Visible = true;
            c8.Width = 75;
            c8.HeaderCell.Style.Font = f;
            c8.DefaultCellStyle.Font = f1;
            c8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c8.DefaultCellStyle.Format = "n2";
            //
            var c5 = new DataGridViewTextBoxColumn();
            c5.DataPropertyName = "CntDocPend";
            c5.HeaderText = "Doc/Pend";
            c5.Visible = true;
            c5.Width = 80;
            c5.HeaderCell.Style.Font = f;
            c5.DefaultCellStyle.Font = f;
            c5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            var c6 = new DataGridViewTextBoxColumn();
            c6.DataPropertyName = "CntFactPend";
            c6.HeaderText = "F/Pend";
            c6.Visible = true;
            c6.Width = 60;
            c6.HeaderCell.Style.Font = f;
            c6.DefaultCellStyle.Font = f1;
            c6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            var c7 = new DataGridViewTextBoxColumn();
            c7.DataPropertyName = "MontoLimiteCredito";
            c7.HeaderText = "Lim/Monto";
            c7.Name = "Estatus";
            c7.Visible = true;
            c7.Width = 80;
            c7.HeaderCell.Style.Font = f;
            c7.DefaultCellStyle.Font = f;
            c7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            var c9 = new DataGridViewTextBoxColumn();
            c9.DataPropertyName = "MontoPorAnticipo";
            c9.HeaderText = "Anticipo";
            c9.Visible = true;
            c9.Width = 75;
            c9.HeaderCell.Style.Font = f;
            c9.DefaultCellStyle.Font = f1;
            c9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            var cA = new DataGridViewTextBoxColumn();
            cA.DataPropertyName = "MontoPorCreditos";
            cA.HeaderText = "Credito";
            cA.Visible = true;
            cA.Width = 75;
            cA.HeaderCell.Style.Font = f;
            cA.DefaultCellStyle.Font = f1;
            cA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            DGV_1.Columns.Add(c1);
            DGV_1.Columns.Add(c2);
            DGV_1.Columns.Add(c3);
            DGV_1.Columns.Add(c9);
            DGV_1.Columns.Add(cA);
            DGV_1.Columns.Add(c4);
            DGV_1.Columns.Add(c8);
            //DGV_1.Columns.Add(c5);
            DGV_1.Columns.Add(c6);
        }
        public ToolsFrm()
        {
            InitializeComponent();
            InicializaDGV_1();
        }
        public void setControlador(ITools ctr)
        {
            _controlador = ctr;
        }
        private void ToolsFrm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            DGV_1.DataSource = _controlador.CtasPendGetSource;
            ActualizarDataPanel();
            TB_BUSCAR_NOMBRE.Text = _controlador.TextoFiltrar;
            CHB_MOSTRAR_CTAS_CERO.Checked = _controlador.MostrarCtasEnCero;
            _modoInicializar = false;
            //
            BT_AGREGAR_ANTICIPO.Visible = Sistema.Fabrica.Cxc_AgregarAnticipos;
            BT_ADM_DOC_ANTICIPO.Visible = Sistema.Fabrica.Cxc_AdmDocAnticipos;
        }
        private void ToolsFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOk)
            {
                e.Cancel = false;
            }
        }
        private void CTRL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void TB_BUSCAR_NOMBRE_Leave(object sender, EventArgs e)
        {
            var txt = TB_BUSCAR_NOMBRE.Text.Trim().ToUpper();
            _controlador.FiltrarPor(txt);
        }
        private void CHB_MOSTRAR_CTAS_CERO_CheckedChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) return;
            _controlador.setMostrarCtasCero();
        }
        //
        //
        private void BT_BUSCAR_Click(object sender, EventArgs e)
        {
            BuscarCtasPendientes();
        }
        private void BT_AGREGAR_CTA_Click(object sender, EventArgs e)
        {
            AgregarCta();
        }
        private void BT_AGREGAR_NCR_ADM_Click(object sender, EventArgs e)
        {
            AgregarNCrAdm();
        }
        private void BT_AGREGAR_NDB_ADM_Click(object sender, EventArgs e)
        {
            AgregarNDbAdm();
        }
        private void BT_GESTION_PAGO_Click(object sender, EventArgs e)
        {
            GestionPago();
        }
        private void BT_AGREGAR_ANTICIPO_Click(object sender, EventArgs e)
        {
            AgregarAnticipo();
        }
        private void BT_ADM_DOC_ANTICIPO_Click(object sender, EventArgs e)
        {
            AdmDocAnticipos();
        }
        private void BT_ADM_PAGOS_REC_Click(object sender, EventArgs e)
        {
            AdmPagosRecibidos();
        }
        private void BT_REPORTE_CTAS_Click(object sender, EventArgs e)
        {
            ListadoCtasPend();
        }
        private void BT_EDO_CTA_Click(object sender, EventArgs e)
        {
            EdoCta();
        }
        private void BT_VER_DETALLES_CTA_Click(object sender, EventArgs e)
        {
            DocDetallesPend();
        }
        private void TSM_ARCHIVO_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
        private void BT_SALIDA_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
        //
        //
        private void BuscarCtasPendientes()
        {
            _modoInicializar = true;
            _controlador.BuscarCtasPendientes();
            ActualizarDataPanel();
            _modoInicializar = false;
        }
        private void ActualizarDataPanel()
        {
            L_MONTO_PENDIENTE.Text = _controlador.GetMontoPendientePorCobrar.ToString("n2");
        }
        private void AgregarCta()
        {
            _controlador.AgregarCta();
            if (_controlador.AgregarCtaIsOk)
            {
                ActualizarDataPanel();
            }
        }
        private void AgregarNCrAdm()
        {
            _controlador.AgregarNCrAdm();
            if (_controlador.AgregarNCrAdmIsOk)
            {
                ActualizarDataPanel();
            }
        }
        private void AgregarNDbAdm()
        {
            _controlador.AgregarNDbAdm();
            if (_controlador.AgregarNDbAdmIsOk)
            {
                ActualizarDataPanel();
            }
        }
        private void GestionPago()
        {
            _controlador.GestionPago();
        }
        private void AgregarAnticipo()
        {
            _controlador.AgregarAnticipo();
        }
        private void AdmDocAnticipos()
        {
            _controlador.AdmDocAnticipos();
        }
        private void AdmPagosRecibidos()
        {
            _controlador.AdmPagosRecibidos();
        }
        private void EdoCta()
        {
            _controlador.EdoCta();
        }
        private void ListadoCtasPend()
        {
            _controlador.ListadoCtasPend();
        }
        private void DocDetallesPend()
        {
            _controlador.DocDetallesPend();
        }
        //
        //
        private void AbandonarFicha()
        {
            _controlador.AbandonarFicha();
            if (_controlador.AbandonarIsOk)
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