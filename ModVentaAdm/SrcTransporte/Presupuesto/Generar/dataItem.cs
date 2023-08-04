using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class dataItem : Remision.IObservador
    {
        private List<Item.IItem> _lst;
        private BindingList<Item.IItem> _bl;
        private BindingSource _bs;
        private List<IItemObservador> _observadores;


        public BindingSource Source_Get { get { return _bs; } }
        public Item.IItem ItemActual { get { return (Item.IItem)_bs.Current; } }
        public int Cnt_Get { get { return _bs.Count; } }
        public decimal MontoPagoAliado_Get { get { return _bl.Sum(s => s.Item.Get_ImporteAliadosLLamados); } }
        public List<Item.IItem> GetItems { get { return _bl.ToList(); } }


        public dataItem(Remision.IRemision itemsRemision)
        {
            _lst = new List<Item.IItem>();
            _bl = new BindingList<Item.IItem>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
            _observadores = new List<IItemObservador>();
            itemsRemision.AgregarObservador(this);
        }

        public void Inicializa()
        {
            _lst.Clear();
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public void AgregarItem(Item.IItem item)
        {
            var _id = 1;
            if (_lst.Count > 0)
            {
                _id = _lst.Max(m => m.id) + 2;
            }
            item.setId(_id);
            _bl.Add(item);
            _bs.CurrencyManager.Refresh();
            foreach (var obs in _observadores)
            {
                obs.OnItemAgregado(item);
            }
        }
        public void EliminarItem(Item.IItem item)
        {
            _bl.Remove(item);
            _bs.CurrencyManager.Refresh();
            foreach (var obs in _observadores)
            {
                obs.OnItemEliminado(item);
            }
        }


        private decimal _tasaDivisa = 0m;
        public void setTasaDivisa(decimal tasa)
        {
            _tasaDivisa = tasa;
        }


        public void LimpiarTodo()
        {
            _lst.Clear();
            _bl.Clear();
            _bs.CurrencyManager.Refresh();
        }
        public bool DataIsOk()
        {
            if (_lst == null)
            {
                Helpers.Msg.Alerta("LISTA DE ITEMS NO DEFINIDA");
                return false;
            }
            if (_lst.Count == 0)
            {
                Helpers.Msg.Alerta("NO HAY ITEMS DEFINIDOS");
                return false;
            }
            var _cntAliado = _lst.Where(w => w.Item.AliadoIsOk != true).Count();
            if (_cntAliado > 0)
            {
                Helpers.Msg.Alerta("HAY ITEMS PENDIENTES POR DEFINIR EL ALIADO");
                return false;
            }
            var _cntImporte = _lst.Where(w => w.ImporteItemMostrar == 0m).Count();
            if (_cntImporte > 0)
            {
                Helpers.Msg.Alerta("HAY ITEMS CON IMPORTES INCORRECTOS (MONTO EN CERO)");
                return false;
            }
            if (_lst.Where(w => w.Item.Get_DescripcionFull.Trim()=="").Count()>0)
            {
                Helpers.Msg.Alerta("HAY ITEMS CON CAMPO [ MAS INFORMACION ] QUE NO PUEDEN ESTAR VACIO");
                return false;
            }
            if (_lst.Where(w => w.Item.Get_UnidadesDetall.Trim() == "").Count() > 0)
            {
                Helpers.Msg.Alerta("HAY ITEMS CON CAMPO [ UNIDADES DETALLE ] QUE NO PUEDEN ESTAR VACIO");
                return false;
            }
            return true;
        }
        public bool DataPendienteIsOk()
        {
            if (_lst == null)
            {
                Helpers.Msg.Alerta("LISTA DE ITEMS NO DEFINIDA");
                return false;
            }
            if (_lst.Count == 0)
            {
                Helpers.Msg.Alerta("NO HAY ITEMS DEFINIDOS");
                return false;
            }
            //var _cntAliado = _lst.Where(w => w.Item.AliadoIsOk != true).Count();
            //if (_cntAliado > 0)
            //{
            //    Helpers.Msg.Alerta("HAY ITEMS PENDIENTES POR DEFINIR EL ALIADO");
            //    return false;
            //}
            var _cntImporte = _lst.Where(w => w.ImporteItemMostrar == 0m).Count();
            if (_cntImporte > 0)
            {
                Helpers.Msg.Alerta("HAY ITEMS CON IMPORTES INCORRECTOS (MONTO EN CERO)");
                return false;
            }
            //if (_lst.Where(w => w.Item.Get_DescripcionFull.Trim() == "").Count() > 0)
            //{
            //    Helpers.Msg.Alerta("HAY ITEMS CON CAMPO [ MAS INFORMACION ] QUE NO PUEDEN ESTAR VACIO");
            //    return false;
            //}
            //if (_lst.Where(w => w.Item.Get_UnidadesDetall.Trim() == "").Count() > 0)
            //{
            //    Helpers.Msg.Alerta("HAY ITEMS CON CAMPO [ UNIDADES DETALLE ] QUE NO PUEDEN ESTAR VACIO");
            //    return false;
            //}
            return true;
        }



        //PARA LOS TOTALES TODO EN BASE A DIVISA
        public decimal MontoNetoDivisa { get { return _bl.Sum(s => s.Item.Get_Importe); } }
        public decimal MontoIvaDivisa { get { return _bl.Sum(s => s.Item.Get_Iva); } }
        public decimal MontoNetoDivisa_Exento { get { return _bl.Where(w => w.Item.Get_Alicuota_ID == "0000000004").Sum(s => s.Item.Get_Importe); } }
        public decimal MontoNetoDivisa_Tasa1 { get { return _bl.Where(w => w.Item.Get_Alicuota_ID == "0000000001").Sum(s => s.Item.Get_Importe); } }
        public decimal MontoNetoDivisa_Tasa2 { get { return _bl.Where(w => w.Item.Get_Alicuota_ID == "0000000002").Sum(s => s.Item.Get_Importe); } }
        public decimal MontoNetoDivisa_Tasa3 { get { return _bl.Where(w => w.Item.Get_Alicuota_ID == "0000000003").Sum(s => s.Item.Get_Importe); } }


        public void RegistrarObservador(IItemObservador observador)
        {
            _observadores.Add(observador);
        }
        public void EliminarObservador(IItemObservador observador)
        {
            _observadores.Remove(observador);
        }


        public void NotificarRemisionDocPresupuesto(OOB.Transporte.Documento.Entidad.Presupuesto.Ficha ficha)
        {
            try
            {
                var r01 = Sistema.MyData.Sistema_TasaFiscal_GetLista();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
                {
                    throw new Exception(r01.Mensaje);
                }

                foreach (var it in ficha.items)
                {
                    var _alicuota = new alicuota()
                    {
                        codigo = "",
                        desc = it.alicuotaDesc,
                        id = it.alicuotaId,
                        tasa = it.alicuotaTasa,
                    };
                    var _tipoServicio = new OOB.Transporte.ServPrest.Entidad.Ficha()
                    {
                        detalle = "",
                        descripcion = it.servicioDesc,
                        codigo = it.servicioCodigo,
                        id = it.servicioId,
                    };
                    var _item = new Item.Agregar.Agregar();
                    _item.Item.setSolicitadoPor(ficha.encabezado.docSolicitadoPor);
                    _item.Item.setModuloaCargar(ficha.encabezado.docModuloCargar);
                    _item.Item.setCntDias(it.cntDias);
                    _item.Item.setCntUnidades(it.cntUnidades);
                    _item.Item.setPrecioDivisa(it.precioNetoDivisa);
                    _item.Item.setDscto(it.dscto);
                    _item.Item.setAlicuota(_alicuota);
                    _item.Item.setUnidadesDetalle(it.unidadesDesc);
                    _item.Item.setTipoServicio(_tipoServicio);
                    _item.Item.setDescripcion(it.servicioDetalle);

                    foreach (var xr in it.aliados)
                    {
                        var _aliado = new OOB.Transporte.Aliado.Entidad.Ficha()
                        {
                            id = xr.idAliado ,
                            ciRif = xr.ciRif ,
                            codigo = xr.codigo,
                            nombreRazonSocial = xr.descripcion,
                        };
                        _item.Item.setAliado(_aliado);
                        _item.Item.setPrecioAliadoPautado(xr.precioUnitDivisa);
                        _item.Item.setCntAliadoPautado(xr.cntDias);
                        _item.Item.GuardarAliado();
                    }
                    _item.Item.setAliado(null);
                    _item.Item.setPrecioAliadoPautado(0m);
                    _item.Item.setCntAliadoPautado(0);
                    _item.Item.setDescripcionFull(it.notas);
                    _item.Item.setTasaIva(it.alicuotaTasa);
                    _item.setTasaFiscal(r01.ListaD);
                    _item.AlicuotaSetFichaById(it.alicuotaId);
                    foreach (var xr in it.fechaServ) 
                    {
                        var hora = DateTime.Parse(xr.hora);
                        DateTime fechaYHora = xr.fecha.Date + hora.TimeOfDay;
                        _item.Item.setFecha(xr.fecha);
                        _item.Item.setHora(fechaYHora);
                        _item.Item.AgregarFecha();
                    }
                    AgregarItem(_item);
                }
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
    }
}