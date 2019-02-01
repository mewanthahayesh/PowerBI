// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.PowerBI.Api.V1.Models
{
    using Microsoft.PowerBI;
    using Microsoft.PowerBI.Api;
    using Microsoft.PowerBI.Api.V1;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Object representing basic authentication credentials
    /// </summary>
    public partial class BasicCredentials
    {
        /// <summary>
        /// Initializes a new instance of the BasicCredentials class.
        /// </summary>
        public BasicCredentials()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the BasicCredentials class.
        /// </summary>
        /// <param name="username">Username required to access the
        /// datasource</param>
        /// <param name="password">Password required to access the
        /// datasource</param>
        public BasicCredentials(string username = default(string), string password = default(string))
        {
            Username = username;
            Password = password;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets username required to access the datasource
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password required to access the datasource
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

    }
}
