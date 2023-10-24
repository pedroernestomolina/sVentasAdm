using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ClienteAnticipo.Administrador.Handler
{
    public class dataItem: Vistas.IdataItem
    {
        private int _idMov;
        private bool _isAnulado;
        private OOB.Transporte.ClienteAnticipo.ListaMov.Ficha _ficha;


        public string CiRif { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaMov { get; set; }
        public bool AplicaRet { get; set; }
        public decimal MontoMov { get; set; }
        public decimal MontoRec { get; set; }
        public string Estatus { get; set; }
        public int idMov { get { return _idMov; } }
        public bool isAnulado { get { return _isAnulado; } }


        public dataItem(OOB.Transporte.ClienteAnticipo.ListaMov.Ficha ficha)
        {
            _ficha = ficha;
            CiRif = ficha.ciRifCliente;
            Nombre = ficha.nombreCliente;
            AplicaRet = ficha.aplicaRet.Trim().ToUpper() == "1";
            FechaMov = ficha.fechaReg;
            MontoMov = ficha.montoMonDiv;
            MontoRec = ficha.montoRecMonDiv;
            Estatus = ficha.estatusAnulado == "1" ? "ANULADO" : "";
            _idMov = ficha.idMov;
            _isAnulado = ficha.estatusAnulado == "1";
        }
        public void setEstatusAnulado()
        {
            _ficha.estatusAnulado = "1";
            Estatus = _ficha.estatusAnulado == "1" ? "ANULADO" : "";
            _isAnulado = _ficha.estatusAnulado == "1";
        }
    }
}