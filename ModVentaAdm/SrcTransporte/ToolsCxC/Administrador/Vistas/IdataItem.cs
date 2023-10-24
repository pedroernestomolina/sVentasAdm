using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ToolsCxC.Administrador.Vistas
{
    public interface IdataItem
    {
        decimal ImporteMov { get; set; }
        decimal AnticipoMov { get; set; }
        string NroRecibo { get; set; }
        string  CiRif { get; set; }
        string Nombre { get; set; }
        DateTime FechaMov { get; set; }
        decimal MontoRec { get; set; }
        string Estatus { get; set; }
    }
}