using System;
using System.Reflection;
using NUnit.Framework;

namespace LoggingSystemTests
{
    public class TestExceptionThrowClass
    {

        public void ThrowNestedException()
        {
            RaiseDoubleNestedException();
        }

        private void RaiseDoubleNestedException()
        {
            try
            {
                RaiseNestedException();
            }
            catch (Exception ex)
            {
                throw new TargetException("I Are Top Outer Exception", ex);
            }
        }

        private void RaiseNestedException()
        {
            try
            {
                MethodThenThrowException();
            }
            catch (Exception ex)
            {
                throw new TargetException("I Are Middle Exception", ex);
            }
        }

        private void MethodThenThrowException()
        {
            ThrowException();
        }

        public void ThrowException()
        {
            throw new SuccessException("I Are Inner Raised Exception");
        }


    }
}
