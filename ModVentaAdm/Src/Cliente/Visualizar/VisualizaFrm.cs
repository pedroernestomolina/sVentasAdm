using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Visualizar
{

    public partial class VisualizaFrm : Form
    {

        private Gestion _controlador;


        public VisualizaFrm()
        {
            InitializeComponent();
        }


        public void setControlador(Gestion ctr)
        {
            _controlador = ctr;
        }

        private void AgregarEditarFrm_Load(object sender, EventArgs e)
        {
            L_CIRIF.Text = _controlador.CiRif;
            L_CODIGO.Text = _controlador.Codigo;
            L_RAZON_SOCIAL.Text = _controlador.RazonSocial;
            L_DIR_FISCAL.Text = _controlador.DirFiscal;

            L_GRUPO.Text=_controlador.Grupo;
            L_CATEGORIA.Text=_controlador.Categoria;
            L_NIVEL.Text=_controlador.Nivel;

            L_DIR_DESPACHO.Text = _controlador.DirDespacho;
            L_PAIS.Text = _controlador.Pais;
            L_ESTADO.Text = _controlador.Estado;
            L_ZONA.Text = _controlador.Zona;
            L_COD_POSTAL.Text = _controlador.CodPostal;

            L_PERSONA.Text = _controlador.Persona;
            L_TELEFONO_1.Text = _controlador.Telefono_1;
            L_TELEFONO_2.Text = _controlador.Telefono_2;
            L_CELULAR.Text = _controlador.Celular;
            L_FAX.Text = _controlador.Fax;
            L_EMAIL.Text = _controlador.Email;
            L_WEBSITE.Text = _controlador.WebSite;

            L_VENDEDOR.Text = _controlador.Vendedor;
            L_PRECIO.Text = _controlador.Precio;
            L_DSCTO.Text = _controlador.Dscto;
            L_CARGO.Text = _controlador.Cargo;

            L_COBRADOR.Text = _controlador.Cobrador;

            CHB_CREDITO.Checked = _controlador.IsCredito;
            L_LIMITE_CREDITO.Text = _controlador.LimiteCredito;
            L_LIMITE_DOC.Text = _controlador.LimiteDoc;
            L_DIAS_CREDITO.Text = _controlador.DiasCredito;
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
        {
            this.Close();
        }

    }

}