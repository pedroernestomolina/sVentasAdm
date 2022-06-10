using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Identificacion
{
    
    public interface ILogin: IGestion
    {

        bool IsOk { get; }
        string GetNombreHerramienta { get; }


        void Aceptar();
        void SetCodigo(string p);
        void SetClave(string p);


    }

}