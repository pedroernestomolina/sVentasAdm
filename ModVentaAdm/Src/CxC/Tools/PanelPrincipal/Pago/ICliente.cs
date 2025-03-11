using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.PanelPrincipal.Pago
{
    public interface ICliente
    {
        bool AbandonarFichaIsOk { get; }
        void AbandonarFicha();
        //
        bool ProcesarFichaIsOk { get; }
        void ProcesarFicha();
        //
        void Inicializa();
        void Inicia();
        void setIdEntidad(object id);

        //
        string GetCliente { get; }
        decimal GetMontoAnticipo { get; }
        decimal GetMontoAbonar { get; }
        void setMontoAbonar(decimal monto);
    }
}