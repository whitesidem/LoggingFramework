using System;
using System.Collections.Generic;
using LoggingSystem;
using LoggingSystem.Interfaces;
using LoggingSystem.LogPublisher;
using LoggingSystem.Models;
using Moq;
using NLog;
using NUnit.Framework;
namespace LoggingSystemTests
{

    [TestFixture]
// ReSharper disable InconsistentNaming
    public class LoggingSystem_Tests : BaseLogPublisher
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly List<string> _loggedMessages = new List<string>();
        private readonly Guid TestGuid = Guid.Empty;
        private Mock<ILogPublisherFactory> _logPublisherFactoryMock;
        private LogSystem _loggingSystm;
        private const string TestMessage1 = "Test Message1";
        private const string TestDateTimeString = "2014-03-02T19:16:02.5050832+00:00";

        public LoggingSystem_Tests(): base(logger)
        {
            
        }


        public LoggingSystem_Tests(Logger logger) : base(logger)
        {
        }


        [SetUp]
        public void SetUp()
        {
            LogSystem.AmbientDate =  DateTime.Parse(TestDateTimeString); 
            _loggedMessages.Clear();
            _logPublisherFactoryMock = new Mock<ILogPublisherFactory>();
            _logPublisherFactoryMock.Setup(m => m.CreatePublisher(It.IsAny<LoggingLevel>(), Logger)).Returns(this);
            _loggingSystm = new LogSystem(TestGuid, _logPublisherFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            LogSystem.AmbientDate = null;
        }


        [Test, Category("Implementation Test")]
        public void NLog_DirectCall_Test()
        {
            //Check result in Sentinal.exe
            logger.Trace("Another test 2");
        }

        [Test, Category("Integration Test")]
        public void NLog_DirectCall_JsonTest()
        {
            var example =
                @"{'timestamp': '2014-03-02T19:16:02.5050832+00:00','facility': 'clientSide','clientip': '123.123.123.123','domain': 'www.example.com','server': 'abc-123','request': '/page/request','pagename': 'funnel:example com:page1','searchKey': '1234567890_','sessionID': '11111111111111','event1': 'loading','event2': 'interstitial display banner','severity': 'WARN','short_message': '....meaning short message for aggregation...','full_message': 'full LOG message','userAgent': '...blah...blah..blah...','RT': 2}";
            example = example.Replace("'", "\"");

            //Check result in Sentinal.exe
            logger.Info(example);
        }

        [Test, Category("Integration Test")]
        public void NLogJsonFormatTest()
        {
            //Check result in Sentinal.exe


        //    logger.Info(example);
        }

        [Test]
        public void Log_GivenMockPublisherWithNoParams_LogsCorrectMessage()
        {

            //Act
            _loggingSystm.Log(Logger, LoggingLevel.Debug, TestMessage1, null);


            //Assert
            Assert.That(_loggedMessages.Count, Is.EqualTo(1), "Not expected number of logs");
            
            Assert.That(_loggedMessages[0], Is.StringContaining(TestMessage1));

            string expectedMessage = String.Format("{{\"DateTime\":\"{0}\",\"Method\":\"{1}\",\"UserId\":\"00000000-0000-0000-0000-000000000000\",\"Message\":\"Test Message1\"}}", TestDateTimeString ,"Log_GivenMockPublisherWithNoParams_LogsCorrectMessage");
            Assert.That(_loggedMessages[0], Is.EqualTo(expectedMessage));

        }

        [Test]
        public void Log_GivenMockPublisherWithParams_LogsCorrectMessage()
        {

            //Act
            _loggingSystm.Log(Logger, LoggingLevel.Debug, TestMessage1, new KeyValueCollection(new KeyValue("Age","9"), new KeyValue("Year","2014")));


            //Assert
            Assert.That(_loggedMessages.Count,Is.EqualTo(1),"Not expected number of logs");
            
            Assert.That(_loggedMessages[0], Is.StringContaining(TestMessage1));

            string expectedMessage = String.Format("{{\"DateTime\":\"{0}\",\"Method\":\"{1}\",\"UserId\":\"00000000-0000-0000-0000-000000000000\",\"Message\":\"Test Message1\",\"Age\":\"9\",\"Year\":\"2014\"}}", TestDateTimeString, "Log_GivenMockPublisherWithParams_LogsCorrectMessage");
            Assert.That(_loggedMessages[0], Is.EqualTo(expectedMessage));

        }



        public override bool IsLogLevelEnabled
        {
            get { return true; }
        }

        public override void LogMessage(string message)
        {
            _loggedMessages.Add(message); 
        }

        public override void LogException(string message, Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
