using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spellcaster : MonoBehaviour
{
    public GameObject nocturne;
    public GameObject raum;
    public GameObject spells;
    public GameObject spellSounds;
    public GameObject castSounds;

    int spellNumber = 0;
    List<List<char>> spellBook;
    string[] spellChorus;
    string spellCast = "";

    int[] spellCosts;

    // Start is called before the first frame update
    void Start()
    {
        spellNumber = spells.transform.childCount;
        spellBook = new(spellNumber);
        spellChorus = new string[spellNumber];
        spellCosts = new int[spellNumber];
        spellCosts[0] = 10;
        spellCosts[1] = 8;

        for (int i=0; i<spellNumber; i++)
        {
            int k = Random.Range(0, 10);  //70 | 30
            int size;
            if (k < 3)
            {
                size = 3;
            }
            else
            {
                size = 5;
            }
            string chant = "";
            List<char> spellscroll = new List<char>(size);
            for (int j=0; j<size; j++)
            {
                int z = Random.Range(0, 3);
                switch(z)
                {
                case 0:
                    spellscroll.Add('Q');
                    chant += "Q";
                    break;
                case 1:
                    spellscroll.Add('W');
                    chant += "W";
                    break;
                case 2:
                    spellscroll.Add('E');
                    chant += "E";
                    break;
                }
            }
            spellChorus[i] = chant;
            spellBook.Add(spellscroll);
            PrintScroll(spellBook[i], i);
        }

        InvokeRepeating("MaybeChangeSpell", 33f, 33f);
        InvokeRepeating("MaybeHideSpell", 10f, 11f);
    }

    // Update is called once per frame
    void Update()
    {
        if (spellCast.Length > 100)
        {
            spellCast = spellCast.Remove(0, 70);
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    spellCast += "Q";
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    spellCast += "W";
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    spellCast += "E";
                }
                CheckIfValidSpell(spellCast);
            }
            else
            {
                spellCast = "";
            }
        }
    }

    void PrintScroll(List<char> spellscroll, int index)
    {
        string spellString = "";
        spellString += spellscroll[0];
        for (int i=1; i<spellscroll.Count; i++)
        {
            spellString += " + " + spellscroll[i];
        }
        spells.transform.GetChild(index).GetChild(0).GetComponent<Text>().text = spellString;
    }

    void MaybeChangeSpell()
    {
        int k = Random.Range(0, 10);
        if (k < 3)
        {
            for (int i=0; i<spellNumber; i++)
            {
                int z = Random.Range(0, 2);
                if (z == 0)
                {
                    ChangeSpell(i);
                }
            }
        }
    }

    void ChangeSpell(int ix)
    {
        int k = Random.Range(0, 10);  //70 | 30
        int size;
        size = (k < 3) ? 2 : 3;
        string chant = "";
        List<char> spellscroll = new List<char>(size);
        for (int j=0; j<size; j++)
        {
            int z = Random.Range(0, 3);
            switch(z)
            {
            case 0:
                spellscroll.Add('Q');
                chant += "Q";
                break;
            case 1:
                spellscroll.Add('W');
                chant += "W";
                break;
            case 2:
                spellscroll.Add('E');
                chant += "E";
                break;
            }
        }
        spellChorus[ix] = chant;
        spellBook[ix] = spellscroll;
        PrintScroll(spellBook[ix], ix);
    }

    void MaybeHideSpell()
    {
        for (int i=0; i<spellNumber; i++)
        {
            int z = Random.Range(0, 3);
            if (z == 0)
            {
                HideSpell(i);
            }
        }
        Invoke("ShowSpells", 4f);
    }

    void HideSpell(int ix)
    {
        spells.transform.GetChild(ix).gameObject.SetActive(false);
    }

    void ShowSpells()
    {
        for (int i=0; i<spellNumber; i++)
        {
            spells.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void CheckIfValidSpell(string spellChant)
    {
        for (int i=0; i<spellNumber; i++)
        {
            if (spellChant == spellChorus[i])
            {
                CastSpell(i);
            }
        }
    }

    void CastSpell(int ix)
    {
        if (!GetComponent<ManaManagment>().CastManaSpell(spellCosts[ix]))
        {
            return;
        }
        bool s = false;
        switch(ix)
        {
        case 0:
            s = nocturne.GetComponent<M_Nocturne>().resetNocturne();
            break;
        case 1:
            s = raum.GetComponent<M_Raum>().resetRaum();
            break;
        }
        if (!s)
        {
            return;
        }

        int z = Random.Range(0, 6);
        spellSounds.transform.GetChild(Random.Range(0, spellSounds.transform.childCount)).GetComponent<AudioSource>().Play();
        
        if (z%2 == 0)
        {
            castSounds.transform.GetChild(Random.Range(0, castSounds.transform.childCount)).GetComponent<AudioSource>().Play();
        }
    }
}
