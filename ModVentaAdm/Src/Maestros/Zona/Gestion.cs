using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModVentaAdm.Src.Maestros.Zona
{

    public class Gestion : IGestion
    {


        private IGestionLista _gestionLista;
        private AgregarEditar _agregarEditar;


        public string Maestro { get { return "Maestro: Zonas"; } }
        public int TotalItems { get { return _gestionLista.TotalItems; } }
        public BindingSource Source { get { return _gestionLista.Source; } }


        public Gestion()
        {
            _gestionLista = new GestionLista();
            _agregarEditar = new AgregarEditar();
        }


        public bool CargarData()
        {
            var rt = true;

            var filtro = new OOB.Maestro.Zona.Lista.Filtro();
            var r01 = Sistema.MyData.ClienteZona_GetLista(filtro);
            if (r01.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r01.Mensaje);
                return false;
            }
            var lst = new List<Maestros.data>();
            foreach (var rg in r01.ListaD)
            {
                lst.Add(new Maestros.data(rg));
            }
            _gestionLista.setLista(lst);

            return rt;
        }

        public void AgregarItem()
        {
            var r00 = Sistema.MyData.Permiso_ClienteZona_Agregar(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                _agregarEditar.Agregar();
                if (_agregarEditar.IsOk)
                {
                    _gestionLista.Agregar(new Maestros.data(_agregarEditar.FichaAgregadaActualizada));
                }
            }
        }

        public void EditarItem()
        {
            var r00 = Sistema.MyData.Permiso_ClienteZona_Editar(Sistema.Usuario.idGrupo);
            if (r00.Result == OOB.Resultado.Enumerados.EnumResult.isError)
            {
                Helpers.Msg.Error(r00.Mensaje);
                return;
            }
            if (Seguridad.Gestion.SolicitarClave(r00.Entidad))
            {
                var itActual = _gestionLista.ItemActual;
                if (itActual != null)
                {
                    _agregarEditar.Editar(itActual);
                    if (_agregarEditar.IsOk)
                    {
                        _gestionLista.Actualizar(new Maestros.data(_agregarEditar.FichaAgregadaActualizada));
                    }
                }
            }
        }

        public void Inicializa()
        {
        }

    }

}