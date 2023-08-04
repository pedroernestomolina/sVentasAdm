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
            InicializaGrid();
            InicializaLB();
        }

        private void InicializaGrid()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);
            var f2 = new Font("Serif", 10, FontStyle.Bold);

            DGV_ALIADO.RowHeadersVisible = false;
            DGV_ALIADO.AllowUserToAddRows = false;
            DGV_ALIADO.AllowUserToDeleteRows = false;
            DGV_ALIADO.AutoGenerateColumns = false;
            DGV_ALIADO.AllowUserToResizeRows = false;
            DGV_ALIADO.AllowUserToResizeColumns = false;
            DGV_ALIADO.AllowUserToOrderColumns = false;
            DGV_ALIADO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_ALIADO.MultiSelect = false;
            DGV_ALIADO.ReadOnly = true;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "AliadoLlamado";
            c2.HeaderText = "Aliado";
            c2.Visible = true;
            c2.MinimumWidth = 150;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "Importe";
            c3.HeaderText = "Importe($)";
            c3.Visible = true;
            c3.Width = 80;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c3.DefaultCellStyle.Format = "n2";

            DGV_ALIADO.Columns.Add(c2);
            DGV_ALIADO.Columns.Add(c3);
        }
        private void InicializaLB()
        {
            LB_FECHA.DisplayMember = "desc";
            LB_FECHA.ValueMember = "id";
            CB_ALICUOTA.DisplayMember = "desc";
            CB_ALICUOTA.ValueMember = "id";
            CB_TIPO_SERV.DisplayMember = "desc";
            CB_TIPO_SERV.ValueMember = "id";
        }
        private bool _modoInicializar;
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializar = true;
            DGV_ALIADO.DataSource = _controlador.Item.Get_SourceAliadosLlamados;
            LB_FECHA.DataSource = _controlador.Item.Get_SourceFechas;
            //
            TB_DESC_BREVE.Text = _controlador.Item.Get_Descripcion;
            TB_SOLICITADO_POR.Text = _controlador.Item.Get_SolicitadoPor;
            TB_MODULO_CARGAR.Text = _controlador.Item.Get_ModuloCargar;
            TB_CNT_DIAS.Text = _controlador.Item.Get_CntDias.ToString("n0", _cult);
            TB_CNT_UNIDADES.Text = _controlador.Item.Get_CntUnidades.ToString("n0", _cult);
            TB_PRECIO_DIVISA.Text = _controlador.Item.Get_PrecioDivisa.ToString("n2", _cult);
            TB_DSCTO.Text = _controlador.Item.Get_Dscto.ToString("n2", _cult);
            L_ALIADO.Text = _controlador.Item.Get_Aliado_Inf;
            TB_PRECIO_PAUTADO_AFILIADO.Text = _controlador.Item.Get_Aliado_PrecioPautado.ToString("n2", _cult);
            TB_CNT_PAUTADA_ALIADO.Text = _controlador.Item.Get_Aliado_CntPautado.ToString("n0");
            L_IMPORTE.Text = _controlador.Item.Get_Importe.ToString("n2", _cult);
            TB_DESCRIPCION_FULL.Text = _controlador.Item.Get_DescripcionFull;
            DTP_FECHA.Value = _controlador.Item.Get_Fecha;
            DTP_HORA.Value = _controlador.Item.Get_Fecha;
            CB_ALICUOTA.DataSource = _controlador.Alicuota.GetSource;
            CB_ALICUOTA.SelectedValue = _controlador.Item.Get_Alicuota_ID;
            CB_TIPO_SERV.DataSource = _controlador.TipoServ.GetSource;
            CB_TIPO_SERV.SelectedValue = _controlador.Item.Get_TipoServ_ID;
            TB_UNIDADES_DETALL.Text = _controlador.Item.Get_UnidadesDetall;

            IrFoco_Detalle();
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
            TB_PRECIO_PAUTADO_AFILIADO.Text = _controlador.Item.Get_Aliado_PrecioPautado.ToString("n2", _cult);
        }
        private void TB_CNT_PAUTADA_ALIADO_Leave(object sender, EventArgs e)
        {
            var _cnt = int.Parse(TB_CNT_PAUTADA_ALIADO.Text);
            _controlador.Item.setCntAliadoPautado(_cnt);
            TB_CNT_PAUTADA_ALIADO.Text = _controlador.Item.Get_Aliado_CntPautado.ToString("n0");
        }
        private void TB_DESCRIPCION_FULL_Leave(object sender, EventArgs e)
        {
            _controlador.Item.setDescripcionFull(TB_DESCRIPCION_FULL.Text);
        }
        private void TB_UNIDADES_DETALL_Leave(object sender, EventArgs e)
        {
            _controlador.Item.setUnidadesDetalle(TB_UNIDADES_DETALL.Text);
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
        private void CB_TIPO_SERV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_modoInicializar) { return; }
            _controlador.TipoServSetFichaById("");
            if (CB_TIPO_SERV.SelectedIndex != -1)
            {
                _controlador.TipoServSetFichaById(CB_TIPO_SERV.SelectedValue.ToString());
            }
            TB_DESC_BREVE.Text = _controlador.Item.Get_Descripcion;
        }


        private void BT_BUSCAR_ALIADO_Click(object sender, EventArgs e)
        {
            BuscarAliado();
        }
        private void BT_GUARDAR_ALIADO_Click(object sender, EventArgs e)
        {
            GuardarAliado();
        }
        private void BT_ELIMINAR_ALIDO_LLAMADO_Click(object sender, EventArgs e)
        {
            EliminarAliado();
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
        private void GuardarAliado()
        {
            _controlador.Item.GuardarAliado();
            L_ALIADO.Text = _controlador.Item.Get_Aliado_Inf;
            TB_PRECIO_PAUTADO_AFILIADO.Text = _controlador.Item.Get_Aliado_PrecioPautado.ToString("n2", _cult);
            TB_CNT_PAUTADA_ALIADO.Text = _controlador.Item.Get_Aliado_CntPautado.ToString("n0");
        }
        private void EliminarAliado()
        {
            _controlador.Item.EliminarAliado();
        }
        private void ActualizaImporte()
        {
            L_IMPORTE.Text = _controlador.Item.Get_Importe.ToString("n2", _cult);
        }
        private void IrFoco_Detalle()
        {
            TB_OPCIONES.SelectedTab = TAB_DETALLE;
            CB_TIPO_SERV.Focus();
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