using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.DialogoFecha
{
    public class Imp: IDialogoFecha
    {
        private DateTime _fecha;
        private bool _isOk;
        //
        public DateTime GetFecha { get { return _fecha; } }
        public bool IsOk { get { return _isOk; } }
        //
        public Imp()
        {
            _isOk = false;
            _fecha = DateTime.Now.Date;
        }
        public void Inicializa()
        {
            _isOk = false;
            _fecha = DateTime.Now.Date;
        }
        Frm frm;
        public void Inicia()
        {
            if (frm == null) 
            {
                frm = new Frm();
                frm.setControador(this);
            }
            frm.ShowDialog();
        }
        public void setFecha(DateTime fecha)
        {
            _fecha = fecha;
        }
        public void Procesar()
        {
            _isOk = true;
        }
    }
}