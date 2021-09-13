using System;
using HarmonyLib;
using System.Reflection;

namespace DavigeditPlus
{
    [HarmonyPatch(typeof(Davigo.Davigedit.CannonControllerObject), "Process")]
    class Patch_CannonControllerObject
    {
        public static void Prefix(Davigo.Davigedit.CannonControllerObject __instance)
        {
            if (__instance.gameObject.transform.parent != null)
            {
                CannonSettings cannonSettings = __instance.gameObject.GetComponentInParent<CannonSettings>();
                if (cannonSettings != null)
                {
                    Type type_identifiableObject = typeof(Davigo.Davigedit.CannonControllerObject);
                    PropertyInfo prop = type_identifiableObject.GetProperty("Replacement");
                    IdentifiableObject replacement = (IdentifiableObject)prop.GetValue(__instance);

                    cannonSettings.cannonObject = replacement.gameObject;
                }
            }
        }
    }
}
