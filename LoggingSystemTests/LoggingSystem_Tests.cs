using System.Runtime.CompilerServices;
using NLog;
using NUnit.Framework;
using Slf;
using Slf.BitFactoryFacade;
using Slf.Factories;

namespace LoggingSystemTests
{

    [TestFixture]
// ReSharper disable InconsistentNaming
    public class LoggingSystem_Tests
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


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

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        //[Test]
        //public void BitFactoryLoggingExplicitly()
        //{
        //    var logger = BitFactoryLogger.CreateRollingFileLogger(@"c:\log\log_{0}.txt", 1000);
        //    logger.Info("Hello New Logger");
        //    logger.Info("Hello2 New Logger");
        //    logger.Info("Hello3 New Logger {0}, and {1}", "ABC", "DEF");
        //}

        [Test]
        public void NLogTest()
        {
            logger.Trace("Sample informational message");

        }



    }
}
