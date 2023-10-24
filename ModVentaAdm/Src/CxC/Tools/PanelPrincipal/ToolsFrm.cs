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


        public ToolsFrm()
        {
            InitializeComponent();
            InicializaDGV_1();
        }

        private void InicializaDGV_1()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);

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

            var c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "CiRif";
            c1.HeaderText = "CI/RIF";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.Width = 100;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "NombreRazonSocial";
            c2.HeaderText = "Nombre/Razón Social";
            c2.Visible = true;
            c2.Width = 180;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "MontoImporte";
            c3.HeaderText = "Importe";
            c3.Visible = true;
            c3.MinimumWidth = 100;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c3.DefaultCellStyle.Format = "n2";

            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "MontoAcumulado";
            c4.HeaderText = "Acumulado";
            c4.Visible = true;
            c4.Width = 100;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f;
            c4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c4.DefaultCellStyle.Format = "n2";

            var c8 = new DataGridViewTextBoxColumn();
            c8.DataPropertyName = "MontoResta";
            c8.HeaderText = "Resta";
            c8.Visible = true;
            c8.Width = 100;
            c8.HeaderCell.Style.Font = f;
            c8.DefaultCellStyle.Font = f;
            c8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c8.DefaultCellStyle.Format = "n2";

            var c5 = new DataGridViewTextBoxColumn();
            c5.DataPropertyName = "CntDocPend";
            c5.HeaderText = "Doc/Pend";
            c5.Visible = true;
            c5.Width = 80;
            c5.HeaderCell.Style.Font = f;
            c5.DefaultCellStyle.Font = f;
            c5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var c6 = new DataGridViewTextBoxColumn();
            c6.DataPropertyName = "CntFactPend";
            c6.HeaderText = "Fact/Pend";
            c6.Visible = true;
            c6.Width = 80;
            c6.HeaderCell.Style.Font = f;
            c6.DefaultCellStyle.Font = f;
            c6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var c7 = new DataGridViewTextBoxColumn();
            c7.DataPropertyName = "MontoLimiteCredito";
            c7.HeaderText = "Lim/Monto";
            c7.Name = "Estatus";
            c7.Visible = true;
            c7.Width = 80;
            c7.HeaderCell.Style.Font = f;
            c7.DefaultCellStyle.Font = f;
            c7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DGV_1.Columns.Add(c1);
            DGV_1.Columns.Add(c2);
            DGV_1.Columns.Add(c3);
            DGV_1.Columns.Add(c4);
            DGV_1.Columns.Add(c8);
            DGV_1.Columns.Add(c5);
            DGV_1.Columns.Add(c6);
        }


        public void setControlador(ITools ctr)
        {
            _controlador = ctr;
        }

        private void TSM_ARCHIVO_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
        private void BT_SALIDA_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }
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
        private void ToolsFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOk) 
            {
                e.Cancel = false;
            }
        }

        private bool _modoInicializar;
        private void ToolsFrm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            DGV_1.DataSource = _controlador.CtasPendGetSource;
            ActualizarDataPanel();
            _modoInicializar = false;
        }

        private void BT_BUSCAR_Click(object sender, EventArgs e)
        {
            BuscarCtasPendientes();
        }
        private void BuscarCtasPendientes()
        {
            _controlador.BuscarCtasPendientes();
            ActualizarDataPanel();
        }

        private void ActualizarDataPanel()
        {
            L_MONTO_PENDIENTE.Text = _controlador.GetMontoPendientePorCobrar.ToString("n2");
        }

        private void BT_VER_DETALLES_CTA_Click(object sender, EventArgs e)
        {
            DocDetallesPend();
        }
        private void DocDetallesPend()
        {
            _controlador.DocDetallesPend();
        }

        private void BT_REPORTE_CTAS_Click(object sender, EventArgs e)
        {
            ListadoCtasPend();
        }
        private void ListadoCtasPend()
        {
            _controlador.ListadoCtasPend();
        }

        private void BT_AGREGAR_CTA_Click(object sender, EventArgs e)
        {
            AgregarCta();
        }
        private void AgregarCta()
        {
            _controlador.AgregarCta();
            if (_controlador.AgregarCtaIsOk) 
            {
                ActualizarDataPanel();
            }
        }

        private void BT_AGREGAR_NCR_ADM_Click(object sender, EventArgs e)
        {
            AgregarNCrAdm();
        }
        private void AgregarNCrAdm()
        {
            _controlador.AgregarNCrAdm();
            if (_controlador.AgregarNCrAdmIsOk)
            {
                ActualizarDataPanel();
            }
        }

        private void BT_AGREGAR_NDB_ADM_Click(object sender, EventArgs e)
        {
            AgregarNDbAdm();
        }
        private void AgregarNDbAdm()
        {
            _controlador.AgregarNDbAdm();
            if (_controlador.AgregarNDbAdmIsOk)
            {
                ActualizarDataPanel();
            }
        }

        private void BT_GESTION_PAGO_Click(object sender, EventArgs e)
        {
            GestionPago();
        }
        private void GestionPago()
        {
            _controlador.GestionPago();
            BuscarCtasPendientes();
        }

        //
        //
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
    }
}