using UnityEngine;
using UnityEngine.UI;

public class SolJumpTester : MonoBehaviour
{
    public SolJumpManager jumpManager;

    void Start()
    {
        //Button jumpA = GameObject.Find("JumpA").GetComponent<Button>();
        //Button jumpB = GameObject.Find("JumpB").GetComponent<Button>();
        //Button jumpC = GameObject.Find("JumpC").GetComponent<Button>();

        //jumpA.onClick.AddListener(() => jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.A));
        //jumpB.onClick.AddListener(() => jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.B));
        //jumpC.onClick.AddListener(() => jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.C));
    }

    public void Jump() 
    {
        if (Resources.fuel >= 50 && Resources.fuel < 75) jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.A);
        if (Resources.fuel >= 75 && Resources.fuel < 100) jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.B);
        if (Resources.fuel >= 100) jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.C);
        Resources.fuel = 0;
    }
}
