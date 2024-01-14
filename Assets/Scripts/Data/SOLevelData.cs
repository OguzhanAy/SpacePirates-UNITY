using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData",menuName = "Moon Games/CN Space pirates/Create Level Data")]
public class SOLevelData : ScriptableObject
{
    //Todo: Verileri obscured(hafıza tarayıcılara karşı korumalı) yapmak
    public string LevelPrefab;
    public Material SkyBoxMaterial;
    public Vector3 StartPosition;

}
