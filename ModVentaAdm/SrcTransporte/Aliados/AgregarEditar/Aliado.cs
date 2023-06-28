using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Aliados.AgregarEditar
{
    public class Aliado
    {
        public class Telefono 
        {
            private string _numero;
            private List<Telefono> _misNumeros;
            private BindingList<Telefono> _bl;
            private BindingSource _bs;


            public BindingSource MisNumeros_GetSource { get { return _bs; } }
            public string Numero_GetData { get { return _numero; } }
            public List<Telefono> MisNumeros { get { return _misNumeros; } }


            public Telefono()
            {
                _numero = "";
                _misNumeros= new List<Telefono>();
                _bl = new BindingList<Telefono>(_misNumeros);
                _bs = new BindingSource();
                _bs.DataSource = _bl;
                _bs.CurrencyManager.Refresh();
            }


            public void Inicializa()
            {
                _numero = "";
                _misNumeros.Clear();
            }
            public void setNumero(string desc)
            {
                _numero = desc;
            }
            public void GuardarNumero()
            {
                if (_numero.Trim() != "") 
                {
                    var ent = new Telefono();
                    ent.setNumero(_numero);

                    _misNumeros.Add(ent);
                    _bs.CurrencyManager.Refresh();
                    _numero = "";
                }
            }
            public void EliminarNumero()
            {
                if (_bs.Current != null) 
                {
                    var _it = _bs.Current;
                    _bs.Remove(_it);
                    _bs.CurrencyManager.Refresh();
                }
            }
            public void setData(List<OOB.Transporte.Aliado.Entidad.Telefono> list)
            {
                foreach (var rg in list) 
                {
                    var nr = new Telefono()
                    {
                        _numero = rg.numero,
                    };
                    _misNumeros.Add(nr);
                }
                _bs.CurrencyManager.Refresh();
            }
        }

        private string _cirif;
        private string _codigo;
        private string _nombreRazonSocial;
        private string _dirFiscal;
        private string _personaContacto;
        private Telefono _telefono;


        public string CiRif_GetData { get { return _cirif; } }
        public string Codigo_GetData { get { return _codigo; } }
        public string NombreRazonSocial_GetData { get { return _nombreRazonSocial; } }
        public string DirFiscal_GetData { get { return _dirFiscal; } }
        public string PersonaContacto_GetData { get { return _personaContacto; } }
        public Telefono MisTelefonos { get { return _telefono; } }


        public Aliado()
        {
            _telefono = new Telefono();
            limpiar();
        }


        public void Inicializa()
        {
            limpiar();
            _telefono.Inicializa();
        }
        private void limpiar()
        {
            _cirif = "";
            _personaContacto = "";
            _codigo = "";
            _dirFiscal = "";
            _nombreRazonSocial = "";
        }

        public void setCirRif(string desc)
        {
            _cirif = desc;
        }
        public void setCodigo(string desc)
        {
            _codigo = desc;
        }
        public void setNombreRazonSocial(string desc)
        {
            _nombreRazonSocial = desc;
        }
        public void setDirFiscal(string desc)
        {
            _dirFiscal = desc;
        }
        public void setPersonaContacto(string desc)
        {
            _personaContacto = desc;
        }
        public void setData(OOB.Transporte.Aliado.Entidad.Ficha ficha)
        {
            _cirif = ficha.ciRif;
            _codigo = ficha.codigo;
            _nombreRazonSocial = ficha.nombreRazonSocial;
            _dirFiscal = ficha.dirFiscal;
            _personaContacto = ficha.personaContacto;
            _telefono.setData( ficha.telefonos);
        }

        public bool DatosAgregarIsOk()
        {
            if (_cirif.Trim() == "") 
            {
                Helpers.Msg.Alerta("CAMPO [ CI/RIF ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_nombreRazonSocial.Trim() == "")
            {
                Helpers.Msg.Alerta("CAMPO [ NOMBRE /RAZON SOCIAL ] NO PUEDE ESTAR VACIO");
                return false;
            }
            if (_dirFiscal.Trim() == "")
            {
                Helpers.Msg.Alerta("CAMPO [ DIR FISCAL ] NO PUEDE ESTAR VACIO");
                return false;
            }
            return true;
        }
    }
}