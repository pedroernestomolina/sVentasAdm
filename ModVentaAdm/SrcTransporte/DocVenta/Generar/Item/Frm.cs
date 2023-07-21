using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Item
{
    public partial class Frm : Form
    {
        private IItem _controlador;
        private CultureInfo _cult;


        public Frm()
        {
            _cult = CultureInfo.CurrentCulture;
            InitializeComponent();
            InicializaLB();
        }
        private void InicializaLB()
        {
            CB_ALICUOTA.DisplayMember = "desc";
            CB_ALICUOTA.ValueMember = "id";
        }
        private bool _modoInicializar;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            TB_DESC_BREVE.Text = _controlador.Item.Get_Descripcion;
            TB_CNT_DIAS.Text = _controlador.Item.Get_Cnt.ToString("n0", _cult);
            TB_PRECIO_DIVISA.Text = _controlador.Item.Get_PrecioDivisa.ToString();
            TB_DSCTO.Text = _controlador.Item.Get_Dscto.ToString("n2", _cult);
            L_IMPORTE.Text = _controlador.Item.Get_Importe.ToString("n2", _cult);
            CB_ALICUOTA.DataSource = _controlador.Alicuota.GetSource;
            CB_ALICUOTA.SelectedValue = _controlador.Item.Get_Alicuota_ID;
            _modoInicializar = false;
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.ProcesarIsOK || _controlador.AbandonarIsOK)
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


        public void setControlador(IItem ctr)
        {
            _controlador = ctr;
        }

      
        private void TB_DESC_BREVE_Leave(object sender, EventArgs e)
        {
            _controlador.Item.setDescripcion(TB_DESC_BREVE.Text.Trim());
        }
        private void TB_CNT_DIAS_Leave(object sender, EventArgs e)
        {
            var _cnt = int.Parse(TB_CNT_DIAS.Text);
            _controlador.Item.setCnt(_cnt);
            ActualizaImporte();
        }
        private void TB_PRECIO_DIVISA_Leave(object sender, EventArgs e)
        {
            var _mnto = decimal.Parse(TB_PRECIO_DIVISA.Text);
            _controlador.Item.setPrecioDivisa(_mnto);
            //TB_PRECIO_DIVISA.Text = _controlador.Item.Get_PrecioDivisa.ToString("n2", _cult);
            TB_PRECIO_DIVISA.Text = _controlador.Item.Get_PrecioDivisa.ToString();
            ActualizaImporte();
        }
        private void TB_DSCTO_Leave(object sender, EventArgs e)
        {
            var _mnto = decimal.Parse(TB_DSCTO.Text);
            _controlador.Item.setDscto(_mnto);
            TB_DSCTO.Text = _controlador.Item.Get_Dscto.ToString("n2", _cult);
            ActualizaImporte();
        }
        private void CB_ALICUOTA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.AlicuotaSetFichaById("");
            if (CB_ALICUOTA.SelectedIndex != -1)
            {
                _controlador.AlicuotaSetFichaById(CB_ALICUOTA.SelectedValue.ToString());
            }
        }

        
        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            ProcesarItem();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }


        private void ActualizaImporte()
        {
            L_IMPORTE.Text = _controlador.Item.Get_Importe.ToString("n2", _cult);
        }
        private void IrFoco_Item()
        {
            TB_OPCIONES.SelectedTab = TAB_DETALLE;
            TB_DESC_BREVE.Focus();
        }
        private void ProcesarItem()
        {
            IrFoco_Item();
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK)
            {
                Salir();
            }
        }
        private void AbandonarFicha()
        {
            IrFoco_Item();
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

        private void TB_DSCTO_Validating(object sender, CancelEventArgs e)
        {
            var _tasa = decimal.Parse(TB_DSCTO.Text);
            if (_tasa >= 100) 
            {
                e.Cancel = true;
            }
        }


        private void BT_ITEM_AGREGAR_PRESUP_Click(object sender, EventArgs e)
        {
            HabilitarPresupuesto();
        }
        private void HabilitarPresupuesto()
        {
            _controlador.HabilitarPresupuesto();
            ActualizarDatosFicha();
        }

        private void BT_ITEM_AGREGAR_SERV_Click(object sender, EventArgs e)
        {
            HabilitarServicio();
        }
        private void HabilitarServicio()
        {
            _controlador.HabilitarServicio();
            ActualizarDatosFicha();
        }

        private void ActualizarDatosFicha()
        {
            TB_DESC_BREVE.Text = _controlador.Item.Get_Descripcion;
            TB_CNT_DIAS.Text = _controlador.Item.Get_Cnt.ToString("n0", _cult);
            //TB_PRECIO_DIVISA.Text = _controlador.Item.Get_PrecioDivisa.ToString("n2", _cult);
            TB_PRECIO_DIVISA.Text = _controlador.Item.Get_PrecioDivisa.ToString();
            TB_DSCTO.Text = _controlador.Item.Get_Dscto.ToString("n2", _cult);
            L_IMPORTE.Text = _controlador.Item.Get_Importe.ToString("n2", _cult);
            this.Refresh();
        }
    }
}