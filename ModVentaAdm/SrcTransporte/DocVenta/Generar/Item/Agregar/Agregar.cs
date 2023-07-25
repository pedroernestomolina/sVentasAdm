using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Item.Agregar
{
    public class Agregar:ImpItem, IAgregar
    {
        public Agregar()
            : base()
        {
            _idCliente = "";
            _solicitadoPor = "";
            _moduloCargar = "";
        }


        public override void Procesar()
        {
            _procesarIsOK = false;
            if (Item.VerificarDatosIsOK())
            {
                var r = Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    _procesarIsOK = true;
                }
            }
        }
        private string _idCliente;
        public void setCliente(string idCliente)
        {
            _idCliente = idCliente;
        }
        private string _solicitadoPor;
        public void setSolicitadoPor(string desc)
        {
            _solicitadoPor = desc;
        }
        private string _moduloCargar;
        public void setModuloCargar(string desc)
        {
            _moduloCargar = desc;
        }


        public override void HabilitarPresupuesto()
        {
            AgregarPresupuesto();
        }
        private Utils.DocLista.IDocLista _listDoc;
        private void AgregarPresupuesto()
        {
            try
            {
                var _idSistemaDocumento = Sistema.Id_SistDocumento_Presupuesto;
                var s01 = Sistema.MyData.Sistema_TipoDocumento_GetFichaById(_idSistemaDocumento);
                if (s01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(s01.Mensaje);
                }

                var filtroOOB = new OOB.Transporte.Documento.Remision.Lista.Filtro()
                {
                    codTipoDoc = s01.Entidad.codigo,
                    idCliente = _idCliente,
                };
                var r01 = Sistema.MyData.TransporteDocumento_Remision_ListaBy(filtroOOB);
                var _lst = r01.ListaD.Where(w => !w.isAnulado).OrderByDescending(o => o.docId).ToList();

                _listDoc = new Utils.DocLista.Remision.Imp();
                _listDoc.Inicializa();
                _listDoc.setDataCargar(_lst);
                _listDoc.Inicia();
                if (_listDoc.ItemSeleccionadoIsOk)
                {
                    var _doc=(Utils.DocLista.Remision.data)_listDoc.ItemSeleccionado;
                    var _docNumero =_doc.DocNumero;
                    var _desc = "PRESUPUESTO #" + _docNumero+ Environment.NewLine+_doc.SolicitadoPor+Environment.NewLine+_doc.ModuloCargar;
                    var _precio =  _doc.Monto;
                    _data.setDescripcion(_desc);
                    _data.setCnt(1);
                    _data.setDscto(0m);
                    _data.setPrecioDivisa(_precio);
                    _data.setItemServicio(null);
                    _data.setItemPresupuesto(_doc);
                }
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }

        public override void HabilitarServicio()
        {
            AgregarServicio();
        }
        private Presupuesto.Generar.Item.Agregar.IAgregar _itemAgregar;
        private void AgregarServicio()
        {
            _itemAgregar = new Presupuesto.Generar.Item.Agregar.Agregar();
            _itemAgregar.Inicializa();
            _itemAgregar.setTasaFiscal(_tasasFiscal);
            _itemAgregar.setValidarDatosCompletos(false);
            _itemAgregar.setSolicitadoPor(_solicitadoPor);
            _itemAgregar.setModuloCargar(_moduloCargar);
            _itemAgregar.Inicia();
            if (_itemAgregar.ProcesarIsOK)
            {
                var _desc = _itemAgregar.Item.Get_Descripcion;
                var _precio = _itemAgregar.Item.Get_Importe;
                _data.setDescripcion(_desc);
                _data.setCnt(1);
                _data.setDscto(0m);
                _data.setPrecioDivisa(_precio);
                _data.setItemServicio(_itemAgregar);
            }
        }
    }
}