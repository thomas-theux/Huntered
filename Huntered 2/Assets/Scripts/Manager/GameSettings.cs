
public class GameSettings {

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
    public static int minGoldDrop = 3;
    public static int maxGoldDrop = 7;

    // Size of the area where enemies drop loot – in this case 2.0m
    public static float dropAreaSize = 2.0f;

    public static float enemySpeedMultiplier = 0.1f;
    public static float enemyDamageMultiplier = 0.3f;

}
