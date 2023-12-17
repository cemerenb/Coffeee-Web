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
            // Handle the Log In button click event
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;

            // Example: Check if the email and password are valid
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                // Make an API call with the email and password
                bool loginSuccess = await CallLoginApi(email, password);

                // Check the API response
                if (loginSuccess)
                {
                    // Redirect to the Orders page with the user's email as a query parameter
                    Response.Redirect($"Orders.aspx?email={HttpUtility.UrlEncode(email)}");
                }
                else
                {
                    // Display an error message or redirect to a login failure page
                    ClientScript.RegisterStartupScript(this.GetType(), "showErrorPopup", "showErrorPopup();", true);
                }
            }
        }

        private async Task<bool> CallLoginApi(string email, string password)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Replace "YourApiEndpoint" with the actual API endpoint
                    string apiEndpoint = "http://localhost:7094/api/Store/login";

                    // Example: Prepare the request payload
                    var requestData = new { storeEmail = email, storePassword = password };

                    // Convert the request data to JSON
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    // Make a POST request to the API
                    HttpResponseMessage response = await client.PostAsync(apiEndpoint, jsonContent);

                    // Check if the API call was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Return true to indicate successful login
                        return true;
                    }
                    else
                    {
                        // Return false to indicate login failure
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return false;
            }
        }
    }
}
