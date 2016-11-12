using UnityEngine;
using System.Collections;

public class BoardCreation : MonoBehaviour 
{
    int die1;
    int die2;
    /* Method for deciding between normal and special
     * 70% chance it will be normal
     * 30% chance it will be special
     */
    void ChooseBlocks()
    {
        die1 = Random.Range(0, 100);
        if (die1 <= 70)
        {
            //choose normal block
        }
        else
        {
            ChooseSpecial();
        }
    }
    /* Method for deciding between pattern, hazard and powerup
     * if it's level 1
         * 0% hazard
         * 0% pattern
         * 100% powerup
     * else
         * 50% hazard
         * 10% pattern
         * 40% powerup
     */
    void ChooseSpecial()
    {
        die1 = Random.Range(0, 6);
        die2 = Random.Range(0, 6);
        //if level 1 just choose powerup
        int totalDice = die1 + die2;
        if (totalDice <= 3)
        {
            ChooseHazard();
        }
        else if (totalDice > 3 && totalDice < 9)
        {
            //Choose pattern
        }
        else
        {
            ChoosePowerUp();
        }

    }
    /* Method for deciding between hazards
     * 40% spikes
     * 40% cannons
     * 20% dynomite
     */
    void ChooseHazard()
    {
        die1 = Random.Range(0, 6);
        die2 = Random.Range(0, 6);
        int totalDice = die1 + die2;
        if (totalDice <= 3)
        {
            //choose dynamite
        }
        else if (totalDice > 3 && totalDice < 9)
        {
            //choose spikes
        }
        else
        {
            //choose cannon
        }
    }
    /* Method for deciding between powerups
     * 10% stars
     * 30% gems
     * 70% chests
     */
    void ChoosePowerUp()
    {
        die1 = Random.Range(0, 6);
        die2 = Random.Range(0, 6);
        int totalDice = die1 + die2;
        if (totalDice <= 3)
        {
            //Choose star
        }
        if (totalDice > 3 && totalDice < 9)
        {
            ChooseChests();
        }
        else
        {
            //choose gem
        }
    }
    /* Method for deciding chests
     * 80% crates
     * 20% treasure chests
     */
    void ChooseChests()
    {
        die1 = Random.Range(0, 100);
        if (die1 < 80)
        {
            //choose crate
        }
        else
        {
            //choose treasure chest
        }
    }
}
