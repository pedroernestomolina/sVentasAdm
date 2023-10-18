using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Agregar.Vistas
{
    public interface IHndCaja
    {
        decimal Get_MontoPendMonAct { get; }
        decimal Get_MontoPendMonDiv { get; }
        decimal MontoCajaPago { get; }
        BindingSource Get_CajaSource { get; }
        IEnumerable<IdataCaja> Get_Lista { get; }
        IEnumerable<IdataCaja> Get_CajasUsadas { get; }

        void Inicializa();
        void CargarData();
        void setDataCargar(IEnumerable<IdataCaja> _lst);
        void setFactorCambio(decimal factor);
        void setMontoPendDiv(decimal montoDiv);
        void EditarMontoAbonar();
        void ActualizarSaldosPend();
        bool IsOk();
    }
}