using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB
{
    public interface ITextoBuscar
    {
        string Get_TextoBuscar { get; }
        void setTextoBuscar(string desc);
    }
}