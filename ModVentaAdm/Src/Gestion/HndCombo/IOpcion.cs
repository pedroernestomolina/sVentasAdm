using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Gestion.HndCombo
{

    public interface IOpcion
    {

        System.Windows.Forms.BindingSource Source { get; }
        string GetId { get; }
        ficha Item { get; }
        bool ItemsCargadosIsOk { get; }


        void setData(List<ficha> lst);
        void setFicha(string id);
        void Limpiar();
        void Inicializa();

    }

}