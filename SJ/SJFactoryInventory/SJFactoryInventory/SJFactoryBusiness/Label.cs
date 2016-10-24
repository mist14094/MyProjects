using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using SjFactoryDataAccess;
namespace SJFactoryBusiness
{
    public class ToteLabel
    {
        public int Sno { get; set; }
        public string TobaccoType { get; set; }
        public string TobaccoDesc { get; set; }
        public DateTime  MfgDate { get; set; }
        public int TotalWeightLb { get; set; }
        public float FinalMoisture { get; set; }
        public string NewTote { get; set; }
        public string Rfid { get; set; }
        public int Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDecommissioned { get; set; }
        public int CreatedUserId { get; set; }
        public int? CreateNewToteLabel(string tobaccoType, string tobaccoDesc, DateTime mfgDate, int totalWeightLb , float finalMoisture, string newTote, string rfid, int location, 
             bool isDecommissioned, int createdUserId)
        {

            DataAccess access = new DataAccess();

            try
            {
                var val = access.CreateNewToteLabel(tobaccoType, tobaccoDesc, mfgDate, totalWeightLb, finalMoisture, newTote,
                    rfid, location, isDecommissioned, createdUserId);

                if (val.Rows.Count > 0)
                {
                    return int.Parse(val.Rows[0][0].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public List<ToteLabel> GetAllToteLabels()
        {

            DataAccess access = new DataAccess();

            try
            {
                return ConvertDataTabletoToteLabels(access.GetAllToteLabels());
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }


        public List<ToteLabel> GetToteLabelsDetails(string sno)
        {

            DataAccess access = new DataAccess();

            try
            {
                return ConvertDataTabletoToteLabels(access.GetToteLabelsDetails(sno)); ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<ToteLabel> ConvertDataTabletoToteLabels(DataTable dt)
        {
            List < ToteLabel > listToteLabels = new List<ToteLabel>();
            if (dt != null)
            {
                foreach (DataRow drRow in dt.Rows)
                {
                    ToteLabel label = new ToteLabel();
                    try
                    {
                        label.Sno = int.Parse(drRow["Sno"].ToString());
                        label.TobaccoType = drRow["TobaccoType"].ToString();
                        label.TobaccoDesc = drRow["TobaccoDesc"].ToString();
                        label.MfgDate = DateTime.Parse(drRow["MfgDate"].ToString());
                        label.TotalWeightLb = int.Parse(drRow["TotalWeightLb"].ToString());
                        label.FinalMoisture = float.Parse(drRow["FinalMoisture"].ToString());
                        label.NewTote = drRow["NewTote"].ToString();
                        label.Rfid = drRow["RFID"].ToString();
                        label.Location = int.Parse(drRow["Location"].ToString());
                        label.IsDecommissioned = bool.Parse(drRow["IsDecommissioned"].ToString());
                        label.CreatedUserId = int.Parse(drRow["CreatedUserID"].ToString());
                        label.CreatedDate = DateTime.Parse(drRow["CreatedDate"].ToString());
                    }
                    catch (Exception exception)
                    {
                        
                      
                    }
                 
                    listToteLabels.Add(label);


                }
                return listToteLabels;
            }
            else
            {
                return null;
            }
        }

    }
}
