using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.DocVenta.Generar.Item.Editar
{
    public interface IEditar: IItem
    {
        void setTasaFiscal(List<OOB.Sistema.Fiscal.Entidad.Ficha> _tasasFiscal);
        void setItemEditar(data data);
        void setCliente(string idCliente);
        void setTipoDocumentoIsFactura(bool tipoDocIsFactura);
    }
}