using System;
using Example.Menu;
using NUnit.Framework;
using SharpTestsEx;

namespace Example.ReadModel.Tests.MenuListTests
{

    [TestFixture]
    public class WhenAnItemIsAdded : 
        ConventionDenormalizerFixture<ItemAdded, MenuList>
    {
        protected override ItemAdded WhenEvent()
        {
            return new ItemAdded(Guid.NewGuid(), "Coffee", 12.5M);
        }

        [Test]
        public void ThenItDoesNotThrow()
        {
            Exception.Should().Be.Null();
        }

        [Test]
        public void ThenTheRowIsAdded()
        {
            var item = Db.MenuList.FindById(Event.MenuItemId);
            Assert.AreEqual(item.Id, Event.MenuItemId);
            Assert.AreEqual(item.Name, Event.Name);
            Assert.AreEqual(item.Price, Event.Price);
        }

    }
}
