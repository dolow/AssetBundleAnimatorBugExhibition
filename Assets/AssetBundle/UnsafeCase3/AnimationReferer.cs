using UnityEngine;

public class AnimationReferer : MonoBehaviour
{
    void Start ()
    {
        Transform[] trans = this.gameObject.GetComponentsInChildren<Transform>();

        GameObject cube = null;

        for (int i = 0; i < trans.Length; i++) {
            if (trans[i].gameObject.name == "Cube") {
                cube = trans[i].gameObject;
                break;
            }
        }

        if (cube == null) {
            Debug.Log("cube == null");
            return;
        }

        MeshRenderer meshRenderer = cube.gameObject.GetComponent<MeshRenderer>();

        if (meshRenderer == null) {
            Debug.Log("meshRenderer == null");
            return;
        }

        Material[] materials = meshRenderer.materials;

        Debug.Log("materials.Length : " + materials.Length);

        for (int i = 0; i < materials.Length; i++) {
            Debug.Log("materials[i].name : " + materials[i].name);
        }

        Animator anim = this.gameObject.GetComponent<Animator>();

        if (anim == null) {
            Debug.Log("anim == null");
            return;
        }

        if (anim.runtimeAnimatorController == null) {
            Debug.Log("anim.runtimeAnimatorController == null");
            return;
        }

        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;

        Debug.Log("clips.Length : " + clips.Length);

        for (int i = 0; i < clips.Length; i++) {
            Debug.Log("clips[i].name : " + clips[i].name);
        }
    }
}
