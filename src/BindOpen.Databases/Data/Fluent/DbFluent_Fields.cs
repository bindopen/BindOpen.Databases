﻿using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This static class represents a factory of data field.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        public static DbField Field(
            string name,
            IDbTable table = null)
            => new DbField()
            {
                Name = name,
                DataTableAlias = table?.Alias,
                DataTable = table?.Name,
                Schema = table?.Schema,
                DataModule = table?.DataModule,
            };

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <typeparam name="T">The class to consider.</typeparam>
        public static DbField Field<T>(
            Expression<Func<T, object>> expr,
            IDbTable table = null) where T : class
        {
            var propertyInfo = expr.GetProperty();
            var name = propertyInfo?.Name;
            var valueType = propertyInfo?.PropertyType.GetValueType() ?? DataValueTypes.None;

            if (propertyInfo?.GetCustomAttribute(typeof(BdoDbFieldAttribute)) is BdoDbFieldAttribute fieldAttribute)
            {
                name = fieldAttribute.Name;
                valueType = fieldAttribute.ValueType;
            }

            var field = DbFluent.Field(name, table);
            field.WithValueType(valueType);

            return field;
        }

        // As literal -----

        /// <summary>
        /// Updates the specified field as literal.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static IDbField AsLiteral(
            this IDbField field,
            object value,
            DataValueTypes valueType = DataValueTypes.Any)
        {
            if (field != null)
            {
                field.ValueType = valueType;
                if (value != null)
                {
                    field.Expression = value.ToString(field.ValueType).AsExpression(BdoExpressionKind.Literal);
                }
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField FieldAsLiteral(
            string name,
            object value,
            DataValueTypes valueType = DataValueTypes.Any)
        {
            return DbFluent.FieldAsLiteral(name, null, value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField FieldAsLiteral(
            string name,
            IDbTable table,
            object value,
            DataValueTypes valueType = DataValueTypes.Any)
        {
            var field = DbFluent.Field(name, table);
            field.AsLiteral(value, valueType);
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField FieldAsLiteral<T>(
            Expression<Func<T, object>> expr,
            object value) where T : class
        {
            return FieldAsLiteral<T>(expr, null, value);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField FieldAsLiteral<T>(
            Expression<Func<T, object>> expr,
            IDbTable table,
            object value) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);
            field.AsLiteral(value, field?.ValueType ?? DataValueTypes.None);
            return field;
        }

        // As script -----

        /// <summary>
        /// Updates the specified field as script.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static IDbField AsScript(
            this IDbField field,
            string script)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                if (script != null)
                {
                    field.Expression = script.AsExpression(BdoExpressionKind.Script);
                }
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField FieldAsScript(
            string name,
            string script)
        {
            return FieldAsScript(name, null, script);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField FieldAsScript(
            string name,
            IDbTable table,
            string script)
        {
            var field = DbFluent.Field(name, table);
            field.AsScript(script);
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField FieldAsScript<T>(
            Expression<Func<T, object>> expr,
            string script) where T : class
        {
            return FieldAsScript<T>(expr, null, script);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField FieldAsScript<T>(
            Expression<Func<T, object>> expr,
            IDbTable table,
            string script) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);
            field.AsScript(script);
            return field;
        }

        // As query -----

        /// <summary>
        /// Updates the specified field as query.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static IDbField AsQuery(
            this IDbField field,
            IDbQuery query)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                field.Query = query as DbQuery;
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery(
            string name,
            IDbQuery query)
        {
            return DbFluent.FieldAsQuery(name, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery(
            string name,
            IDbTable table,
            IDbQuery query)
        {
            var field = DbFluent.Field(name, table);
            field.AsQuery(query);
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery<T>(
            Expression<Func<T, object>> expr,
            IDbQuery query) where T : class
        {
            return FieldAsQuery<T>(expr, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery<T>(
            Expression<Func<T, object>> expr,
            IDbTable table,
            IDbQuery query) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);
            field.AsQuery(query);
            return field;
        }

        // As other -----

        /// <summary>
        /// Updates the specified field as other.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static IDbField AsOther(
            this IDbField field,
            IDbField otherField)
        {
            if (field != null)
            {
                field.Expression = (otherField.ToScript()).AsExpression(BdoExpressionKind.Script);
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther(
            string name,
            IDbField otherField)
        {
            return DbFluent.FieldAsOther(name, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther(
            string name,
            IDbTable table,
            IDbField otherField)
        {
            var field = DbFluent.Field(name, table);
            field.AsOther(otherField);
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther<T>(
            Expression<Func<T, object>> expr,
            IDbField otherField) where T : class
        {
            return FieldAsOther<T>(expr, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther<T>(
            Expression<Func<T, object>> expr,
            IDbTable table,
            IDbField otherField) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);
            field.AsOther(otherField);
            return field;
        }

        // As All ---------------------------------

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="table">The data table to consider.</param>
        public static DbField FieldAsAll(IDbTable table)
        {
            var field = DbFluent.Field(null, table);
            field.AsAll();
            return field;
        }

        // As parameter with name -----------------

        /// <summary>
        /// Updates the specified field as parameter.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static IDbField AsParameter(
            this IDbField field,
            string parameterName)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                field.Expression = BdoElements.NewScalar(parameterName).AsExp();
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string parameterName)
        {
            return DbFluent.FieldAsParameter(name, null, parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            IDbTable table,
            string parameterName)
        {
            var field = Field(name, table);
            field.AsParameter(parameterName);
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            string parameterName) where T : class
        {
            return FieldAsParameter<T>(expr, null, parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            IDbTable table,
            string parameterName) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);
            field.AsParameter(parameterName);
            return field;
        }

        // As parameter with index -----

        /// <summary>
        /// Updates the specified field as parameter.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static IDbField AsParameter(
            this IDbField field,
            byte parameterIndex)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                field.Expression = (new ScalarElement() { Index = parameterIndex }).AsExp();
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            byte parameterIndex)
        {
            return DbFluent.FieldAsParameter(name, null, parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            IDbTable table,
            byte parameterIndex)
        {
            var field = Field(name, table);
            field.AsParameter(parameterIndex);
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            byte parameterIndex) where T : class
        {
            return FieldAsParameter<T>(expr, null, parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            IDbTable table,
            byte parameterIndex) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);
            field.AsParameter(parameterIndex);
            return field;
        }

        // As parameter with parameter -----

        /// <summary>
        /// Updates the specified field as parameter.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static IDbField AsParameter(
            this IDbField field,
            IScalarElement parameter)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                field.Expression = parameter.AsExp();
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            IScalarElement parameter)
        {
            return DbFluent.FieldAsParameter(name, null, parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            IDbTable table,
            IScalarElement parameter)
        {
            var field = Field(name, table);
            field.AsParameter(parameter);
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            IScalarElement parameter) where T : class
        {
            return FieldAsParameter<T>(expr, null, parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            IDbTable table,
            IScalarElement parameter) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);
            field.AsParameter(parameter);
            return field;
        }

        // As Null -----

        /// <summary>
        /// Updates the specified field as null.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        public static IDbField AsNull(this IDbField field)
        {
            return field?.AsScript(DbFluent.Null().ToString());
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public static DbField FieldAsNull(string name)
        {
            return FieldAsNull(name, null);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        public static DbField FieldAsNull(
            string name, IDbTable table)
        {
            var field = DbFluent.Field(name, table);
            field.AsNull();
            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        public static DbField FieldAsNull<T>(
            Expression<Func<T, object>> expr) where T : class
        {
            return FieldAsNull<T>(expr, null);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        public static DbField FieldAsNull<T>(
            Expression<Func<T, object>> expr,
            IDbTable table) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);
            field.AsNull();
            return field;
        }
    }
}