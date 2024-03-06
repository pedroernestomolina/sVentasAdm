﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.Transporte
{
    public partial class ModoTransporte: IFabrica
    {
        private __.Cliente.IData.IData _dataCliente;
        private __.Documentos.IData.IData _dataDocumentos;
        //
        public __.Cliente.IData.IData DataCliente { get { return _dataCliente; } }
        public __.Documentos.IData.IData DataDocumentos { get { return _dataDocumentos; } }
        //
        public ModoTransporte()
        {
            _dataDocumentos = new __.Documentos.ParaTranspRivas.ImpData();
            _dataCliente = new __.Cliente.ParaTranspRivas.ImpData();
        }
        //


        public string NombreHerramienta { get { return "Gestión Administrativa."; } }
        public void Iniciar_FrmPrincipal(Src.Principal.Gestion ctr)
        {
            Sistema.NombreHerramienta = NombreHerramienta;
            var frm = new SrcTransporte.Principal.PrincipalFrm();
            frm.setControlador(ctr);
            frm.ShowDialog();
        }

        //CLIENTES
        public SrcComun.Clientes.Filtros.Comp.Vista.IVista 
            ClienteFiltros()
        {
            return null;
        }
        public SrcComun.Clientes.Filtros.Opciones.IOpciones ClienteFiltrosOpciones_SaldoPend
        {
            get 
            {
                return null;
            }
        }

        //DOCUMENTOS
        //NOTA DE CREDITO 
        public SrcComun.Documento.NotaCreditoAdm.Generar.Vista.IVista 
            Documentos_Generar_NotaCredito()
        {
            return new SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Handler.ImpVista();
        }
        //NOTA DE CREDITO ADMINISTRATIVA, DONDE NO EXISTE EL DOCUMENTO FISCAL
        public SrcComun.Documento.NotaCreditoAdm.Generar.Vista.IVista
            Documentos_Generar_NotaCreditoAdm()
        {
            return new SrcTransporte.DocVenta.NotaCreditoAdm.GenerarAdm.Handler.ImpVista();
        }
    }
}