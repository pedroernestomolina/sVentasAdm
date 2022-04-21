using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Maestros
{
    
    public interface IGestion
    {

        string Maestro { get; }
        int TotalItems { get; }
        System.Windows.Forms.BindingSource Source { get; }


        bool CargarData();
        void AgregarItem();
        void EditarItem();
        void Inicializa();

    }

}