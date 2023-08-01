using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ServPrestado.AgregarEditar.Editar
{
    public interface IEditar: IAgregarEditar
    {
        void setFichaEditar(int id);
    }
}
