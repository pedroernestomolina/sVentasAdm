using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar
{
    
    public interface IBuscarProducto
    {

        bool ItemSeleccionadoIsOk { get; }
        string IdItemSeleccionado { get;  }


        void setActivarBusPorCodigo();
        void setActivarBusPorDescripcion();
        void setActivarBusPorReferencia();
        void setCadenaBusq(string cad);
        void setDepositoBuscar(string p);
        void setActivarSeleccionItem(bool p);
        void setFactorCambio(decimal TasaDivisa);
        void Inicializa();
        void Buscar();

    }

}