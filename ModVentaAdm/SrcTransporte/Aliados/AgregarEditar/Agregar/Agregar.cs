﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Aliados.AgregarEditar.Agregar
{
    public class Agregar: ImpAgregarEditar, IAgregar
    {

        public Agregar()
            : base()
        {
        }

        public override void Procesar()
        {
            _procesarIsOK = false;
            if (Ficha.DatosAgregarIsOk())
            {
                var r = Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    var fichaOOB = new OOB.Transporte.Aliado.Agregar.Ficha()
                    {
                        ciRif = Ficha.CiRif_GetData,
                        codigo = Ficha.Codigo_GetData,
                        dirFiscal = Ficha.DirFiscal_GetData,
                        nombreRazonSocial = Ficha.NombreRazonSocial_GetData,
                        personaContacto = Ficha.PersonaContacto_GetData,
                        telefonos = Ficha.MisTelefonos.MisNumeros.Select(s=>
                        {
                             var nr = new OOB.Transporte.Aliado.Agregar.Telefono()
                             {
                                  numero= s.Numero_GetData,
                             };
                            return nr;
                        }).ToList(),
                    };
                    try
                    {
                        var r01 = Sistema.MyData.TransporteAliado_Agregar(fichaOOB);
                        _procesarIsOK = true;
                        Helpers.Msg.AgregarOk();
                    }
                    catch (Exception e)
                    {
                        Helpers.Msg.Error(e.Message);
                    }
                }
            }
        }
    }
}