using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SolJumpManager : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public GameObject sun;

    public int currentDDU = 150;
    public int jumpCount = 0;
    public TextMeshProUGUI jumpText;

    public enum JumpLevel { A, B, C }

    public void JumpToNewSol(JumpLevel jumpLevel)
    {
        // Increase DDU based on jump level
        switch (jumpLevel)
        {
            case JumpLevel.A: currentDDU += 25; break;
            case JumpLevel.B: currentDDU += 50; break;
            case JumpLevel.C: currentDDU += 100; break;
        }

        // Apply penalty based on total jumps
        //currentDDU -= jumpCount * 50;
        //currentDDU = Mathf.Max(0, currentDDU);

        if (jumpCount >= 7) currentDDU -= 85;
        else if (jumpCount >= 4) currentDDU -= 65;
        else currentDDU -= 50;
        currentDDU = Mathf.Max(0, currentDDU);


        // Determine Sun HP and Sol Tier
        float sunHP;
        MapGenerator.SolTier tier;
        if (currentDDU >= 200)
        {
            sunHP = 1000;
            tier = MapGenerator.SolTier.A;
        }
        else if (currentDDU >= 150)
        {
            sunHP = 800;
            tier = MapGenerator.SolTier.B;
        }
        else if (currentDDU >= 100)
        {
            sunHP = 600;
            tier = MapGenerator.SolTier.C;
        }
        else
        {
            sunHP = 400;
            tier = MapGenerator.SolTier.D;
        }

        jumpCount++;
        jumpText.text = "Jumps : " + jumpCount;

        // Set Sun HP and Tier
        Health sunHealth = sun.GetComponent<Health>();
        sunHealth.health = sunHP;
        sunHealth.maxHealth = 1000;

        mapGenerator.currentSolTier = tier;

        // Regenerate world
        mapGenerator.GenerateMap();

        // Limit coins
        if (Resources.coins > Resources.maxCoins)
            Resources.coins = Resources.maxCoins;

        // Revert Player Max Stats
        Resources.maxCoins = 100;
        Resources.maxFuel = 50;

        Debug.Log($"Jumped to new Sol! DDU: {currentDDU}, Tier: {tier}, Sun HP: {sunHP}");
    }
}
