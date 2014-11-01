<%@ Page Title="Person Details" Language="C#" CodeBehind="Details.aspx.cs" Inherits="WebApplication4.Controllers.People.Details" %>
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
                ItemType="WebApplication4.Models.Person" DataKeyNames="ID"
                SelectMethod="GetItem"
                OnItemCommand="ItemCommand" RenderOuterTable="false">
                <EmptyDataTemplate>
                    Cannot find the Person with ID <%: Request.QueryString["ID"] %>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <fieldset class="form-horizontal">
                        <legend>Person Details</legend>
    							<div class="row">
    								<div class="col-sm-2 text-right">
    									<strong>ID</strong>
    								</div>
    								<div class="col-sm-4">
    									<asp:DynamicControl runat="server" DataField="ID" ID="ID" Mode="ReadOnly" />
    								</div>
    							</div>
    							<div class="row">
    								<div class="col-sm-2 text-right">
    									<strong>Name</strong>
    								</div>
    								<div class="col-sm-4">
    									<asp:DynamicControl runat="server" DataField="Name" ID="Name" Mode="ReadOnly" />
    								</div>
    							</div>
                     	<div class="row">
    					  &nbsp;
    					</div>
    					<div class="form-group">
    						<div class="col-sm-offset-2 col-sm-10">
    							<asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Back" CssClass="btn btn-default" />
    						</div>
    					</div>
                    </fieldset>
                </ItemTemplate>
            </asp:FormView>
        </div>
	</div>
    </form>

	<!-- Bootstrap JavaScript -->
	<script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>

</body>
</html>

