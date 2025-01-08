using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.ReglasNegocio.EditarItemsAntesImprimir.Items
{
    public class data: Idata
    {
        private OOB.Transporte.Documento.Entidad.Venta.FichaDetalle it;
        private OOB.Transporte.Documento.Entidad.Venta.DetDoc it1;
        //
        public string Descripcion { get; set; }
        public OOB.Transporte.Documento.Entidad.Venta.FichaDetalle ItemDetalle { get; set; }
        //
        public data(OOB.Transporte.Documento.Entidad.Venta.FichaDetalle it)
        {
            this.it = it;
            Descripcion = it.detalle;
        }
        public data(OOB.Transporte.Documento.Entidad.Venta.DetDoc it1)
        {
            this.it1 = it1;
            Descripcion = it1.detalle;
        }
        public void setActualizaDescripcion(string texto, int modo)
        {
            Descripcion = texto;
            if (modo == 1) 
            {
                this.it.detalle = texto;
            }
            else if (modo == 2) 
            {
                this.it1.detalle = texto;
            }
        }
    }
}