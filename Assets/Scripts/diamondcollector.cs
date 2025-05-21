using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine;
public class diamondcollector : MonoBehaviour
{
    public static diamondcollector Instance;
    private int diamonds = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDiamond(int value = 1)
    {
        diamonds += value;
    }

    public int GetDiamondCount()
    {
        return diamonds;
    }
}