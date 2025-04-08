using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.DialogoFecha
{
    public interface IDialogoFecha: Src.IGestion
    {
        DateTime GetFecha { get; }
        bool IsOk { get; }
        //
        void setFecha(DateTime dateTime);
        void Procesar();
    }
}