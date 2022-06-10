using ModVentaAdm.Data.Prov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm
{

    public class Gestion
    {

        private Src.Identificacion.ILogin _gLogin ;
        private Src.Principal.Gestion _gestionPrincipal;
        

        public Gestion()
        {
            _gLogin= new Src.Identificacion.Login();
            _gestionPrincipal = new Src.Principal.Gestion();
        }


        public void Inicia()
        {
            if (CargarDataXML()) 
            {
                Sistema.MyData = new DataPrv(Sistema.Instancia, Sistema.BaseDatos);
                Sistema.EquipoEstacion = Environment.MachineName;

                _gLogin.Inicializa();
                _gLogin.Inicia();
                if (_gLogin.IsOk)
                {
                    _gestionPrincipal.Inicializa();
                    _gestionPrincipal.Inicia();
                }
            }
        }

        private bool CargarDataXML()
        {
            var  r01= Helpers.Utilitis.CargarXml();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            return true;
        }

    }

}