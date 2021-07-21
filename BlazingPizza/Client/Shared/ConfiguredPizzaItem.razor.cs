using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Shared
{
    public partial class ConfiguredPizzaItem
    {
        [Parameter]
        public Pizza Pizza { get; set; }

        [Parameter]
        public EventCallback OnRemoved { get; set; }
    }
}
