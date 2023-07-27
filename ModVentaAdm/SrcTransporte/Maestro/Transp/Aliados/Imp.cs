using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Maestro.Transp.Aliados
{
    public class Imp: IAliado
    {
        private IAliadoLista _lista;


        public Utils.Maestro.ILista Lista { get { return _lista; } }
        public string TituloMaestro_Get { get { return "Maestro: ALIADOS"; } }
        public BindingSource DataSource_Get { get { return _lista.DataSource_Get; } }
        public int CntItems_Get { get { return _lista.CntItems_Get; } }


        public Imp()
        {
            _lista = new ImpLista();
        }


        public void Inicializa()
        {
            _lista.Inicializa();
        }
        Frm frm;
        public void Inicia()
        {
            if (cargarData())
            {
                if (frm == null)
                {
                    frm = new Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }

        public void AgregarItem()
        {
            SrcTransporte.Aliados.AgregarEditar.Agregar.IAgregar _gest;
            _gest = new SrcTransporte.Aliados.AgregarEditar.Agregar.Agregar();
            _gest.Inicializa();
            _gest.Inicia();
            if (_gest.ProcesarIsOK) 
            {
                InsertarItemLista(_gest.IdAliadoAgregado);
            }
        }
        public void EditarItem()
        {
            if (_lista.ItemActual != null)
            {
                var _item = ((data)_lista.ItemActual);
                SrcTransporte.Aliados.AgregarEditar.Editar.IEditar _gest;
                _gest = new SrcTransporte.Aliados.AgregarEditar.Editar.Editar();
                _gest.Inicializa();
                _gest.setAliadoEditar(_item.Ficha.id);
                _gest.Inicia();
                if (_gest.ProcesarIsOK) 
                {
                    ActualizarItemLista(_item.Ficha.id);
                }
            }
        }


        private bool cargarData()
        {
            try
            {
                var filtroOOB = new OOB.Transporte.Aliado.Busqueda.Filtro(); 
                var r01 = Sistema.MyData.TransporteAliado_GetLista(filtroOOB);
                _lista.setDataCargar(r01.ListaD);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        private void InsertarItemLista(int idItem)
        {
            try
            {
                var xr1 = Sistema.MyData.TransporteAliado_GetById(idItem);
                _lista.AgregarItem(xr1.Entidad);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        private void ActualizarItemLista(int idItem)
        {
            try
            {
                var xr1 = Sistema.MyData.TransporteAliado_GetById(idItem);
                _lista.RemoverItemBy(idItem);
                _lista.AgregarItem(xr1.Entidad);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
    }
}