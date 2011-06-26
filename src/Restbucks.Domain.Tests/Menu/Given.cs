﻿using System;
using TechTalk.SpecFlow;

namespace Restbucks.Menu
{
    [Binding]
    public class Given
    {

        [Given(@"I have added coffee to the menu")]
        public void GivenIHaveAddedCoffeeToTheMenu()
        {
            var menuItemId = Guid.NewGuid();
            var name = "Coffee";
            var price = 7.20M;

            DomainHelper.SetId<MenuItem>(menuItemId, name);
            
            var e = new MenuItemAdded(menuItemId, name, price);
            DomainHelper.GivenEvent<MenuItem>(e);
        }

        [Given(@"I have added coffee sizes")]
        public void GivenIHaveAddedCoffeeSizes()
        {
            var name = "Coffee";
            var menuItemId = DomainHelper.GetId<MenuItem>(name);

            var e = new CustomizationAdded(menuItemId, "Size", new[] {"Short", "Tall", "Grande", "Venti"});
            DomainHelper.GivenEvent<MenuItem>(e);
        }





    }
}
