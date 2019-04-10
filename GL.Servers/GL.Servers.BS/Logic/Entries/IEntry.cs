namespace GL.Servers.BS.Logic.Entries
{
    using System;

    using GL.Servers.BS.Logic.Enums;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptIn)]
    internal interface IEntry
    {
        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        long MessageID
        {
            get;
        }

        /// <summary>
        /// Gets the player identifier.
        /// </summary>
        long PlayerID
        {
            get;
        }

        [JsonProperty("high_id")]           int Message_HighID  { get; set; }
        [JsonProperty("low_id")]            int Message_LowID   { get; set; }

        [JsonProperty("sender_high_id")]    int Sender_HighID   { get; set; }
        [JsonProperty("sender_low_id")]     int Sender_LowID    { get; set; }

        [JsonProperty("sender_name")]       string Sender_Name  { get; set; }

        [JsonProperty("date")]              DateTime Sent       { get; set; }
        [JsonProperty("sender_role")]       Role Sender_Role    { get; set; }

        Stream_Type Type { get; }

        /// <summary>
        /// Gets or sets to bytes.
        /// </summary>
        byte[] ToBytes
        {
            get;
        }
    }
}