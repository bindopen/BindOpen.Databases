﻿using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Databases.Data.Models;
using BindOpen.Databases.Data.Queries;
using BindOpen.Tests.Databases.PostgreSql.Data.Dtos.Test1;
using BindOpen.Tests.Databases.PostgreSql.Data.Entities.Test1;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Models
{
    /// <summary>
    /// This class represents a test database model.
    /// </summary>
    public partial class TestDbModel : BdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpsertEmployee(EmployeeDto employee)
        {
            return DbFluent.Upsert(Table("Employee"))
                .WithQueries(
                    DbFluent.UpdateQuery(Table("Employee"))
                    .WithFields(q => new[]
                    {
                        DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), q.UseParameter("contactEmail", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), q.UseParameter("lastName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.IntegrationDate), q.UseParameter("integrationDate", DataValueType.Date))
                    }),
                    DbFluent.InsertQuery(Table("Employee"))
                        .WithFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), q.UseParameter("contactEmail", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), q.UseParameter("lastName", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.IntegrationDate), q.UseParameter("integrationDate", DataValueType.Date))
                        })
                        .WithParameters(
                            ElementFactory.CreateScalar("code", employee.Code),
                            ElementFactory.CreateScalar("contactEmail", employee.ContactEmail),
                            ElementFactory.CreateScalar("fisrtName", employee.FisrtName),
                            ElementFactory.CreateScalar("lastName", employee.LastName),
                            ElementFactory.CreateScalar("integrationDate", employee.IntegrationDate)))
                .WithCTE(
                    DbFluent.TableAsQuery(
                        DbFluent.SelectQuery(null)
                            .WithFields(q => new[]
                            {
                                DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))
                            }))
                    .WithAlias("T"))
                .WithParameters(
                    ElementFactory.CreateScalar("code", employee.Code),
                    ElementFactory.CreateScalar("contactEmail", employee.ContactEmail),
                    ElementFactory.CreateScalar("fisrtName", employee.FisrtName),
                    ElementFactory.CreateScalar("lastName", employee.LastName),
                    ElementFactory.CreateScalar("integrationDate", employee.IntegrationDate));
        }
    }
}
