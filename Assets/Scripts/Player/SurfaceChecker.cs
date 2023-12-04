using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SurfaceType { Stone, Wood, Water }

public class SurfaceChecker : MonoBehaviour
{
    [SerializeField]
    Material[] woodMaterials, stoneMaterials;

    string[] woodNames, stoneNames;

    public static SurfaceChecker Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        woodNames = new string[woodMaterials.Length];
        stoneNames = new string[stoneMaterials.Length];
        for(int i =0; i < woodMaterials.Length; i++)
        {
            woodNames[i] = woodMaterials[i].name + " (Instance)";   
        }
        for (int i = 0; i < stoneMaterials.Length; i++)
        {
            stoneNames[i] = stoneMaterials[i].name + " (Instance)";
        }
    }

    public SurfaceType GetSurfaceTypeFromMaterials(Material[] mats)
    {
        if(mats == null)
        {
            return SurfaceType.Stone;
        }
        foreach(Material mat in mats)
        {
            foreach(string s in woodNames)
            {
                if (s == mat.name)
                {
                    return SurfaceType.Wood;
                }
            }
            foreach (string s in stoneNames)
            {
                if (s == mat.name)
                {
                    return SurfaceType.Stone;
                }
            }
        }
        return SurfaceType.Stone;
    }
}
