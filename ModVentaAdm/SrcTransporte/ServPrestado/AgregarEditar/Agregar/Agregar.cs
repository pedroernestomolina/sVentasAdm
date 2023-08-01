using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ServPrestado.AgregarEditar.Agregar
{
    public class Agregar: ImpAgregarEditar, IAgregar
    {
        private int _idAgreagado;


        public int IdAgregado { get { return _idAgreagado; } }


        public Agregar()
            : base()
        {
            _idAgreagado = -1;
        }

        public override void Inicializa()
        {
            base.Inicializa();
            _idAgreagado = -1;
        }
        public override void Procesar()
        {
            _procesarIsOK = false;
            if (Ficha.DatosAgregarIsOk())
            {
                var r = Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    var fichaOOB = new OOB.Transporte.ServPrest.Agregar.Ficha()
                    {
                        codigo = Ficha.Codigo_GetData,
                        descripcion = Ficha.Descripcion_GetData,
                        detalle = Ficha.Detalle_GetData,
                    };
                    try
                    {
                        var r01 = Sistema.MyData.TransporteServPrest_Agregar(fichaOOB);
                        _idAgreagado = r01.Id;
                        _procesarIsOK = true;
                        Helpers.Msg.AgregarOk();
                    }
                    catch (Exception e)
                    {
                        Helpers.Msg.Error(e.Message);
                    }
                }
            }
        }
    }
}