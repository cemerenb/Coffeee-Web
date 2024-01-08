<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="Coffeee_Web.OrderDetails" Async="true" %>

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
        .orderImageHeader {
    background-color: white;
   color: white;
    padding: 10px;
    text-align: center;
    border-bottom: 1px solid #ddd;
    border-radius: 8px 8px 0 0;
}

        .itemImage {
            width: 100px; /* Belirli bir genişlik değeri */
            height: auto; /* Otomatik yükseklik ayarı */
            border-radius: 15px;
            box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.1);
            margin-right: 10px; /* Ayarlanabilir */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="orderContainer">

            <asp:ListView ID="lvOrders" runat="server" ItemPlaceholderID="itemPlaceholder">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li class="orderCard">
                        <div class="orderCardHeader">
                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("MenuItemName") %>'></asp:Label>
                        </div>
                        <div class ="orderImageHeader">
                            <img runat="server" class="itemImage" src='<%# Eval("MenuItemImageLink") %>' alt='<%# Eval("MenuItemName") %>' />
                        </div>
                        <div class="orderCardContent">
                            Count: <asp:Label runat="server" ID="Label11" Text='<%# Eval("ItemCount") %>'></asp:Label>
                        </div>
                        <div class="orderCardContent">
                            Total: <asp:Label runat="server" ID="Label2" Text='<%# Eval("MenuItemTotal") %>'></asp:Label>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
