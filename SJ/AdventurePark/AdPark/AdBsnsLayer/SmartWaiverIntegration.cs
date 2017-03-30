using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdConstants;
using AdDataLayer;
namespace AdBsnsLayer
{
    public class SmartWaiverIntegration
    {

        DataAccess Access = new DataAccess();
       List< UserDetailsWithWaiverClass> DetailsWithWaiverClasses = new List<UserDetailsWithWaiverClass>();
        /// <remarks/>

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class xml
        {

            private decimal api_versionField;

            private xmlParticipant[] participantsField;

            private bool more_participants_existField;

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

            /// <remarks/>
            public bool more_participants_exist
            {
                get
                {
                    return this.more_participants_existField;
                }
                set
                {
                    this.more_participants_existField = value;
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
            [System.Xml.Serialization.XmlElementAttribute("dob", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("firstname", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("how_many_auto_photo_capture_photographs_captured", typeof(byte))]
            [System.Xml.Serialization.XmlElementAttribute("images", typeof(xmlParticipantImages))]
            [System.Xml.Serialization.XmlElementAttribute("is_adult", typeof(bool))]
            [System.Xml.Serialization.XmlElementAttribute("lastname", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("marketingallowed", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("middlename", typeof(object))]
            [System.Xml.Serialization.XmlElementAttribute("overageofmajority", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("participant_id", typeof(ulong))]
            [System.Xml.Serialization.XmlElementAttribute("pdf_url", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("pending_email_validation", typeof(bool))]
            [System.Xml.Serialization.XmlElementAttribute("pg_firstname", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("pg_lastname", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("primary_email", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("primary_email_confirm", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("sw_kioskv3_id", typeof(string))]
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

            private xmlParticipantCheckinsCheckin[] checkinField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("checkin")]
            public xmlParticipantCheckinsCheckin[] checkin
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

            private System.DateTime value_iso8601Field;

            private bool value_iso8601FieldSpecified;

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

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime value_iso8601
            {
                get
                {
                    return this.value_iso8601Field;
                }
                set
                {
                    this.value_iso8601Field = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool value_iso8601Specified
            {
                get
                {
                    return this.value_iso8601FieldSpecified;
                }
                set
                {
                    this.value_iso8601FieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipantImages
        {

            private xmlParticipantImagesImage[] imageField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("image")]
            public xmlParticipantImagesImage[] image
            {
                get
                {
                    return this.imageField;
                }
                set
                {
                    this.imageField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipantImagesImage
        {

            private string sourceField;

            private string date_utcField;

            private string img_urlField;

            private string tagField;

            private string file_extField;

            /// <remarks/>
            public string source
            {
                get
                {
                    return this.sourceField;
                }
                set
                {
                    this.sourceField = value;
                }
            }

            /// <remarks/>
            public string date_utc
            {
                get
                {
                    return this.date_utcField;
                }
                set
                {
                    this.date_utcField = value;
                }
            }

            /// <remarks/>
            public string img_url
            {
                get
                {
                    return this.img_urlField;
                }
                set
                {
                    this.img_urlField = value;
                }
            }

            /// <remarks/>
            public string tag
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

            /// <remarks/>
            public string file_ext
            {
                get
                {
                    return this.file_extField;
                }
                set
                {
                    this.file_extField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class xmlParticipantTags
        {

            private string tagField;

            /// <remarks/>
            public string tag
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
            overageofmajority,

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
            primary_email,

            /// <remarks/>
            primary_email_confirm,

            /// <remarks/>
            sw_kioskv3_id,

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









        public bool? Deserialize(string str)
        {
            xml xmlObjects=new xml();
            var serializer = new XmlSerializer(typeof(xml));
            using (var reader = new StringReader(str))
            {
                xmlObjects = (xml)serializer.Deserialize(reader);
                {
                    foreach (xmlParticipant participant in xmlObjects.participants)
                    {
                        try
                        {
                            UserDetailsWithWaiverClass detailsWithWaiverClass = new UserDetailsWithWaiverClass();
                            ConverttoUserDetails(participant, detailsWithWaiverClass);
                            Access.UserDetailsWithWaiverInsert(detailsWithWaiverClass);
                        }
                        catch (Exception ex)
                        {
                            
                        }
                       
                    }
                }
                if (xmlObjects.more_participants_exist)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return null;

        }

        private static void ConverttoUserDetails(xmlParticipant participant, UserDetailsWithWaiverClass detailsWithWaiverClass)
        {
            if (participant != null)
            {
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.primary_email) >= 0)
                {
                    detailsWithWaiverClass.EmailID = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.primary_email)]
                      .ToString();
                }
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.firstname) >= 0)
                {
                    detailsWithWaiverClass.FirstName = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.firstname)]
                      .ToString();
                }
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.lastname) >= 0)
                {
                    detailsWithWaiverClass.LastName = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.lastname)]
                      .ToString();
                }
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.dob) >= 0)
                {
                    if (
                        participant.Items[participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.dob)
                        ].ToString() != "N/A")
                    {
                        detailsWithWaiverClass.DateOfBirth = DateTime.Parse((participant.Items[participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.dob)].ToString()));
                    }
                }
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.date_created_utc) >= 0)
                {
                    if (
                        participant.Items[participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.date_created_utc)
                        ].ToString() != "N/A")
                    {
                        var dat = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(participant.Items[participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.date_created_utc)].ToString()),
                        TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

                        detailsWithWaiverClass.CreatedDate = dat;
                    }
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.participant_id) >= 0)
                {
                    detailsWithWaiverClass.ParticipantID = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.participant_id)]
                      .ToString();
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.is_adult) >= 0)
                {
                    detailsWithWaiverClass.IsMinor = !bool.Parse(participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.is_adult)]
                      .ToString());
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.waiver_id) >= 0)
                {
                    detailsWithWaiverClass.waiver_id = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.waiver_id)]
                      .ToString();
                }

                //if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.date_accepted_utc) >= 0)
                //{
                //    if (
                //        participant.Items[participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.date_accepted_utc)
                //        ].ToString() != "N/A")
                //    {
                //        detailsWithWaiverClass.date_accepted_utc = DateTime.Parse((participant.Items[participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.date_accepted_utc)].ToString()));
                //    }
                //}

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.pdf_url) >= 0)
                {
                    detailsWithWaiverClass.pdf_url = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.pdf_url)]
                      .ToString();
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.waiver_title) >= 0)
                {
                    detailsWithWaiverClass.waiver_title = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.waiver_title)]
                      .ToString();
                }
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.pending_email_validation) >= 0)
                {
                    detailsWithWaiverClass.pending_email_validation = bool.Parse(participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.pending_email_validation)]
                      .ToString());
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.overageofmajority) >= 0)
                {
                    detailsWithWaiverClass.overageofmajority = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.overageofmajority)]
                      .ToString();
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.web_browsers_user_agent) >= 0)
                {
                    detailsWithWaiverClass.web_browsers_user_agent = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.web_browsers_user_agent)]
                      .ToString();
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.completed_from_ip_address) >= 0)
                {
                    detailsWithWaiverClass.completed_from_ip_address = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.completed_from_ip_address)]
                      .ToString();
                }
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.SERVERSIDE_WAIVER_START_TS) >= 0)
                {
                    detailsWithWaiverClass.SERVERSIDE_WAIVER_START_TS = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.SERVERSIDE_WAIVER_START_TS)]
                      .ToString();
                }
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.waiver_type_guid) >= 0)
                {
                    detailsWithWaiverClass.waiver_type_guid = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.waiver_type_guid)]
                      .ToString();
                }
                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.marketingallowed) >= 0)
                {
                    detailsWithWaiverClass.marketingallowed = participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.marketingallowed)]
                      .ToString();
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.completed_at_kiosk) >= 0)
                {
                    detailsWithWaiverClass.completed_at_kiosk = (participant.Items[
                          participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.completed_at_kiosk)]
                      .ToString());
                }

                if (participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.customfields) >= 0)
                {
                    xmlParticipantCustomfields xmlParticipantCustomfields = (xmlParticipantCustomfields) participant.Items[
                        participant.ItemsElementName.ToList().IndexOf(ItemsChoiceType.customfields)];
                    if (xmlParticipantCustomfields.customfield != null)
                    {
                        if (xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "Zip Code").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.Zipcode = xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "Zip Code").FirstOrDefault().value.ToString();
                        }
                        if (xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "Address").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.Address = xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "Address").FirstOrDefault().value.ToString();
                        }
                        if (xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "City").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.City = xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "City").FirstOrDefault().value.ToString();
                        }
                        if (xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "State").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.State = xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "State").FirstOrDefault().value.ToString();
                        }
                        if (xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "Date visiting").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.Date_visiting = DateTime.Parse(xmlParticipantCustomfields.customfield.Where(customfield => customfield.title == "Date visiting").FirstOrDefault().value.ToString());
                        }
                        if (
                            xmlParticipantCustomfields.customfield.Where(
                                customfield => customfield.title == "Time Visiting").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.Time_Visiting =
                                xmlParticipantCustomfields.customfield.Where(
                                        customfield => customfield.title == "Time Visiting")
                                    .FirstOrDefault()
                                    .value.ToString();
                        }
                        if (
                            xmlParticipantCustomfields.customfield.Where(
                                customfield => customfield.title == "Ziplining").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.ZipLining =
                                xmlParticipantCustomfields.customfield.Where(
                                        customfield => customfield.title == "Ziplining")
                                    .FirstOrDefault()
                                    .value.ToString();
                        }
                        if (
                            xmlParticipantCustomfields.customfield.Where(
                                customfield => customfield.title == "Jump Off").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.JumpOff =
                                xmlParticipantCustomfields.customfield.Where(
                                        customfield => customfield.title == "Jump Off")
                                    .FirstOrDefault()
                                    .value.ToString();
                        }
                        if (
                            xmlParticipantCustomfields.customfield.Where(
                                customfield => customfield.title == "Ropes Course").ToList().Count > 0)
                        {
                            detailsWithWaiverClass.RopesCourse =
                                xmlParticipantCustomfields.customfield.Where(
                                        customfield => customfield.title == "Ropes Course")
                                    .FirstOrDefault()
                                    .value.ToString();
                        }
                    }
                }

            }
        }
    }
}
