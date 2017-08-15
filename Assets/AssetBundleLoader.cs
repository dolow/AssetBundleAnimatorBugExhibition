using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class AssetBundleLoader : MonoBehaviour
{
    void Start ()
    {
        
        //StartCoroutine(LoadSafeAssetBundle());
        StartCoroutine(LoadUnsafeCase1AssetBundle());
    }

    IEnumerator LoadSafeAssetBundle()
    {
        WWW www = new WWW(this.GetStreamingAssetsUrl("/ab_test_safecase_root"));

        yield return www;

        if (www.error != null) {
            Debug.Log(www.error);
            yield break;
        }

        WWW depsWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_safecase_deps"));

        yield return depsWww;

        if (depsWww.error != null) {
            Debug.Log(depsWww.error);
            yield break;
        }

        // not crash even if it is not loaded
        depsWww.assetBundle.LoadAsset<Material>("Material");

        GameObject go = www.assetBundle.LoadAsset<GameObject>("Root");
        Instantiate(go);
    }
    IEnumerator LoadUnsafeCase1AssetBundle()
    {
        WWW rootWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_unsafecase1_root"));

        yield return rootWww;

        if (rootWww.error != null) {
            Debug.Log(rootWww.error);
            yield break;
        }

        WWW depsWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_unsafecase1_deps"));

        yield return depsWww;

        if (depsWww.error != null) {
            Debug.Log(rootWww.error);
            yield break;
        }

        // crash if not loaded
        // depsWww.assetBundle.LoadAsset<AnimationClip>("AnimationClip");

        GameObject go = rootWww.assetBundle.LoadAsset<GameObject>("Root");
        Instantiate(go);
    }

    string GetStreamingAssetsUrl(string path)
    {
        return "file://" + Application.streamingAssetsPath + path;
    }
}
