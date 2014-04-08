#region Using Directives
using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
#endregion

namespace EsccWebTeam.Gdsc
{
	/// <summary>
	/// Collection of parts of a name where the same part of a name occurs more than once, eg multiple given names
	/// </summary>
	/// <example>Joe Fred Harry Smith - "Joe", "Fred" and "Harry" are all given names and would be in this collection</example>
	/// <example>Brigadier General Sir Fred Smith - "Brigadier General" and "Sir" are both titles and would be in this collection</example>
	/// <example>Fred Smith MBE, MRCVS - "MBE" and "MRCVS" are both suffixes and would be in this collection</example>
    [Serializable]
    public class PersonPartialNameCollection : List<string>
	{
		private int maximumNameLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonPartialNameCollection"/> class.
        /// </summary>
        public PersonPartialNameCollection()
        {
            this.maximumNameLength = 35;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonPartialNameCollection"/> class.
        /// </summary>
        /// <param name="maximumNameLength">Maximum length of the name.</param>
        public PersonPartialNameCollection(int maximumNameLength)
        {
            this.maximumNameLength = maximumNameLength;
        }

        /// <summary>
        /// Add a part of a name to the collection. Truncates name if longer than the Address and Personal Details standard allows.
        /// </summary>
        /// <param name="item">The <see cref="string" /> to add</param>
        /// <returns>The index at which the part of a name was added</returns>
        new public void Add(string item)
        {
            if (item == null) return;

            string namePart = item;
            namePart = namePart.Trim();
            if (namePart.Length == 0) return;
            if (namePart.Length > this.maximumNameLength) namePart = namePart.Substring(0, this.maximumNameLength);

            base.Add(namePart);
        }

        /// <summary>
        /// Insert a part of a name into the collection at the specified index. Truncates name if longer than the Address and Personal Details standard allows.
        /// </summary>
        /// <param name="index">The index at which to insert the part of a name</param>
        /// <param name="item">The <see cref="string" /> to insert</param>
        new public void Insert(int index, string item)
        {
            string namePart = item;
            if (namePart != null) namePart = namePart.Trim();
            if (namePart != null && namePart.Length > this.maximumNameLength) namePart = namePart.Substring(0, this.maximumNameLength);

            base.Insert(index, namePart);
        }

        /// <summary>
        /// Gets a joined string of the parts of the name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(this[i]).Append(" ");
            }
            return sb.ToString().Trim();
        }

	}
}
