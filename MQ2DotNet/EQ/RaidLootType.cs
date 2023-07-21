namespace MQ2DotNet.EQ
{
    /// <summary>
    /// 1 = Leader, 2 = Leader and GroupLeader, 3 = Leader and Specified
    /// </summary>
    public enum RaidLootType : uint
    {
        Leader = 1U,
        LeaderAndGroupLeader = 2U,
        LeaderAndSpecified = 3U
    }
}
