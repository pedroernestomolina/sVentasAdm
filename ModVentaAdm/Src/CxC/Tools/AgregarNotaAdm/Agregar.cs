using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.AgregarNotaAdm
{
    
    public class Agregar: IAgregar
    {


        private bool _procesarIsOk;
        private bool _abandonarIsOk;
        private dataAgregar _data;
        private Gestion.HndCombo.IOpcion _gVend;
        private Documentos.Generar.BuscarCliente.IBuscar _gCliente;
        private IAgregarTipoNotaAdm _gTipoNotaAdm;


        public bool AbandonarIsOK { get { return _abandonarIsOk; } }
        public bool ProcesarIsOK { get { return _procesarIsOk; } }
        public bool AgregarIsOk { get { return _procesarIsOk; } }


        public string NumeroConsecutivoDocGet{ get { return _gTipoNotaAdm.GetNumeroDocConsecutivo; } }
        public DateTime FechaEmisionDocGet { get { return _gTipoNotaAdm.GetFechaEmision; } }
        public decimal MontoDocGet { get { return _data.MontDivisaDocGet; } }
        public string NotasDocGet { get { return _data.NotasDocGet; } }
        public decimal TasaFactorDocGet { get { return _data.TasaFactorDocGet; } }


        public Agregar() 
        {
            _procesarIsOk = false;
            _abandonarIsOk = false;
            _data = new dataAgregar();
            _gVend = new Gestion.HndCombo.Opcion();
            _gCliente = new Documentos.Generar.BuscarCliente.Gestion();
        }


        AgregarNotaAdmFrm frm;
        public void Inicializa()
        {
            _procesarIsOk = false;
            _abandonarIsOk = false;
            _data.Inicializa();
            _gVend.Inicializa();
            _gCliente.Inicializa();
        }
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null) 
                {
                    frm = new AgregarNotaAdmFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }


        public void AbandonarFicha()
        {
            _abandonarIsOk = false;
            var rt = MessageBox.Show("Abandona Ficha ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rt == DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }
        public void Procesar()
        {
            _procesarIsOk = false;
            if (_data.IsOk())
            {
                _procesarIsOk = _gTipoNotaAdm.Procesar(_data);
            }
        }


        public BindingSource VendGetSource { get { return _gVend.Source; } }
        public string VendGetId { get { return _gVend.GetId; } }
        public void setVend(string id)
        {
            _gVend.setFicha(id);
            _data.setVend(_gVend.Item);

        }
   

        public void setMontoDoc(decimal monto)
        {
            _data.setMontoDoc(monto);
        }
        public void setFactor(decimal tasa)
        {
            _data.setFactor(tasa);
        }
        public void setNotas(string p)
        {
            _data.setNotas(p);
        }


        private bool _clienteSeleccionadoIsOk;
        public bool ClienteSeleccionadoIsOk { get { return _clienteSeleccionadoIsOk; } }
        public string ClienteDataGet { get { return _data.ClienteDataGet; } }
        public void BuscarCliente()
        {
            _clienteSeleccionadoIsOk = false;
            _gCliente.Inicializa();
            _gCliente.setActivarSeleccionItem(true);
            _gCliente.Inicia();
            if (_gCliente.ItemSeleccionadoIsOk) 
            {
                string idCliente = _gCliente.IdItemSeleccionado;
                var r01 = Sistema.MyData.Cliente_GetFicha(idCliente);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _clienteSeleccionadoIsOk = true;
                _data.setCliente(r01.Entidad);
                setVend(r01.Entidad.idVendedor);
            }
        }


        private bool CargarData()
        {
            var rt = true;

            if (_gTipoNotaAdm.CargarData())
            {
                var r01 = Sistema.MyData.Sistema_Vendedor_GetLista();
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
                _data.setFactor(r02.Entidad);

                var lst = new List<Gestion.ficha>();
                foreach (var rg in r01.ListaD.OrderBy(o => o.nombre).ToList())
                {
                    var nr = new Gestion.ficha()
                    {
                        codigo = rg.codigo,
                        desc = rg.nombre,
                        id = rg.id,
                    };
                    lst.Add(nr);
                }
                _gVend.setData(lst);
            }
            else
            {
                rt = false;
            }

            return rt;
        }

        public string TipoNotaAdmGet { get { return _gTipoNotaAdm.GetTipoNotaAdm; } }
        public void setTipoNota(IAgregarTipoNotaAdm ctr)
        {
            _gTipoNotaAdm = ctr;
        }

    }

}