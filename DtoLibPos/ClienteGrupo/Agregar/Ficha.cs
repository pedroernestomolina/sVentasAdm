﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.ClienteGrupo.Agregar
{
    
    public class Ficha
    {

        public string codigoSucursalRegistro { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }


        public Ficha()
        {
            codigoSucursalRegistro = "";
            codigo = "";
            nombre = "";
        }

    }

}