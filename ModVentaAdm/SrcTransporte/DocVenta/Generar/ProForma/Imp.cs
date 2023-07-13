using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.ProForma
{
    public class Imp: ImpGenerar, IProForma
    {
        public override string TipoDocumento_Get { get { return "PRO - FORMA"; } }


        public Imp()
            :base()
        {
        }
    }
}