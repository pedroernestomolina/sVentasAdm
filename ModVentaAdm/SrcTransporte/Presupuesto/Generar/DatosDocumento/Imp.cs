using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.DatosDocumento
{
    public class Imp: IDatosDoc
    {
        private data _data;


        public data Data { get { return _data; } }


        public Imp()
        {
            _abandonarIsOK = false;
            _procesarIsOK = false;
            _data = new data();
        }


        public void Inicializa()
        {
            _abandonarIsOK = false;
            _procesarIsOK = false;
            _data.Inicializa();
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

        private bool CargarData()
        {
            try
            {
                var r01 = Sistema.MyData.FechaServidor();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                _data.setFechaSistema(r01.Entidad);
                //
                var _lst = new List<ficha>();
                _lst.Add(new ficha() { id = "01", codigo = "", desc = "CONTADO" });
                _lst.Add(new ficha() { id = "02", codigo = "", desc = "CREDITO" });
                _data.CondicionPagoCargar(_lst);
                //
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }

        private bool _abandonarIsOK;
        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }

        private bool _procesarIsOK;
        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar()
        {
            _procesarIsOK = true;
        }
    }
}