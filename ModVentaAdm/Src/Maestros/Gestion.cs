using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Maestros
{
    
    public class Gestion
    {


        private IGestion _gestion;


        public string Maestro { get { return _gestion.Maestro; } }
        public int TotalItems { get { return _gestion.TotalItems; } }
        public BindingSource Source { get { return _gestion.Source; } }


        public Gestion()
        {
        }


        public void setGestion(IGestion gestion)
        {
            _gestion = gestion;
        }

        public void Inicia()
        {
            if (_gestion.CargarData())
            {
                var frm = new MaestroFrm();
                frm.setControlador(this);
                frm.ShowDialog();
            }
        }

        public void AgregarItem()
        {
            _gestion.AgregarItem();
        }

        public void EditarItem()
        {
            _gestion.EditarItem();
        }

        public void Inicializa()
        {
            _gestion.Inicializa();
        }

    }

}