﻿using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Stores;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder : BdoObject, IDbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder class.
        /// </summary>
        protected DbQueryBuilder()
        {
        }

        #endregion

        // ------------------------------------------
        // IDbQueryBuilder Implementation
        // ------------------------------------------

        #region IDbQueryBuilder

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        #endregion

        // ------------------------------------------
        // ITIdentifiedPoco Implementation
        // ------------------------------------------

        #region ITIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // IDbQueryBuilder Implementation
        // ------------------------------------------

        #region IDbQueryBuilder

        /// <summary>
        /// Gets the database name corresponding to the specified data module name.
        /// </summary>
        /// <param name="dataModuleName">The data module name to consider.</param>
        /// <remarks>If not found, it returns the specified data module name.</remarks>
        protected string GetDatabaseName(string dataModuleName)
        {
            var dataSourceDepot = Scope?.GetDatasourceDepot();
            if (dataSourceDepot == null)
                return dataModuleName;
            else
            {
                var databaseName = dataModuleName;
                return databaseName;
            }
        }

        /// <summary>
        /// Updates the specified parameter set with the specified query.
        /// </summary>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="query">The query to consider.</param>
        protected static void UpdateParameterSet(IBdoMetaSet parameterSet, IDbQuery query)
        {
            //parameterSet?.Update(query?.ParameterSpecs);
            parameterSet?.Update(query?.ParameterSet);
        }

        /// <summary>
        /// Builds the SQL text from the specified database query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="parameterMode">The display mode of parameters to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        public string BuildQuery(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            if (query != null)
            {
                try
                {
                    if (query is IDbSingleQuery singleDbQuery)
                    {
                        varSet.AddDbBuilder(this);
                        queryString = GetSqlText_Query(singleDbQuery, parameterSet, varSet, log);
                    }
                    else if (query is IDbCompositeQuery compositeDbQuery)
                    {
                        varSet.AddDbBuilder(this);
                        queryString = GetSqlText_Query(compositeDbQuery, parameterSet, varSet, log);
                    }
                    else if (query is IDbStoredQuery storedDbQuery)
                    {
                        if (!storedDbQuery.QueryTexts.TryGetValue(Id, out queryString))
                        {
                            queryString = BuildQuery(storedDbQuery.Query, DbQueryParameterMode.Scripted, parameterSet, varSet, log);
                            storedDbQuery.QueryTexts.Add(Id, queryString);
                        }
                    }

                    if (query.SubQueries != null)
                    {
                        for (int i = 0; i < query.SubQueries.Count; i++)
                        {
                            var subQuery = query.SubQueries[i];
                            var subQueryString = BuildQuery(subQuery, parameterMode, parameterSet, varSet, log);

                            queryString = queryString.Replace((i + 1).ToString().AsQueryWildString(), subQueryString);
                        }
                    }

                    if (parameterMode != DbQueryParameterMode.Scripted)
                    {
                        parameterSet ??= BdoData.NewSet();
                        UpdateParameterSet(parameterSet, query);

                        if (query is IDbStoredQuery storedDbQuery)
                        {
                            UpdateParameterSet(parameterSet, storedDbQuery.Query);
                        }

                        if (parameterSet?.Items != null)
                        {
                            foreach (var parameter in parameterSet.Items)
                            {
                                if (parameterMode == DbQueryParameterMode.ValueInjected)
                                {
                                    queryString = queryString.Replace((parameter?.Name ?? parameter.Index.ToString())?.AsParameterWildString(),
                                        GetSqlText_Value(parameter?.GetData(Scope, varSet, log), parameter.DataType?.ValueType ?? DataValueTypes.None));
                                }
                                else
                                {
                                    queryString = queryString.Replace((parameter?.Name ?? parameter.Index.ToString())?.AsParameterWildString(),
                                        "@" + parameter?.Name ?? parameter.Index.ToString());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log?.AddEvent(
                        EventKinds.Error,
                        "Error trying to build query '" + (query?.Name ?? "(Undefinied)") + "'",
                        description: ex.ToString() + ". Built query is : '" + queryString + "'.");
                }
            }

            return queryString;
        }

        // Builds single query ----------------------

        /// <summary>
        /// Builds the SQL text of the specified basic query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected abstract string GetSqlText_Query(
            IDbSingleQuery query,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        // Builds merge query ----------------------

        /// <summary>
        /// Builds the SQL text of the specified merge query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected abstract string GetSqlText_Query(
            IDbCompositeQuery query,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        #endregion

        // --------------------------------------------------
        // IDisposable IMPLEMENTATION
        // --------------------------------------------------

        #region IDisposable Implementation

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                Scope?.Dispose();
            }
        }

        #endregion
    }
}