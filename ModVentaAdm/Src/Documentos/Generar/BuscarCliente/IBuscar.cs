using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.BuscarCliente
{
    
    public interface IBuscar: IGestion
    {


        bool ItemSeleccionadoIsOk { get; }
        string IdItemSeleccionado { get; }


        void setActivarSeleccionItem(bool p);

    }

}