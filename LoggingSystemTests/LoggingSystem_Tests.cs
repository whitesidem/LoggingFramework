using System;
using System.Collections.Generic;
using LoggingSystem;
using LoggingSystem.Interfaces;
using LoggingSystem.LogPublisher;
using LoggingSystem.Models;
using Moq;
using NUnit.Framework;
namespace LoggingSystemTests
{

    [TestFixture]
// ReSharper disable InconsistentNaming
    public class LoggingSystem_Tests : BaseLogPublisher
    {
        private readonly List<string> _loggedMessages = new List<string>();
        private readonly List<string> _loggedExceptionMessages = new List<string>();
        private readonly Guid TestGuid = Guid.Empty;
        private Mock<ILogPublisherFactory> _logPublisherFactoryMock;
        private LogSystem _loggingSystm;
        private const string TestMessage1 = "Test Message1";
        private const string TestDateTimeString = "2014-03-02T19:16:02.5050832+00:00";
        private bool _isLogEnabled = true;

        [SetUp]
        public void SetUp()
        {
            _isLogEnabled = true;
            LogSystem.AmbientDate =  DateTime.Parse(TestDateTimeString); 
            _loggedMessages.Clear();
            _loggedExceptionMessages.Clear();
            _logPublisherFactoryMock = new Mock<ILogPublisherFactory>();
            _logPublisherFactoryMock.Setup(m => m.CreatePublisher(It.IsAny<LoggingLevel>())).Returns(this);
            _loggingSystm = new LogSystem(TestGuid, _logPublisherFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            LogSystem.AmbientDate = null;
        }


        [Test, Category("Integration Test")]
        public void NLogJsonFormatTest()
        {
            LogSystem.AmbientDate = null;

            //Check result in Sentinal.exe and Kibana viewer
            var loggingSystm = new LogSystem(TestGuid, new LogPublisherFactory());

            //Act
            loggingSystm.Log(LoggingLevel.Info, TestMessage1, null);
            loggingSystm.Log(LoggingLevel.Info, TestMessage1, new KeyValueCollection(new KeyValue("Age", "9"), new KeyValue("Year", "2014")));
            loggingSystm.Log(LoggingLevel.Info, TestMessage1, new KeyValueCollection(new KeyValue("Age", "9"), new KeyValue("Answer", null), new KeyValue("Year", "2014")));
        }

        [Test, Category("Integration Test")]
        public void ExceptionTest()
        {
            LogSystem.AmbientDate = null;

            //Check result in Sentinal.exe and Kibana viewer
            var loggingSystm = new LogSystem(TestGuid, new LogPublisherFactory());

            Exception testException = CreateNestedException();

            //Act
            loggingSystm.Log(testException, LoggingLevel.Error, TestMessage1, null);
            loggingSystm.Log(testException, LoggingLevel.Error, TestMessage1, new KeyValueCollection(new KeyValue("Age", "9"), new KeyValue("Year", "2014")));
            loggingSystm.Log(testException, LoggingLevel.Error, TestMessage1, new KeyValueCollection(new KeyValue("Age", "9"), new KeyValue("Answer", null), new KeyValue("Year", "2014")));
        }



        [Test]
        public void Log_GivenMockPublisherWithNoParams_LogsCorrectMessage()
        {

            //Act
            _loggingSystm.Log(LoggingLevel.Debug, TestMessage1, null);


            //Assert
            Assert.That(_loggedMessages.Count, Is.EqualTo(1), "Not expected number of logs");
            
            Assert.That(_loggedMessages[0], Is.StringContaining(TestMessage1));

            string expectedMessageContains1 = String.Format("{{\"LogType\":\"Debug\",\"DateTime\":\"{0}\",\"Method\":\"{1}\",\"LineNumber\":", TestDateTimeString, System.Reflection.MethodBase.GetCurrentMethod().Name);
            const string expectedMessageContains2 = ",\"UserId\":\"00000000-0000-0000-0000-000000000000\",\"Message\":\"Test Message1\",\"FilePath\":";
            Assert.That(_loggedMessages[0], Is.StringContaining(expectedMessageContains1));
            Assert.That(_loggedMessages[0], Is.StringContaining(expectedMessageContains2));

        }

        [Test]
        public void Log_GivenMockPublisherWithParams_LogsCorrectMessage()
        {

            //Act
            _loggingSystm.Log(LoggingLevel.Debug, TestMessage1, new KeyValueCollection(new KeyValue("Age","9"), new KeyValue("Year","2014")));


            //Assert
            Assert.That(_loggedMessages.Count,Is.EqualTo(1),"Not expected number of logs");
            
            Assert.That(_loggedMessages[0], Is.StringContaining(TestMessage1));

            string expectedMessageContains1 = String.Format("{{\"LogType\":\"Debug\",\"DateTime\":\"{0}\",\"Method\":\"{1}\",\"LineNumber\":", TestDateTimeString, System.Reflection.MethodBase.GetCurrentMethod().Name);
            const string expectedMessageContains2 = "\"UserId\":\"00000000-0000-0000-0000-000000000000\",\"Message\":\"Test Message1\",\"Age\":\"9\",\"Year\":\"2014\",\"FilePath\":";
            Assert.That(_loggedMessages[0], Is.StringContaining(expectedMessageContains1));
            Assert.That(_loggedMessages[0], Is.StringContaining(expectedMessageContains2));

        }

        [Test]
        public void Log_GivenMockPublisherWithException_LogsCorrectMessage()
        {

            //Act
            _loggingSystm.Log(new Exception("TestException"), LoggingLevel.Error, TestMessage1, null);


            //Assert
            Assert.That(_loggedExceptionMessages.Count, Is.EqualTo(1), "Not expected number of logs");

            Assert.That(_loggedExceptionMessages[0], Is.StringContaining(TestMessage1));

            string expectedMessageContains1 = String.Format("{{\"LogType\":\"Error\",\"DateTime\":\"{0}\",\"Method\":\"{1}\",\"LineNumber\":", TestDateTimeString, System.Reflection.MethodBase.GetCurrentMethod().Name);
            const string expectedMessageContains2 = ",\"UserId\":\"00000000-0000-0000-0000-000000000000\",\"Message\":\"Test Message1\",\"Exception\":\"System.Exception: TestException\",\"FilePath\":";
            Assert.That(_loggedExceptionMessages[0], Is.StringContaining(expectedMessageContains1));
            Assert.That(_loggedExceptionMessages[0], Is.StringContaining(expectedMessageContains2));

        }


        [Test]
        public void Log_GivenMockPublisherWithANullParamValue_LogsCorrectMessage()
        {

            //Act
            _loggingSystm.Log(LoggingLevel.Debug, TestMessage1, new KeyValueCollection(new KeyValue("Age", null)));


            //Assert
            Assert.That(_loggedMessages.Count, Is.EqualTo(1), "Not expected number of logs");

            Assert.That(_loggedMessages[0], Is.StringContaining(TestMessage1));

            string expectedMessageContains1 =
                String.Format("{{\"LogType\":\"Debug\",\"DateTime\":\"{0}\",\"Method\":\"{1}\",\"LineNumber\":", TestDateTimeString, System.Reflection.MethodBase.GetCurrentMethod().Name);
            const string expectedMessageContains2 = "\"UserId\":\"00000000-0000-0000-0000-000000000000\",\"Message\":\"Test Message1\",\"Age\":null,\"FilePath\":";
            Assert.That(_loggedMessages[0], Is.StringContaining(expectedMessageContains1));
            Assert.That(_loggedMessages[0], Is.StringContaining(expectedMessageContains2));
        }


        [Test]
        public void Log_GivenMockPublisherWithParams_DoesNotLogWhenLevelDisabled()
        {
            _isLogEnabled = false;

            //Act
            _loggingSystm.Log(LoggingLevel.Debug, TestMessage1, null);

            //Assert
            Assert.That(_loggedMessages.Count,Is.EqualTo(0),"Not expected number of logs");
        }



        public override bool IsLogLevelEnabled
        {
            get
            {
                return _isLogEnabled;
            }
        }

        public override void LogMessage(string message)
        {
            _loggedMessages.Add(message); 
        }

        public override void LogException(string message, Exception ex)
        {
            _loggedExceptionMessages.Add(message);
        }

        private Exception CreateNestedException()
        {
            var exceptionSimulator = new TestExceptionThrowClass();

            try
            {
                exceptionSimulator.ThrowNestedException();

            }
            catch (Exception ex)
            {
                return ex;
            }
            throw new NotImplementedException("Should never get ths");
        }

    }
}
