using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Aliados.AgregarEditar.Editar
{
    public interface IEditar: IAgregarEditar
    {
        void setAliadoEditar(int id);
    }
}
