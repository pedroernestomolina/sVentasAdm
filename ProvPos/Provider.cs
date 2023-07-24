﻿using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider: IPos.IProvider
    {
        public static EntityConnectionStringBuilder _cnPos;
        private string _Instancia;
        private string _BaseDatos;
        private string _Usuario;
        private string _Password;


        public Provider(string instancia, string bd)
        {
            _Usuario = "root";
            _Password = "123";
            _Instancia = instancia;
            _BaseDatos = bd;
            setConexion();
        }


        private void setConexion()
        {
            _cnPos = new EntityConnectionStringBuilder();
            _cnPos.Metadata = "res://*/PosModel.csdl|res://*/PosModel.ssdl|res://*/PosModel.msl";
            _cnPos.Provider = "MySql.Data.MySqlClient";
            _cnPos.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";Convert Zero Datetime=True;";
        }

        public DtoLib.ResultadoEntidad<DateTime> 
            FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();

            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    result.Entidad = fechaSistema.Date;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado 
            Test()
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                };
            }
            catch (Exception ex)
            {
                result.Mensaje = ex.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
    }
}