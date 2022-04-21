using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.ReportesCliente.Filtro
{
    
    public class data
    {

        private general _grupo;
        private general _estado;
        private general _zona;
        private general _vendedor;
        private general _cobrador;
        private general _categoria;
        private general _nivel;
        private general _tarifa;
        private general _estatus;
        private general _credito;


        public general Grupo { get { return _grupo; } }
        public general Estado { get { return _estado; } }
        public general Zona { get { return _zona; } }
        public general Vendedor { get { return _vendedor; } }
        public general Cobrador { get { return _cobrador; } }
        public general Categoria { get { return _categoria; } }
        public general Nivel { get { return _nivel; } }
        public general Tarifa { get { return _tarifa; } }
        public general Estatus { get { return _estatus; } }
        public general Credito { get { return _credito; } }


        public data()
        {
            limpiar();
        }


        private void limpiar()
        {
            _grupo = null;
            _zona = null;
            _estado = null;
            _vendedor= null;
            _cobrador = null;
            _categoria = null;
            _nivel = null;
            _tarifa = null;
            _estatus = null;
            _credito = null;
        }

        public bool IsOk()
        {
            var rt = true;

            return rt;
        }

        public void Inicializa()
        {
            limpiar();
        }

        public void setGrupo(general ficha)
        {
            _grupo = ficha;
        }

        public void setEstado(general ficha)
        {
            _estado = ficha;
        }

        public void setZona(general ficha)
        {
            _zona = ficha;
        }

        public void setVendedor(general ficha)
        {
            _vendedor = ficha;
        }

        public void setCobrador(general ficha)
        {
            _cobrador = ficha;
        }

        public  void setCategoria(general ficha)
        {
            _categoria = ficha;
        }

        public  void setNivel(general ficha)
        {
            _nivel = ficha;
        }

        public void setCredito(general ficha)
        {
            _credito = ficha;
        }

        public void setEstatus(general ficha)
        {
            _estatus = ficha;
        }

        public void setTarifa(general ficha)
        {
            _tarifa = ficha;
        }

    }

}