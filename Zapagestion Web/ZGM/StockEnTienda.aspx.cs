using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AVE;
using System.Data.SqlClient; 

public partial class StockEnTienda :  AVE.CLS.Cls_Session 
{
  

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

    protected void Page_Load(object sender, EventArgs e)
    {


        //Registramos en javascript el control de la caja de texto para usarlo desde otras funciones jscript;
        string script = "var Talla_Selecionada = document.getElementById('" + hiddenTallas.ClientID + "');";
        script += "var Talla_SelecionadaNo = document.getElementById('" + hiddenTallasNo.ClientID + "');";
        Page.ClientScript.RegisterStartupScript(typeof(string), "Talla_Selecionada", script, true);
        btnFoto.Visible = false;




        if (!Page.IsPostBack)
        {
            DataView dv = null;
            string tallaBuscada = string.Empty;

            if (Request.QueryString["Talla"] != null)
                tallaBuscada = Request.QueryString["Talla"].ToUpper().Trim();

            // Obtenemos la información del producto
            if (Request.QueryString[Constantes.QueryString.IdArticulo] != null && Request.QueryString["Tipo"] == null)
            {
                //Búsqueda normal
                AVE_StockEnTiendaObtener.SelectParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;

                dv = (DataView)AVE_StockEnTiendaObtener.Select(new DataSourceSelectArguments());

                FotoArticulo.ImageUrl = this.ResolveUrl(ObtenerURL());
            }
            else if (Request.QueryString["Tipo"] != null && Request.QueryString["IdArticulo"] != null)
            {
                //Búsqueda de C o S
                AVE_StockEnTiendaCSObtener.SelectParameters["IdTienda"].DefaultValue = AVE.Contexto.IdTienda;
                dv = (DataView)AVE_StockEnTiendaCSObtener.Select(new DataSourceSelectArguments());
            }


            if (dv != null && dv.Count > 0)
            {
                //// Almacenamos toda la información del producto en dtProductos
                DataTable dtProductos = dv.Table;
                                              

                //// Almacenamos las tallas en dtTallas. que tieen stock
                ////DataTable dtTallas = dtProductos.DefaultView.ToTable(true, new string[] { "Talla" });
                //DataTable dtTallas = ((DataView)SqlDataTallajes.Select(new DataSourceSelectArguments())).ToTable(true, new string[] { "Talla" });


                // //vamos a eliminar Tallas que no son necesarias vamos a visualizar solo 3 tallajes
                // if (tallaBuscada.Length > 0 &&  dtTallas.Rows.Count>3)
                // {
                //     Int32 CounTalla= dtTallas.Rows.Count;

                //     for (int i=0; i < dtTallas.Rows.Count; i++)
                //     {

                //         if (tallaBuscada == dtTallas.Rows[i]["Talla"].ToString().ToUpper())
                //         {

                //             if (i == 0)
                //             {
                //                 //eliminar a partir del indice  del 4 tallaje
                //                 RemoveTallas(ref dtTallas, 2, (dtTallas.Rows.Count - 1));

                //             }

                //             else
                //             {

                //                 if (i == (dtTallas.Rows.Count - 1))
                //                 {
                //                     //eliminamos los tallajes anteriores cuando es el ultimo
                //                     RemoveTallas(ref dtTallas, 0, (dtTallas.Rows.Count - 3));

                //                 }
                //                 else
                //                 {
                //                     //esta en medio del tallaje

                //                     //eliminamos las posteriores 
                //                     RemoveTallas(ref dtTallas, i + 1, (dtTallas.Rows.Count - 1));

                //                     //eliminamos las primeras
                //                     if (i > 1)
                //                     {
                //                         if ((i - 2) == 0)
                //                         {
                //                             RemoveTallas(ref dtTallas, -1, 0);

                //                         }
                //                         else
                //                         {
                //                             RemoveTallas(ref dtTallas, -1, (i - 2));

                //                         }

                //                     }


                //                 }
                //             }  
                //         }    
                //    }  

                //}  

                //Añadimos las columnas dinámicas al grid. Es una por cada talla devuelta
                //BoundField bf;
                //foreach (DataRow dr in dtTallas.Rows)
                //{
                //    bf = new BoundField();
                //    bf.DataField = dr["Talla"].ToString();          //El campo de la columna corresponde al nombre de la talla
                //    bf.HeaderText = dr["Talla"].ToString();
                //    bf.HeaderStyle.Width = Unit.Pixel(50);
                //    bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                //    //Resaltamos la columna si es la talla buscada
                //    if (tallaBuscada == dr["Talla"].ToString().ToUpper())
                //    {
                //        //bf.HeaderStyle.CssClass = "GridItemTallaBuscada";
                //        bf.ItemStyle.CssClass += " GridItemTallaBuscada";
                //    }

                //    GridView2.Columns.Add(bf);
                //}

                //// Añadimos las columnas con articulo, color y tallas a la tabla auxiliar    
                //DataTable dtAux = new DataTable();
                //dtAux.Columns.Add("Articulo");
                //dtAux.Columns.Add("Color");
                //for (int i = 0; i < dtTallas.Rows.Count; i++)
                //{
                //    dtAux.Columns.Add(dtTallas.Rows[i][0].ToString());
                //}

                ////Establecemos las cantidades en las filas correspondientes
                //for (int i = 0; i < dtProductos.Rows.Count; i++)
                //{
                //    int Fila = Esta(dtAux, dtProductos.Rows[i]["idArticulo"].ToString());

                //    // Si se encuentra ya el producto, me indica la fila donde tengo que escribir el valor
                //    if (Fila >= 0)
                //    {
                //        for (int j = 0; j < dtAux.Columns.Count; j++)
                //            if (dtProductos.Rows[i]["Talla"].ToString() == dtAux.Columns[j].ColumnName.ToString())
                //                dtAux.Rows[Fila][dtAux.Columns[j].ColumnName] = dtProductos.Rows[i]["Cantidad"];
                //    }
                //    else
                //    {
                //        // Si no encuentra el producto, añadimos una nueva fila
                //        DataRow dr = dtAux.NewRow();
                //        dr["Articulo"] = dtProductos.Rows[i]["idArticulo"];
                //        dr["Color"] = dtProductos.Rows[i]["Color"];
                //        for (int j = 0; j < dtAux.Columns.Count; j++)
                //            if (dtProductos.Rows[i]["Talla"].ToString() == dtAux.Columns[j].ColumnName.ToString())
                //                dr[dtAux.Columns[j].ColumnName] = dtProductos.Rows[i]["Cantidad"];

                //        dtAux.Rows.Add(dr);
                //    }
                //}

                // Escribimos en las labels, la descripción y el precio
                lblDescripcion.Text = dtProductos.Rows[0]["Descripcion"].ToString();
                lblPrecioValor.Text = string.Format(" Precio : {0:C}",Decimal.Parse(dtProductos.Rows[0]["Precio"].ToString()));
                hiddenTallas.Value = dtProductos.Rows[0]["Precio"].ToString();
             
                //// Cargamos en el GridView los datos del DataTable
                //GridView2.DataSource = dtAux;
                //GridView2.DataBind();

                //Si es un ean o codigoalfa no lo encontraremos así; si es un Compl o susti, menos.
                ////// Seleccionamos la fila donde se encuentra el producto exacto que hemos buscado
                ////for (int i = 0; i < GridView2.Rows.Count; i++)
                ////{
                ////    if (GridView2.Rows[i].Cells[1].Text == Request.QueryString["Producto"] | GridView2.Rows[i].Cells[1].Text == Request.QueryString["IdArticulo"])
                ////        GridView2.SelectedIndex = i;
                ////}

                // Seleccionamos la primera fila.
                //  GridView2.SelectedIndex = 0;

                //Establecemos los enlaces
                // EvaluarEnlaces();


                CargarNuevoGrid();

                if (GridStock.Rows.Count > 0)
                {
                    if (GridStock.Rows[0].Cells[0].Text.Equals(Contexto.IdTienda))
                    {
                        GridStock.SelectedIndex = 0;

                    }

                }

                CargarColoresModelo();

                if (Request.QueryString["Stock"] != null)
                {
                     GenerarSolicitud();
                }
            }
            else
                if (Request.QueryString[Constantes.QueryString.IdArticulo] != null)
                {
                    //Response.Redirect("~/StockOtras.aspx?IdArticulo=" + Request.QueryString[Constantes.QueryString.IdArticulo].ToString());
                }
                else
                {
                   
                    this.lblDescripcion.Text = Resources.Resource.NoInfoProducto;
                }
        }
        else
        {
            CargarColoresModelo();
        }

        //Ejecutamos la rutina para montar los enlaces en los valores de stock. No puede hacerse en el RowDataBound porque los
        //controles que añadimos dinámicamente se pierden en los postbacks (por ejemplo al seleccionar otro producto)
        //this.MontarEnlacesGrid();

       // CargarTabladeTallaje();
    }


    protected void CargarColoresModelo()
    {
        DataView dv;
        DataView dvaux;
        int numfilas;
        dv = (DataView)SqlDataModColores.Select(new DataSourceSelectArguments());
        dvaux = dv.Table.Copy().DefaultView; 
        Int64 idArticulo= Int64.Parse(Request["IdArticulo"].ToString());

        Table Tabla = new Table();
        Tabla.ID = "TablaColores";
        numfilas = dv.Count + 1;

        dvaux.RowFilter = "idArticulo=" + idArticulo;

        if (dvaux.Count > 0)
        {
            TableRow Fila = new TableRow();
            TableCell  Celda = new TableCell();
            Label LblCodigoAlfa = new Label();
            LblCodigoAlfa.Width = 200;
            LblCodigoAlfa.BorderStyle=BorderStyle.Double;
            Celda.CssClass = "EspacioCelda";
            LblCodigoAlfa.ID = "lblCodigoAlfa";
            LblCodigoAlfa.Text = dvaux[0]["CodigoAlfa"].ToString();
            Celda.VerticalAlign = VerticalAlign.Middle;
            Celda.HorizontalAlign = HorizontalAlign.Center;
            Celda.Controls.Add(LblCodigoAlfa);
            Fila.Cells.Add(Celda);
            Tabla.Rows.Add(Fila);
        }


        if (dv.Count > 0)
        {
            RadioButtonList option;
            TableCell Celda = new TableCell();
            TableRow Fila = new TableRow();
            option=new RadioButtonList();
            option.ID = dv[0]["modeloProveedor"].ToString();
            
            foreach (DataRowView  dr in dv)
            {
                 option.Items.Add(new ListItem(dr["Color"].ToString(),dr["idArticulo"].ToString()));
                
            }

            option.SelectedValue = idArticulo.ToString();
            option.SelectedIndexChanged += new EventHandler(RadioButtonList1_SelectedIndexChanged);
            option.AutoPostBack = true;
            Celda.VerticalAlign = VerticalAlign.Middle;
            Celda.HorizontalAlign = HorizontalAlign.Center;
            Celda.Controls.Add(option);
            Fila.Cells.Add(Celda);
            Tabla.Rows.Add(Fila);
        }

        PModelo.Controls.Add(Tabla);

     

    }    
    /// <summary>
    /// Procedimiento para eliminar las tallas que no se van a visualizar en el datatable
    /// </summary>
    /// <param name="dtTallas"></param>
    protected void RemoveTallas(ref DataTable dtTallas,Int32 inicio,Int32 Fin)
    {
        for (int i = Fin; i > inicio ; i--)
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
                    if (int.TryParse(row.Cells[i].Text,out aux) && aux>0)
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
    }

    // Al seleccionar una fila diferente, cogemos el valor del Producto
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  EvaluarEnlaces();

    }

    private void EvaluarEnlaces()
    {
        //Evaluamos los enlaces de sustitutivo y complementario


        AVE.Configuracion.ComprobarCompatibilidad(); 
        SDSTieneCS.SelectParameters[0].DefaultValue = GridView2.SelectedValue.ToString();
        DataView dv = (DataView)SDSTieneCS.Select(new DataSourceSelectArguments());

        if (dv != null && dv.Count > 0)
        {
            if ((int)dv[0][Constantes.TipoRelacionCS.Sustitutivo.ToString()] > 0)
                btnSustitutivos.Enabled= true;
            else
                btnSustitutivos.Enabled= false;

            if ((int)dv[0][Constantes.TipoRelacionCS.Complementario.ToString()] > 0)
                btnComplementos.Enabled= true;
            else
                btnComplementos.Enabled= false;
        }

        btnMasTiendas.Enabled = true;
        btnFoto.Enabled = true;
        btnDetalles.Enabled = true;

    }

    protected void btnComplementos_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StockEnTienda.aspx?IdArticulo=" + GridView2.SelectedValue.ToString() + "&Tipo=" + Constantes.TipoRelacionCS.Complementario);
    }

    protected void btnSustitutivos_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StockEnTienda.aspx?IdArticulo=" + GridView2.SelectedValue.ToString() + "&Tipo=" + Constantes.TipoRelacionCS.Sustitutivo);
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
        rutaLocal = ConfigurationManager.AppSettings["Foto.RutaLocal"]+ "/" + idTemporada + "/" + idProveedor + ModeloProveedor + ".jpg";


        if (rutaLocal.IndexOf("http://")== 0)
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

    protected void GenerarSolicitud()
    {

        DataView dv;
        String TipoNegado="0";
        String  script=String.Empty ;
        String TallasOtras = String.Empty;
        String TallaMensaje = String.Empty;
        String TiendaSeleccionada = String.Empty;
        String strDescripcion = "Par Negado en Tienda"; 

           

        dv = (DataView)AVE_StockEnTiendaObtener.Select(new DataSourceSelectArguments());

          
        dv.RowFilter = "idArticulo=" + Request.QueryString[Constantes.QueryString.IdArticulo].ToString() + " and Talla='" + Request.QueryString["Talla"].ToString() + "' and cantidad>0  and idTienda='" + Request.QueryString["idTienda"].ToString() + "'";

        if (dv.Count > 0)
        {
            SDSPedido.InsertParameters["IdArticulo"].DefaultValue = Request["IdArticulo"].ToString();
            SDSPedido.InsertParameters["Talla"].DefaultValue = Request.QueryString["Talla"].ToString();
            SDSPedido.InsertParameters["Unidades"].DefaultValue = "1";
            SDSPedido.InsertParameters["Precio"].DefaultValue= hiddenTallas.Value.ToString() ;
            SDSPedido.InsertParameters["Usuario"].DefaultValue = Contexto.Usuario;
            SDSPedido.InsertParameters["IdEmpleado"].DefaultValue = Contexto.IdEmpleado;
            SDSPedido.InsertParameters["IdTienda"].DefaultValue = Request.QueryString["idTienda"].ToString();
            SDSPedido.InsertParameters["Stock"].DefaultValue = dv[0]["Cantidad"].ToString();


            if (SDSPedido.Insert() > 0)
            {
                if (Contexto.IdTienda == Request.QueryString["idTienda"].ToString())
                {
                    script += "Solicitud a Bodega con el Nº" + IdPedido.ToString() + ".";
                    script = "alert('" + script + "');";
                    script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + Request["IdArticulo"].ToString()) + "';";
                   // HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = DateTime.Now.AddSeconds(5);
                }
                else
                {
                    TipoNegado = "0";
                    //pares negados
                    Comun.InsertarProductoNegado(Request["IdArticulo"].ToString(), Request.QueryString["Talla"].ToString(), "Par Negado en Tienda", ref  TipoNegado,TiendaSeleccionada);
                    if (TipoNegado == "1")
                    {
                        script = "Solicitud de Traspaso con el Nº " + IdPedido.ToString() + " en la tienda " + Request.QueryString["idTienda"].ToString() + ".";
                        script += " Par Negado en Tienda de la talla " + Request.QueryString["Talla"].ToString() + ".";

                    }


                    script = "alert('" + script + "');";
                    script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + Request["IdArticulo"].ToString()) + "';";
                    HttpContext.Current.Session[Constantes.Session.FechaUltimoPedido] = DateTime.Now.AddSeconds(5);


                }
            }
        }
            else
            {
                TipoNegado = "0";
                //pares negados
                if (Contexto.IdTienda != Request.QueryString["idTienda"].ToString() && Request.QueryString["Stock"].ToString().Equals("-"))
                {
                    dv.RowFilter = "idArticulo=" + Request.QueryString[Constantes.QueryString.IdArticulo].ToString() + " and Talla='" + Request.QueryString["Talla"].ToString() + "' and cantidad>0  and idTienda='" + Contexto.IdTienda + "'";

                    if (dv.Count > 0)
                    {
                        script += " No se tienen existencias en la talla seleccionada, en la Tienda foránea " + Request.QueryString["idTienda"].ToString();
                        script = "alert('" + script + "');";
                        script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + Request["IdArticulo"].ToString()) + "';";
                    }
                    else
                    {
                        Comun.InsertarProductoNegado(Request["IdArticulo"].ToString(), Request.QueryString["Talla"].ToString(), strDescripcion, ref  TipoNegado, TiendaSeleccionada);

                        if (TipoNegado == "2")
                        {
                            script += " Par Negado en todas las Tiendas de la talla " + Request.QueryString["Talla"].ToString() + ".";
                            script = "alert('" + script + "');";
                            script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + Request["IdArticulo"].ToString()) + "';";
                        }
                        else
                        {
                            script += " Par Negado en Tienda de la talla  " + Request.QueryString["Talla"].ToString() + ".";
                            script = "alert('" + script + "');";
                            script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + Request["IdArticulo"].ToString()) + "';";


                        }

                    }


                }
                else
                {
                    Comun.InsertarProductoNegado(Request["IdArticulo"].ToString(), Request.QueryString["Talla"].ToString(), strDescripcion, ref  TipoNegado, TiendaSeleccionada);

                    if (TipoNegado == "2")
                    {
                        script += " Par Negado en todas las Tiendas de la talla " + Request.QueryString["Talla"].ToString() + ".";
                        script = "alert('" + script + "');";
                        script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + Request["IdArticulo"].ToString()) + "';";
                    }
                    else
                    {
                        script += " Par Negado en Tienda de la talla  " + Request.QueryString["Talla"].ToString() + ".";
                        script = "alert('" + script + "');";
                        script += "document.location.href = '" + ResolveClientUrl(Constantes.Paginas.StockEnTienda + "?Talla=&IdArticulo=" + Request["IdArticulo"].ToString()) + "';";


                    }
                }
            }
        
               
             
            ClientScript.RegisterStartupScript(typeof(string), "", script, true);    
            hiddenTallas.Value = String.Empty;
            hiddenTallasNo.Value = String.Empty;
         
    }


          /// Para recoger el id insertado hay que acudir a este evento 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SDSPedido_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        IdPedido = (int)((SqlParameter)(e.Command.Parameters["@IdPedido"])).Value;

    
    }

    protected void hiddenTallasNo_ValueChanged(object sender, EventArgs e)
    {

    }

    protected void GridStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hl;
            for (int i = IndexColumnasStock.InicioTallas; i < GridStock.Columns.Count - 1; i++)        //la última columna (TOTAL) no tiene enlaces
            {
                if (e.Row.Cells[0].Text != String.Empty && e.Row.Cells[1].Text != "Total")
                {
                    hl = new HyperLink();
                    hl.Text = e.Row.Cells[i].Text;
                    //  esta parte se ha desactivado para que no permita hacer solicitudes a otras tiendas
                    hl.NavigateUrl = string.Format("StockEnTienda.aspx?IdArticulo={0}&Talla={1}&IdTienda={2}&Stock={3}",
                                    Request["IdArticulo"].ToString(),
                                    GridStock.Columns[i].HeaderText,
                                    e.Row.Cells[IndexColumnasStock.IdTienda].Text,
                                    e.Row.Cells[i].Text);
                    e.Row.Cells[i].Controls.Add(hl);
                }
                else
                {
                    if (e.Row.Cells[1].Text == "Total")
                    {
                        e.Row.Cells[1].Text = "<h4>Total</h4>";
                        break;
                    }

                }
            }
        }

          }
    protected void CargarNuevoGrid()
    {
        DataView dvORderTalla;
        String StrCadena=String.Empty ;

        
        //Tablas de resultados de BD
        DataTable dtTallas = ((DataView)SqlDataTallajes.Select(new DataSourceSelectArguments())).ToTable(true, new string[] { "Talla" });
        DataTable dtStock = ((DataView) AVE_StockEnTiendaObtener.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];

           


            //se ha metido en un dataview para poder ordenar las tallas y pasar al datatable Inicial
            dvORderTalla = dtTallas.DefaultView;
               
            dvORderTalla.Sort = "Talla";
            dtTallas = dvORderTalla.ToTable();
            //----------------------------------

            //Añadimos las columnas dinámicas al grid. Es una por cada talla devuelta
            BoundField bf;
            foreach (DataRow dr in dtTallas.Rows)
            {
                bf = new BoundField();
                bf.DataField = dr["Talla"].ToString();          //El campo de la columna corresponde al nombre de la talla
                bf.HeaderText = dr["Talla"].ToString();
                bf.HeaderStyle.Width = Unit.Pixel(50);
                bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GridStock.Columns.Insert(GridStock.Columns.Count-1,bf);
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

                nuevaFila[dr["Talla"].ToString()] = dr["Cantidad"];     //Siempre insertamos el stock de la talla en la fila que se está manejando, que puede ser nueva o la de la iteración anterior

                //Acumulamos para el total solo si es una cantidad positiva
                if (int.TryParse(dr["Cantidad"].ToString(), out auxCantidad))
                {
                    acumulado += auxCantidad;
                    //Como no sé cual será la última cantidad a sumar en la fila para poner el valor en la columna, lo que hago es actualizar el valor de Total cada vez que varía el acumulado
                    nuevaFila["Total"] = acumulado;
                }

                if (dtResultado.Rows.IndexOf(nuevaFila) == -1)          //Si la fila no está insertada ya, la insertamos
                    dtResultado.Rows.Add(nuevaFila);

                prevIdTienda = idTienda;                    //Guardamos el idtienda para que en la próxima iteración sepamos si es una fila nueva o no

            }

            nuevaFila = dtResultado.NewRow();
            nuevaFila[1] = "Total";
            int TotalTalla=0;

            for (int i = 2; i <= dtResultado.Columns.Count -2; i++)
            {
                for (int index = 0; index <= dtResultado.Rows.Count -1; index++)
                {

                    if (dtResultado.Rows[index][i].ToString() == String.Empty)
                    {
                        dtResultado.Rows[index][i] = "-";

                    }
                    else
                    {
                        TotalTalla += Int16.Parse(dtResultado.Rows[index][i].ToString());
                    }

                }  

                nuevaFila[i]=TotalTalla.ToString();
                TotalTalla=0;
            }

            dtResultado.Rows.Add(nuevaFila);
            dtResultado.AcceptChanges();
                                        
            //Cargamos el grid
            GridStock.DataSource = dtResultado;
            GridStock.DataBind();

            //controlar para ver las fotos de otras
            if (GridStock.Rows.Count > 0) 
            {
                btnFoto.Enabled = true;
               
           }
            
        }
      

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        String Value=((RadioButtonList)sender).SelectedValue;

        Response.Redirect("~/StockEnTienda.aspx?IdArticulo=" + Value); 

    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {

        Response.Redirect(Server.UrlDecode(Constantes.Paginas.Inicio));
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string cad1 = "";
        string cad2 = "";


        int i = txtBusquedaProducto.Text.IndexOf('*');
        cad1 = txtBusquedaProducto.Text.Split('*')[0].ToString();
        if (i > -1)
            cad2 = txtBusquedaProducto.Text.Split('*')[1].ToString();

        //Insertar estadística
        Estadisticas.InsertarBusqueda(cad1, cad2, Contexto.Usuario, Contexto.IdTerminal);

        //Response.Redirect("StockEnTienda.aspx?Producto=" + cad1 + "&Talla=" + cad2);
        //Dirección a la que tiene qeu reenviar EleccionProducto
        string returnUrl = Server.UrlEncode(Constantes.Paginas.StockEnTienda + "?Talla=" + cad2);

        //Direccion de EleccionProducto con los parámetros del filtro de artículo a buscar y de la dirección a la que tiene que redirigir
        //EleccionProducto.aspx?Filtro=1234&ReturnUrl=StockEnTienda%3FTalla=38
        string urlEleccionProducto = Constantes.Paginas.EleccionProducto + "?" + Constantes.QueryString.FiltroArticulo + "=" + cad1 +
                                     "&" + Constantes.QueryString.ReturnUrl + "=" + returnUrl;

        Response.Redirect(urlEleccionProducto);
    }

  

  }


    

   


 