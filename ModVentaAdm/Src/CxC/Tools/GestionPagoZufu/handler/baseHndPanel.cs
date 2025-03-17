using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler
{
    public class baseHndPanel: PanelPrincipal.Pago.IPanelCtas
    {
        private object _idCliente;
        private DateTime _fechaServidor;
        private PanelPrincipal.Pago.ICtasPend _ctasPend;
        //
        public IEnumerable<PanelPrincipal.Pago.IItemCtaPend> GetListaDocPagar 
        { 
            get 
            { 
                var rt =_ctasPend.GetListaDocPagar;
                return rt;
            } 
        }
        public decimal GetTotalMontoCtasPendientes { get { return _ctasPend.GetTotalMontoPend; } }
        public decimal GetMontoPagar { get { return _ctasPend.GetMontoPagar; } }
        public int GetCntCtasPagar { get { return _ctasPend.GetCntCtasPagar; } }
        //
        public baseHndPanel(PanelPrincipal.Pago.ICtasPend hndCtasPend)
        {
            _idCliente = "";
            _fechaServidor = DateTime.Now.Date;
            _ctasPend = hndCtasPend;
        }
        public void Inicializa()
        {
            _ctasPend.Inicializa();
        }
        public void setIdEntidad(object id)
        {
            _idCliente = id;
        }
        public void setFechaServidor(DateTime fecha)
        {
            _fechaServidor = fecha;
        }
        public void ListarCtasPagar()
        {
            _ctasPend.setIdEntidad(_idCliente);
            _ctasPend.setFechaServidor(_fechaServidor);
            _ctasPend.Inicia();
        }
        public void setMontoAbonadoASaldar(decimal monto)
        {
            _ctasPend.setMontoAbonadoASaldar(monto);
        }
        //
        private string intToStr(int p)
        {
            return p.ToString();
        }
        //
        public void setClientePagar(string dat)
        {
            _ctasPend.setClientePagar(dat);
        }
        //
        public void LimpiarData()
        {
            _idCliente = "";
        }
    }
}