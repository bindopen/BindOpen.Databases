﻿using BindOpen.Extensions.Carriers;
using System;
using System.Linq.Expressions;

namespace BindOpen.Databases.Data.Models
{
    public partial interface IBdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        DbField Field(string name, string tableName, string tableAlias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        DbField Field<T>(Expression<Func<T, object>> expression, string tableAlias = null);
    }
}