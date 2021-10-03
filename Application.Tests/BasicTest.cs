using Application.Resources.Data;
using Moq;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Application.Tests
{
    public class BasicTest
    {
        [Fact]
        public void TransactionDao_Constructor_Throws_If_Session_IsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TransactionDao(null));
        }

        [Fact]
        public void TransactionDao_Constructor_Works_If_Session_IsNotNull()
        {
            var session = new Mock<ISession>();
            try
            {
                var personDao = new TransactionDao(session.Object);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
