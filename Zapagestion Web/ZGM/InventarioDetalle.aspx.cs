using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AVE
{
    public partial class InventarioDetalle :  CLS.Cls_Session 
    {
        #region VARIABLES

        private struct Navegacion
        {
            public const string Primero = "<<";
            public const string Anterior = "<";
            public const string Siguiente = ">";
            public const string Ultimo = ">>";
        }

        private struct BloquesFiltro
        {
            public const string Total = "0";
            public const string Nuevo = "1";
        }

        //Este no se puede guardar en ViewState porque al pasar a la página de buscar producto se perdería
        private string bloque 
        { 
            get 
            {
                if (Session["Bloque"] != null)
                    return Session["Bloque"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                Session["Bloque"] = value; 
            }
        }

        //Total de artículos
        private int prodStockTotal
        {
            get
            {
                return Convert.ToInt32(Session["prodStockTotal"].ToString());
            }
            set
            {
                Session["prodStockTotal"] = value;
            }
        }

        //Artículo actual sobre el total de artículos
        private int prodStockActual
        {
            get
            {
                return Convert.ToInt32(Session["prodStockActual"].ToString());
            }
            set
            {
                Session["prodStockActual"] = value;
            }
        }

        //Inventario actual
        private Nullable<int> idInventario
        {
            get
            {
                if (Session["idInventario"] != null)
                    return Convert.ToInt32(Session["idInventario"].ToString());
                else
                    return null;
            }
            set
            {
                Session["idInventario"] = value;
            }
        }

        //Inventario actual
        public Nullable<int> idOrdenInventario
        {
            get
            {
                if (Session["idOrdenInventario"] != null)
                    return Convert.ToInt32(Session["idOrdenInventario"].ToString());
                else
                    return null;
            }
            set
            {
                Session["idOrdenInventario"] = value;
            }
        }

        //Id del artículo actual
        private Nullable<int> idArticulo
        {
            get
            {
                if (Session["idArticulo"] != null)
                    return Convert.ToInt32(Session["idArticulo"].ToString());
                else
                    return null;
            }
            set
            {
                Session["idArticulo"] = value;
            }
        }

        //Tallas del artículo actual
        private DataTable dtTallasArticulo
        {
            get
            {
                if (ViewState["dtTallasArticulo"] != null)
                    return ((DataTable)ViewState["dtTallasArticulo"]);
                else
                    return null;
            }
            set
            {
                ViewState["dtTallasArticulo"] = value;
            }
        }

        private DataTable dtInventario = new DataTable();
        private DataTable dtInventarioDetalle = new DataTable();
        private DataTable dtArticulo = new DataTable();
        private Nullable<int> idCabeceroDetalle = null;
        private bool blNuevoBloque = false;
        private int totalProductos;
        private int modoActual;

        #endregion
       
      
        /// <summary>
        /// Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Establecimiento del título de la página
            this.Title = Resources.Resource.DetalleInventario;

            if (!Page.IsPostBack)
            {
                //Si se viene de la página de selección de artículo y ha sido seleccionado alguno
                //se realiza el guardado de modo previo a la carga de productos sólo si el producto no existe ya en el inventario
                if (Request.QueryString[Constantes.QueryString.IdArticulo] != null)
                {
                    //Si estamos trabajando con una OI, debemos comprobar si el articulo cumple con el perímetro de la OI. Si no es así
                    //no se puede incorporar al inventario y hay que indicarlo al usuario.
                    //Así, seguimos si no hay OI (!idOrdenInventario.HasValue) o si habíendola el artículo cumple.
                    if (!idOrdenInventario.HasValue || ArticuloValidoEnPerimetroOI(idOrdenInventario.Value, Request.QueryString[Constantes.QueryString.IdArticulo]))
                    {
                        //Obtención del IdArticulo de la querystring
                        idArticulo = Convert.ToInt32(Request.QueryString[Constantes.QueryString.IdArticulo].ToString());

                        ObtenerDatosArticulo();

                        //Comprobamos si el producto está en el inventario. Si no existe, hacemos el guardado con sus datos en blanco.
                        sdsInventariosPendientesExisteArticulo.SelectParameters["IdInventario"].DefaultValue = idInventario.ToString();
                        sdsInventariosPendientesExisteArticulo.SelectParameters["Bloque"].DefaultValue = bloque;
                        sdsInventariosPendientesExisteArticulo.SelectParameters["IdArticulo"].DefaultValue = idArticulo.ToString();

                        DataView dv = (DataView)sdsInventariosPendientesExisteArticulo.Select(new DataSourceSelectArguments());
                        if (dv.Count > 0 && !(bool)(dv[0][0]))
                            RealizarGuardado(true);
                    }
                    else
                    {
                        string script = "alert('" + Resources.Resource.ArticuloFueraPerimetro + "');";
                        ClientScript.RegisterClientScriptBlock(typeof(string), string.Empty, script, true);
                    }
                }

                //Obtención del inventario para el empleado, tienda y terminal actual
                ObtenerInventario();
            }

            //Habilitamos o deshabilitamos el boton de guardar y borrar
            if (ddlBloques.SelectedValue == BloquesFiltro.Total)
            {
                btnGuardar.Enabled = false;
                btnBorrar.Enabled = false;
                btnBuscar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
                btnBorrar.Enabled = true;
                btnBuscar.Enabled = true;
            }
        }

        //Comprueba si el artículo está en el perímetro de la OI y devuelve un bool que lo indica.
        private bool ArticuloValidoEnPerimetroOI(int idOrdenInventario, string idArticulo)
        {
            sdsOrdenInventarioEsArticuloEnPerimetro.SelectParameters["IdOrdenInventario"].DefaultValue = idOrdenInventario.ToString();
            sdsOrdenInventarioEsArticuloEnPerimetro.SelectParameters["IdArticulo"].DefaultValue = idArticulo;

            DataView dv = (DataView)sdsOrdenInventarioEsArticuloEnPerimetro.Select(DataSourceSelectArguments.Empty);
            return (bool)dv[0][0];
        }

        /// <summary>
        /// Busqueda de productos que coincidan con el texto introducido en la correspondiente 
        /// caja de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Se evalua que existe algún valor de bloque
            if (txtBloque.Visible)
            {
                if (txtBloque.Text != string.Empty)
                {
                    bloque = txtBloque.Text;
                    Response.Redirect(Constantes.Paginas.EleccionProducto + "?Filtro=" + txtBusquedaProducto.Text);
                }
                else
                {
                    string script = "alert('" + Resources.Resource.BloqueObligatorio + "');";
                    ClientScript.RegisterClientScriptBlock(typeof(string), string.Empty, script, true);
                }
            }
            else
            {
                //Realizaremos el correspondiente guardado
                RealizarGuardado(false);

                bloque = ddlBloques.Text;
                Response.Redirect(Constantes.Paginas.EleccionProducto + "?Filtro=" + txtBusquedaProducto.Text);
            }
            
        }

        /// <summary>
        /// Se quita el producto actual del carrusel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            //Obtenemos el artículo.
            if (lblIdArticulo.Text.Replace(":", "") != String.Empty)
                idArticulo = Convert.ToInt32(lblIdArticulo.Text.Replace(":", "")); 

            sdsArticuloEliminar.DeleteParameters["IdInventario"].DefaultValue = idInventario.ToString();
            sdsArticuloEliminar.DeleteParameters["Bloque"].DefaultValue = bloque;
            sdsArticuloEliminar.DeleteParameters["IdArticulo"].DefaultValue = idArticulo.ToString();

            //Eliminación del artículo
            sdsArticuloEliminar.Delete();

            //Se inicializa el valor del idArtídulo
            idArticulo = null;

            //Obtención del inventario tras la eliminación
            ObtenerInventario();

            //Si no existe ningún registro borramos los datos
            if(dtInventarioDetalle.Rows.Count == 0)
                LimpiarDatos();

            EstablecerNavegacion(Navegacion.Ultimo);
        }

        /// <summary>
        /// Salir del inventario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            if (ddlBloques.SelectedValue != BloquesFiltro.Total)
                //Proceso de guardado de artículo actual
                RealizarGuardado(false);

            //Se inicializa el valor del idArtídulo
            idArticulo = null;

            //Redirigimos a la página de inventario
            Response.Redirect(Constantes.Paginas.Inventario);
        }

        /// <summary>
        /// Ver la imagen del producto actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFoto_Click(object sender, EventArgs e)
        {
            if(ddlBloques.SelectedValue != BloquesFiltro.Total)
                //Si seleccionado un producto realizaremos el correspondiente guardado
                RealizarGuardado(false);

            if(idArticulo != null)
                Response.Redirect(Constantes.Paginas.Foto + "?IdArticulo=" + idArticulo);
        }

        /// <summary>
        /// Porceso de guardado del inventario actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idArticulo != null && ddlBloques.SelectedValue != BloquesFiltro.Total)
                RealizarGuardado(true);
            else if (idArticulo == null)
            {
                string script = "alert('" + Resources.Resource.ArticuloObligatorio + "');";
                ClientScript.RegisterClientScriptBlock(typeof(string), string.Empty, script, true);
            }
            else
            {
                string script = "alert('" + Resources.Resource.TotalSelec + "');";
                ClientScript.RegisterClientScriptBlock(typeof(string), string.Empty, script, true);
            }
        }

        /// <summary>
        /// Inserción del formulario pendiente a la tabla histórica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            //Evaluación de la existencia de artículos
            if (idArticulo != null)
            {
                if (ddlBloques.SelectedValue != BloquesFiltro.Total)
                    //Proceso de guardado de artículo actual
                    RealizarGuardado(true);

                sdsInventariosPendientesFinalizar.InsertParameters["IdInventario"].DefaultValue = idInventario.ToString();
                sdsInventariosPendientesFinalizar.InsertParameters["Usuario"].DefaultValue = Contexto.Usuario;
                sdsInventariosPendientesFinalizar.InsertParameters["IdOrdenInventario"].DefaultValue = idOrdenInventario.ToString();
                sdsInventariosPendientesFinalizar.InsertParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
                sdsInventariosPendientesFinalizar.InsertParameters["ObservacionesTienda"].DefaultValue = hddComentariosOI.Value;

                //Ejecución del proceso Finalizar
                sdsInventariosPendientesFinalizar.Insert();
                
                //Se inicializa el valor del idArtídulo
                idArticulo = null;
            }
            //Redirigimos a la página de inventario
            Response.Redirect(Constantes.Paginas.Inventario);
        }

        /// <summary>
        /// Navegación al primer producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrimero_Click(object sender, EventArgs e)
        {
            //Sólo se realiza el guardado si tenemos seleccionado
            //algún bloque que no sea total
            if(ddlBloques.SelectedValue != BloquesFiltro.Total)
                //Proceso de guardado de artículo actual
                RealizarGuardado(true);

            EstablecerNavegacion(Navegacion.Primero);
        }

        /// <summary>
        /// Navegación al producto anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (ddlBloques.SelectedValue != BloquesFiltro.Total)
                //Proceso de guardado de artículo actual
                RealizarGuardado(true);

            EstablecerNavegacion(Navegacion.Anterior);
        }

        /// <summary>
        /// Navegación al producto siguiente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (ddlBloques.SelectedValue != BloquesFiltro.Total)
                //Proceso de guardado de artículo actual
                RealizarGuardado(true);

            EstablecerNavegacion(Navegacion.Siguiente);
        }

        /// <summary>
        /// Navegación al último producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUltimo_Click(object sender, EventArgs e)
        {
            if (ddlBloques.SelectedValue != BloquesFiltro.Total)
                //Proceso de guardado de artículo actual
                RealizarGuardado(true);

            EstablecerNavegacion(Navegacion.Ultimo);
        }

        /// <summary>
        /// Generación del grid en función de las tallas existentes para el artículo definido
        /// </summary>
        /// <param name="dvProdTallas"></param>
        private void GenerarGrid(DataTable dtProdTallas)
        {
            //Declaración de las filas
            TableRow trCabecera = new TableRow();
            TableRow trCantidad = new TableRow();
            TableRow trCantidadIntro = new TableRow();
            TableRow trCantidadDif = new TableRow();
            int cantidad;
            int cantidadIntro;
            int contadorStock = 0;                  //Auxiliar para ir sumando el total de cantidades en stock
            int contadorInventariado = 0;           //Auxiliar para ir sumando el total de cantidades inventariadas

            //Se limpian las filas de la tabla
            this.tblTallas.Rows.Clear();

            for (int i = 0; i < dtProdTallas.Rows.Count; i++)
            {
                //Fila con las tallas
                TableHeaderCell tTalla = new TableHeaderCell();
                tTalla.CssClass = "celdaTalla";
                tTalla.Text = dtProdTallas.Rows[i]["Talla"].ToString();
                trCabecera.Cells.Add(tTalla);

                //Fila con las cantidades 
                TableCell tCantidad = new TableCell();
                tCantidad.CssClass = "celdaCantidad";
                tCantidad.Text = dtProdTallas.Rows[i]["Cantidad"].ToString() == "" ? tCantidad.Text = "0" : tCantidad.Text = dtProdTallas.Rows[i]["Cantidad"].ToString();
                trCantidad.Cells.Add(tCantidad);
                cantidad =Convert.ToInt32(tCantidad.Text);

                //Fila con las cantidades introducidas
                TableCell tCantidadIntro = new TableCell();
                tCantidadIntro.CssClass = "celdaCantidadIntro";
                TextBox txtCantidadIntro = new TextBox();
                txtCantidadIntro.BorderWidth = new Unit(0);
                txtCantidadIntro.CssClass = "celdaCantidadIntroTxt";
                
                txtCantidadIntro.Font.Bold = true;

                if (dtInventarioDetalle.Rows.Count > 0)
                {
                    //Obtención de la cantidad introducida
                    DataTable dt = dtInventarioDetalle.Copy();

                    //El filtrado se realiza por Artículo, Talla y Bloque en caso de que este seleccionado
                    //un bloque concreto en el combo. En caso contrario se filtrará sólo por IdArticulo e
                    //Id_Cabecero_Detalle
                    if (ddlBloques.SelectedValue == BloquesFiltro.Total)
                        dt.DefaultView.RowFilter = "IdArticulo = " + idArticulo + " AND Id_Cabecero_Detalle = " + dtProdTallas.Rows[i]["Id_Cabecero_Detalle"].ToString();
                    else
                        dt.DefaultView.RowFilter = "IdArticulo = " + idArticulo + " AND Id_Cabecero_Detalle = " + dtProdTallas.Rows[i]["Id_Cabecero_Detalle"].ToString() +
                        " AND Bloque = '" + bloque.ToString() + "'";

                    //Se realiza un sumatorio de las columnas cantidad existentes (esto se realiza para el supuesto
                    //de haber seleccionado mostrar las totalidad de los bloques y un producto se encuentra en varios.
                    //De este modo el valor de cantidad será el total de valores introducidos para dicha talla.
                    if (dt.DefaultView.Count > 0)
                        txtCantidadIntro.Text = dt.DefaultView.ToTable().Compute("Sum(Cantidad)", String.Empty).ToString();
                }
                txtCantidadIntro.Attributes.Add("onchange", "forzarInteger(this); CalcularDiferenciaInventario(" + i + "); CalcularTotales();");
                tCantidadIntro.Controls.Add(txtCantidadIntro);
                trCantidadIntro.Cells.Add(tCantidadIntro);

                //Si se buscó un EAN, recibiré un valor de IdCabeceroDetalle por querystring. Si la talla coincide con este 
                //IdCabeceroDetalle recibido, sumo 1 unidad a la cantidad introducida
                if (Request.QueryString[Constantes.QueryString.IdCabeceroDetalle] != null &&
                    Request.QueryString[Constantes.QueryString.IdCabeceroDetalle].ToString() == dtProdTallas.Rows[i]["Id_Cabecero_Detalle"].ToString()
                    && !Page.IsPostBack)
                {
                    if (txtCantidadIntro.Text == "")
                        txtCantidadIntro.Text = "1";
                    else
                        txtCantidadIntro.Text = (Convert.ToInt32(txtCantidadIntro.Text) + 1).ToString();
                }

                cantidadIntro = txtCantidadIntro.Text == "" ? cantidadIntro = 0 : cantidadIntro = Convert.ToInt32(txtCantidadIntro.Text);

                //Fila con la diferencia de cantidades
                TableCell tCantidadDif = new TableCell();
                tCantidadDif.CssClass = "celdaCantidadDif";
                tCantidadDif.Text = Convert.ToString(cantidadIntro - cantidad);
                trCantidadDif.Cells.Add(tCantidadDif);

                //Totales 
                contadorStock += cantidad;
                contadorInventariado += cantidadIntro;
            }

            //Añadimos la columna de totales
            //Celda de cabecera
            TableHeaderCell totalCabecera = new TableHeaderCell();
            totalCabecera.CssClass = "celdaTalla";
            totalCabecera.Text = Resources.Resource.Total;
            trCabecera.Cells.Add(totalCabecera);
            //Celda de stock
            TableCell totalCantidad = new TableCell();
            totalCantidad.CssClass = "celdaCantidad";
            totalCantidad.Text = contadorStock.ToString();
            trCantidad.Cells.Add(totalCantidad);
            //Celda de inventario
            TableCell totalCantidadIntro = new TableCell();
            totalCantidadIntro.CssClass = "celdaCantidadIntro";
            totalCantidadIntro.Text = contadorInventariado.ToString();
            trCantidadIntro.Cells.Add(totalCantidadIntro);
            //Celda de diferencia
            TableCell totalCantidadDif = new TableCell();
            totalCantidadDif.CssClass = "celdaCantidadDif";
            totalCantidadDif.Text = (contadorInventariado - contadorStock).ToString();
            trCantidadDif.Cells.Add(totalCantidadDif);

            //Se añaden las filas a la tabla
            tblTallas.Rows.Add(trCabecera);
            tblTallas.Rows.Add(trCantidad);
            tblTallas.Rows.Add(trCantidadIntro);
            tblTallas.Rows.Add(trCantidadDif);

            //En el supuesto de encontrarnos en modo ciego no mostrarán las líneas de stock teórico 
            //ni la diferencia
            if (modoActual == Convert.ToInt32(ConfigurationManager.AppSettings["ModoCiego"].ToString()))
            {
                tblTallas.Rows[1].Visible = false;
                tblTallas.Rows[3].Visible = false;
            }

            if (dtProdTallas.Rows.Count > 0)
            {
                //Se añade el valor del IdArticulo, cabecero detalle y la referencia
                idCabeceroDetalle = Convert.ToInt32(dtProdTallas.Rows[0]["Id_Cabecero_Detalle"].ToString());
                idArticulo = Convert.ToInt32(dtProdTallas.Rows[0]["IdArticulo"].ToString());
                lblIdArticulo.Text = ": " + dtProdTallas.Rows[0]["IdArticulo"].ToString();
                lblIdReferencia.Text = ": " + dtProdTallas.Rows[0]["Referencia"].ToString();
            }

            //Si esta seleccionado en el combo de bloques ver la totalidad de los productos
            //se deshabilitará la fila de introducción de cantidades
            if (ddlBloques.SelectedValue == BloquesFiltro.Total)
                tblTallas.Rows[2].Enabled = false;
            else
                tblTallas.Rows[2].Enabled = true;
        }

        /// <summary>
        /// Busqueda del inventario actual
        /// </summary>
        private void ObtenerInventario()
        {
            //Obtención del IdInventario
            sdsObtenerInventario.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            sdsObtenerInventario.SelectParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
            sdsObtenerInventario.SelectParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal;
            dtInventario = ((DataView)sdsObtenerInventario.Select(new DataSourceSelectArguments())).ToTable();

            //Se recoge el IdOrdenInventario si lo hubiera
            idOrdenInventario = dtInventario.Rows[0].Field<int?>("IdOrdenInventario");

            //Se recoge el modo de visualización del inventario actual
            modoActual = Convert.ToInt32(dtInventario.Rows[0]["IdTipoVista"].ToString());

            //En el supuesto de encontrarnos en modo ciego no mostrarán las líneas de stock teórico 
            //ni la diferencia siempre y cuando exista alguna talla
            if (modoActual == Convert.ToInt32(ConfigurationManager.AppSettings["ModoCiego"].ToString()) && tblTallas.Rows.Count>0)
            {
                tblTallas.Rows[1].Visible = false;
                tblTallas.Rows[3].Visible = false;
            }

            //Se establece el título de la página
            this.Title = Resources.Resource.DetalleInventario  + 
                            " (" + dtInventario.Rows[0]["TipoInventario"].ToString() + ")";

            //Se obtiene el id de inventario
            idInventario = Convert.ToInt32(dtInventario.Rows[0]["IdInventario"].ToString());

            //Obtención del detalle del inventario
            sdsObtenerInventarioDetalle.SelectParameters["IdInventario"].DefaultValue = idInventario.ToString();
            dtInventarioDetalle = ((DataView)sdsObtenerInventarioDetalle.Select(new DataSourceSelectArguments())).ToTable();

            //Si aún no se ha asignado ningún bloque se coge el primero de la totalidad
            //de registros que tenos
            if (bloque == "" && dtInventarioDetalle.Rows.Count > 0)
            {
                bloque = dtInventarioDetalle.Rows[0]["Bloque"].ToString();
                idArticulo = Convert.ToInt32(dtInventarioDetalle.Rows[0]["IdArticulo"].ToString());
            }
            totalProductos = dtInventarioDetalle.DefaultView.ToTable(true,"IdArticulo").Rows.Count;

            //Obtención de los bloques
            CargarBloques();

            //Si se está trabajando sobre algún artículo en concreto se carga. En caso contrario nos
            //posicionamos en el primer registro
            if (idArticulo != null)
                //Establecimiento del artículo actual y el número de registros
                EstablecerNavegacion(string.Empty);
            else
                //Establecimiento del artículo actual y el número de registros
                EstablecerNavegacion(Navegacion.Primero);
        }

        /// <summary>
        /// Selección de bloque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBloques_SelectedIndexChanged(object sender, EventArgs e)
        {
            RealizarGuardado(true);

            switch (ddlBloques.SelectedValue)
            {
                //Mostrado de la totalidad de productos
                case BloquesFiltro.Total:
                    {
                        blNuevoBloque = false;
                        EstablecerNavegacion(Navegacion.Primero);
                        break;
                    }
                //Selección de creación de un nuevo bloque
                case BloquesFiltro.Nuevo:
                    {
                        blNuevoBloque = true;
                        lblUP.Text = "0";
                        EstablecerEstadoCmbBloques();
                        LimpiarDatos();
                        break;
                    }
                //Fitro por bloque
                default:
                    {
                        bloque = ddlBloques.Text;
                        EstablecerNavegacion(Navegacion.Primero);
                        break;
                    } 
            
            }
        }

        /// <summary>
        /// Obtención del artículo actual para realizar la correspondiente carga en el grid
        /// </summary>
        private void ObtenerDatosArticulo()
        {
            sdsStockInventarioObtener.SelectParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;
            sdsStockInventarioObtener.SelectParameters["IdArticulo"].DefaultValue = idArticulo.ToString();

            //Obtención de las tallas para el producto y tienda indicados
            dtArticulo = ((DataView)sdsStockInventarioObtener.Select(new DataSourceSelectArguments())).ToTable();
            
            //Se almacena el artículo en el ViewState
            dtTallasArticulo = dtArticulo;

            //Generación del Grid
            GenerarGrid(dtTallasArticulo);
        }

        /// <summary>
        /// Proceso de guardado de artículo actual
        /// </summary>
        private void RealizarGuardado( bool blObtenerInventario)
        {
            //Recogemos el articulo de ViewState
            dtArticulo = dtTallasArticulo;
            int? id = idArticulo;

            //Evaluación de la creación de un nuevo bloque
            if (txtBloque.Visible)
            {
                //Comprobación de que se ha introducido algún valor
                if (txtBloque.Text != string.Empty)
                {
                    bloque = txtBloque.Text;
                    blNuevoBloque = false;
                }
                else
                {
                    string script = "alert('" + Resources.Resource.BloqueObligatorio + "');";
                    ClientScript.RegisterClientScriptBlock(typeof(string), string.Empty, script, true);
                }
                //Establecimiento de la visibilidad de los controles de bloques
                EstablecerEstadoCmbBloques();
            }
            else
            {
                if (Page.IsPostBack)
                    //En caso de introducirse un nombre es asignado
                    bloque = ddlBloques.Text;
                else
                    ddlBloques.Text = bloque;
            }

            //Evaluación de la existencia de artículos
            if (idArticulo != null)
            {
                for (int i = 0; i < dtArticulo.Rows.Count; i++)
                {
                    sdsInventariosPendientesGuardarDetalle.InsertParameters["IdInventario"].DefaultValue = idInventario.ToString();
                    sdsInventariosPendientesGuardarDetalle.InsertParameters["Bloque"].DefaultValue = bloque;
                    sdsInventariosPendientesGuardarDetalle.InsertParameters["IdArticulo"].DefaultValue = idArticulo.ToString();
                    sdsInventariosPendientesGuardarDetalle.InsertParameters["Id_Cabecero_Detalle"].DefaultValue = dtArticulo.Rows[i]["Id_Cabecero_Detalle"].ToString();
                    
                    //Si no se introdujo ningún valor en el campo de unidades inventariadas se procederá a guardar una cadena de
                    //texto vacia que posteriormente el valor de cantidad en BBDD sea nulo 
                    if (((HiddenField)form1.FindControl("hddCantidades")).Value == string.Empty)
                        sdsInventariosPendientesGuardarDetalle.InsertParameters["Cantidad"].DefaultValue = string.Empty;
                    else
                    {
                        string strCelda = ((HiddenField)form1.FindControl("hddCantidades")).Value.Split(';')[i];
                        int valor;

                        //Si la celda está vacia o contiene un valor no válido (como texto)  el valor será una
                        //cadena vacia
                        if (strCelda == string.Empty || !(Int32.TryParse(strCelda, out valor)))
                            sdsInventariosPendientesGuardarDetalle.InsertParameters["Cantidad"].DefaultValue = string.Empty;
                        else
                            sdsInventariosPendientesGuardarDetalle.InsertParameters["Cantidad"].DefaultValue = strCelda;
                   }

                    sdsInventariosPendientesGuardarDetalle.InsertParameters["Usuario"].DefaultValue = AVE.Contexto.Usuario;

                    //Inserción del artículo
                    sdsInventariosPendientesGuardarDetalle.Insert();
                }

                //Se obtendrá el inventario en el caso de que no seleccionemos salir del inventario
                if (blObtenerInventario)
                {
                    ObtenerInventario();

                    //Obtención de id del artículo actual
                    idArticulo = id;
                }

                EstablecerNavegacion(string.Empty);

                //Ya se ha guardado el registro
                blNuevoBloque = false;

                EstablecerEstadoCmbBloques();
            }
        }

        /// <summary>
        /// Posicionamiento en función del registro en el que nos encontremos y de los registros que tenemos
        /// </summary>
        private void EstablecerNavegacion(string strAccionado)
        {
            DataTable dtInventarioDetalleTemp;

            dtInventarioDetalle = ((DataView)sdsObtenerInventarioDetalle.Select(new DataSourceSelectArguments())).ToTable();
            
            //Se comprueba que se puede realizr un sumatorio de cantidades
            if (dtInventarioDetalle.DefaultView.ToTable().Compute("Sum(Cantidad)", String.Empty).ToString() != string.Empty)
                totalProductos = Convert.ToInt32(dtInventarioDetalle.DefaultView.ToTable().Compute("Sum(Cantidad)", String.Empty).ToString());
            else
                totalProductos = 0;

            if (ddlBloques.SelectedValue == BloquesFiltro.Total && Page.IsPostBack)
                dtInventarioDetalleTemp = dtInventarioDetalle.DefaultView.ToTable(true, "idArticulo");
            else
            {
                //Establecimiento del total de artículos
                if(ddlBloques.SelectedValue != BloquesFiltro.Total)
                    dtInventarioDetalle.DefaultView.RowFilter = "Bloque = '" + bloque + "'";

                dtInventarioDetalleTemp = dtInventarioDetalle.DefaultView.ToTable(true, "idArticulo");
            }

            prodStockTotal = dtInventarioDetalleTemp.Rows.Count;
            lblTotal.Text = prodStockTotal.ToString();

            //Uno o ningún registro
            if (dtInventarioDetalleTemp.Rows.Count == 0 || dtInventarioDetalleTemp.Rows.Count == 1)
            {
                prodStockActual = dtInventarioDetalleTemp.Rows.Count; 
                lblActual.Text = prodStockActual.ToString();

                //Si existe 1 realizarmos la correspondiente carga
                if (dtInventarioDetalle.DefaultView.ToTable(true, "IdArticulo").Rows.Count == 1)
                {
                    //Solamente se recogerá el ID del artículo del total de registros en caso de que
                    //no vengamos de la página de selección de artículos
                    if(Page.IsPostBack || (!Page.IsPostBack && Request.QueryString["IdArticulo"] == null))
                        idArticulo = Convert.ToInt32(dtInventarioDetalleTemp.Rows[0]["IdArticulo"].ToString());
                    
                    ObtenerDatosArticulo();
                }     
            }
            //Navegación por el carrusel de producos
            else
            {
                switch (strAccionado)
                {
                    case Navegacion.Primero:
                        {
                            prodStockActual = 1;
                            idArticulo = Convert.ToInt32(dtInventarioDetalleTemp.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                            break;
                        }
                    case Navegacion.Anterior:
                        {
                            if (prodStockActual > 1)
                                prodStockActual--;
                            idArticulo = Convert.ToInt32(dtInventarioDetalleTemp.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                            break;
                        }
                    case Navegacion.Siguiente:
                        {
                            if (prodStockActual < prodStockTotal)
                            {
                                idArticulo = Convert.ToInt32(dtInventarioDetalleTemp.Rows[prodStockActual]["IdArticulo"].ToString());
                                prodStockActual++;
                            }
                            else //Por si nos encontramos en el último registro
                                idArticulo = Convert.ToInt32(dtInventarioDetalleTemp.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                            break;
                        }
                    case Navegacion.Ultimo:
                        {
                            prodStockActual = dtInventarioDetalleTemp.Rows.Count;
                            
                            //Solamente se recogerá el ID del artículo del total de registros en caso de que
                            //no vengamos de la página de selección de artículos
                            if(Page.IsPostBack)
                                idArticulo = Convert.ToInt32(dtInventarioDetalleTemp.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                            break;
                        }
                    default:
                        {
                            if (idArticulo != null)
                            {
                                //Se recorren mediante bucle debido a que no se puede filtrar sonbre la tabla temporal
                                //para buscar una fila determinada al no tener las mismas columnas (está contiene sólo
                                //el idArticulo
                                for (int i = 0; i < dtInventarioDetalleTemp.Rows.Count; i++)
                                {
                                    if (dtInventarioDetalleTemp.Rows[i]["IdArticulo"].ToString() == idArticulo.ToString())
                                        prodStockActual = i;
                                }
                                //idArticulo = Convert.ToInt32(drFilaGuardada[0]["IdArticulo"].ToString());
                                prodStockActual++;

                            } break;
                        }
                        
                }
                    //Actual producto sobre la totalidad de los mismos
                    lblActual.Text = prodStockActual.ToString();
                    
                    //Obtención de los articulos
                    ObtenerDatosArticulo();
             }

            
            //Obtención de la totalidad de artículos y del total de artículos para ese bloque
            lblUT.Text = ": " + totalProductos.ToString();

            //Si nos encontramos en modo Total
            if(ddlBloques.SelectedValue == BloquesFiltro.Total)
                lblUP.Text = ": " + totalProductos.ToString();
            else
            {
            dtInventarioDetalle.DefaultView.RowFilter = "Bloque = '" + bloque + "'";

            //Se comprueba que se puede realizr un sumatorio de cantidades
            if (dtInventarioDetalle.DefaultView.ToTable().Compute("Sum(Cantidad)", String.Empty).ToString() != string.Empty)
                lblUP.Text = ": " + dtInventarioDetalle.DefaultView.ToTable().Compute("Sum(Cantidad)", String.Empty).ToString();
            else
                lblUP.Text = ": 0";
            }
        }
        /// <summary>
        /// Limpieza de los campos cuando no existe ningún registro
        /// </summary>
        private void LimpiarDatos()
        {
            tblTallas.Rows.Clear();
            lblIdArticulo.Text = string.Empty;
            lblIdReferencia.Text = string.Empty;
        }

        /// <summary>
        /// Carga de los diferentes bloques al combo
        /// </summary>
        private void CargarBloques()
        {
            ddlBloques.Items.Clear();

            DataTable dtBloques = new DataTable();
            ddlBloques.Items.Insert(0, new ListItem(Resources.Resource.Total, "0"));
            ddlBloques.Items.Insert(1, new ListItem(Resources.Resource.NuevoBloque, "1"));

            //Si venimos de la creación del inventario
            if (!Page.IsPostBack && Request.QueryString["IdArticulo"] == null && dtInventarioDetalle.Rows.Count == 0)
            {
                bloque = ConfigurationManager.AppSettings["BloqueInicial"].ToString();
                ddlBloques.Items.Insert(2, new ListItem(bloque, bloque));
                ddlBloques.Text = bloque;
            }

            //Se realiza la ordenación por bloques
            dtInventarioDetalle.DefaultView.Sort = "Bloque";
            dtBloques = dtInventarioDetalle.DefaultView.ToTable(true, "Bloque");

            foreach (DataRow dr in dtBloques.Rows)
                ddlBloques.Items.Insert(ddlBloques.Items.Count, new ListItem(dr["Bloque"].ToString(), dr["Bloque"].ToString()));

            //Si existe algún artículo asociado al bloque este es es asociado al bloque
            if (dtInventarioDetalle.DefaultView.Count > 0)
            {
                //Comprobamos que exista algún artículo del último bloque seleccionado para
                //si es así seleccionar dicho bloque y en caso contrario seleccionar en el combo
                //como valor Total
                DataRow[] drFilaBloque = dtInventarioDetalle.Select(@"Bloque= '" + bloque + "'");

                if (drFilaBloque.Length > 0)
                    ddlBloques.SelectedValue = bloque;
            }
            else if (Page.IsPostBack && dtInventarioDetalle.Rows.Count == 0)
            {
                btnGuardar.Enabled = false;
                btnBorrar.Enabled = false;
                btnBuscar.Enabled = false;
            }
        }

        /// <summary>
        /// Se evalua si se esta creando o no un nuevo bloque
        /// </summary>
        private void EstablecerEstadoCmbBloques()
        {
            if (blNuevoBloque)
            {
                txtBloque.Visible = true;
                ddlBloques.Visible = false;
                lblActual.Text = "0";
                lblTotal.Text = "0";

            }
            else
            {
                txtBloque.Visible = false;
                ddlBloques.Visible = true;
            }
        }
    }
}
