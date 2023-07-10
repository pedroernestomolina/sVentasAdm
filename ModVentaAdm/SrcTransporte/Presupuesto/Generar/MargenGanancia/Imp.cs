using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.MargenGanancia
{
    public class Imp: IBeneficio, IItemObservador, ITotalObservador
    {
        private data _data;
        private dataItem _items;
        private dataTotales _totales;


        public data Data { get { return _data; } }


        public Imp(dataItem items, dataTotales totales)
        {
            _data = new data();
            _items = items;
            _totales = totales;
            _items.RegistrarObservador(this);
            _totales.RegistrarObservador(this);
        }


        public void Inicializa()
        {
            _data.Inicializa();
            setPagoAliado(_items.MontoPagoAliado_Get);
            setMontoDoc(_totales.MontoNeto_MonedaDivisa_Get);
        }
        Frm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        public void setMontoDoc(decimal monto)
        {
            _data.setMontoDoc(monto);
        }
        public void setPagoAliado(decimal monto)
        {
            _data.setPagoAliado(monto);
        }


        private bool _abandonarIsOK;
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = true;
        }


        private bool CargarData()
        {
            return true;
        }

        public void OnItemAgregado(Item.IItem item)
        {
            setPagoAliado(_items.MontoPagoAliado_Get);
        }
        public void OnItemEliminado(Item.IItem item)
        {
            setPagoAliado(_items.MontoPagoAliado_Get);
        }
        public void ActualizarTotal()
        {
            setMontoDoc(_totales.MontoNeto_MonedaDivisa_Get);
        }
    }
}