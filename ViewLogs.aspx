<%@ Page Title="" Language="C#" MasterPageFile="~/KoyaAdminNew/KAdminNew.Master" AutoEventWireup="true" CodeBehind="ViewLogs.aspx.cs"
    Inherits="KoyaLawWeb.KoyaAdminNew.ViewLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpViewLogs" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="container-fluid">
                              <div class="row">                        
                            <div class="col-lg-12">
                                <div class="page-header">
                            <h1>Audit Log Details</h1>
                        </div>
                            </div>
                    </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                                    <table id="sample-table-2" class="table table-striped table-bordered table-hover1">
                                                        
                                                             <tr>
                                <td style="background-color: #307ecc; color: white;width:25%;text-align:center">Name
                                </td>
                                <td style="background-color: #307ecc; color: white;width:25%;text-align:center">Username
                                </td>
                                <td style="background-color: #307ecc; color: white;width:25%;text-align:center">Login Date
                                </td>
                                <td style="background-color: #307ecc; color: white;width:25%;text-align:center">Logout Time
                                </td>
                            </tr>
                                            <tr>
                                <td style="width:25%;text-align:center">
                                    <%=Name%>
                                </td>
                                <td style="width:25%;text-align:center">
                                    <%=UserName%>
                                </td>
                                <td style="width:25%;text-align:center">
                                    <%=LoginDate%>
                                </td>
                                <td style="width:25%;text-align:center">
                                    <%=LogoutTime%>
                                </td>
                            </tr>            
                                                        </table>
                                        </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4 style="text-align:center;color: #4F99C6;">View Logs</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    &nbsp;
                                </div>
                            </div>
                             <div class="row">
                        <div class="col-lg-8 align-right">
                    <div class="form-group">
					    <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Select Audit Action : 
</label>
                        
                         <div class="col-sm-9" style="text-align:left;">
						   <asp:DropDownList ID="ddlAuditActions" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAuditActions_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFV" runat="server" InitialValue="<-- Select -->" 
ControlToValidate="ddlAuditActions"></asp:RequiredFieldValidator>
				        </div>
		            </div>
                        </div>
                        </div>
                            <div class="row">
                                <div class="col-lg-12">
                                      <asp:GridView runat="server" ID="gridViewLogs" AlternatingRowStyle-CssClass="evenrow"
                            GridLines="None" CellPadding="0" CellSpacing="0" AutoGenerateColumns="false" Width="100%"
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gridViewLogs_PageIndexChanging" AllowSorting="true">
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" FirstPageText="First" LastPageText="Last" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                       <div class="table-responsive">
                                                    <table id="sample-table-2" class="table table-striped table-bordered table-hover1">
                                                          <thead class="table-header">
                                                              <tr>
                                            <th style="width:20%;text-align:center;" >Client Entity Name
                                            </th>
                                            <th style="width:20%;text-align:center;">Counterparty
                                            </th>
                                            <th style="width:20%;text-align:center;">Created Date
                                            </th>

                                            <th style="width:20%;text-align:center;">Created by Name
                                            </th>
                                                                  </tr>
                                                              </thead>
                                        </table>
                                           </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="table-responsive">
                                                    <table id="sample-table-2" class="table table-striped table-bordered table-hover1">
                                                        <tbody>
                                            <tr>
                                                <td style="width:20%;text-align:center;">
                                                    <%#Eval("ClientEntityName")%>    
                                                </td>
                                                <td style="width:20%;text-align:center;">
                                                    <%#Eval("CounterParty") %>
                                                </td>
                                                <td style="width:20%;text-align:center;">
                                                    <%#Eval("CreatedDate")%>
                                                </td>
                                                <td style="width:20%;text-align:center;">
                                                    <%#Eval("CreatedByName")%>
                                                </td>
                                            </tr>
                                                            </tbody>
                                        </table>
                                            </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                      <asp:Panel ID="pnlReports" runat="server" Width="100%">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-12">
                                          <h5 style="text-align:center;color: #4F99C6;">Saved Reports</h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                          <asp:GridView runat="server" ID="gvSavedReport" AlternatingRowStyle-CssClass="evenrow"
                                            GridLines="None" CellPadding="0" CellSpacing="0" AutoGenerateColumns="false" Width="100%"
                                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gvSavedReport_PageIndexChanging" 
AllowSorting="true">
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" FirstPageText="First" LastPageText="Last" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div class="table-responsive">
                                                    <table id="sample-table-2" class="table table-striped table-bordered table-hover1">
                                                        <thead class="table-header">
                                                            <tr>
                                                                <th style="width:20%;text-align:center;">Report ID
                                                                </th>
                                                                <th style="width:25%;text-align:center;">Report Name
                                                                </th>
                                                                <th style="width:15%;text-align:center;">ISDA Agreements Count
                                                                </th>
                                                                <th style="width:20%;text-align:center;">Selected ISDA Questions
                                                                </th>
                                                                <th style="width:20%;text-align:center;">Date Report Generated</th>


                                                            </tr>
                                                            </thead>
                                                        </table>
                                                            </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                       <div class="table-responsive">
                                                    <table id="sample-table-2" class="table table-striped table-bordered table-hover1">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width:20%;text-align:center;">
                                                                    <%#Eval("ReportCode") %>
                                                                </td>
                                                                <td style="width:25%;text-align:center;">
                                                                    <%#Eval("ReportName") %>
                                                                </td>
                                                                <td style="width:15%;text-align:center;">
                                                                    <%#Eval("SelectedAgreeementsCount") %>
                                                                </td>

                                                                <td style="width:20%;text-align:center;">
                                                                    <%#Eval("SelectQuestionsCount")%>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <%#Eval("ReportDate")%>
                                                                </td>
                                                            </tr>
                                                            </tbody>
                                                        </table>
                                                           </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>                                
                          </div>
                        </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
