namespace GL.Servers.SP.Packets.Enums
{
    internal enum AttackHomeFailedReason
    {
        Generic,

        TargetOnline,
        AlreadyUnderAttack,
        SameAlliance,
        Shield,
        LevelDifference,
        NewbieProtected,
        NoMatches,
        NotEnoughResource,

        CooldownAfterMaintenance = 10,
        AttackDisabled,
        PersonalBreakAttackDisabled,

        TargetHasGuard = 16
    }
}