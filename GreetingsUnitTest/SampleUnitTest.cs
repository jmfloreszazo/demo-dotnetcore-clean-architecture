using GreetingsCore.Adapters.Db;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GreetingsUnitTest
{
    public class Tests
    {
        private GreetingContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GreetingContext>().Options;
            _context = new GreetingContext(options);            
        }

        [Test]
        public void CanConnnectToDataBase_ReturnFalse()
        {
            var isConnected =  _context.Database.CanConnect();
            Assert.IsFalse(isConnected, "It can't connect with data base.");
        }

    }
}