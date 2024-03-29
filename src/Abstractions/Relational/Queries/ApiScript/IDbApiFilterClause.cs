﻿using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents a Api script clause.
    /// </summary>
    public interface IDbApiFilterClause : IDbApiClause
    {
        /// <summary>
        /// The operators of this instance.
        /// </summary>
        public List<DataOperators> Operators { get; set; }

        /// <summary>
        /// The filter definition of this instance.
        /// </summary>
        public IDbApiFilterDefinition FilterDefinition { get; set; }
    }
}
