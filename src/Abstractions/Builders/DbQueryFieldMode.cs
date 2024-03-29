﻿using System;
using System.Xml.Serialization;

namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This enumerates the possible modes of database query field.
    /// </summary>
    [Serializable()]
    [XmlType("DbQueryFieldMode", Namespace = "https://docs.bindopen.org/xsd")]
    public enum DbQueryFieldMode
    {
        /// <summary>
        /// Only name.
        /// </summary>
        OnlyName,

        /// <summary>
        /// Complete name.
        /// </summary>
        CompleteName,

        /// <summary>
        /// Complete name or value.
        /// </summary>
        CompleteNameOrValue,

        /// <summary>
        /// Name equals value.
        /// </summary>
        NameEqualsValue,

        /// <summary>
        /// Name equals value in condition.
        /// </summary>
        NameEqualsValueInCondition,

        /// <summary>
        /// Only value.
        /// </summary>
        OnlyValue,

        /// <summary>
        /// Complete name as alias.
        /// </summary>
        CompleteNameOrValueAsAlias,

        /// <summary>
        /// Only name as alias.
        /// </summary>
        OnlyNameAsAlias
    }
}
