using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Cliente.Editar.ObtenerData
{

    public class Ficha
    {

        public string idGrupo;
        public string idEstado;
        public string idZona;
        public string idVendedor;
        public string idCobrador;
        public string tarifa;
        public string categoria;
        public string nivel;
        public string ciRif;
        public string codigo;
        public string razonSocial;
        public string dirFiscal;
        public string dirDespacho;
        public string pais;
        public string contacto;
        public string telefono1;
        public string telefono2;
        public string email;
        public string celular;
        public string fax;
        public string webSite;
        public string codPostal;
        public string estatusCredito;
        public decimal dscto;
        public decimal cargo;
        public int limiteDoc;
        public int diasCredito;
        public decimal limiteCredito;


        public Ficha()
        {
            idGrupo = "";
            idEstado = "";
            idZona = "";
            idVendedor = "";
            idCobrador = "";
            tarifa = "";
            categoria = "";
            nivel = "";
            ciRif = "";
            codigo = "";
            razonSocial = "";
            dirFiscal = "";
            dirDespacho = "";
            pais = "";
            contacto = "";
            telefono1 = "";
            telefono2 = "";
            email = "";
            celular = "";
            fax = "";
            webSite = "";
            codPostal = "";
            estatusCredito = "";
            dscto = 0.0m;
            cargo = 0.0m;
            limiteDoc = 0;
            diasCredito = 0;
            limiteCredito = 0.0m;
        }

    }

}