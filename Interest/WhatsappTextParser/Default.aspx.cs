using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class WhatsappMess
{
    public string Sender { get; set; }
    public string Message { get; set; }
    public string Line { get; set; }
    public DateTime EventTime { get; set; }
    public bool IsChat { get; set; }
    public bool IsAddedPeople { get; set; }
    public bool IsRemovedPeople { get; set; }
    public bool IsSubjectChange { get; set; }
    public bool IsForward { get; set; }
    public bool IsLeft { get; set; }
    public bool IsNumberChanged { get; set; }
    public bool IsGroupIconChanged { get; set; }

    public WhatsappMess()
    {

        IsChat = false;
        IsAddedPeople = false;
        IsRemovedPeople = false;
        IsSubjectChange = false;
        IsForward = false;
        IsLeft = false;
        IsGroupIconChanged = false;
        IsNumberChanged = false;
        Message = "";

    }

    public string InsertStats( )
        
        {
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format("InsertStats"),
                CommandType = CommandType.StoredProcedure,
                
            };
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@Sender",SqlDbType.NVarChar,Int32.MaxValue,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Sender),
                new SqlParameter("@Message",SqlDbType.NVarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Message),
                new SqlParameter("@EventTime",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,EventTime),
                new SqlParameter("@IsChat",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsChat),
                new SqlParameter("@IsAddedPeople",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsAddedPeople),
                new SqlParameter("@IsRemovedPeople",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsRemovedPeople),
                new SqlParameter("@IsSubjectChange",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsSubjectChange),
                new SqlParameter("@IsForward",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsForward),
                new SqlParameter("IsLeft",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsLeft),
                new SqlParameter("@IsNumberChanged",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsNumberChanged),
                new SqlParameter("@IsGroupIconChanged",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsGroupIconChanged),
                new SqlParameter("@Line",SqlDbType.NVarChar,Int32.MaxValue,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Line),
                
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(@"Data Source=SJMWKS82\SQLSERVER;Initial Catalog=WhatsappAnalytics;User ID=sa;Password=manager@123;");
            
            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
                
            }
            catch (Exception ex)
            {
              
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
         
    }


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        readstring();
       // string sdf = "may 6, 7:10 PM";
       // DateTime sd = DateTime.ParseExact(sdf, "MMM d, h:mm tt", CultureInfo.InvariantCulture);
    }

    public void readstring()
    {
        string text = System.IO.File.ReadAllText(@"C:\Users\vramalingam\Desktop\chat.txt");

        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\vramalingam\Desktop\chat.txt");

   
        foreach (string line in lines)
        {
            // Use a tab to indent each line of the file.
            try
            {
                int i = 0;
                WhatsappMess mess = new WhatsappMess();
                mess.Line = line;
                mess.EventTime = DateTime.ParseExact(line.Substring(0, line.IndexOf("-") - 1), "MMM d, h:mm tt", CultureInfo.InvariantCulture);
                if (line.Contains("changed the subject to"))
                {
                    mess.IsSubjectChange = true;
                    mess.Message = line.Substring(line.IndexOf(" - ") + 3);
                    mess.Sender = mess.Message.Substring(0, mess.Message.IndexOf("changed the subject to") - 1);
                    mess.Message = mess.Message.Substring(mess.Message.IndexOf(" changed the subject to ") + 25);
                    mess.Message = mess.Message.Substring(0, mess.Message.Length - 1);
                    i++;
                }

                if (line.Contains(" was added"))
                {
                    mess.IsAddedPeople = true;
                    mess.Sender = line.Substring(line.IndexOf(" - ") + 3);
                    mess.Sender = mess.Sender.Substring(0, mess.Sender.IndexOf(" was added"));
                    mess.Message = "People added";
                    i++;
                }

                if (line.Contains(" was removed"))
                {
                    mess.IsRemovedPeople = true;
                    mess.Sender = line.Substring(line.IndexOf(" - ") + 3);
                    mess.Sender = mess.Sender.Substring(0, mess.Sender.IndexOf(" was removed"));
                    mess.Message = "People removed";
                    i++;
                }

                if (line.Contains(": "))
                {
                    mess.IsChat = true;
                    mess.Sender = line.Substring(line.IndexOf(" - ") + 3);
                    mess.Sender = mess.Sender.Substring(0, mess.Sender.IndexOf(": "));
                    mess.Message = line.Substring(line.IndexOf(": ") + 2);
                    i++;
                }

                if (line.Contains("changed this group's icon"))
                {
                    mess.IsGroupIconChanged = true;
                    mess.Sender = line.Substring(line.IndexOf(" - ") + 3);
                    mess.Sender = mess.Sender.Substring(0, mess.Sender.IndexOf(" changed this group's icon"));
                    mess.Message = "Icon Changed";
                    i++;
                }

                if (line.Contains("changed to "))
                {
                    mess.IsNumberChanged = true;
                    mess.Sender = line.Substring(line.IndexOf(" - ") + 3);
                    mess.Message = mess.Sender.Substring(mess.Sender.IndexOf("changed to ") + 11);
                    mess.Sender = mess.Sender.Substring(0, mess.Sender.IndexOf(" changed to"));
                 
                    i++;
                }


                if (i == 0)
                {

                    if (line.Contains(" left"))
                    {
                        mess.IsLeft = true;
                        mess.Sender = line.Substring(line.IndexOf(" - ") + 3);
                        mess.Sender = mess.Sender.Substring(0, mess.Sender.IndexOf(" left"));
                    }
                }

                if (i > 1)
                {
                    
                }
                mess.InsertStats();


            }
            catch (Exception ex)
            {
                
              
            }
          
            


            Response.Write("\t" + line);
        }

        

     
    }
}