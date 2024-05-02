using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.SrcTransporte.GestionAliadosDoc.AliadosDoc.Vistas
{
    public interface IMain: Src.IGestion
    {
        ImainItems dataItems { get; }
        string Get_Doc_Entidad { get; }
        string Get_Doc_Fecha { get; }
        string GetDoc_Numero { get; }
        string Get_Doc_Nombre { get; }
        string Get_Doc_Monto { get; }
        //
        void setIdDoc(string idDoc);
        void EliminarAliadoInv();
        void AgregarAliadoInv();
    }
}