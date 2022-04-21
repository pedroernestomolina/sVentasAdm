using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Documentos.Generar.AgregarEditarItem
{
    
    public class Gestion
    {

        private data _data;
        private string _modoFicha;
        private string _tarifaPrecioManejar;
        private IGestion _gestionItem;
        private List<precio> _precios;
        private BindingSource _preciosSource;
        private bool _abandonarIsOk ;
        private bool _procesarItemIsOk;
        private int _idTempVenta;
        private bool _isModoAgregar;
        private int _idItemAgregado;
        private decimal _exReal;
        private decimal _exDisponible;
        private bool _visualizarCostoIsActivo;
        private int _idItemEditar;
        private int _posActual;


        public OOB.Producto.Entidad.Ficha _prd { get { return _data.Producto; } }
        public string ModoFicha { get { return _modoFicha; } }
        public string Producto_Desc { get { return "(Código: " +_prd.CodigoPrd + ") " + _prd.NombrePrd; } }
        public BindingSource PreciosSource { get { return _preciosSource; } }
        public string Data_Empaque { get { return _data.Empaque; } }
        public decimal Data_Importe { get { return _data.Importe; } }
        public decimal Data_TasaIva { get { return _data.TasaIva; } }
        public decimal Data_Precio { get { return _data.PNeto; } }
        public decimal Data_Total { get { return _data.GetImporteFull; } }
        public decimal Data_Iva { get { return _data.GetIva; } }
        public bool AbandonarIsOk { get { return _abandonarIsOk; } }
        public bool ProcesarItemIsOk { get { return _procesarItemIsOk; } }
        public int IdItemAgregado { get { return _idItemAgregado; } }
        public decimal Cantidad { get { return _data.GetCantidad; } }
        public string NDecimales { get { return _data.GetDecimales; } }
        public string Notas { get { return _data.GetNotas; } }
        public decimal Dscto { get { return _data.GetDsctoPorct; } }
        public decimal Data_CostoUnd { get { return _data.GetCostoUnd; } }
        public decimal Data_CostoEmp { get { return _data.GetCostoEmp; } }
        public decimal Data_ExReal { get { return _exReal; } }
        public decimal Data_ExDisponible { get { return _exDisponible; } }
        public bool VisualizarCostoIsActivo { get { return _visualizarCostoIsActivo; } }
        public decimal Data_Dscto { get { return _data.GetDsctoPorct; } }


        public Gestion() 
        {
            _data = new data();
            _isModoAgregar = false;
            _idTempVenta = -1;
            _idItemEditar = -1;
            _modoFicha = "";
            _abandonarIsOk = false;
            _procesarItemIsOk = false;
            _precios = new List<precio>();
            _preciosSource = new BindingSource();
            _preciosSource.CurrentChanged += _preciosSource_CurrentChanged;
            _preciosSource.DataSource = _precios;
            _idItemAgregado = -1;
            _exReal = 0m;
            _exDisponible = 0m;
            _visualizarCostoIsActivo = false;
            _posActual = 0;
        }

        private void _preciosSource_CurrentChanged(object sender, EventArgs e)
        {
            if (_preciosSource.Current != null) 
            {
                var it=(precio) _preciosSource.Current;
                _data.setPrecio(_precios.FirstOrDefault(f => f.ID == it.ID));
            }
        }

        public void Inicializa() 
        {
            _data.Limpiar();
            _isModoAgregar = false;
            _idTempVenta = -1;
            _idItemEditar = -1;
            _precios.Clear();
            _modoFicha = "";
            _abandonarIsOk = false;
            _procesarItemIsOk = false;
            _idItemAgregado = -1;
            _exReal = 0m;
            _exDisponible = 0m;
            _visualizarCostoIsActivo = false;
            _posActual = 0;
            _precioNetoCargar = 0m;
        }

        AgregarEditarItemFrm _frm;
        public void Inicia() 
        {
            if (CargarData()) 
            {
                if (_frm == null) 
                {
                    _frm = new AgregarEditarItemFrm();
                    _frm.setControlador(this);
                }
                _frm.ShowDialog();
            }
        }

        private bool CargarData()
        {
            var r01 = Sistema.MyData.Producto_Existencia_GetFicha(_data.Producto.Auto, _data.GetIdDeposito);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            _exReal = r01.Entidad.real;
            _exDisponible = r01.Entidad.disponible;

            switch (_tarifaPrecioManejar)
            {
                case "1":
                    var p1 = new precio("1", "P1", _prd.empaque_1, _prd.contenido_1, _prd.pneto_1, _prd.decimales);
                    _precios.Add(p1);
                    break;
                case "2":
                    var p2 = new precio("2", "P2", _prd.empaque_2, _prd.contenido_2, _prd.pneto_2, _prd.decimales);
                    _precios.Add(p2);
                    break;
                case "3":
                    var p3 = new precio("3", "P3", _prd.empaque_3, _prd.contenido_3, _prd.pneto_3, _prd.decimales);
                    _precios.Add(p3);
                    break;
                case "4":
                    var p4 = new precio("4", "P4", _prd.empaque_4, _prd.contenido_4, _prd.pneto_4, _prd.decimales);
                    _precios.Add(p4);
                    break;
                case "5":
                    var p5 = new precio("5", "P5", _prd.empaque_5, _prd.contenido_5, _prd.pneto_5, _prd.decimales);
                    _precios.Add(p5);
                    break;
                default:
                    var xp1 = new precio("1", "P1", _prd.empaque_1, _prd.contenido_1, _prd.pneto_1, _prd.decimales);
                    _precios.Add(xp1);
                    var xp2 = new precio("2", "P2", _prd.empaque_2, _prd.contenido_2, _prd.pneto_2, _prd.decimales);
                    _precios.Add(xp2);
                    var xp3 = new precio("3", "P3", _prd.empaque_3, _prd.contenido_3, _prd.pneto_3, _prd.decimales);
                    _precios.Add(xp3);
                    var xp4 = new precio("4", "P4", _prd.empaque_4, _prd.contenido_4, _prd.pneto_4, _prd.decimales);
                    _precios.Add(xp4);
                    var xp5 = new precio("5", "P5", _prd.empaque_5, _prd.contenido_5, _prd.pneto_5, _prd.decimales);
                    _precios.Add(xp5);
                    break;
            }
            var pM1 = new precio("6", "PM1", _prd.empaqueMay_1, _prd.contenidoMay_1, _prd.pnetoMay_1, _prd.decimales);
            _precios.Add(pM1);
            var pM2 = new precio("7", "PM2", _prd.empaqueMay_2, _prd.contenidoMay_2, _prd.pnetoMay_2, _prd.decimales);
            _precios.Add(pM2);

            _preciosSource.CurrencyManager.Refresh();
            return true;
        }

        public void setAgregar(OOB.Producto.Entidad.Ficha ficha, int _idVentaTemporal)
        {
            this._idTempVenta = _idVentaTemporal;
            _modoFicha = "Agregar Item";
            _isModoAgregar = true;
            _data.setProducto(ficha);
        }

        public void setTarifaPrecio(string tarifa)
        {
            _tarifaPrecioManejar = tarifa;
        }

        public void setItemDocGestion(IGestion gestion) 
        {
            _gestionItem = gestion;
        }

        public void setCantidad(decimal cnt)
        {
            _data.setCantidad(cnt);
        }

        public void setDescuento(decimal dsct)
        {
            if (dsct > 0m)
            {
                var r00 = Sistema.MyData.Permiso_GenerarDoc_DarDsctoItem(Sistema.Usuario.idGrupo);
                if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r00.Mensaje);
                    return;
                }
                if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
                {
                    _data.setDescuento(dsct);
                }
            }
        }

        public void setNotas(string p)
        {
            _data.setNotas(p);
        }

        public void Abandonar()
        {
            _abandonarIsOk = false;
            var msg = "Abandonar Ficha ?";
            var rt = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rt== DialogResult.Yes)
            {
                _abandonarIsOk = true;
            }
        }

        public void ProcesarItem()
        {
            if (_data.IsCantidad_IgualA_Cero)
            {
                Helpers.Msg.Error("Item Cantidad A Despachar No Puede Ser Cero (0)");
                return;
            }
            if (_data.IsPrecio_IgualA_Cero)
            {
                Helpers.Msg.Error("Item Precio No Puede Ser Cero (0)");
                return;
            }
            if (_data.IsImporte_Cero)
            {
                Helpers.Msg.Error("Item Importe No Puede Ser Cero (0)");
                return;
            }
            if (_data.IsPrecioPorDebajoDelCosto)
            {
                Helpers.Msg.Error("Producto Por Debajo Del Costo, Verifique Por Favor!!!");
                return;
            }

            _idItemAgregado = -1;
            if (_isModoAgregar )
            {
                if (_gestionItem.AgregarItem(_data, _idTempVenta))
                {
                    _procesarItemIsOk = true;
                    _idItemAgregado = _gestionItem.IdItemAgregado;
                }
            }
            else
            {
                if (_gestionItem.EditarItem(_data, _idTempVenta, _idItemEditar))
                {
                    _procesarItemIsOk = true;
                    _idItemAgregado = _gestionItem.IdItemAgregado;
                }
            }
        }

        public void setTasaDivisa(decimal TasaDivisa)
        {
            _data.setTasaDivisa(TasaDivisa);
        }

        public void setEntidadDeposito(DatosDocumento.ficha ficha)
        {
            _data.setIdDeposito(ficha.id);
            _data.setNombreDeposito(ficha.desc);
        }

        public void setRupturaPorExistencia(bool ruptura)
        {
            _data.setRupturaPorExistencia(ruptura);
        }

        public void setEditar(Items.data _itActual,int _idVentaTemporal,OOB.Producto.Entidad.Ficha _prd)
        {
            this._idTempVenta = _idVentaTemporal;
            _modoFicha = "Actualizar/Editar Item";
            _isModoAgregar = false;
            _data.setCantidad(_itActual.Cant);
            _data.setDescuento(_itActual.Dscto);
            _data.setNotas(_itActual.Notas);
            _data.setPrecio(_itActual);
            _data.setPrecioNeto(_itActual.PNeto);
            _data.setProducto(_prd);
            _idItemEditar = _itActual.Id;
            var pos = 0;
            switch (_itActual.DataItem.tarifaPrecio)
            { 
                case "1":
                    pos = 0;
                    break;
                case "2":
                    pos = 1;
                    break;
                case "3":
                    pos = 2;
                    break;
                case "4":
                    pos = 3;
                    break;
                case "5":
                    pos = 4;
                    break;
                case "6":
                    pos = 5;
                    break;
                case "7":
                    pos = 6;
                    break;
            }
            _posActual = pos;
        }

        public void VisualizarCosto()
        {
            var r00 = Sistema.MyData.Permiso_GenerarDoc_VisualizarCosto(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }

            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _visualizarCostoIsActivo = true;
            }
        }

        public void PrecioIniciar()
        {
            _preciosSource.CurrencyManager.Position = _posActual;
            _preciosSource.CurrencyManager.Refresh();
        }

        public void EliminarDscto()
        {
            _data.setDescuento(0m);
        }

        public void setPrecio(decimal precio)
        {
            if (precio > 0m)
            {
                if (precio != _data.GetPrecioNeto)
                {
                    var r00 = Sistema.MyData.Permiso_GenerarDoc_PrecioLibre(Sistema.Usuario.idGrupo);
                    if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                    {
                        Helpers.Msg.Error(r00.Mensaje);
                        return;
                    }
                    if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
                    {
                        _data.setPrecioNeto(precio);
                    }
                }
            }
        }

        private decimal _precioNetoCargar=0m;
        public void setCargarPrecioNeto(decimal p)
        {
            _precioNetoCargar = p;
        }

        public void ActualizarPrecioNeto()
        {
            if (_precioNetoCargar == 0m) { return; }
            _data.setPrecioNeto(_precioNetoCargar);
        }

    }

}