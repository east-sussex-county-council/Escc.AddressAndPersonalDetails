#region Using Directives
using System;
using System.Collections.Generic;
#endregion

namespace EsccWebTeam.Gdsc
{
    /// <summary>
    /// A collection of email addresses
    /// </summary>
    [Serializable]
    public class ContactEmailCollection : List<ContactEmail>
    {
        /// <summary>
        /// Add a email address to the collection
        /// </summary>
        /// <param name="emailAddress">The email address to add</param>
        /// <returns>The index at which the email address was added</returns>
        public void Add(string emailAddress)
        {
            if (emailAddress == null || emailAddress.Trim().Length == 0) return;

            ContactEmail item = new ContactEmail(emailAddress);
            this.Add(item);
        }
    }
}
