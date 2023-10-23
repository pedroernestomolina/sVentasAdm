using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Administrador.Vistas
{
    public interface IdataItem
    {
        DateTime FechaMov { get; set; }
        decimal Monto { get; set; }
        string Motivo { get; set; }
        string Estatus { get; set; }
        string TipoMov { get; set; }
        int SignoMov { get; set; }
        string CajaDesc { get; set; }
        string EsDivisa { get; set; }
    }
}