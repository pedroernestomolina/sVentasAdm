using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Maestro.Transp.ServPrest
{
    public interface IServ: Utils.Maestro.IMaestro
    {
        Utils.Maestro.ILista Lista { get; }
    }
}