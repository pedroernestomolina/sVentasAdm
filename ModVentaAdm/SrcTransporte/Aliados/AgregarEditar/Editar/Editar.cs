using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Aliados.AgregarEditar.Editar
{
    public class Editar: ImpAgregarEditar, IEditar
    {
        private int _idAliado;


        public void setAliadoEditar(int id)
        {
            _idAliado = id;
        }

        public override void Procesar()
        {
            _procesarIsOK = false;
            if (Ficha.DatosAgregarIsOk())
            {
                var r = Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    var fichaOOB = new OOB.Transporte.Aliado.Editar.Ficha()
                    {
                        idAliado = _idAliado,
                        ciRif = Ficha.CiRif_GetData,
                        codigo = Ficha.Codigo_GetData,
                        dirFiscal = Ficha.DirFiscal_GetData,
                        nombreRazonSocial = Ficha.NombreRazonSocial_GetData,
                        personaContacto = Ficha.PersonaContacto_GetData,
                        telefonos = Ficha.MisTelefonos.MisNumeros.Select(s =>
                        {
                            var nr = new OOB.Transporte.Aliado.Editar.Telefono()
                            {
                                numero = s.Numero_GetData,
                            };
                            return nr;
                        }).ToList(),
                    };
                    try
                    {
                        var r01 = Sistema.MyData.TransporteAliado_Editar(fichaOOB);
                        _procesarIsOK = true;
                        Helpers.Msg.EditarOk();
                    }
                    catch (Exception e)
                    {
                        Helpers.Msg.Error(e.Message);
                    }
                }
            }
        }

        protected override bool CargarData()
        {
            try
            {
                var r01= Sistema.MyData.TransporteAliado_GetById(_idAliado);
                Ficha.setData(r01.Entidad);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
    }
}