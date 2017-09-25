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
    public partial class CargosEntrada :  CLS.Cls_Session 
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
            public const int CantTeorica = 2;
        }

        //Total de artículos
        private int prodStockTotal
        {
            get
            {
                return Convert.ToInt32(ViewState["prodStockTotal"].ToString());
            }
            set
            {
                ViewState["prodStockTotal"] = value;
            }
        }

        //Artículo actual sobre el total de artículos
        private int prodStockActual
        {
            get
            {
                return Convert.ToInt32(ViewState["prodStockActual"].ToString());
            }
            set
            {
                ViewState["prodStockActual"] = value;
            }
        }

        //Id del artículo actual
        private Nullable<int> idArticulo
        {
            get
            {
                if (ViewState["idArticulo"] != null)
                    return Convert.ToInt32(ViewState["idArticulo"].ToString());
                else
                    return null;
            }
            set
            {
                ViewState["idArticulo"] = value;
            }
        }

        //Id del artículo actual
        private Nullable<int> idCargo
        {
            get
            {
                if (ViewState["idCargo"] != null)
                    return Convert.ToInt32(ViewState["idCargo"].ToString());
                else
                    return null;
            }
            set
            {
                ViewState["idCargo"] = value;
            }
        }

        //Id del artículo actual
        private Nullable<int> idEntrada
        {
            get
            {
                if (ViewState["idEntrada"] != null)
                    return Convert.ToInt32(ViewState["idEntrada"].ToString());
                else
                    return null;
            }
            set
            {
                ViewState["idEntrada"] = value;
            }
        }

        //Id del tienda destino
        private String idTiendaDestino
        {
            get
            {
                if (ViewState["idTiendaDestino"] != null)
                    return ViewState["idTiendaDestino"].ToString();
                else
                    return null;
            }
            set
            {
                ViewState["idTiendaDestino"] = value;
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
        
        private DataTable dtCargo = new DataTable();

        private DataTable dtArticuloActual = new DataTable();

        #endregion
       
        /// <summary>
        /// Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Establecimiento del título de la página
            this.Title = Resources.Resource.EntradaCargo;

            //Se define el rango del validador del campo de búsqueda
            rnvPedido.MinimumValue = "0";
            rnvPedido.MaximumValue = Int32.MaxValue.ToString();
        }

        /// <summary>
        /// Busqueda de productos que coincidan con el texto introducido en la correspondiente 
        /// caja de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Se obtienen, si los hubiese, los datos del cargo
                ObtenerCargoAccesoInicial();

                //Se establece el estado de los controles
                bool blCargoEncontrado = dtArticuloActual.Rows.Count > 0 ? blCargoEncontrado = true : blCargoEncontrado = false;
                EstablecerControles(blCargoEncontrado);

                if (!blCargoEncontrado)
                {
                    string script = "alert('" + Resources.Resource.NoCargoEntrada + "');";
                    ClientScript.RegisterClientScriptBlock(typeof(string), string.Empty, script, true);
                }
            }
        }

        /// <summary>
        /// Salir de la página de pedido de entrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Eliminación del cargo de entrada
            sdsBorrarCargoEntrada.DeleteParameters["IdEntrada"].DefaultValue = idEntrada.ToString();

            //Obtención de las tallas para el producto y tienda indicados
            sdsBorrarCargoEntrada.Delete();

            //Se borran los datos de sesión
            idCargo = null;
            idEntrada = null;
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
        /// Inserción del formulario pendiente a la tabla histórica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            //Evaluación de la existencia de artículos
            if (idArticulo != null)
                RealizarGuardado(true);

            if (idEntrada != null)
            {
                //Se finaliza la entrada
                sdsFinalizarEntrada.UpdateParameters["IdEntrada"].DefaultValue = idEntrada.ToString();
                sdsFinalizarEntrada.UpdateParameters["Usuario"].DefaultValue = AVE.Contexto.Usuario;
                sdsFinalizarEntrada.Update();
            }

            //Se borran los datos de sesión
            //Se borran los datos de sesión
            idCargo = null;
            idEntrada = null;
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
            TableRow trCantidad = new TableRow();
            int contadorIntroducida = 0;
            int contadorCantidad = 0;
            int cantidad;

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
                tCantidad.Text = dtProdTallas.Rows[i]["CantidadCargo"].ToString() == "" ? tCantidad.Text = "0" : tCantidad.Text = dtProdTallas.Rows[i]["CantidadCargo"].ToString();
                trCantidad.Cells.Add(tCantidad);
                cantidad = Convert.ToInt32(tCantidad.Text);

                //Fila con las cantidades introducidas
                TableCell tCantidadIntro = new TableCell();
                tCantidadIntro.CssClass = "celdaCantidadIntro";
                TextBox txtCantidadIntro = new TextBox();
                txtCantidadIntro.BorderWidth = new Unit(0);
                txtCantidadIntro.CssClass = "celdaCantidadIntroTxt";
                txtCantidadIntro.Font.Bold = true;
                txtCantidadIntro.Text = dtProdTallas.Rows[i]["CantidadEntrada"].ToString();
                txtCantidadIntro.Attributes.Add("onchange", "forzarInteger(this); CalcularTotales();");
                tCantidadIntro.Controls.Add(txtCantidadIntro);
                trCantidadIntro.Cells.Add(tCantidadIntro);

                if (txtCantidadIntro.Text != string.Empty)
                    contadorIntroducida += Convert.ToInt32(txtCantidadIntro.Text);

                contadorCantidad += cantidad;
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
            totalCantidadIntro.Text = contadorIntroducida.ToString();
            trCantidadIntro.Cells.Add(totalCantidadIntro);
            //Celda de cantidad
            TableCell totalCantidad = new TableCell();
            totalCantidad.CssClass = "celdaCantidad";
            totalCantidad.Text = contadorCantidad.ToString();
            trCantidad.Cells.Add(totalCantidad);

            //Se añaden las filas a la tabla
            tblTallas.Rows.Add(trCabecera);
            tblTallas.Rows.Add(trCantidadIntro);
            tblTallas.Rows.Add(trCantidad);

            //Se deshabilitan las filas con las tallas y con el número de unidades incluidas 
            //que en teoría componen el pedido
            tblTallas.Rows[Fila.Talla].Enabled = false;
            tblTallas.Rows[Fila.CantTeorica].Enabled = false;

            //Se evalua si existe algún registro
            if (dtProdTallas.Rows.Count > 0)
            {
                //Se añade el valor del IdArticulo, IdReferencia, IdCargo, IdModelo, tienda origen y tienda destino
                idArticulo = Convert.ToInt32(dtProdTallas.Rows[0]["IdArticulo"].ToString());
                lblIdArticulo.Text = ": " + idArticulo.ToString();
                lblIdReferencia.Text = ": " + dtProdTallas.Rows[0]["Referencia"].ToString();
                lblIdNCargo.Text = ": " + txtBusquedaCargo.Text;
                lblIdModelo.Text = ": " + dtProdTallas.Rows[0]["Modelo"].ToString();
                lblIdTiendaOrigen.Text = ": " + dtProdTallas.Rows[0]["IdTiendaOrigen"].ToString();
                lblIdTiendaDestino.Text = ": " + dtProdTallas.Rows[0]["IdTiendaDestino"].ToString();
            }
        }

        /// <summary>
        /// Se obtienen los datos del cargo en el primer acceso a la página
        /// </summary>
        private void ObtenerCargoAccesoInicial()
        {
            //Obtención del cargo
            sdsObtenerCargoPrimerAcceso.SelectParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;
            sdsObtenerCargoPrimerAcceso.SelectParameters["IdCargo"].DefaultValue = txtBusquedaCargo.Text;

            dtCargo = ((DataView)sdsObtenerCargoPrimerAcceso.Select(new DataSourceSelectArguments())).ToTable();

            if (dtCargo.Rows.Count > 0)
            {
                //Creación de la entrada del cargo
                sdsCrearCargoEntrada.InsertParameters["IdCargo"].DefaultValue = txtBusquedaCargo.Text;
                sdsCrearCargoEntrada.InsertParameters["FechaEntrada"].DefaultValue = DateTime.Today.ToShortDateString();
                sdsCrearCargoEntrada.InsertParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;
                sdsCrearCargoEntrada.InsertParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
                sdsCrearCargoEntrada.InsertParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal;
                sdsCrearCargoEntrada.InsertParameters["Usuario"].DefaultValue = AVE.Contexto.Usuario;

                sdsCrearCargoEntrada.Insert();

                //Obtención del IdEntrada
                sdsObtenerCargoEntrada.SelectParameters["IdEntrada"].DefaultValue = idEntrada.ToString();

                //Obtención del cargo de entrada
                dtCargo = ((DataView)sdsObtenerCargoEntrada.Select(new DataSourceSelectArguments())).ToTable();

                //Establecimiento de los totales
                EstablecerTotales();

                //Establecimiento del artículo actual y el número de registros
                EstablecerNavegacion(Navegacion.Primero);
            }
        }



        /// <summary>
        /// Busqueda del cargo actual
        /// </summary>
        private void ObtenerCargo()
        {
            //Obtención del IdEntrada
            sdsObtenerCargoEntrada.SelectParameters["IdEntrada"].DefaultValue = idEntrada.ToString();

            //Obtención del cargo de entrada
            dtCargo = ((DataView)sdsObtenerCargoEntrada.Select(new DataSourceSelectArguments())).ToTable();

            //Establecimiento de los totales
            EstablecerTotales();
        }

        /// <summary>
        /// Obtención del artículo actual para realizar la correspondiente carga en el grid
        /// </summary>
        private void ObtenerDatosArticulo()
        {
            DataTable dtCargoTemp = dtCargo;
            dtCargoTemp.DefaultView.RowFilter = "IdArticulo = " + idArticulo;

            //Obtención de las tallas para el producto y tienda indicados
            dtArticuloActual = dtCargoTemp.DefaultView.ToTable();

            //Se almacena el artículo en el ViewState
            dtTallasArticulo = dtArticuloActual;

            //Generación del Grid
            GenerarGrid(dtTallasArticulo);
        }

        /// <summary>
        /// Proceso de guardado de artículo actual.
        /// </summary>
        /// <param name="blObtenerCargo">Indica
        /// si tras realizar el guardado se vuelven
        /// a cargar los datos del cargo (navegación) o
        /// si por el contrario no se carga nada (al 
        /// finalizar y cancelar)</param>
        private void RealizarGuardado(bool blObtenerCargo)
        {
            //Recogemos el articulo de ViewState
            dtArticuloActual = dtTallasArticulo;
            int? id = idArticulo;


            //Evaluación de la existencia de artículos
            if (idArticulo != null)
            {
                for (int i = 0; i < dtArticuloActual.Rows.Count; i++)
                {
                    sdsGuardarDetalleCargoEntrada.InsertParameters["IdEntrada"].DefaultValue = idEntrada.ToString();
                    sdsGuardarDetalleCargoEntrada.InsertParameters["IdArticulo"].DefaultValue = idArticulo.ToString();
                    sdsGuardarDetalleCargoEntrada.InsertParameters["Id_Cabecero_Detalle"].DefaultValue = dtArticuloActual.Rows[i]["Id_Cabecero_Detalle"].ToString();
                    sdsGuardarDetalleCargoEntrada.InsertParameters["Usuario"].DefaultValue = AVE.Contexto.Usuario;

                    //Si no se introdujo ningún valor en el campo de unidades inventariadas se procederá a guardar una cadena de
                    //texto vacia que posteriormente el valor de cantidad en BBDD sea nulo 
                    if (((HiddenField)form1.FindControl("hddCantidades")).Value == string.Empty)
                        sdsGuardarDetalleCargoEntrada.InsertParameters["Cantidad"].DefaultValue = string.Empty;
                    else
                    {
                        string strCelda = ((HiddenField)form1.FindControl("hddCantidades")).Value.Split(';')[i];
                        int valor;

                        //Si la celda está vacia o contiene un valor no válido (como texto)  el valor será una
                        //cadena vacia
                        if (strCelda == string.Empty || !(Int32.TryParse(strCelda, out valor)))
                            sdsGuardarDetalleCargoEntrada.InsertParameters["Cantidad"].DefaultValue = string.Empty;
                        else
                            sdsGuardarDetalleCargoEntrada.InsertParameters["Cantidad"].DefaultValue = strCelda;
                    }

                    //Inserción del artículo
                    sdsGuardarDetalleCargoEntrada.Insert();
                }

                //Se obtendrá el cargo en el caso de que no seleccionemos salir del cargo
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
            DataTable dtDistinctProdCargo = new DataTable();

            //Obtenemos los diferentes artículos de la session
            dtDistinctProdCargo = dtCargo.DefaultView.ToTable(true, "IdArticulo");

            prodStockTotal = dtDistinctProdCargo.Rows.Count;

            switch (strAccionado)
            {
                case Navegacion.Primero:
                    {
                        prodStockActual = 1;
                        idArticulo = Convert.ToInt32(dtDistinctProdCargo.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                        break;
                    }
                case Navegacion.Anterior:
                    {
                        if (prodStockActual > 1)
                            prodStockActual--;
                        idArticulo = Convert.ToInt32(dtDistinctProdCargo.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                        break;
                    }
                case Navegacion.Siguiente:
                    {
                        if (prodStockActual < prodStockTotal)
                        {
                            idArticulo = Convert.ToInt32(dtDistinctProdCargo.Rows[prodStockActual]["IdArticulo"].ToString());
                            prodStockActual++;
                        }
                        else //Por si nos encontramos en el último registro
                            idArticulo = Convert.ToInt32(dtDistinctProdCargo.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                        break;
                    }
                case Navegacion.Ultimo:
                    {
                        prodStockActual = dtDistinctProdCargo.Rows.Count;

                        //Solamente se recogerá el ID del artículo del total de registros en caso de que
                        //no vengamos de la página de selección de artículos
                        if (Page.IsPostBack)
                            idArticulo = Convert.ToInt32(dtDistinctProdCargo.Rows[prodStockActual - 1]["IdArticulo"].ToString());
                        break;
                    }
                default:
                    {
                        if (idArticulo != null)
                        {
                            //Se recorren mediante bucle debido a que no se puede filtrar sonbre la tabla temporal
                            //para buscar una fila determinada al no tener las mismas columnas (está contiene sólo
                            //el idArticulo
                            for (int i = 0; i < dtDistinctProdCargo.Rows.Count; i++)
                            {
                                if (dtDistinctProdCargo.Rows[i]["IdArticulo"].ToString() == idArticulo.ToString())
                                    prodStockActual = i;
                            }
                            prodStockActual++;
                        }
                        break;
                    }
            }

            //Actual producto sobre la totalidad de los mismos
            lblTotal.Text = prodStockTotal.ToString();
            lblActual.Text = prodStockActual.ToString();

            //Obtención de los articulos
            ObtenerDatosArticulo();

        } 

        /// <summary>
        /// Se establece el estado de los controles en función de si fue o no
        /// encontrado el pedido indicado
        /// </summary>
        /// <param name="blPedidoEncontrado"></param>
        private void EstablecerControles(bool blPedidoEncontrado)
        {
            BtnFoto.Enabled = blPedidoEncontrado;
            BtnCopiar.Enabled = blPedidoEncontrado;
            btnFinalizar.Enabled = blPedidoEncontrado;
            btnPrimero.Enabled = blPedidoEncontrado;
            btnAnterior.Enabled = blPedidoEncontrado;
            btnSiguiente.Enabled = blPedidoEncontrado;
            btnUltimo.Enabled = blPedidoEncontrado;
            btnBuscar.Enabled = !blPedidoEncontrado;
            txtBusquedaCargo.Enabled = !blPedidoEncontrado;
        }

        /// <summary>
        /// Obtención del IdEntrada del nuevo cargo creado. Generado en el primer acceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void sdsCrearCargoEntrada_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            idEntrada = Convert.ToInt32(e.Command.Parameters["@IdEntrada"].Value);
        }

        /// <summary>
        /// Se establecen los valores de total cargo y total entrada
        /// </summary>
        private void EstablecerTotales()
        {
            if (dtCargo.DefaultView.ToTable().Compute("Sum(CantidadCargo)", String.Empty).ToString() != string.Empty)
                lblTotCargo.Text = ": " + Convert.ToInt32(dtCargo.DefaultView.ToTable().Compute("Sum(CantidadCargo)", String.Empty).ToString());
            else
                lblTotCargo.Text = ": 0";

            //Obtención de todas las unidades que, para todas las tallas y productos del pedido, 
            //el usuario ha indicado que se han recibido
            if (dtCargo.DefaultView.ToTable().Compute("Sum(CantidadEntrada)", String.Empty).ToString() != string.Empty)
                lblTotEntrada.Text = ": " + dtCargo.DefaultView.ToTable().Compute("Sum(CantidadEntrada)", String.Empty).ToString();
            else
                lblTotEntrada.Text = ": 0";
        }
    }
}
