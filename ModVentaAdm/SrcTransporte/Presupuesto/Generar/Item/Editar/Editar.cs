using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.Presupuesto.Generar.Item.Editar
{
    public class Editar : ImpItem, IEditar
    {
        public Editar()
            :base()
        {
        }
        public override void Procesar()
        {
            _procesarIsOK = false;
            if (Item.VerificarDatosIsOK())
            {
                var r = Helpers.Msg.ProcesarGuardar();
                if (r)
                {
                    if (Item.Get_TurnoIsActivo)
                    {
                        Item.setCntDias(Item.Get_CntDias * Item.Get_TurnoCntDias);
                        foreach (var aliad in Item.Get_ListaAliadosLLamados)
                        {
                            aliad.setCnt(Item.Get_TurnoCntDias);
                        }
                    }
                    _procesarIsOK = true;
                }
            }
        }
        public void setItemEditar(data data)
        {
            if (data.Get_TurnoIsActivo)
            {
                var _cntDiasTurno = data.Get_TurnoCntDias;
                if (_cntDiasTurno > 0)
                {
                    data.setCntDias(data.Get_CntDias / _cntDiasTurno);
                    foreach (var aliad in data.Get_ListaAliadosLLamados)
                    {
                        aliad.setCnt(aliad.cnt / _cntDiasTurno);
                    }
                }
            }
            Item.setDescripcion(data.Get_Descripcion);
            Item.setSolicitadoPor(data.Get_SolicitadoPor);
            Item.setModuloaCargar(data.Get_ModuloCargar);
            foreach (var rg in data.Get_Fechas)
            {
                Item.setFecha(rg.fechaServ);
                Item.setHora(rg.horaServ);
                Item.AgregarFecha();
            }
            Item.setCntUnidades(data.Get_CntUnidades);
            Item.setPrecioDivisa(data.Get_PrecioDivisa);
            Item.setDscto(data.Get_Dscto);
            Item.setFecha(DateTime.Now);
            Item.setHora(DateTime.Now);
            Item.setPrecioAliadoPautado(data.Get_Aliado_PrecioPautado);
            Item.setCntAliadoPautado(data.Get_Aliado_CntPautado);
            Item.setAliado(data.Get_Aliado);
            Item.setListaAliadosLlamados(data.Get_ListaAliadosLLamados);
            Item.setDescripcionFull(data.Get_DescripcionFull);
            Item.setUnidadesDetalle(data.Get_UnidadesDetall);
            Item.setTipoServicio(data.Get_TipoServicio);
            //
            Item.setTurnoInicializaEstatus(data.Get_TurnoIsActivo);
            Item.setTurnoCntDias(data.Get_TurnoCntDias);
            Item.setTipoTurno(data.Get_TipoTurno);
            //
            if (_tasasFiscal != null)
            {
                var it = _tasasFiscal.FirstOrDefault(f=>f.id==data.Get_Alicuota_ID);
                if (it != null) 
                {
                    var nr = new alicuota() { id = it.id, codigo = "", desc = it.ToString(), tasa = it.tasa };
                    Item.setAlicuota(nr);
                }
            }
        }
        public void setSolicitadoPor(string desc)
        {
            Item.setSolicitadoPor(desc);
        }
        public void setModuloCargar(string desc)
        {
            Item.setModuloaCargar(desc);
        }
    }
}