using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject[] lights;

    Color lightColor;
    float duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        turnOnOff(false);
    }

    void turnOnOff(bool status) {
        foreach (GameObject light in lights)
        {
            light.transform.GetChild(0).GetComponent<Light>().enabled = status;
        }
    }

    public void LightControl(string color) {
        turnOnOff(true);

        Color preColor = lights[0].transform.GetChild(0).GetComponent<Light>().color;

        switch (color)
        {
            case "Red" :
                lightColor = Color.red;
                break;
            case "Yellow" :
                lightColor = Color.yellow;
                break;
            case "Blue" :
                lightColor = Color.blue;
                break;
            case "Green" :
                lightColor = Color.green;
                break;
        }

        foreach(GameObject light in lights)
        {
            //light.GetComponent<Animator>().SetTrigger("TurnOn");
            Light mLight = light.transform.GetChild(0).GetComponent<Light>();
            mLight.color = lightColor;
            mLight.intensity = 5.0f;
        }
    }
}
