using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace LoggingSystemTests
{

    [TestFixture]
// ReSharper disable InconsistentNaming
    public class LoggingSystem_Tests
    {

        [Test]
        public void WhenLogging_CanGetCallingMethod()
        {

            //Act
            var callingMethod = TraceStubCall("Logged output");
            //Assert
            Assert.That(callingMethod, Is.EqualTo("WhenLogging_CanGetCallingMethod"));

        }

        private string TraceStubCall(string loggedOutput, [CallerMemberName] string caller = "")
        {
            Assert.That(loggedOutput, Is.Not.Null);
            return caller;
        }
    }
}
