using App.AssetLoading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace App.Session
{

    public class SessionManager : MonoBehaviour
    {
        [SerializeField] private PlayerManagerBase playerPrefab;

        [SerializeField] private TextMeshProUGUI m_SessionIdText;
        [SerializeField] private TextMeshProUGUI m_AvatarMeshUrlText;
        [SerializeField] private TextMeshProUGUI m_AvatarImageUrlText;

        private PlayerManagerBase m_PlayerInstance;

        private ModelLoader m_ModelLoader = new();
        private ImageLoader m_ImageLoader = new();

        private void Awake()
        {
            m_PlayerInstance = Instantiate(playerPrefab);
        }

        /// <summary>
        /// Creates or joins a session using the specified session ID. This method handles asynchronous operations
        /// involved in establishing a session connection, either by creating a new session or joining an existing one.
        /// </summary>
        /// <param name="id">The unique identifier for the session to create or join.</param>
        /// <returns>A UniTask representing the asynchronous operation of creating or joining the session.</returns>
        public async UniTask CreateOrJoinSession(int id)
        {
            // in here we would:
            // - create a new session with the given id
            // - or join an existing session with the given id
            
            // FIXME: for now just return a completed task
            Debug.Log($"CreateOrJoinSession: {id}");
            m_SessionIdText.text = id.ToString();
            await UniTask.CompletedTask;
        }

        /// <summary>
        /// Sets the current user's name to the specified username.
        /// </summary>
        /// <param name="playerName">The name to be assigned to the current user.</param>
        public void SetPlayerName(string playerName)
        {
            m_PlayerInstance.SetPlayerName(playerName);
        }

        /// <summary>
        /// Downloads and sets the avatar of the current user from the specified URL.
        /// </summary>
        /// <param name="url">The URL from which to download the avatar image.</param>
        public async UniTask SetAvatarModelFromUrlAsync(string url)
        {
            m_AvatarMeshUrlText.text = url;
            var mesh = await m_ModelLoader.LoadAssetFromUrlAsync(url);
            m_PlayerInstance.SetAvatarMesh(mesh);
        }

        public async UniTask SetAvatarImageFromUrlAsync(string url)
        {
            m_AvatarImageUrlText.text = url;
            var image = await m_ImageLoader.LoadAssetFromUrlAsync(url);
            m_PlayerInstance.SetAvatarImage(image);
        }
    }
}