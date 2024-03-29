﻿using BindOpen.Data;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryOrderByStatement :
        IDbObject,
        IDbQueryStatement
    {
        /// <summary>
        /// 
        /// </summary>
        IDbField Field { get; set; }

        /// <summary>
        /// Sets the field of this instance.
        /// </summary>
        IDbQueryOrderByStatement WithField(IDbField field);

        /// <summary>
        /// 
        /// </summary>
        DataSortingModes Sorting { get; set; }

        /// <summary>
        /// Sets the sorting mode of this instance.
        /// </summary>
        IDbQueryOrderByStatement WithSorting(DataSortingModes sorting);
    }
}