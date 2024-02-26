using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Vista
{
    public interface IFiscal
    {
        decimal Get_Tasa { get; }
        decimal Get_Base { get; }
        decimal Get_Iva { get; }
        decimal Get_Total { get; }
        //
        void setBase(decimal monto);
        void setTasa(decimal tasa);
        void Inicializa();
    }
}