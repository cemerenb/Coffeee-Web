<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="Coffeee_Web.Orders" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport"/>
    <title>Orders Page</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }

        #orderContainer {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }

        .orderCard {
            list-style-type: none;
            margin: 0;
            padding: 0;
            border: 1px solid #ddd;
            border-radius: 8px;
            margin-bottom: 15px;
            overflow: hidden;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .orderCardContent {
            padding: 8px;
        }

        .orderCardHeader {
            background-color: #1e232c;
            color: #fff;
            padding: 10px;
            text-align: center;
            border-bottom: 1px solid #ddd;
            border-radius: 8px 8px 0 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="orderContainer">
            <h1>Your Orders</h1>
            <asp:ListView ID="lvOrders" runat="server" ItemPlaceholderID="itemPlaceholder">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li class="orderCard">
                        <div class="orderCardHeader">
    Order ID: <asp:Label runat="server" ID="Label1" Text='<%# Eval("OrderId") %>'></asp:Label>
</div>
                        
                        <div class="orderCardContent">
                            User Email: <asp:Label runat="server" ID="lblOrderId" Text='<%# Eval("UserEmail") %>'></asp:Label>
                            <!-- Example: Product, Quantity, Date, etc. -->
                        </div>
                        <div class="orderCardContent">
    Order Status: <asp:Label runat="server" ID="Label6" Text='<%# Eval("OrderStatus") %>'></asp:Label>
    <!-- Example: Product, Quantity, Date, etc. -->
</div>
                        <div class="orderCardContent">
    Order Total: <asp:Label runat="server" ID="Label11" Text='<%# Eval("OrderTotalPrice") %>'></asp:Label>₺
    <!-- Example: Product, Quantity, Date, etc. -->
</div>
                        <div class="orderCardContent">
    Order Date:<asp:Label runat="server" ID="Label16" Text='<%# SplitOrderCreatingTime(Eval("OrderCreatingTime"))%>'></asp:Label>
    <!-- Example: Product, Quantity, Date, etc. -->
</div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
