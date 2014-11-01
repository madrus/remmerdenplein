<%@ Page Title="PersonInsert" Language="C#" CodeBehind="Insert.aspx.cs" Inherits="WebApplication4.Controllers.People.Insert" %>
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
        <div>
    		<p>&nbsp;</p>
            <asp:FormView runat="server"
                ItemType="WebApplication4.Models.Person" DefaultMode="Insert"
                InsertItemPosition="FirstItem" InsertMethod="InsertItem"
                OnItemCommand="ItemCommand" RenderOuterTable="false">
                <InsertItemTemplate>
                    <fieldset class="form-horizontal">
    				<legend>Insert Person</legend>
    		        <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
    						    <asp:DynamicControl Mode="Insert" DataField="Name" runat="server" />
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:Button runat="server" ID="InsertButton" CommandName="Insert" Text="Insert" CssClass="btn btn-primary" />
                                <asp:Button runat="server" ID="CancelButton" CommandName="Cancel" Text="Cancel" CausesValidation="false" CssClass="btn btn-default" />
                            </div>
    					</div>
                    </fieldset>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
	</div>
    </form>

	<!-- Bootstrap JavaScript -->
	<script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
</body>
</html>
