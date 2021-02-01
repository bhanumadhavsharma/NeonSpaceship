using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp")]
public class PowerUp : ScriptableObject
{
    public new string powerUpType;
    public Sprite artwork;
    public bool slowMotion;
    public bool invincibility;
    public bool fastShooting;
    public bool coinMagnet;
}
