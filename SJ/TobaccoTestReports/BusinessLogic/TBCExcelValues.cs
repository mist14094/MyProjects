using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;



namespace BusinessLogic
{
    public class TbcExcelValues
    {
        public TbcExcelValues()
        {
        }

        public DateTime ImportedTime
            {
                get { return _importedTime; }
                set { _importedTime = value; }
            }

            public string ProductClass
            {
                get { return _productClass; }
                set { _productClass = value; }
            }

            public bool ValueTobeProcessed
            {
                get { return _valueTobeProcessed; }
                set { _valueTobeProcessed = value; }
            }

            public double RSugarPercentage
            {
                get { return _rSugarPercentage; }
                set { _rSugarPercentage = value; }
            }

            public double TSugarPercentage
            {
                get { return _tSugarPercentage; }
                set { _tSugarPercentage = value; }
            }

            public double NicotinePercentage
            {
                get { return _nicotinePercentage; }
                set { _nicotinePercentage = value; }
            }

            public double Wgt
            {
                get { return _wgt; }
                set { _wgt = value; }
            }

            public string Dil
            {
                get { return _dil; }
                set { _dil = value; }
            }

            public string Identifier
            {
                get { return _identifier; }
                set { _identifier = value; }
            }

            public string Sam
            {
                get { return _sam; }
                set { _sam = value; }
            }

            public int LineNumber
            {
                get { return _lineNumber; }
                set { _lineNumber = value; }
            }

            public int ReferenceId
            {
                get { return _referenceId; }
                set { _referenceId = value; }
            }

            public int Sno
            {
                get { return _sno; }
                set { _sno = value; }
            }

            private int _sno;
            private int _referenceId;
            private int _lineNumber;
            private string _sam;
            private string _identifier;
            private string _dil;
            private double _wgt;
            private double _nicotinePercentage;
            private double _tSugarPercentage;
            private double _rSugarPercentage;
            private bool _valueTobeProcessed;
            private string _productClass;
            private DateTime _importedTime;

            public List<TbcExcelValues> TbcExcelInternals(int sno)
            {
                DataLogic.DataLogic dl = new DataLogic.DataLogic();
                List<TbcExcelValues> tbcExcelInternals = null;
                if (sno!=null)
                {
                    var InternalDetails = new DataTable();
                    InternalDetails = dl.GetFileDetailsInternal(sno: sno);
                    tbcExcelInternals = InternalDetails.AsEnumerable().Select(
                        row => new TbcExcelValues
                        {
                            Dil = row.Field<string>("Dil"),
                            Identifier = row.Field<string>("Identifier"),
                            ImportedTime = row.Field<DateTime>("ImportedTime"),
                            LineNumber = row.Field<int>("LineNumber"),
                            NicotinePercentage = row.Field<double>("NicotinePercentage"),
                            ProductClass = row.Field<string>("ProductClass"),
                            RSugarPercentage = row.Field<double>("RSugarPercentage"),
                            ReferenceId = row.Field<int>("ReferenceID"),
                            Sam = row.Field<string>("Sam"),
                            Sno = row.Field<int>("Sno"),
                            TSugarPercentage = row.Field<double>("TSugarPercentage"),
                            ValueTobeProcessed = row.Field<bool>("ValueTobeProcessed"),
                            Wgt = row.Field<double>("Wgt")
                        }).ToList();
                }
                return tbcExcelInternals;
            }
        
    }
}
