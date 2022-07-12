using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.AgregarNotaAdm.Debito
{
    
    public class AgregarNotaDebitoAdm: IAgregarTipoNotaAdm
    {


        private string _numDoc;
        private DateTime _fechaEmision;


        public string GetTipoNotaAdm { get { return "Agregar: Nota de Débito Administrativa"; } }
        public string GetNumeroDocConsecutivo { get { return _numDoc; } }
        public DateTime GetFechaEmision { get { return _fechaEmision; } }


        public AgregarNotaDebitoAdm() 
        {
            _numDoc = "";
            _fechaEmision = DateTime.Now.Date;
        }


        public void Inicializa()
        {
            _numDoc = "";
            _fechaEmision = DateTime.Now.Date;
        }
        public void Inicia()
        {
        }
        public bool CargarData()
        {
            var rt = true;

            var r01 = Sistema.MyData.FechaServidor();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _fechaEmision = r01.Entidad.Date;
            var r02 = Sistema.MyData.CxC_Get_ContadorNotaDebitoAdm();
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r02.Mensaje);
                return false;
            }
            _numDoc = (r02.Entidad + 1).ToString().Trim().PadLeft(10, '0');
            return rt;
        }

        public bool Procesar(dataAgregar data)
        {
            var r00 = Sistema.MyData.Sistema_TipoDocumento_GetFichaById(Sistema.Id_SistemaDocumento_NOTA_DEBITO_ADMINISTRATIVA_POC_COBRAR);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return false;
            }
            var _tipoDoc = r00.Entidad;

            var rt = MessageBox.Show("Procesar/Guardar Ficha ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rt == DialogResult.Yes)
            {
                var fichaOOb = new OOB.CxC.AgregarNotaAdm.Ficha()
                {
                    autoCliente = data.ClienteGet.id,
                    autoVendedor = data.VendedorGet.id,
                    ciRifCliente = data.ClienteGet.ciRif,
                    codigoCliente = data.ClienteGet.codigo,
                    codSucursal = Sistema.Sucursal.codigo,
                    montoDivisaDoc = data.MontDivisaDocGet,
                    montoDoc = data.MontoDoc,
                    nombreCliente = data.ClienteGet.razonSocial,
                    notasDoc = data.NotasDocGet,
                    signoDoc = _tipoDoc.signo,
                    tasaCambioDoc = data.TasaFactorDocGet,
                    tipoDoc = _tipoDoc.siglas,
                };
                var r01 = Sistema.MyData.CxC_AgregarNotaDebitoAdm(fichaOOb);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return false;
                }
                return true;
            }
            return false;
        }

    }

}