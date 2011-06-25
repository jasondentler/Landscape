using System;
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

            AddProduct("Capuccino", 6.7M, new Dictionary<string, string[]>()
                                              {
                                                  {"Milk", milk},
                                                  {"Shots", shots},
                                                  {"Size", size}
                                              });
            AddProduct("Cookie", 1M, new Dictionary<string, string[]>()
                                         {
                                             {"Kind", kind}
                                         });

            AddProduct("Espresso", 6.9M, new Dictionary<string, string[]>()
                                             {
                                                 {"Milk", milk},
                                                 {"Shots", shots},
                                                 {"Size", size}
                                             });

            AddProduct("Hot Chocolate", 10.5M, new Dictionary<string, string[]>()
                                                   {
                                                       {"Milk", milk},
                                                       {"Size", size},
                                                       {"Whipped Cream", whippedCream}
                                                   });
            AddProduct("Latte", 7.6M, new Dictionary<string, string[]>()
                                          {
                                              {"Milk", milk},
                                              {"Shots", shots},
                                              {"Size", size}
                                          });

            AddProduct("Tea", 8.4M, new Dictionary<string, string[]>()
                                        {
                                            {"Milk", milk},
                                            {"Shots", shots},
                                            {"Size", size}
                                        });

        }

        private void AddProduct(
            string name,
            decimal price,
            IDictionary<string, string[]> customizations)
        {
            var productId = AddProduct(name, price);
            AddCustomizations(productId, customizations);
        }

        private Guid AddProduct(string name, decimal price)
        {
            var productId = Guid.NewGuid();
            DomainHelper.SetId<Product>(productId, name);

            var e = new ProductAdded(productId, name, price);
            DomainHelper.GivenEvent<Product>(e);
            return productId;
        }

        private void AddCustomizations(
            Guid productId, 
            IDictionary<string, string[]> customizations)
        {
            foreach (var item in customizations)
                AddCustomization(productId, item.Key, item.Value);
        }

        private void AddCustomization(
            Guid productId,
            string customization,
            string[] options)
        {
            var e = new CustomizationAdded(productId, customization, options);
            DomainHelper.GivenEvent<Product>(e);
        }

    }
}
