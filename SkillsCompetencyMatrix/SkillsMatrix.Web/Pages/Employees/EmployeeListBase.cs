using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SelectPdf;
using System.Data;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace SkillsMatrix.Web.Pages.Employees
{
    public class EmployeeListBase : ComponentBase
    {

        [Inject]
        public IToastService toastService { get; set; }

        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]
        public IActivityService ActivityService { get; set; }

        [Inject]
        public ISkillsService SkillsService { get; set; }

        [Inject]
        public IPersonService personService { get; set; }

        [Inject]
        public IPersonCompetencies PersonCompetencies { get; set; }

        [Inject]
        public IPersonExpertiseService PersonExpertiseService { get; set; }

        [Inject]
        public IAddressService AddressService { get; set; }
        [Inject]
        public IEducationService educationService { get; set; }

        [Inject]
        public IEmployementHistoryService employementHistoryService { get; set; }

        [Inject]
        public IExpertiseService ExpertiseService { get; set; }

        [Inject]
        public ICompetenciesCategoryService CompetenciesCategoryService { get; set; }

        [Inject]
        public ICompetenciesService CompetenciesService { get; set; }

        protected PersonalInfo Person = new PersonalInfo();

        protected Address address = new Address();
        protected List<Skill> skills = new List<Skill>();
        protected List<Education> educations = new List<Education>();
        protected List<Employment> employments = new List<Employment>();
        protected List<UserActivities> activities = new List<UserActivities>();
        protected List<UserCompetency> userCompetencies = new List<UserCompetency>();
        protected List<UserExpertise> expertises = new List<UserExpertise>();

        public IEnumerable<PersonalInfo> Employees { set; get; }

        [Inject]
        Microsoft.JSInterop.IJSRuntime JS { get; set; }

        public string EmployeeName { set; get; }

        public string Skills { set; get; }

        public string QualificationLevel { set; get; }

        public string Country { set; get; }

        public bool SelectAll { set; get; }

        public string expertiseID { get; set; }

        public string competencyCategoryID { get; set; }

        public string competencyID { get; set; }

        public bool Searching = true;

        public int Percentage;

        protected bool IsDownloading { get; set; }

        public IEnumerable<Expertise> functionalList { get; set; }

        public IEnumerable<CompetencyCategory> competencyCategoryList { get; set; }

        protected List<Competency> competencies { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = await PersonService.GetAllEmployees("", 0, 0, "", "", "", 0);
            functionalList = await ExpertiseService.GetAll();
            competencyCategoryList = await CompetenciesCategoryService.GetCompetencies();



            Searching = false;

        }

        protected async Task Search()
        {
            Searching = true;
            this.StateHasChanged();

            Employees = await PersonService.GetAllEmployees(EmployeeName, Convert.ToInt32(expertiseID), Convert.ToInt32(competencyCategoryID), Skills, QualificationLevel, Country, Convert.ToInt32(competencyID));

            if (Employees.Count() == 0)
            {
                Employees = null;
            }

            EmployeeCheckedList.Clear();

            Searching = false;
            this.StateHasChanged();
        }

        public void Clear()
        {
            EmployeeName = "";
            Skills = "";
            QualificationLevel = "";
            Country = "";
            expertiseID = "0";
            competencyCategoryID = "0";
            competencyID = "0";
            functionalList = null;
            competencyCategoryList = null;

        }

        public async Task DownloadCV()
        {
            IsDownloading = true;
            this.StateHasChanged();

            if (EmployeeCheckedList.Count == 0)
            {
                toastService.ShowError("Please ensure at least one employee is selected for CV Download.", "Error");
            }

            else
            {
                try
                {
                    await ExportToPDF();
                    EmployeeCheckedList.Clear();
                }

                catch (Exception ex)
                {
                    toastService.ShowError("Error downloading CV", "Error");
                }
            }

            IsDownloading = false;
            this.StateHasChanged();
        }

        protected void ViewCV(int ViewUserID)
        {

            NavigationManager.NavigateTo($"/viewCV/" + ViewUserID.ToString());
        }

        public List<PersonalInfo> EmployeeCheckedList { get; set; } = new List<PersonalInfo>();

        public List<ExportInfo> ExportInfoList { get; set; } = new List<ExportInfo>();
        public class ExportInfo{
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public long IdNumber { get; set; }
            public DateTime DateOfBirth { get; set; } = DateTime.Now;
            public string Gender { get; set; }
            public string Nationality { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }


        protected void empSelect(PersonalInfo emp, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (EmployeeCheckedList.Count == Employees.Count())
                {
                    SelectAll = true;
                }

                if (!EmployeeCheckedList.Any(x => x.Id == emp.Id))
                {
                    EmployeeCheckedList.Add(emp);
                }
            }
            else
            {
                SelectAll = false;

                if (EmployeeCheckedList.Any(x => x.Id == emp.Id))
                {
                    EmployeeCheckedList.Remove(emp);
                }
            }
        }

        protected void empSelectAll(object checkedValue)
        {

            if ((bool)checkedValue)
            {
                foreach (var item in Employees)
                {
                    if (!EmployeeCheckedList.Any(x => x.Id == item.Id))
                    {
                        EmployeeCheckedList.Add(item);
                    }
                }

                SelectAll = true;
            }
            else
            {
                foreach (var item in Employees)
                {
                    if (EmployeeCheckedList.Any(x => x.Id == item.Id))
                    {
                        EmployeeCheckedList.Remove(item);
                    }
                }

                SelectAll = false;
            }

            this.StateHasChanged();
        }

        protected bool empCheckSelectStatus(int empID)
        {
            if (EmployeeCheckedList.Any(x => x.Id == empID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void functionalClicked(object functional)
        {
            expertiseID = functional.ToString();
        }

        protected async void competencyCategoryClicked(object competencyCategory)
        {
            competencyCategoryID = competencyCategory.ToString();
            competencies = await CompetenciesService.GetAll(Convert.ToInt32(competencyCategoryID));
            this.StateHasChanged();
        }

        public static byte[] GetZipArchive(List<InMemoryFile> files)
        {
            byte[] archiveFile;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open())
                            zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }


                archiveFile = archiveStream.ToArray();
            }

            return archiveFile;
        }


        //protected async Task ExportToExcel()
        //{
        //    IsDownloading = true;
        //    this.StateHasChanged();

        //    if (EmployeeCheckedList.Count == 0)
        //    {
        //        toastService.ShowError("Please ensure at least one employee is selected for CV Export.", "Error");
        //    }

        //    else
        //    {
        //        try
        //        {
        //            var workbook = new ClosedXML.Excel.XLWorkbook();
        //            workbook.AddWorksheet("sheetName");
        //            var ws = workbook.Worksheet("sheetName");

        //            int row = 1;
        //            foreach (object item in EmployeeCheckedList)
        //            {
        //                ws.Cell("A" + row.ToString()).Value = item.ToString();
        //                row++;
        //            }
        //            string path = @"c:\users\mtshabalala\documents";

        //            workbook.SaveAs("yourExcel.xlsx");

        //            //DataTable dtProduct = ConvertToDataTable(EmployeeCheckedList);

        //            //using (ClosedXML.Excel.XLWorkbook woekBook = new ClosedXML.Excel.XLWorkbook())
        //            //{
        //            //    woekBook.Worksheets.Add(dtProduct);
        //            //    using (MemoryStream stream = new MemoryStream())
        //            //    {
        //            //        woekBook.SaveAs(stream);
        //            //        //File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductDetails.xlsx");
        //            //    }
        //            //}
        //        }

        //        catch (Exception ex)
        //        {
        //            toastService.ShowError("Error Export CVs", "Error");
        //        }
        //    }

        //    IsDownloading = false;
        //    this.StateHasChanged();


        //}

        //public void CreateWordTableWithDataTable(DataTable dt)
        //{
        //    int RowCount = dt.Rows.Count; int ColumnCount = dt.Columns.Count;
        //    Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];
        //    //int RowCount = 0; int ColumnCount = 0;
        //    int r = 0;
        //    for (int c = 0; c <= ColumnCount-1; c++)
        //    {
        //        DataArray[r, c] = dt.Columns[c].ColumnName;
        //        for (r = 0; r <= RowCount -1; r++)
        //        {
        //            DataArray[r, c] = dt.Rows[r][c];
        //        } //end row loop
        //    } //end column loop

        //    Word.Document oDoc = new Word.Document();
        //    oDoc.Application.Visible = true;
        //    oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

        //    dynamic oRange = oDoc.Content.Application.Selection.Range;
        //    String oTemp = "";
        //    for (r = 0; r <= RowCount -1; r++)
        //    {
        //        for (int c = 0; c <= ColumnCount -1; c++)
        //        {
        //            oTemp = oTemp + DataArray[r, c] + "\t";

        //        }
        //    }

        //    oRange.Text = oTemp;

        //    object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
        //    object Format = Word.WdTableFormat.wdTableFormatWeb1;
        //    object ApplyBorders = true;
        //    object AutoFit = true;

        //    object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;
        //    oRange.ConvertToTable(ref Separator,
        //ref RowCount, ref ColumnCount, Type.Missing, ref Format,
        //ref ApplyBorders, Type.Missing, Type.Missing, Type.Missing,
        // Type.Missing, Type.Missing, Type.Missing,
        // Type.Missing, ref AutoFit, ref AutoFitBehavior,
        // Type.Missing);

        //    oRange.Select();
        //    oDoc.Application.Selection.Tables[1].Select();
        //    oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
        //    oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
        //    oDoc.Application.Selection.Tables[1].Rows[1].Select();
        //    oDoc.Application.Selection.InsertRowsAbove(1);
        //    oDoc.Application.Selection.Tables[1].Rows[1].Select();

        //    //gotta do the header row manually
        //    for (int c = 0; c <= ColumnCount-1; c++)
        //    {
        //        oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = dt.Columns[c].ColumnName;
        //    }

        //    oDoc.Application.Selection.Tables[1].Rows[1].Select();
        //    oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

        //}




        //protected async Task ExportToWord()
        //{
        //    IsDownloading = true;
        //    this.StateHasChanged();

        //    if (EmployeeCheckedList.Count == 0)
        //    {
        //        toastService.ShowError("Please ensure at least one employee is selected for CV Export.", "Error");
        //    }

        //    else
        //    {
        //        try
        //        {
        //            var dt = ConvertToDataTable(EmployeeCheckedList);
        //            CreateWordTableWithDataTable(dt);

        //        }

        //        catch (Exception ex)
        //        {
        //            toastService.ShowError("Error Export CVs", "Error");
        //        }
        //    }

        //    IsDownloading = false;
        //    this.StateHasChanged();


        //}
        //protected async Task ExportToExcel()
        //{
        //    IsDownloading = true;
        //    this.StateHasChanged();

        //    if (EmployeeCheckedList.Count == 0)
        //    {
        //        toastService.ShowError("Please ensure at least one employee is selected for CV Export.", "Error");
        //    }

        //    else
        //    {
        //        try
        //        {
        //            string fileName = "CVExport.xlsx";
        //            Console.WriteLine("Please give a location to save :");
        //            string location = "c:/users/mtshabalala/documents";
        //            string customExcelSavingPath = location + "\\" + fileName;
        //            GenerateExcel(ConvertToDataTable(EmployeeCheckedList), customExcelSavingPath);
        //        }

        //        catch (Exception ex)
        //        {
        //            toastService.ShowError("Error Export CVs", "Error");
        //        }
        //    }

        //    IsDownloading = false;
        //    this.StateHasChanged();


        //}

        //public static void GenerateExcel(DataTable dataTable, string path)
        //{

        //    DataSet dataSet = new DataSet();
        //    dataSet.Tables.Add(dataTable);

        //    // create a excel app along side with workbook and worksheet and give a name to it  
        //    Excel.Application excelApp = new Excel.Application();
        //    Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
        //    Excel._Worksheet xlWorksheet = (Excel._Worksheet)excelWorkBook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;
        //    foreach (DataTable table in dataSet.Tables)
        //    {
        //        //Add a new worksheet to workbook with the Datatable name  
        //        Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Sheets.Add();
        //        excelWorkSheet.Name = table.TableName;

        //        // add all the columns  
        //        for (int i = 1; i < table.Columns.Count + 1; i++)
        //        {
        //            excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
        //        }

        //        // add all the rows  
        //        for (int j = 0; j < table.Rows.Count; j++)
        //        {
        //            for (int k = 0; k < table.Columns.Count; k++)
        //            {
        //                excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
        //            }
        //        }
        //    }
        //    // excelWorkBook.Save(); -> this is save to its default location  
        //    excelWorkBook.SaveAs(path); // -> this will do the custom  
        //    excelWorkBook.Close();
        //    excelApp.Quit();
        //}

        // T is a generic class  
        static DataTable ConvertToDataTable<T>(List<T> models)
        {
            // creating a data table instance and typed it as our incoming model   
            // as I make it generic, if you want, you can make it the model typed you want.  
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties of that model  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Loop through all the properties              
            // Adding Column name to our datatable  
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names    
                dataTable.Columns.Add(prop.Name);
            }
            // Adding Row and its value to our dataTable  
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows    
                    values[i] = Props[i].GetValue(item, null);
                }
                // Finally add value to datatable    
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }


        public static DataSet ToDataSet<T>(IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }

        protected async Task ExportToWord()
        {
                IsDownloading = true;
                this.StateHasChanged();

                if (EmployeeCheckedList.Count == 0)
                {
                    toastService.ShowError("Please ensure at least one employee is selected for CV Export.", "Error");
                }

                else
                {
                    try
                    {
                        
                        foreach (var Employee in EmployeeCheckedList)
                        {
                            ExportInfo export = new ExportInfo
                            {
                                DateOfBirth = Employee.DateOfBirth,
                                Email= Employee.Email,
                                FirstName= Employee.FirstName,
                                Gender= Employee.Gender,
                                IdNumber= Employee.IdNumber,
                                LastName= Employee.LastName,
                                Nationality= Employee.Nationality,
                               Phone= Employee.Phone,
                               Title= Employee.Title

                            };
                            ExportInfoList.Add(export);
                        }
                        var data = ConvertToDataTable(ExportInfoList);


                    using (MemoryStream mem = new MemoryStream())
                    {
                        WordprocessingDocument doc = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document);
                        MainDocumentPart mainDocPart = doc.AddMainDocumentPart();
                        mainDocPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                        Body body = new Body();
                        mainDocPart.Document.Append(body);

                        DocumentFormat.OpenXml.Wordprocessing.Table table = new DocumentFormat.OpenXml.Wordprocessing.Table();

                        ////// Create Table Borders

                        //TableBorders tblBorders = new TableBorders();



                        //DocumentFormat.OpenXml.Drawing.TopBorder topBorder = new DocumentFormat.OpenXml.Drawing.TopBorder();

                        //topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                        //topBorder.Color = "CC0000";

                        //tblBorders.AppendChild(topBorder);



                        //BottomBorder bottomBorder = new BottomBorder();

                        //bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                        //bottomBorder.Color = "CC0000";

                        //tblBorders.AppendChild(bottomBorder);



                        //RightBorder rightBorder = new RightBorder();

                        //rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                        //rightBorder.Color = "CC0000";

                        //tblBorders.AppendChild(rightBorder);



                        //LeftBorder leftBorder = new LeftBorder();

                        //leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                        //leftBorder.Color = "CC0000";

                        //tblBorders.AppendChild(leftBorder);



                        //InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();

                        //insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                        //insideHBorder.Color = "CC0000";

                        //tblBorders.AppendChild(insideHBorder);



                        //InsideVerticalBorder insideVBorder = new InsideVerticalBorder();

                        //insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                        //insideVBorder.Color = "CC0000";

                        //tblBorders.AppendChild(insideVBorder);
                        for (int i = 0; i < data.Rows.Count; ++i)
                        {
                            TableRow row = new TableRow();
                            for (int j = 0; j < data.Columns.Count; j++)
                            {
                                TableCell cell = new TableCell();
                                cell.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(data.Rows[i][j].ToString()))));
                                cell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1500" }));
                                row.Append(cell);
                            }
                            table.Append(row);
                        }
                        body.Append(table);

                        doc.MainDocumentPart.Document.Save();

                        

                        doc.Dispose();


                        string file = "CVExports.docx";
                        await JS.SaveAs(file, mem.ToArray());
                    }
                }

                    catch (Exception ex)
                    {
                        toastService.ShowError("Error Export CVs", "Error");
                    }
                }

                IsDownloading = false;
                this.StateHasChanged();

        }
        protected async Task ExportToExcel()
        {

                IsDownloading = true;
            this.StateHasChanged();

            if (EmployeeCheckedList.Count == 0)
            {
                toastService.ShowError("Please ensure at least one employee is selected for CV Export.", "Error");
            }

            else
            {
                try
                {
                        foreach (var Employee in EmployeeCheckedList)
                        {
                            ExportInfo export = new ExportInfo
                            {
                                DateOfBirth = Employee.DateOfBirth,
                                Email = Employee.Email,
                                FirstName = Employee.FirstName,
                                Gender = Employee.Gender,
                                IdNumber = Employee.IdNumber,
                                LastName = Employee.LastName,
                                Nationality = Employee.Nationality,
                                Phone = Employee.Phone,
                                Title = Employee.Title

                            };
                            ExportInfoList.Add(export);
                        }
                        var ds = ToDataSet(ExportInfoList);

                    using (MemoryStream mem = new MemoryStream())
                    {
                        using (var workbook = SpreadsheetDocument.Create(mem, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
                        {
                            var workbookPart = workbook.AddWorkbookPart();

                            workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                            workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();

                            foreach (System.Data.DataTable table in ds.Tables)
                            {

                                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                                var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                                sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

                                DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                                string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                                uint sheetId = 1;
                                if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Count() > 0)
                                {
                                    sheetId =
                                        sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                                }

                                DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet() { Id = relationshipId, SheetId = sheetId, Name = table.TableName };
                                sheets.Append(sheet);

                                DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                                List<String> columns = new List<string>();
                                foreach (System.Data.DataColumn column in table.Columns)
                                {
                                    columns.Add(column.ColumnName);

                                    DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName);
                                    headerRow.AppendChild(cell);
                                }


                                sheetData.AppendChild(headerRow);

                                foreach (System.Data.DataRow dsrow in table.Rows)
                                {
                                    DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                                    foreach (String col in columns)
                                    {
                                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString()); //
                                        newRow.AppendChild(cell);
                                    }

                                    sheetData.AppendChild(newRow);
                                }

                            }
                        }


                        string file = "CVExports.xlsx";
                        await JS.SaveAs(file, mem.ToArray());
                    }
                }

                catch (Exception ex)
                {
                    toastService.ShowError("Error Export CVs", "Error");
                }
            }

            IsDownloading = false;
            this.StateHasChanged();

        }



        protected async Task ExportToPeeDEEF()
        {

                IsDownloading = true;
            this.StateHasChanged();

            if (EmployeeCheckedList.Count == 0)
            {
                toastService.ShowError("Please ensure at least one employee is selected for CV Export.", "Error");
            }

            else
            {
                try
                {
                        foreach (var Employee in EmployeeCheckedList)
                        {
                            ExportInfo export = new ExportInfo
                            {
                                DateOfBirth = Employee.DateOfBirth,
                                Email = Employee.Email,
                                FirstName = Employee.FirstName,
                                Gender = Employee.Gender,
                                IdNumber = Employee.IdNumber,
                                LastName = Employee.LastName,
                                Nationality = Employee.Nationality,
                                Phone = Employee.Phone,
                                Title = Employee.Title

                            };
                            ExportInfoList.Add(export);
                        }
                        var dt = ConvertToDataTable(ExportInfoList);

                    using (MemoryStream mem = new MemoryStream())
                    {
                        iTextSharp.text.Document document = new iTextSharp.text.Document();
                        iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, mem);
                        document.Open();
                        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 5);

                        iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(dt.Columns.Count);
                        iTextSharp.text.pdf.PdfPRow row = null;
                        float[] widths = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };

                        table.SetWidths(widths);

                        table.WidthPercentage = 100;
                        int iCol = 0;
                        string colname = "";
                        iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("CV's"));

                        cell.Colspan = dt.Columns.Count;

                        foreach (DataColumn c in dt.Columns)
                        {

                            table.AddCell(new iTextSharp.text.Phrase(c.ColumnName, font5));
                        }

                        foreach (DataRow r in dt.Rows)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                table.AddCell(new iTextSharp.text.Phrase(r[0].ToString(), font5));
                                table.AddCell(new iTextSharp.text.Phrase(r[1].ToString(), font5));
                                table.AddCell(new iTextSharp.text.Phrase(r[2].ToString(), font5));
                                table.AddCell(new iTextSharp.text.Phrase(r[3].ToString(), font5));
                                table.AddCell(new iTextSharp.text.Phrase(r[4].ToString(), font5));
                                table.AddCell(new iTextSharp.text.Phrase(r[5].ToString(), font5));
                                table.AddCell(new iTextSharp.text.Phrase(r[6].ToString(), font5));
                                table.AddCell(new iTextSharp.text.Phrase(r[7].ToString(), font5));
                                table.AddCell(new iTextSharp.text.Phrase(r[8].ToString(), font5));
                            }
                        }
                        document.Add(table);

                        document.Close();

                        string newfile = "CVExports.pdf";
                        await JS.SaveAs(newfile, mem.ToArray());
                    }
                }

                catch (Exception ex)
                {
                    toastService.ShowError("Error Export CVs", "Error");
                }
            }

            IsDownloading = false;
            this.StateHasChanged();

        }



        protected async Task<string> ExportToPDF()
        {
            return await Task.Run(async () =>
            {
                string pdf_page_size = "A4";
                PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);

                int webPageWidth = 1024;
                try
                {
                    webPageWidth = Convert.ToInt32("1024");
                }
                catch { }

                int webPageHeight = 0;
                try
                {
                    webPageHeight = Convert.ToInt32("0");
                }
                catch { }

                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // set converter options
                converter.Options.PdfPageSize = pageSize;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                converter.Options.MarginBottom = 15;
                converter.Options.MarginTop = 15;
                converter.Options.MarginLeft = 10;
                converter.Options.MarginRight = 10;
                // create a new pdf document converting an url

                List<InMemoryFile> files = new List<InMemoryFile>();
                byte[] zip;

                foreach (var item in EmployeeCheckedList)
                {
                    string url = "";

                    url = NavigationManager.ToAbsoluteUri("/viewPDF/" + item.UserId.ToString()).ToString();

                    PdfDocument doc = converter.ConvertUrl(url);
                    MemoryStream pdfStream = new MemoryStream();

                    doc.Save(pdfStream);
                    doc.Save();
                    doc.Close();

                    files.Add(new InMemoryFile() { FileName = item.FirstName + "_" + item.LastName + "_" + string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now) + "_CV.pdf", Content = pdfStream.ToArray() });

                    pdfStream.Close();
                    pdfStream.Dispose();
                }

                if (files.Count == 1)
                {
                    await JS.SaveAs(files[0].FileName, files[0].Content);

                }

                else
                {
                    zip = GetZipArchive(files);
                    await JS.SaveAs($"DownloadCV_" + string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now) + ".zip", zip.ToArray());
                }

                return "Task Complete";
            });
        }

    }

    public class InMemoryFile
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
