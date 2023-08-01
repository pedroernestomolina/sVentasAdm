using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ServPrestado.AgregarEditar.Editar
{
    public class Editar: ImpAgregarEditar, IEditar
    {
        private int _idFicha;


        public void setFichaEditar(int id)
        {
            _idFicha= id;
        }

        public override void Procesar()
        {
            _procesarIsOK = false;
            if (Ficha.DatosAgregarIsOk())
            {
                var r = Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    var fichaOOB = new OOB.Transporte.ServPrest.Editar.Ficha()
                    {
                        idFicha = _idFicha,
                        codigo = Ficha.Codigo_GetData,
                        descripcion = Ficha.Descripcion_GetData,
                        detalle = Ficha.Detalle_GetData,
                    };
                    try
                    {
                        var r01 = Sistema.MyData.TransporteServPrest_Editar(fichaOOB);
                        _procesarIsOK = true;
                        Helpers.Msg.EditarOk();
                    }
                    catch (Exception e)
                    {
                        Helpers.Msg.Error(e.Message);
                    }
                }
            }
        }

        protected override bool CargarData()
        {
            try
            {
                var r01 = Sistema.MyData.TransporteServPrest_GetById (_idFicha);
                Ficha.setData(r01.Entidad);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
            return true;
        }
    }
}