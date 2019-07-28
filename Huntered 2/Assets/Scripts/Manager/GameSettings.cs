﻿
public class GameSettings {

    // Game language // 0 = english ; 1 = german ; 2 = developers edition (swabian)
    public static int language = 2;

    public static int ConnectedGamepads = 0;
    public static int PlayerCount = 0;
    public static int PlayerMax = 2;

    public static float MoveDelay = 0.01f;

    // Base reputation and multiplier with which every level will be calculated
    public static int baseRepGain = 2;
    public static int baseRepNeeded = 8;

    public static float repMultiplier = 1.9f;
    public static float repNeededMultiplier = 2.4f;

    public static float NPCKillMultiplier = 2.0f;

    public static int maxRepLevel = 50;

    // Base health of the enemy with which every level will be calculated with
    public static float baseNPCHealth = 100.0f;
    public static float enemyBaseHealth = 40.0f;
    public static float enemyHealthMultiplier = 1.3f;

    // Base gold and mutliplier with which every loot will be calculated with
    public static int baseGoldGain = 50;
    public static float goldMultiplier = 1.05f;

    // How much gold will be dropped minimum and maximum
    public static int minGoldDrop = 4;
    public static int maxGoldDrop = 8;

    // Set the min and max for a Ghosts worth
    public static int minGhostValue = 500;
    public static int maxGhostValue = 1000;

    // Size of the area where enemies drop loot – in this case 2.0m
    public static float dropAreaSize = 2.0f;

    public static float enemySpeedMultiplier = 0.1f;
    public static float enemyDamageMultiplier = 0.3f;

    // Min values for hero's stats
    // public static float MinHealthStat = 50.0f;
    // public static float MinDamageStat = 16.0f;
    // public static float MinSpeedStat = 6.0f;
    public static float MinCooldownStat = 1000.0f;

    // Max values for hero's stats
    public static float MaxHealthStat = 1000.0f;
    public static float MaxDamageStat = 500.0f;
    public static float MaxSpeedStat = 22.0f;
    public static float MaxCooldownStat = 200.0f;

    // ALTERNATE VALUES FOR DEV TESTING
    // public static float MaxHealthStat = 60.0f;
    // public static float MaxDamageStat = 18.0f;
    // public static float MaxSpeedStat = 8.0f;
    // public static float MaxCooldownStat = 450.0f;

}
