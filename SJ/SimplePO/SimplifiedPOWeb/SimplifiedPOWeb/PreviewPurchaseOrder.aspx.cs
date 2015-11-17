using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SimplifiedPOBusiness;
using SimplifiedPOConstants;

namespace SimplifiedPOWeb
{
    public partial class PreviewPurchaseOrder : System.Web.UI.Page
    {
        SPOBL _spobl = new SPOBL();
        private static User user = new User();
        public static DataTable dt = new DataTable();
        public static DataTable dtItems = new DataTable();
        public static string SNO;
        private enum Priority : int
        {
            Low = 1,
            Medium = 2,
            High = 3,
            Urgent = 4
        }

        private enum OrderType : int
        {
            OneTimeOrder = 1,
            ReOrder = 2,
            FutureReorder = 3
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["AccessCode"] != null)
                {
                    SNO = _spobl.GetOnlinePO(Request.QueryString["AccessCode"].ToString());
                    if (SNO != "Error")
                    {
                        PONumber.Text = "PO Number : " + SNO;
                        Date.Text = "Previewed on " + DateTime.Now.ToString("f");
                        dt = _spobl.GetPOMasterDetail(SNO);
                        dtItems = _spobl.GetPOItemsDetail(SNO);
                        LoadValues(dt, dtItems);
                        Content.Visible = true;
                        NotVisible.Visible = false;
                    }
                    else
                    {
                        Content.Visible = false;
                        NotVisible.Visible = true;
                    }

                }
                else
                {
                    Content.Visible = false;
                    NotVisible.Visible = true;
                }
              
            }
        }

        private void LoadValues(DataTable dataTable, DataTable Items)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    var userlist = _spobl.GetAllUsers();
                    if (userlist != null)
                    {
                        lblBuyerPostFor.Text = userlist.Where(user1 => Equals(user1.UserID.ToString(), dt.Rows[0]["POPostForID"]))
                            .Select(user1 => user.FirstName + " " + user.LastName).ToList()[0].ToString();
                        lblUserName.Text = dt.Rows[0]["LoginUserName"].ToString();
                        lblBuyerName.Text = dt.Rows[0]["BuyerName"].ToString();
                        lblBuyerAddress.Text = dt.Rows[0]["BuyerAddress"].ToString();
                        lblBuyerContact.Text = dt.Rows[0]["BuyerContactNumber"].ToString();
                        lblPriority.Text = ((Priority)int.Parse(dt.Rows[0]["Priority"].ToString())).ToString();
                        lblEntity.Text = dt.Rows[0]["SupplierEntity"].ToString();
                        lblSupplier.Text = dt.Rows[0]["SupplierID"].ToString();
                        lblSupplierName.Text = dt.Rows[0]["SupplierName"].ToString();
                        lblSupplierAddress.Text = dt.Rows[0]["SupplierAddress"].ToString();
                        lblSupplierContact.Text = dt.Rows[0]["SupplierContactNumber"].ToString();
                        lblNotes.Text = dt.Rows[0]["Notes"].ToString();

                        //lblSubtotal.Text = string.Format("{0:C}", Convert.ToDecimal(dt.Rows[0]["SubTotal"].ToString()));// String.Format("{0:C}", dt.Rows[0]["SubTotal"].ToString());
                        //lblShipping.Text = dt.Rows[0]["Shipping"].ToString();
                        //lblDiscount.Text = dt.Rows[0]["Discount"].ToString();
                        //lblTotal.Text = dt.Rows[0]["Total"].ToString();
                        //lblCheckRequired.Text = dt.Rows[0]["CheckRequired"].ToString();
                        //lblRFIDTags.Text = dt.Rows[0]["RFIDTags"].ToString();
                        string OrderType = "";
                        try
                        {
                            OrderType = ((OrderType)int.Parse(dt.Rows[0]["OrderType"].ToString())).ToString(); //dt.Rows[0]["OrderType"].ToString();

                        }
                        catch (Exception)
                        {
                            OrderType = "N/A";
                        }
                        DataTable dtPoDetailsBottom = new DataTable();
                        dtPoDetailsBottom.Columns.Add("SubTotal");
                        dtPoDetailsBottom.Columns.Add("Shipping");
                        dtPoDetailsBottom.Columns.Add("Discount");
                        dtPoDetailsBottom.Columns.Add("Total");
                        dtPoDetailsBottom.Columns.Add("CheckRequired");
                        dtPoDetailsBottom.Columns.Add("RFIDTags");
                        dtPoDetailsBottom.Columns.Add("OrderType");

                        dtPoDetailsBottom.Rows.Add(
                            string.Format("{0:C}", Convert.ToDecimal(dt.Rows[0]["SubTotal"].ToString())),
                            string.Format("{0:C}", Convert.ToDecimal(dt.Rows[0]["Shipping"].ToString())),
                            string.Format("{0:C}", Convert.ToDecimal(dt.Rows[0]["Discount"].ToString())),
                            string.Format("{0:C}", Convert.ToDecimal(dt.Rows[0]["Total"].ToString())),
                            dt.Rows[0]["CheckRequired"].ToString(),
                            dt.Rows[0]["RFIDTags"].ToString(), OrderType);
                        RadGrid2.DataSource = dtPoDetailsBottom;
                        RadGrid2.DataBind();

                        lblPurchaseReason.Text = dt.Rows[0]["PReason"].ToString();
                        submissionDate.Text = dt.Rows[0]["SubmissionTime"].ToString();

                        RadGrid1.DataSource = dtItems;
                        RadGrid1.DataBind();
                    }



                    //   lblBuyerPostFor.Text = _spobl.GetAllUsers();


                }
            }
        }


    
    }

}