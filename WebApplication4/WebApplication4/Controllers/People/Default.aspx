<%@ Page Title="PersonList" Language="C#" CodeBehind="Default.aspx.cs" Inherits="WebApplication4.Controllers.People.Default" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>

	<!-- Bootstrap CSS -->
	<link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css">
</head>
<body>
    <form runat="server" id="form1">
	<div class="container body-content">
        <h2>People List</h2>
        <p>
            <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
        </p>
        <div>
            <asp:ListView id="ListView1" runat="server"
                DataKeyNames="ID" 
    			ItemType="WebApplication4.Models.Person"
                SelectMethod="GetData">
                <EmptyDataTemplate>
                    There are no entries found for People
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>
    								<asp:LinkButton Text="ID" CommandName="Sort" CommandArgument="ID" runat="Server" />
    							</th>
                                <th>
    								<asp:LinkButton Text="Name" CommandName="Sort" CommandArgument="Name" runat="Server" />
    							</th>
                                <th>&nbsp;</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr runat="server" id="itemPlaceholder" />
                        </tbody>
                    </table>
    				<asp:DataPager PageSize="5"  runat="server">
    					<Fields>
                            <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn" />
                            <asp:NumericPagerField ButtonType="Button"  NumericButtonCssClass="btn" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                            <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn" />
                        </Fields>
    				</asp:DataPager>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
    							<td>
    								<asp:DynamicControl runat="server" DataField="ID" ID="ID" Mode="ReadOnly" />
    							</td>
    							<td>
    								<asp:DynamicControl runat="server" DataField="Name" ID="Name" Mode="ReadOnly" />
    							</td>
                        <td>
    					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Controllers/People/Details", Item.ID) %>' Text="Details" /> | 
    					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Controllers/People/Edit", Item.ID) %>' Text="Edit" /> | 
                            <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/Controllers/People/Delete", Item.ID) %>' Text="Delete" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
	</div>
    </form>

	<!-- Bootstrap JavaScript -->
	<script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
</body>
</html>

