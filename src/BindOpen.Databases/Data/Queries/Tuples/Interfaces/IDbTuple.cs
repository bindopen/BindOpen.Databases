﻿using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbTuple
    {
        /// <summary>
        /// The fields of this instance.
        /// </summary>
        List<DbField> Fields { get; set; }

        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        IDbTuple AddFields(params DbField[] fields);
    }
}