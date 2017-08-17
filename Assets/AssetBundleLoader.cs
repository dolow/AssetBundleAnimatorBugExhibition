using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundleLoader : MonoBehaviour
{
    private void Start()
    {
        Canvas   canvas  = GameObject.FindObjectOfType<Canvas>();
        Button[] buttons = canvas.gameObject.GetComponentsInChildren<Button>();

        System.Type thisType = this.GetType();

        MethodInfo[] methods = thisType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
         
        for (int i = 0; i < methods.Length; i++) {
            MethodInfo method = methods[i];

            if (method.ReturnType.FullName != typeof(IEnumerator).FullName)
                continue;
            
            // for (int j = 0; j < fields.Length; j++) {
            //     FieldInfo field = fields[j];
            for (int j = 0; j < buttons.Length; j++) {
                Button button = buttons[j];
                if (button.gameObject.name != "Button" + method.Name)
                    continue;
                
                button.onClick.AddListener(() => StartCoroutine((IEnumerator)method.Invoke(this, null)));
                break;
            }
        }
    }

    private IEnumerator LoadSafeAssetBundle()
    {
        Debug.Log("LoadSafeAssetBundle");
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

    private IEnumerator LoadUnsafeCase1AssetBundle()
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

        // if it is not loaded, it causes crash when try to show inspector window of Root prefab
        // depsWww.assetBundle.LoadAsset<AnimationClip>("AnimationClip");

        Debug.Log("if you try to show inspector window of Root instance, UnityEditor will crash");

        GameObject go = rootWww.assetBundle.LoadAsset<GameObject>("Root");
        Instantiate(go);
    }

    private IEnumerator LoadUnsafeCase2AssetBundle()
    {
        WWW rootWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_unsafecase2_root"));

        yield return rootWww;

        if (rootWww.error != null) {
            Debug.Log(rootWww.error);
            yield break;
        }

        WWW depsWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_unsafecase2_deps"));

        yield return depsWww;

        if (depsWww.error != null) {
            Debug.Log(rootWww.error);
            yield break;
        }

        // if it is not loaded, it causes crash when try to show inspector window of Dependency prefab
        /*
        WWW depdepWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_unsafecase2_depdep"));

        yield return depdepWww;

        if (depdepWww.error != null) {
            Debug.Log(depdepWww.error);
            yield break;
        }

        depdepWww.assetBundle.LoadAsset<AnimationClip>("AnimationClip");
        */

        depsWww.assetBundle.LoadAsset<GameObject>("Dependency");

        GameObject go = rootWww.assetBundle.LoadAsset<GameObject>("Root");
        Instantiate(go);
    }

    private IEnumerator LoadUnsafeCase3AssetBundle()
    {
        WWW rootWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_unsafecase3_root"));

        yield return rootWww;

        if (rootWww.error != null) {
            Debug.Log(rootWww.error);
            yield break;
        }

        WWW depsWww = new WWW(this.GetStreamingAssetsUrl("/ab_test_unsafecase3_deps"));

        yield return depsWww;

        if (depsWww.error != null) {
            Debug.Log(rootWww.error);
            yield break;
        }

        // if it is not loaded, it causes crash when try to show inspector window of Dependency prefab
        // depsWww.assetBundle.LoadAsset<AnimationClip>("AnimationClip");

        GameObject go = rootWww.assetBundle.LoadAsset<GameObject>("Root");
        Instantiate(go);
    }


    private string GetStreamingAssetsUrl(string path)
    {
        return "file://" + Application.streamingAssetsPath + path;
    }
}
