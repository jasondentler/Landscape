using System;
using NUnit.Framework;

namespace Example.ReadModel.Tests
{
    public abstract class Fixture
    {

        protected Exception Exception { get; private set; }

        [TestFixtureSetUp]
        public void Execute()
        {
            OnSetup();
            Given();
            try
            {
                When();
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }

        protected virtual void OnSetup()
        {
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            OnTeardown();
        }

        protected virtual void OnTeardown()
        {
        }

        protected virtual void Given()
        {
        }


        protected abstract void When();


    }
}
