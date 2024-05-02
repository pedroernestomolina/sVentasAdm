using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Handler
{
    public class ImpMain : Vistas.IMain
    {
        private string _idDocCargar;
        private OOB.Transporte.Documento.GestionAliados.ObtenerLista.Documento _docMostar;
        private List<OOB.Transporte.Documento.GestionAliados.ObtenerLista.Item> _aliadosInv;
        private bool _isDocumentoValido;
        private bool _isDocumentoModificable;
        private Vistas.ImainItems _dataItems;
        private int _idAliadoAgregado;
        private bool _agregarAliadoIsOk;
        private string _fechaDoc;
        private string _numeroDoc;
        private string _entidadDoc;
        private string _nombreDoc;
        private string _montoDoc;
        //
        public Vistas.ImainItems dataItems { get { return _dataItems; } }
        public string Get_Doc_Fecha { get { return _fechaDoc; } }
        public string GetDoc_Numero { get { return _numeroDoc; } }
        public string Get_Doc_Entidad { get { return _entidadDoc; } }
        public string Get_Doc_Nombre { get { return _nombreDoc; } }
        public string Get_Doc_Monto { get { return _montoDoc; } }
        //
        public ImpMain()
        {
            _idDocCargar = "";
            _docMostar = null;
            _aliadosInv = null;
            _isDocumentoModificable = false;
            _isDocumentoValido = false;
            _dataItems = new ImpMainItems();
            _idAliadoAgregado = -1;
            _agregarAliadoIsOk = false;
            _fechaDoc = "";
            _numeroDoc = "";
            _entidadDoc = "";
            _nombreDoc="";
            _montoDoc="";
        }
        public void Inicializa()
        {
            _idDocCargar = "";
            _docMostar = null;
            _aliadosInv = null;
            _isDocumentoModificable = false;
            _isDocumentoValido = false;
            _dataItems.Inicializa();
            _idAliadoAgregado = -1;
            _agregarAliadoIsOk = false;
            _fechaDoc = "";
            _numeroDoc = "";
            _entidadDoc = "";
            _nombreDoc = "";
            _montoDoc = "";
        }
        Vistas.Frm frm;
        public void Inicia()
        {
            if (cargarData())
            {
                if (frm == null)
                {
                    frm = new Vistas.Frm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }
        public void setIdDoc(string idDoc)
        {
            _idDocCargar = idDoc;
        }
        public void AgregarAliadoInv()
        {
            if (_isDocumentoModificable)
            {
                agregarAliado();
                if (_agregarAliadoIsOk)
                {
                    cargarData();
                }
            }
        }
        public void EliminarAliadoInv()
        {
            if (_isDocumentoModificable)
            {
                eliminarAliado();
            }
        }
        //
        private bool cargarData()
        {
            try
            {
                if (_idDocCargar.Trim().ToUpper() == "")
                {
                    throw new Exception("NO SE HA INDICADO EL DOCUMENTO A CARGAR");
                }
                var r01 = Sistema.MyData.TransporteDocumento_GestionAliados_ObtenerListaByIdDoc(_idDocCargar);
                _docMostar = r01.Entidad.documento;
                _aliadosInv = r01.Entidad.items;
                _isDocumentoValido = isDocumentoValido(_docMostar.codigoTipoDoc);
                if (!_isDocumentoValido)
                {
                    throw new Exception("DOCUMENTO A CARGAR NO ES UN DOCUMENTO VALIDO PARA MOSTRAR ALIADOS");
                }
                _isDocumentoModificable = isDocumentoModificable(_docMostar.codigoTipoDoc, _docMostar.estatusDoc);
                _dataItems.setAliadosInv(_aliadosInv.Where(w => w.estatusAnuladoAliadoDoc == "0").OrderBy(o => o.aliadoNombre).ToList());
                _fechaDoc = "Fecha: "+_docMostar.fechaDoc.ToShortDateString();
                _numeroDoc = "Numero: "+_docMostar.numeroDoc;
                _entidadDoc = _docMostar.ciRifEntidadDoc + Environment.NewLine + _docMostar.entidadDoc;
                _nombreDoc = _docMostar.nombreDoc;
                _montoDoc = "Monto($): " + _docMostar.montoDivDoc.ToString("n2");
                return true;
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
                return false;
            }
        }
        //
        private bool isDocumentoValido(string codDoc)
        {
            if (codDoc == "01" || codDoc == "04")
                return true;
            else
                return false;
        }
        //
        private bool isDocumentoModificable(string codDoc, string estatus)
        {
            if (estatus == "0")
            {
                if (codDoc == "01" || codDoc == "04")
                    return true;
                else
                    return false;
            }
            else 
            {
                return false;
            }
        }
        //
        private Src.Anular.Gestion _anular;
        private void eliminarAliado()
        {
            if (_dataItems.Get_ItemActual == null)
                return;

            var it = (Impdata)_dataItems.Get_ItemActual;
            if (it != null)
            {
                if (_anular == null)
                {
                    _anular = new Src.Anular.Gestion();
                }
                _anular.Inicializa();
                _anular.Inicia();
                if (_anular.ProcesarIsOK)
                {
                    try
                    {
                        var fichaOOB = new OOB.Transporte.Documento.GestionAliados.AnularAliado.Ficha()
                        {
                            idAliado = it.aliadoId,
                            idRefAliadoDoc = it.idRef,
                            idUsuario = Sistema.Usuario.id,
                            montoAnular = it.importeDiv,
                            nombreUsuario = Sistema.Usuario.nombre,
                            motivo = _anular.Motivo,
                        };
                        var r01 = Sistema.MyData.TransporteDocumento_GestionAliados_AnularAliado(fichaOOB);
                        _dataItems.EliminarAliadoInv(it);
                        Helpers.Msg.EliminarOk();
                    }
                    catch (Exception e)
                    {
                        Helpers.Msg.Error(e.Message);
                        return;
                    }
                }
            }
        }
        private Agregar.Vistas.IMain _agregarAliado;
        private void agregarAliado()
        {
            _idAliadoAgregado = -1;
            _agregarAliadoIsOk = false;
            //
            if (_agregarAliado == null)
            {
                _agregarAliado = new Agregar.Handler.ImpMain();
            }
            _agregarAliado.Inicializa();
            _agregarAliado.Inicia();
            if (_agregarAliado.ProcesarIsOk)
            {
                try
                {
                    var _data= (Agregar.Handler.dataExportar)_agregarAliado.dataAgregar();
                    var ficha = new OOB.Transporte.Documento.GestionAliados.InsertarAliado.Ficha()
                    {
                        codigoServ = _data.servicio.codigo,
                        descServ = _data.servDesc,
                        detalleServ = _data.servicio.desc,
                        docCodigo = _docMostar.codigoTipoDoc,
                        docFecha = _docMostar.fechaDoc,
                        docId = _docMostar.idDoc,
                        docIdCliente = _docMostar.idEntidadDoc,
                        docNombre = _docMostar.nombreDoc,
                        docNumero = _docMostar.numeroDoc,
                        idAliado = int.Parse(_data.aliado.id),
                        idServ = int.Parse(_data.servicio.id),
                        montoDiv = _data.montoDiv,
                    };
                    var r01 = Sistema.MyData.TransporteDocumento_GestionAliados_InsertarAliado(ficha);
                    _idAliadoAgregado = r01.Entidad;
                    _agregarAliadoIsOk = true;
                    Helpers.Msg.AgregarOk();
                }
                catch (Exception e)
                {
                    Helpers.Msg.Error(e.Message);
                }
            }
        }
    }
}