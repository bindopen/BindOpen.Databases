﻿using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Databases.Data.Queries;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Queries
{
    [TestFixture]
    public class QueriesDbFluent
    {
        private IBdoHost _appHost;
        private DateTime _value_datetime = new DateTime(2020, 12, 20);

        [SetUp]
        public void Setup()
        {
            _appHost = GlobalVariables.AppHost;
        }

        [Test]
        public void TestSqlValue()
        {
            var expression = DbFluent.Value(_value_datetime);
            var log = new BdoLog();

            string expectedResult = @"$sqlValue('2020-12-20T00:00:00')";

            string result = (string)expression;

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void TestSqlLike()
        {
            var expression = DbFluent.Like(
                DbFluent.Table("MyTable"),
                DbFluent.Concat("%", DbFluent.Parameter("myText").AsExp(), "%"));
            var log = new BdoLog();

            string expectedResult = @"$sqlLike($sqlTable('MyTable'), $sqlConcatenate('%', $sqlParameter('myText'), '%'))";

            string result = (string)expression;

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}
