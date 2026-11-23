using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LiveConfigLoader : MonoBehaviour
{
    const string CacheKey = "LiveConfig.LastJson";

    [SerializeField] LiveConfigAsset liveConfig;
    [SerializeField]
    string url =
        "https://raw.githubusercontent.com/<user>/<repo>/main/LiveOps/live-config.json";
    [SerializeField] int timeoutSeconds = 5;

    void Start()
    {
        // 1. Use the last good copy immediately, so the game never waits for the network.
        string cached = PlayerPrefs.GetString(CacheKey, "");
        if (TryApply(cached)) Debug.Log("[LiveOps] Applied cached config");

        // 2. Then try to fetch the newest copy.
        StartCoroutine(Fetch());
    }

    IEnumerator Fetch()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.timeout = timeoutSeconds;
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"[LiveOps] Download failed: {request.error}. Keeping current config.");
                yield break;
            }

            string json = request.downloadHandler.text;
            if (TryApply(json))
            {
                PlayerPrefs.SetString(CacheKey, json);
                PlayerPrefs.Save();
                Debug.Log($"[LiveOps] Applied remote config v{liveConfig.Current.version}, " +
                          $"event active: {liveConfig.EventActive}");
            }
        }
    }

    bool TryApply(string json)
    {
        if (string.IsNullOrEmpty(json)) return false;
        try
        {
            LiveConfig config = JsonUtility.FromJson<LiveConfig>(json);
            if (config == null) return false;
            liveConfig.Apply(config);
            return true;
        }
        catch (System.ArgumentException e)   // thrown by JsonUtility on malformed JSON
        {
            Debug.LogWarning($"[LiveOps] Bad JSON ignored: {e.Message}");
            return false;
        }
    }
}