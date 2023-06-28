using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item
{
    abstract public class ImpItem : IItem
    {
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;
        private data _data;
        private Aliado.IAliado _aliado;


        public data Item { get { return _data; } }
        public Aliado.IAliado MiAliado { get { return _aliado; } }
        //
        public string ServItemMostrar { get { return _data.Get_Descripcion; } }
        public string AliadoItemMostrar { get { return _data.Get_Aliado_ItemMostrar ; } }
        public decimal PrecioItemMostrar { get { return _data.Get_PrecioDivisa; } }
        public int CantDiasItemMostrar { get { return _data.Get_CntDias; } }
        public int CantVehicItemMostrar { get { return _data.Get_CntUnidades; } }
        public decimal ImporteItemMostrar { get { return _data.Get_Importe; } }


        public ImpItem()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _data = new data();
            _aliado = new Aliado.ImpAliado();
        }


        public void Inicializa()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _data.Inicializa();
            _aliado.Inicializa();
        }
        Frm frm;
        public void Inicia()
        {
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }


        public void Aliados()
        {
            _aliado.Inicializa();
            _aliado.Inicia();
            if (_aliado.ItemSeleccionadoIsOk) 
            {
                var id = _aliado.AliadoSeleccionado_GetId;
                try
                {
                    var r01 = Sistema.MyData.TransporteAliado_GetById(id);
                    Item.setAliado(r01.Entidad);
                }
                catch (Exception e)
                {
                    Helpers.Msg.Error(e.Message);
                }
            }
        }


        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        abstract public void Procesar();

        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }


        private bool CargarData()
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }

        private int _id=-1;
        public int id { get { return _id; } }
        public void setId(int id)
        {
            _id = id;
        }
    }
}