using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item.AliadosLlamado
{
    public class data
    {
        public int cnt { get; set; }
        public decimal precio { get; set; }
        public OOB.Transporte.Aliado.Entidad.Ficha aliado { get; set; }
        public string AliadoLlamado { get { return aliado.nombreRazonSocial; } }
        public decimal Importe { get { return cnt * precio; } }
        public void setCnt(int cant)
        {
            cnt = cant;
        }
    }
}