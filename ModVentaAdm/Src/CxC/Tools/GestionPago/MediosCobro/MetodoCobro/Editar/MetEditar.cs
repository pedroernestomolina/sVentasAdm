using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro.Editar
{
    public class MetEditar: Metodo, IMetEditar
    {
        new public string GetTituloFicha { get { return "METODO DE COBRO: EDITAR"; } }
        public bool EditarIsOk { get { return ProcesarIsOK; } }
    }
}