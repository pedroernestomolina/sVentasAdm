using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Item
{
    public interface IItem: Src.IGestion, 
                            Src.Gestion.IAbandonar, 
                            Src.Gestion.IProcesar
    {
        int id { get; }
        data Item { get; }
        LibUtilitis.CtrlCB.ICtrl Alicuota { get; }
        // PARA EL DGV CABECERA A MOSTRAR
        string ServItemMostrar { get; }
        decimal PrecioItemMostrar{ get;  }
        int CantItemMostrar { get; }
        decimal ImporteItemMostrar { get; }
        string PresupuestoMostrar { get; }

        void setId(int id);
        void AlicuotaSetFichaById(string id);

        void HabilitarPresupuesto();
        void HabilitarServicio();
        void HabilitarHojasServicio();
    }
}