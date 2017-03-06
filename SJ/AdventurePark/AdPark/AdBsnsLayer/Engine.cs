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
        private AdBsnsLayer.CountAndExpireProcesssor _countAndExpireProcesssor = new CountAndExpireProcesssor();
        private AdBsnsLayer.CountAndWaitProcessor _countAndWaitProcessor = new CountAndWaitProcessor();
        private ThrowLeaderBoard _board = new ThrowLeaderBoard();
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

                if (TagNumber == "CountAndWaitValue")
                {
                  TagNumber=  _countAndWaitProcessor.SelectCountAndWaitWithTagNumber(_devices[0].DeviceTable, TagNumber)
                        .OrderByDescending(processor => processor.CreatedDate).First().TagNumber;
                }
                List<TagDetail> _TagDetails = tagDetail.GetTagDetails(TagNumber);
                if (!(_TagDetails.Count > 0))
                {
                    throw new Exception("Invalid Tag");
                }
                if (!(_devices.Any()))
                {
                    throw new Exception("Device Configuration Wont Exist");
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
                            tagDetail.UseTagForActivity(TagNumber, _devices[0].DeviceColumn);
                            Message = "Enjoy "+ _devices[0].DisplayName + "!<br/> You have "+(int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString())-1).ToString()  +" Token(s) Left" ;
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
                        tagDetail.UseTagForActivity(TagNumber, _devices[0].DisplayName);
                        Message = "Enjoy " + _devices[0].DisplayName + "!<br/> You have " + (int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString()) - 1).ToString() + " Token(s) Left";
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
                            Message = "Congratulations! You Sucessfully finished the " + _devices[0].DisplayName + " in "+OutDetails[0].TotalDurationInMinutes.ToString()+" Minutes." + "!<br/> You have " + (int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString())).ToString() + " Token(s) Left";

                        }
                        else
                        {
                            throw new Exception("There is no Existing Active Session, Start from the beginning");
                        }
                        break;


                    case Devices._DeviceType.CountExpire_In:
                        RemainingCount = int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString());
                        if (RemainingCount <= 0)
                        {
                            tagDetail.UseTagForActivityButExpired(TagNumber, _devices[0].DisplayName);
                            throw new Exception("Token Expired - Buy More");
                        }
                        var countAndExpireTagDetails = _countAndExpireProcesssor.SelectCountAndExpireWithTagNumber(_devices[0].DeviceTable, TagNumber);

                        if (countAndExpireTagDetails.Where(processor => processor.OutTime == null).Count() > 0 && countAndExpireTagDetails.Where(processor => processor.InTime != null).Count() > 0)
                        {
                            throw new Exception("Already a Session Exist,Please Finish and Continue");
                        }
                        _countAndExpireProcesssor.CountAndExpire_In(_devices[0].DeviceTable, _devices[0].DeviceColumn,
                           TagNumber);
                        tagDetail.UseTagForActivityWOReducing(TagNumber, _devices[0].DisplayName);
                        Message = "Enjoy " + _devices[0].DisplayName + "!<br/> You have " + (int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString())).ToString() + " Minute(s) Left";
                        break;


                    case Devices._DeviceType.CountExpire_Out:
                        var CountExpire_out = _countAndExpireProcesssor.SelectCountAndExpireWithTagNumber(_devices[0].DeviceTable, TagNumber);
                        CountExpire_out = CountExpire_out.Where(processor => processor.OutTime == null).ToList();
                        if (CountExpire_out.Where(processor => processor.OutTime == null).Any())
                        {
                            var OutDetails = _countAndExpireProcesssor.UpdateCountAndExpire_Out(_devices[0].DeviceTable,
                                "OutTime", CountExpire_out[0].Sno.ToString(), "InTime");
                            int totalminutescalculated = _TagDetails[0].RopeCourseInMinutes -
                                                         OutDetails[0].TotalDurationInMinutes;
                            string SubMess = "";
                            if ((totalminutescalculated) > 0)
                            {
                                _countAndExpireProcesssor.UpdateCountAndExpire_UseTagForActivity(TagNumber,
                                    _devices[0].ActivitiesTagColumnName,
                                    (totalminutescalculated).ToString());

                            }
                            else
                            {
                                SubMess = "<br/> You Overplayed " + totalminutescalculated.ToString() +
                                          " more! But its OK!";
                                totalminutescalculated = 0;
                                _countAndExpireProcesssor.UpdateCountAndExpire_UseTagForActivity(
                                 TagNumber, _devices[0].ActivitiesTagColumnName,
                                (0).ToString());

                            }
                            tagDetail.CountAndExpireProcesssor_OutLog(TagNumber, _devices[0].ActivitiesTagColumnName, OutDetails[0].TotalDurationInMinutes.ToString());
                             //   );
                            Message = "Congratulations! You Sucessfully finished the " + _devices[0].DisplayName + " in " + OutDetails[0].TotalDurationInMinutes.ToString() + " Minutes." + "!<br/> You have " + totalminutescalculated.ToString() + " Minutes(s) Left"+SubMess;

                        }
                        else
                        {
                            throw new Exception("There is no Existing Active Session, Start from the beginning");
                        }
                        break;
                    case Devices._DeviceType.CountAndWait_Counter:

                        _countAndWaitProcessor.UpdateExpiredCountAndWait(_devices[0].DeviceTable);
                        RemainingCount = int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString());
                        if (RemainingCount <= 0)
                        {
                            tagDetail.UseTagForActivityButExpired(TagNumber, _devices[0].ActivitiesTagColumnName);
                            throw new Exception("Token Expired - Buy More");
                        }
                        var CountAndWaitTagDetails = _countAndWaitProcessor.SelectCountAndWaitWithTagNumber(_devices[0].DeviceTable, TagNumber);

                        if(CountAndWaitTagDetails.Any(processor => processor.Value == null))
                        {
                            throw new Exception("Already a Session Exist,Please Finish and Continue");
                        }
                        //if (inAndOutTagDetails.Where(processor => processor.OutTime == null).Count() > 0 && inAndOutTagDetails.Where(processor => processor.InTime != null).Count() > 0)
                        //{
                        //    throw new Exception("Already a Session Exist,Please Finish and Continue");
                        //}
                        _countAndWaitProcessor.InsertCountAndWaitCounter(_devices[0].DeviceTable,TagNumber);
                        tagDetail.UseTagForActivityWOReducing(TagNumber, _devices[0].DisplayName);
                     
                        Message = "Enjoy " + _devices[0].DisplayName + "!<br/> You have " + (int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString()) - 1).ToString() + " Token(s) Left";
                        Message += "<br/> You have Sixty Seconds to finish your Activity";
                        break;

                    case Devices._DeviceType.CountAndWait_Value:
                        _countAndWaitProcessor.UpdateExpiredCountAndWait(_devices[0].DeviceTable);
                        var CountandWait_out = _countAndWaitProcessor.SelectCountAndWaitWithTagNumber(_devices[0].DeviceTable, TagNumber);
                        CountandWait_out = CountandWait_out.Where(processor => processor.Value == null).ToList();

                        if (CountandWait_out.Where(processor => processor.Value == null).Any())
                        {
                            var InsertDetails = _board.InsertThrowLeaderBoard(DeviceID.ToString(), TagNumber, DeviceValue);
                            var OutDetails = _countAndWaitProcessor.UpdateExpiredCountAndWait_Out(_devices[0].DeviceTable,DeviceValue, CountandWait_out[0].Sno.ToString());
                            tagDetail.UseTagForActivity(TagNumber, _devices[0].DeviceColumn);
                            Message = "Congratulations! You Sucessfully finished the " + _devices[0].DisplayName + "!<br/> You have " + (int.Parse(_TagDetails[0].GetType().GetProperty(_devices[0].ActivitiesTagColumnName).GetValue(_TagDetails[0], null).ToString()) - 1).ToString() + " Token(s) Left";

                        }
                        else
                        {
                            throw new Exception("There is no Existing Active Session, Start from the beginning");
                        }
                        break;




                }





                Access.InsertEngineLogWithResults(DeviceID, TagNumber, DeviceValue, LoginID, Message);
                return Message;
            }
            catch (Exception ex)
            {
                Access.InsertEngineLogWithResults(DeviceID, TagNumber, DeviceValue, LoginID, ex.Message);
                return ex.Message;
            }
        }
         
    }
}
