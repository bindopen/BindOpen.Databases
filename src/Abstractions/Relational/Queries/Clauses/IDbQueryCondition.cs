﻿using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryCondition : IDbObject
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The field 1 of this instance.
        /// </summary>
        IDbField Field1 { get; set; }

        /// <summary>
        /// Sets the field 1 of this instance.
        /// </summary>
        IDbQueryCondition WithField1(IDbField field);

        /// <summary>
        /// The field 2 of this instance.
        /// </summary>
        IDbField Field2 { get; set; }

        /// <summary>
        /// Sets the field 2 of this instance.
        /// </summary>
        IDbQueryCondition WithField2(IDbField field);

        /// <summary>
        /// The operator of this instance.
        /// </summary>
        DataOperators Operator { get; set; }

        /// <summary>
        /// Sets the operator of this instance.
        /// </summary>
        IDbQueryCondition WithOperator(DataOperators op);

        /// <summary>
        /// The set of parameters of this instance.
        /// </summary>
        IBdoMetaSet ParameterSet { get; set; }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery WithParameters(params IBdoMetaData[] parameters);

        /// <summary>
        /// Add the specified parameter to this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery AddParameters(params IBdoMetaScalar[] parameters);

        #endregion
    }
}