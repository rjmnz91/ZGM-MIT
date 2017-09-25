using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using DLLGestionVenta.CapaDatos;

namespace AVE.controles
{
    public partial class UCStockEnTienda : System.Web.UI.UserControl
    {
        public EleccionProducto EP;
        //Total de artículos
        private string _IdArticulo
        {
            get
            {
                if (ViewState["_IdArticulo"] != null)
                    return ViewState["_IdArticulo"].ToString();
                else
                    return null;
            }
            set
            {
               // Session["IdArt"] = value;
                ViewState["_IdArticulo"] = value;
            }

        }

        private Boolean _Complementario
        {
            get
            {
                if (ViewState["_Complementario"] != null)
                    return Boolean.Parse(ViewState["_Complementario"].ToString());
                else
                    return false;
            }
            set
            {
                ViewState["_Complementario"] = value;
            }

        }
        private string valors
        {
            get
            {

                return ViewState["valors"] != null ? ViewState["valors"].ToString() : Session["valors"].ToString();
            }
            set
            {
                Session["valors"] = value;
                ViewState["valors"] = value;
            }

        }
        public int idCabeceroDetalle;
        public Boolean Complementario
        {
            get { return _Complementario; }
            set
            {
                _Complementario = value;
                Detalle.complementario(value);
            }
        }

        public string IdArticulo
        {
            get { return _IdArticulo; }
            set
            {
                _IdArticulo = value;
                AVE_ArticuloFotoObtener.SelectParameters["IdArticulo"].DefaultValue = value;
                AVE_StockEnTiendaObtener.SelectParameters["IdArticulo"].DefaultValue = value;
                SqlDataTallajes.SelectParameters["IdArticulo"].DefaultValue = value;
                SqlDataModColores.SelectParameters["IdArticulo"].DefaultValue = value;
                Detalle.IdArticulo = value;

                // MJM 26/03/2014 INICIO
                AVE_StockEnTiendaComplementario.SelectParameters["IdArticulo"].DefaultValue = value;
                AVE_StockEnTiendaComplementario.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
                AVE_StockEnTiendaComplementario.SelectParameters["Tipo"].DefaultValue = "C";
                AVE_StockEnTiendaComplementario.DataBind();
                repComplementos.DataBind();
                AVE_StockEnTiendaSustitutivo.SelectParameters["IdArticulo"].DefaultValue = value;
                AVE_StockEnTiendaSustitutivo.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
                AVE_StockEnTiendaSustitutivo.SelectParameters["Tipo"].DefaultValue = "S";
                AVE_StockEnTiendaSustitutivo.DataBind();
                repSustitutosMini.DataBind();
                // MJM 26/03/2014 FIN

                EvaluarEnlaces(_IdArticulo);
            }
        }

        struct IndexColumnas
        {
            internal const int Articulo = 1;
            internal const int Color = 2;
            internal const int InicioTallas = 3;
        }

        struct IndexColumnasStock
        {
            internal const int IdTienda = 0;
            internal const int Tienda = 1;
            internal const int InicioTallas = 2;
        }

        //Total de artículos
        private string returnUrl
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

        int IdPedido;
        int IdCarro;

        //User y pass ALMACEN CENTRAL PIAGUI (Keys AppSettings)

        private string UserACPiaguiAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UserACPiagui"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["UserACPiagui"].ToString();
                else
                    return "ERROR";
            }
        }

        private string PassACPiaguiAppSettings
        {
            get
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["PassACPiagui"].ToString()))
                    return System.Configuration.ConfigurationManager.AppSettings["PassACPiagui"].ToString();
                else
                    return "ERROR";
            }
        }

        private const string ALMACEN_CENTRAL = "ALMACEN CENTRAL";

        protected void Page_Load(object sender, EventArgs e)
        {


            //Registramos en javascript el control de la caja de texto para usarlo desde otras funciones jscript;
            string script = "var Talla_Selecionada = document.getElementById('" + hiddenTallas.ClientID + "');";
            script += "var Talla_SelecionadaNo = document.getElementById('" + hiddenTallasNo.ClientID + "');";
            Page.ClientScript.RegisterStartupScript(typeof(string), "Talla_Selecionada", script, true);
            btnFoto.Visible = false;
            if (Page.IsPostBack)
            {
                CargarGridStock();
                if (!string.IsNullOrEmpty(IdArticulo)) CargarColoresModelo();
            }

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "GridStock", "<script> $(document).ready( function() { MakeStaticHeader('" + GridStock.ClientID + "', 150, 900 , 15 ,false); }); </script>", false);

        }

        // MJM 26/03/2014 INICIO

        protected int StockDeArticulo(string idArticulo, bool TiendaPropia)
        {
            int iRet = 0;
            try
            {
                CapaDatos.ClsCapaDatos objDatos = new CapaDatos.ClsCapaDatos();
                objDatos.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ConnectionString;

                if (TiendaPropia)
                {
                    iRet = objDatos.StockArticuloEnTienda(idArticulo, Contexto.IdTienda);
                }
                else
                {
                    iRet = objDatos.StockArticuloOtrasTiendas(idArticulo, Contexto.IdTienda);
                }
            }
            catch (Exception ex)
            {
                iRet = 0;
            }
            finally
            {
                iRet = (iRet < 0 ? 0 : iRet);
            }
            return iRet;
        }

        // MJM 26/03/2014 FIN 

        public void CargarStock()
        {
            DataView dv = null;

            // Obtenemos la información del producto
            if (IdArticulo != null)
            {
                //Búsqueda normal
                AVE_StockEnTiendaObtener.SelectParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;
                dv = (DataView)AVE_StockEnTiendaObtener.Select(new DataSourceSelectArguments());

                FotoArticulo.ImageUrl = this.ResolveUrl(ObtenerURL());
                if (dv != null && dv.Count > 0)
                {
                    //// Almacenamos toda la información del producto en dtProductos
                    DataTable dtProductos = dv.Table;
                    btnComplementos.CommandArgument = IdArticulo;
                    // Escribimos en las labels, la descripción y el precio
                    lblDescripcion.Text = dtProductos.Rows[0]["Descripcion"].ToString();

                    // MJM 2|/02/2014- INICIO
                    decimal nPrecioActual = Decimal.Parse(dtProductos.Rows[0]["Precio"].ToString());
                    //lblPrecioValor.Text = string.Format(" Precio : {0:C}", Decimal.Parse(dtProductos.Rows[0]["Precio"].ToString()));
                    lblPrecioValor.Text = string.Format(" {0:C}", nPrecioActual);
                    // MJM 24/02/2014- FIN

                    hiddenTallas.Value = dtProductos.Rows[0]["Precio"].ToString();

                    // MJM 24/02/2014- INICIO
                    using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
                    {
                        try
                        {
                            SqlCommand command = new SqlCommand();
                            connection.Open();
                            command.Connection = connection;
                            command.CommandType = System.Data.CommandType.Text;
                            command.CommandText = string.Format("SELECT PRECIOVENTAEURO FROM ARTICULOS WHERE IDARTICULO={0}", IdArticulo);
                            SqlDataReader reader = command.ExecuteReader();


                            Decimal nPrecioDescuento = 0;
                            if (reader.Read())
                            {
                                nPrecioDescuento = decimal.Parse(reader["PRECIOVENTAEURO"].ToString());
                            }

                            // Si el precio actual es distinto hacemos visible el campo PRECIO ORIGINAL
                            if (nPrecioActual != nPrecioDescuento)
                            {
                                lblPrecioDescuento.Visible = (nPrecioDescuento != 0);
                                lblPrecioDescuento.Text = string.Format(" {0:C}", nPrecioDescuento);
                                lblPrecioDescuento.Attributes.Add("style", "text-decoration:line-through");
                                // MJM 18/03/2014 INICIO
                                if (nPrecioDescuento > 0)
                                {
                                    decimal porcentaje = 100 - Math.Round(((nPrecioActual * 100) / nPrecioDescuento), 2);
                                    lblPorcentajeDescuento.Text = string.Format(" -{0} %", porcentaje);
                                    lblPorcentajeDescuento.Visible = true;
                                }
                                // MJM 18/03/2014 FIN
                                lblPrecioValor.Attributes.Add("style", "margin-left:55px");
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            if (connection.State != System.Data.ConnectionState.Closed)
                                connection.Close();
                        }
                        connection.Close();
                    }



                    // MJM 24/02/2014- FIN

                    CargarNuevoGrid();

                    if (GridStock.Rows.Count > 0)
                    {
                        if (GridStock.Rows[0].Cells[0].Text.Equals(Contexto.IdTienda))
                        {
                            GridStock.SelectedIndex = 0;

                        }

                    }
                    Detalle.cargar();
                    CargarColoresModelo();


                }
            }
            else
            {

                this.lblDescripcion.Text = Resources.Resource.NoInfoProducto;
            }


        }

        protected void CargarColoresModelo()
        {
            DataView dv;
            DataView dvaux;
            int numfilas;
            dv = (DataView)SqlDataModColores.Select(new DataSourceSelectArguments());
            dvaux = dv.Table.Copy().DefaultView;


            Table Tabla = new Table();
            Tabla.ID = "TablaColores";
            numfilas = dv.Count + 1;

            dvaux.RowFilter = "idArticulo=" + IdArticulo;

            if (dv.Count > 0)
            {
                RadioButtonList option;
                TableCell Celda = new TableCell();
                Celda.Width = 325;
                TableRow Fila = new TableRow();
                option = new RadioButtonList();
                option.ID = dv[0]["modeloProveedor"].ToString();

                foreach (DataRowView dr in dv)
                {
                    option.Items.Add(new ListItem(dr["Color"].ToString(), dr["idArticulo"].ToString()));
                }

                option.SelectedValue = IdArticulo;
                option.SelectedIndexChanged += new EventHandler(RadioButtonList1_SelectedIndexChanged);
                option.AutoPostBack = true;
                Celda.VerticalAlign = VerticalAlign.Middle;
                Celda.HorizontalAlign = HorizontalAlign.Center;
                Celda.Controls.Add(option);
                Fila.Cells.Add(Celda);
                Tabla.Rows.Add(Fila);
            }
            PModelo.Controls.Clear();
            PModelo.Controls.Add(Tabla);

        }

        /// <summary>
        /// Procedimiento para eliminar las tallas que no se van a visualizar en el datatable
        /// </summary>
        /// <param name="dtTallas"></param>
        protected void RemoveTallas(ref DataTable dtTallas, Int32 inicio, Int32 Fin)
        {
            for (int i = Fin; i > inicio; i--)
            {
                dtTallas.Rows.Remove(dtTallas.Rows[i]);

            }
        }

        /// <summary>
        /// Función que busca el producto en el DataTable
        /// </summary>
        /// <param name="dt">DataTable donde buscamos</param>
        /// <param name="articulo">El artículo buscado</param>
        /// <returns>Retorna la fila donde se encuentra el producto</returns>
        protected int Esta(DataTable dt, string articulo)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Articulo"].ToString() == articulo)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Se produce cuando una fila de datos se enlaza a los datos de un control GridView. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MontarEnlacesGrid()
        {
            int aux;

            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HyperLink hl;
                    for (int i = 3; i < GridView2.Columns.Count; i++)
                    {
                        if (int.TryParse(row.Cells[i].Text, out aux) && aux > 0)
                        {
                            hl = new HyperLink();
                            hl.Text = row.Cells[i].Text;
                            hl.NavigateUrl = string.Format("Solicitud.aspx?IdArticulo={0}&Talla={1}&IdTienda={2}&Tienda={3}&Stock={4}",
                                            row.Cells[IndexColumnas.Articulo].Text,
                                            GridView2.Columns[i].HeaderText,
                                            AVE.Contexto.IdTienda,
                                            AVE.Contexto.NombreTienda,
                                            row.Cells[i].Text);
                            row.Cells[i].Controls.Add(hl);
                        }
                    }
                }
            }
            if (GridView2.Columns.Count == 4) GridView2.Columns.RemoveAt(GridStock.Columns.Count - 1);
        }

        // Al seleccionar una fila diferente, cogemos el valor del Producto
        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   EvaluarEnlaces();

        }

        protected void btnMasTiendas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StockOtras.aspx?IdArticulo=" + GridView2.SelectedValue.ToString());
        }

        protected void btnFoto_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Foto.aspx?IdArticulo=" + GridView2.SelectedValue.ToString());
        }

        protected void btnDetalles_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DetallesProducto.aspx?IdArticulo=" + GridView2.SelectedValue.ToString());
        }

        protected void btnPrecio_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constantes.Paginas.ConversionMoneda + "?" + Constantes.QueryString.ImporteConversion + "=" + lblPrecioValor.Text);
        }

        private string ObtenerURL()
        {
            string rutaLocal;
            string rutaVision = String.Empty;

            DataTable dt = ((DataView)AVE_ArticuloFotoObtener.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];

            string idTemporada = dt.Rows[0]["idTemporada"].ToString();
            string idProveedor = dt.Rows[0]["idProveedor"].ToString();
            string ModeloProveedor = dt.Rows[0]["ModeloProveedor"].ToString();

            // Construimos la ruta en Local
            rutaLocal = ConfigurationManager.AppSettings["Foto.RutaLocal"] + "/" + idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";


            if (rutaLocal.IndexOf("http://") == 0)
            {
                if (Comun.RemoteFileExists(rutaLocal, 2000))
                {
                    return rutaLocal;
                }
                else
                {
                    rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
                    return rutaVision;
                }

            }
            else
                if (System.IO.File.Exists(Server.MapPath(rutaLocal)))
                    return rutaLocal;
                else
                {
                    rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
                    return rutaVision;
                }
        }

        // MJM 26/03/2014 INICIO
        protected string ObtenerURLFoto(string pIdArticulo)
        {
            string rutaLocal;
            string rutaVision = String.Empty;


            AVE_ArticuloFotoByIdArticulo.SelectParameters["idArticulo"].DefaultValue = pIdArticulo;
            DataTable dt = ((DataView)AVE_ArticuloFotoByIdArticulo.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];

            string idTemporada = dt.Rows[0]["idTemporada"].ToString();
            string idProveedor = dt.Rows[0]["idProveedor"].ToString();
            string ModeloProveedor = dt.Rows[0]["ModeloProveedor"].ToString();

            // Construimos la ruta en Local
            rutaLocal = ConfigurationManager.AppSettings["Foto.RutaLocal"] + "/" + idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
            if (rutaLocal.IndexOf("http://") == 0)
            {
                if (Comun.RemoteFileExists(rutaLocal, 2000))
                {
                    return rutaLocal;
                }
                else
                {
                    rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
                    return rutaVision;
                }
            }
            else
                if (System.IO.File.Exists(Server.MapPath(rutaLocal)))
                    return rutaLocal;
                else
                {
                    rutaVision = ConfigurationManager.AppSettings["Foto.RutaVision"];// +idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";
                    return rutaVision;
                }
        }
        // MJM 26/03/2014 FIN

        /// Para recoger el id insertado hay que acudir a este evento 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SDSPedido_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            IdPedido = (int)((SqlParameter)(e.Command.Parameters["@IdPedido"])).Value;
            Session["IdPedido"] = IdPedido;

        }

        protected void hiddenTallasNo_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void CargarGridStock()
        {
            HyperLink hl;
            LinkButton b;
            foreach (GridViewRow fila in GridStock.Rows)
            {

                for (int i = IndexColumnasStock.InicioTallas; i < GridStock.Columns.Count - 1; i++)        //la última columna (TOTAL) no tiene enlaces
                {
                    if (fila.Cells[0].Text != String.Empty && fila.Cells[0].Text != "&nbsp;" && fila.Cells[1].Text != "Total")
                    {
                        b = new LinkButton();
                        b.Style.Add(HtmlTextWriterStyle.BorderStyle, "None");
                        b.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Transparent");
                        b.CommandName = "GenerarSolicitud";
                        b.Text = fila.Cells[i].Text;
                        b.CommandArgument = IdArticulo + ";" + GridStock.Columns[i].HeaderText + ";" + fila.Cells[IndexColumnasStock.IdTienda].Text + ";" + fila.Cells[i].Text;

                        fila.Cells[i].Controls.Add(b);
                    }
                    else
                    {
                        if (fila.Cells[1].Text == "Total")
                        {
                            //fila.Cells[1].Text = "<h5>Total</h5>";
                            break;
                        }

                    }
                }
            }
            // if (GridStock.Columns.Count == 4) GridStock.Columns.RemoveAt(GridStock.Columns.Count -1); 

        }

        protected void GridStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            // MJM 27/03/2014 INICIO

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            int maxCell = (e.Row.Cells.Count - 1);
            Unit size;
            for (int i = 0; i < e.Row.Cells.Count - 1; i++)
            {
                switch (i)
                {
                    case 0:
                        size = new Unit(70, UnitType.Pixel);
                        break;
                    case 1:
                        size = new Unit(220, UnitType.Pixel);
                        break;
                    default:
                        size = new Unit(50, UnitType.Pixel);
                        break;
                }

                e.Row.Cells[i].Width = size;
            }
        }

        protected void CargarNuevoGrid()
        {
            DataView dvORderTalla;
            DataView dvOrderStock;
            String StrCadena = String.Empty;
            string almacenCentral = string.Empty;
            string nombreAlmacenCentral = "ALMACEN CENTRAL";
            string idArticulo = string.Empty;
            string codigoAlfa = string.Empty;
            List<string> arrayStockTallas = null;
            string talla = string.Empty;
            string cantidadStock = string.Empty;
            string descripcion = string.Empty;
            string color = string.Empty;
            string precio = string.Empty;


            while (GridStock.Columns.Count > 3)
            {
                GridStock.Columns.RemoveAt(2);
            }
            DataTable dtTallas;
            DataTable dtStock = ((DataView)AVE_StockEnTiendaObtener.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];
            idArticulo = dtStock.Rows[0]["IdArticulo"].ToString();
            AlmacenPiaguiWS almacenPiagui = new AlmacenPiaguiWS();

            //Obtener id de Almacen Central mediante WS para añadirlo a la tabla de stock posteriormente
            almacenCentral = almacenPiagui.ObtenerAlmacen(UserACPiaguiAppSettings, PassACPiaguiAppSettings);
            almacenCentral = "T-" + almacenCentral;

            //Obtener referencia del articulo
            codigoAlfa = almacenPiagui.GetReferencia(idArticulo);

            //Recibir el stock de tallajes del Almacen Central
            arrayStockTallas = almacenPiagui.ObtenerExistencias(UserACPiaguiAppSettings, PassACPiaguiAppSettings, codigoAlfa);

            EliminarStockLocalAC(idArticulo, almacenCentral);
            //Recorrer arrayStocktallas e ir añadiendo el stock de Almacen Central en el datable de dsStock
            if (arrayStockTallas != null && arrayStockTallas.Count > 0)
            {
                for (int i = 0; i < arrayStockTallas.Count; i++)
                {
                    talla = arrayStockTallas[i].Split('#')[0].ToString();
                    cantidadStock = arrayStockTallas[i].Split('#')[1].ToString();
                    InsertarStockAC(idArticulo, almacenCentral, talla, cantidadStock);
                }
            }

            //Tablas de resultados de BD
            dtTallas = ((DataView)SqlDataTallajes.Select(new DataSourceSelectArguments())).ToTable(true, new string[] { "Talla" });
            dtStock = ((DataView)AVE_StockEnTiendaObtener.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];

            //Obtenemos datos del articulo y de la tienda para introducir el almacen central con stock 0
            descripcion = dtStock.Rows[0]["Descripcion"].ToString();
            color = dtStock.Rows[0]["Color"].ToString();
            precio = dtStock.Rows[0]["Precio"].ToString();


            DataRow rowAC;
            //Si no recibimos respuesta del WS, Introducimos en el grid todas las tallas de AC a 0
            if (arrayStockTallas == null)
            {
                foreach (DataRow rowTalla in dtTallas.Rows)
                {
                    rowAC = dtStock.NewRow();
                    rowAC["Descripcion"] = descripcion;
                    rowAC["Talla"] = rowTalla["Talla"];
                    //dejamos vacia la column "cantidad" para luego cambiarlo por ceros
                    rowAC["IdTienda"] = almacenCentral;
                    rowAC["Tienda"] = nombreAlmacenCentral;
                    rowAC["Color"] = color;
                    rowAC["Precio"] = precio;
                    rowAC["idArticulo"] = idArticulo;
                    rowAC["Orden"] = 1;

                    dtStock.Rows.Add(rowAC);
                }
            }

            //se ha metido en un dataview para poder ordenar las tallas y pasar al datatable Inicial
            dvORderTalla = dtTallas.DefaultView;

            dvORderTalla.Sort = "Talla";
            dtTallas = dvORderTalla.ToTable();
            //----------------------------------

            dvOrderStock = dtStock.DefaultView;
            dvOrderStock.Sort = "Orden ASC, IdTienda";

            dtStock = dvOrderStock.ToTable();

            //Añadimos las columnas dinámicas al grid. Es una por cada talla devuelta
            BoundField bf;
            foreach (DataRow dr in dtTallas.Rows)
            {
                bf = new BoundField();
                bf.DataField = dr["Talla"].ToString();          //El campo de la columna corresponde al nombre de la talla
                bf.HeaderText = dr["Talla"].ToString();
                //bf.HeaderStyle.Width = Unit.Pixel(50);
                bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                bf.HeaderStyle.Width = new Unit(51, UnitType.Pixel);
                bf.ItemStyle.Width = new Unit(51, UnitType.Pixel);

                GridStock.Columns.Insert(GridStock.Columns.Count - 1, bf);
            }

            //Tabla de datos con el formato correcto
            DataTable dtResultado = new DataTable();

            //Columnas de resultados. Son dos fijas, una por talla y la de totales
            dtResultado.Columns.Add("IdTienda", typeof(string));
            dtResultado.Columns.Add("Tienda", typeof(string));
            foreach (DataRow dr in dtTallas.Rows)
            {
                dtResultado.Columns.Add(dr["Talla"].ToString(), typeof(string));       //Es un entero pues albergará la cantidad de stock
            }
            dtResultado.Columns.Add("Total", typeof(int));

            //Filas
            DataRow nuevaFila = dtResultado.NewRow();
            string prevIdTienda = string.Empty;         //Para controlar cual es la fila que se comprobó en la iteración anterior
            string idTienda;                            //Para contener la fila que se está comprobando en esta iteración
            int acumulado = 0;                          //Para ir sumando los totales por tienda
            int auxCantidad;                            //Auxiliar para tratar con las cantidades pasadas a entero

            //Recorremos la tabla de stock devuelto. Como los resultados vienen ordenados por idtienda el procedimiento será:
            //Insertamos una fila con el primer registro y guardamos la tienda que es; en el siguiente registro comprobamos si 
            //seguimos con la misma tienda, en cuyo caso no creamos fila nueva sino que solo incluimos el dato de stock para la 
            //talla que sea; si el registro es de una tienda distinta, entonces sí creamos una fila nueva y además añadimos el 
            //stock de la talla. Finalmente solo insertamos la fila si no está ya insertada.
            foreach (DataRow dr in dtStock.Rows)
            {
                idTienda = dr["IdTienda"].ToString();
                if (idTienda != prevIdTienda)               //Si la fila anterior era de una tienda distinta entonces tenemos que crear una nueva fila para la tienda actual
                {
                    nuevaFila = dtResultado.NewRow();
                    nuevaFila["IdTienda"] = dr["IdTienda"];
                    nuevaFila["Tienda"] = dr["Tienda"];

                    acumulado = 0;
                    nuevaFila["Total"] = acumulado;                     //Metemos el valor a 0 para que no quede en blanco si no hay ninguna columna que sume 
                }

                nuevaFila[dr["Talla"].ToString()] = (dr["Cantidad"].ToString().Equals("0") ? "  " : dr["Cantidad"].ToString()); ;     //Siempre insertamos el stock de la talla en la fila que se está manejando, que puede ser nueva o la de la iteración anterior

                //Acumulamos para el total solo si es una cantidad positiva
                if (int.TryParse(dr["Cantidad"].ToString(), out auxCantidad))
                {
                    acumulado += auxCantidad;
                    //Como no sé cual será la última cantidad a sumar en la fila para poner el valor en la columna, lo que hago es actualizar el valor de Total cada vez que varía el acumulado

                    // MJM 23/03/2014 INICIO
                    if (acumulado != auxCantidad)
                        nuevaFila["Total"] = acumulado;
                }

                if (dtResultado.Rows.IndexOf(nuevaFila) == -1)          //Si la fila no está insertada ya, la insertamos
                    dtResultado.Rows.Add(nuevaFila);

                prevIdTienda = idTienda;                    //Guardamos el idtienda para que en la próxima iteración sepamos si es una fila nueva o no

            }

            nuevaFila = dtResultado.NewRow();
            nuevaFila[1] = "Total";
            int TotalTalla = 0;

            int ultimaTalla = -1; // MJM 23/03/2014
            for (int i = 2; i <= dtResultado.Columns.Count - 2; i++)
            {
                for (int index = 0; index <= dtResultado.Rows.Count - 1; index++)
                {

                    if (dtResultado.Rows[index]["idtienda"].ToString() == almacenCentral)
                    {
                        //Pintamos * en el caso de que no haya conexion con AC
                        if (dtResultado.Rows[index][i] == String.Empty)
                            dtResultado.Rows[index][i] = "*";

                        dtResultado.Rows[index]["Total"] = "0";
                    }
                    else
                    {
                        int cantidad;
                        if (int.TryParse(dtResultado.Rows[index][i].ToString(), out cantidad))
                        {
                            TotalTalla += cantidad;
                            ultimaTalla = cantidad;
                        }
                        else
                        {
                            TotalTalla += 0;
                            ultimaTalla = 0;
                        }
                    }
                }

                // MJM 23/03/2014 INICIO
                if (TotalTalla != 0 && TotalTalla != ultimaTalla)
                    nuevaFila[i] = TotalTalla.ToString();
                else
                    nuevaFila[i] = ultimaTalla.ToString();
                // MJM 23/03/2014 FIN
               TotalTalla = 0;
            }
            if (dtResultado.Rows.Count > 1)
            {
                dtResultado.Rows.Add(nuevaFila);
            }
            dtResultado.AcceptChanges();

            //Cargamos el grid
            GridStock.DataSource = dtResultado;
            GridStock.DataBind();

            //controlar para ver las fotos de otras
            if (GridStock.Rows.Count > 0)
            {
                btnFoto.Enabled = true;

            }
            CargarGridStock();

        }

        public DataTable RemoveDuplicateRows(DataTable table, string DistinctColumn)
        {
            try
            {
                ArrayList UniqueRecords = new ArrayList();
                ArrayList DuplicateRecords = new ArrayList();

                // Check if records is already added to UniqueRecords otherwise,
                // Add the records to DuplicateRecords
                foreach (DataRow dRow in table.Rows)
                {
                    if (UniqueRecords.Contains(dRow[DistinctColumn]))
                        DuplicateRecords.Add(dRow);
                    else
                        UniqueRecords.Add(dRow[DistinctColumn]);
                }

                // Remove dupliate rows from DataTable added to DuplicateRecords
                foreach (DataRow dRow in DuplicateRecords)
                {
                    table.Rows.Remove(dRow);
                }

                // Return the clean DataTable which contains unique records.
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

//            IdArticulo = ((RadioButtonList)sender).SelectedValue;
            IdArticulo = ((DropDownList)sender).SelectedValue;
            CargarStock();

        }

        private void InsertarStockAC(string idArticulo, string almacenCentral, string talla, string cantidad)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;
            int idCabecero;
            int idCabeceroDetalle;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            //Eliminamos el stock local de BD para actualizar con los datos del WS
            string sql = "SELECT Id_Cabecero FROM ARTICULOS WHERE IdArticulo = " + idArticulo;

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);

            idCabecero = Convert.ToInt32(myCommand.ExecuteScalar());

            sql = "SELECT Id_Cabecero_Detalle FROM CABECEROS_DETALLES WHERE Id_Cabecero = " + idCabecero.ToString() + " AND Nombre_Talla = '" + talla + "'";
            myCommand = new SqlCommand(sql, myConnection);

            idCabeceroDetalle = Convert.ToInt32(myCommand.ExecuteScalar());

            sql = "INSERT INTO N_EXISTENCIAS (IdArticulo, IdTienda, Id_Cabecero_Detalle, Cantidad) VALUES (" + idArticulo + ", '" + almacenCentral + "', " + idCabeceroDetalle + "," + cantidad + " )";
            myCommand = new SqlCommand(sql, myConnection);
            myCommand.ExecuteScalar();
        }

        private void EliminarStockLocalAC(string idArticulo, string idTienda)
        {

            SqlConnection myConnection;
            SqlCommand myCommand;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

            //Eliminamos el stock local de BD para actualizar con los datos del WS
            string sql = "DELETE FROM N_EXISTENCIAS WHERE IdArticulo = " + idArticulo + " AND IdTienda = '" + idTienda + "'";

            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            myCommand = new SqlCommand(sql, myConnection);
            myCommand.ExecuteScalar();

            myConnection.Close();
        }

        protected void GridStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String IdPedido = String.Empty;
            int Num = 0;
            AlmacenPiaguiWS almacenPiaguiWS = new AlmacenPiaguiWS();

            if (e.CommandName == "GenerarSolicitud")
            {

                
                valors = e.CommandArgument.ToString();
                string[] valor = valors.Split(';');

                ViewState["talla"] = valor[1].ToString();
                Session["tall"] = valor[1].ToString();

                    if (AVE.Contexto.IdTienda == valor[2].ToString())
                    {
                        int paramTPV = 0;
                        paramTPV = ObtenenerParamUsoSinAlmTPV();
                        if (paramTPV == 0)
                        {
                        GenerarSolicitud(valor[1].ToString(), valor[2].ToString(), valor[3].ToString());
                        }
                        else
                        {
                            int resulPed=GenerarPedSinSolicitud(valor[1].ToString(), valor[2].ToString(), valor[3].ToString());
                            if (resulPed > 0)
                            {
                             
                                ClientScriptManager cs = Page.ClientScript;
                                if (ViewState["talla"] != null || Session["tall"]!= null)
                                    cs.RegisterStartupScript(this.GetType(), "ConfirmarCarrito", "ConfirmarCarritoSinSol('¿Desea agregar el articulo " + Detalle.modelo + " con Talla " + ViewState["talla"] + " a la compra?');", true);
                                else
                                    cs.RegisterStartupScript(this.GetType(), "ConfirmarCarrito", "ConfirmarCarritoSinSol('¿Desea agregar el articulo " + Detalle.modelo + " a la compra?');", true);
                            }
                            else
                            {
                                String script = String.Empty;
                                script = "alert('Error al generar el pedido para este artículo');";
                                Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                            }
                        }
                    }
                    else if (valor[2].Substring(2).ToString() == almacenPiaguiWS.ObtenerAlmacen(UserACPiaguiAppSettings, PassACPiaguiAppSettings))
                    {
                        String script = String.Empty;
                        ClientScriptManager cs = Page.ClientScript;
                        script = "¿ Estas seguro que deseas solicitar mercancía a Almacén Central ?";
                        cs.RegisterStartupScript(this.GetType(), "ConfirmarSolicitud", "ConfirmarSolicitud('" + script + "');", true);
                    }
                    else
                    {
                        String TipoNegado = "0";
                        String script = String.Empty;
                        String TiendaSeleccionada = String.Empty;
                        //pares negados
                        Comun.InsertarProductoNegado(IdArticulo, valor[1].ToString(), "Par Negado en Tienda", ref  TipoNegado, TiendaSeleccionada);
                        if (TipoNegado == "1")
                        {

                            script += " Par Negado  " + Detalle.modelo + "  talla " + valor[1].ToString() + ".";

                            if (int.TryParse(valor[3], out Num))
                            {
                                script += "  ¿Desea realizar una solicitud a otra tienda?";
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "ConfirmarSolicitud", "ConfirmarSolicitud('" + script + "');", true);
                            }
                            else
                            {
                                script = "alert('" + script + "');";
                                Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                            }
                        }
                        else
                        {
                            if (TipoNegado == "2") script += " Par Negado en todas las Tiendas de la talla " + valor[1].ToString() + ".";
                            else script += " Par Negado en Tienda de la talla  " + valor[1].ToString() + ".";
                            script = "alert('" + script + "');";
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
                        }

                    }
               
            }
        }
        
        protected void GenerarSolicitud(string talla, string idTienda, string stock)
        {

            DataView dv;
            String TipoNegado = "0";
            String script = String.Empty;
            String TallasOtras = String.Empty;
            String TallaMensaje = String.Empty;
            String TiendaSeleccionada = String.Empty;
            String strDescripcion = "Par Negado en Tienda";



            dv = (DataView)AVE_StockEnTiendaObtener.Select(new DataSourceSelectArguments());


            dv.RowFilter = "idArticulo=" + IdArticulo + " and Talla='" + talla + "' and cantidad>0  and idTienda='" + idTienda + "'";

            if (dv.Count > 0)
            {
                SDSPedido.InsertParameters["IdArticulo"].DefaultValue = IdArticulo;
                SDSPedido.InsertParameters["Talla"].DefaultValue = talla;
                SDSPedido.InsertParameters["Unidades"].DefaultValue = "1";
                SDSPedido.InsertParameters["Precio"].DefaultValue = hiddenTallas.Value.ToString();
                SDSPedido.InsertParameters["Usuario"].DefaultValue = Contexto.Usuario;
                SDSPedido.InsertParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
                SDSPedido.InsertParameters["IdTienda"].DefaultValue = idTienda;
                SDSPedido.InsertParameters["Stock"].DefaultValue = dv[0]["Cantidad"].ToString();
                SDSPedido.InsertParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal;

                if (SDSPedido.Insert() > 0)
                {
                    if (Contexto.IdTienda == idTienda)
                    {
                        script += "Solicitud a Bodega con el Nº" + IdPedido.ToString() + ".";
                        script = "alert('" + script + "');";
                        HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = DateTime.Now.AddSeconds(5);
                        if (Session["SOL_TIENDA"] != null)
                        {
                            Session["SOL_TIENDA"] = Session["SOL_TIENDA"].ToString() + '|' + IdPedido.ToString();
                        }
                        else
                        {
                            Session["SOL_TIENDA"] = IdPedido.ToString();

                        }

                    }
                    else
                    {
                        TipoNegado = "0";
                        //    HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = DateTime.Now.AddSeconds(5);
                    }
                }
            }
            else
            {
                TipoNegado = "0";
                //pares negados
                if (Contexto.IdTienda != idTienda && stock.ToString().Equals("-"))
                {
                    dv.RowFilter = "idArticulo=" + IdArticulo + " and Talla='" + talla + "' and cantidad>0  and idTienda='" + Contexto.IdTienda + "'";

                    if (dv.Count > 0)
                    {
                        script += " No se tienen existencias en la talla seleccionada, en la Tienda foránea " + idTienda;
                        script = "alert('" + script + "');";
                        //script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + IdArticulo ) + "';";
                    }
                    else
                    {
                        Comun.InsertarProductoNegado(IdArticulo, talla, strDescripcion, ref  TipoNegado, TiendaSeleccionada);

                        if (TipoNegado == "2")
                        {
                            script += " Par Negado en todas las Tiendas de la talla " + talla + ".";
                            script = "alert('" + script + "');";
                            //  script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + IdArticulo ) + "';";
                        }
                        else
                        {
                            script += " Par Negado en Tienda de la talla  " + talla + ".";
                            script = "alert('" + script + "');";
                            //   script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" +IdArticulo ) + "';";


                        }

                    }


                }
                else
                {
                    Comun.InsertarProductoNegado(IdArticulo, talla, strDescripcion, ref  TipoNegado, TiendaSeleccionada);

                    if (TipoNegado == "2")
                    {
                        script += " Par Negado en todas las Tiendas de la talla " + talla + ".";
                        script = "alert('" + script + "');";
                        //      script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + IdArticulo ) + "';";
                    }
                    else
                    {
                        script += " Par Negado en Tienda de la talla  " + talla + ".";
                        script = "alert('" + script + "');";
                        //     script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + IdArticulo ) + "';";


                    }
                }
            }



            Page.ClientScript.RegisterStartupScript(typeof(string), "", script, true);
            hiddenTallas.Value = String.Empty;
            hiddenTallasNo.Value = String.Empty;

        }

        protected int GenerarPedSinSolicitud(string talla, string idTienda, string stock)
        {

            DataView dv;
         
            String script = String.Empty;
            String TallasOtras = String.Empty;
            String TallaMensaje = String.Empty;
            String TiendaSeleccionada = String.Empty;
            int result = 0;
         


            dv = (DataView)AVE_StockEnTiendaObtener.Select(new DataSourceSelectArguments());


            dv.RowFilter = "idArticulo=" + IdArticulo + " and Talla='" + talla + "' and cantidad>0  and idTienda='" + idTienda + "'";

            if (dv.Count > 0)
            {
                SDSPedido.InsertParameters["IdArticulo"].DefaultValue = IdArticulo;
                SDSPedido.InsertParameters["Talla"].DefaultValue = talla;
                SDSPedido.InsertParameters["Unidades"].DefaultValue = "1";
                SDSPedido.InsertParameters["Precio"].DefaultValue = hiddenTallas.Value.ToString();
                SDSPedido.InsertParameters["Usuario"].DefaultValue = Contexto.Usuario;
                SDSPedido.InsertParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
                SDSPedido.InsertParameters["IdTienda"].DefaultValue = idTienda;
                SDSPedido.InsertParameters["Stock"].DefaultValue = dv[0]["Cantidad"].ToString();
                SDSPedido.InsertParameters["IdTerminal"].DefaultValue = Contexto.IdTerminal;

                if (SDSPedido.Insert() > 0)
                {
                    result = 1;
                }
            }
            return result;
        }

        private int ObtenenerParamUsoSinAlmTPV() {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = string.Format("SELECT Valor FROM CONFIGURACIONES_TPV_AVANZADO WHERE NombreCampo='Eliminar Solicitudes Al. Local'");
                    SqlDataReader reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        result = int.Parse(reader["Valor"].ToString());
                    }

                    
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
                connection.Close();
            }
            return result;
        }
      
        protected void Button2_Click(object sender, EventArgs e)
        {
            int Num = 0;
            String Mensaje = String.Empty;
            DLLGestionVenta.ProcesarVenta _V;
            Int64 idCarrito = 0;
           
            string[] valor = valors.Split(';');
          
                GenerarSolicitud(valor[1].ToString(), valor[2].ToString(), valor[3].ToString());

                if (!int.TryParse(valor[3], out Num)) { return; }


                _V = new DLLGestionVenta.ProcesarVenta();
                _V.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                if (Session["idCarrito"] != null) { idCarrito = Int64.Parse(Session["IdCarrito"].ToString()); }

                if (!_V.ComprobarStock(Int64.Parse(Session["IdPedido"].ToString()), idCarrito))
                {

                    Mensaje = "Se dispone a vender una Referencia sin Stock Registrado. ";
                }
                else
                {
                    if (int.Parse(valor[3].ToString()) <= 0) { Mensaje = "Se dispone a vender una Referencia sin Stock Registrado. "; }

                }

                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "ConfirmarCarrito", "ConfirmarCarrito('" + Mensaje + "¿Desea agregar el articulo a la compra?');", true);
            
        }
        
        protected void ConfirmPedido_Click(object sender, EventArgs e) {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = string.Format(" update AVE_PEDIDOS SET IdEstadoSolicitud=6 WHERE IdPedido=" + Session["IdPedido"].ToString());
                    result=command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
                connection.Close();
                Button3_Click(sender,e);
            }

        }
        
        protected void CancelPedido_Click(object sender, EventArgs e)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString()))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = string.Format("update AVE_PEDIDOS SET IdEstadoSolicitud=5 WHERE IdPedido=" + Session["IdPedido"].ToString());
                    result = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
                connection.Close();
            }
        }
        
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Session["IdPedido"] != null)
            {
                IdPedido = (int)Session["IdPedido"];
                añadiralcarrito(IdPedido.ToString());
                añadirLineaCarrito();

                CalculoPromocion_Carrito();

            }
        }
        
        protected int ValidaArticulosCarrito(string idCarrito)
        {
            int numArticulos = 0;
            DLLGestionVenta.ProcesarVenta objCarrito;
            try
            {
                objCarrito = new DLLGestionVenta.ProcesarVenta();
                objCarrito.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                numArticulos = objCarrito.GetArticulosCarrito(idCarrito);
            }
            catch (Exception ex)
            {
                //log.Error(ex);
            }
            return numArticulos;
        }
        
        protected void añadiralcarrito(string IdPedido)
        {
            //Si no hay carrito anterior, generamos uno nuevo
            if (Session["IdCarrito"] == null)
            {
                AnadirCarrito.InsertParameters["Usuario"].DefaultValue = Contexto.IdEmpleado;
                AnadirCarrito.InsertParameters["IdCliente"].DefaultValue = "0";
                AnadirCarrito.InsertParameters["Maquina"].DefaultValue = System.Web.HttpContext.Current.Request.UserHostAddress;
                AnadirCarrito.InsertParameters["EstadoCarrito"].DefaultValue = "0";
               

                AnadirCarrito.Insert();






            }
        }
        
        protected void añadirLineaCarrito()
        {
            if (Session["IdCarrito"] != null && (ViewState["talla"] != null || Session["tall"] != null))
            {
                AnadirDetalleCarrito.InsertParameters["IdArticulo"].DefaultValue = IdArticulo;
                AnadirDetalleCarrito.InsertParameters["IdCarrito"].DefaultValue = Session["IdCarrito"].ToString();
                AnadirDetalleCarrito.InsertParameters["IdPedido"].DefaultValue = Session["IdPedido"].ToString();
                AnadirDetalleCarrito.InsertParameters["Talla"].DefaultValue = ViewState["talla"] != null ? ViewState["talla"].ToString(): Session["tall"].ToString();
                AnadirDetalleCarrito.InsertParameters["Cantidad"].DefaultValue = "1";

            }
           
            AnadirDetalleCarrito.Insert();
            HyperLink lab = (HyperLink)this.Page.FindControl("lblNumArt");
            if (Session["IdCarrito"] != null)
            {
               
                int ArtiCarrito = ValidaArticulosCarrito(Session["IdCarrito"].ToString());
                if (ArtiCarrito > 0)
                lab.Visible = true;
                lab.Text = ArtiCarrito.ToString();
            }
            else lab.Visible = false;
            

        }
        
        protected void AnadirCarrito_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            IdCarro = (int)((SqlParameter)(e.Command.Parameters["@IdCarrit"])).Value;
            Session["IdCarrito"] = IdCarro;


            ImageButton Img = (ImageButton)this.Page.FindControl("ImageButton1");

            if (Session["IdCarrito"] != null)
            {
                Img.Visible = true;
            }
            else
            {
                Img.Visible = false;
            }


        }
      
        private void EvaluarEnlaces(string IdArticulo)
        {
            if (!string.IsNullOrEmpty(IdArticulo))
            {
                //Evaluamos los enlaces de sustitutivo y complementario

                AVE.Configuracion.ComprobarCompatibilidad();
                SDSTieneCS.SelectParameters[0].DefaultValue = IdArticulo;
                DataView dv = (DataView)SDSTieneCS.Select(new DataSourceSelectArguments());

                if (dv != null && dv.Count > 0)
                {
                    if ((!string.IsNullOrEmpty((dv[0][Constantes.TipoRelacionCS.Sustitutivo.ToString()]).ToString()) && (int)dv[0][Constantes.TipoRelacionCS.Sustitutivo.ToString()] > 0) || (!string.IsNullOrEmpty((dv[0][Constantes.TipoRelacionCS.Complementario.ToString()]).ToString()) && (int)dv[0][Constantes.TipoRelacionCS.Complementario.ToString()] > 0))
                    {
                        btnComplementos.Visible = true;
                    }
                    else btnComplementos.Visible = false;

                    btnComplementosMini.Visible = (btnComplementos.Visible && repComplementos.Items.Count > 0);
                    btnSustitutivoMini.Visible = (btnComplementos.Visible && repSustitutosMini.Items.Count > 0);
                }

            }
        }
        
        protected void btnComplementos_Click(object sender, EventArgs e)
        {
            Boolean parent = true;
            //Object o = this.Parent; // <- VARIABLE O ) yeah
            Object objDocument = this.Parent;
            int contador = 0;
            while (parent)
            {
                if (objDocument.GetType().Name.Replace("_aspx", "").ToLower() == typeof(EleccionProducto).Name.ToLower())
                    parent = false;
                else
                    switch (contador)
                    {
                        case 0:
                            objDocument = this.Parent.Parent;
                            break;
                        case 1:
                            objDocument = this.Parent.Parent.Parent;
                            break;
                        case 2:
                            objDocument = this.Parent.Parent.Parent.Parent;
                            break;
                        case 3:
                            objDocument = this.Parent.Parent.Parent.Parent.Parent;
                            break;
                        case 4:
                            objDocument = this.Parent.Parent.Parent.Parent.Parent.Parent;
                            break;
                        case 5:
                            objDocument = this.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
                            break;
                        case 6:
                            objDocument = this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
                            break;
                        case 7:
                            objDocument = this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
                            break;
                        case 8:
                            objDocument = this.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
                            break;
                        default:
                            break;
                    }
                contador++;

            }

            ((EleccionProducto)objDocument).Complementos(((Button)sender).CommandArgument);
        }

        private void CalculoPromocion_Carrito()
        {

            DLLGestionVenta.ProcesarVenta objVenta;
            String rMensaje = String.Empty;

            if (Session["IdCarrito"] != null)
            {
                objVenta = new DLLGestionVenta.ProcesarVenta();

                objVenta.ConexString = System.Configuration.ConfigurationManager.ConnectionStrings["MC_TDAConnectionString"].ToString();

                rMensaje = objVenta.GetObjCarritoPromocion(Int64.Parse(Session["IdCarrito"].ToString()), AVE.Contexto.IdTienda, AVE.Contexto.FechaSesion);

                if (rMensaje.Length > 1)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "PROMO", "alert('" + rMensaje + "');", true);
                    return;
                }

            }
        }

        protected void btnComplementosMini_Click(object sender, EventArgs e)
        {
            divComplementosMini.Visible = true;
            divSustitutosMini.Visible = false;
        }

        protected void btnSustitutivoMini_Click(object sender, EventArgs e)
        {
            divComplementosMini.Visible = false;
            divSustitutosMini.Visible = true;
        }

    }

}