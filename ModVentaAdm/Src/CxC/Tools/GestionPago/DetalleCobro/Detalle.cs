using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.CxC.Tools.GestionPago.DetalleCobro
{
    
    public class Detalle: IDetalle, Gestion.IAbandonar, Gestion.IProcesar
    {

        private bool _procesarIsOk;
        private bool _abandonarIsOk;
        private string _notas;
        private Gestion.HndCombo.IOpcion _gCobrador;


        public bool ProcesarIsOK { get { return _procesarIsOk; } }
        public bool AbandonarIsOK { get { return _abandonarIsOk; } }
        public bool DetalleIsOk { get { return _procesarIsOk; } }
        public string GetNotas { get { return _notas; } }
        public string GetIdCobrador { get { return _gCobrador.GetId; } }
        public BindingSource CobradorSource { get { return _gCobrador.Source; } }


        public Detalle() 
        {
            _procesarIsOk = false;
            _abandonarIsOk = false;
            _notas = "";
            _gCobrador= new Gestion.HndCombo.Opcion ();
        }


        public void Inicializa()
        {
            _procesarIsOk = false;
            _abandonarIsOk = false;
            _notas = "";
            _gCobrador.Inicializa();
        }
        DetalleFrm frm;
        public void Inicia()
        {
            if (CargarData()) 
            {
                if (frm == null)
                {
                    frm = new DetalleFrm();
                    frm.setControlador(this);
                }
                frm.ShowDialog();
            }
        }


        private bool CargarData()
        {
            var rt = true;

            var rt1= Sistema.MyData.Sistema_Cobrador_GetLista();
            if (rt1.Result == OOB.Resultado.Enumerados.EnumResult.isError) 
            {
                Helpers.Msg.Error(rt1.Mensaje);
                return false;
            }
            var _lst = rt1.ListaD.OrderBy(o=>o.nombre).Select(s =>
            {
                var f = new Gestion.ficha()
                {
                    codigo = s.codigo,
                    desc = s.nombre,
                    id = s.id,
                };
                return f;
            }).ToList();
            _gCobrador.setData(_lst);

            return rt;
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
            if (_gCobrador.Item == null) 
            {
                Helpers.Msg.Error("CAMPO [ COBRADOR ] NO PUEDE ESTAR VACIO");
                return;
            }
            var msg = "Procesar y Guardar Los Cambios ?";
            var r = MessageBox.Show(msg, "*** ALERTA ***", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                _procesarIsOk = true;
            }
        }


        public void setNotas(string p)
        {
            _notas = p;
        }
        public void setCobrador(string id)
        {
            _gCobrador.setFicha(id);
        }

    }

}