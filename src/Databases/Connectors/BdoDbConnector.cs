﻿using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping.Connectors;
using BindOpen.Plus.Databases.Builders;
using BindOpen.Plus.Databases.Data;
using System.Data;

namespace BindOpen.Plus.Databases.Connectors
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public abstract class BdoDbConnector :
        BdoConnector, IBdoDbConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        protected BdoDbConnector() : this(BdoDbConnectorKind.Any)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        /// <param name="kind">The database kind of this instance.</param>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        protected BdoDbConnector(BdoDbConnectorKind kind) : base()
        {
            DatabaseConnectorKind = kind;
        }

        #endregion

        // -----------------------------------------------
        // IBdoDbConnector Implementation
        // -----------------------------------------------

        #region IBdoDbConnector

        /// <summary>
        /// The query builder of this instance.
        /// </summary>
        protected IDbQueryBuilder _queryBuilder;

        /// <summary>
        /// The provider of this instance.
        /// </summary>
        [BdoProperty(Name = "provider")]
        public string Provider { get; set; }

        /// <summary>
        /// The server address of this instance.
        /// </summary>
        [BdoProperty(Name = "serverAddress")]
        public string ServerAddress { get; set; }

        /// <summary>
        /// The initial catalog of this instance.
        /// </summary>
        [BdoProperty(Name = "initialCatalog")]
        public string InitialCatalog { get; set; }

        /// <summary>
        /// The integrated security of this instance.
        /// </summary>
        [BdoProperty(Name = "integratedSecurity")]
        public string IntegratedSecurity { get; set; }

        /// <summary>
        /// The user name of this instance.
        /// </summary>
        [BdoProperty(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// The password of this instance.
        /// </summary>
        [BdoProperty(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// The database kind of this instance.
        /// </summary>
        [BdoProperty(Name = "kind")]
        public BdoDbConnectorKind DatabaseConnectorKind { get; set; }

        // SQL commands

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public string CreateCommandText(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varElementSet = null,
            IBdoLog log = null)
        {
            if (_queryBuilder == null)
            {
                log?.AddEvent(EventKinds.Error, "Data builder missing");
                return null;
            }

            string sqlText = _queryBuilder.BuildQuery(query, parameterMode, parameterSet, varElementSet, log);
            return sqlText;
        }

        /// <summary>
        /// Creates a command from the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the database command.</returns>
        public abstract IDbCommand CreateCommand(
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// Estimates the database connector kind of this instance.
        /// </summary>
        /// <returns>The database connector kind of this instance.</returns>
        public BdoDbConnectorKind EstimateDbConnectorKind()
        {
            return ConnectionString.GuessDbConnectorKind();
        }

        #endregion
    }
}