using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace App.Session
{
    /// <summary>
    /// Manages player-related functionalities, including setting player names and avatars.
    /// </summary>
    public class PlayerManager : PlayerManagerBase
    {
        [SerializeField] private TextMeshProUGUI m_PlayerNameText;

        [Header("Avatar Mesh")]
        [SerializeField] private Mesh m_DefaultPlayerMesh;
        [FormerlySerializedAs("m_MeshRenderer")] [SerializeField]
        private MeshFilter m_MeshFilter;

        [Header("Avatar Image")]
        [SerializeField] private Texture2D m_DefaultAvatarImage;
        [SerializeField] private RawImage m_AvatarImage;

        private Mesh m_PlayerMesh;

        private void Awake()
        {
            if (m_MeshFilter)
                m_MeshFilter.mesh = m_DefaultPlayerMesh;
        }

        private void Update()
        {
            if (Keyboard.current.aKey.isPressed)
            {
                transform.position += Vector3.left * Time.deltaTime;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                transform.position += Vector3.right * Time.deltaTime;
            }
        }

        /// <summary>
        /// Sets the player's name and updates the UI text element to reflect the new name.
        /// </summary>
        /// <param name="playerName">The new name to assign to the player.</param>
        public override void SetPlayerName(string playerName)
        {
            base.SetPlayerName(playerName);
            m_PlayerNameText.text = playerName;
        }

        /// <summary>
        /// Sets the avatar mesh for the player.
        /// </summary>
        /// <param name="mesh">The mesh to set as the player's avatar.</param>
        public override void SetAvatarMesh(Mesh mesh)
        {
            base.SetAvatarMesh(mesh);
            if (!m_MeshFilter) return;
            
            if (mesh)
                m_MeshFilter.sharedMesh = mesh;
            else
                m_MeshFilter.mesh = m_DefaultPlayerMesh;
        }

        /// <summary>
        /// Sets the avatar image for the player.
        /// </summary>
        /// <param name="image">The image to set as the player's avatar.</param>
        public override void SetAvatarImage(Texture2D image)
        {
            base.SetAvatarImage(image);
            if (!m_AvatarImage) return;
            
            if (image)
                m_AvatarImage.texture = image;
            else
                m_AvatarImage.texture = m_DefaultAvatarImage;
        }
    }
}