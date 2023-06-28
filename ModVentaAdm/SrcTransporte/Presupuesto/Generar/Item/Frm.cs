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


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item
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
            LB_FECHA.DisplayMember = "desc";
            LB_FECHA.ValueMember = "id";
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            TB_DESC_BREVE.Text = _controlador.Item.Get_Descripcion;
            TB_SOLICITADO_POR.Text = _controlador.Item.Get_SolicitadoPor;
            TB_MODULO_CARGAR.Text = _controlador.Item.Get_ModuloCargar;
            TB_CNT_DIAS.Text = _controlador.Item.Get_CntDias.ToString("n0", _cult);
            TB_CNT_UNIDADES.Text = _controlador.Item.Get_CntUnidades.ToString("n0", _cult);
            TB_PRECIO_DIVISA.Text = _controlador.Item.Get_PrecioDivisa.ToString("n2", _cult);
            TB_DSCTO.Text = _controlador.Item.Get_Dscto.ToString("n2", _cult);
            L_ALIADO.Text = _controlador.Item.Get_Aliado_Inf;
            TB_PRECIO_PAUTADO_AFILIADO.Text = _controlador.Item.Get_Aliado_PrecioPautado.ToString("n2", _cult);
            L_IMPORTE.Text = _controlador.Item.Get_Importe.ToString("n2", _cult);
            LB_FECHA.DataSource = _controlador.Item.Get_SourceFechas;
            TB_DESCRIPCION_FULL.Text = _controlador.Item.Get_DescripcionFull;
            DTP_FECHA.Value = _controlador.Item.Get_Fecha;
            DTP_HORA.Value = _controlador.Item.Get_Fecha;
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
        private void TB_SOLICITADO_POR_Leave(object sender, EventArgs e)
        {
            _controlador.Item.setSolicitadoPor(TB_SOLICITADO_POR.Text.Trim());
        }
        private void TB_MODULO_CARGAR_Leave(object sender, EventArgs e)
        {
            _controlador.Item.setModuloaCargar(TB_MODULO_CARGAR.Text.Trim());
        }
        private void TB_CNT_DIAS_Leave(object sender, EventArgs e)
        {
            var _cnt = int.Parse(TB_CNT_DIAS.Text);
            _controlador.Item.setCntDias(_cnt);
            ActualizaImporte();
        }
        private void TB_CNT_UNIDADES_Leave(object sender, EventArgs e)
        {
            var _cnt = int.Parse(TB_CNT_UNIDADES.Text);
            _controlador.Item.setCntUnidades(_cnt);
            ActualizaImporte();
        }
        private void TB_PRECIO_DIVISA_Leave(object sender, EventArgs e)
        {
            var _mnto = decimal.Parse(TB_PRECIO_DIVISA.Text);
            _controlador.Item.setPrecioDivisa(_mnto);
            TB_PRECIO_DIVISA.Text = _controlador.Item.Get_PrecioDivisa.ToString("n2", _cult);
            ActualizaImporte();
        }
        private void TB_DSCTO_Leave(object sender, EventArgs e)
        {
            var _mnto = decimal.Parse(TB_DSCTO.Text);
            _controlador.Item.setDscto(_mnto);
            TB_DSCTO.Text = _controlador.Item.Get_Dscto.ToString("n2", _cult);
            ActualizaImporte();
        }
        private void DTP_FECHA_Leave(object sender, EventArgs e)
        {
            _controlador.Item.setFecha(DTP_FECHA.Value);
        }
        private void DTP_HORA_Leave(object sender, EventArgs e)
        {
            _controlador.Item.setHora(DTP_HORA.Value);
        }
        private void TB_PRECIO_PAUTADO_AFILIADO_Leave(object sender, EventArgs e)
        {
            var _monto = decimal.Parse(TB_PRECIO_PAUTADO_AFILIADO.Text);
            _controlador.Item.setPrecioAliadoPautado(_monto);
        }
        private void TB_DESCRIPCION_FULL_Leave(object sender, EventArgs e)
        {
            _controlador.Item.setDescripcionFull(TB_DESCRIPCION_FULL.Text);
        }


        private void BT_BUSCAR_ALIADO_Click(object sender, EventArgs e)
        {
            BuscarAliado();
        }
        private void BT_LIMPIAR_ALIADO_Click(object sender, EventArgs e)
        {
            LimpiarAliado();
        }
        private void BT_AGREGAR_FECHA_Click(object sender, EventArgs e)
        {
            AgregarFecha();
        }
        private void BT_ELIMINAR_FECHA_Click(object sender, EventArgs e)
        {
            EliminarFecha();
        }

        
        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            ProcesarItem();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            AbandonarFicha();
        }


        private void AgregarFecha()
        {
            _controlador.Item.AgregarFecha();
            TB_CNT_DIAS.Text = _controlador.Item.Get_CntDias.ToString("n0", _cult);
            LB_FECHA.Refresh();
            ActualizaImporte();
        }
        private void EliminarFecha()
        {
            _controlador.Item.EliminarFecha();
            TB_CNT_DIAS.Text = _controlador.Item.Get_CntDias.ToString("n0", _cult);
            LB_FECHA.Refresh();
            ActualizaImporte();
        }
        private void BuscarAliado()
        {
            _controlador.Aliados();
            L_ALIADO.Text = _controlador.Item.Get_Aliado_Inf;
        }
        private void LimpiarAliado()
        {
            _controlador.Item.LimpiarAliado();
            L_ALIADO.Text = _controlador.Item.Get_Aliado_Inf;
        }
        private void ActualizaImporte()
        {
            L_IMPORTE.Text = _controlador.Item.Get_Importe.ToString("n2", _cult);
        }
        private void IrFoco_Item()
        {
            TB_OPCIONES.SelectedTab = TAB_DETALLE;
            TB_DESCRIPCION_FULL.Focus();
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
    }
}