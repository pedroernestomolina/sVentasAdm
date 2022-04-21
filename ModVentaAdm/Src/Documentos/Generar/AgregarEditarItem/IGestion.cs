using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.AgregarEditarItem
{
    
    public interface IGestion
    {

        int IdItemAgregado { get; }


        bool AgregarItem(data _data, int _idTempVenta);
        bool EditarItem(data _data, int _idTempVenta, int _idItemEditar);

    }

}