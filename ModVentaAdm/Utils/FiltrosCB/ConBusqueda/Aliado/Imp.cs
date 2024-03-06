﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Utils.FiltrosCB.ConBusqueda.Aliado
{
    public class Imp: LibUtilitis.CtrlCB.ImpCB, ICtrlConBusqueda
    {
        public Imp()
            :base()
        {
        }
        public void ObtenerData()
        {
            try
            {
                var _lst = new List<Idata>();
                var filtro = new OOB.Transporte.Aliado.Busqueda.Filtro();
                var r01 = Sistema.MyData.TransporteAliado_GetLista(filtro);
                foreach (var rg in r01.ListaD.OrderBy(o => o.nombreRazonSocial ).ToList()) 
                {
                    _lst.Add(new data(rg));
                }
                this.CargarData(_lst);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        public void setTextoBuscar(string desc)
        {
            try
            {
                var _lst = new List<Idata>();
                var filtro = new OOB.Transporte.Aliado.Busqueda.Filtro();
                var r01 = Sistema.MyData.TransporteAliado_GetLista(filtro);
                foreach (var rg in r01.ListaD.Where(w => w.nombreRazonSocial.Trim().ToUpper().Contains(desc.Trim().ToUpper())).OrderBy(o => o.nombreRazonSocial).ToList())
                {
                    _lst.Add(new data(rg));
                }
                this.CargarData(_lst);
            }
            catch (Exception e)
            {
                Helpers.Msg.Error(e.Message);
            }
        }
        public string Get_TextoBuscar { get { return ""; } }
    }
}