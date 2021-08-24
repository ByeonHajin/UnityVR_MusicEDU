using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform Seat;
    public GameObject testPanel;
    public GameObject Teleport;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Seat")
        {
            testPanel.SetActive(true);
            Destroy(other.gameObject);
            Teleport.SetActive(false);
        }
    }
}
