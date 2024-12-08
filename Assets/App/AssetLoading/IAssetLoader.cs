using Cysharp.Threading.Tasks;

namespace App.AssetLoading
{
    public interface IAssetLoader<T>
        where T : UnityEngine.Object
    {
        public T LoadAsset(byte[] data);
        public UniTask<T> LoadAssetFromUrlAsync(string url);
    }
}