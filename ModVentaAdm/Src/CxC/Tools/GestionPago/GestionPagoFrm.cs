using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago
{

    public partial class GestionPagoFrm : Form
    {

        private IGestionPago _controlador;


        public GestionPagoFrm()
        {
            InitializeComponent();
            InicializaGrid();
        }
        private void InicializaGrid()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);


            //autoDoc = "";
            //fechaEmisionDoc = DateTime.Now.Date;
            //tipoDoc = "";
            //numeroDoc = "";
            //fechaVencDoc = DateTime.Now.Date;
            //notasDoc = "";
            //importeDoc = 0m;
            //acumuladoDoc = 0m;
            //signoDoc = 1;
            //serieDoc = "";
            //diasCreditoDoc = 0;
            //tasaCambioDoc = 0m;

            DGV.AllowUserToAddRows = false;
            DGV.AllowUserToDeleteRows = false;
            DGV.AutoGenerateColumns = false;
            DGV.AllowUserToResizeRows = false;
            DGV.AllowUserToResizeColumns = false;
            DGV.AllowUserToOrderColumns = false;
            DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV.MultiSelect = false;
            DGV.ReadOnly = false;

            var c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "fechaEmisionDoc";
            c1.HeaderText = "Fecha/Doc";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.Width = 80;
            c1.ReadOnly = true;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "tipoDoc";
            c2.HeaderText = "Tipo/Doc";
            c2.Visible = true;
            c2.Width = 80;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleCenter;
            c2.ReadOnly = true;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "numeroDoc";
            c3.HeaderText = "Documento";
            c3.Visible = true;
            c3.MinimumWidth = 100;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            c3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            c3.ReadOnly = true;

            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "fechaVencDoc";
            c4.HeaderText = "Fecha/Vto";
            c4.Visible = true;
            c4.Width = 80;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f;
            c4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c4.ReadOnly = true;

            var c5 = new DataGridViewTextBoxColumn();
            c5.DataPropertyName = "diasVencida";
            c5.HeaderText = "Dias/Venc";
            c5.Visible = true;
            c5.Width = 80;
            c5.HeaderCell.Style.Font = f;
            c5.DefaultCellStyle.Font = f;
            c5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c5.DefaultCellStyle.Format = "n0";
            c5.ReadOnly = true;

            var c6 = new DataGridViewTextBoxColumn();
            c6.DataPropertyName = "montoImporte";
            c6.HeaderText = "Importe";
            c6.Visible = true;
            c6.Width = 100;
            c6.HeaderCell.Style.Font = f;
            c6.DefaultCellStyle.Font = f;
            c6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c6.DefaultCellStyle.Format = "n2";
            c6.ReadOnly = true;

            var c9 = new DataGridViewTextBoxColumn();
            c9.DataPropertyName = "tasaCambioDoc";
            c9.HeaderText = "Tasa";
            c9.Visible = true;
            c9.Width = 60;
            c9.HeaderCell.Style.Font = f;
            c9.DefaultCellStyle.Font = f;
            c9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c9.DefaultCellStyle.Format = "n2";
            c9.ReadOnly = true;

            var c7 = new DataGridViewTextBoxColumn();
            c7.DataPropertyName = "montoAcumulado";
            c7.HeaderText = "Abonado";
            c7.Visible = true;
            c7.Width = 100;
            c7.HeaderCell.Style.Font = f;
            c7.DefaultCellStyle.Font = f;
            c7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c7.DefaultCellStyle.Format = "n2";
            c7.ReadOnly = true;

            var c8 = new DataGridViewTextBoxColumn();
            c8.DataPropertyName = "montoResta";
            c8.HeaderText = "Resta";
            c8.Visible = true;
            c8.Width = 100;
            c8.HeaderCell.Style.Font = f;
            c8.DefaultCellStyle.Font = f;
            c8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c8.DefaultCellStyle.Format = "n2";
            c8.ReadOnly = true;

            var cA = new DataGridViewCheckBoxColumn();
            cA.DataPropertyName = "isPagarOk";
            cA.HeaderText = "Pagar";
            cA.Visible = true;
            cA.Width = 60;
            cA.HeaderCell.Style.Font = f;
            cA.DefaultCellStyle.Font = f;
            cA.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cA.ReadOnly = true;

            var cB = new DataGridViewTextBoxColumn();
            cB.DataPropertyName = "montoAbonar";
            cB.HeaderText = "Abonar";
            cB.Visible = true;
            cB.Width = 100;
            cB.HeaderCell.Style.Font = f;
            cB.DefaultCellStyle.Font = f;
            cB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            cB.DefaultCellStyle.Format = "n2";
            cB.ReadOnly = true;

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(c4);
            DGV.Columns.Add(c5);
            DGV.Columns.Add(c6);
            //DGV.Columns.Add(c9);
            //DGV.Columns.Add(c7);
            DGV.Columns.Add(c8);
            DGV.Columns.Add(cA);
            DGV.Columns.Add(cB);
        }

        public void setControlador(IGestionPago ctr)
        {
            _controlador = ctr;
        }

        private void BT_SALIDA_Click(object sender, EventArgs e)
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
        private void DocumentosPendFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK) 
            {
                e.Cancel = false;
            }
        }

        private bool _modoInicializar;
        private BindingSource _bs;
        private void DocumentosPendFrm_Load(object sender, EventArgs e)
        {
            _bs = _controlador.DocPendGetSource;
            _bs.CurrentChanged += _bs_CurrentChanged;
            _modoInicializar = true;
            DGV.DataSource = _bs;
            ActualizarDataPanel();
            _modoInicializar = false;
        }

        private void _bs_CurrentChanged(object sender, EventArgs e)
        {
            L_NOTAS.Text = _controlador.GetNotas;
        }

        private void ActualizarDataPanel()
        {
            L_CLIENTE_DATA.Text = _controlador.GetClienteData;
            L_RESTA.Text = _controlador.GetMontoResta.ToString("n2");
            L_ABONAR.Text = _controlador.GetMontoAbonar.ToString("n2");
            L_PEND.Text = _controlador.GetMontoPend.ToString("n2");
            L_CNT_DOC.Text = _controlador.GetCantDoc.ToString();
            L_NOTAS.Text = _controlador.GetNotas;
        }

        private void L_CLIENTE_DATA_DoubleClick(object sender, EventArgs e)
        {
            VerFichaCliente();
        }
        private void VerFichaCliente()
        {
            _controlador.VerFichaCliente();
        }

       

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                MarcarDesmarcarDoc();
            }
        }
        private void MarcarDesmarcarDoc()
        {
            _controlador.MarcarDesmarcarDoc();
            ActualizarTotal();
        }
        private void BT_LIMPIAR_ABONO_Click(object sender, EventArgs e)
        {
            _controlador.LimpiarAbonoMarcado();
            ActualizarTotal();
        }
        private void BT_APLICAR_Click(object sender, EventArgs e)
        {
            AplicarPagos();
        }
        private void AplicarPagos()
        {
            _controlador.AplicarPagos();
            if (_controlador.ProcesarPagoIsOk) 
            {
                ActualizarDataPanel();
                ActualizarTotal();
            }
        }

        private void ActualizarTotal()
        {
            L_ABONAR.Text = _controlador.GetMontoAbonar.ToString("n2");
            L_PEND.Text = _controlador.GetMontoPend.ToString("n2");
        }

    }

}