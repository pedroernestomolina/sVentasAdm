﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLib
{

    public class ResultadoEntidad<T> : Resultado
    {
        public T Entidad { get; set; }

        public ResultadoEntidad()
            : base()
        {
        }
    }

}