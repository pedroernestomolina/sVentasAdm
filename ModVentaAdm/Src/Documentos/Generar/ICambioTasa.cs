using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar
{

    public interface ICambioTasa
    {

        bool CambioTasaIsOk { get; }
        decimal TasaCambiar { get; }


        void setTasa(decimal TasaDivisa);
        void Inicializa();
        void Inicia();

    }

}