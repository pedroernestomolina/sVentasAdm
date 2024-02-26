using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.Generar.Vista
{
    public interface IDocGenerar
    {
        IFiscal MontoExento { get; }
        IFiscal MontoFiscal_1 { get; }
        IFiscal MontoFiscal_2 { get; }
        IFiscal MontoFiscal_3 { get; }
        decimal Get_Subt_Base { get; }
        decimal Get_Subt_Imp { get; }
        decimal Get_Total { get; }
        string Get_Motivo { get; }
        //
        void setMotivo(string mot);
        void Inicializa();
        void ValidarDataIsOk();
        void Limpiar();
    }
}