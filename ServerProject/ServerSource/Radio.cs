using Barotrauma;
using Barotrauma.Items.Components;
using Barotrauma.Networking;
using System.Text.Json;

namespace BarotraumaRadio
{
    public partial class Radio : ItemComponent
    {
        public string ServerUrl
        {
            get => currentStationUrl;
            set
            {
                currentStationUrl = value;
                UpdateServerLastPlayed();
                SendStateToClients();
            }
        }

        private void UpdateServerLastPlayed()
        {
            ServerRadioConfig config = new(currentStationUrl);
            string serializedConfig = JsonSerializer.Serialize(config);
            File.WriteAllText(serverConfigPath, serializedConfig);
        }

        public bool RadioEnabled
        {
            get => radioEnabled;
            set
            {
                radioEnabled = value;
                SendStateToClients();
            }
        }

        public void SendStateToClients()
        {
            IWriteMessage message = GameMain.LuaCs.Networking.Start("ChangeStateFromServer");

            if (string.IsNullOrEmpty(ServerUrl))
            {
                return;
            }

            INetSerializableStruct dataStruct = new RadioDataStruct(item.ID, ServerUrl, RadioEnabled);

            dataStruct.Write(message);
            GameMain.LuaCs.Networking.Send(message);
        }
    }
}
