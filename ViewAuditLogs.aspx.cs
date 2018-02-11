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
    public partial class ViewAuditLogs : System.Web.UI.Page
    {
        int AuditActionID = 0;
        int ClientID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtclients = ISDAAgreement.GetClientList();
                ddClientActions.DataSource = dtclients;
                ddClientActions.DataValueField = "ClientID";
                ddClientActions.DataTextField = "ClientName";
                ddClientActions.DataBind();
                ddClientActions.Items.Insert(0, new ListItem("<-- Select Client --->", "<-- Select Client --->"));

                DataTable auditLogs = ISDAAgreement.GetAuditActions();
                ddAuditActions.DataSource = auditLogs;
                ddAuditActions.DataTextField = "AuditAction";
                ddAuditActions.DataValueField = "AuditActionID";
                ddAuditActions.DataBind();
                ddAuditActions.Items.Insert(0, new ListItem("<-- Select -->", "<-- Select -->"));
            }
        }

        protected void ddClientActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddClientActions.SelectedValue != "<-- Select Client --->")
            {
                ClientID = Convert.ToInt32(ddClientActions.SelectedValue);
            }
        }

        protected void ddAuditActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddAuditActions.SelectedValue != "<-- Select -->")
            {
                AuditActionID = Convert.ToInt32(ddAuditActions.SelectedValue);
            }
            gridViewLogs.DataSource = ISDAAgreement.GetLogSessionsbyClientID(ClientID, AuditActionID);
            gridViewLogs.DataBind();
        }

        protected void gridViewLogs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}