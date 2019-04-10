namespace GL.Servers.CoC.Packets.Enums
{
    internal enum AvatarNameChangeFailedReason
    {
        InvalidName,

        ThrottleError = 2,
        ChangeNameAgain
    }
}