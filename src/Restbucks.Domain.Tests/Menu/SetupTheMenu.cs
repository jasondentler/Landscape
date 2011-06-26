﻿using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Restbucks.Menu
{
    [Binding]
    public class SetupTheMenu
    {

        [Given(@"the franchise owner has set up the menu")]
        public void GivenTheFranchiseOwnerHasSetUpTheMenu()
        {

            var milk = new[] {"skim", "semi", "whole"};
            var shots = new[] {"single", "double", "triple"};
            var size = new[] {"small", "medium", "large"};
            var kind = new[] {"chocolate chip", "ginger"};
            var whippedCream = new[] {"yes", "no"};

            AddMenuItem("Cappuccino", 6.7M, new Dictionary<string, string[]>()
                                              {
                                                  {"Milk", milk},
                                                  {"Shots", shots},
                                                  {"Size", size}
                                              });
            AddMenuItem("Cookie", 1M, new Dictionary<string, string[]>()
                                         {
                                             {"Kind", kind}
                                         });

            AddMenuItem("Espresso", 6.9M, new Dictionary<string, string[]>()
                                             {
                                                 {"Milk", milk},
                                                 {"Shots", shots},
                                                 {"Size", size}
                                             });

            AddMenuItem("Hot Chocolate", 10.5M, new Dictionary<string, string[]>()
                                                   {
                                                       {"Milk", milk},
                                                       {"Size", size},
                                                       {"Whipped Cream", whippedCream}
                                                   });
            AddMenuItem("Latte", 7.6M, new Dictionary<string, string[]>()
                                          {
                                              {"Milk", milk},
                                              {"Shots", shots},
                                              {"Size", size}
                                          });

            AddMenuItem("Tea", 8.4M, new Dictionary<string, string[]>()
                                        {
                                            {"Milk", milk},
                                            {"Shots", shots},
                                            {"Size", size}
                                        });

        }

        private void AddMenuItem(
            string name,
            decimal price,
            IDictionary<string, string[]> customizations)
        {
            var menuItemId = AddMenuItem(name, price);
            AddCustomizations(menuItemId, customizations);
        }

        private Guid AddMenuItem(string name, decimal price)
        {
            var menuItemId = Guid.NewGuid();
            AggregateRootHelper.SetIdFor<MenuItem>(menuItemId, name);

            var e = new MenuItemAdded(menuItemId, name, price);
            GivenHelper.GivenEvent<MenuItem>(e);
            return menuItemId;
        }

        private void AddCustomizations(
            Guid menuItemId, 
            IDictionary<string, string[]> customizations)
        {
            foreach (var item in customizations)
                AddCustomization(menuItemId, item.Key, item.Value);
        }

        private void AddCustomization(
            Guid menuItemId,
            string customization,
            string[] options)
        {
            var e = new CustomizationAdded(menuItemId, customization, options);
            GivenHelper.GivenEvent<MenuItem>(e);
        }

    }
}
