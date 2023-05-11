using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    public TextMeshProUGUI speed;
    public TextMeshProUGUI distance;
    public TextMeshProUGUI lifetime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "AI Car")
                {
                    speed.text = "Speed: " + transform.GetComponent<AICarMove>().speed;
                    distance.text = "Distance: " + transform.GetComponent<AICarMove>().distance;
                    lifetime.text = "Lifetime: " + AICarMove.lifetime;
                }
            }
        }
    }
}
