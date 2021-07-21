using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Shared
{
    public partial class ConfigurePizzaDialog
    {
        [Parameter] 
        public Pizza Pizza { get; set; }
        IEnumerable<Topping> Toppings;

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }
        [Parameter]
        public EventCallback OnConfirm { get; set; }


        protected async override Task OnInitializedAsync()
        {
            Toppings = await HttpClient.GetFromJsonAsync<IEnumerable<Topping>>("toppings");
        }

        void AddTopping(Topping topping)
        {
            if (Pizza.Toppings.Find(pt => pt.Topping == topping) == null)
            {
                Pizza.Toppings.Add(new PizzaTopping { Topping = topping });
            }
        }

        void ToppingSelected(ChangeEventArgs e)
        {
            int index = Convert.ToInt32(e.Value);
            AddTopping(Toppings.ElementAt(index));
        }

        void RemoveTopping(Topping topping)
        {
            Pizza.Toppings.RemoveAll(pt => pt.Topping == topping);
        }
    }
}
