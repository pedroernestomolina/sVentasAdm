using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ModVentaAdm.Helpers
{
    
    public class Utilitis
    {
        static public string _DataLocal { get; set; }


        static public void Calculadora()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"calc.exe";
            p.Start();
            //p.WaitForExit();
        }

        static public OOB.Resultado.Ficha CargarXml()
        {
            var result = new OOB.Resultado.Ficha();

            try
            {
                var doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"\Conf.XML");

                if (doc.HasChildNodes)
                {
                    foreach (XmlNode nd in doc)
                    {
                        if (nd.LocalName.ToUpper().Trim() == "CONFIGURACION")
                        {
                            foreach (XmlNode nv in nd.ChildNodes)
                            {
                                if (nv.LocalName.ToUpper().Trim() == "SERVIDOR")
                                {
                                    foreach (XmlNode sv in nv.ChildNodes)
                                    {
                                        if (sv.LocalName.Trim().ToUpper() == "INSTANCIA")
                                        {
                                            Sistema.Instancia = sv.InnerText.Trim();
                                        }
                                        if (sv.LocalName.Trim().ToUpper() == "CATALOGO")
                                        {
                                            Sistema.BaseDatos = sv.InnerText.Trim();
                                        }
                                    }
                                }

                                if (nv.LocalName.ToUpper().Trim() == "IDEQUIPO")
                                {
                                    Sistema.IdEquipo= nv.InnerText.Trim();
                                }

                                if (nv.LocalName.ToUpper().Trim() == "DATALOCAL") 
                                {
                                    _DataLocal = nv.InnerText.Trim();
                                }

                                if (nv.LocalName.ToUpper().Trim()=="IMPRESORATICKET")
                                {
                                    foreach (XmlNode mi in nv.ChildNodes)
                                    {
                                        if (mi.LocalName.Trim().ToUpper() == "TAMANOROLLO")
                                        {
                                            if (mi.InnerText.Trim().ToUpper() == "G")
                                            {
                                                //Sistema.ImpresoraTicket = Sistema.EnumModoRolloTicket.Grande;
                                            }                                            
                                            if (mi.InnerText.Trim().ToUpper() == "P")
                                            {
                                                //Sistema.ImpresoraTicket = Sistema.EnumModoRolloTicket.Pequeno;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.Result =  OOB.Resultado.Enumerados.EnumResult.isError;
                result.Mensaje = e.Message;
            }

            return result;
        }

    }

}
