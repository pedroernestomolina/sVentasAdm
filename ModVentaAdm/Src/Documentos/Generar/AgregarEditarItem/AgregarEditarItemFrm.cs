using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.AgregarEditarItem
{

    public partial class AgregarEditarItemFrm : Form
    {

        private Gestion _controlador;


        public AgregarEditarItemFrm()
        {
            InitializeComponent();
            InicializarGrid();
        }

        private void InicializarGrid()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 10, FontStyle.Regular);

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
            c1.DataPropertyName = "Etiqueta";
            c1.HeaderText = "PRECIO";
            c1.Visible = true;
            c1.Width = 100;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "Empaque";
            c2.HeaderText = "Empaque";
            c2.Visible = true;
            c2.MinimumWidth = 120;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "PNeto";
            c3.HeaderText = "Precio";
            c3.Visible = true;
            c3.Width = 120;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c3.DefaultCellStyle.Format = "n2";

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Abandonar();
        }

        private void Abandonar()
        {
            IrFoco();
            _controlador.Abandonar();
            if (_controlador.AbandonarIsOk) 
            {
                Salir();
            }
        }

        private void Salir()
        {
            this.Close();
        }

        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private BindingSource _bs;
        private void AgregarEditarItemFrm_Load(object sender, EventArgs e)
        {
            DGV.DataSource = _controlador.PreciosSource;
            _bs = _controlador.PreciosSource;
            _bs.CurrentChanged+=_bs_CurrentChanged;
            //
            L_MODO.Text = _controlador.ModoFicha;
            L_PRODUCTO.Text = _controlador.Producto_Desc;
            TB_CANT.Text = _controlador.Cantidad.ToString("n"+_controlador.NDecimales);
            TB_PRECIO.Text = _controlador.Data_Precio.ToString("n2");
            TB_NOTAS.Text = _controlador.Notas;
            TB_DSCTO.Text = _controlador.Dscto.ToString("n2");
            _controlador.PrecioIniciar();
            //
            ActualizarPrecioNeto();
            ActualizarData();
            P_COSTO.Visible = false;
            //
            IrFoco();
        }

        private void IrFoco()
        {
            TB_CANT.Focus();
        }

        private void ActualizarPrecioNeto()
        {
            _controlador.ActualizarPrecioNeto();
        }

        private void _bs_CurrentChanged(object sender, EventArgs e)
        {
            ActualizarData();
        }

        private void ActualizarData()
        {
            L_EMPAQUE.Text = _controlador.Data_Empaque;
            L_IMPORTE.Text = _controlador.Data_Importe.ToString("n2");
            L_TASA_IVA.Text = _controlador.Data_TasaIva.ToString("n2");
            L_IVA.Text = _controlador.Data_Iva.ToString("n2");
            L_TOTAL.Text = _controlador.Data_Total.ToString("n2");
            L_COSTO_UND.Text = _controlador.Data_CostoUnd.ToString("n2");
            L_COSTO_EMP.Text = _controlador.Data_CostoEmp.ToString("n2");
            L_EX_REAL.Text = _controlador.Data_ExReal.ToString("n"+_controlador.NDecimales);
            L_EX_DISPONIBLE.Text = _controlador.Data_ExDisponible.ToString("n" + _controlador.NDecimales);
            TB_PRECIO.Text = _controlador.Data_Precio.ToString("n2");
        }

        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            ProcesarItem();
        }

        private void ProcesarItem()
        {
            IrFoco();
            _controlador.ProcesarItem();
            if (_controlador.ProcesarItemIsOk)
            {
                Salir();
            }
        }

        private void TB_CANT_Leave(object sender, EventArgs e)
        {
            var cnt = decimal.Parse(TB_CANT.Text);
            _controlador.setCantidad(cnt);
            ActualizarData();
        }

        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void TB_DSCTO_Leave(object sender, EventArgs e)
        {
            var dsct = decimal.Parse(TB_DSCTO.Text);
            _controlador.setDescuento(dsct);
            TB_DSCTO.Text = _controlador.Data_Dscto.ToString("n2");
            ActualizarData();
        }

        private void TB_NOTAS_Leave(object sender, EventArgs e)
        {
            _controlador.setNotas(TB_NOTAS.Text);
        }

        private void AgregarEditarItemFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.AbandonarIsOk || _controlador.ProcesarItemIsOk)
            {
                e.Cancel = false;
            }
        }

        private void BT_VER_COSTO_Click(object sender, EventArgs e)
        {
            VisualizarCosto();
        }

        private void VisualizarCosto()
        {
            IrFoco();
            _controlador.VisualizarCosto();
            if (_controlador.VisualizarCostoIsActivo)
            {
                P_COSTO.Visible = true;
                ActualizarData();
            }
        }

        private void BT_ELIMINAR_DSCTO_Click(object sender, EventArgs e)
        {
            EliminarDscto();
        }

        private void EliminarDscto()
        {
            _controlador.EliminarDscto();
            TB_DSCTO.Text = _controlador.Data_Dscto.ToString("n2");
            ActualizarData();
        }

        private void TB_PRECIO_Leave(object sender, EventArgs e)
        {
            var precio= decimal.Parse(TB_PRECIO.Text);
            _controlador.setPrecio(precio);
            TB_PRECIO.Text = _controlador.Data_Precio.ToString("n2");
            ActualizarData();
        }

    }

}