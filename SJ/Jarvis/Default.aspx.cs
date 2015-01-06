using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnHelloWorld_Click(object sender, EventArgs e)
    {
        JarvisWebRefer.Jarvis jd = new JarvisWebRefer.Jarvis();
        string url = jd.SpeakToJarvis(txtHelloWorld.Text);
        IFrame.Attributes["src"] = url;
        Label1.Text = url;
    }
}