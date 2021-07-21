using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Index
    {        
        [Inject] 
        public HttpClient HttpClient { get; set; }
        List<PizzaSpecial> Specials;                // Lista de productos
        Pizza ConfiguringPizza;                     // Detalle de la Orden
        bool ShowingConfigureDialog;
        Order Order = new();

        protected async override Task OnInitializedAsync()
        {
            Specials = await HttpClient
               .GetFromJsonAsync<List<PizzaSpecial>>("specials");
        }        

        void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>()
            };
            ShowingConfigureDialog = true;
        }

        void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }               

        void ConfirmConfigurePizzaDialog()
        {
            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }

        void RemoveConfiguredPizza(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);
        }

        async Task PlaceOrder()
        {
            await HttpClient.PostAsJsonAsync("orders", Order);
            Order = new Order();
        }
    }    
}
