using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Remision
{
    public class data
    {
        private string _id;
        private string _nombre;
        private string _numero;
        private string _fecha;
        private string _tipo;


        public string docId { get { return _id; } }
        public string docNombre { get { return _nombre; } }
        public string docNumero { get { return _numero; } }
        public string docFecha { get { return _fecha; } }
        public string docTipo { get { return _tipo; } }
        public data()
        {
            _id = "";
            _nombre = "";
            _numero = "";
            _fecha = "";
            _tipo = "";
        }


        public void Inicializa()
        {
            _id = "";
            _nombre = "";
            _numero = "";
            _fecha = "";
            _tipo = "";
        }
        public void Limpiar()
        {
            _id = "";
            _nombre = "";
            _numero = "";
            _fecha = "";
            _tipo = "";
        }

        public void setId(string id)
        {
            _id = id;
        }
        public void setNombre(string desc)
        {
            _nombre = desc;
        }
        public void setNumero(string desc)
        {
            _numero = desc;
        }
        public void setFecha(DateTime fecha)
        {
            _fecha = fecha.ToShortDateString();
        }
        public void setTipo(string tipo)
        {
            _tipo= tipo;
        }
    }
}