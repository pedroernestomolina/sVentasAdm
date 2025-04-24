using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.CxC.Tools.GestionPagoZufu.handler.NtCred
{
    public class HndCtasPend: baseHndCtas, PanelPrincipal.Pago.ICtasPend
    {
        //
        public HndCtasPend()
            : base(new listaCtasPend())
        {
        }
        public override bool CargarData()
        {
            try
            {
                if (!_cargarData) return true;
                //
                var filtroOOb = new OOB.CxC.DocumentosPend.Filtro()
                {
                    idCliente = (string)GetIdEntidad,
                };
                var r01 = Sistema.MyData.CxC_DocumentosPend_GetLista(filtroOOb);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    throw new Exception(r01.Mensaje);
                }
                var lst = r01.ListaD.Select(s =>
                {
                    var nr = new itemCtaPend(s);
                    return nr;
                }).ToList().Where(w=>w.SignoDoc==-1).OrderBy(o=> o.fechaEmisionDoc).ToList();
                ListaCtas.setData(lst);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        public override void VisualizarDocumento()
        {
            if (ItemActual != null) 
            {
            }
        }
    }
}