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
        /// <param name="code"></param>
        /// <param name="isPartialUpdate"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpdateEmployee1(string code, bool isPartialUpdate, EmployeeDto employee)
        {
            return this.UseQuery("UpdateEmployee1",
                p =>
                {
                    var query = DbFluent.UpdateQuery(Table("Employee"))
                        .From(
                            DbFluent.TableAsJoin(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>())
                                .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                        .WithReturnedIdFields(Field("Code", "Employee"));

                    query.AddField(
                        !isPartialUpdate || employee?.Code?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.ByteArrayField?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("byteArrayField", DataValueType.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.DateTimeField != null,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("doubleField", DataValueType.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.DoubleField != null,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("dateTimeField", DataValueType.Text)));

                    query.AddField(
                        true,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("longField", DataValueType.Date)));

                    query.AddField(
                        !isPartialUpdate || employee?.RegionalDirectorateCode?.Length > 0,
                        q => DbFluent.FieldAsQuery(nameof(DbEmployee.RegionalDirectorateId),
                                DbFluent.SelectQuery(Table<DbRegionalDirectorate>())
                                    .AddField(DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId)))
                                    .WithIdFields(
                                        DbFluent.FieldAsParameter(nameof(DbRegionalDirectorate.Code), q.UseParameter("directorateCode", DataValueType.Text)))));

                    return query;
                })
                .WithParameters(
                    ElementFactory.CreateScalar("code", code),
                    ElementFactory.CreateScalar("byteArrayField", employee.ByteArrayField),
                    ElementFactory.CreateScalar("doubleField", employee.DoubleField),
                    ElementFactory.CreateScalar("dateTimeField", employee.DateTimeField),
                    ElementFactory.CreateScalar("directorateCode", null),
                    ElementFactory.CreateScalar("longField", employee.LongField));
        }

        /// <summary>
        /// Updates the regional directorate of the specified Fidal employee.
        /// </summary>
        /// <param name="regionalDirectorateCode">The code of the regional directorate to consider.</param>
        /// <returns>Returns the query.</returns>
        internal IDbQuery UpdateEmployee2(string regionalDirectorateCode)
        {
            return this.UseQuery("UpdateEmployee2", p =>
                DbFluent.UpdateQuery(Table("Employee", "employee"))
                    .AddField(q => DbFluent.Field<DbEmployee>(q => q.RegionalDirectorateId).AsNull())
                    .From(Table<DbRegionalDirectorate>("regionalDirectorate"))
                    .AddIdField(q =>
                        DbFluent.FieldAsOther<DbEmployee>(
                            t => t.RegionalDirectorateId, Table("Employee", "employee"),
                            Field<DbRegionalDirectorate>(
                                t => t.RegionalDirectorateId, "regionalDirectorate")))
                    .AddIdField(q =>
                        DbFluent.FieldAsParameter<DbEmployee>(
                            t => t.Code,
                            DbFluent.Table("employee"),
                            q.UseParameter("regionalDirectorateCode", DataValueType.Text)))
                    .WithReturnedIdFields(Field("Code", "Employee", "employee"))
                )
                .WithParameters(
                    ElementFactory.CreateScalar("regionalDirectorateCode", regionalDirectorateCode));
        }

        /// <summary>
        /// Updates the regional directorate of the specified Fidal employee.
        /// </summary>
        /// <param name="regionalDirectorateCode">The code of the regional directorate to consider.</param>
        /// <returns>Returns the query.</returns>
        internal IDbQuery UpdateEmployee3(string regionalDirectorateCode)
        {
            return this.UseQuery("UpdateFidalEmployee_RegionalDirectorateAsNull", p =>
                DbFluent.UpdateQuery(Table("Employee", "employee"))
                    .AddField(q => DbFluent.Field<DbEmployee>(q => q.RegionalDirectorateId).AsNull())
                    .AddField(q => DbFluent.FieldAsParameter<DbEmployee>(q => q.LongField, ElementFactory.CreateScalar("length", DataValueType.Long)))
                    .From(Table<DbRegionalDirectorate>("regionalDirectorate"))
                    .AddIdField(q =>
                        DbFluent.FieldAsOther<DbEmployee>(
                            t => t.RegionalDirectorateId, Table("Employee", "employee"),
                            Field<DbRegionalDirectorate>(
                                t => t.RegionalDirectorateId, "regionalDirectorate")))
                    .AddIdField(q =>
                        DbFluent.FieldAsParameter<DbEmployee>(
                            t => t.Code,
                            DbFluent.Table("employee"),
                            q.UseParameter("regionalDirectorateCode", DataValueType.Text)))
                    .WithReturnedIdFields(Field("Code", "Employee", "employee"))
                )
                .WithParameters(
                    ElementFactory.CreateScalar("regionalDirectorateCode", regionalDirectorateCode),
                    ElementFactory.CreateScalar("length", 2500));
        }
    }
}
