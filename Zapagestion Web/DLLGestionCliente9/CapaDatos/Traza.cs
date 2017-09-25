using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using log4net.Config;
using System.Diagnostics;
using System.Data;

namespace ModdoOnline.Utiles
{
    public class Traza
    {
        public const string INICIO = "INICIO ";
        public const string FINAL = "FIN ";
        public const string RETURN = "RETURN {0} ";


        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string GetParameters(params object[] parameters)
        {
            StackTrace stackTrace = new StackTrace();

            StringBuilder sb = new StringBuilder();

            sb.Append(" (");

            if (parameters != null || parameters.Count() > 0)
            {
                int i = 0;
                foreach (ParameterInfo paramaterInfo in stackTrace.GetFrame(1).GetMethod().GetParameters())
                {
                    sb.Append(string.Format("{0} = {1}, ", paramaterInfo.Name, parameters[i] != null ? parameters[i].ToString() : string.Empty));
                    i++;
                }
            }
            sb.Append(") ");


            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetDataSetValues(string parameterNameDataset, DataSet DataSet)
        {

            if (DataSet != null || DataSet.Tables.Count > 0)
            {

                StringBuilder sb = new StringBuilder();

                //Establecemos el nombre del dataset
                sb.Append("Parametro " + parameterNameDataset + " - Nombre Dataset: " + DataSet.DataSetName + " (" + DataSet.Tables.Count + ")\r\n");

                //recorremos el dataset por filas y columnas
                foreach (DataTable thisTable in DataSet.Tables)
                {
                    sb.Append(GetDataTableValues(string.Empty, thisTable));
                }
                sb.Append("\r\n");
                return sb.ToString();

            }
            else
            {
                return string.Empty;
            }

        }

        public static string GetDataTableValues(string parameterNameDatatable, DataTable table)
        {
            StringBuilder sb = new StringBuilder();

            //Establecemos el nombre del datatable
            if (!string.IsNullOrEmpty(parameterNameDatatable))
                sb.Append("Parametro " + parameterNameDatatable + " - Nombre DataTable: " + table.TableName + "(" + table.Rows.Count + ")\r\n");
            else
                sb.Append("\t" + table.TableName + "(" + table.Rows.Count + ")\r\n");

            sb.Append("\t\t");
            foreach (DataColumn column in table.Columns)
            {
                sb.Append(column.ColumnName + "(" + column.Ordinal + "); ");
            }
            sb.Append("\r\n");
            // For each row, print the values of each column.
            foreach (DataRow row in table.Rows)
            {
                sb.Append("\t\t");
                foreach (DataColumn column in table.Columns)
                {
                    sb.Append(row[column] + "; ");
                }
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        public static string GetDataViewValues(string parameterNameDataview, DataView Dataview)
        {

            StringBuilder sb = new StringBuilder();

            //Establecemos el nombre del datatable
            if (!string.IsNullOrEmpty(parameterNameDataview))
                sb.Append("Parametro " + parameterNameDataview + " - Nombre DataTable: " + Dataview.Table.TableName + "(" + Dataview.Table.Rows.Count + ")\r\n");
            else
                sb.Append("\t" + Dataview.Table.TableName + "(" + Dataview.Table.Rows.Count + ")\r\n");

            sb.Append("\t\t");

            foreach (DataColumn column in Dataview.Table.Columns)
            {
                sb.Append(column.ColumnName + "(" + column.Ordinal + "); ");
            }

            sb.Append("\r\n");

            // For each row, print the values of each column.
            foreach (DataRow row in Dataview.Table.Rows)
            {
                sb.Append("\t\t");
                foreach (DataColumn column in Dataview.Table.Columns)
                {
                    sb.Append(row[column] + "; ");
                }
                sb.Append("\r\n");
            }
            return sb.ToString();

        }

        

    }
}
