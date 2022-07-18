using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleTest
{

    class Program
    {

        static void Main(string[] args)
        {
            IPos.IProvider posProv = new ProvPos.Provider("localhost", "mscala");
            
            ////var r01 = posProv.CxC_Get_ContadorNotaCreditoAdm();
            //var fichaDto = new DtoLibPos.CxC.GestionCobro.Ficha();
            //fichaDto.SucPrefijo = "15";

            //var xtasa = 5.85m;
            //var xmontoDivisa = 100m;
            //fichaDto.Cobro = new DtoLibPos.CxC.GestionCobro.FichaCobro()
            //{
            //    AutoCliente = "1500001962",
            //    AutoVendedor = "0000000001",
            //    CiRif = "J000000000",
            //    Cliente = "CLIENTE CREDITO",
            //    CodigoCliente = "CRED",
            //    MontoDivisa = xmontoDivisa,
            //    Nota = "",
            //    TasaDivisa = xtasa,
            //    Importe = Math.Round(xmontoDivisa * xtasa, 2, MidpointRounding.AwayFromZero),
            //};

            //var xcambioDivisa = 0m;
            //var ximporteDivisa = 100m;
            //var xmontoRecibidoDivisa = 100m;
            //fichaDto.Recibo = new DtoLibPos.CxC.GestionCobro.FichaRecibo()
            //{
            //    AutoCliente = "1500001962",
            //    AutoCobrador = "0000000001",
            //    AutoUsuario = "0000000036",
            //    Cambio = Math.Round(xcambioDivisa * xtasa, 2, MidpointRounding.AwayFromZero),
            //    CambioDivisa = xcambioDivisa,
            //    CiRif = "J000000000",
            //    Cliente = "CLIENTE CREDITO",
            //    Cobrador = "DIRECTO",
            //    Codigo = "",
            //    CodigoCobrador = "01",
            //    Direccion = "VALENCIA",
            //    Importe = Math.Round(ximporteDivisa * xtasa, 2, MidpointRounding.AwayFromZero),
            //    ImporteDivisa = ximporteDivisa,
            //    MontoRecibido = Math.Round(xmontoRecibidoDivisa * xtasa, 2, MidpointRounding.AwayFromZero),
            //    MontoRecibidoDivisa = xmontoRecibidoDivisa,
            //    Nota = "",
            //    Telefono = "",
            //    Usuario = "MSCALA",
            //};

            //var xdoc_ImporteDivisa=100m;
            //var xdoc_Importe =Math.Round(xdoc_ImporteDivisa* xtasa,2, MidpointRounding.AwayFromZero);
            //var doc1 = new DtoLibPos.CxC.GestionCobro.FichaDocumento()
            //{
            //    AutoCxC = "1500009990",
            //    EstatusDocCancelado="0",
            //    DocumentoNro = "900",
            //    Id = 1,
            //    Importe = xdoc_Importe,
            //    ImporteDivisa = xdoc_ImporteDivisa,
            //    TipoDocumento = "FAC",
            //};
            //var documentos = new List<DtoLibPos.CxC.GestionCobro.FichaDocumento>();
            //documentos.Add(doc1);
            //fichaDto.Documentos = documentos;

            //var metodosCobro = new List<DtoLibPos.CxC.GestionCobro.FichaMetodoPago>();
            //var met1 = new DtoLibPos.CxC.GestionCobro.FichaMetodoPago()
            //{
            //    AutoCobrador = "0000000001",
            //    AutoMedioPago = "0000000002",
            //    AutoUsuario = "0000000036",
            //    Cierre = "",
            //    Codigo = "02",
            //    Lote = "50",
            //    Medio = "Divisa",
            //    MontoRecibido = 50m, // monto que se recibie ya sea en bs, divisa, etc..
            //    OpAplicaConversion = "0", //1:si, 0:no
            //    OpBanco = "",
            //    OpDetalle = "PAGO EN EFECTIVO",
            //    OpFecha = DateTime.Now.Date,
            //    OpMonto = 50m,  // en divisa
            //    OpNroCta = "",
            //    OpNroRef = "",
            //    OpTasa = 5.85m, //tasa de conversion
            //    Referencia = "5,85",
            //};
            //var met2 = new DtoLibPos.CxC.GestionCobro.FichaMetodoPago()
            //{
            //    AutoCobrador = "0000000001",
            //    AutoMedioPago = "0000000003",
            //    AutoUsuario = "0000000036",
            //    Cierre = "",
            //    Codigo = "03",
            //    Lote = "100",
            //    Medio = "Tarjeta Debito",
            //    MontoRecibido = 292.5m,
            //    OpAplicaConversion = "1",  //si aplica la conversion
            //    OpBanco = "",
            //    OpDetalle = "PAGO POR PTO",
            //    OpFecha = DateTime.Now.Date,
            //    OpMonto = 50m,  // 292,5 div 5.85
            //    OpNroCta = "",
            //    OpNroRef = "",
            //    OpTasa = 5.85m, // tasa de conversion
            //    Referencia = "0090",
            //};
            //metodosCobro.Add(met1);
            //metodosCobro.Add(met2);
            //fichaDto.MetodosPago = metodosCobro;

            //fichaDto.saldoCliente = new DtoLibPos.CxC.GestionCobro.FichaCliente()
            //{
            //    idCliente = "1500001962",
            //    monto = xmontoDivisa,
            //};
            //var r01 = posProv.CxC_GestionCobro_Agregar(fichaDto);


            //var r01 = posProv.Permiso_Cliente("0000000001");
            //var r01 = posProv.Permiso_CxC("0000000001");
        }

    }

}