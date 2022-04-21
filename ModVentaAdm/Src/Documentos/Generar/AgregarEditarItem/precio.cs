using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.AgregarEditarItem
{
    
    public class precio
    {

        private string _id;
        private string _etiqueta;
        private string _empaque;
        private int _contenido;
        private decimal _pNeto;
        private string _decimales;


        public string ID { get { return _id; } }
        public string Etiqueta { get { return _etiqueta; } }
        public string Empaque { get { return _empaque+"/"+_contenido.ToString().Trim(); } }
        public decimal PNeto { get { return _pNeto; } }
        public string EmpqDesc { get { return _empaque; } }
        public int EmpqCont { get { return _contenido; } }
        public string Decimales { get { return _decimales; } }


        public precio()
        {
            _id = "";
            _etiqueta = "";
            _empaque = "";
            _contenido = 0;
            _pNeto=0.0m;
            _decimales = "";
        }

        public precio(string _id, string _et, string _empq, int _cont, decimal _pn, string _decimales)
            : this()
        {
            this._id = _id;
            this._etiqueta = _et;
            this._empaque = _empq;
            this._contenido = _cont;
            this._pNeto = _pn;
            this._decimales = _decimales;
        }

    }

}