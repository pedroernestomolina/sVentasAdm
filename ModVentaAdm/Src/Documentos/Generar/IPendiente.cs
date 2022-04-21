using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar
{
    
    public interface IPendiente
    {

        bool ItemSeleccionadoIsOk { get; }
        int IdItemSeleccionado { get; }


        void Inicializa();
        void Inicia();
        void setData(List<OOB.Venta.Temporal.Pendiente.Lista.Ficha> list);

    }

}