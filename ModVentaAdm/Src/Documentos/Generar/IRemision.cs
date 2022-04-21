using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar
{
    
    public interface IRemision
    {

        BindingSource RemisionSource { get; }
        bool ItemSeleccionadoIsOk { get;  }
        string IdItemSeleccionado { get; }


        void Inicializa();
        void setIdCliente(string p);
        void Inicia();
        void setTipoDocRemision(List<Remision.tipoDoc> list);

    }

}