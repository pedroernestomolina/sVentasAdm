using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item.Agregar
{
    public class Agregar: ImpItem, IAgregar
    {
        public Agregar()
            :base()
        {
            _validarDatosCompletos = false;
        }
        public override void Procesar()
        {
            _procesarIsOK = false;
            if (Item.VerificarDatosIsOK()) 
            {
                if (_validarDatosCompletos) 
                {
                    if (!Item.AliadoIsOk)
                    {
                        Helpers.Msg.Alerta("CAMPO [ALIADO] NO PUEDE ESTAR VACIO");
                        return;
                    }
                    if (Item.Get_Aliado_PrecioPautado==0m)
                    {
                        Helpers.Msg.Alerta("CAMPO [PRECIO PAUTADO POR ALIADO] NO PUEDE ESTAR VACIO");
                        return;
                    }
                }
                var r= Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    _procesarIsOK = true;
                }
            }
        }


        private bool _validarDatosCompletos;
        public void setValidarDatosCompletos(bool est)
        {
            _validarDatosCompletos = est;
        }
        public void setSolicitadoPor(string desc)
        {
            Item.setSolicitadoPor(desc);
        }
        public void setModuloCargar(string desc)
        {
            Item.setModuloaCargar(desc);
        }
    }
}