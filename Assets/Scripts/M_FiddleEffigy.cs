using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_FiddleEffigy : MonoBehaviour
{
    public AudioSource fiddleKill;
    public GameObject fiddleLaughs;
    public GameObject fiddleAttack;

    public bool immortal = false;
    public GameObject effigyHolder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            float dist = Vector3.Distance(mousePosition, transform.position);
            Debug.Log(dist);
            if (dist < 1.3 && !immortal)
            {
                Destroy(effigyHolder);
                Destroy(gameObject);
            }
        }
    }

}
