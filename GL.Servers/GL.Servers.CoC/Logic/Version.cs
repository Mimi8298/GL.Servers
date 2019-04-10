namespace GL.Servers.CoC.Logic
{
    internal class Version
    {
        internal const int ClientMajorVersion = 9;
        internal const int ClientBuildVersion = 256;

        internal const int ServerMajorVersion = 9;
        internal const int ServerBuildVersion = 256;

        internal const bool IsProd = true;
        internal const bool IsDev  = false;
        internal const bool IsIntegration = false;
        internal const bool IsStage = false;

        internal const string ServerEnvironment = "content-stage";
    }
}