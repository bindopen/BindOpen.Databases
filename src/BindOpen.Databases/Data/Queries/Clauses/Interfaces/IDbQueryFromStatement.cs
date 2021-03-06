﻿using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromStatement : IDbQueryStatement
    {
        /// <summary>
        /// The tables of this instance.
        /// </summary>
        List<DbTable> Tables { get; set; }
    }
}