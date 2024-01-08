using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Coffeee_Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected async void LoginButton_Click(object sender, EventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                String loginMessage = await CallLoginApi(email, password);

                if (loginMessage != "Error")
                {
                     SaveEmailAsCookie(email);
                    Response.Redirect($"Orders.aspx?token={HttpUtility.UrlEncode(loginMessage)}");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showErrorPopup", "showErrorPopup();", true);
                }
            }
        }
        private void SaveEmailAsCookie(string email)
        {
            HttpCookie emailCookie = new HttpCookie("Email");

            emailCookie.Value = email;

            Response.SetCookie(emailCookie);
        }
        private async Task<String> CallLoginApi(string email, string password)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiEndpoint = "http://localhost:7094/api/Store/login";

                    var requestData = new { storeEmail = email, storePassword = password };

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiEndpoint, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        string[] parts = responseContent.Split('-');

                        if (parts.Length > 0)
                        {
                            Console.WriteLine("Logged in successfully. Log: " + parts[0]);
                        }

                        return parts[0];
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Login failed. Error: " + errorResponse);

                        return "Error";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during login: " + ex.Message);
                return "Error";
            }
        }

    }
}
