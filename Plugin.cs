using ActionMenu.Menu;
using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;
using Utilla;
using Valve.VR;

namespace ActionMenu
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    //[ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public bool MenuOpen;
        public bool MenuOpenControl;
        public static GameObject ActionMenu;
        public GameObject SelectorStick;

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            //Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
            Utilla.Events.GameInitialized -= OnGameInitialized;
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            if (stream == null)
            {
                Debug.LogError($"Failed to get the resource stream for path: {path}");
                return null;
            }

            AssetBundle bundle = AssetBundle.LoadFromStream(stream);

            if (bundle == null)
            {
                Debug.LogError($"Failed to load AssetBundle from stream for path: {path}");
            }
            stream.Close();
            return bundle;
        }

        public void LoadAssets()
        {
            AssetBundle ActionMenuAssets = LoadAssetBundle("ActionMenu.Resources.actionmenu");
            ActionMenu = ActionMenuAssets.LoadAsset<GameObject>("ActionMenu_Parent");
        }

        public void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
            LoadAssets();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            GameObject hand = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L");
            ActionMenu = Instantiate(ActionMenu);
            GameObject pain = GameObject.Find("ActionMenu_Parent(Clone)");
            //SelectorStick = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/ActionMenu_Parent(Clone)/Selector Background/Selector Stick");
            ActionMenu.SetActive(false);
            ActionMenu.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            ActionMenu.transform.SetParent(hand.transform);
            ActionMenu.transform.localPosition = new Vector3(-0.06f, 0.06f, 0f);
            ActionMenu.transform.localRotation = Quaternion.Euler(90, 270, 0);
            SelectorStick = ActionMenu.transform.Find("ActionMenu/Selector Background/Selector Stick").gameObject;
        }

        public void FixedUpdate()
        {
            #region BasicFuntions
            if (SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand))
            {
                if(!MenuOpenControl)
                {
                    if (ActionMenu.active)
                    {
                        ActionMenu.SetActive(false);
                        MenuOpen = false;
                    }
                    else
                    {
                        ActionMenu.SetActive(true);
                        MenuOpen = true;
                    }
                }
                MenuOpenControl = true;
            }
            else
            {
                MenuOpenControl = false;
            }

            if(MenuOpen)
            {
                SelectorStick.transform.localPosition = new Vector3(SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.axis.y / 2, SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.axis.x / 2, 0.01f);
            }
            #endregion

            float distanceTest = Vector3.Distance(SelectorStick.transform.position, ActionMenu.transform.Find("ActionMenu/Main/Actions/Config/Selected").transform.position);

            if (distanceTest <= 0.025)
            {
                if (!ActionMenu.transform.Find("ActionMenu/Main/Actions/Config/Selected").gameObject.active)
                {
                    ActionMenu.transform.Find("ActionMenu/Main/Actions/Config/Selected").gameObject.SetActive(true);
                }
            }
            if(distanceTest > 0.025)
            {
                if(ActionMenu.transform.Find("ActionMenu/Main/Actions/Config/Selected").gameObject.active = true)
                {
                    ActionMenu.transform.Find("ActionMenu/Main/Actions/Config/Selected").gameObject.active = false;
                }
            }
        }
    }
}
