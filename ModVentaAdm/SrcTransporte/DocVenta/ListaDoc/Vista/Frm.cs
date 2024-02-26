﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.DocVenta.ListaDoc.Vista
{
    public partial class Frm : Form
    {
        private IVista _controlador;
        //
        private void InicializarDGV()
        {
            var f = new Font("Serif", 8, FontStyle.Bold);
            var f1 = new Font("Serif", 8, FontStyle.Regular);

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
            c1.DataPropertyName = "FechaEmision";
            c1.HeaderText = "Fecha";
            c1.Visible = true;
            c1.HeaderCell.Style.Font = f;
            c1.DefaultCellStyle.Font = f1;
            c1.Width = 70;

            var c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "TipoDocumento";
            c2.HeaderText = "Tipo";
            c2.Visible = true;
            c2.MinimumWidth = 80;
            c2.HeaderCell.Style.Font = f;
            c2.DefaultCellStyle.Font = f1;
            c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "DocumentoNro";
            c3.HeaderText = "Documento";
            c3.Visible = true;
            c3.Width = 90;
            c3.HeaderCell.Style.Font = f;
            c3.DefaultCellStyle.Font = f1;

            var c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "EntidadNombre";
            c4.HeaderText = "Nombre";
            c4.Visible = true;
            c4.MinimumWidth = 220;
            c4.HeaderCell.Style.Font = f;
            c4.DefaultCellStyle.Font = f1;
            c4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var c5 = new DataGridViewTextBoxColumn();
            c5.DataPropertyName = "EntidadCiRif";
            c5.HeaderText = "CiRif";
            c5.Visible = true;
            c5.Width = 100;
            c5.HeaderCell.Style.Font = f;
            c5.DefaultCellStyle.Font = f1;

            var c7 = new DataGridViewTextBoxColumn();
            c7.DataPropertyName = "MontoMonAct";
            c7.HeaderText = "Monto";
            c7.Visible = true;
            c7.Width = 90;
            c7.HeaderCell.Style.Font = f;
            c7.DefaultCellStyle.Font = f1;
            c7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c7.DefaultCellStyle.Format = "n2";

            var c8 = new DataGridViewTextBoxColumn();
            c8.DataPropertyName = "MontoMonDiv";
            c8.HeaderText = "Monto($)";
            c8.Visible = true;
            c8.Width = 90;
            c8.HeaderCell.Style.Font = f;
            c8.DefaultCellStyle.Font = f1;
            c8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            c8.DefaultCellStyle.Format = "n2";

            DGV.Columns.Add(c1);
            DGV.Columns.Add(c2);
            DGV.Columns.Add(c3);
            DGV.Columns.Add(c4);
            DGV.Columns.Add(c5);
            DGV.Columns.Add(c7);
            DGV.Columns.Add(c8);
        }
        public Frm()
        {
            InitializeComponent();
            InicializarDGV();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            DGV.DataSource = _controlador.ListaDoc.Get_Source;
            L_ITEM_CNT.Text = "Items Encontrados: "  + _controlador.ListaDoc.Cnt.ToString();
        }
        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                SeleccionarDocumento();
            }
        }
        public void setControlador(IVista ctr)
        {
            _controlador = ctr;
        }

        private void BT_SALIR_Click(object sender, EventArgs e)
        {
            abandonarFicha();
        }

        //
        private void SeleccionarDocumento()
        {
            _controlador.SeleccionarDoc();
            if (_controlador.ItemSeleccionadoIsOk)
            {
                salir();
            }
        }
        private void abandonarFicha()
        {
            _controlador.BtSalida.Opcion();
            if (_controlador.BtSalida.OpcionIsOK) 
            {
                salir();
            }
        }
        private void salir()
        {
            this.Close();
        }
    }
}