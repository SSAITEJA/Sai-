<%@ Page Title="" Language="C#" MasterPageFile="~/KoyaAdminNew/KAdminNew.Master" AutoEventWireup="true" CodeBehind="ViewAuditLogs.aspx.cs" Inherits="KoyaLawWeb.KoyaAdminNew.ViewAuditLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            &nbsp;
        </div>
    </div>
    
    <div class="row">
        <div class="col-lg-8 align-right">
            <div class="form-group">
                <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Select Client : </label>
                <div class="col-sm-9" style="text-align:left;">
                    <asp:DropDownList ID="ddClientActions" runat="server" OnSelectedIndexChanged="ddClientActions_SelectedIndexChanged" Width="300px" Height="30px" Style="text-align: left"></asp:DropDownList>
                </div>
            </div>
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
                <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Select Audit Action : </label>
                <div class="col-sm-9" style="text-align:left;">
                    <asp:DropDownList ID="ddAuditActions" runat="server" OnSelectedIndexChanged="ddAuditActions_SelectedIndexChanged" Width="300px" Height="30px" Style="text-align: left" AutoPostBack="true"></asp:DropDownList>
                </div>
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
            &nbsp;
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <asp:GridView runat="server" ID="gridViewLogs" AlternatingRowStyle-CssClass="evenrow" GridLines="None" CellPadding="0" CellSpacing="0" AutoGenerateColumns="false" Width="100%"
                            AllowPaging="true" PageSize="10"  AllowSorting="true" OnSelectedIndexChanged="gridViewLogs_SelectedIndexChanged">
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="3" FirstPageText="First" LastPageText="Last" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <div class="table-responsive">
                                <table id="sample-table-1" class="table table-striped table-bordered table-hover1">
                                    <thead class="table-header">
                                        <tr>
                                            <th style="width:15%;text-align:center;" > UserName </th>
                                            <th style="width:20%;text-align:center;" > NavigationUrl </th>
                                            <th style="width:20%;text-align:center;"> ClientEntityName </th>
                                            <th style="width:10%;text-align:center;"> AgreementID </th>
                                            <th style="width:20%;text-align:center;"> DateTime  </th>
                                            <th style="width:15%;text-align:center;"> SavedReportID </th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="table-responsive">
                                <table id="sample-table-1" class="table table-striped table-bordered table-hover1">
                                    <tbody>
                                        <tr>
                                            <td style="width:15%;text-align:center;"><%#Eval("UserName")%> </td>
                                            <td style="width:20%;text-align:center;"><%#Eval("NavigationUrl")%> </td>
                                            <td style="width:20%;text-align:center;"><%#Eval("ClientEntityName") %> </td>
                                            <td style="width:10%;text-align:center;"><%#Eval("AgreementID")%> </td>
                                            <td style="width:20%;text-align:center;"><%#Eval("DateTime")%> </td>
                                            <td style="width:15%;text-align:center;"><%#Eval("SavedReportID")%> </td>
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
</asp:Content>
