using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using System.Xml;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using DataAccess;
using System.Text;

namespace KoyaLawWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public string agreementprocess;
        public string agreementawaiting;
        public string agreementapproved;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                ddlClient.DataSource = DataAccess.ISDAAgreement.GetClientList();
                ddlClient.DataValueField = "ClientID";
                ddlClient.DataTextField = "ClientName";
                ddlClient.DataBind();
                ddlClient.Items.Insert(0, new ListItem("<-- Select --->", "<-- Select --->"));
            }


            // DataTable Agreementstatus = manikanta.GetallAgreementStatusData();

        }



        protected void ddlagreement_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlagreement.SelectedValue == "Draft Report")
            {
                if (ddlClient.SelectedValue != "<-- Select --->" && ddlClient.SelectedValue != null)
                {

                    string ClientEnityIds = string.Empty;
                    DataTable clientEnities = DataAccess.ISDAAgreement.GetClientEntityList(Convert.ToInt32(ddlClient.SelectedValue));
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("<table>");
                    sb1.Append("<tr>");
                    sb1.Append("<td>");
                    sb1.Append("<table border=\"2\" bgcolor=\"#d4d4d4\" style=\"vertical-align:top;\">");
                    sb1.Append("<tr><td colspan=\"3\"><center>  ISDA Draft AGREMENTS</center> </td></tr>");
                    sb1.Append("<tr><th> ClientEntityName </th><th>Reopend</th><th>Finalized</th></tr>");
                    foreach (DataRow item in clientEnities.Rows)
                    {
                        int cid = Convert.ToInt32(item["ClientEntityID"]);
                        string finalzeddraft = DataAccess.Draft_AgreementsISDA.GetFinalizedofAllIsda_Draftagreements(cid);
                        string unfinalzeddraft = DataAccess.Draft_AgreementsISDA.GetUnFinalizedofAllIsda_Draftagreements(cid);

                        sb1.Append("<tr><td>" + item["ClientEntityName"].ToString() + "</td> <td>" + finalzeddraft + "</td><td>" + unfinalzeddraft + "</td> </tr>");
                    }

                    sb1.Append("</table>");
                    sb1.Append("</td>");
                    sb1.Append("<td>");
                    sb1.Append("<table border=\"2\" bgcolor=\"#d4d4d4\" style=\"vertical-align:top;\">");
                    sb1.Append("<tr><td colspan=\"3\"><center>  PB Draft AGREMENTS</center> </td></tr>");
                    sb1.Append("<tr><th> ClientEntityName </th><th>Reopend</th><th>Finalized</th></tr>");
                    foreach (DataRow items in clientEnities.Rows)
                    {
                        int cid = Convert.ToInt32(items["ClientEntityID"]);
                        string PBfinalzeddraft = DataAccess.Draft_AgreementsPB.Prime_finalizedAllPB_DraftAgreements(cid);
                        string PBunfinalzeddraft = DataAccess.Draft_AgreementsPB.Prime_UnfinalizedAllPB_DraftAgreements(cid);
                        sb1.Append("<tr><td>" + items["ClientEntityName"].ToString() + "</td> <td>" + PBfinalzeddraft + "</td><td>" + PBunfinalzeddraft + "</td> </tr>");
                    }
                    sb1.Append("</table>");
                    sb1.Append("</td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");
                    txt2.Text = sb1.ToString();
                }
            }
            else
            {

                //
                if (ddlagreement.SelectedValue == "Executed Report")
                {
                    if (ddlClient.SelectedValue != "<-- Select --->" && ddlClient.SelectedValue != null)
                    {
                        string ClientEnityIds = string.Empty;
                        DataTable clientEnities = DataAccess.ISDAAgreement.GetClientEntityList(Convert.ToInt32(ddlClient.SelectedValue));

                        StringBuilder sb = new StringBuilder();
                        //  StringBuilder sb1 = new StringBuilder();
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append("<table border=\"2\" bgcolor=\"#d4d4d4\" style=\"vertical-align:top;\">");
                        sb.Append("<tr><td colspan=\"4\"><center>  ISDA AGREMENTS</center> </td></tr>");

                        sb.Append("<tr><th> ClientEntityName </th><th>Approved</th><th>UnApproved</th><th>Review</th></tr>");

                        foreach (DataRow item in clientEnities.Rows)
                        {
                            int cid = Convert.ToInt32(item["ClientEntityID"]);

                            DataTable dt = DataAccess.ISDAAgreement.GetStatusofAllISDAAgreementsbyClientEntityID(cid);
                            string a = dt.Select("StatusID = 3").Count().ToString();
                            string b = dt.Select("StatusID = 1").Count().ToString();
                            string c = dt.Select("StatusID in (2,5,6)").Count().ToString();

                            sb.Append("<tr><td>" + item["ClientEntityName"].ToString() + "</td> <td>" + a + "</td> <td>" + b + "</td> <td>" + c + "</td></tr>");



                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("<td>");

                        sb.Append("<table border=\"2\" bgcolor=\"#d4d4d4\" style=\"vertical-align:top;\">");
                        sb.Append("<tr><td colspan=\"4\"><center>  PB AGREMENTS</center> </td></tr>");

                        sb.Append("<tr><th>Approved</th><th>UnApproved</th><th>Review</th></tr>");




                        foreach (DataRow item in clientEnities.Rows)
                        {
                            int cid = Convert.ToInt32(item["ClientEntityID"]);

                            DataTable pbdt = DataAccess.PBAgreements.GetStatusofAllPBAgreementsbyclientEntitiyID(cid);
                            string d = pbdt.Select("StatusID = 3").Count().ToString();
                            string k = pbdt.Select("StatusID = 1").Count().ToString();
                            string f = pbdt.Select("StatusID in (2,5,6)").Count().ToString();

                            sb.Append("<tr> <td>" + d + "</td> <td>" + k + "</td> <td>" + f + "</td></tr>");







                        }
                        sb.Append("</table>");
                        sb.Append("</td>");

                        sb.Append("</tr>");



                        sb.Append("</table>");



                        txt1.Text = sb.ToString();

                    }
                }
                else
                {
                    ddlClient.Items.Clear();
                }
            }


        }

        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClient.SelectedValue != "<-- Select Client --->")
            {
                if (ddlClient.SelectedValue != "<-- Select --->" && ddlClient.SelectedValue != null)
                {
                    string ClientEnityIds = string.Empty;
                    DataTable clientEnities = DataAccess.ISDAAgreement.GetClientEntityList(Convert.ToInt32(ddlClient.SelectedValue));

                    StringBuilder sb = new StringBuilder();
                    //  StringBuilder sb1 = new StringBuilder();
                    sb.Append("<table>");
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append("<table border=\"2\" bgcolor=\"#d4d4d4\" style=\"vertical-align:top;\">");
                    sb.Append("<tr><td colspan=\"4\"><center>  ISDA AGREMENTS</center> </td></tr>");

                    sb.Append("<tr><th> ClientEntityName </th><th>Approved</th><th>UnApproved</th><th>Review</th></tr>");

                    foreach (DataRow item in clientEnities.Rows)
                    {
                        int cid = Convert.ToInt32(item["ClientEntityID"]);

                        DataTable dt = DataAccess.ISDAAgreement.GetStatusofAllISDAAgreementsbyClientEntityID(cid);
                        string a = dt.Select("StatusID = 3").Count().ToString();
                        string b = dt.Select("StatusID = 1").Count().ToString();
                        string c = dt.Select("StatusID in (2,5,6)").Count().ToString();

                        sb.Append("<tr><td>" + item["ClientEntityName"].ToString() + "</td> <td>" + a + "</td> <td>" + b + "</td> <td>" + c + "</td></tr>");



                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("<td>");

                    sb.Append("<table border=\"2\" bgcolor=\"#d4d4d4\" style=\"vertical-align:top;\">");
                    sb.Append("<tr><td colspan=\"4\"><center>  PB AGREMENTS</center> </td></tr>");

                    sb.Append("<tr><th>Approved</th><th>UnApproved</th><th>Review</th></tr>");




                    foreach (DataRow item in clientEnities.Rows)
                    {
                        int cid = Convert.ToInt32(item["ClientEntityID"]);

                        DataTable pbdt = DataAccess.PBAgreements.GetStatusofAllPBAgreementsbyclientEntitiyID(cid);
                        string d = pbdt.Select("StatusID = 3").Count().ToString();
                        string k = pbdt.Select("StatusID = 1").Count().ToString();
                        string f = pbdt.Select("StatusID in (2,5,6)").Count().ToString();

                        sb.Append("<tr> <td>" + d + "</td> <td>" + k + "</td> <td>" + f + "</td></tr>");







                    }
                    sb.Append("</table>");
                    sb.Append("</td>");

                    sb.Append("</tr>");



                    sb.Append("</table>");



                    txt1.Text = sb.ToString();
                }

            }



        }
    }
}