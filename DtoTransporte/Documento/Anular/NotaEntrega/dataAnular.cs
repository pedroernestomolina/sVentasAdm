using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Anular.NotaEntrega
{
    public class dataAnular
    {
        public string idDocCxC { get; set; }
        public string idCliente { get; set; }
        public decimal montoDivisa { get; set; }
        public List<FichaAliado> aliadosInv { get; set; }
        public List<FichaAliadoDoc> aliadosDoc { get; set; }
        public List<FichaItemServ> itemsServ { get; set; }
        public  List<FichaDocRef> docRef{ get; set; }
        public dataAnular()
        {
            idDocCxC = "";
            idCliente = "";
            montoDivisa = 0m;
            aliadosInv = new List<FichaAliado>();
            aliadosDoc = new List<FichaAliadoDoc>();
            itemsServ = new List<FichaItemServ>();
            docRef = new List<FichaDocRef>();
        }
    }
}