using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Remision
{
    public class Imp: IRemision
    {
        private LibUtilitis.CtrlCB.ICtrl _ctrl;
        private string _docNombre;
        private string _docNumero;
        private string _docFecha;


        public BindingSource SourceItems_Get { get { return _ctrl.GetSource; } }
        public string ItemId_Get { get { return _ctrl.GetId; } }
        public string DocNombre_Get { get { return _docNombre; } }
        public string DocNumero_Get { get { return _docNumero; } }
        public string DocFecha_Get { get { return _docFecha; } }


        public Imp()
        {
            _docNombre = "";
            _docFecha = "";
            _docNumero = "";
            _ctrl = new LibUtilitis.CtrlCB.ImpCB();
        }


        public void setFichaId(string id)        
        {
            _ctrl.setFichaById(id);
        }


        public void Inicializa()
        {
            _docNombre = "";
            _docFecha = "";
            _docNumero = "";
            _ctrl.Inicializa();
        }
        public void CargarData()
        {
            var _lst = new List<Utils.dataFiltro>();
            _lst.Add(new Utils.dataFiltro() { codigo = "", desc = "PRESUPUESTO", id = "1" });
            _ctrl.CargarData(_lst);
        }
        public void Limpiar()
        {
            _docNombre = "";
            _docFecha = "";
            _docNumero = "";
            _ctrl.LimpiarOpcion();
        }
        public void Buscar()
        {
            var rt = BuscarDocumentos();
            if (rt != null && rt.Count > 0) 
            {
                MostrarSeleccionarDocumentos(rt);
            }
        }


        private List<OOB.Transporte.Documento.Remision.Lista.Ficha> BuscarDocumentos()
        {
            try
            {
                var filtroOOB = new OOB.Transporte.Documento.Remision.Lista.Filtro()
                {
                    codTipoDoc = "05",
                    idCliente = "",
                };
                var r01 =Sistema.MyData.TransporteDocumento_Remision_ListaBy(filtroOOB);
                return r01.ListaD;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return null;
            }
        }
        private Utils.DocLista.IDocLista _listDoc;
        private void MostrarSeleccionarDocumentos(List<OOB.Transporte.Documento.Remision.Lista.Ficha> list)
        {
            _listDoc = new Utils.DocLista.Remision.Imp();
            _listDoc.Inicializa();
            _listDoc.setDataCargar(list);
            _listDoc.Inicia();
        }
    }
}