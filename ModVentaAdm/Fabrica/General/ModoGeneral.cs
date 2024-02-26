using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.General
{
    public partial class ModoGeneral: IFabrica
    {
        private __.Cliente.IData.IData _dataCliente;
        //
        public __.Cliente.IData.IData DataCliente { get { return _dataCliente; } }
        public __.Documentos.IData.IData DataDocumentos { get { return null; } }
        //
        public string NombreHerramienta { get { return "Tools Ventas Adm."; } }
        public void Iniciar_FrmPrincipal(Src.Principal.Gestion ctr)
        {
            Sistema.NombreHerramienta = NombreHerramienta;
            var frm = new Src.Principal.PrincipalFrm();
            frm.setControlador(ctr);
            frm.ShowDialog();
        }
        //
        public ModoGeneral()
        {
            _dataCliente = new __.Cliente.ParaZufu.ImpData();
        }


        //CLIENTES
        public SrcComun.Clientes.Filtros.Comp.Vista.IVista ClienteFiltros()
        {
            return new SrcComun.Clientes.Filtros.Comp.Handler.Imp();
        }
        public SrcComun.Clientes.Filtros.Opciones.IOpciones 
            ClienteFiltrosOpciones_SaldoPend
        {
            get
            {
                return new SrcComun.Clientes.Filtros.Opciones.ImpOpciones()
                {
                    ActivarGrupo = true,
                    ActivarEstado = true,
                    ActivarVendedor = true,
                    ActivarZona = true,
                    ActivarCategoria = true,
                    ActivarCobrador = true,
                    ActivarCredito=true,
                    ActivarEstatus=true,
                    ActivarNivel=true,
                };
            }
        }

        //DOCUMENTOS
        public SrcComun.Documento.NotaCreditoAdm.Generar.Vista.IVista 
            Documentos_Generar_NotaCreditoAdm()
        {
            return null;
        }
    }
}