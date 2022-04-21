using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModVentaAdm.Src.Documentos.Generar
{
    
    public interface IDatosDoc
    {


        string FechaDocRemision { get; }
        bool AceptarDatosIsOK { get; }
        int IdRegDocTemporal { get; }

        DatosDocumento.ficha EntidadDeposito { get; }
        DatosDocumento.ficha EntidadVendedor { get; }
        DatosDocumento.ficha EntidadSucursal { get; }
        DatosDocumento.ficha EntidadTransporte { get; }
        OOB.Sistema.TipoDocumento.Entidad.Ficha EntidadTipoDoc { get; }
        OOB.Maestro.Cliente.Entidad.Ficha EntidadCliente { get; }
        DatosDocumento.data GetData { get; }


        void setHabilitarSucursal(bool p);
        void setHabilitarDeposito(bool p);
        void setHabilitarBusquedaCliente(bool p);
        void setHabilitarDatosDoc(IDatosDocumento datosDocumento);
        void setTipoDocumento(OOB.Sistema.TipoDocumento.Entidad.Ficha SistTipoDocumento);
        void setIsModoRegistrar(bool p);
        void setFactorDivisa(decimal TasaDivisa);
        void setIdEquipo(string p);
        void Inicializa();
        void Inicia();
        void Limpiar();
        void setNotasDoc(string p);
        void setCargarData(OOB.Venta.Temporal.Encabezado.Entidad.Ficha ficha);
        void setRemision(OOB.Documento.Entidad.Ficha ficha);

    }

}