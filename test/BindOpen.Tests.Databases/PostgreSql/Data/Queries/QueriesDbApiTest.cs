﻿using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.Data.Models;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.Data.Queries
{
    [TestFixture]
    public class QueriesDbApiTest
    {
        TestDbModel _model;
        IBdoDbConnector _dbConnector;

        [SetUp]
        public void Setup()
        {
            _model = GlobalVariables.AppHost.GetModel<TestDbModel>();
            _dbConnector = GlobalVariables.AppHost.CreatePostgreSqlConnector();
        }

        [Test]
        public void SimpleSelectWithApi1()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*,""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"",""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode1("codeC"));

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleSelectWithApi2()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*,""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"",""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode2("codeC"));

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleSelectWithApi3()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*,""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"",""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode3("codeC"));

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}
