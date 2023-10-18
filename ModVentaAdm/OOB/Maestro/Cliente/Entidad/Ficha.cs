using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Maestro.Cliente.Entidad
{
    public class Ficha
    {
        public string id { get; set; }
        public string idGrupo { get; set; }
        public string idEstado { get; set; }
        public string idZona { get; set; }
        public string idVendedor { get; set; }
        public string idCobrador { get; set; }
        public string tarifa { get; set; }
        public string categoria { get; set; }
        public string nivel { get; set; }
        public string ciRif { get; set; }
        public string codigo { get; set; }
        public string razonSocial { get; set; }
        public string dirFiscal { get; set; }
        public string dirDespacho { get; set; }
        public string pais { get; set; }
        public string contacto { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string email { get; set; }
        public string celular { get; set; }
        public string fax { get; set; }
        public string webSite { get; set; }
        public string codPostal { get; set; }
        public string estatusCredito { get; set; }
        public decimal dscto { get; set; }
        public decimal cargo { get; set; }
        public int limiteDoc { get; set; }
        public int diasCredito { get; set; }
        public decimal limiteCredito { get; set; }
        public string estatus { get; set; }
        public string grupo { get; set; }
        public string estado { get; set; }
        public string zona { get; set; }
        public string vendedor { get; set; }
        public string cobrador { get; set; }
        public string denFiscal { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime fechaBaja { get; set; }
        public bool IsActivo { get { return estatus.Trim().ToUpper() == "ACTIVO" ? true : false; } }
        public bool IsCreditoActivo { get { return estatusCredito.Trim().ToUpper() == "1"; } }


        public Ficha()
        {
            Limpiar();
        }

        public Ficha(Venta.Temporal.Encabezado.Entidad.Ficha ficha)
            :this()
        {
            id = ficha.autoCliente;
            ciRif = ficha.ciRifCliente;
            razonSocial = ficha.razonSocialCliente;
            dirFiscal = ficha.dirFiscalCliente;
            codigo = ficha.codigoCliente;
        }

        public void Limpiar()
        {
            id = "";
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
            estatus = "";
            grupo = "";
            estado = "";
            zona = "";
            vendedor = "";
            cobrador = "";
            denFiscal = "";
            fechaAlta = DateTime.Now.Date;
            fechaBaja = DateTime.Now.Date;
        }

        //
        public string Info 
        { 
            get 
            {
                return "("+codigo.Trim().ToUpper() +")"+
                    Environment.NewLine + ciRif.Trim().ToUpper() +
                    Environment.NewLine + razonSocial.Trim();
            }
        }
    }
}