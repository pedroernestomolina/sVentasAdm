using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Maestros.Grupo
{

    public class AgregarEditar
    {

        private data _data;
        private bool _isModoAgregar;
        private OOB.Maestro.Grupo.Entidad.Ficha _ficha;
        private bool _isOk;


        public bool IsOk { get { return _isOk; } }
        public string Codigo { get { return _data.Codigo; } }
        public string Nombre { get { return _data.Descripcion; } }
        public OOB.Maestro.Grupo.Entidad.Ficha FichaAgregadaActualizada { get { return _ficha; } }


        public AgregarEditar()
        {
            _data = new data();
            _ficha= null;
        }


        AgregarEditarFrm frm;
        public void Agregar()
        {
            _isModoAgregar = true;
            LimpiarEntradas();
            if (CargarData())
            {
                if (frm == null)
                {
                    frm = new AgregarEditarFrm();
                    frm.setControlador(this);
                }
                frm.setTitulo("Agregar Grupo:");
                frm.ShowDialog();
            }
        }

        private void LimpiarEntradas()
        {
            _data.Limpiar();
            _isOk = false;
            _ficha = null;
        }

        private bool CargarData()
        {
            var rt = true;
            return rt;
        }

        public void Guardar()
        {
            if (_data.VerificarIsOk())
            {
                if (_isModoAgregar)
                {
                    var msg = MessageBox.Show("Guardar Data ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (msg == DialogResult.Yes)
                    {
                        var xficha = new OOB.Maestro.Grupo.Agregar.Ficha()
                        {
                            nombre = Nombre,
                            codigo = Codigo,
                        };
                        var r01 = Sistema.MyData.ClienteGrupo_Agregar(xficha);
                        if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                        {
                            Helpers.Msg.Error(r01.Mensaje);
                            return;
                        }
                        var r02 = Sistema.MyData.ClienteGrupo_GetFichaById(r01.Auto);
                        if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                        {
                            Helpers.Msg.Error(r02.Mensaje);
                            return;
                        }
                        _ficha = r02.Entidad;
                        _isOk = true;
                    }
                }
                else
                {
                    if (_data.Id.Trim() == "")
                    {
                        Helpers.Msg.Error("ID ENTIDAD NO PUEDE ESTAR VACIO");
                        return;
                    }
                    var msg = MessageBox.Show("Cambiar/Actualizar Data ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (msg == DialogResult.Yes)
                    {
                        var xficha = new OOB.Maestro.Grupo.Editar.Ficha()
                        {
                            auto = _data.Id,
                            nombre = Nombre,
                            codigo = Codigo,
                        };
                        var r01 = Sistema.MyData.ClienteGrupo_Editar(xficha);
                        if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                        {
                            Helpers.Msg.Error(r01.Mensaje);
                            return;
                        }
                        var r02 = Sistema.MyData.ClienteGrupo_GetFichaById(_data.Id);
                        if (r02.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                        {
                            Helpers.Msg.Error(r02.Mensaje);
                            return;
                        }
                        _ficha = r02.Entidad;
                        _isOk = true;
                    }
                }
            }
        }

        public void Editar(Maestros.data itActual)
        {
            LimpiarEntradas();
            _isModoAgregar = false;
            if (CargarData())
            {
                var r01 = Sistema.MyData.ClienteGrupo_GetFichaById(itActual.id);
                if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
                {
                    Helpers.Msg.Error(r01.Mensaje);
                    return;
                }
                _data.setId(r01.Entidad.auto);
                setCodigo(r01.Entidad.codigo);
                setNombre(r01.Entidad.nombre);
                if (frm == null)
                {
                    frm = new AgregarEditarFrm();
                    frm.setControlador(this);
                }
                frm.setTitulo("Editar Grupo:");
                frm.ShowDialog();
            }
        }

        public bool AbandonarDocumento()
        {
            var msg = MessageBox.Show("Abandonar Documento ?", "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            return (msg == DialogResult.Yes);
        }

        public void setNombre(string p)
        {
            _data.setNombre(p);
        }

        public void setCodigo(string p)
        {
            _data.setCodigo(p);
        }

    }

}