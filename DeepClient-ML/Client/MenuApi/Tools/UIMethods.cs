namespace AstroClient.MenuApi.Tools
{
    using System;
    using System.Reflection;
    using AstroClient.ClientActions;
    using MelonLoader;
    using UnhollowerRuntimeLib;
    using VRC.Core;

    internal class UiMethods : AstroEvents
    {
        internal override void RegisterToEvents()
        {
            ClientEventActions.OnApplicationStart += OnApplicationStart;
        }

        internal static MethodInfo _apiUserToIUser;

        private void OnApplicationStart()
        {
            try
            {
                // Use APIUser itself as the IUser implementation
                Type iUserImpl = typeof(APIUser);

                // Retrieve the generic converter method from ActionMenu API host type
                MethodInfo methodInfo = typeof(ObjectPublicDi2StObUnique)
                    .GetMethod(
                        nameof(ObjectPublicDi2StObUnique.Method_Public_TYPE_String_TYPE2_Boolean_0)
                    );

                if (methodInfo == null)
                {
                    Log.Error(
                        "UiMethods: Method_Public_TYPE_String_TYPE2_Boolean_0 not found on ObjectPublicDi2StObUnique");
                    return;
                }

                _apiUserToIUser = methodInfo.MakeGenericMethod(iUserImpl, typeof(APIUser));
                MelonLogger.Msg($"UiMethods: converter bound to {iUserImpl.FullName}");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"UiMethods.OnApplicationStart failed: {ex}");
            }
        }
    }
}