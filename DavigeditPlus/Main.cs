using Davigo.Davigedit;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DavigeditPlus
{
    public class Main : MelonMod
    {
        private bool firstLoad = true;
        private string quickLoadPath;

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("yea im workin");
        }

        // in late update so davigo can initialise required components. not optimal, but easy
        public override void OnLateUpdate()
        {
            if (SceneManager.GetActiveScene().name == AppConstants.Instance.MenuScene && firstLoad)
            {
                quickLoadPath = Path.Combine(Environment.CurrentDirectory, "QuickLoad.txt");
                firstLoad = false;
                if (File.Exists(quickLoadPath))
                {
                    string quickLoadContents = File.ReadAllText(quickLoadPath);
                    if (!File.Exists(quickLoadContents))
                    {
                        MelonLogger.Warning($"{Path.GetFileName(quickLoadContents)} does not exist!");
                        File.Delete(quickLoadPath);
                        return;
                    }
                    GameObject.FindObjectOfType<MapMenu>().Deinitialize();
                    File.Delete(quickLoadPath);

                    CustomMap customMap = new CustomMap(quickLoadContents);
                    customMap.LoadAssetBytes();
                    MapIdentifier mapID = customMap.MapIdentifier;
                    customMap.Dispose();

                    List<DeviceList> warriorInputDevices = null;

                    MelonLogger.Msg("aight");
                    MatchLoader.Instance.LoadMap(mapID, new GameMode(), new GameOptions(), 1, true, false, warriorInputDevices);
                }
            }
        }
    }
}
