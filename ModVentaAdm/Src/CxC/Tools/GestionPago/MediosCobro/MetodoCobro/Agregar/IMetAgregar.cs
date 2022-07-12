using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro.Agregar
{
    
    public interface IMetAgregar: IMetodoAgregarEditar
    {

        bool AgregarIsOk { get;  }
        dataItem  ItemAgregarEditar { get;  }

    }

}