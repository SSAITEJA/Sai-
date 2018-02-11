using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

namespace KoyaLawWeb.KoyaAdminNew
{
    public partial class ViewLogs : System.Web.UI.Page
    {
        int LogSessionID = 0;

        int AuditActionID = 0;

        public string Name = string.Empty;

        public string UserName = string.Empty;

        public string LoginDate = string.Empty;

        public string LogoutTime = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("Logout.aspx");
            }
            if (Session["UserDetails"] != null)
            {
                if (Session["UserDetails"].ToString().Split('|')[1].ToString() != "1")
                {
                    Session.Abandon();
               
                    Response.Redirect("~/ClientLogin.aspx");
                }
            }
            else
            {
                Response.Redirect("~/ClientLogin.aspx");
            }


            if (Request.QueryString["LSID"] != null && Request.QueryString["LSID"].ToString() != "")
            {
                int name;

                string decrpytValue = Protect.Decrypt(Request.QueryString["LSID"]);
                bool isNumber = int.TryParse(decrpytValue, out name);

                if (isNumber)
                {
                    LogSessionID = Convert.ToInt32(decrpytValue);
                }
                else
                {
                    Session.Abandon();
               
                    Response.Redirect("~/ClientLogin.aspx");
                }
               
            }

           
         
            if (!Page.IsPostBack)
            {               
              
                gridViewLogs.DataSource = ISDAAgreement.GetLogSessionsbyLogSessionID(LogSessionID, 0);
                gridViewLogs.DataBind();


            }


            DataTable dt = ISDAAgreement.GetLogSessionDetailsbyLogSessionID(LogSessionID);

            if (dt != null && dt.Rows.Count > 0)
            {
                Name = dt.Rows[0]["Name"].ToString();

                LoginDate = dt.Rows[0]["LoginDateTime"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["LogoutDataeTime"].ToString()))
                {
                    LogoutTime = dt.Rows[0]["LogoutDataeTime"].ToString();
                }
                else
                {
                    LogoutTime = "Current Session Closed";
                }

                UserName = dt.Rows[0]["UserName"].ToString();

                string UserTypeID = dt.Rows[0]["UserTypeID"].ToString();

                if (!Page.IsPostBack)
                {
                    if (UserTypeID != "13")
                    {

                        DataTable auditLogs = ISDAAgreement.GetAuditActions();
                        ddlAuditActions.DataSource = auditLogs;
                        ddlAuditActions.DataTextField = "AuditAction";
                        ddlAuditActions.DataValueField = "AuditActionID";
                        ddlAuditActions.DataBind();
                        ddlAuditActions.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
                    }
                    else
                    {
                        DataRow[] auditLogs = ISDAAgreement.GetAuditActions().Select("AuditActionID <> 2");

                        if (auditLogs.Length > 0)
                        {
                            ddlAuditActions.DataSource = auditLogs.CopyToDataTable();
                            ddlAuditActions.DataTextField = "AuditAction";
                            ddlAuditActions.DataValueField = "AuditActionID";
                            ddlAuditActions.DataBind();
                            ddlAuditActions.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));

                        }
                    }

                   
                }

            }
        }

        protected void ddlAuditActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuditActionID = Convert.ToInt32(ddlAuditActions.SelectedValue);
            gridViewLogs.DataSource = ISDAAgreement.GetLogSessionsbyLogSessionID(LogSessionID, AuditActionID);
            gridViewLogs.DataBind();

        }

        protected void gridViewLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewLogs.PageIndex = e.NewPageIndex;
            gridViewLogs.DataSource = ISDAAgreement.GetLogSessionsbyLogSessionID(LogSessionID, AuditActionID);
            gridViewLogs.DataBind();
        }

        
    }
}