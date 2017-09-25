using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AVE.controles
{

   public partial class UCEleccionProducto : System.Web.UI.UserControl
    {
        #region VARIABLES
        public EleccionProducto EP;
      
        public string IdArticulo {
            get { return ST0.IdArticulo;}
        }
        public string Talla 
        {
            get{return AVE_ArticuloBuscarLike.SelectParameters["StrTalla"].DefaultValue;}
            set { AVE_ArticuloBuscarLike.SelectParameters["StrTalla"].DefaultValue = value; }
        }
        public string Where
        {
            get { return AVE_ArticuloBuscarLike.SelectParameters["Filtro"].DefaultValue; }
            set { AVE_ArticuloBuscarLike.SelectParameters["Filtro"].DefaultValue = value; }
        }
        //Total de artículos
         public string returnUrl
        {
            get
            {
                return ViewState["returnUrl"].ToString();
            }
            set
            {
                ViewState["returnUrl"] = value;
            }

        }
       
        private struct columnasGrid
        {
            public const int ID = 3;
            public const int CodigoAlfa = 4;
            public const int EAN = 5;
            public const int Modelo = 6;
            public const int Descripcion = 7;
        }
                
        String StrTalla = String.Empty;

        #endregion

     
        public void cargar(EleccionProducto ep)
        {
            string uri = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] cadena = uri.Split('?');
            string[] filters = cadena[1].Split('&');
            string[] value = filters[0].Split('=');
            lblResult.Text = "Busqueda: " + value[1].ToUpper();
            ST0.EP = ep;  
            AVE_ArticuloBuscarLike.DataBind();
                        
        }
        /// <summary>
        /// Evento producido al seleccionar un elemento de la lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdListaProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Obteción del idArticulo para mostrar la información que corresponda 
           MostrarStock(Convert.ToInt32(grdListaProductos.DataKeys[grdListaProductos.SelectedIndex].Value.ToString()));
           
        }

        /// <summary>
        /// Controlamos el evento Selected del SDS para recoger los parámetros de salida. Si están vacíos se deja que se muestre el 
        /// grid con los resultados; si tenemos un idArticulo es porque solo hay un producto que coincida y debemos devolverlo 
        /// directamente por querystring; si tenemos idCabeceroDetalle también debemos incluirlo en la query.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AVE_ArticuloBuscarLike_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            string direccion;
            int idArticulo;
            int idCabeceroDetalle;

            int.TryParse(e.Command.Parameters["@IdArticulo"].Value.ToString(), out idArticulo);
            int.TryParse(e.Command.Parameters["@IdCabeceroDetalle"].Value.ToString(), out idCabeceroDetalle);
            //Si ha devuelto un idCabeceroDetalle, lo incluímos
            if (idCabeceroDetalle != 0) ST0.idCabeceroDetalle = idCabeceroDetalle;
            MostrarStock(idArticulo);
           
         
        }

        private void MostrarStock(int idArticulo) {
          
            if (idArticulo != 0)
            {
                ST0.IdArticulo = idArticulo.ToString();
                Stock.Visible = true;
                if (grdListaProductos.Rows.Count != 0) btnVolver.Visible = true; 
                Resultado.Visible = false;
                ST0.CargarStock();
               
            }
            else
            {
                Stock.Visible = false;
                btnVolver.Visible = false;  
            }
        }

        public void Volver()
        {
            if (grdListaProductos.Rows.Count != 0)
            {
                btnVolver.Visible = false;
                Stock.Visible = false;
                Resultado.Visible = true;

            }
            else { ST0.CargarStock(); }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            String Talla;
            String Seccion;
            String Marca;
            String Corte;
            String Material;
            String Color;
            String Script;

            Talla = ddlTalla.SelectedItem.ToString();
            Seccion = ddlSeccion.SelectedItem.ToString();
            Marca = ddlMarca.SelectedItem.ToString();
            Corte = ddlCorte.SelectedItem.ToString();
            Material = ddlMaterial.SelectedItem.ToString();
            Color = ddlColor.SelectedItem.ToString();

            if (Talla.Length != 0 || Seccion.Length != 0 || Marca.Length != 0 || Corte.Length != 0 || Material.Length != 0 || Color.Length != 0)
            {
                Estadisticas.InsertarBusqueda("", Talla, Contexto.Usuario, Contexto.IdTerminal, Seccion, Marca, Corte, Material, Color, txtComentario.Text);

                ddlTalla.SelectedIndex = 0;
                ddlSeccion.SelectedIndex = 0;
                ddlMarca.SelectedIndex = 0;
                ddlCorte.SelectedIndex = 0;
                ddlMaterial.SelectedIndex = 0;
                ddlColor.SelectedIndex = 0;
                txtComentario.Text = "";

                Script = "alert('Se ha registrado la petición de un articulo no disponible en Stock en todas las Tiendas.');";

               // ClientScript.RegisterStartupScript(typeof(string), "MensajeGrabar", Script, true);

                //txtBusquedaProducto.Focus();
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Volver(); 
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.Inicio);
        }
      
    }
}