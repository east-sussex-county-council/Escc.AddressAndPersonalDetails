using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// Transforms an email address from its standard format
    /// </summary>
    public interface IEmailAddressTransformer
    {
        /// <summary>
        /// Gets the transformed email address
        /// </summary>
        /// <returns></returns>
        string TransformEmailAddress(ContactEmail email);
    }
}
