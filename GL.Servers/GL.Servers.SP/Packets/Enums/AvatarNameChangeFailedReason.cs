namespace GL.Servers.SP.Packets.Enums
{
    internal enum AvatarNameChangeFailedReason
    {
        InvalidName,

        ThrottleError = 2,
        ChangeNameAgain
    }
}