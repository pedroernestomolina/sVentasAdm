using ModVentaAdm.Data.Prov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Sistema.MotorDatos = new ConfiguracionMotorDatos();
            var r01 = Helpers.Utilitis.CargarXml();
            if (r01.Result != OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Sistema.MyData = new DataPrv(Sistema.MotorDatos.Instancia,
                                                Sistema.MotorDatos.BaseDatos);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Src.Identificacion.ILogin _gLogin = new Src.Identificacion.Login();
                _gLogin.Inicializa();
                _gLogin.Inicia();
                if (_gLogin.IsOk)
                {
                    Sistema.Fabrica = new Fabrica.Transporte.ModoTransporte();
                    //Sistema.Fabrica = new Fabrica.General.ModoGeneral();
                    var gestion = new Gestion();
                    gestion.Inicia();
                }
            }
            else
            {
                Helpers.Msg.Error(r01.Mensaje);
                Application.Exit();
            }
        }
    }
}