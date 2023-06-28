using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public interface IGenerar : Src.IGestion, Src.Gestion.IAbandonar, Src.Gestion.IProcesar
    {
        BindingSource SourceItems_Get { get; }
        data Ficha { get; }

        void NuevoDocumento();
        void AgregarItem();
        void EliminarItem();
        void EditarItem();
    }
}