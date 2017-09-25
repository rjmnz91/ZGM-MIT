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
    public partial class CargosDetalle :  CLS.Cls_Session 
    {
        #region VARIABLES

        private struct Navegacion
        {
            public const string Primero = "<<";
            public const string Anterior = "<";
            public const string Siguiente = ">";
            public const string Ultimo = ">>";
        }

        private struct Fila
        {
            public const int Talla = 0;
            public const int CantIntroducida = 1;
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

        //Id del cargo
        private Nullable<int> idCargo
        {
            get
            {
                if (Session["idCargo"] != null)
                    return Convert.ToInt32(Session["idCargo"].ToString());
                else
                    return null;
            }
            set
            {
                Session["idCargo"] = value;
            }
        }

        //Id del tienda destino
        private String idTiendaDestino
        {
            get
            {
                if (Session["idTiendaDestino"] != null)
                    return Session["idTiendaDestino"].ToString();
                else
                    return null;
            }
            set
            {
                Session["idTiendaDestino"] = value;
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

        private DataTable dtCargo= new DataTable();
        private DataTable dtArticuloActual = new DataTable();
        private bool bExisteArticulo = false;
        private DataTable dtDistinctCargo = new DataTable();

        #endregion
       
        /// <summary>
        /// Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //En la carga inicial recogemos el valor del cargo de salida creado
            if (idCargo == null)
                idCargo = Convert.ToInt32(Request.QueryString["IdCargo"].ToString());
            
            //En la carga inicial recogemos el valor del cargo de salida creado
            if (idTiendaDestino == null)
                idTiendaDestino = Request.QueryString["idTiendaDestino"].ToString();

            //Establecimiento del título de la página
            this.Title = Resources.Resource.DetalleCargo;

            //Si se viene de la pagina de selección de producto
            if (!Page.IsPostBack && Request.QueryString["IdTiendaDestino"] == null)
            {
                ObtenerCargo();

                //Si se viene de la página de selección de artículo y este no está aún
                //agregado al cargo se añade, de modo temporal, un registro más a los
                //contadores. En caso de que se pulsase volver no se incrementará el 
                //contador
                if (!bExisteArticulo)
                {
                    if (Request.QueryString["IdArticulo"] != null)
                    {
                        lblActual.Text = (dtDistinctCargo.Rows.Count + 1).ToString();
                        lblTotal.Text = (dtDistinctCargo.Rows.Count + 1).ToString();
                    }
                    //En caso contrario se asigna el número de registros por defecto
                    else
                    {
                        CargarDatosCargoInicial();
                        lblActual.Text = (dtDistinctCargo.Rows.Count).ToString();
                        lblTotal.Text = (dtDistinctCargo.Rows.Count).ToString();
                    }
                }
            }
            else
                CargarDatosCargoInicial();
        }

        /// <summary>
        /// Busqueda de productos que coincidan con el texto introducido en la correspondiente 
        /// caja de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Realizaremos el correspondiente guardado
            RealizarGuardado(false);


            if (txtBusquedaCargo.Text.IndexOf("*") > 0)
            {   //habilitamos la busqueda por talla
                String StrArticulo;
                String StrTalla;

                StrArticulo = txtBusquedaCargo.Text.Split('*')[0].ToString();
                StrTalla = txtBusquedaCargo.Text.Split('*')[1].ToString();

                Response.Redirect(Constantes.Paginas.EleccionProducto + "?Filtro=" + StrArticulo  + "&Talla=" + StrTalla);
                
            }
            else
            {

                Response.Redirect(Constantes.Paginas.EleccionProducto + "?Filtro=" + txtBusquedaCargo.Text);

            }

           
        }

        /// <summary>
        /// Salir de la página de pedido de entrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Eliminación del cargo
            sdsBorrarCargo.DeleteParameters["IdCargo"].DefaultValue = idCargo.ToString();

            //Obtención de las tallas para el producto y tienda indicados
            sdsBorrarCargo.Delete();

            //Se borran los datos de sesión
            idCargo = null;
            idArticulo = null;
            idTiendaDestino = string.Empty;
            
            //Redirigimos a la página de inicio
            Response.Redirect(Constantes.Paginas.Inicio);
        }

        /// <summary>
        /// Ver la imagen del producto actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFoto_Click(object sender, EventArgs e)
        {
            RealizarGuardado(false);

            if (idArticulo != null)
                Response.Redirect(Constantes.Paginas.Foto + "?IdArticulo=" + idArticulo);
        }

        /// <summary>
        /// Borrado de un artículo de un cargo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            sdsBorrarArticuloCargo.DeleteParameters["IdCargo"].DefaultValue = idCargo.ToString();
            sdsBorrarArticuloCargo.DeleteParameters["IdArticulo"].DefaultValue = idArticulo.ToString();

            //Eliminación del artículo
            sdsBorrarArticuloCargo.Delete();

            //Se inicializa el valor del idArtídulo
            idArticulo = null;

            //Obtención del inventario tras la eliminación
            ObtenerCargo();

            EstablecerNavegacion(Navegacion.Ultimo);
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
                RealizarGuardado(true);

            if (idCargo != null)
            {
                //Se finaliza la entrada
                sdsFinalizarCargo.UpdateParameters["IdCargo"].DefaultValue = idCargo.ToString();
                sdsFinalizarCargo.UpdateParameters["Usuario"].DefaultValue = Contexto.Usuario;
                sdsFinalizarCargo.Update();
            }

            //Se borran los datos de sesión
            idCargo = null;
            idArticulo = null;
            idTiendaDestino = string.Empty;

            //Redirigimos a la página de inventario
            Response.Redirect(Constantes.Paginas.Inicio);
        }

        /// <summary>
        /// Navegación al primer producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrimero_Click(object sender, EventArgs e)
        {
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
            TableRow trCantidadIntro = new TableRow();
            DataTable dtCargoTemp = dtCargo;
            DataView dvCargoTemp = new DataView();
            int contadorInventariado = 0;

            //Se limpian las filas de la tabla
            this.tblTallas.Rows.Clear();

            for (int i = 0; i < dtProdTallas.Rows.Count; i++)
            {
                //Fila con las tallas
                TableHeaderCell tTalla = new TableHeaderCell();
                tTalla.CssClass = "celdaTalla";
                tTalla.Text = dtProdTallas.Rows[i]["Talla"].ToString();
                trCabecera.Cells.Add(tTalla);

                //Fila con las cantidades introducidas
                TableCell tCantidadIntro = new TableCell();
                tCantidadIntro.CssClass = "celdaCantidadIntro";
                TextBox txtCantidadIntro = new TextBox();
                txtCantidadIntro.BorderWidth = new Unit(0);
                txtCantidadIntro.CssClass = "celdaCantidadIntroTxt";
                txtCantidadIntro.Font.Bold = true;

                //Comprobamos que ya tenga algún artículo guardado el cargo
                if (dtCargoTemp.Rows.Count > 0)
                {
                    dtCargoTemp.DefaultView.RowFilter = "Id_Cabecero_Detalle = " + dtProdTallas.Rows[i]["Id_Cabecero_Detalle"].ToString() + " AND IdArticulo = " + idArticulo.ToString();

                    if (dtCargoTemp.DefaultView.Count > 0)
                        txtCantidadIntro.Text = dtCargoTemp.DefaultView[0]["Cantidad"].ToString();
                    else
                        txtCantidadIntro.Text = string.Empty;
                }
                else
                    txtCantidadIntro.Text = string.Empty;

                //Si se buscó un EAN, recibiré un valor de IdCabeceroDetalle por querystring. Si la talla coincide con este 
                //IdCabeceroDetalle recibido, sumo 1 unidad a la cantidad introducida
                if (Request.QueryString[Constantes.QueryString.IdCabeceroDetalle] != null &&
                    Request.QueryString[Constantes.QueryString.IdCabeceroDetalle].ToString() == dtProdTallas.Rows[i]["Id_Cabecero_Detalle"].ToString() 
                    && !Page.IsPostBack)
                {
                    if (txtCantidadIntro.Text == string.Empty)
                        txtCantidadIntro.Text = "1";
                    else
                        txtCantidadIntro.Text = (Convert.ToInt32(txtCantidadIntro.Text) + 1).ToString();
                }

                txtCantidadIntro.Attributes.Add("onchange", "forzarInteger(this); CalcularTotales();");
                tCantidadIntro.Controls.Add(txtCantidadIntro);
                trCantidadIntro.Cells.Add(tCantidadIntro);

                if(txtCantidadIntro.Text != string.Empty)
                    contadorInventariado += Convert.ToInt32(txtCantidadIntro.Text);
            }

            //Añadimos la columna de totales
            //Celda de cabecera
            TableHeaderCell totalCabecera = new TableHeaderCell();
            totalCabecera.CssClass = "celdaTalla";
            totalCabecera.Text = Resources.Resource.Total;
            trCabecera.Cells.Add(totalCabecera);
            //Celda de la cantidad introducida
            TableCell totalCantidadIntro = new TableCell();
            totalCantidadIntro.CssClass = "celdaCantidadIntro";
            totalCantidadIntro.Text = contadorInventariado.ToString();
            trCantidadIntro.Cells.Add(totalCantidadIntro);

            //Se añaden las filas a la tabla
            tblTallas.Rows.Add(trCabecera);
            tblTallas.Rows.Add(trCantidadIntro);

            //Se deshabilitan las filas con las tallas y con el número de unidades incluidas 
            //que en teoría componen el pedido
            tblTallas.Rows[Fila.Talla].Enabled = false;

            //Se cargan los datos del cargo
            CargarDatosCargo();
        }

        /// <summary>
        /// Busqueda del inventario actual
        /// </summary>
        private void ObtenerCargo()
        {
            //Obtención del cargo
            sdsObtenerCargo.SelectParameters["IdCargo"].DefaultValue = idCargo.ToString();

            //Obtención del pedido de entrada
            dtCargo = ((DataView)sdsObtenerCargo.Select(new DataSourceSelectArguments())).ToTable();

            //Establecimiento de los totales
            EstablecerTotales();

            //Se evalua la existencia del artículo en el cargo actual si se viene de la página de selección de
            //artículo y ya hay alguno asignado al cargo
            if ((!Page.IsPostBack) && (Request.QueryString["IdArticulo"] != null) && (dtCargo.Rows.Count >0))
                bExisteArticulo = EvaluarExisteProducto();

            //Si se viene de la pantalla de selección de artículos, no se ha seleccionado ninguno y existe
            //ya algún artículo asignado al cargo este último es seleccionado
            if (Request.QueryString["IdArticulo"] == null && dtCargo.Rows.Count > 0)
                    idArticulo = Convert.ToInt32(dtCargo.Rows[dtCargo.Rows.Count - 1]["IdArticulo"].ToString());
    
            ////Si se está trabajando sobre algún artículo en concreto se carga. En caso contrario nos
            ////posicionamos en el primer registro
            if (idArticulo != null)
                //Establecimiento del artículo actual y el número de registros
                EstablecerNavegacion(string.Empty);
            else
                //Establecimiento del artículo actual y el número de registros
                EstablecerNavegacion(Navegacion.Primero);
        }

        /// <summary>
        /// Obtención del artículo actual para realizar la correspondiente carga en el grid
        /// </summary>
        private void ObtenerDatosArticulo()
        {
            DataTable dtCargoTemp = new DataTable();

            if (!Page.IsPostBack && Request.QueryString["IdArticulo"] != null)
                idArticulo = Convert.ToInt32(Request.QueryString["IdArticulo"].ToString());

            if (idArticulo != null)
            {
                sdsObtenerCabeceros.SelectParameters["IdArticulo"].DefaultValue = idArticulo.ToString();

                dtCargoTemp = ((DataView)sdsObtenerCabeceros.Select(new DataSourceSelectArguments())).ToTable();

                //Obtención de las tallas para el producto y tienda indicados
                dtArticuloActual = dtCargoTemp.DefaultView.ToTable();

                //Se almacena el artículo en el ViewState
                dtTallasArticulo = dtArticuloActual;

                //Generación del Grid
                GenerarGrid(dtTallasArticulo);
            }
        }

        /// <summary>
        /// Proceso de guardado de artículo actual
        /// </summary>
        private void RealizarGuardado(bool blObtenerCargo)
        {
            //Recogemos el articulo de ViewState
            dtArticuloActual = dtTallasArticulo;
            int? id = idArticulo;
            string[] cantidades = ((HiddenField)form1.FindControl("hddCantidades")).Value.Split(';');


            //Evaluación de la existencia de artículos
            if (idArticulo != null)
            {
                for (int i = 0; i < cantidades.Length; i++)
                {
                    int valor;
                    if ((Int32.TryParse(cantidades[i].ToString(), out valor)))
                    {
                        sdsGuardarCargo.InsertParameters["IdCargo"].DefaultValue = idCargo.ToString();
                        sdsGuardarCargo.InsertParameters["IdArticulo"].DefaultValue = idArticulo.ToString();
                        sdsGuardarCargo.InsertParameters["Id_Cabecero_Detalle"].DefaultValue = dtArticuloActual.Rows[i]["Id_Cabecero_Detalle"].ToString();
                        sdsGuardarCargo.InsertParameters["Usuario"].DefaultValue = AVE.Contexto.Usuario;
                        sdsGuardarCargo.InsertParameters["Cantidad"].DefaultValue = cantidades[i].ToString();
                        
                        //Inserción del artículo
                        sdsGuardarCargo.Insert();
                    }
                }

                //Se obtendrá el inventario en el caso de que no seleccionemos salir del inventario
                if (blObtenerCargo)
                {
                    ObtenerCargo();

                    //Obtención de id del artículo actual
                    idArticulo = id;
                }
            }
        }

        /// <summary>
        /// Posicionamiento en función del registro en el que nos encontremos y de los registros que tenemos
        /// </summary>
        private void EstablecerNavegacion(string strAccionado)
        {
            sdsObtenerCargo.SelectParameters["IdCargo"].DefaultValue = idCargo.ToString();
            dtCargo = ((DataView)sdsObtenerCargo.Select(new DataSourceSelectArguments())).ToTable();

            //Se evalua que exista algún registro
            if (dtCargo.Rows.Count > 0)
            {
                dtDistinctCargo = dtCargo.DefaultView.ToTable(true, "IdArticulo");

                prodStockTotal = dtDistinctCargo.Rows.Count;

                switch (strAccionado)
                {
                    case Navegacion.Primero:
                        {
                            prodStockActual = 1;
                            idArticulo = Convert.ToInt32(dtDistinctCargo.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                            break;
                        }
                    case Navegacion.Anterior:
                        {
                            if (prodStockActual > 1)
                                prodStockActual--;
                            idArticulo = Convert.ToInt32(dtDistinctCargo.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                            break;
                        }
                    case Navegacion.Siguiente:
                        {
                            if (prodStockActual < prodStockTotal)
                            {
                                idArticulo = Convert.ToInt32(dtDistinctCargo.Rows[prodStockActual]["IdArticulo"].ToString());
                                prodStockActual++;
                            }
                            else //Por si nos encontramos en el último registro
                                idArticulo = Convert.ToInt32(dtDistinctCargo.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                            break;
                        }
                    case Navegacion.Ultimo:
                        {
                            prodStockActual = dtDistinctCargo.Rows.Count;

                            //Solamente se recogerá el ID del artículo del total de registros en caso de que
                            //no vengamos de la página de selección de artículos
                            if (Page.IsPostBack)
                                idArticulo = Convert.ToInt32(dtDistinctCargo.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                            break;
                        }
                    default:
                        {
                            if (idArticulo != null)
                            {
                                //Se recorren mediante bucle debido a que no se puede filtrar sonbre la tabla temporal
                                //para buscar una fila determinada al no tener las mismas columnas (está contiene sólo
                                //el idArticulo
                                for (int i = 0; i < dtDistinctCargo.Rows.Count; i++)
                                {
                                    if (dtDistinctCargo.Rows[i]["IdArticulo"].ToString() == idArticulo.ToString())
                                    {
                                        bExisteArticulo = true;
                                        prodStockActual = i;
                                    }
                                }
                                if (bExisteArticulo)
                                    prodStockActual++;
                                else
                                    prodStockActual = dtDistinctCargo.Rows.Count;
                            }
                            break;
                        }
                }

            //Actual producto sobre la totalidad de los mismos
            lblTotal.Text = prodStockTotal.ToString();
            lblActual.Text = prodStockActual.ToString();
            }

            if ((dtCargo.Rows.Count > 0) || (dtCargo.Rows.Count == 0 && !Page.IsPostBack))
                //Obtención de los articulos
                ObtenerDatosArticulo();
            else
                LimpiarDatos();
        }

        /// <summary>
        /// Limpieza de los campos cuando no existe ningún registro
        /// </summary>
        private void LimpiarDatos()
        {
            tblTallas.Rows.Clear();
            lblIdArticulo.Text = string.Empty;
            lblIdReferencia.Text = string.Empty;
            lblIdModelo.Text = string.Empty;
            lblTotal.Text = "0";
            lblActual.Text = "0";
        }

        /// <summary>
        /// Se establecen los valores de total pedido y total entrada
        /// </summary>
        private void EstablecerTotales()
        {
            if (dtCargo.DefaultView.ToTable().Compute("Sum(Cantidad)", String.Empty).ToString() != string.Empty)
                lblTotCargo.Text = ": " + Convert.ToInt32(dtCargo.DefaultView.ToTable().Compute("Sum(Cantidad)", String.Empty).ToString());
            else
                lblTotCargo.Text = ": 0";
        }

        /// <summary>
        /// Carga de los datos correspondientes al cargo
        /// </summary>
        private void CargarDatosCargo()
        {
            //Se añade el valor de la tienda origen, tienda destino,número de cargo, IdArticulo
            lblIdTiendaOrigen.Text = ": " + Contexto.IdTienda;
            lblIdTiendaDestino.Text = ": " + idTiendaDestino.ToString();
            lblIdArticulo.Text = ": " + idArticulo.ToString();
            lblIdNCargo.Text = ": " + idCargo.ToString();

            //Obtención de los datos del modelo y la referencia
            DataTable dtArticuloTemp;

            sdsObtenerArticuloDetalle.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            sdsObtenerArticuloDetalle.SelectParameters["IdArticulo"].DefaultValue = idArticulo.ToString();

            dtArticuloTemp = ((DataView)sdsObtenerArticuloDetalle.Select(new DataSourceSelectArguments())).ToTable();

            lblIdReferencia.Text = ": " + dtArticuloTemp.Rows[0]["Referencia"].ToString();
            lblIdModelo.Text = ": " + dtArticuloTemp.Rows[0]["ModeloProveedor"].ToString();
        }

        /// <summary>
        /// Carga de los datos correspondientes al cargo
        /// </summary>
        private void CargarDatosCargoInicial()
        {
            //Se añade el valor de la tienda origen, tienda destino,número de cargo, IdArticulo
            lblIdTiendaOrigen.Text = ": " + Contexto.IdTienda;
            lblIdTiendaDestino.Text = ": " + idTiendaDestino.ToString();
            lblIdNCargo.Text = ": " + idCargo.ToString();
        }

        /// <summary>
        /// Cuando se agrega un nuevo artículo se evalúa que ese producto existe o no ya en el cargo.
        /// </summary>
        private bool EvaluarExisteProducto()
        {
            DataTable dtCargoTemp = dtCargo;
            dtCargoTemp.DefaultView.RowFilter = "IdArticulo = " + Request.QueryString["IdArticulo"].ToString();

            //Se evalua que el artículo ya exista 
            if (dtCargoTemp.DefaultView.Count > 0)
            {
                //Si es así recogemos se Id
                idArticulo = Convert.ToInt32(dtCargoTemp.DefaultView[0]["IdArticulo"].ToString());
                return true;
            }
            else
            {
                //En caso contrario se recoge el Id que nos viene por QueryString
                lblActual.Text = (dtDistinctCargo.Rows.Count + 1).ToString();
                lblTotal.Text = (dtDistinctCargo.Rows.Count + 1).ToString();
                idArticulo = Convert.ToInt32(Request.QueryString["IdArticulo"].ToString());
                return false;
            }
        }

        protected void sdsGuardarCargo_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

    }
}
