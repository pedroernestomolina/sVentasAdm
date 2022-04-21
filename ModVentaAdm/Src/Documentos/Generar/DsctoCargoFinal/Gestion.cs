using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.DsctoCargoFinal
{

    public class Gestion : IDsctoCargoFinal
    {


        private bool _abandonarIsOk;
        private bool _isOk;
        private decimal _dscto;
        private decimal _cargo;
        private decimal _monto;
        private decimal _total;
        private Decimal _factorDivisa;
        private decimal _montoDscto;
        private decimal _montoCargo;


        public bool IsOk { get { return _isOk; } }
        public bool AbandonarIsOk { get { return _abandonarIsOk; } }
        public decimal DsctoFinal { get { return _dscto; } }
        public decimal CargoFinal { get { return _cargo; } }
        public decimal Monto { get { return _monto; } }
        public decimal Total { get { return _total; } }
        public decimal TotalDivisa { get { return _total / _factorDivisa; } }


        public Gestion()
        {
            _abandonarIsOk = false;
            _isOk = false;
            _dscto = 0m;
            _monto = 0m;
            _total = 0m;
            _montoDscto = 0m;
            _montoCargo = 0m;
            _cargo = 0m;
        }


        public void Inicializa()
        {
            _abandonarIsOk = false;
            _isOk = false;
            _dscto = 0m;
            _monto = 0m;
            _total = 0m;
            _montoDscto = 0m;
            _montoCargo = 0m;
            _cargo = 0m;
        }

        private DsctoCargoFinalFrm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new DsctoCargoFinalFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            return true;
        }

        public void Procesar()
        {
            var xmsg = "Datos Correctos, Procesar Documento ?";
            var msg = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (msg == DialogResult.Yes)
            {
                _isOk = true;
            }
        }

        public void Abandonar()
        {
            var xmsg = "Abandonar Cambios ?";
            var msg = MessageBox.Show(xmsg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (msg == DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }

        public void setData(decimal monto, decimal factorDivisa)
        {
            _monto = monto;
            _factorDivisa = factorDivisa;
        }

        public void setDscto(decimal dscto)
        {
            if (dscto >= 100)
            {
                Helpers.Msg.Error("Porcentaje (%) Incorrecto");
                return;
            }
            if (dscto<=0)
            {
                return;
            }

            var r00 = Sistema.MyData.Permiso_GenerarDoc_DsctoGlobal(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _dscto = dscto;
                Calcula();
            }
        }

        private void Calcula()
        {
            _montoDscto = (_monto * _dscto / 100);
            _total = _monto - _montoDscto;
            _montoCargo = (_total * _cargo / 100);
            _total += _montoCargo;
            _total = Math.Round(_total, 2, MidpointRounding.AwayFromZero);
        }

        public void setCargo(decimal cargo)
        {
            if (cargo >= 100)
            {
                Helpers.Msg.Error("Porcentaje (%) Incorrecto");
                return;
            }
            _cargo = cargo;
            Calcula();
        }

        public void EliminarDscto()
        {
            _dscto = 0m;
            Calcula();
        }

        public void EliminarCargo()
        {
            _cargo= 0m;
            Calcula();
        }

    }

}