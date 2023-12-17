using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Coffeee_Web
{
    public partial class Orders : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Check if the email parameter exists in the query string
            if (!string.IsNullOrEmpty(Request.QueryString["email"]))
            {
                // Retrieve the email parameter value
                string userEmail = Request.QueryString["email"];

                // Call the API to get all orders
                var orders = await GetOrdersFromApi();

                // Filter orders based on matching storeEmail
                var userOrders = FilterOrdersByStoreEmail(orders, userEmail);

                // Bind the filtered orders to a ListView or another control
                lvOrders.DataSource = userOrders;
                lvOrders.DataBind();
            }
            else
            {
                // Handle the case when the email parameter is not present
                Response.Redirect("LoginPage.aspx"); // Redirect to the login page or handle accordingly
            }
        }

        private async Task<Order[]> GetOrdersFromApi()
        {
            
                using (HttpClient client = new HttpClient())
                {
                    // Replace "YourApiEndpoint" with the actual API endpoint for getting orders
                    string apiEndpoint = "http://localhost:7094/api/Order/get-orders";

                    // Make a GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiEndpoint);

                    // Check if the API call was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response content to an array of Order objects
                        string responseData = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine("API Response:");
                        System.Diagnostics.Debug.WriteLine(responseData);
                        return JsonConvert.DeserializeObject<Order[]>(responseData);
                    }

                    System.Diagnostics.Debug.WriteLine("API Error Status Code:");
                    System.Diagnostics.Debug.WriteLine(response.StatusCode);
                    return null;
                }
            
            
        }

        private Order[] FilterOrdersByStoreEmail(Order[] orders, string userEmail)
        {
            System.Diagnostics.Debug.WriteLine("orders lenght");
            System.Diagnostics.Debug.WriteLine(orders.Length);
            System.Diagnostics.Debug.WriteLine("Email");
            System.Diagnostics.Debug.WriteLine(userEmail);
            // Filter orders based on matching storeEmail
            return orders.Where(order => order.StoreEmail == userEmail).ToArray();
        }
        protected string SplitOrderCreatingTime(object orderCreatingTime)
        {
            if (orderCreatingTime != null)
            {
                // Convert the orderCreatingTime to string and split using 't'
                string[] parts = orderCreatingTime.ToString().Split('t');

                // Check if the array has at least two parts
                if (parts.Length >= 2)
                {
                    // Return the formatted date and time
                    return $"{parts[0]}  {parts[1]}";
                }
            }

            // If something goes wrong, return the original value
            return orderCreatingTime?.ToString();
        }
    }

    public class Order
    {
        public string OrderId { get; set; }
        public string UserEmail { get; set; }
        public string OrderStatus { get; set; }
        public string OrderTotalPrice { get; set; }
        public string OrderCreatingTime { get; set; }

        public string StoreEmail { get; set; }
        // Add other properties of your Order class
    }
    

}
