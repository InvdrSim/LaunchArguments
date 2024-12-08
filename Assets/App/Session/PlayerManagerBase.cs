using UnityEngine;

namespace App.Session
{
    public abstract class PlayerManagerBase : MonoBehaviour
    {
        public string PlayerName { get; private set; }
        public Mesh AvatarMesh { get; private set; }
        public Texture2D AvatarImage { get; private set; }

        /// <summary>
        /// Sets the player's name.
        /// </summary>
        /// <param name="playerName">The new name to assign to the player.</param>
        public virtual void SetPlayerName(string playerName)
        {
            PlayerName = playerName;
            Debug.Log($"SetPlayerName: {playerName}");
        }

        /// <summary>
        /// Sets the player's avatar using an image from the specified URL.
        /// </summary>
        /// <param name="url">The URL of the image to set as the player's avatar.</param>
        public virtual void SetAvatarMesh(Mesh mesh)
        {
            AvatarMesh = mesh;
            Debug.Log($"SetAvatarMesh: {mesh}");
        }
        
        public virtual void SetAvatarImage(Texture2D image)
        {
            AvatarImage = image;
            Debug.Log($"SetAvatarImage: {image}");
        }
    }
}