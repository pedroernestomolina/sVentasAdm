﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar
{
    public partial class Frm : Form
    {
        private IGenerar _controlador;
        private CultureInfo _cult;


        public Frm()
        {
            _cult = CultureInfo.CurrentCulture;
            InitializeComponent();
            InicializaGrid();
            InicializaCB();
        }
        private void InicializaCB()
        {
            CB_REMISION.DisplayMember = "desc";
            CB_REMISION.ValueMember = "id";
        }
        private void InicializaGrid()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);
            var f2 = new Font("Serif", 10, FontStyle.Bold);

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

            var c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "ServItemMostrar";
            c1.HeaderText = "Servicio";
            c1.Visible = true;
            c1.MinimumWidth= 180;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "PrecioItemMostrar";
            c3.HeaderText = "Precio($)";
            c3.Visible = true;
            c3.Width = 80;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c3.DefaultCellStyle.Format = "n2";

            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "CantItemMostrar";
            c4.HeaderText = "Cant";
            c4.Visible = true;
            c4.Width = 60;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f1;
            c4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c4.DefaultCellStyle.Format = "n0";

            var c6 = new DataGridViewTextBoxColumn();
            c6.DataPropertyName = "ImporteItemMostrar";
            c6.HeaderText = "Importe($)";
            c6.Visible = true;
            c6.Width = 80;
            c6.HeaderCell.Style.Font = f;
            c6.DefaultCellStyle.Font = f1;
            c6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c6.DefaultCellStyle.Format = "n2";

            var c7 = new DataGridViewTextBoxColumn();
            c7.DataPropertyName = "PresupuestoMostrar";
            c7.HeaderText = "Presupuesto #";
            c7.Visible = true;
            c7.Width = 120;
            c7.HeaderCell.Style.Font = f;
            c7.DefaultCellStyle.Font = f1;
            c7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c4);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(c6);
            DGV.Columns.Add(c7);
        }
        private bool _modoInicializa;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializa = true;
            //
            L_TIPO_DOCUMENTO.Text = _controlador.TipoDocumento_Get;
            DGV.DataSource = _controlador.SourceItems_Get;
            CB_REMISION.DataSource = _controlador.Remision.SourceItems_Get;
            //
            L_ITEMS_CNT.Text = "Items Registrados";
            L_CNT_ITEM.Text =  _controlador.Ficha.Items.Cnt_Get.ToString("n0");
            L_TASA_DIVISA.Text = _controlador.Ficha.Totales.TasaDivisaActual_Get.ToString("n2", _cult);
            L_MONTO_NETO.Text = _controlador.Ficha.Totales.MontoNeto_MonedaActual_Get.ToString("n2", _cult);
            L_MONTO_IVA.Text = _controlador.Ficha.Totales.MontoIva_MonedaActual_Get.ToString("n2", _cult);
            L_MONTO.Text = _controlador.Ficha.Totales.MontoTotal_MonedaActual_Get.ToString("n2", _cult);
            L_MONTO_DIVISA.Text = _controlador.Ficha.Totales.MontoTotal_MonedaDivisa_Get.ToString("n2", _cult);
            //
            L_RIF_CLIENTE.Text = _controlador.Ficha.Cliente_ciRif_Get;
            L_CODIGO_CLIENTE.Text = _controlador.Ficha.Cliente_codigo_Get;
            L_CLIENTE.Text = _controlador.Ficha.Cliente_razonSocial_Get;
            L_DATOS_DOC_FECHA.Text = _controlador.Ficha.DatosDoc_FechaEmi_Get;
            L_DATOS_DOC_COND_PAGO.Text = _controlador.Ficha.DatosDoc_CondPago_Get;
            L_DATOS_DOC_FECHA_VENCE.Text = _controlador.Ficha.DatosDoc_FechaVenc_Get;
            ////
            CB_REMISION.SelectedValue = _controlador.Remision.ItemId_Get;
            L_NOMBRE_DOC_REMISION.Text = _controlador.Remision.DocNombre_Get;
            L_NUMERO_DOC_REMISION.Text = _controlador.Remision.DocNumero_Get;
            L_FECHA_DOC_REMISION.Text = _controlador.Remision.DocFecha_Get;
            //
            TB_NOTAS.Text = _controlador.NotasObserv_Get;
            //
            _modoInicializa = false;
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.ProcesarIsOK || _controlador.AbandonarIsOK)
            {
                e.Cancel = false;
            }
        }

        public void setControlador(IGenerar ctr)
        {
            _controlador = ctr;
        }


        private void BT_NUEVO_DOC_Click(object sender, EventArgs e)
        {
            NuevoDocumento();
        }


        private void TB_NOTAS_Leave(object sender, EventArgs e)
        {
            _controlador.setNotas(TB_NOTAS.Text);
        }


        private void CB_REMISION_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializa) { return; }
            _controlador.Remision.setFichaId("");
            if (CB_REMISION.SelectedIndex != -1)
            {
                _controlador.Remision.setFichaId(CB_REMISION.SelectedValue.ToString());
            }
        }
        private void BT_REMISION_Click(object sender, EventArgs e)
        {
            RemisionBuscar();
        }


        private void BT_ITEM_AGREGAR_Click(object sender, EventArgs e)
        {
            AgregarItem();
        }
        private void BT_ITEM_EDITAR_Click(object sender, EventArgs e)
        {
            EditarItem();
        }
        private void BT_ITEM_ELIMINAR_Click(object sender, EventArgs e)
        {
            EliminarItem();
        }


        private void BT_LIMPIAR_Click(object sender, EventArgs e)
        {
            _controlador.LimpiarDocumento();
            if (_controlador.LimpiarDocumentoIsOK)
            {
                ActualizarFicha();
                ActualizarFichaRemision();
                ActualizarContadores();
                ActualizarTotales();
                TB_NOTAS.Text = _controlador.NotasObserv_Get;
            }
        }
        private void BT_EDITAR_Click(object sender, EventArgs e)
        {
            _controlador.EditarDocumento();
            if (_controlador.EditarDocumentoIsOK)
            {
                ActualizarFicha();
                ActualizarFichaRemision();
            }
        }

        
        private void BT_DOC_PENDIENTE_Click(object sender, EventArgs e)
        {
        }
        private void BT_PROCESAR_DOC_Click(object sender, EventArgs e)
        {
            ProcesarDocumento();
        }
        private void BT_SALIDA_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }


        private void RemisionBuscar()
        {
            _controlador.BuscarRemision();
            if (_controlador.RemisionIsOK)
            {
                ActualizarFichaRemision();
                ActualizarContadores();
                ActualizarTotales();
                ActualizarFicha();
                TB_NOTAS.Text = _controlador.NotasObserv_Get;
            }
        }
        private void NuevoDocumento()
        {
            _controlador.NuevoDocumento();
            ActualizarFicha();
        }
        private void AgregarItem()
        {
            _controlador.AgregarItem();
            ActualizarContadores();
            ActualizarTotales();
        }
        private void EditarItem()
        {
            //_controlador.EditarItem();
            //ActualizarContadores();
            //ActualizarTotales();
        }
        private void EliminarItem()
        {
            _controlador.EliminarItem();
            ActualizarContadores();
            ActualizarTotales();
        }
        private void ActualizarFicha()
        {
            L_RIF_CLIENTE.Text = _controlador.Ficha.Cliente_ciRif_Get;
            L_CODIGO_CLIENTE.Text = _controlador.Ficha.Cliente_codigo_Get;
            L_CLIENTE.Text = _controlador.Ficha.Cliente_razonSocial_Get;
            L_DATOS_DOC_FECHA.Text = _controlador.Ficha.DatosDoc_FechaEmi_Get;
            L_DATOS_DOC_COND_PAGO.Text = _controlador.Ficha.DatosDoc_CondPago_Get;
            L_DATOS_DOC_FECHA_VENCE.Text = _controlador.Ficha.DatosDoc_FechaVenc_Get;
        }
        private void ActualizarFichaRemision()
        {
            CB_REMISION.SelectedValue = _controlador.Remision.ItemId_Get;
            L_NOMBRE_DOC_REMISION.Text = _controlador.Remision.DocNombre_Get;
            L_NUMERO_DOC_REMISION.Text = _controlador.Remision.DocNumero_Get;
            L_FECHA_DOC_REMISION.Text = _controlador.Remision.DocFecha_Get;
        }
        private void ActualizarTotales()
        {
            L_MONTO_NETO.Text = _controlador.Ficha.Totales.MontoNeto_MonedaActual_Get.ToString("n2", _cult);
            L_MONTO_IVA.Text = _controlador.Ficha.Totales.MontoIva_MonedaActual_Get.ToString("n2", _cult);
            L_MONTO.Text = _controlador.Ficha.Totales.MontoTotal_MonedaActual_Get.ToString("n2", _cult);
            L_MONTO_DIVISA.Text = _controlador.Ficha.Totales.MontoTotal_MonedaDivisa_Get.ToString("n2", _cult);
        }
        private void ActualizarContadores()
        {
            L_CNT_ITEM.Text = _controlador.Ficha.Items.Cnt_Get.ToString("n0");
        }
        private void ProcesarDocumento()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                ActualizarFicha();
                ActualizarFichaRemision();
                ActualizarContadores();
                ActualizarTotales();
                TB_NOTAS.Text = _controlador.NotasObserv_Get;
                _controlador.IniciarEnLimpio();
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