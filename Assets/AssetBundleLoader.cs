using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleLoader : MonoBehaviour
{
    void Start ()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        WWW rootWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_root"));

        yield return rootWww;

        if (rootWww.error != null) {
            Debug.Log(rootWww.error);
            yield break;
        }

        WWW depsWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_dependency"));

        yield return depsWww;

        if (depsWww.error != null) {
            Debug.Log(rootWww.error);
            yield break;
        }

        GameObject go = rootWww.assetBundle.LoadAsset<GameObject>("Root");
        Instantiate(go);
    }

    string GetStreamingAssetsUrl(string path)
    {
        return "file://" + Application.streamingAssetsPath + path;
    }
}
