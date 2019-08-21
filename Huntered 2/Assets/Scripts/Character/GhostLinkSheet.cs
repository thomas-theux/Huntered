using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLinkSheet : MonoBehaviour {

    private PlayerSheet playerSheetScript;


    private void Awake() {
        playerSheetScript = GetComponent<PlayerSheet>();
    }


    public void UpdateLinkedGhosts(int addedEffect) {
        switch (addedEffect) {

            //////////////
            // STRENGTH //
            //////////////

            // Deal more damage
            case 00:
                break;
            // Receive less damage
            case 01:
                break;
            // Increase being-hit-heal chance
            case 02:
                break;
            // Gain 1% health every 1000 steps
            case 03:
                break;
            // Increase hit-heal chance
            case 04:
                break;
            
            ///////////
            // SPEED //
            ///////////

            // Increase attack speed
            case 05:
                break;
            // Shorten respawn delay
            case 06:
                break;
            // Increase move speed
            case 07:
                GoThroughLinkedGhosts(addedEffect);
                break;
            // Shorten skill cooldown
            case 08:
                break;
            
            //////////
            // LUCK //
            //////////

            // Increase dodge heal
            case 09:
                break;
            // Increase crit hit chance
            case 10:
                break;
            // Increase dodge-heal chance
            case 11:
                break;
            // Increase crit-hit-heal chance
            case 12:
                break;
            // Increase dodge chance
            case 13:
                break;
            // Increase crit hit damage
            case 14:
                break;
            // Increase crit hit heal
            case 15:
                break;
            
            ////////////
            // WISDOM //
            ////////////

            // Increase gold pickup radius
            case 16:
                break;
            // Increase XP gain
            case 17:
                break;
            // Gain gold when leveling up
            case 18:
                break;
            // Increase collect-double-gold chance
            case 19:
                break;
            // Gain 1% gold every 1000 steps
            case 20:
                break;
            // Gain 1% XP every 1000 steps
            case 21:
                break;
            // Increase gold drop
            case 22:
                break;
            // Increase Ghost drop chance
            case 23:
                break;
        }
    }


    private void GoThroughLinkedGhosts(int addedEffect) {
        float newMoveSpeed = playerSheetScript.moveSpeed;

        for (int i = 0; i < playerSheetScript.UnlockedSlots; i++) {
            if (playerSheetScript.SlottedGhostsArr[0][i].Contains("Effect")) {
                int getEffectID = (int)playerSheetScript.SlottedGhostsArr[0][i]["Effect"];

                if (getEffectID == addedEffect) {
                    float addSpeed = newMoveSpeed * ((float)playerSheetScript.SlottedGhostsArr[0][i]["Effect Value"] / 100);
                    newMoveSpeed += addSpeed;
                }
            }
        }

        playerSheetScript.moveSpeed = newMoveSpeed;
    }

}
