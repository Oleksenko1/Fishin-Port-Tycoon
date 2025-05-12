using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FishSO", menuName = "Scriptable Objects/FishSO")]
public class FishSO : ScriptableObject
{
    public string nameString;
    [Tooltip("How fast the cursor moves during fishing")]
    public FishSpeed speed;
    [Tooltip("How small is catch area during fishing")]
    public FishStrength stregth;
    public FishRarity rarity;
    public int sellValue;
    [Space(10)]
    public Transform model;
}
public enum FishSpeed
{
    VerySlow,
    Slow,
    Average,
    Fast,
    VeryFast,
    Flash,
    LightSpeed
}

public enum FishStrength
{
    VeryWeak,
    Weak,
    Average,
    Strong,
    VeryStrong,
    Overpowered,
    Godlike
}

public enum FishRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Mythic,
    Legendary
}
