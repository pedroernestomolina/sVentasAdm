using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.General
{
    public partial class ModoGeneral: IFabrica
    {
        public string NombreHerramienta { get { return "Tools Ventas Adm."; } }

        public void Iniciar_FrmPrincipal(Src.Principal.Gestion ctr)
        {
            Sistema.NombreHerramienta = NombreHerramienta;
            var frm = new Src.Principal.PrincipalFrm();
            frm.setControlador(ctr);
            frm.ShowDialog();
        }
    }
}