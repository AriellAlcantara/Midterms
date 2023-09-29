using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSample : MonoBehaviour
{
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 48;

        GUI.Label(new Rect(10, 0, 0, 0), "Rotating on X" + x + "Y:" + y + "Z:" + z, style);


        GUI.Label(new Rect(10, 35, 0, 0), "CurrentEulerAngles:", style);


        GUI.Label(new Rect(10, 35, 0, 0), "World Euler Angles:", style);

    }




}
