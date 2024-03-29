﻿using Newtonsoft.Json;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a dated tracked DTO.
    /// </summary>
    public class DatedTrackedDtoFake : DatedDtoFake, IDatedTrackedDtoFake
    {
        #region Properties

        /// <summary>
        /// The one who has created this instance.
        /// </summary>
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// The one who has lastly modified this instance.
        /// </summary>
        [JsonProperty("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        #endregion
    }
}