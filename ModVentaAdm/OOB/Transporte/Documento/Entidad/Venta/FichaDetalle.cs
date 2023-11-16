using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Transporte.Documento.Entidad.Venta
{
    public class FichaDetalle
    {
        public string detalle { get; set; }
        public int cntDias { get; set; }
        public decimal precioNetoMonLocal { get; set; }
        public decimal precioNetoMonDivisa { get; set; }
        public decimal dsctoMontoMonLocal { get; set; }
        public decimal dsctoMontoMonDivisa { get; set; }
        public decimal dsctoPorc { get; set; }
        public string alicuotaId { get; set; }
        public decimal alicuotaTasa { get; set; }
        public decimal impuestoMonLocal { get; set; }
        public decimal impuestoMonDivisa { get; set; }
        public string alicuotaDesc { get; set; }
        public decimal precioItemMonLocal { get; set; }
        public decimal precioItemMonDivisa { get; set; }
        public decimal precioFinalMonLocal { get; set; }
        public decimal precioFinalMonDivisa { get; set; }
        public decimal importeNetoMonLocal { get; set; }
        public decimal importeNetoMonDivisa { get; set; }
        public decimal importeTotalMonLocal { get; set; }
        public decimal importeTotalMonDivisa { get; set; }
        public decimal totalMonLocal { get; set; }
        public decimal totalMonDivisa { get; set; }
        public string idDocRef { get; set; }
        public string numDocRef { get; set; }
        public DateTime fechaDocRef { get; set; }
        public decimal montoDocRef { get; set; }
        public string codigoDocRef { get; set; }
        public string tipoProcedenciaItem { get; set; }
        public int idItemServicio { get; set; }
        public string mostrarItemDocFinal { get; set; }
        public FichaDetalle()
        {
            detalle = "";
            cntDias = 0;
            precioNetoMonLocal = 0m;
            precioNetoMonDivisa = 0m;
            dsctoMontoMonLocal = 0m;
            dsctoMontoMonDivisa = 0m;
            dsctoPorc = 0m;
            alicuotaId = "";
            alicuotaTasa = 0m;
            impuestoMonLocal = 0m;
            impuestoMonDivisa = 0m;
            alicuotaDesc = "";
            precioItemMonLocal = 0m;
            precioItemMonDivisa = 0m;
            precioFinalMonLocal = 0m;
            precioFinalMonDivisa = 0m;
            importeNetoMonLocal = 0m;
            importeNetoMonDivisa = 0m;
            importeTotalMonLocal = 0m;
            importeTotalMonDivisa = 0m;
            totalMonLocal = 0m;
            totalMonDivisa = 0m;
            idDocRef = "";
            numDocRef = "";
            fechaDocRef = DateTime.Now.Date;
            montoDocRef = 0m;
            codigoDocRef = "";
            tipoProcedenciaItem = "";
            idItemServicio = -1;
            mostrarItemDocFinal = "";
        }
    }
}