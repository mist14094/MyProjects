using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotControlBusiness
{
    public class JobDetails
    {
        public string Job { get; set; }
        public string  StockCode { get; set; }
        public string WareHouse { get; set; }
        public string Line { get; set; }
        public string Bin { get; set; }
        public float UnitQtyReqrd { get; set; }
        public float QtyToMake { get; set; }
        public string KitIssueItem { get; set; }
        public string StockUom { get; set; }
        public string Route { get; set; }
        public float BinQtyOnHand { get; set; }
        public float WareHouseOnHand { get; set; }
        public string StockCodeDesc { get; set; }
        public string StockCodeLongDesc { get; set; }
        public string Uom { get; set; }
        public float QtyIssued { get; set; }
        public float CalculatedRequired { get; set; }


        public List<JobDetails> GetJobDetails(string jobNumber)
        {
            var business = new LcBusiness();
            var dt = business.GetJobDetails(jobNumber);

            return (from DataRow row in dt.Rows
                select new JobDetails()
                {
                    Job = row["Job"].ToString().Trim(),
                    StockCode = row["StockCode"].ToString().Trim(),
                    WareHouse = row["WareHouse"].ToString().Trim(),
                    Line = row["Line"].ToString().Trim(),
                    Bin = row["Bin"].ToString().Trim(),
                    KitIssueItem = row["KitIssueItem"].ToString().Trim(),
                    BinQtyOnHand = float.Parse(row["QtyOnHand1"].ToString().Trim()),
                    WareHouseOnHand = float.Parse(row["QtyOnHand"].ToString().Trim()),
                    StockCodeDesc = row["StockDescription"].ToString().Trim(),
                    StockCodeLongDesc = row["LongDesc"].ToString().Trim(),
                    Uom = row["Uom"].ToString().Trim(),
                    QtyIssued = float.Parse(row["QtyIssued"].ToString().Trim()),
                    UnitQtyReqrd = float.Parse(row["UnitQtyReqd"].ToString().Trim()),
                    QtyToMake = float.Parse(row["QtyToMake"].ToString().Trim()),
                    CalculatedRequired = float.Parse(row["UnitQtyReqd"].ToString().Trim()) * float.Parse(row["QtyToMake"].ToString().Trim())
                }).ToList();
        }
    }

    public class JobMaster
    {
        public string Job { get; set; }
        public string  JobDescr { get; set; }
        public string StockCode { get; set; }
        public string StockDescr { get; set; }
        public string JobClassification { get; set; }
        public string Warehouse { get; set; }
        public string IsComplete { get; set; }
        public float QtyToMake { get; set; }
        public DateTime JobStartDate { get; set; }
        public string Uom { get; set; }


        public JobMaster GetJobMasterDetails(string jobNumber)
        {
            var business = new LcBusiness();
            var dt = business.GetJobMasterDetails(jobNumber);

            return (from DataRow row in dt.Rows
                    select new JobMaster()
                    {
                        Job = row["Job"].ToString().Trim(),
                        StockCode = row["StockCode"].ToString().Trim(),
                        JobDescr = row["JobDescription"].ToString().Trim(),
                        StockDescr = row["StockDescription"].ToString().Trim(),
                        JobClassification = row["JobClassification"].ToString().Trim(),
                        Warehouse = row["Warehouse"].ToString().Trim(),
                        IsComplete = row["Complete"].ToString().Trim(),
                        QtyToMake = float.Parse(row["QtyToMake"].ToString().Trim()),
                        JobStartDate = DateTime.Parse(row["JobStartDate"].ToString().Trim()),
                        Uom = row["StockUom"].ToString().Trim(),
                    }).ToList().FirstOrDefault();
        }
    }
}
