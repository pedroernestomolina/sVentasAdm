﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Identificacion
{
    public class Login: ILogin
    {
        private bool _isOk;
        private string _codigoUsu;
        private string _claveUsu;


        public bool IsOk { get { return _isOk; } }
        public string GetNombreHerramienta { get { return Sistema.NombreHerramienta; } }

        
        public Login()
        {
            Inicializa();
        }


        IdentificacionFrm frm;
        public void Inicia()
        {
            frm = new IdentificacionFrm();
            frm.setControlador(this);
            frm.ShowDialog();
        }

        public void Inicializa()
        {
            _isOk = false;
            _codigoUsu = "";
            _claveUsu = "";
        }

        public void SetCodigo(string p)
        {
            _codigoUsu = p.Trim().ToUpper();
        }

        public void SetClave(string p)
        {
            _claveUsu = p.Trim().ToUpper();
        }

        public  void Aceptar()
        {
            _isOk = VerificarUsuario();
        }

        public bool VerificarUsuario()
        {
            var rt = true;

            if (_codigoUsu == "ADMINISTRADOR" && _claveUsu == "ADMIN")
            {
                Sistema.Usuario = new OOB.Usuario.Entidad.Ficha();
                Sistema.Usuario.setInvitado();
                return true;
            }
            var ficha = new OOB.Usuario.Identificar.Ficha()
            {
                codigo = _codigoUsu,
                clave = _claveUsu,
            };
            try
            {
                var r01 = Sistema.MyData.Usuario_Identificar(ficha);
                Sistema.Usuario = r01.Entidad;
                return rt;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
    }
}