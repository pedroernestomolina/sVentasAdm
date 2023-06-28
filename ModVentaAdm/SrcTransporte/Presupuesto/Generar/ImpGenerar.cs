using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar
{
    public class ImpGenerar: IGenerar 
    {
        protected bool _procesarIsOK;
        private bool _abandonarIsOK;
        private data _dataGen;


        public BindingSource SourceItems_Get { get { return _dataGen.Items.Source_Get; } }
        public data Ficha { get { return _dataGen; } }


        public ImpGenerar()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _dataGen = new data();
        }


        public void Inicializa()
        {
            _procesarIsOK = false;
            _abandonarIsOK = false;
            _dataGen.Inicializa();
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

        public bool ProcesarIsOK { get { return _procesarIsOK; } }
        public void Procesar() 
        {
        }

        public bool AbandonarIsOK { get { return _abandonarIsOK; } }
        public void AbandonarFicha()
        {
            _abandonarIsOK = Helpers.Msg.Abandonar();
        }

        private bool CargarData()
        {
            try
            {
                var r01 = Sistema.MyData.Configuracion_FactorDivisa();
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
                {
                    throw new Exception(r01.Mensaje);
                }
                _dataGen.Items.setTasaDivisa(r01.Entidad);
                _dataGen.Totales.setTasaDivisa(r01.Entidad);
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }


        private DatosDocumento.IDatosDoc _datosDoc;
        public void NuevoDocumento()
        {
            if (_datosDoc==null)
            {
                _datosDoc = new DatosDocumento.Imp();
            }
            _datosDoc.Inicializa();
            _datosDoc.Inicia();
        }


        private Item.Agregar.IAgregar _itemAgregar;
        public void AgregarItem()
        {
            _itemAgregar = new Item.Agregar.Agregar();
            _itemAgregar.Inicializa();
            _itemAgregar.Inicia();
            if (_itemAgregar.ProcesarIsOK) 
            {
                _dataGen.Items.AgregarItem(_itemAgregar);
                _dataGen.Totales.setMontoNetoDivisa(_dataGen.Items.MontoNetoDivisa);
                _dataGen.Totales.setMontoIvaDivisa(_dataGen.Items.MontoIvaDivisa);
                Helpers.Msg.AgregarOk();
            }
        }
        public void EliminarItem()
        {
            if (_dataGen.Items.ItemActual != null)
            {
                var _msg = "Estas Seguro de querer eliminar este Item ?";
                var r = MessageBox.Show(_msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (r == DialogResult.Yes) 
                {
                    var _item = _dataGen.Items.ItemActual;
                    _dataGen.Items.EliminarItem(_item);
                    _dataGen.Totales.setMontoNetoDivisa(_dataGen.Items.MontoNetoDivisa);
                    _dataGen.Totales.setMontoIvaDivisa(_dataGen.Items.MontoIvaDivisa);
                }
            }
        }
        private Item.Editar.IEditar _itemEditar;
        public void EditarItem()
        {
            if (_dataGen.Items.ItemActual != null)
            {
                var _itemViejo = _dataGen.Items.ItemActual;
                _itemEditar = new Item.Editar.Editar();
                _itemEditar.Inicializa();
                _itemEditar.setItemEditar(_dataGen.Items.ItemActual.Item);
                _itemEditar.Inicia();
                if (_itemEditar.ProcesarIsOK)
                {
                    _dataGen.Items.EliminarItem(_itemViejo);
                    _dataGen.Items.AgregarItem(_itemEditar);
                    _dataGen.Totales.setMontoNetoDivisa(_dataGen.Items.MontoNetoDivisa);
                    _dataGen.Totales.setMontoIvaDivisa(_dataGen.Items.MontoIvaDivisa);
                    Helpers.Msg.AgregarOk();
                }
            }
        }
    }
}