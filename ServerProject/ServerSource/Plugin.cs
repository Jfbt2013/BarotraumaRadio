using Barotrauma;
using Barotrauma.Items.Components;
using Barotrauma.Networking;
using System;

namespace BarotraumaRadio
{
    public partial class Plugin : IAssemblyPlugin
    {
        public void InitServer()
        {
            SetServerSyncCallbacks();
        }

        public void SetServerSyncCallbacks()
        {
            GameMain.LuaCs.Networking.Receive("ChangeStationFromClient", (object[] args) =>
            {
                ExecuteCallBack(args, (Radio radioComponent, RadioDataStruct dataStruct) =>
                    {
                        radioComponent.ServerUrl = dataStruct.StringParamValue ?? radioComponent.ServerUrl;
                    }
                );
            });

            GameMain.LuaCs.Networking.Receive("RequestStationFromClient", (object[] args) =>
            {
                ExecuteCallBack(args, (Radio radioComponent, RadioDataStruct dataStruct) =>
                    {
                        radioComponent.SendStateToClients();
                    }
                );
            });

            GameMain.LuaCs.Networking.Receive("ChangeStateFromClient", (object[] args) =>
            {
                ExecuteCallBack(args, (Radio radioComponent, RadioDataStruct dataStruct) =>
                    {
                        radioComponent.RadioEnabled = dataStruct.BooleanParamValue ?? radioComponent.RadioEnabled;
                    }
                );
            });
        }
    }
}
