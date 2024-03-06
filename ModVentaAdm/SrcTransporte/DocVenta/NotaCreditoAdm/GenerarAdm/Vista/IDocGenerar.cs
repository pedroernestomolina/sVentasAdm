using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.NotaCreditoAdm.GenerarAdm.Vista
{
    public interface IDocGenerar
    {
        Generar.Vista.IFiscal MontoExento { get; }
        Generar.Vista.IFiscal MontoFiscal_1 { get; }
        Generar.Vista.IFiscal MontoFiscal_2 { get; }
        Generar.Vista.IFiscal MontoFiscal_3 { get; }
        decimal Get_Subt_Base { get; }
        decimal Get_Subt_Imp { get; }
        decimal Get_Total { get; }
        string Get_Motivo { get; }
        DateTime Get_FechaEmision { get; }
        string Get_DocNumero { get; }
        decimal Get_TasaCambio { get; }
        //
        void setFechaEmision(DateTime fecha);
        void setDocumentoNro(string docNro);
        void setFactorCambio(decimal tasaCamb);
        void setMotivo(string mot);
        void Inicializa();
        void ValidarDataIsOk();
        void Limpiar();
    }
}