using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coffeee_Web
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["OrderId"]))
            {
                // Retrieve the email parameter value
                string orderId = Request.QueryString["OrderId"];
                HttpCookie emailCookie = Request.Cookies["Email"];

                string email =  emailCookie.Value;
                
                // Call the API to get all orders
                var orders = await GetOrdersFromApi();
                var menu = await GetMenuFromApi();
                var filteredOrders = FilterOrdersByOrderId(orders,orderId);
                var filteredMenus = FilterOrdersByMenuItemId(menu, email);
                // Filter orders based on matching storeEmail
                var joinedData = from order in filteredOrders
                                 join menuItem in filteredMenus on order.MenuItemId equals menuItem.MenuItemId
                                 select new OrderMenuItem
                                 {
                                     OrderId = order.OrderId,
                                     MenuItemId = order.MenuItemId,
                                     UserEmail = order.UserEmail,
                                     ItemCount = order.ItemCount,
                                     ItemCanceled = order.ItemCanceled,
                                     CancelNote = order.CancelNote,
                                     MenuItemName = menuItem.MenuItemName,
                                     MenuItemImageLink = menuItem.MenuItemImageLink,
                                     MenuItemPrice = menuItem.MenuItemPrice,
                                     MenuItemTotal = menuItem.MenuItemPrice * order.ItemCount,
                                 };

                // Bind the filtered orders to a ListView or another control
                lvOrders.DataSource = joinedData.ToList();
                lvOrders.DataBind();
            }
            else
            {
                // Handle the case when the email parameter is not present
                Response.Redirect("LoginPage.aspx"); // Redirect to the login page or handle accordingly
            }
        }
        private async Task<Details[]> GetOrdersFromApi()
        {

            using (HttpClient client = new HttpClient())
            {
                // Replace "YourApiEndpoint" with the actual API endpoint for getting orders
                string apiEndpoint = "http://localhost:7094/api/OrderDetails/get-order-details";

                // Make a GET request to the API
                HttpResponseMessage response = await client.GetAsync(apiEndpoint);

                // Check if the API call was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to an array of Order objects
                    string responseData = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("API Response:");
                    System.Diagnostics.Debug.WriteLine(responseData);
                    return JsonConvert.DeserializeObject<Details[]>(responseData);
                }

                System.Diagnostics.Debug.WriteLine("API Error Status Code:");
                System.Diagnostics.Debug.WriteLine(response.StatusCode);
                return null;
            }


        }
        private decimal GetTotal(string text1, string text2)
        {
            decimal num1;
            decimal num2;
            decimal.TryParse(text1, out num1);
            decimal.TryParse(text2, out num2);
            return num1 * num2;
        }
        private async Task<Menu[]> GetMenuFromApi()
        {

            using (HttpClient client = new HttpClient())
            {
                // Replace "YourApiEndpoint" with the actual API endpoint for getting orders
                string apiEndpoint = "http://localhost:7094/api/Menu/get-all";

                // Make a GET request to the API
                HttpResponseMessage response = await client.GetAsync(apiEndpoint);

                // Check if the API call was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to an array of Order objects
                    string responseData = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("API Response:");
                    System.Diagnostics.Debug.WriteLine(responseData);
                    return JsonConvert.DeserializeObject<Menu[]>(responseData);
                }

                System.Diagnostics.Debug.WriteLine("API Error Status Code:");
                System.Diagnostics.Debug.WriteLine(response.StatusCode);
                return null;
            }


        }

        private Details[] FilterOrdersByOrderId(Details[] orders, string orderId)
        {
            System.Diagnostics.Debug.WriteLine("orders lenght");
            System.Diagnostics.Debug.WriteLine(orders.Length);
            
            // Filter orders based on matching storeEmail
            return orders.Where(order => order.OrderId == orderId).ToArray();
        }

        private Menu[] FilterOrdersByMenuItemId(Menu[] menus, string email)
        {
            System.Diagnostics.Debug.WriteLine("Menu lenght");
            System.Diagnostics.Debug.WriteLine(menus.Length);

            // Filter orders based on matching storeEmail
            return menus.Where(menu => menu.StoreEmail == email).ToArray();
        }
        private class OrderMenuItem
        {
            public string OrderId { get; set; }
            public string MenuItemId { get; set; }
            public string UserEmail { get; set; }
            public int ItemCount { get; set; }
            public int ItemCanceled { get; set; }
            public string CancelNote { get; set; }

            public string MenuItemName { get; set; }
            public string MenuItemImageLink { get; set; }
            public float MenuItemPrice { get; set; }
            public float MenuItemTotal {  get; set; }
        }


        public class Details
        {
            public string OrderId { get; set; } = string.Empty;
            public string MenuItemId { get; set; } = string.Empty;
            public string UserEmail { get; set; } = string.Empty;

            public int ItemCount { get; set; }
            public int ItemCanceled { get; set; }
            public string CancelNote { get; set; } = string.Empty;
        }

        public class Menu
        {
            public string StoreEmail { get; set; } = string.Empty;
            public string MenuItemName { get; set; } = string.Empty;
            public string MenuItemImageLink { get; set; } = string.Empty;
            public string MenuItemId { get; set; } = string.Empty;
            public float MenuItemPrice { get; set; }
        }
    }

}