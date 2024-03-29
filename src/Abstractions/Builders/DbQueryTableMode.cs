﻿using System;
using System.Xml.Serialization;

namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This enumerates the possible modes of database query table.
    /// </summary>
    [Serializable()]
    [XmlType("DbQueryTableMode", Namespace = "https://docs.bindopen.org/xsd")]
    public enum DbQueryTableMode
    {
        /// <summary>
        /// Complete name.
        /// </summary>
        CompleteName,

        /// <summary>
        /// Name as alias.
        /// </summary>
        CompleteNameAsAlias,

        /// <summary>
        /// Alias as.
        /// </summary>
        AliasAsCompleteName
    }
}
