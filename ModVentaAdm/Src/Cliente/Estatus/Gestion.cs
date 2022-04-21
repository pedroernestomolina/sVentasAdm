using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Cliente.Estatus
{

    public class Gestion
    {

        public enum EnumEstatus { Activo = 1, Inactivo = 0 };


        private string _autoId;
        private OOB.Maestro.Cliente.Entidad.Ficha _cliente;
        private EnumEstatus _estatus;
        private bool _procesarIsOk;


        public string Cliente { get { return _cliente.ciRif + Environment.NewLine + _cliente.razonSocial+ Environment.NewLine +_cliente.dirFiscal; } }
        public EnumEstatus Estatus { get { return _estatus; } }
        public bool ProcesarIsOk { get { return _procesarIsOk; } }
        public bool AbandonarIsOk { get { return _abandonarIsOk; } }


        public Gestion()
        {
            Inicializa();
        }


        EstatusFrm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                frm = new EstatusFrm();
                frm.setControlador(this);
                frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            var rt = true;

            var r01 = Sistema.MyData.Cliente_GetFicha(_autoId);
            if (r01.Result ==  OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _cliente = r01.Entidad;
            _estatus = EnumEstatus.Activo;
            if (!_cliente.IsActivo)
                _estatus = EnumEstatus.Inactivo;

            return rt;
        }

        private void Limpiar()
        {
        }

        public void setFicha(string autoId)
        {
            this._autoId = autoId;
        }

        public void setEstatusActivo()
        {
            _estatus = EnumEstatus.Activo;
        }

        public void setEstatusInactivo()
        {
            _estatus = EnumEstatus.Inactivo;
        }

        public void Procesar()
        {
            var msg = MessageBox.Show("Guardar Cambios ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (msg == DialogResult.Yes)
            {
                var ficha= new OOB.Maestro.Cliente.EstatusActivarInactivar.Ficha()
                {
                     autoId=_autoId,
                };
                if (_estatus == EnumEstatus.Activo) 
                {
                    var r01 = Sistema.MyData.Cliente_Activar(ficha);
                    if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
                    {
                        Helpers.Msg.Error(r01.Mensaje);
                        return;
                    }
                }
                else
                {
                    var r01 = Sistema.MyData.Cliente_Inactivar(ficha);
                    if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                    {
                        Helpers.Msg.Error(r01.Mensaje);
                        return;
                    }
                }
                Helpers.Msg.EditarOk();
                _procesarIsOk = true;
            }
        }

        public void Inicializa()
        {
            _cliente = null;
            _autoId = "";
            _procesarIsOk = false;
            _abandonarIsOk = false;
        }

        public void setCliente(string auto)
        {
            this._autoId = auto;
        }

        private bool _abandonarIsOk;
        public void Salir()
        {
            _abandonarIsOk = false;
            var msg = MessageBox.Show("Abandonar Cambios ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (msg == DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }

    }

}