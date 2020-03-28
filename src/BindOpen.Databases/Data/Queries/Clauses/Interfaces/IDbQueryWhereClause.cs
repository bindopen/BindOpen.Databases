﻿using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryWhereClause : IDbQueryClause
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbField> IdFields { get; set; }
    }
}