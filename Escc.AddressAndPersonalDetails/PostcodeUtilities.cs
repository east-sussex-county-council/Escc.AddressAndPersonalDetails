
using System.Text.RegularExpressions;

namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// Class for working with UK postcodes
    /// </summary>
    public static class PostcodeUtilities
    {
        /// <summary>
        /// Adds a space to the expected place in the middle of a postcode which has been entered without spaces
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public static string Format(string postcode)
        {
            if (postcode != null)
            {
                postcode = Regex.Replace(postcode.ToUpper(), "\\W", ""); ;

                if (postcode.Length == 6)
                {
                    postcode = postcode.Insert(3, " ");
                }
                else if (postcode.Length == 7)
                {
                    postcode = postcode.Insert(4, " ");
                }
            }

            return postcode;
        }
    }
}
