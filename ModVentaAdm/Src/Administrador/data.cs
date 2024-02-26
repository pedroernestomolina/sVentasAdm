using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Administrador
{
    
    public class data
    {

        public enum enumTipoDoc { SinDefinir = -1, Factura = 1, NotaDebito, NotaCredito, NotaEntrega, Presupuesto };


        private OOB.Documento.Lista.Ficha doc;


        public string idDocumento { get { return doc.Id; } }
        public string FechaHora { get { return doc.FechaEmision.ToShortDateString(); } }//+ ", " + doc.HoraEmision; } }
        public string Serie { get { return doc.Serie; } }
        public string Documento { get { return doc.DocNumero; } }
        public string Renglones { get { return doc.Renglones.ToString().Trim(); } }
        public string ClienteCiRif { get { return doc.CiRif; } }
        public string ClienteNombre { get { return doc.NombreRazonSocial; } }
        public decimal Importe { get { return doc.Monto; } }
        public decimal ImporteDivisa { get { return doc.MontoDivisa; } }
        public bool IsAnulado { get { return !doc.IsActivo; } }
        public string DocCodigo { get { return doc.DocCodigo; } }
        public string Estatus { get { return IsAnulado ? "ANULADO" : ""; } }
        public string Aplica { get { return doc.DocAplica; } }
        public string SucursalCod { get { return doc.SucursalCod; } }
        public string SucursalDesc { get { return doc.SucursalDesc; } }
        public int Signo { get { return doc.DocSigno; } }
        public string Situacion { get { return doc.DocSituacion; } }
        public string DocNombre
        {
            get
            {
                var _docNombre = "";
                switch (DocCodigo.Trim().ToUpper())
                {
                    case "01":
                        _docNombre = "FACTURA";
                        break;
                    case "02":
                        _docNombre = "NOTA DEBITO";
                        break;
                    case "03":
                        _docNombre = "NOTA CREDITO";
                        break;
                    case "04":
                        _docNombre = "NOTA ENTREGA";
                        break;
                    case "05":
                        _docNombre = "PRESUPUESTO";
                        break;
                }
                //return _docNombre;
                return doc.DocNombre;
            }
        }
        public enumTipoDoc DocTipo
        {
            get
            {
                var tp = enumTipoDoc.SinDefinir;
                switch (DocCodigo.Trim().ToUpper())
                {
                    case "01":
                        tp = enumTipoDoc.Factura;
                        break;
                    case "02":
                        tp = enumTipoDoc.NotaDebito;
                        break;
                    case "03":
                        tp = enumTipoDoc.NotaCredito;
                        break;
                    case "04":
                        tp = enumTipoDoc.NotaEntrega;
                        break;
                    case "05":
                        tp = enumTipoDoc.Presupuesto;
                        break;
                }
                return tp;
            }
        }
        public bool IsDocVentaAdministrativo { get { return doc.ClaveSistema.Trim().ToUpper() == "03"; } }


        public data()
        {
        }

        public data(OOB.Documento.Lista.Ficha doc)
        {
            this.doc = doc;
        }

        public void SetAnulado()
        {
            this.doc.Estatus = "1";
        }

    }

}