using System;
using System.Data;
using System.Text;
using System.Web;

namespace CMS.Web.Util
{
    public class DataExports
    {
        public static void ExportToExcel(HttpContext Context, DataTable dataTable, string excelSheet, string fileName)
        {
            //
            //export to excel
            //
            Context.Response.ClearContent();
            Context.Response.ClearHeaders();
            Context.Response.Buffer = true;
            //EnableViewState         = true;

            try
            {
                Context.Response.ContentType = "application/vnd.ms-excel";
                Context.Response.ContentEncoding = Encoding.Default;
                Context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
                Context.Response.Write(ConvertToExcelAllFieldsFromTable(dataTable, excelSheet));
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
            finally
            {
                Context.Response.Flush();
                Context.Response.Close();
                Context.Response.End();
            }
        }

        #region -- Somethings --
        ////
        //// this method return is an colum from dataset is for deleting or not
        ////
        //private bool isForDeleting(IList columsForDeleting, string targetColum)
        //{
        //    bool actionResult = true;

        //    if (columsForDeleting.Count > 0)
        //    {
        //        for (int i = 0; i < columsForDeleting.Count; i++)
        //        {
        //            if (columsForDeleting[i].ToString() == targetColum)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        actionResult = false;
        //    }

        //    return actionResult;
        //}
        #endregion

        #region -- Unused --
        //
        // this method return an string with only choosen one colums with correct colum's captions
        //
        /*
                private static string ConvertToExcelFewFiledsFromTable(DataTable dt, string sheetName, string[] exportFieldsName,
                                                                int[] tableColums)
                {
                    //
                    // Define an error export message
                    //
                    const string lenghtError = "The number of colum's names has to be equal of number of colum's id!";

                    //
                    // In case that colum's names and exported fields are equal as numbers going
                    // to export, otherwise generate an export error.
                    //
                    if (exportFieldsName.Length != tableColums.Length)
                    {
                        //
                        // Return an export error.
                        //
                        return lenghtError;
                    }
                    DataTable dtUpdated = dt.Copy();

                    //
                    // Going to update and remove necessary Colum's captions with new one
                    //
                    for (int i = 0; i < dtUpdated.Columns.Count; i++)
                    {
                        for (int j = 0; j < exportFieldsName.Length; j++)
                        {
                            if (tableColums[j] == i)
                            {
                                //
                                // Going to update Colum's captions with new one
                                //
                                dtUpdated.Columns[i].Caption = exportFieldsName[j].ToUpper();
                            }
                        }
                    }

                    //
                    // Going to remove not necessary colums
                    //
                    string columsNoForDeleting = "";

                    for (int i = 0; i < tableColums.Length; i++)
                    {
                        columsNoForDeleting = columsNoForDeleting + ";" + dtUpdated.Columns[tableColums[i]].ColumnName;
                    }


                    ArrayList columsForRemove = new ArrayList();

                    for (int j = 0; j < dtUpdated.Columns.Count; j++)
                    {
                        if (columsNoForDeleting.IndexOf(dtUpdated.Columns[j].ColumnName, 0) == -1)
                        {
                            columsForRemove.Add(dtUpdated.Columns[j].ColumnName);
                        }
                    }

                    if (columsForRemove.Count > 0)
                    {
                        dtUpdated.PrimaryKey = null;

                        for (int i = 0; i < columsForRemove.Count; i++)
                        {
                            dtUpdated.Columns.Remove(columsForRemove[i].ToString());
                        }
                    }

                    //
                    // Call again the export function into excel for whole datatable
                    //
                    return ConvertToExcelAllFieldsFromTable(dtUpdated, sheetName);
                }
        */
        #endregion
        //
        // this method return an string with all colums with original colum's captions
        //
        private static string ConvertToExcelAllFieldsFromTable(DataTable dt, string sheetName)
        {
            //
            // Define a few local constatnts
            //
            const string notAVDataType = "Unknowable data type";

            StringBuilder sb = new StringBuilder();

            //
            // Define the title color
            //
            const string titleColor = "Navy";

            //
            // Going to define the excel styles
            //
            sb.Append("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" \n\r");
            sb.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\" \n\r");
            sb.Append("xmlns=\"http://www.w3.org/TR/REC-html40\"> \n\r");
            sb.Append("<head> \n\r");
            sb.Append("<meta http-equiv=Content-Type content=\"text/html; charset=unicode\"> \n\r");
            sb.Append("<meta name=ProgId content=Excel.Sheet>");
            sb.Append("<meta name=Generator content=\"Microsoft Excel 11\"> \n\r");
            sb.Append("<link rel=File-List href=\"gopro_files/filelist.xml\"> \n\r");
            sb.Append("<link rel=Edit-Time-Data href=\"gopro_files/editdata.mso\"> \n\r");
            sb.Append("<link rel=OLE-Object-Data href=\"gopro_files/oledata.mso\"> \n\r");
            sb.Append("<style> \n\r");
            sb.Append("<!--table \n\r");
            sb.Append("{mso-displayed-decimal-separator:\"\\,\"; \n\r");
            sb.Append("	mso-displayed-thousand-separator:\" \";} \n\r");
            sb.Append("@page \n\r");
            sb.Append(" {margin:1.0in .75in 1.0in .75in; \n\r");
            sb.Append("	mso-header-margin:.5in; \n\r");
            sb.Append("	mso-footer-margin:.5in;} \n\r");
            sb.Append("tr \n\r");
            sb.Append("	{mso-height-source:auto;} \n\r");
            sb.Append(" col \n\r");
            sb.Append("	{mso-width-source:auto;} \n\r");
            sb.Append(" br \n\r");
            sb.Append(" {mso-data-placement:same-cell;} \n\r");
            sb.Append(".style0 \n\r");
            sb.Append("	{mso-number-format:General; \n\r");
            sb.Append("	text-align:general; \n\r");
            sb.Append(" vertical-align:bottom; \n\r");
            sb.Append("	white-space:nowrap; \n\r");
            sb.Append("	mso-rotate:0; \n\r");
            sb.Append(" mso-background-source:auto; \n\r");
            sb.Append(" mso-pattern:auto; \n\r");
            sb.Append("	color:windowtext; \n\r");
            sb.Append("	font-size:10.0pt; \n\r");
            sb.Append("	font-weight:400; \n\r");
            sb.Append("	font-style:normal; \n\r");
            sb.Append("	text-decoration:none; \n\r");
            sb.Append(" font-family:Arial; \n\r");
            sb.Append(" mso-generic-font-family:auto; \n\r");
            sb.Append(" mso-font-charset:204; \n\r");
            sb.Append("	border:none; \n\r");
            sb.Append(" mso-protection:locked visible; \n\r");
            sb.Append("	mso-style-name:Normal; \n\r");
            sb.Append("	mso-style-id:0;} \n\r");
            sb.Append("	td \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	padding-top:1px; \n\r");
            sb.Append("	padding-right:1px; \n\r");
            sb.Append("	padding-left:1px; \n\r");
            sb.Append("	mso-ignore:padding; \n\r");
            sb.Append("	color:windowtext; \n\r");
            sb.Append("	font-size:10.0pt; \n\r");
            sb.Append("	font-weight:400; \n\r");
            sb.Append("	font-style:normal; \n\r");
            sb.Append("	text-decoration:none; \n\r");
            sb.Append("	font-family:Arial; \n\r");
            sb.Append("	mso-generic-font-family:auto; \n\r");
            sb.Append("	mso-font-charset:204; \n\r");
            sb.Append("	mso-number-format:General; \n\r");
            sb.Append("	text-align:general; \n\r");
            sb.Append("	vertical-align:bottom; \n\r");
            sb.Append(" border:none; \n\r");
            sb.Append("	mso-background-source:auto; \n\r");
            sb.Append("	mso-pattern:auto; \n\r");
            sb.Append("	mso-protection:locked visible; \n\r");
            sb.Append("	white-space:nowrap; \n\r");
            sb.Append("	mso-rotate:0;} \n\r");
            sb.Append(".xl24 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	color:white; \n\r");
            sb.Append("	font-weight:700; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif; \n\r");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	text-align:center; \n\r");
            sb.Append("	vertical-align:middle; \n\r");
            sb.Append("	border:.5pt solid windowtext; \n\r");
            sb.Append("	background:" + titleColor + "; \n\r");
            sb.Append("	mso-pattern:auto none; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append(".xl25 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	color:white; \n\r");
            sb.Append("	font-weight:700; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif; \n\r");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	text-align:center; \n\r");
            sb.Append("	vertical-align:middle; \n\r");
            sb.Append("	border-top:.5pt solid windowtext; \n\r");
            sb.Append("	border-right:.5pt solid windowtext; \n\r");
            sb.Append("	border-bottom:.5pt solid windowtext; \n\r");
            sb.Append("	border-left:none; \n\r");
            sb.Append("	background:blue; \n\r");
            sb.Append("	mso-pattern:auto none; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append(".xl26 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif; \n\r");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	border-top:none; \n\r");
            sb.Append("	border-right:.5pt solid windowtext; \n\r");
            sb.Append("	border-bottom:.5pt solid windowtext; \n\r");
            sb.Append("	border-left:.5pt solid windowtext; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append(".xl27 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif; \n\r");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	border-top:none; \n\r");
            sb.Append("	border-right:.5pt solid windowtext; \n\r");
            sb.Append("	border-bottom:.5pt solid windowtext; \n\r");
            sb.Append("	border-left:none; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append(".xl28 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif;");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	mso-number-format:\"\\#\\,\\#\\#0\\.00\\\\ \\0022ыт\\0022\"; \n\r");
            sb.Append("	border-top:none; \n\r");
            sb.Append("	border-right:.5pt solid windowtext; \n\r");
            sb.Append("	border-bottom:.5pt solid windowtext; \n\r");
            sb.Append("	border-left:none; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append(".xl29 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif; \n\r");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	mso-number-format:Standard; \n\r");
            sb.Append("	border-top:none; \n\r");
            sb.Append("	border-right:.5pt solid windowtext; \n\r");
            sb.Append(" border-bottom:.5pt solid windowtext; \n\r");
            sb.Append("	border-left:none; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append(".xl30 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif; \n\r");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	mso-number-format:\"\\#\\,\\#\\#0\"; \n\r");
            sb.Append("	border-top:none; \n\r");
            sb.Append("	border-right:.5pt solid windowtext; \n\r");
            sb.Append("	border-bottom:.5pt solid windowtext; \n\r");
            sb.Append("	border-left:none; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append(".xl31 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif; \n\r");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	mso-number-format:\"dd\\\\\\.m\\\\\\.yyyy\\\\ \\0022y\\.\\0022\\;\\@\"; \n\r");
            sb.Append("	border-top:none; \n\r");
            sb.Append("	border-right:.5pt solid windowtext; \n\r");
            sb.Append("	border-bottom:.5pt solid windowtext; \n\r");
            sb.Append("	border-left:none; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append(".xl32 \n\r");
            sb.Append("	{mso-style-parent:style0; \n\r");
            sb.Append("	font-family:\"Arial\", sans-serif; \n\r");
            sb.Append("	mso-font-charset:0; \n\r");
            sb.Append("	mso-number-format:\"\\[$-409\\]h\\:mm\\\\ AM\\/PM\\;\\@\"; \n\r");
            sb.Append("	border-top:none; \n\r");
            sb.Append("	border-right:.5pt solid windowtext; \n\r");
            sb.Append("	border-bottom:.5pt solid windowtext; \n\r");
            sb.Append("	border-left:none; \n\r");
            sb.Append("	white-space:normal;} \n\r");
            sb.Append("--> \n\r");
            sb.Append("</style> \n\r");
            sb.Append("<!--[if gte mso 9]><xml> \n\r");
            sb.Append(" <x:ExcelWorkbook> \n\r");
            sb.Append(" <x:ExcelWorksheets> \n\r");
            sb.Append("  <x:ExcelWorksheet> \n\r");
            sb.Append("   <x:Name>" + sheetName.Trim().ToUpper() + "</x:Name> \n\r");
            sb.Append("    <x:WorksheetOptions> \n\r");
            sb.Append("     <x:Print> \n\r");
            sb.Append("      <x:ValidPrinterInfo/> \n\r");
            sb.Append("      <x:PaperSizeIndex>9</x:PaperSizeIndex> \n\r");
            sb.Append("      <x:HorizontalResolution>600</x:HorizontalResolution> \n\r");
            sb.Append("      <x:VerticalResolution>600</x:VerticalResolution> \n\r");
            sb.Append("     </x:Print> \n\r");
            sb.Append("     <x:Selected/> \n\r");
            sb.Append("     <x:DoNotDisplayGridlines/> \n\r");
            sb.Append("     <x:Panes> \n\r");
            sb.Append("      <x:Pane> \n\r");
            sb.Append("       <x:Number>3</x:Number> \n\r");
            sb.Append("       <x:ActiveRow>17</x:ActiveRow> \n\r");
            sb.Append("       <x:ActiveCol>2</x:ActiveCol> \n\r");
            sb.Append("      </x:Pane> \n\r");
            sb.Append("     </x:Panes> \n\r");
            sb.Append("     <x:ProtectContents>False</x:ProtectContents> \n\r");
            sb.Append("     <x:ProtectObjects>False</x:ProtectObjects> \n\r");
            sb.Append("     <x:ProtectScenarios>False</x:ProtectScenarios> \n\r");
            sb.Append("    </x:WorksheetOptions> \n\r");
            sb.Append("   </x:ExcelWorksheet> \n\r");
            sb.Append("  </x:ExcelWorksheets> \n\r");
            sb.Append("  <x:WindowHeight>12660</x:WindowHeight> \n\r");
            sb.Append("  <x:WindowWidth>19020</x:WindowWidth> \n\r");
            sb.Append("  <x:WindowTopX>120</x:WindowTopX> \n\r");
            sb.Append("  <x:WindowTopY>105</x:WindowTopY> \n\r");
            sb.Append("  <x:ProtectStructure>False</x:ProtectStructure> \n\r");
            sb.Append("  <x:ProtectWindows>False</x:ProtectWindows> \n\r");
            sb.Append(" </x:ExcelWorkbook> \n\r");
            sb.Append("</xml><![endif]--> \n\r");
            sb.Append("</head> \n\r");
            sb.Append("<body link=blue vlink=purple> \n\r");
            sb.Append("<table x:str border=0 cellpadding=0 cellspacing=0 width=277 style='border-collapse: \n\r");
            sb.Append(" collapse;table-layout:fixed;width:208pt'> \n\r");
            sb.Append(" <col width=179 style='mso-width-source:userset;mso-width-alt:6546;width:134pt'> \n\r");
            sb.Append(" <col width=98 style='mso-width-source:userset;mso-width-alt:3584;width:74pt'> \n\r");

            //
            // Going to generate the excel content
            //
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    //
                    // Going to generate the titles of Excel colums
                    //
                    sb.Append("<tr height=20 style='height:15.0pt'> \n\r");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string columLenght = dt.Columns[i].ColumnName.Length.ToString().Trim();
                        string columName = dt.Columns[i].Caption.Trim().ToUpper();
                        sb.Append("<td height=20 class=xl24 width=" + columLenght + "pt style='height:15.0pt;width:" +
                                  columLenght + "pt'>" + columName + "</td> \n\r");
                    }
                    sb.Append("</tr> \n\r");

                    //
                    // Going to generate the contain of Excel file
                    //
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //
                        // Going to generate the rows from excel/table contents row by row
                        //
                        sb.Append("<tr height=20 style='height:15.0pt'> \n\r");
                        //
                        // Going to generate excel data cell by cell from current table
                        //
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string ourDBType = dt.Columns[j].DataType.ToString().ToUpper();
                            switch (ourDBType)
                            {
                                case "SYSTEM.STRING":
                                    {
                                        string ourValue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=x126 width=" + ourValueSize + " x:str=\"'" +
                                                  ourValue + "\">" + ourValue + "</td> \n\r");
                                        break;
                                    }
                                case "SYSTEM.GUID":
                                    {
                                        string ourValue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=x126 width=" + ourValueSize + " x:str=\"'" +
                                                  ourValue + "\">" + ourValue + "</td> \n\r");
                                        break;
                                    }
                                case "SYSTEM.DOUBLE":
                                    {
                                        string ourValue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=xl29 width=" + ourValueSize + "  x:num=\"\">" +
                                                  ourValue + "</td> \n\r");
                                        break;
                                    }
                                case "SYSTEM.DECIMAL":
                                    {
                                        string ourValue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=xl29 width=" + ourValueSize + " x:num=\"\">" +
                                                  ourValue + "</td> \n\r");
                                        break;
                                    }
                                case "SYSTEM.INT16":
                                    {
                                        string ourValue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=xl30 width=" + ourValueSize + " x:num>" +
                                                  ourValue + "</td> \n\r");
                                        break;
                                    }
                                case "SYSTEM.INT32":
                                    {
                                        string ourValue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=xl30 width=" + ourValueSize + " x:num>" +
                                                  ourValue + "</td> \n\r");
                                        break;
                                    }
                                case "SYSTEM.INT64":
                                    {
                                        string ourValue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=xl30 width=" + ourValueSize + " x:num>" +
                                                  ourValue + "</td> \n\r");
                                        break;
                                    }
                                case "SYSTEM.DATETIME":
                                    {
                                        string ourValue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=xl31 width=" + ourValueSize +
                                                  " x:num=\"'39059\">" + ourValue + "</td> \n\r");
                                        break;
                                    }
                                default:
                                    {
                                        const string ourValue = notAVDataType;
                                        string ourValueSize = ourValue.Length.ToString().Trim();
                                        sb.Append("<td height=20 class=x126 width=" + ourValueSize + " x:str=\"'" +
                                                  ourValue + "\">" + ourValue + "</td> \n\r");
                                        break;
                                    }
                            }
                        }
                        sb.Append("</tr> \n\r");
                    }
                }
            }

            //
            // Finilyze the HTML table
            //
            sb.Append(" <![if supportMisalignedColumns]> \n\r");
            sb.Append(" <tr height=0 style='display:none'> \n\r");
            sb.Append(" <td width=179 style='width:134pt'></td> \n\r");
            sb.Append(" <td width=98 style='width:74pt'></td> \n\r");
            sb.Append(" </tr> \n\r");
            sb.Append(" <![endif]> \n\r");
            sb.Append("</table> \n\r");
            sb.Append("</body> \n\r");
            sb.Append("</html> \n\r");
            return sb.ToString();
        }
    }
}