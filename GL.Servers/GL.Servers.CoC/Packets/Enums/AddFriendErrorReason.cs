namespace GL.Servers.CoC.Packets.Enums
{
    internal enum AddFriendErrorReason
    {
        Generic,
        TooManyRequestsYou,
        TooManyRequestsOther,

        OwnAvatar = 4,
        DoesNotExist,

        TooManyFriendsYou = 7,
        TooManyFriendsOther
    }
}