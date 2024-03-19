using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class ManaManagment : MonoBehaviour
{
    public int manaPoints = 100;
    public GameObject manaBar;
    public Text manaPointsText;
    UnityEngine.Vector3 manaBarScale;

    float manaBarX;
    float manaBarY;

    // Start is called before the first frame update
    void Start()
    {
        manaBarX = manaBar.transform.position.x;
        manaBarY = manaBar.transform.position.y;

        manaBarScale = manaBar.transform.localScale;
    }

    public bool CastManaSpell(int cost)
    {
        if (manaPoints < cost)
            return false;
        manaPoints -= cost;
        UpdateManaBar();
        return true;
    }

    void UpdateManaBar()
    {
        manaPointsText.text = manaPoints.ToString();
        UnityEngine.Vector3 locScale = manaBar.transform.localScale;
        locScale.x = manaBarScale.x * manaPoints/100f;
        manaBar.transform.localScale = locScale;
        manaBar.transform.position = new UnityEngine.Vector3(manaBarX + (manaPoints - 100)*3f/2f, manaBarY, 0);
    }

    
}
