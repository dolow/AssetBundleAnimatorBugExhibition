using UnityEditor;
using System.IO;

public class BuildAssetBundle
{
    [MenuItem ("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string dest = UnityEngine.Application.dataPath + "/StreamingAssets";

        if (Directory.Exists(dest))
            Directory.Delete(dest, recursive:true);
        
        Directory.CreateDirectory(dest);

        BuildPipeline.BuildAssetBundles(dest, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXIntel64);
    }
}