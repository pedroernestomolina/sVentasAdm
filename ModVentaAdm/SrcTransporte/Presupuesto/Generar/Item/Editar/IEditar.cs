using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item.Editar
{
    public interface IEditar: IItem
    {
        void setItemEditar(data data);
        void setTasaFiscal(List<OOB.Sistema.Fiscal.Entidad.Ficha> tasas);
    }
}
