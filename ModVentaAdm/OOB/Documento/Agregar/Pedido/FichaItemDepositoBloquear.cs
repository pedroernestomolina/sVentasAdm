using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.OOB.Documento.Agregar.Pedido
{
    
    public class FichaItemDepositoBloquear: BaseFichaItemDepositoBloquear
    {

        public FichaItemDepositoBloquear ()
        {
            autoDeposito = "";
            autoProducto = "";
            prdDescripcion = "";
            depDescripcion = "";
            cntUnd = 0m;
        }
    }

}