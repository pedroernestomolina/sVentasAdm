using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.ToolsCxC.Administrador.Handler
{
    public class dataItem: Vistas.IdataItem
    {
        private string _idMov;
        private bool _isAnulado;
        private OOB.Transporte.CxcMovCobro.ListaMov.Ficha _ficha;


        public decimal ImporteMov { get; set; }
        public decimal AnticipoMov { get; set; }
        public decimal RetencionMov { get; set; }
        public string NroRecibo { get; set; }
        public string CiRif { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaMov { get; set; }
        public decimal MontoRec { get; set; }
        public string Estatus { get; set; }
        public string idMov { get { return _idMov; } }
        public bool isAnulado { get { return _isAnulado; } }


        public dataItem(OOB.Transporte.CxcMovCobro.ListaMov.Ficha ficha)
        {
            _ficha = ficha;
            CiRif = ficha.ciRifCliente;
            Nombre = ficha.nombreCliente;
            FechaMov = ficha.fechaEmision;
            ImporteMov = ficha.importeDiv;
            AnticipoMov = ficha.montoAnticipoDiv;
            RetencionMov = ficha.montoRetDiv;
            MontoRec = ficha.montoRecibidoDiv;
            Estatus = ficha.estatusAnulado == "1" ? "ANULADO" : "";
            NroRecibo = ficha.numRecibo;
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