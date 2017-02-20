using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdDataLayer;
using NLog;

namespace AdBsnsLayer
{
    public class Engine
    {
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private  AdBsnsLayer.Devices devices = new Devices();
        private  AdBsnsLayer.TagDetail tagDetail = new TagDetail();
        private AdDataLayer.DataAccess Access = new DataAccess();
        private AdBsnsLayer.JustOnceProcessor _justOnce = new JustOnceProcessor();
        private AdBsnsLayer.CountInAndOutProcessor _countInAndOutProcessor = new CountInAndOutProcessor(); 
        public Engine()
        {
            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");

            _nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

        public string Process(int DeviceID, string TagNumber, string DeviceValue, string LoginID)
        {
          
            string Message = "";
            try
            {
                Access.InsertEngineLog(DeviceID, TagNumber, DeviceValue, LoginID);
                List<Devices> _devices = devices.GetAllDevices().Where(devices1 => devices1.DeviceID == DeviceID).ToList();
                List<TagDetail> _TagDetails = tagDetail.GetTagDetails(TagNumber);
                if (!(_TagDetails.Count > 0))
                {
                    throw new Exception("Invalid Tag");
                }
                if (!(_devices.Any()))
                {
                    throw new Exception("Device Configuration Won't Exist");
                }

                if (_TagDetails.Count(detail => detail.ExpirationDate <= DateTime.Now) > 0)
                {
                    throw new Exception("Tag Expired");
                }
                int RemainingCount = 0;
                switch (_devices[0].DeviceType)
                {
                    case Devices._DeviceType.JustCount:
                        {
                           RemainingCount = int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString());
                            if (RemainingCount <= 0)
                            {
                                tagDetail.UseTagForActivityButExpired(TagNumber, _devices[0].ActivitiesTagColumnName);
                                throw new Exception("Token Expired - Buy More");
                            }
                            _justOnce.InsertJustOnce(_devices[0].DeviceTable, TagNumber, DeviceID.ToString(), LoginID);
                            tagDetail.UseTagForActivity(TagNumber, _devices[0].ActivitiesTagColumnName);
                            Message = "Enjoy "+ _devices[0].ActivitiesTagColumnName + "!<br\\> You have "+(int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString())-1).ToString()  +" Token(s) Left" ;
                        }
                        break;


                    case Devices._DeviceType.CountInAndOut_In:
                         RemainingCount = int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString());
                        if (RemainingCount <= 0)
                        {
                            tagDetail.UseTagForActivityButExpired(TagNumber, _devices[0].ActivitiesTagColumnName);
                            throw new Exception("Token Expired - Buy More");
                        }
                        var inAndOutTagDetails=  _countInAndOutProcessor.SelectCountInAndOutWithTagNumber(_devices[0].DeviceTable, TagNumber);

                        if (inAndOutTagDetails.Where(processor => processor.OutTime == null).Count()>0 && inAndOutTagDetails.Where(processor => processor.InTime != null).Count()>0)
                        {
                            throw new Exception("Already a Session Exist,Please Finish and Continue"); 
                        }
                        _countInAndOutProcessor.CountInAndOut_In(_devices[0].DeviceTable, _devices[0].DeviceColumn,
                            TagNumber);
                        tagDetail.UseTagForActivity(TagNumber, _devices[0].ActivitiesTagColumnName);
                        Message = "Enjoy " + _devices[0].ActivitiesTagColumnName + "!<br\\> You have " + (int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString()) - 1).ToString() + " Token(s) Left";
                        break;

                    case Devices._DeviceType.CountInAndOut_Out:
                        var inAndOutTagDetails_out = _countInAndOutProcessor.SelectCountInAndOutWithTagNumber(_devices[0].DeviceTable, TagNumber);
                        inAndOutTagDetails_out = inAndOutTagDetails_out.Where(processor => processor.OutTime == null).ToList();
                        if (inAndOutTagDetails_out.Where(processor => processor.OutTime == null).Any())
                        {
                            var OutDetails = _countInAndOutProcessor.UpdateCountInAndOut_Out(_devices[0].DeviceTable,
                                "OutTime", inAndOutTagDetails_out[0].Sno.ToString(), "InTime");

                            tagDetail.CountInAndOut_OutLog(TagNumber, _devices[0].ActivitiesTagColumnName,OutDetails[0].TotalDurationInMinutes.ToString()
                                );
                            Message = "Congragulations! You Sucessfully finished the " + _devices[0].ActivitiesTagColumnName + "in "+OutDetails[0].TotalDurationInMinutes.ToString()+" Minutes." + "!<br\\> You have " + (int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString())).ToString() + " Token(s) Left";

                        }
                        else
                        {
                            throw new Exception("There is no Existing Active Session, Start from the beginning");
                        }
                        break;
                }
              



               

                return Message;
            }
            catch (Exception ex)
            {
               return ex.Message;
            }
        }
         
    }
}
