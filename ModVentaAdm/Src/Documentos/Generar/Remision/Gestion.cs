using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.Remision
{
    
    public class Gestion: IRemision
    {


        private string _idCliente;
        private List<tipoDoc> _lst;
        private BindingSource _bs;
        private Cliente.Documentos.Gestion _gDoc;
        private bool _itemSeleccionadoIsOk;
        private string _idItemSeleccionado;


        public BindingSource RemisionSource { get { return _bs; } }
        public string IdItemSeleccionado { get { return _idItemSeleccionado; } }
        public bool ItemSeleccionadoIsOk { get { return _itemSeleccionadoIsOk; } }


        public Gestion()
        {
            _idCliente = "";
            _lst= new List<tipoDoc>();
            _bs= new BindingSource();
            _bs.DataSource = _lst;
            _gDoc = new Cliente.Documentos.Gestion();
            _itemSeleccionadoIsOk = false;
            _idItemSeleccionado = "";
        }


        public void Inicializa()
        {
            _idCliente = "";
            _itemSeleccionadoIsOk = false;
            _idItemSeleccionado = "";
        }

        public void Inicia()
        {
            if (_bs.Current == null)
                return;

            var filtroOOB = new OOB.Maestro.Cliente.Documento.Filtro()
            {
                desde = null,
                hasta = null,
                autoCliente = _idCliente,
                tipoDoc = OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.SinDefinir,
            };
            var tipoDoc = (tipoDoc)_bs.Current;
            switch (tipoDoc.id)
            {
                case "01":
                    filtroOOB.tipoDoc = OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.Factura;
                    break;
                case "04":
                    filtroOOB.tipoDoc = OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.NotaEntrega;
                    break;
                case "05":
                    filtroOOB.tipoDoc = OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.Presupuesto;
                    break;
                case "06":
                    filtroOOB.tipoDoc = OOB.Maestro.Cliente.Documento.Enumerados.enumTipoDoc.Pedido;
                    break;
            }
            var r01 = Sistema.MyData.Cliente_Documentos_GetLista(filtroOOB);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return;
            }

            _gDoc.Inicializa();
            _gDoc.setHabilitarSeleccionarDocumento(true);
            _gDoc.setHabilitarVisualizarDocumento(false);
            _gDoc.setCliente(_idCliente);
            _gDoc.setLista(r01.ListaD);
            _gDoc.Inicia();
            if (_gDoc.SeleccionarDocumentoIsOk)
            {
                _itemSeleccionadoIsOk=true;
                _idItemSeleccionado=_gDoc.IdDocumentoSeleccionado;
            }
        }

        public void setIdCliente(string id)
        {
            _idCliente = id;
        }

        public void setTipoDocRemision(List<tipoDoc> list)
        {
            _lst .Clear();
            _lst= list.Select(s =>
            {
                return new tipoDoc(s.id, s.descripcion);
            }).ToList(); ;
            _bs.DataSource =_lst;
            _bs.CurrencyManager.Refresh();
        }

    }

}