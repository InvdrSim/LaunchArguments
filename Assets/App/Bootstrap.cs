using System;
using App.Session;
using App.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App
{
    public class Bootstrap : MonoBehaviour
    {
        private const string k_UserNameKey = "portal-user-name";
        
        private const string k_SessionIdKey = "app-session-id";
        private const string k_AvatarModelUrlKey = "app-avatar-model-url";
        private const string k_AvatarImageUrlKey = "app-avatar-image-url";
        
        private void Start()
        {
            // for now call this in start
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            var appSettings = Arguments.Instance;

            // try to find the session manager
            var sessionManager = FindAnyObjectByType<SessionManager>();
            if (sessionManager == null)
            {
                Debug.LogError("No session manager found, unable to join session");
                return;
            }

            // try to find the session id in the launch arguments ... 
            if (!appSettings.TryGet(k_SessionIdKey, out int sessionId))
            {
                // no session id? nothing to join, so return early
                Debug.LogError($"\"{k_SessionIdKey}\" argument missing, unable to join session.");
                return;
            }

            // ... and start the session
            await sessionManager.CreateOrJoinSession(sessionId);

            // set the player name from the launch arguments
            ApplySetting<string>(k_UserNameKey, sessionManager.SetPlayerName);
            // set the avatar from the launch arguments
            ApplySetting<string>(k_AvatarModelUrlKey, url => sessionManager.SetAvatarModelFromUrlAsync(url).Forget());
            // set the avatar image from the launch arguments
            ApplySetting<string>(k_AvatarImageUrlKey, url => sessionManager.SetAvatarImageFromUrlAsync(url).Forget());
        }
        
        private void ApplySetting<T>(string key, Action<T> handler)
        {
            var appSettings = Arguments.Instance;
            if (appSettings.TryGet(key, out T value))
                handler(value);
            else
                Debug.LogWarning($"Missing argument: --{key}");
        }
    }
}