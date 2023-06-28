using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModVentaAdm.Fabrica
{
    public class ModoTransporte: IFabrica
    {
        public string NombreHerramienta { get { return "Gestión Presupuestos."; } }

        public void Iniciar_FrmPrincipal(Src.Principal.Gestion ctr)
        {
            Sistema.NombreHerramienta = NombreHerramienta;

            var frm = new  SrcTransporte.Principal.PrincipalFrm();
            frm.setControlador(ctr);
            frm.ShowDialog();
        }
    }
}
