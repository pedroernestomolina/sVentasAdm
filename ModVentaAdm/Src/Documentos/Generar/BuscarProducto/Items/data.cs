using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar.BuscarProducto.Items
{
    
    public class data
    {

        private string _id;
        private string _codigo;
        private string _descripcion;
        private bool _isActivo;
        private decimal _exActual;
        private decimal _exDisp;
        private decimal _tasaIva;
        private string _empqCont_1;
        private decimal _pneto_1;
        private string _empqCont_2;
        private decimal _pneto_2;
        private string _empqCont_3;
        private decimal _pneto_3;
        private string _empqCont_4;
        private decimal _pneto_4;
        private string _empqCont_5;
        private decimal _pneto_5;
        private string _empqCont_6;
        private decimal _pneto_6;


        public string Id { get { return _id; } }
        public string Codigo { get { return _codigo; } }
        public string Descripcion { get { return _descripcion; } }
        public bool IsActivo { get { return _isActivo; } }
        public string Estatus { get { return IsActivo ? "" : "INACTIVO"; } }
        public decimal ExActual { get { return _exActual; } }
        public decimal ExDisponible { get { return _exDisp; } }
        public decimal TasaIva { get { return _tasaIva; } }
        //
        public string EmpqCont_1 { get { return _empqCont_1; } }
        public decimal PNeto_1 { get { return _pneto_1; } }
        public decimal PFull_1 { get { return calculaFull(_pneto_1); } }
        //
        public string EmpqCont_2 { get { return _empqCont_2; } }
        public decimal PNeto_2 { get { return _pneto_2; } }
        public decimal PFull_2 { get { return calculaFull(_pneto_2); } }
        //
        public string EmpqCont_3 { get { return _empqCont_3; } }
        public decimal PNeto_3 { get { return _pneto_3; } }
        public decimal PFull_3 { get { return calculaFull(_pneto_3); } }
        //
        public string EmpqCont_4 { get { return _empqCont_4; } }
        public decimal PNeto_4 { get { return _pneto_4; } }
        public decimal PFull_4 { get { return calculaFull(_pneto_4); } }
        //
        public string EmpqCont_5 { get { return _empqCont_5; } }
        public decimal PNeto_5 { get { return _pneto_5; } }
        public decimal PFull_5 { get { return calculaFull(_pneto_5); } }
        //
        public string EmpqCont_6 { get { return _empqCont_6; } }
        public decimal PNeto_6 { get { return _pneto_6; } }
        public decimal PFull_6 { get { return calculaFull(_pneto_6); } }

                
        public data() 
        {
            Limpiar();
        }

        public data(OOB.Producto.Lista.Ficha it)
            :this()
        {
            _id = it.Id;
            _codigo = it.Codigo;
            _descripcion = it.Nombre;
            _isActivo = it.IsActivo;
            _exActual = it.ExFisica;
            _exDisp = it.ExDisponible;
            _tasaIva = it.TasaIva;
            _empqCont_1 = it.Empq_1.Trim() + "/" + it.Cont_1.ToString().Trim();
            _pneto_1 = it.PNeto1;
            _empqCont_2 = it.Empq_2.Trim() + "/" + it.Cont_2.ToString().Trim();
            _pneto_2 = it.PNeto2;
            _empqCont_3 = it.Empq_3.Trim() + "/" + it.Cont_3.ToString().Trim();
            _pneto_3 = it.PNeto3;
            _empqCont_4 = it.Empq_4.Trim() + "/" + it.Cont_4.ToString().Trim();
            _pneto_4 = it.PNeto4;
            _empqCont_5 = it.Empq_5.Trim() + "/" + it.Cont_5.ToString().Trim();
            _pneto_5 = it.PNeto5;
            _empqCont_6 = it.EmpqMayor1 .Trim() + "/" + it.ContMayor1 .ToString().Trim();
            _pneto_6 = it.PNetoMayor1;
        }
       
        public void Limpiar()
        {
            _id = "";
            _codigo = "";
            _descripcion = "";
            _isActivo = true;
            _exActual = 0m;
            _exDisp = 0m;
            _tasaIva = 0m;
            _empqCont_1 = "";
            _pneto_1 = 0m;
            _empqCont_2 = "";
            _pneto_2 = 0m;
            _empqCont_3 = "";
            _pneto_3 = 0m;
            _empqCont_4 = "";
            _pneto_4 = 0m;
            _empqCont_5 = "";
            _pneto_5 = 0m;
            _empqCont_6 = "";
            _pneto_6 = 0m;
        }

        private decimal calculaFull(decimal pn) 
        {
            var rt = pn;
            var iva= pn * (_tasaIva/100);
            rt += iva;
            return rt;
        }

    }

}