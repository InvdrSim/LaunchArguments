using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace App.AssetLoading
{
    public abstract class AssetLoaderBase<T> : IAssetLoader<T>
    where T : UnityEngine.Object
    {
        public abstract T LoadAsset(byte[] data);

        public async UniTask<T> LoadAssetFromUrlAsync(string url)
        {
            Debug.Log($"Loading asset ({typeof(T)}) from {url}");
            var request = UnityWebRequest.Get(url);
            request.timeout = 10;
            try
            {
                await request.SendWebRequest();
            }
            catch (UnityWebRequestException) { }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error downloading asset '{url}': {request.error}");
                return null;
            }

            byte[] data = request.downloadHandler.data;
            return LoadAsset(data);
        }
    }
}