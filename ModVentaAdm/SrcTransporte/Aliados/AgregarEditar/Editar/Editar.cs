using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Aliados.AgregarEditar.Editar
{
    public class Editar: ImpAgregarEditar, IEditar
    {
        private int _idAliado;


        public void setAliadoEditar(int id)
        {
            _idAliado = id;
        }

        public override void Procesar()
        {
        }

        protected override bool CargarData()
        {
            try
            {
                var r01= Sistema.MyData.TransporteAliado_GetById(_idAliado);
                Ficha.setData(r01.Entidad);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
    }
}