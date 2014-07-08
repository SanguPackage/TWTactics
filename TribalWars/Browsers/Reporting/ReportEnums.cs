using System;

namespace TribalWars.Browsers.Reporting
{
    /// <summary>
    /// The different status report units can be in
    /// </summary>
    public enum TotalPeople
    {
        /// <summary>
        /// Units home at the beginning of the report
        /// </summary>
        Start,
        /// <summary>
        /// Units lost in battle
        /// </summary>
        Lost,
        /// <summary>
        /// Units left over home
        /// </summary>
        End,
        /// <summary>
        /// Units outside of the village
        /// </summary>
        Out,
        /// <summary>
        /// Total amount of units left
        /// </summary>
        EndPlusOut
    }

    /// <summary>
    /// The different types of reports
    /// </summary>
    public enum ReportTypes
    {
        /// <summary>
        /// A regular clearing attack
        /// </summary>
        Attack,
        /// <summary>
        /// Any attack with a noble
        /// </summary>
        Noble,
        /// <summary>
        /// An attack with few troops and/or scouts
        /// </summary>
        Scout,
        /// <summary>
        /// An attack with few troops
        /// </summary>
        Fake,
        /// <summary>
        /// An attack without nobles in a defenseless village
        /// </summary>
        Farm,
        /// <summary>
        /// The report was forwarded by someone else
        /// </summary>
        Forwarded
    }

    /// <summary>
    /// The possible outcomes of the attack
    /// </summary>
    public enum ReportStatusses
    {
        Success,
        Failure,
        HalfSuccess
    }

    /// <summary>
    /// The side of the report
    /// </summary>
    public enum ReportSide
    {
        None = 0,
        Attacker,
        Defender
    }

    /// <summary>
    /// The possible secundary effects of the attack
    /// </summary>
    [Flags]
    public enum ReportFlags
    {
        /// <summary>
        /// The scouts saw the amount of troops outside the village
        /// </summary>
        SeenOutside = 1,
        /// <summary>
        /// The defender troops amounts were visible
        /// </summary>
        SeenDefense = 4,
        /// <summary>
        /// All defenses were cleared in the village
        /// </summary>
        Clear = 2,
        /// <summary>
        /// Change of village owner
        /// </summary>
        Nobled = 8
    }
}
