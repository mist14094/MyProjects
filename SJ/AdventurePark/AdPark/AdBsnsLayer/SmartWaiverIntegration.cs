using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdDataLayer;
namespace AdBsnsLayer
{
    public class SmartWaiverIntegration
    {

        DataAccess Access = new DataAccess();

        /// <remarks/>

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class xml
        {

            private decimal api_versionField;

            private xmlParticipant[] participantsField;

            /// <remarks/>
            public decimal api_version
            {
                get
                {
                    return this.api_versionField;
                }
                set
                {
                    this.api_versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("participant", IsNullable = false)]
            public xmlParticipant[] participants
            {
                get
                {
                    return this.participantsField;
                }
                set
                {
                    this.participantsField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipant
        {

            private object[] itemsField;

            private ItemsChoiceType[] itemsElementNameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("SERVERSIDE_WAIVER_START_TS", typeof(uint))]
            [System.Xml.Serialization.XmlElementAttribute("SERVERSIDE_csrf_token", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("checkins", typeof(xmlParticipantCheckins))]
            [System.Xml.Serialization.XmlElementAttribute("completed_at_kiosk", typeof(bool))]
            [System.Xml.Serialization.XmlElementAttribute("completed_from_ip_address", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("customfields", typeof(xmlParticipantCustomfields))]
            [System.Xml.Serialization.XmlElementAttribute("date_accepted_utc", typeof(object))]
            [System.Xml.Serialization.XmlElementAttribute("date_created_utc", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("dob", typeof(System.DateTime), DataType = "date")]
            [System.Xml.Serialization.XmlElementAttribute("firstname", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("how_many_auto_photo_capture_photographs_captured", typeof(byte))]
            [System.Xml.Serialization.XmlElementAttribute("images", typeof(object))]
            [System.Xml.Serialization.XmlElementAttribute("is_adult", typeof(bool))]
            [System.Xml.Serialization.XmlElementAttribute("lastname", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("marketingallowed", typeof(bool))]
            [System.Xml.Serialization.XmlElementAttribute("middlename", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("participant_id", typeof(ulong))]
            [System.Xml.Serialization.XmlElementAttribute("pdf_url", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("pending_email_validation", typeof(bool))]
            [System.Xml.Serialization.XmlElementAttribute("pg_firstname", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("pg_lastname", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("pg_phone", typeof(uint))]
            [System.Xml.Serialization.XmlElementAttribute("phone", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("primary_email", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("primary_email_confirm", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("tags", typeof(xmlParticipantTags))]
            [System.Xml.Serialization.XmlElementAttribute("waiver_id", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("waiver_title", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("waiver_type_guid", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("web_browsers_user_agent", typeof(string))]
            [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public ItemsChoiceType[] ItemsElementName
            {
                get
                {
                    return this.itemsElementNameField;
                }
                set
                {
                    this.itemsElementNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipantCheckins
        {

            private xmlParticipantCheckinsCheckin checkinField;

            /// <remarks/>
            public xmlParticipantCheckinsCheckin checkin
            {
                get
                {
                    return this.checkinField;
                }
                set
                {
                    this.checkinField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipantCheckinsCheckin
        {

            private ulong checkin_idField;

            private string checkin_utcField;

            /// <remarks/>
            public ulong checkin_id
            {
                get
                {
                    return this.checkin_idField;
                }
                set
                {
                    this.checkin_idField = value;
                }
            }

            /// <remarks/>
            public string checkin_utc
            {
                get
                {
                    return this.checkin_utcField;
                }
                set
                {
                    this.checkin_utcField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipantCustomfields
        {

            private xmlParticipantCustomfieldsCustomfield[] customfieldField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("customfield")]
            public xmlParticipantCustomfieldsCustomfield[] customfield
            {
                get
                {
                    return this.customfieldField;
                }
                set
                {
                    this.customfieldField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipantCustomfieldsCustomfield
        {

            private string guidField;

            private string titleField;

            private string valueField;

            /// <remarks/>
            public string guid
            {
                get
                {
                    return this.guidField;
                }
                set
                {
                    this.guidField = value;
                }
            }

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipantTags
        {

            private string[] tagField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("tag")]
            public string[] tag
            {
                get
                {
                    return this.tagField;
                }
                set
                {
                    this.tagField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
        public enum ItemsChoiceType
        {

            /// <remarks/>
            SERVERSIDE_WAIVER_START_TS,

            /// <remarks/>
            SERVERSIDE_csrf_token,

            /// <remarks/>
            checkins,

            /// <remarks/>
            completed_at_kiosk,

            /// <remarks/>
            completed_from_ip_address,

            /// <remarks/>
            customfields,

            /// <remarks/>
            date_accepted_utc,

            /// <remarks/>
            date_created_utc,

            /// <remarks/>
            dob,

            /// <remarks/>
            firstname,

            /// <remarks/>
            how_many_auto_photo_capture_photographs_captured,

            /// <remarks/>
            images,

            /// <remarks/>
            is_adult,

            /// <remarks/>
            lastname,

            /// <remarks/>
            marketingallowed,

            /// <remarks/>
            middlename,

            /// <remarks/>
            participant_id,

            /// <remarks/>
            pdf_url,

            /// <remarks/>
            pending_email_validation,

            /// <remarks/>
            pg_firstname,

            /// <remarks/>
            pg_lastname,

            /// <remarks/>
            pg_phone,

            /// <remarks/>
            phone,

            /// <remarks/>
            primary_email,

            /// <remarks/>
            primary_email_confirm,

            /// <remarks/>
            tags,

            /// <remarks/>
            waiver_id,

            /// <remarks/>
            waiver_title,

            /// <remarks/>
            waiver_type_guid,

            /// <remarks/>
            web_browsers_user_agent,
        }








        public xml Deserialize(string str)
        {
            //object result = new XmlSerializer(typeof(xml)).Deserialize(new StringReader(str));
            //var xml = @"<car/>";
            xml xmlObjects=new xml();
            var serializer = new XmlSerializer(typeof(xml));
            using (var reader = new StringReader(str))
            {
                xmlObjects = (xml)serializer.Deserialize(reader);
               // foreach (xmlParticipantsParticipant participant in xmlObjects.participants)
                {
                    foreach (xmlParticipant participant in xmlObjects.participants)
                    {
                        var CustomFields =
                            ((AdBsnsLayer.SmartWaiverIntegration.xmlParticipantCustomfields)
                                participant.Items[ participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.customfields)]);
                     // participant.Items[participant.ItemsElementName.ToList().IndexOf( ItemsChoiceType.firstname)]
                        var dat = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(participant.Items[participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.date_created_utc)].ToString()),
                         TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

                        var xmlParticipantCustomfieldsCustomfield = CustomFields.customfield.Where(customfield => customfield.title == "Address")
                            .FirstOrDefault();
                        if (xmlParticipantCustomfieldsCustomfield != null)

                            if (participant.Items[participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.is_adult)].ToString() == "True")
                            {

                                Access.InsertUserWaiver(
                                    participant.Items[
                                        participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.firstname)]
                                        .ToString(),
                                    participant.Items[
                                        participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.lastname)]
                                        .ToString(),
                                    xmlParticipantCustomfieldsCustomfield
                                        .value.ToString(),
                                    CustomFields.customfield.Where(customfield => customfield.title == "City")
                                        .FirstOrDefault()
                                        .value.ToString(),
                                    CustomFields.customfield.Where(customfield => customfield.title == "State")
                                        .FirstOrDefault()
                                        .value.ToString(),
                                    CustomFields.customfield.Where(customfield => customfield.title == "Country")
                                        .FirstOrDefault()
                                        .value.ToString(),
                                    CustomFields.customfield.Where(customfield => customfield.title == "Zip Code")
                                        .FirstOrDefault()
                                        .value.ToString(),
                                    participant.Items[
                                        participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.phone)]
                                        .ToString(),
                                    participant.Items[
                                        participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.primary_email)]
                                        .ToString(),
                                    participant.Items[
                                        participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.participant_id)]
                                        .ToString(),
                                    DateTime.Parse(
                                        participant.Items[
                                            participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.dob)]
                                            .ToString()),
                                    dat,false);
                            }
                            else
                            {
                                Access.InsertUserWaiver(
                                  participant.Items[
                                      participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.firstname)]
                                      .ToString(),
                                  participant.Items[
                                      participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.lastname)]
                                      .ToString(),
                                  xmlParticipantCustomfieldsCustomfield
                                      .value.ToString(),
                                  CustomFields.customfield.Where(customfield => customfield.title == "City")
                                      .FirstOrDefault()
                                      .value.ToString(),
                                  CustomFields.customfield.Where(customfield => customfield.title == "State")
                                      .FirstOrDefault()
                                      .value.ToString(),
                                  CustomFields.customfield.Where(customfield => customfield.title == "Country")
                                      .FirstOrDefault()
                                      .value.ToString(),
                                  CustomFields.customfield.Where(customfield => customfield.title == "Zip Code")
                                      .FirstOrDefault()
                                      .value.ToString(),
                                  participant.Items[
                                      participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.pg_phone)]
                                      .ToString(),
                                  participant.Items[
                                      participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.primary_email)]
                                      .ToString(),
                                  participant.Items[
                                      participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.participant_id)]
                                      .ToString(),
                                  DateTime.Parse(
                                      participant.Items[
                                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.dob)]
                                          .ToString()),
                                  dat,true);
                            }

                        //        .customfield[1].value
                        //   ((xmlParticipantCustomfields)participant.Items[]).
                        //        participant.customfields.Where(customfield => customfield.title == "Address")
                        //            .FirstOrDefault()
                        //            .value,
                        //        participant.customfields.Where(customfield => customfield.title == "City")
                        //            .FirstOrDefault()
                        //            .value,
                        //        participant.customfields.Where(customfield => customfield.title == "State")
                        //            .FirstOrDefault()
                        //            .value,
                        //        participant.customfields.Where(customfield => customfield.title == "Country")
                        //            .FirstOrDefault()
                        //            .value,
                        //        participant.customfields.Where(customfield => customfield.title == "Zip Code")
                        //            .FirstOrDefault()
                        //            .value, participant.phone, participant.primary_email, participant.participant_id.ToString(),
                        //        participant.dob,
                        //        dat);
                    }
                }
            }
            return null;


            //    xml SmartWaiverXMLS;
            //    var serializer = new XmlSerializer(typeof(xml[]));
            ////str = str.Substring(str.IndexOf("<xml>"));
            //    using (var reader = new StringReader(str))
            //    { 
            //        SmartWaiverXMLS = (xml)serializer.Deserialize(reader);
            //    }
            //    return SmartWaiverXMLS;
        }

    }
}
