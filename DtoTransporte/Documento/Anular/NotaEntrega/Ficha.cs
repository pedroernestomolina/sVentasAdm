﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoTransporte.Documento.Anular.NotaEntrega
{
    public class Ficha
    {
        public string idDocVenta { get; set; }
        public string idDocCxC { get; set; }
        public string idCliente { get; set; }
        public decimal montoDivisa { get; set; }
        public FichaAuditoria auditoria { get; set; }
        public List<FichaAliado> aliadosInv { get; set; }
        public List<FichaAliadoDoc> aliadosDoc { get; set; }
        public List<FichaItemServ> itemsServicio { get; set; }
        public List<FichaDocRef> docRef { get; set; }
        public Ficha()
        {
            idDocVenta = "";
            idDocCxC = "";
            idCliente = "";
            montoDivisa = 0m;
            auditoria = new FichaAuditoria();
            aliadosInv = new List<FichaAliado>();
            aliadosDoc = new List<FichaAliadoDoc>();
            itemsServicio = new List<FichaItemServ>();
            docRef = new List<FichaDocRef>();
        }
    }
}