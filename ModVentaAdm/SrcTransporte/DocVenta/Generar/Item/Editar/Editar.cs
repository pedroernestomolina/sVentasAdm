using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Item.Editar
{
    public class Editar:ImpItem, IEditar
    {
        public Editar()
            : base()
        {
            _idCliente = "";
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
        public override void HabilitarPresupuesto()
        {
            AgregarPresupuesto();
        }
        private Utils.DocLista.Remision.IRemision _listDoc;
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
                    var _doc = (Utils.DocLista.Remision.data)_listDoc.ItemSeleccionado;
                    var _docNumero = _doc.DocNumero;
                    var _desc = "PRESUPUESTO #" + _docNumero;
                    var _precio = _doc.Monto;
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


        private Presupuesto.Generar.Item.IItem _itemServicioEditar;
        public void setItemEditar(data data)
        {
            _itemServicioEditar = data.Get_ItemServicio;
            Item.setDescripcion(data.Get_Descripcion);
            Item.setCnt (data.Get_Cnt);
            Item.setPrecioDivisa(data.Get_PrecioDivisa);
            Item.setDscto(data.Get_Dscto);
            Item.setItemPresupuesto(data.Get_ItemPresupuesto);
            Item.setItemServicio(data.Get_ItemServicio);
            if (_tasasFiscal != null)
            {
                var it = _tasasFiscal.FirstOrDefault(f => f.id == data.Get_Alicuota_ID);
                if (it != null)
                {
                    var nr = new Presupuesto.Generar.alicuota() { id = it.id, codigo = "", desc = it.ToString(), tasa = it.tasa };
                    Item.setAlicuota(nr);
                }
            }
        }

        private string _idCliente;
        public void setCliente(string idCliente)
        {
            _idCliente = idCliente;
        }

        public override void HabilitarServicio()
        {
            EditarServicio();
        }
        private Presupuesto.Generar.Item.Editar.IEditar _itemEditar;
        private void EditarServicio()
        {
            _itemEditar = new Presupuesto.Generar.Item.Editar.Editar();
            _itemEditar.Inicializa();
            _itemEditar.setTasaFiscal(_tasasFiscal);
            _itemEditar.setItemEditar(_itemServicioEditar.Item);
            _itemEditar.Inicia();
            if (_itemEditar.ProcesarIsOK)
            {
                var _desc = _itemEditar.Item.Get_Descripcion;
                var _precio = _itemEditar.Item.Get_Importe;
                _data.setDescripcion(_desc);
                _data.setCnt(1);
                _data.setDscto(0m);
                _data.setPrecioDivisa(_precio);
                _data.setItemServicio(_itemEditar);
            }
        }
    }
}