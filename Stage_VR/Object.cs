using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    //콘솔, 밴드 와 같은 object 활성화
    [SerializeField]
    GameObject[] gameObjects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void active(int num) {
        gameObjects[num-1].SetActive(true);
    }
}
