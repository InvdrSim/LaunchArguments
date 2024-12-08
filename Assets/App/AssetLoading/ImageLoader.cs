using UnityEngine;

namespace App.AssetLoading
{
    public class ImageLoader : AssetLoaderBase<Texture2D>
    {
        public override Texture2D LoadAsset(byte[] data)
        {
            var texture = new Texture2D(2, 2);
            texture.LoadImage(data);
            return texture;
        }
    }
}