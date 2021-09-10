using System;
using MelonLoader;
using HarmonyLib;
using System.Reflection;

namespace DavigeditPlus
{
    [HarmonyPatch(typeof(Davigo.Davigedit.CannonControllerObject), "Process")]
    class Patch_CannonControllerObject
    {
        public static void Prefix(Davigo.Davigedit.CannonControllerObject __instance)
        {
            Type type_identifiableObject = typeof(Davigo.Davigedit.CannonControllerObject);
            PropertyInfo prop = type_identifiableObject.GetProperty("Replacement");
            IdentifiableObject replacement = (IdentifiableObject)prop.GetValue(__instance);

            __instance.gameObject.transform.parent.GetComponent<CannonEvents>().cannonObject = replacement.gameObject;
        }
    }
}
