using System;
using System.Linq;
using Example.Menu;
using NUnit.Framework;
using SharpTestsEx;

namespace Example.ReadModel.Tests.MenuListTests
{

    [TestFixture]
    public class RetrieveSecondPageOfMenu : 
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
            return menuList.GetAllItems(2, 2);
        }

        [Test]
        public void ThenItDoesNotThrow()
        {
            Exception.Should().Be.Null();
        }

        [Test]
        public void ThenOnlyOnetemIsReturned()
        {
            Result.Items.Count().Should().Be.EqualTo(1);
        }

        [Test]
        public void ThenTheLastItemIsReturned()
        {
            var expected = GivenEvents.Cast<ItemAdded>().OrderBy(e => e.Name).Last();
            var actual = Result.Items.Single();
            actual.Name.Should().Be.EqualTo(expected.Name);
            actual.Id.Should().Be.EqualTo(expected.MenuItemId);
            actual.Price.Should().Be.EqualTo(expected.Price);
        }

        [Test]
        public void ThenThereAreTwoPages()
        {
            Result.PageCount.Should().Be.EqualTo(2);
        }

        [Test]
        public void ThenPageTwoIsReturned()
        {
            Result.PageNumber.Should().Be.EqualTo(2);
        }

        [Test]
        public void ThenThereAreUpToTwoItemsPerPage()
        {
            Result.ItemsPerPage.Should().Be.EqualTo(2);
        }

    }
}
