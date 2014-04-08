
namespace EsccWebTeam.Gdsc
{
    /// <summary>
    /// Methods of validating address information
    /// </summary>
    /// <remarks>Numbers match database ids in AddressValidationType lookup table</remarks>
    public enum AddressValidationType
    {
        /// <summary>
        /// Current state of the address is not known
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Address has not been checked against PAF or NLPG, e-GIF requires this option
        /// </summary>
        NotChecked = 1,
        /// <summary>
        /// PAF Address has been edited by user
        /// </summary>
        PafCheckFailed = 2,
        /// <summary>
        /// Nlpg Address has been edited by user
        /// </summary>
        NlpgCheckFailed = 3,
        /// <summary>
        /// Valid PAF address
        /// </summary>
        PafCheckValid = 4,
        /// <summary>
        /// Valid Nlpg address
        /// </summary>
        NlpgCheckValid = 5
    }
}
