using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Administrador.Vistas
{
    public interface IdataItem
    {
        string  CiRif { get; set; }
        string Nombre { get; set; }
        DateTime FechaMov { get; set; }
        decimal MontoMov { get; set; }
        decimal MontoRec { get; set; }
        string Estatus { get; set; }
        string AplicaRet { get; set; }
    }
}