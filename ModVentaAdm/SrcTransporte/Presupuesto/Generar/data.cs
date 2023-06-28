using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class data
    {
        private dataItem _items;
        private dataTotales _totales;


        public dataItem Items { get { return _items; } }
        public dataTotales Totales { get { return _totales; } }


        public data()
        {
            _items = new dataItem();
            _totales = new dataTotales();
        }


        public void Inicializa()
        {
            _items.Inicializa();
            _totales.Inicializa();
        }
    }
}