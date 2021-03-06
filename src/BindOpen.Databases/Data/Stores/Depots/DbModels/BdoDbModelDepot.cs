﻿using BindOpen.Databases.Data.Models;
using BindOpen.System.Assemblies;
using BindOpen.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a database model depot.
    /// </summary>
    [Serializable()]
    [XmlType("DbModelDepot", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "dbModel.depot", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoDbModelDepot : TBdoDepot<BdoDbModel>, IBdoDbModelDepot
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Queries of this instance.
        /// </summary>
        [XmlArray("queries")]
        [XmlArrayItem("add")]
        public List<BdoDbModel> Models
        {
            get { return Items; }
            set { Items = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbModelDepot class.
        /// </summary>
        public BdoDbModelDepot() : base()
        {
            Id = "dbModels";
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="item">The new item to add.</param>
        /// <param name="referenceCollection">The reference collection to consider.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        protected override void Add(BdoDbModel item)
        {
            if (item == null)
                return;

            if (string.IsNullOrEmpty(item.Key()))
            {
                item.Id = item.GetType().Name;
            }

            base.Add(item);
        }

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="log">The log to consider.</param>
        public override IBdoDepot AddFromAssembly(string assemblyName, IBdoLog log = null)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAsssembly(assemblyName);
            if (assembly != null)
            {
                var types = assembly.GetTypes().Where(p => p.IsClass && typeof(IBdoDbModel).IsAssignableFrom(p));
                foreach (Type type in types)
                {
                    var model = Activator.CreateInstance(type) as BdoDbModel;

                    Add(model);
                }
            }

            return this;
        }

        /// <summary>
        /// Gets the database model of the specified type and with the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public override BdoDbModel Get<BdoDbModel>(string key = null)
        {
            var item = Items?.FirstOrDefault(p => p is BdoDbModel);
            return item as BdoDbModel;
        }

        #endregion
    }
}
