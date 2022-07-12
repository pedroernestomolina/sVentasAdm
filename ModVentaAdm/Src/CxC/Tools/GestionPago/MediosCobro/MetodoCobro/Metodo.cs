using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.MediosCobro.MetodoCobro
{
    
    public class Metodo: IMetodoAgregarEditar
    {

        private decimal _montoResta;
        private bool _procesarIsOk;
        private bool _abandonarIsOk;
        private dataItem _item;
        private Gestion.HndCombo.IOpcion _gCB_MetCobro;
        private dataItem _itemEditar;


        public bool AbandonarIsOK { get { return _abandonarIsOk; } }
        public bool ProcesarIsOK { get { return _procesarIsOk; } }
        public string GetTituloFicha { get { return ""; } }
        public dataItem ItemAgregarEditar { get { return _item; } }


        public Metodo()
        {
            _itemEditar = null;
            _montoResta = 0m;
            _abandonarIsOk = false;
            _procesarIsOk = false;
            _gCB_MetCobro = new Gestion.HndCombo.Opcion();
        }


        public void Inicializa()
        {
            _itemEditar = null;
            _item = new dataItem();
            limpiar();
            _gCB_MetCobro.Inicializa();
        }

        MetCobroFrm frm;
        public void Inicia()
        {
            if (CargarDta()) 
            {
                if (frm == null) 
                {
                    frm = new MetCobroFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }


        public decimal GetMontoResta { get { return _montoResta; } }
        public BindingSource GetMetCobroSource { get { return _gCB_MetCobro.Source; } }
        public string GetMetCobroID { get { return _gCB_MetCobro.GetId; } }
        public decimal GetMonto { get { return _item.GetMonto; } }
        public decimal GetFactor { get { return _item.GetFactorCambio; } }
        public string GetBanco { get { return _item.GetBanco; } }
        public string GetNroCta { get { return _item.GetNroCta; } }
        public string GetCheqRefTrans { get { return _item.GetCheqRefTranf; } }
        public DateTime GetFechaOp { get { return _item.GetFechaOp; } }
        public string GetDetalleOp { get { return _item.GetDetalleOp; } }
        public bool GetAplicaFactor { get { return _item.GetAplicaFactor; } }
        public string GetReferencia { get { return _item.GetReferencia ; } }
        public string GetLote { get { return _item.GetLote ; } }


        public void setMetCobro(string id)
        {
            _gCB_MetCobro.setFicha(id);
            _item.setMetCobro(_gCB_MetCobro.Item);
        }
        public void setMonto(decimal monto)
        {
            _item.setMonto(monto);
        }
        public void setFactor(decimal factor)
        {
            _item.setFactor(factor);
        }
        public void setBanco(string banco)
        {
            _item.setBanco(banco);
        }
        public void setCtaNro(string cta)
        {
            _item.setCtaNro(cta);
        }
        public void setChequeRefTranf(string cheqRefTranf)
        {
            _item.setChequeRefTranf(cheqRefTranf);
        }
        public void setFechaOperacion(DateTime fecha)
        {
            _item.setFechaOperacion(fecha);
        }
        public void setDetalleOperacion(string detalleOp)
        {
            _item.setDetalleOperacion(detalleOp);
        }
        public void setAplicaFactor(bool p)
        {
            _item.setAplicaFactor(p);
        }
        public void setLote(string lote)
        {
            _item.setLote(lote);
        }
        public void setReferencia(string referenc)
        {
            _item.setReferencia(referenc);
        }
        public void setMontoResta(decimal montPend)
        {
            _montoResta = montPend;
        }
        public void setItemEditar(dataItem item)
        {
            _itemEditar = item;
        }


        public void AbandonarFicha()
        {
            _abandonarIsOk = false;
            var msg = "Abandonar y Perder Los Cambios ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }
        public void Procesar()
        {
            _procesarIsOk = false;
            if (_item==null)
            {
                Helpers.Msg.Error("ITEM NO PUEDE SER NULO");
                return;
            }
            if (_gCB_MetCobro.Item == null) 
            {
                Helpers.Msg.Error("CAMPO [METODO DE COBRO] NO PUEDE ESTAR VACIO");
                return;
            }
            if (_item.GetMonto <=0m)
            {
                Helpers.Msg.Error("CAMPO [MONTO] NO PUEDE SER CERO (0)");
                return;
            }
            var msg = "Procesar y Guardar Los Cambios ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                _procesarIsOk = true;
            }
        }


        private bool CargarDta()
        {
            var r01 = Sistema.MyData.Sistema_MedioCobro_GetLista();
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            var r02 = Sistema.MyData.Configuracion_FactorDivisa();
            if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r02.Mensaje);
                return false;
            }
            var _lst = new List<Gestion.ficha>();
            foreach (var rg in r01.ListaD.OrderBy(o => o.nombre).ToList())
            {
                var _rg = new Gestion.ficha(rg.id, rg.codigo, rg.nombre);
                _lst.Add(_rg);
            }
            _gCB_MetCobro.setData(_lst);
            setFactor(r02.Entidad);
            if (_itemEditar != null) 
            {
                setMetCobro(_itemEditar.GetMetodo.id);
                setMonto(_itemEditar.GetMonto);
                setAplicaFactor(_itemEditar.GetAplicaFactor);
                setFactor(_itemEditar.GetFactorCambio);
                setBanco(_itemEditar.GetBanco);
                setCtaNro(_itemEditar.GetNroCta);
                setChequeRefTranf(_itemEditar.GetCheqRefTranf);
                setFechaOperacion(_itemEditar.GetFechaOp);
                setDetalleOperacion(_itemEditar.GetDetalleOp);
                setReferencia(_itemEditar.GetReferencia);
                setLote(_itemEditar.GetLote);
            }
            return true;
        }
        private void limpiar()
        {
            _item.Limpiar();
            _abandonarIsOk = false;
            _procesarIsOk = false;
        }

    }

}