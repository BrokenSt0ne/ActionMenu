using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using Utilla;

namespace ActionMenu.Menu
{
    public class MenuHandler : MonoBehaviour
    {
        /*public void Awake()
        {
            Debug.Log("!!!!! trying to load ongameinit !!!!!");
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            Debug.Log("!!!!! it loaded ongameinit !!!!!");
            var ActionMenuAssets = LoadAssetBundle("ActionMenu.Resources.actionmenu");
            var ActionMenu = UnityEngine.Object.Instantiate(ActionMenuAssets.LoadAsset<GameObject>("ActionMenu_Parent"));
            UnityEngine.Object.Instantiate(ActionMenuAssets.LoadAsset<GameObject>("ActionMenu_Parent"));
            ActionMenu.transform.position = Vector3.zero;
            ActionMenu.transform.rotation = Quaternion.identity;
            ActionMenu.transform.localScale = Vector3.one;
        }*/
    }
}
