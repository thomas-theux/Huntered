using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GhostSheet : MonoBehaviour {

    public Hashtable GhostStats = new Hashtable();

    public Image GhostImage;
    public Image GhostTypeTag;

    public TMP_Text GhostLevel;
    public TMP_Text GhostName;
    public TMP_Text GhostDescription;
    public TMP_Text GhostValue;
    public TMP_Text GhostLinkChance;
    public TMP_Text GhostType;


    private void Start() {
        int ghostType = (int)GhostStats["Type"];
        string typeText = "";

        switch (ghostType) {
            case 0:
                typeText = TextsUI.GhostsTypeStrength[GameSettings.language];
                break;
            case 1:
                typeText = TextsUI.GhostsTypeSpeed[GameSettings.language];
                break;
            case 2:
                typeText = TextsUI.GhostsTypeLuck[GameSettings.language];
                break;
            case 3:
                typeText = TextsUI.GhostsTypeWisdom[GameSettings.language];
                break;
        }

        GhostImage.color = ColorManager.GhostColors[ghostType];
        GhostTypeTag.color = ColorManager.GhostColors[ghostType];

        GhostLevel.text = (int)GhostStats["Level"] + "";
        GhostName.text = (string)GhostStats["Name"];
        GhostDescription.text = (string)GhostStats["Description"];
        GhostValue.text = (int)GhostStats["Value"] + "●";
        GhostLinkChance.text = (int)GhostStats["Link Chance"] + "%";

        GhostType.text = typeText;
    }

}