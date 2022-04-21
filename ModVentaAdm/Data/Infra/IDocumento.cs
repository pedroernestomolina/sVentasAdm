﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Data.Infra
{
    
    public interface IDocumento
    {

        OOB.Resultado.Lista<OOB.Documento.Lista.Ficha> Documento_Get_Lista(OOB.Documento.Lista.Filtro filtro);
        OOB.Resultado.FichaEntidad<OOB.Documento.Entidad.Ficha> Documento_GetById(string idAuto);
        OOB.Resultado.FichaAuto Documento_Agregar_Presupuesto(OOB.Documento.Agregar.Presupuesto.Ficha ficha);
        OOB.Resultado.Ficha Documento_Anular_Presupuesto(OOB.Documento.Anular.Presupuesto.Ficha ficha);
        OOB.Resultado.FichaAuto Documento_Agregar_Pedido(OOB.Documento.Agregar.Pedido.Ficha ficha);

    }

}