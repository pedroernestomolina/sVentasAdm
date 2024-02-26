using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica
{
    public interface IFabrica
    {
        __.Cliente.IData.IData DataCliente { get; }
        __.Documentos.IData.IData DataDocumentos { get; }
        //
        string NombreHerramienta { get;  }
        void Iniciar_FrmPrincipal(Src.Principal.Gestion ctr);
        bool AnularDocumentoVenta(Src.Administrador.data GetItemActual, Src.Anular.Gestion _gAnular);
        void VisualizarDocumento(Src.Administrador.data GetItemActual);
        OOB.Resultado.Lista<OOB.Documento.Lista.Ficha> DocumentosGetLista(OOB.Documento.Lista.Filtro filtro);
        void ClienteAnticipos(object idCliente);


        //CLIENTES
        SrcComun.Clientes.Filtros.Comp.Vista.IVista 
            ClienteFiltros();
        SrcComun.Clientes.Filtros.Opciones.IOpciones
            ClienteFiltrosOpciones_SaldoPend { get; }

        //DOCUMENTOS
        SrcComun.Documento.NotaCreditoAdm.Generar.Vista.IVista 
            Documentos_Generar_NotaCreditoAdm();
    }
}