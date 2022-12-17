﻿using BindOpen.Framework.MetaData.Expression;
using BindOpen.Framework.MetaData.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data
{

    /// <summary>
    /// This class represents the GroupBy clause of a database data query.
    /// </summary>
    public class DbQueryGroupByClause : DataItem, IDbQueryGroupByClause
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryGroupByClause class.
        /// </summary>
        public DbQueryGroupByClause()
        {
        }

        #endregion

        // ------------------------------------------
        // IDbItem Implementation
        // ------------------------------------------

        #region IDbItem

        /// <summary>
        /// 
        /// </summary>
        public IDataExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IDbQueryGroupByClause WithExpression(IDataExpression expression)
        {
            Expression = expression;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQueryGroupByClause Implementation
        // ------------------------------------------

        #region IDbQueryGroupByClause

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<IDbField> Fields { get; set; }

        public IDbQueryGroupByClause WithFields(params IDbField[] fields)
        {
            Fields = fields.ToList();
            return this;
        }

        public IDbQueryGroupByClause AddFields(params IDbField[] fields)
        {
            Fields ??= new List<IDbField>();
            Fields.AddRange(fields);
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDataItem Implementation
        // ------------------------------------------

        #region IDataItem

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone<IDbQueryGroupByClause>(areas);
            clone.Fields = Fields?.Select(p => p.Clone<IDbField>()).ToList();

            return clone;
        }

        #endregion
    }
}