using UnityEngine;

namespace App.AssetLoading
{
    public class ModelLoader : AssetLoaderBase<Mesh>
    {
        public override Mesh LoadAsset(byte[] data)
        {
            // this is just an example
            // normally you would use an importer plugin here
            // for now we just log and return null

            Debug.LogWarning("Model loading is not implemented.");
            return null;
        }
    }
}