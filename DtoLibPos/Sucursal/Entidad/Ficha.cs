﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Sucursal.Entidad
{

    public class Ficha
    {

        public string id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string nombreGrupo { get; set; }
        public string precioManejar { get; set; }
        public string estatusVentaMayor { get; set; }


        public Ficha()
        {
            id = "";
            codigo = "";
            nombre = "";
            nombreGrupo = "";
            precioManejar = "";
            estatusVentaMayor = "";
        }

    }

}