using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Vistas
{
    public partial class Frm : Form
    {
        private IMain _controlador;
        private bool _modoInicializa = false;
        //
        public Frm()
        {
            InitializeComponent();
            InicializaDGV();
        }
        private void InicializaDGV()
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
            c1.DataPropertyName = "AliadoCiRif";
            c1.HeaderText = "CiRif";
            c1.Visible = true;
            c1.Width = 100;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "AliadoNombre";
            c2.HeaderText = "Nombre";
            c2.Visible = true;
            c2.MinimumWidth = 200;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "ImporteDiv";
            c3.HeaderText = "Importe($)";
            c3.Visible = true;
            c3.Width = 100;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c3.DefaultCellStyle.Format = "n2";

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            _modoInicializa = true;
            //
            DGV.DataSource = _controlador.dataItems.Get_Source;
            actualizarEnc();
            actualizaContador();
            //
            _modoInicializa = false;
        }
        public void setControlador(IMain ctr)
        {
            _controlador = ctr;
        }

        private void BT_ITEM_AGREGAR_Click(object sender, EventArgs e)
        {
            AgregarAliado();
        }
        private void BT_ITEM_ELIMINAR_Click(object sender, EventArgs e)
        {
            EliminarAliado();
        }
        //
        private void AgregarAliado()
        {
            _controlador.AgregarAliadoInv();
            actualizaContador();
        }
        private void EliminarAliado()
        {
            _controlador.EliminarAliadoInv();
            actualizaContador();
        }
        private void actualizaContador()
        {
            L_ALIADOS_INV.Text = "Aliados Involucrados: ( " + _controlador.dataItems.Get_CntItems.ToString("n0") + " )";
        }
        private void actualizarEnc()
        {
            L_FECHA.Text = _controlador.Get_Doc_Fecha;
            L_NUMERO.Text = _controlador.GetDoc_Numero;
            L_CLIENTE.Text = _controlador.Get_Doc_Entidad;
            L_TIPO_DOC.Text = _controlador.Get_Doc_Nombre;
            L_MONTO_DOC.Text = _controlador.Get_Doc_Monto;
        }
    }
}