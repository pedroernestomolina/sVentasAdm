using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.UsarDisponer.Handler
{
    public class Imp: Vista.IHnd
    {
        private string _idCliente;
        private bool _procesarIsOK;
        private bool _abandonarIsOK;
        private decimal _montoDisponer;
        private decimal _montoDeuda;
        private OOB.Transporte.ClienteAnticipo.Obtener.Ficha _cliente;


        public string Get_Cliente { get { return _cliente.Info; } }
        public decimal Get_MontoADisponer { get { return _montoDisponer; } }
        public decimal Get_Cliente_MontoAnticipo { get { return _cliente.montoDiv; } }
        public decimal Get_MontoDeudaVerificar { get { return _montoDeuda; } }


        public Imp()
        {
            _idCliente="";
            _montoDeuda = 0m;
            _montoDisponer = 0m;
            _cliente = null;
            _procesarIsOK = false;
            _abandonarIsOK = false;
        }


        public void Inicializa()
        {
            _idCliente = "";
            _montoDeuda = 0m;
            _montoDisponer = 0m;
            _cliente = null;
            _procesarIsOK = false;
            _abandonarIsOK = false;
        }
        Vista.Frm frm;
        public void Inicia()
        {
            if (cargarData()) 
            {
                if (_cliente.montoDiv > 0m)
                {
                    frm = new Vista.Frm();
                    frm.setControlador(this);
                    frm.ShowDialog();
                }
            }
        }

        public void setCliente(string idCliente)
        {
            _idCliente = idCliente;
        }

        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar()
        {
            _procesarIsOK = true;
        }

        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }


        private bool cargarData()
        {
            try
            {
                var r01 = Sistema.MyData.Transporte_Cliente_Anticipo_Obtener_ById(_idCliente);
                _cliente = r01.Entidad;
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        public void setMontoDisponer(decimal monto)
        {
            _montoDisponer = monto;
        }
        public void setMontoDeuda(decimal monto)
        {
            _montoDeuda = monto;
        }
    }
}