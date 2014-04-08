#region Using Directives
using System;
using System.Collections.Generic;
#endregion

namespace EsccWebTeam.Gdsc
{
    /// <summary>
    /// A collection of addresses in a format compliant with National Land and Property Gazetteer and e-GIF
    /// </summary>
    [Serializable]
    public class BS7666AddressCollection : List<BS7666Address>
    {
    }
}
