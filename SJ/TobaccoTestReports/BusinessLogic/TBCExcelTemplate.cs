using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataLogic;


namespace BusinessLogic
{
    public class TbcExcelTemplate
    {
        private int _sno;
        private string _fileName;
        private string _sheetName;
        private string _reportName;
        private DateTime _date;
        private string _baseConfiguration;
        private string _description;
        private string _fromLocation;
        private string _toLocation;
        private DateTime _importedTime;
        private bool _isValid;
        List<TbcExcelValues> _tbcExcelValues;

        public TbcExcelTemplate()
        { }



        public List<TbcExcelValues> TbcExcelValues
        {
            get { return _tbcExcelValues; }
            set { _tbcExcelValues = value; }
        }



        public int Sno
        {
            get { return _sno; }
            set { _sno = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public string SheetName
        {
            get { return _sheetName; }
            set { _sheetName = value; }
        }

        public string ReportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string BaseConfiguration
        {
            get { return _baseConfiguration; }
            set { _baseConfiguration = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string FromLocation
        {
            get { return _fromLocation; }
            set { _fromLocation = value; }
        }

        public DateTime ImportedTime
        {
            get { return _importedTime; }
            set { _importedTime = value; }
        }

        public string ToLocation
        {
            get { return _toLocation; }
            set { _toLocation = value; }
        }

        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }




        public List<TbcExcelTemplate> GetFileDetails(int sno)
        {
            var dl = new DataLogic.DataLogic();
            var datatable = dl.GetFileDetails(sno);
            List<TbcExcelTemplate> tbcExcelTemplate = null;

            try
            {
                tbcExcelTemplate = datatable.AsEnumerable().Select(row => new TbcExcelTemplate
                   {
                       BaseConfiguration = row.Field<string>("BaseConfiguration"),
                       Date = row.Field<DateTime>("Date"),
                       Description = row.Field<string>("Description"),
                       FileName = row.Field<string>("FileName"),
                       FromLocation = row.Field<string>("FromLocation"),
                       ImportedTime = row.Field<DateTime>("ImportedTime"),
                       IsValid = row.Field<bool>("IsValid"),
                       ReportName = row.Field<string>("ReportName"),
                       SheetName = row.Field<string>("SheetName"),
                       Sno = row.Field<int>("Sno"),
                       ToLocation = row.Field<string>("ToLocation")
                   }).ToList();

                //    var tbcExcelInternals = TbcExcelValues;
                //    TbcExcelValues.
                //   List<TbcExcelValues> tbcExcelValueses = new List<TbcExcelValues>();

                BusinessLogic.TbcExcelValues tbcExcel = new TbcExcelValues();
                List<TbcExcelValues> tbcExcelValueses = tbcExcel.TbcExcelInternals(sno);
                if (tbcExcelTemplate.Count > 0)
                {
                    if (tbcExcelValueses != null) tbcExcelTemplate[0].TbcExcelValues = tbcExcelValueses;
                }
            }
            catch (Exception ex)
            {


            }


            return tbcExcelTemplate;
        }


    }
}
