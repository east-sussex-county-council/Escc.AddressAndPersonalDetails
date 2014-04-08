#region Using Directives
using System;
using System.Collections.Generic;
#endregion

namespace EsccWebTeam.Gdsc
{
    /// <summary>
    /// A collection of contact numbers
    /// </summary>
    [Serializable]
    public class UKContactNumberCollection : List<UKContactNumber>
    {
        /// <summary>
        /// Add a contact number to the collection
        /// </summary>
        /// <param name="nationalNumber">The national number to add</param>
        /// <returns>The index at which the contact number was added</returns>
        public void Add(string nationalNumber)
        {
            if (nationalNumber == null || nationalNumber.Trim().Length == 0) return;

            UKContactNumber item = new UKContactNumber(nationalNumber);
            this.Add(item);
        }
    }

}
