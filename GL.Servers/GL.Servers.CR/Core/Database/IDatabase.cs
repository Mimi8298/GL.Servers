namespace GL.Servers.CR.Core.Database
{
    using System.Threading.Tasks;

    internal interface IDatabase
    {
        int PlayerSeed
        {
            get;
        }

        int AllianceSeed
        {
            get;
        }

        void CreatePlayer(int HighID, int LowID, string JSON);
        void CreateAlliance(int HighID, int LowID, string JSON);

        string LoadPlayer(int HighID, int LowID);
        string LoadPlayer(int HighID, int LowID, string PassToken);
        Task<string> LoadPlayerAsync(int HighID, int LowID);
        Task<string> LoadPlayerAsync(int HighID, int LowID, string PassToken);

        string LoadAlliance(int HighID, int LowID);
        Task<string> LoadAllianceAsync(int HighID, int LowID);

        void SavePlayer(int HighID, int LowID, string JSON);
        void SaveAlliance(int HighID, int LowID, string JSON);
    }
}