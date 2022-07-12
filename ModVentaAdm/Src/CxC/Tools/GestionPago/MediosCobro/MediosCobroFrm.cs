using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro
{
    
    public partial class MediosCobroFrm : Form
    {


        private IMedCobro _controlador;


        public MediosCobroFrm()
        {
            InitializeComponent();
            InicializarGrid_1();
        }


        private void InicializarGrid_1()
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

            var xc1 = new DataGridViewTextBoxColumn();
            xc1.DataPropertyName = "Metodo";
            xc1.HeaderText = "Medio Pago";
            xc1.Visible = true;
            xc1.MinimumWidth = 100;
            xc1.HeaderCell.Style.Font = f;
            xc1.DefaultCellStyle.Font = f1;
            xc1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            xc1.AutoSizeMode= DataGridViewAutoSizeColumnMode.Fill;
            xc1.ReadOnly = true;

            var xc11 = new DataGridViewTextBoxColumn();
            xc11.DataPropertyName = "Monto";
            xc11.HeaderText = "Monto";
            xc11.Visible = true;
            xc11.MinimumWidth = 100;
            xc11.HeaderCell.Style.Font = f;
            xc11.DefaultCellStyle.Font = f1;
            xc11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            xc11.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            xc11.DefaultCellStyle.Format = "n2";
            xc11.ReadOnly = true;

            var xc12 = new DataGridViewTextBoxColumn();
            xc12.DataPropertyName = "Tasa";
            xc12.HeaderText = "Tasa";
            xc12.Visible = true;
            xc12.MinimumWidth = 60;
            xc12.HeaderCell.Style.Font = f;
            xc12.DefaultCellStyle.Font = f1;
            xc12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            xc12.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            xc12.ReadOnly = true;

            var xc2 = new DataGridViewTextBoxColumn();
            xc2.DataPropertyName = "Importe";
            xc2.HeaderText = "Importe";
            xc2.Visible = true;
            xc2.Width = 120;
            xc2.HeaderCell.Style.Font = f;
            xc2.DefaultCellStyle.Font = f1;
            xc2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            xc2.DefaultCellStyle.Format = "n2";
            xc2.ReadOnly = true;

            DGV.Columns.Add(xc1);
            DGV.Columns.Add(xc11);
            DGV.Columns.Add(xc12);
            DGV.Columns.Add(xc2);
        }

        public void setControlador(IMedCobro ctr)
        {
            _controlador = ctr;
        }

        private void MetodosPagoFrm_Load(object sender, EventArgs e)
        {
            DGV.DataSource = _controlador.Source;
            _controlador.Source.CurrentChanged +=Source_CurrentChanged;
            ActualizarFicha();
            ActualizarTotal();
        }

        private void Source_CurrentChanged(object sender, EventArgs e)
        {
            ActualizarFicha();
        }

        private void ActualizarTotal()
        {
            L_MONTO_COBRAR.Text = _controlador.GetMontoCobrar.ToString("n2");
            L_MONTO_RECIBIDO.Text = _controlador.GetMontoRecibido.ToString("n2");
            L_MONTO_PEND.Text = _controlador.GetMontoPend.ToString("n2");
            L_RESTA_CAMBIO.Text = _controlador.GetRestaCambio;
        }


        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            Procesar();
        }
        private void Procesar()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }
        private void Abandonar()
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
        private void MetodosPagoFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOK || _controlador.ProcesarIsOK)
            {
                e.Cancel = false;
            }
        }


        private void BT_AGREGAR_Click(object sender, EventArgs e)
        {
            AgregarFicha();
        }
        private void AgregarFicha()
        {
            _controlador.AgregarFicha();
            if (_controlador.AgregarFichaIsOk) 
            {
                ActualizarTotal();
                ActualizarFicha();
            }
        }

        private void BT_ELIMINAR_METODO_PAGO_Click(object sender, EventArgs e)
        {
            EliminarMetodoPago();
        }
        private void EliminarMetodoPago()
        {
            _controlador.EliminarMetodoPago();
            if (_controlador.EliminarMetodoPagoIsOk) 
            {
                ActualizarTotal();
                ActualizarFicha();
            }
        }

        private void BT_EDITAR_Click(object sender, EventArgs e)
        {
            EditarMetodoPago();
        }
        private void EditarMetodoPago()
        {
            _controlador.EditarMetodoPago();
            if (_controlador.EditarMetodoPagoIsOk)
            {
                ActualizarTotal();
                ActualizarFicha();
            }
        }

        private void ActualizarFicha()
        {
            L_METODO_PAGO.Text = _controlador.GetMetodoPagoOp;
            L_MONTO.Text = _controlador.GetMontoOp.ToString("n2");
            L_BANCO.Text = _controlador.GetBancoOp;
            L_FECHA_OP.Text = _controlador.GetFechaOp.ToShortDateString();
            L_DETALLE_OP.Text = _controlador.GetDetalleOp;
            L_NRO_CTA.Text = _controlador.GetNroCtaOp;
            L_REF.Text = _controlador.GetRefOp;
            L_APLICA_FACTOR.Text = _controlador.GetAplicaFactorOp;
        }

        private void CHB_GENERAR_NOTA_CREDITO_CheckedChanged(object sender, EventArgs e)
        {
            _controlador.setGenerarNotaCredito(CHB_GENERAR_NOTA_CREDITO.Checked);
        }
  
    }

}