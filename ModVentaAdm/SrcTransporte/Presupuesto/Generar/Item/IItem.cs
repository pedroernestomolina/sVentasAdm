using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item
{
    public interface IItem: Src.IGestion, 
                            Src.Gestion.IAbandonar, 
                            Src.Gestion.IProcesar
    {
        int id { get; }
        data Item { get; }
        Aliado.IAliado MiAliado { get; }
        LibUtilitis.CtrlCB.ICtrl Alicuota { get; }
        LibUtilitis.CtrlCB.ICtrl TipoServ { get; }
        // PARA EL DGV CABECERA A MOSTRAR
        string ServItemMostrar { get; }
        string AliadoItemMostrar { get; }
        decimal PrecioItemMostrar{ get;  }
        int CantDiasItemMostrar { get; }
        int CantVehicItemMostrar { get; }
        decimal ImporteItemMostrar { get; }

        void Aliados();
        void setId(int id);
        void AlicuotaSetFichaById(string id);
        void TipoServSetFichaById(string id);

        //
        LibUtilitis.CtrlCB.ICtrl TipoTurno { get; }
        void TipoTurnoSetFichaById(string id);
    }
}