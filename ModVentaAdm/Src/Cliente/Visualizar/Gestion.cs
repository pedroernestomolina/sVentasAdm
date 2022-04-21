using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Cliente.Visualizar
{
    
    public class Gestion
    {


        private string _autoId;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;


        public string CiRif { get {return  _cliente.ciRif;} }
        public string Codigo { get { return _cliente.codigo; } }
        public string RazonSocial { get { return _cliente.razonSocial; } }
        public string DirFiscal { get { return _cliente.dirFiscal; } }
        public string Grupo { get { return _cliente.grupo; } }
        public string Categoria { get { return _cliente.categoria; } }
        public string Nivel { get { return _cliente.nivel; } }
        public string Pais { get { return _cliente.pais; } }
        public string DirDespacho { get { return _cliente.dirDespacho; } }
        public string Estado { get { return _cliente.estado; } }
        public string Zona { get { return _cliente.zona; } }
        public string CodPostal { get { return _cliente.codPostal; } }
        public string Persona { get { return _cliente.contacto; } }
        public string Telefono_1 { get { return _cliente.telefono1; } }
        public string Telefono_2 { get { return _cliente.telefono2; } }
        public string Celular { get { return _cliente.celular; } }
        public string Fax { get { return _cliente.fax; } }
        public string Email { get { return _cliente.email; } }
        public string WebSite { get { return _cliente.webSite; } }
        public string Vendedor { get { return _cliente.vendedor; } }
        public string Precio { get { return _cliente.tarifa; } }
        public string Cobrador { get { return _cliente.cobrador; } }
        public bool IsCredito { get { return _cliente.estatusCredito == "1" ? true : false; } }
        public string Dscto 
        { 
            get 
            {
                var dat = string.Format("{0:N2}%", _cliente.dscto);
                return dat;
            } 
        }
        public string Cargo 
        {
            get 
            { 
                var dat = string.Format("{0:N2}%",_cliente.cargo);
                return dat;
            }
        }
        public string LimiteCredito 
        {
            get 
            { 
                var dat = string.Format("{0:N2}",_cliente.limiteCredito);
                return dat;
            }
        }
        public string LimiteDoc 
        {
            get 
            { 
                var dat = string.Format("{0:N0}",_cliente.limiteDoc);
                return dat;
            }
        }
        public string DiasCredito 
        {
            get 
            { 
                var dat = string.Format("{0:N0}",_cliente.diasCredito);
                return dat;
            }
        }


        public Gestion()
        {
        }


        public void Inicializa()
        {
            _autoId = "";
            _cliente = null;
        }

        public void setFicha(string id)
        {
            _autoId=id;
        }

        public void setFicha(OOB.Maestro.Cliente.Entidad.Ficha ficha)
        {
            _autoId = "";
            _cliente = ficha;
        }

        VisualizaFrm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new VisualizaFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            var rt = true;

            if (_autoId != "") 
            {
                var r01 = Sistema.MyData.Cliente_GetFicha(_autoId);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return false;
                }
                _cliente = r01.Entidad;
            }

            return rt;
        }

    }

}