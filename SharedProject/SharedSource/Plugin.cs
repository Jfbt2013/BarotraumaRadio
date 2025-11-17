using Barotrauma;
using Barotrauma.Items.Components;
using Barotrauma.Networking;
using HarmonyLib;
using System.Runtime.CompilerServices;

[assembly: IgnoresAccessChecksTo("Barotrauma")]
[assembly: IgnoresAccessChecksTo("DedicatedServer")]
[assembly: IgnoresAccessChecksTo("BarotraumaCore")]

namespace BarotraumaRadio
{
    public partial class Plugin : IAssemblyPlugin
    {
        public static Plugin? Instance;

        public void Initialize()
        {
            Instance = new Plugin();
#if CLIENT
            LuaCsSetup.PrintCsMessage("init client started");
            InitClient();
#elif SERVER
            LuaCsSetup.PrintCsMessage("init server started");
            InitServer();
#endif
        }

        public void OnLoadCompleted()
        {
            // After all plugins have loaded
            // Put code that interacts with other plugins here.
        }

        public void PreInitPatching()
        {
            // Not yet supported: Called during the Barotrauma startup phase before vanilla content is loaded.
        }

        public void Dispose()
        {
        }

        public void ExecuteCallBack(object[] args, Action<Radio, RadioDataStruct> callback)
        {
            IReadMessage message = (IReadMessage)args[0];
            RadioDataStruct dataStruct = INetSerializableStruct.Read<RadioDataStruct>(message);
            Item? item = Item.ItemList.FirstOrDefault(serverItem => serverItem.ID == dataStruct.RadioID);

            if (item == null)
                return;

            ItemComponent? component = item.Components.FirstOrDefault(c => c is Radio);

            if (component != null && component is Radio radioComponent)
            {
                callback(radioComponent, dataStruct);
            }
        }
    }
}
