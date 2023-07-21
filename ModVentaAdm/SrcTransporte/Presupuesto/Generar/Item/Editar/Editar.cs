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
                    _procesarIsOK = true;
                }
            }
        }
        public void setItemEditar(data data)
        {
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
    }
}