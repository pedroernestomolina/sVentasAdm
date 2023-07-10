﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class dataItem
    {
        private List<Item.IItem> _lst;
        private BindingList<Item.IItem> _bl;
        private BindingSource _bs;
        private List<IItemObservador> _observadores;


        public BindingSource Source_Get { get { return _bs; } }
        public Item.IItem ItemActual { get { return (Item.IItem)_bs.Current; } }
        public int Cnt_Get { get { return _bs.Count; } }
        public decimal MontoPagoAliado_Get { get { return _bl.Sum(s => s.Item.Get_Aliado_PrecioPautado); } }
        public List<Item.IItem> GetItems { get { return _bl.ToList(); } }


        public dataItem()
        {
            _lst = new List<Item.IItem>();
            _bl = new BindingList<Item.IItem>(_lst);
            _bs = new BindingSource();
            _bs.DataSource = _bl;
            _bs.CurrencyManager.Refresh();
            _observadores = new List<IItemObservador>();
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
            if (_lst.Count>0)
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
            _tasaDivisa=tasa;
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
            var _cntAliado = _lst.Where(w => w.Item.AliadoIsOk!= true).Count();
            if (_cntAliado > 0) 
            {
                Helpers.Msg.Alerta("HAY ITEMS PENDIENTES POR DEFINIR EL ALIADO");
                return false;
            }
            var _cntAliadoPrecio = _lst.Where(w => w.Item.Get_Aliado_PrecioPautado ==0m ).Count();
            if (_cntAliadoPrecio > 0)
            {
                Helpers.Msg.Alerta("HAY ITEMS PENDIENTES POR DEFINIR EL MONTO PAUTADO POR EL ALIADO");
                return false;
            }

            var _cntImporte = _lst.Where(w => w.ImporteItemMostrar ==0m ).Count();
            if (_cntImporte > 0)
            {
                Helpers.Msg.Alerta("HAY ITEMS CON IMPORTES INCORRECTOS (MONTO EN CERO)");
                return false;
            }
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
    }
}