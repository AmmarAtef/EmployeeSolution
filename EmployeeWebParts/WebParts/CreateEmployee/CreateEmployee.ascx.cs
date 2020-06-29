using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Net.Http;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using RestSharp;

namespace EmployeeWebParts.WebParts.CreateEmployee
{
    [ToolboxItemAttribute(false)]
    public partial class CreateEmployee : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        private static readonly HttpClient client = new HttpClient();
        public CreateEmployee()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    var appSettings = ConfigurationManager.AppSettings;
            //    string siteUrl = appSettings["SiteUrl"];
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "AddEmployee", "CreateListItem()", true);
            //}

            

        }
    }
}
