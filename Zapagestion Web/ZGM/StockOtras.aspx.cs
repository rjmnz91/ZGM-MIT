using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Resources;
using AVE;

public partial class StockOtras :  AVE.CLS.Cls_Session 
{
    struct IndexColumnas
    {
        internal const int IdTienda = 0;
        internal const int Tienda = 1;
        internal const int InicioTallas = 2;
    }
        
    protected void Page_Load(object sender, EventArgs e)
    {
        DataView dvORderTalla;
        String[] Tallas;
        String StrCadena=String.Empty ;

        
        if (!Page.IsPostBack)
        {
            //Tablas de resultados de BD
            SDSStock.SelectParameters["IdTienda"].DefaultValue = Contexto.IdTienda;
            DataTable dtTallas = ((DataView)SDSStock.Select(new DataSourceSelectArguments())).ToTable(true, new string[] { "Talla" });
            DataTable dtStock = ((DataView)SDSStock.Select(new DataSourceSelectArguments())).Table.DataSet.Tables[0];


            if (Request.QueryString["Talla"] != null)
            {
                Tallas = Request.QueryString["Talla"].ToString().Trim().Split(new Char[] { '|' });
                
                foreach (String strValor in Tallas)
                {
                    StrCadena = "'" + strValor + "',";

                }

                StrCadena = StrCadena.Substring(0, StrCadena.Length - 1);
            }


            //se ha metido en un dataview para poder ordenar las tallas y pasar al datatable Inicial
            dvORderTalla = dtTallas.DefaultView;
            if (StrCadena.Length > 0)
            {
                DataView dvStock = dtStock.DefaultView;
                dvStock.RowFilter = "Talla in (" + StrCadena + ")  ";
                dtStock = dvStock.ToTable().Copy();
                dvORderTalla.RowFilter = "Talla in (" + StrCadena + ")";
            }
   
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
                dtResultado.Columns.Add(dr["Talla"].ToString(), typeof(int));       //Es un entero pues albergará la cantidad de stock
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

            //Establecemos las demás etiquetas. Debemos hacerlo antes del Bindd del grid para que tengan valor durante RowDataBound
            if (dtStock.Rows.Count > 0)
            {
                lblDescripcion.Text = dtStock.Rows[0]["Descripcion"].ToString() + " " + dtStock.Rows[0]["Color"].ToString();
                lblPrecioValor.Text = string.Format("{0:0.00}", dtStock.Rows[0]["Precio"]);
            }
            else
                lblDescripcion.Text = Resource.ProductoNoEncontrado;

            //Cargamos el grid
            GridStock.DataSource = dtResultado;
            GridStock.DataBind();

            //controlar para ver las fotos de otras
            if (GridStock.Rows.Count > 0) 
            {
                btnFoto.Enabled = true;
               
           }
            
        }
    }

    protected void GridStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hl;
            for (int i = IndexColumnas.InicioTallas; i < GridStock.Columns.Count-1; i++)        //la última columna (TOTAL) no tiene enlaces
            {
                if (int.TryParse(e.Row.Cells[i].Text, out aux) && aux > 0)
                {
                    hl = new HyperLink();
                    hl.Text = e.Row.Cells[i].Text;
                //  esta parte se ha desactivado para que no permita hacer solicitudes a otras tiendas
                    hl.NavigateUrl = string.Format("Solicitud.aspx?IdArticulo={0}&Talla={1}&IdTienda={2}&Tienda={3}&Stock={4}",
                                    Request["IdArticulo"].ToString(), 
                                    GridStock.Columns[i].HeaderText, 
                                    e.Row.Cells[IndexColumnas.IdTienda].Text, 
                                    Server.UrlEncode(e.Row.Cells[IndexColumnas.Tienda].Text),
                                    e.Row.Cells[i].Text); 
                    e.Row.Cells[i].Controls.Add(hl);
                }
            }
        }
    }

    protected void btnPrecio_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constantes.Paginas.ConversionMoneda + "?" + Constantes.QueryString.ImporteConversion + "=" + lblPrecioValor.Text);
    }

    protected void btnFoto_Click(object sender, EventArgs e)
    {
        String IDArticulo;

        if (Request.QueryString["IdArticulo"] != null)
        {
            IDArticulo = Request.QueryString["IdArticulo"].ToString();
            Response.Redirect("~/Foto.aspx?IdArticulo=" + IDArticulo);
       }
       
    }

}
