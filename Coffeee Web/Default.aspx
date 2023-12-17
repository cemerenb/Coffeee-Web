<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Coffeee_Web.Default" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport"/>
    <title>Login Page</title>
    <style>
        body {
    margin: 0;
    padding: 0;
    font-family: Arial, sans-serif;
}

.container {
    display: flex;

}

.column1{
    width:60%;
    margin-right: auto; 
    margin-left: 10%;
    margin-top:15%;
}


.switch-store-button {
    background-color: #d2b48c; /* Latte color */
    border-radius: 50px;
    padding: 10px;
    position: absolute;
    width:120px;
    height:35px;
    top: 70px;
    left: 50px;
    cursor: pointer;
}

.login-container {
    width: 60%;
    margin: 0 auto;
    display: flex;
    flex-direction: column;
    align-items: center;
    height: 100%;
}

.input-field {
    width: 100%;
    margin-bottom: 20px;
    position: relative;
    padding-top:10px;
padding-right:10px;
}

label {
    display: block;
    margin-bottom: 5px;
}

input {
    width: 100%;
    height:50px;
    
    border: 1px solid #000;
    border-radius: 10px;
}

.forgot-password {
    left:100px;
    text-align: end;
    margin-bottom: 20px;
}

.password-icon {
    position: absolute;
    top: 50%;
    right: 10px;
    transform: translateY(-50%);
    cursor: pointer;
}

.login-button {
    height:50px;
    width: 100%;
    background-color: #1e232c;
    color: #fff;
    
    border-radius: 10px;
    cursor: pointer;
}

.empty-container {
    height: 150px;
}

.register-container {
    align-content:start;
    display: flex;
    justify-content: space-between;
    margin-top: 20px;
}

.register-button {
    background-color: transparent; /* Latte color */
    border-radius: 10px;
    
    cursor: pointer;
}

    </style>
    <script>
        function togglePasswordVisibility() {
            var passwordInput = document.getElementById('<%= PasswordTextBox.ClientID %>');
            var passwordIcon = document.getElementById('password-icon');

            // Toggle the password input type between text and password
            passwordInput.type = (passwordInput.type === 'password') ? 'text' : 'password';

            // Change the eye icon based on the password visibility
            var iconImageUrl = (passwordInput.type === 'password') ? 'https://cdn-icons-png.flaticon.com/512/63/63498.png' : 'https://cdn3.iconfinder.com/data/icons/google-material-design-icons/48/ic_visibility_48px-512.png';
            passwordIcon.src = iconImageUrl;
        }

        function showErrorPopup() {
            console.log('showErrorPopup function called.');
            alert('Login failed. Please check your credentials.');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="column1">
                <div class="login-container">
                    <div>
                        <h1 style="text-align:start;">Store Panel</h1>
                    </div>
                    <div class="input-field">
                        <asp:Label runat="server" AssociatedControlID="EmailTextBox">Email</asp:Label>
                        <asp:TextBox runat="server" ID="EmailTextBox" placeholder="Enter your email"></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <asp:Label runat="server" AssociatedControlID="PasswordTextBox">Password</asp:Label>
                        <div style="position: relative;">
                            <asp:TextBox runat="server" ID="PasswordTextBox" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                            <img style="height:20px; width:20px;" class="password-icon" id="password-icon" src="https://cdn-icons-png.flaticon.com/512/63/63498.png" alt="Visibility Icon" onclick="togglePasswordVisibility()" />
                        </div>
                    </div>
                    
                    <asp:Button ID="LoginButton" runat="server" Text="Log In" CssClass="login-button" OnClick="LoginButton_Click" />
                    
                </div>
                <div class="empty-container"></div>
            </div>
            <div style="margin-left: auto; margin-right: 0; margin-bottom:0; object-fit:cover; height:100%;" class="column2">
                <img style="object-fit:cover" src="https://i.ibb.co/frTJmF9/login-bg.png" alt="Login Background" />
            </div>
        </div>
    </form>
</body>
</html>