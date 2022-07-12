using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro.Editar
{
    
    public interface IMetEditar: IMetodoAgregarEditar
    {

        bool EditarIsOk { get; }
        dataItem ItemAgregarEditar { get; }
        void setItemEditar(dataItem dataItem);

    }

}