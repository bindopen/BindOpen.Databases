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
        internal IDbQuery InsertEmployee1(EmployeeDto employee)
        {
            return DbFluent.InsertQuery(Table<DbEmployee>())
                .WithFields(q => new[]
                {
                    DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployee>(p=> p.EmployeeId)
                })
                .WithParameters(
                    ElementFactory.CreateScalar("code", employee.Code),
                    ElementFactory.CreateScalar("ByteArrayField", employee.ByteArrayField),
                    ElementFactory.CreateScalar("DoubleField", employee.DoubleField),
                    ElementFactory.CreateScalar("DateTimeField", employee.DateTimeField),
                    ElementFactory.CreateScalar("LongField", employee.LongField));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee2(EmployeeDto employee)
        {
            return DbFluent.InsertQuery(Table<DbEmployee>())
                .WithFields(q => new[]
                {
                    DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("newCode", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                })
                .WithIdFields(q => new[]
                {
                    DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("oldCode", DataValueTypes.Text))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployee>(p=> p.EmployeeId)
                })
                .WithParameters(
                    ElementFactory.CreateScalar("newCode", employee.Code),
                    ElementFactory.CreateScalar("oldCode", "oldCode"),
                    ElementFactory.CreateScalar("ByteArrayField", employee.ByteArrayField),
                    ElementFactory.CreateScalar("DoubleField", employee.DoubleField),
                    ElementFactory.CreateScalar("DateTimeField", employee.DateTimeField),
                    ElementFactory.CreateScalar("LongField", employee.LongField));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee3(EmployeeDto employee)
        {
            return DbFluent.InsertQuery(Table<DbEmployee>())
                .WithFields(q => new[]
                {
                    DbFluent.Field(nameof(DbEmployee.Code)),
                    DbFluent.Field(nameof(DbEmployee.ByteArrayField)),
                    DbFluent.Field(nameof(DbEmployee.DoubleField)),
                    DbFluent.Field(nameof(DbEmployee.DateTimeField)),
                    DbFluent.Field(nameof(DbEmployee.LongField))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployee>(p=> p.EmployeeId)
                })
                .From(p => p.UseSubQuery(
                    DbFluent.SelectQuery(Table<DbEmployee>())
                        .WithFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("newCode", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueTypes.Date)),

                            DbFluent.FieldAsQuery<DbContact>(p => p.ContactId,
                                DbFluent.SelectQuery(Table<DbContact>())
                                    .WithLimit(1)
                                    .AddField(Field<DbContact>(f=>f.ContactId))
                                    .AddIdField(
                                        DbFluent.FieldAsParameter<DbContact>(f => f.Code, q.UseParameter("contactCode", DataValueTypes.Text)))),
                        })
                        .WithIdFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("oldCode", DataValueTypes.Text))
                        })
                        .WithParameters(
                            ElementFactory.CreateScalar("newCode", employee.Code),
                            ElementFactory.CreateScalar("contactCode", "contactCodeA"),
                            ElementFactory.CreateScalar("oldCode", "oldCode"),
                            ElementFactory.CreateScalar("ByteArrayField", employee.ByteArrayField),
                            ElementFactory.CreateScalar("DoubleField", employee.DoubleField),
                            ElementFactory.CreateScalar("DateTimeField", employee.DateTimeField),
                            ElementFactory.CreateScalar("LongField", employee.LongField))));
        }
    }
}
