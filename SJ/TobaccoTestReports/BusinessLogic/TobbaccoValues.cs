using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
   public class TobbaccoValues
    {

        public TobbaccoValues ()
        {}

        private double _avgNicotinePercentage;
        private double _avgtSugarPercentage;
        private double _avgrSugarPercentage;
        private string _productClass;
        private int _sno;
        private string _filename;
        private DateTime _date;
        private DateTime _importedTime;


        public double AvgNicotinePercentage
        {
            get { return _avgNicotinePercentage; }
            set { _avgNicotinePercentage = value; }
        }

        public double AvgRSugarPercentage
        {
            get { return _avgrSugarPercentage; }
            set { _avgrSugarPercentage = value; }
        }

        public double AvgTSugarPercentage
        {
            get { return _avgtSugarPercentage; }
            set { _avgtSugarPercentage = value; }
        }

        public string ProductClass
        {
            get { return _productClass; }
            set { _productClass = value; }
        }

        public int Sno
        {
            get { return _sno; }
            set { _sno = value; }
        }

        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public DateTime ImportedTime
        {
            get { return _importedTime; }
            set { _importedTime = value; }
        }
    }
}
