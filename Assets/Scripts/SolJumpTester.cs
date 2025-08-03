using UnityEngine;
using UnityEngine.UI;

public class SolJumpTester : MonoBehaviour
{
    public SolJumpManager jumpManager;

    void Start()
    {
        Button jumpA = GameObject.Find("JumpA").GetComponent<Button>();
        Button jumpB = GameObject.Find("JumpB").GetComponent<Button>();
        Button jumpC = GameObject.Find("JumpC").GetComponent<Button>();

        jumpA.onClick.AddListener(() => jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.A));
        jumpB.onClick.AddListener(() => jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.B));
        jumpC.onClick.AddListener(() => jumpManager.JumpToNewSol(SolJumpManager.JumpLevel.C));
    }
}
