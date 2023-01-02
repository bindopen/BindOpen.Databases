﻿using BindOpen.Databases.Data;
using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public abstract partial class BdoDbModel : BdoItem, IBdoDbModel, IBdoDbModelBuilder
    {
        #region Variables

        internal Dictionary<string, IDbTableModel> TableModelDictionary = new Dictionary<string, IDbTableModel>();
        internal Dictionary<string, IDbTableRelationship> TableRelationShipDictionary = new Dictionary<string, IDbTableRelationship>();
        internal Dictionary<string, IDbField[]> TupleDictionary = new Dictionary<string, IDbField[]>();
        internal Dictionary<string, IDbStoredQuery> QueryDictionary = new Dictionary<string, IDbStoredQuery>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected BdoDbModel()
        {
            OnCreating();
        }

        #endregion

        // ------------------------------------------
        // ITIdentifiedPoco Implementation
        // ------------------------------------------

        #region ITIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBdoDbModel WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion

        // -----------------------------------------------
        // IReferenced Implementation
        // -----------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public string Key() => Id;

        #endregion

        // -----------------------------------------------
        // IBdoDbModel Implementation
        // -----------------------------------------------

        #region IBdoDbModel

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnCreating()
        {
        }

        #endregion
    }
}
