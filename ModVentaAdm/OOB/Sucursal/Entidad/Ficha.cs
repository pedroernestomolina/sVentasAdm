using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Sucursal.Entidad 
{
    
    public class Ficha
    {


        public string auto { get; set; }
        public string autoDepositoPrincipal { get; set; }
        public string autoEmpresaGrupo { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string codigoDepositoPrincipal { get; set; }
        public string nombreDepositoPrincipal { get; set; }
        public string nombreEmpresaGrupo { get; set; }


        public Ficha()
        {
            Limpiar();
        }


        public Ficha(Ficha it)
            : this()
        {
            auto = it.auto;
            autoDepositoPrincipal = it.autoDepositoPrincipal;
            autoEmpresaGrupo = it.autoEmpresaGrupo;
            codigo = it.codigo;
            nombre = it.nombre;
            codigoDepositoPrincipal = it.codigoDepositoPrincipal;
            nombreDepositoPrincipal = it.nombreDepositoPrincipal;
            nombreEmpresaGrupo = it.nombreEmpresaGrupo;
        }

        private void Limpiar()
        {
            auto = "";
            autoDepositoPrincipal = "";
            autoEmpresaGrupo = "";
            codigo = "";
            nombre = "";
            codigoDepositoPrincipal = "";
            nombreDepositoPrincipal = "";
            nombreEmpresaGrupo = "";
        }

    }

}