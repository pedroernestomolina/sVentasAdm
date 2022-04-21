using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar
{
    
    public interface IItems
    {

        int  CntItem { get; }
        decimal MontoTotal { get; }
        decimal MontoTotalDivisa { get; }
        decimal MontoNeto { get; }
        decimal MontoIva { get; }
        System.Windows.Forms.BindingSource ItemsSource { get; }
        bool HayItemsEnBandeja { get; }
        Items.data ItemActual { get; }
        List<Items.data> ListaItems { get; }


        void Inicializa();
        void setDivisa(decimal TasaDivisa);
        void EliminarItem(Items.data _itActual);
        void EliminarLista(int _idEditar);
        void EliminarListaItems();
        void AgregarItem(OOB.Venta.Temporal.Item.Entidad.Ficha ficha, decimal TasaDivisa);
        void AgregarLista(List<OOB.Venta.Temporal.Item.Entidad.Ficha> list, decimal p);
        void LimpiarItems();
        void setCambioTasaDivisa(decimal p);

    }

}