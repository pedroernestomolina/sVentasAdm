using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.Componente.Administrador.Vistas
{
    public interface IAdmin: Src.IGestion, Src.Gestion.IAbandonar 
    {
        ILista data { get; }
        string Get_TituloAdm { get; }
        int Get_CntItem { get; }


        void Buscar();
        void AnularItem();
        void VisualizarDocumento();
        void Imprimir();
    }
}