using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FunctionalUtility
{
    public static void SetLayerRecursively(this GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetLayerRecursively(layer);
        }
    }

    public static void SetScaleRecursively(this GameObject obj, float scale)
    {
        obj.transform.localScale *= scale;

        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetScaleRecursively(scale);
        }
    }
}
