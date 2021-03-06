﻿using System;
using System.Collections.Generic;
using System.Linq;
using Example.Menu;
using NUnit.Framework;
using SharpTestsEx;

namespace Example.ReadModel.Tests.MenuListTests
{

    [TestFixture]
    public class RetrievePageOfMenu : 
        ConventionQueryFixture<MenuList.Model, MenuList>
    {

        protected override void Given()
        {
            GivenEvent(new ItemAdded(Guid.NewGuid(), "Latte", 3M));
            GivenEvent(new ItemAdded(Guid.NewGuid(), "Hot Chocolate", 2M));
            GivenEvent(new ItemAdded(Guid.NewGuid(), "Coffee", 12.5M));
        }

        protected override MenuList.Model WhenQuerying()
        {
            var menuList = new MenuList();
            return menuList.GetAllItems(1, 2);
        }

        [Test]
        public void ThenItDoesNotThrow()
        {
            Exception.Should().Be.Null();
        }

        [Test]
        public void ThenOnlyTwoItemsAreReturned()
        {
            Result.Items.Count().Should().Be.EqualTo(2);
        }

        [Test]
        public void ThenTheItemsAreInAlphabeticalOrder()
        {
            Result.Items.Select(item => item.Name).Should().Be.OrderedAscending();
        }

        [Test]
        public void ThenThereAreTwoPages()
        {
            Result.PageCount.Should().Be.EqualTo(2);
        }

        [Test]
        public void ThenPageOneIsReturned()
        {
            Result.PageNumber.Should().Be.EqualTo(1);
        }

        [Test]
        public void ThenThereAreUpToTwoItemsPerPage()
        {
            Result.ItemsPerPage.Should().Be.EqualTo(2);
        }

    }
}
