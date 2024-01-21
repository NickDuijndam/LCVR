using System;
using System.Net;
using UnityEngine.InputSystem;

namespace LCVR.Input
{
    public class Actions
    {
        public static InputAction Head_Position;
        public static InputAction Head_Rotation;
        public static InputAction Head_TrackingState;

        public static InputAction RightHand_Position;
        public static InputAction RightHand_Rotation;
        public static InputAction RightHand_TrackingState;

        public static InputAction LeftHand_Position;
        public static InputAction LeftHand_Rotation;
        public static InputAction LeftHand_TrackingState;

        public static InputAction RightFoot_Position;
        public static InputAction RightFoot_Rotation;
        public static InputAction RightFoot_TrackingState;
        
        public static InputAction LeftFoot_Position;
        public static InputAction LeftFoot_Rotation;
        public static InputAction LeftFoot_TrackingState;
        
        public static InputAction Waist_Position;
        public static InputAction Waist_Rotation;
        public static InputAction Waist_TrackingState;

        public readonly static InputActionAsset VRInputActions = InputActionAsset.FromJson(Properties.Resources.vr_inputs);
        public readonly static InputActionAsset LCInputActions = InputActionAsset.FromJson(Properties.Resources.lc_inputs);

        static Actions()
        {
            (VRInputActions, LCInputActions) = DownloadControllerProfiles(Plugin.Config.ControllerBindingsOverrideProfile.Value);

            Head_Position = VRInputActions.FindAction("Head/Position");
            Head_Rotation = VRInputActions.FindAction("Head/Rotation");
            Head_TrackingState = VRInputActions.FindAction("Head/Tracking State");

            RightHand_Position = VRInputActions.FindAction("Right Hand/Position");
            RightHand_Rotation = VRInputActions.FindAction("Right Hand/Rotation");
            RightHand_TrackingState = VRInputActions.FindAction("Right Hand/Tracking State");

            LeftHand_Position = VRInputActions.FindAction("Left Hand/Position");
            LeftHand_Rotation = VRInputActions.FindAction("Left Hand/Rotation");
            LeftHand_TrackingState = VRInputActions.FindAction("Left Hand/Tracking State");

            RightFoot_Position = VRInputActions.FindAction("Right Foot/Position");
            RightFoot_Rotation = VRInputActions.FindAction("Right Foot/Rotation");
            RightFoot_TrackingState = VRInputActions.FindAction("Right Foot/Tracking State");
            
            LeftFoot_Position = VRInputActions.FindAction("Left Foot/Position");
            LeftFoot_Rotation = VRInputActions.FindAction("Left Foot/Rotation");
            LeftFoot_TrackingState = VRInputActions.FindAction("Left Foot/Tracking State");
            
            Waist_Position = VRInputActions.FindAction("Waist/Position");
            Waist_Rotation = VRInputActions.FindAction("Waist/Rotation");
            Waist_TrackingState = VRInputActions.FindAction("Waist/Tracking State");

            LCInputActions.Enable();
            VRInputActions.Enable();
        }

        /// <summary>
        /// Dynamically download controller profiles from GitHub, so that users won't have to
        /// mess around with files and only need to specify a profile name inside the configuration.
        /// </summary>
        /// <returns></returns>
        private static (InputActionAsset, InputActionAsset) DownloadControllerProfiles(string profile)
        {
            try
            {
                if (string.IsNullOrEmpty(profile))
                    throw new Exception("Using default controller profile");

                using var client = new WebClient();

                var vr = client.DownloadString($"https://raw.githubusercontent.com/DaXcess/LCVR-Controller-Profiles/main/{profile}/lcvr_vr_inputs.json");
                var lc = client.DownloadString($"https://raw.githubusercontent.com/DaXcess/LCVR-Controller-Profiles/main/{profile}/lcvr_lc_inputs.json");

                return (InputActionAsset.FromJson(vr), InputActionAsset.FromJson(lc));
            }
            catch
            {
                return (VRInputActions, LCInputActions);
            }
        }

        public static void ReloadInputBindings()
        {
            IngamePlayerSettings.Instance.playerInput.actions = LCInputActions;

            Logger.LogDebug("Loaded XR input binding overrides");
        }
    }
}
