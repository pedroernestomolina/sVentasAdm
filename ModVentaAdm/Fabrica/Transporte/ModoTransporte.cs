using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Fabrica.Transporte
{
    public partial class ModoTransporte: IFabrica
    {
        public string NombreHerramienta { get { return "Gestión Administrativa."; } }

        public void Iniciar_FrmPrincipal(Src.Principal.Gestion ctr)
        {
            Sistema.NombreHerramienta = NombreHerramienta;

            var frm = new  SrcTransporte.Principal.PrincipalFrm();
            frm.setControlador(ctr);
            frm.ShowDialog();
        }
    }
}