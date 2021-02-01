using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int health, maxHealth, amtBullets;

    public float bulletFlyingSpeed, bulletShootingSpeed;

    public float playerLevel, xpRequired, xp;

    public int coinCount;
    public bool coinX4Unlocked;

    public int shipColor, shipType;
    public bool shipColor0, shipColor1, shipColor2, shipColor3, shipColor4;
    public bool shipType0, shipType1, shipType2, shipType3, shipType4;

    public bool immune, asteroidImmune, shooterImmune, hovererImmune;

    public bool musicType0, musicType1, musicType2, musicType3;

    public SaveData()
    {
        health = PlayerStats.instance.health;
        maxHealth = PlayerStats.instance.maxHealth;
        amtBullets = PlayerStats.instance.amtBullets;

        bulletFlyingSpeed = PlayerStats.instance.bulletFlyingSpeed;
        bulletShootingSpeed = PlayerStats.instance.bulletShootingSpeed;

        playerLevel = PlayerStats.instance.playerLevel;
        xpRequired = PlayerStats.instance.xpRequired;
        xp = PlayerStats.instance.xp;

        coinCount = PlayerStats.instance.coinCount;
        coinX4Unlocked = PlayerStats.instance.coinX4Unlocked;

        shipColor = PlayerStats.instance.shipColor;
        shipType = PlayerStats.instance.shipType;
        shipColor0 = PlayerStats.instance.shipColor0;
        shipColor1 = PlayerStats.instance.shipColor1;
        shipColor2 = PlayerStats.instance.shipColor2;
        shipColor3 = PlayerStats.instance.shipColor3;
        shipColor4 = PlayerStats.instance.shipColor4;
        shipType0 = PlayerStats.instance.shipType0;
        shipType1 = PlayerStats.instance.shipType1;
        shipType2 = PlayerStats.instance.shipType2;
        shipType3 = PlayerStats.instance.shipType3;
        shipType4 = PlayerStats.instance.shipType4;

        immune = PlayerStats.instance.immune;
        asteroidImmune = PlayerStats.instance.asteroidImmune;
        shooterImmune = PlayerStats.instance.shooterImmune;
        hovererImmune = PlayerStats.instance.hovererImmune;

        /*musicType0 = GameManager.instance.musicType0;
        musicType1 = GameManager.instance.musicType1;
        musicType2 = GameManager.instance.musicType2;
        musicType3 = GameManager.instance.musicType3; */

        // DONT FORGET TO MAKE THESE CHANGES TO LOAD IN PLAYERSTATS.START() AND PLAYERSTATS.SETINITIALSTATS()

        //any changes added here, add to <load in PlayerStats.Start()> and to <setInitialStats in PlayerStats.SetInitialStats()>
    }
}
