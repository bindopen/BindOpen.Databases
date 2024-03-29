﻿using BindOpen.Scoping.Script;
using System;

namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql
    {
        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Eq(string value1, string value2)
        {
            if ((value1 == null || string.Equals(value1, GetSqlText_Null(), StringComparison.OrdinalIgnoreCase))
                && value2 != null)
            {
                return value2 + " is null";
            }
            else if ((value2 == null || string.Equals(value2, GetSqlText_Null(), StringComparison.OrdinalIgnoreCase))
                && value1 != null)
            {
                return value1 + " is null";
            }

            return value1 + "=" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Diff(string value1, string value2)
        {
            return value1 + "<>" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Gt(string value1, string value2)
        {
            return value1 + ">" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Gte(string value1, string value2)
        {
            return value1 + ">=" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Lt(string value1, string value2)
        {
            return value1 + "<" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Lte(string value1, string value2)
        {
            return value1 + "<=" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_IsNull(string value1)
        {
            return value1 + " is null";
        }

        /// <summary>
        /// Evaluates the script word $SQLIN.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_In(params object[] parameters)
        {
            string text = "[";
            foreach (object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += "'" + st.GetValueFromScript() + "'" + (text == "[" ? ", " : "");
                }
            }

            text += "]";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLEXISTS.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        /// <returns>The existsterpreted strexistsg value.</returns>
        public override string GetSqlText_Exists(string value)
        {
            return "exists(" + value + ")";
        }
    }
}