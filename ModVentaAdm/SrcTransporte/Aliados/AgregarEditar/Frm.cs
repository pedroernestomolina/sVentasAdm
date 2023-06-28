using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Aliados.AgregarEditar
{
    public partial class Frm : Form
    {
        private IAgregarEditar _controlador;


        public Frm()
        {
            InitializeComponent();
            InicializaGrid_Telefono();
        }
        private void InicializaGrid_Telefono()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);
            var f2 = new Font("Serif", 10, FontStyle.Bold);

            DGV_TELEFONO.AllowUserToAddRows = false;
            DGV_TELEFONO.AllowUserToDeleteRows = false;
            DGV_TELEFONO.AutoGenerateColumns = false;
            DGV_TELEFONO.AllowUserToResizeRows = false;
            DGV_TELEFONO.AllowUserToResizeColumns = false;
            DGV_TELEFONO.AllowUserToOrderColumns = false;
            DGV_TELEFONO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_TELEFONO.MultiSelect = false;
            DGV_TELEFONO.ReadOnly = true;

            var c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "Numero_GetData";
            c1.HeaderText = "Numero";
            c1.Visible = true;
            c1.MinimumWidth = 150;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DGV_TELEFONO.Columns.Add(c1);
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            IrFoco_Identificacion();
            TB_CIRIF.Text = _controlador.Ficha.CiRif_GetData;
            TB_CODIGO.Text = _controlador.Ficha.Codigo_GetData;
            TB_RAZON_SOCIAL.Text = _controlador.Ficha.NombreRazonSocial_GetData;
            TB_DIR_FISCAL.Text = _controlador.Ficha.DirFiscal_GetData;
            TB_PERSONA_CONTACTO.Text = _controlador.Ficha.PersonaContacto_GetData;

            DGV_TELEFONO.DataSource = _controlador.Ficha.MisTelefonos.MisNumeros_GetSource;
        }
        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (_controlador.ProcesarIsOK || _controlador.AbandonarIsOK)
            {
                e.Cancel = false;
            }
        }


        public void setControlador(IAgregarEditar ctr)
        {
            _controlador = ctr;
        }


        private void TB_CIRIF_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.setCirRif(TB_CIRIF.Text.Trim().ToUpper());
            TB_CIRIF.Text = _controlador.Ficha.CiRif_GetData;
        }
        private void TB_CODIGO_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.setCodigo(TB_CODIGO.Text.Trim().ToUpper());
            TB_CODIGO.Text = _controlador.Ficha.Codigo_GetData;
        }
        private void TB_RAZON_SOCIAL_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.setNombreRazonSocial(TB_RAZON_SOCIAL.Text.Trim().ToUpper());
            TB_RAZON_SOCIAL.Text = _controlador.Ficha.NombreRazonSocial_GetData;
        }
        private void TB_DIR_FISCAL_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.setDirFiscal(TB_DIR_FISCAL.Text.Trim());
            TB_DIR_FISCAL.Text = _controlador.Ficha.DirFiscal_GetData;
        }
        private void TB_PERSONA_CONTACTO_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.setPersonaContacto(TB_PERSONA_CONTACTO.Text.Trim().ToUpper());
            TB_PERSONA_CONTACTO.Text = _controlador.Ficha.PersonaContacto_GetData;
        }
        private void TB_TELEFONO_Leave(object sender, EventArgs e)
        {
            _controlador.Ficha.MisTelefonos.setNumero(TB_TELEFONO.Text.Trim().ToUpper());
            TB_TELEFONO.Text = _controlador.Ficha.MisTelefonos.Numero_GetData;
        }


        private void BT_IDENTIFICACION_Click(object sender, EventArgs e)
        {
            ApagarTodos();
            GB_IDENTIFICACION.Visible = true;
            IrFoco_Identificacion();
        }
        private void BT_TELEFONOS_Click(object sender, EventArgs e)
        {
            ApagarTodos();
            GB_TELEFONO.Visible = true;
            IrFoco_Telefono();
        }
        private void BT_UBICACION_Click(object sender, EventArgs e)
        {
            //IrFoco();
            ApagarTodos();
        }


        private void BT_GUARDAR_TELEFONO_Click(object sender, EventArgs e)
        {
            IrFoco_Telefono();
            _controlador.Ficha.MisTelefonos.GuardarNumero();
            TB_TELEFONO.Text = _controlador.Ficha.MisTelefonos.Numero_GetData;
        }
        private void BT_ELIMINAR_TELEFONO_Click(object sender, EventArgs e)
        {
            IrFoco_Telefono();
            _controlador.Ficha.MisTelefonos.EliminarNumero();
        }
        private void BT_PROCESAR_Click(object sender, EventArgs e)
        {
            IrFoco_Identificacion();
            ProcesarFicha();
        }
        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            IrFoco_Identificacion();
            AbandonarFicha();
        }


        private void ProcesarFicha()
        {
            _controlador.Procesar();
            if (_controlador.ProcesarIsOK) 
            {
                Salir();
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

        private void ApagarTodos()
        {
            GB_IDENTIFICACION.Visible = false;
            GB_TELEFONO.Visible = false;
        }
        private void IrFoco_Identificacion()
        {
            GB_IDENTIFICACION.Focus();
            TB_CIRIF.Focus();
        }
        private void IrFoco_Telefono()
        {
            GB_TELEFONO.Focus();
            TB_TELEFONO.Focus();
        }
    }
}