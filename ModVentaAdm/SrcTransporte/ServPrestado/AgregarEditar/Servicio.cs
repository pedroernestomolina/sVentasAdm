using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.ServPrestado.AgregarEditar
{
    public class Servicio
    {
        private string _codigo;
        private string _detalle;
        private string _descripcion;


        public string Codigo_GetData { get { return _codigo; } }
        public string Detalle_GetData { get { return _detalle; } }
        public string Descripcion_GetData { get { return _descripcion; } }


        public Servicio()
        {
            limpiar();
        }


        public void Inicializa()
        {
            limpiar();
        }
        private void limpiar()
        {
            _codigo = "";
            _descripcion = "";
            _detalle = "";
        }

        public void setCodigo(string desc)
        {
            _codigo = desc;
        }
        public void setDetalle(string desc)
        {
            _detalle = desc;
        }
        public void setDescripcion(string desc)
        {
            _descripcion= desc;
        }
        public void setData(OOB.Transporte.ServPrest.Entidad.Ficha ficha)
        {
            _codigo = ficha.codigo;
            _descripcion = ficha.descripcion;
            _detalle = ficha.detalle;
        }

        public bool DatosAgregarIsOk()
        {
            if (_codigo.Trim() == "") 
            {
                Helpers.Msg.Alerta("CAMPO [ CODIGO ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_descripcion.Trim() == "")
            {
                Helpers.Msg.Alerta("CAMPO [ DESCRIPCION ] NO PUEDE ESTAR VACIO");
                return false;
            }
            return true;
        }
    }
}