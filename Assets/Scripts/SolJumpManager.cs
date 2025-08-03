using UnityEngine;
using UnityEngine.SceneManagement;

public class SolJumpManager : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public GameObject sun;

    public int currentDDU = 150;
    public int jumpCount = 0;

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
        currentDDU -= jumpCount * 50;
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

        // Set Sun HP and Tier
        Health sunHealth = sun.GetComponent<Health>();
        sunHealth.health = sunHP;
        sunHealth.maxHealth = 1000;

        mapGenerator.currentSolTier = tier;

        // Regenerate world
        mapGenerator.GenerateMap();

        Debug.Log($"Jumped to new Sol! DDU: {currentDDU}, Tier: {tier}, Sun HP: {sunHP}");
    }
}
