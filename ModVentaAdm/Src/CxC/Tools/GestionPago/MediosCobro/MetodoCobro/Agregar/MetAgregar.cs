using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro.Agregar
{

    public class MetAgregar: Metodo, IMetAgregar
    {

        new public string GetTituloFicha { get { return "METODO DE COBRO: AGREGAR"; } }
        public bool AgregarIsOk { get { return ProcesarIsOK; } }


    }

}