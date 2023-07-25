using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica
{
    public interface IFabrica
    {
        string NombreHerramienta { get;  }
        void Iniciar_FrmPrincipal(Src.Principal.Gestion ctr);
        bool AnularDocumentoVenta(Src.Administrador.data GetItemActual, Src.Anular.Gestion _gAnular);
    }
}