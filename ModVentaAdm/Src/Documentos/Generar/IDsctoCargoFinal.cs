using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar
{

    public interface IDsctoCargoFinal
    {

        bool IsOk { get; }
        decimal DsctoFinal { get; }
        decimal CargoFinal { get; }


        void Inicializa();
        void Inicia();
        void setData(decimal Monto, decimal TasaDivisa);

    }

}