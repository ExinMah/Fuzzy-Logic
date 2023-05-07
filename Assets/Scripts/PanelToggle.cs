using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
{
    public Image panel;
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isActive)
            {
                panel.gameObject.SetActive(true);
                isActive = true;
            }
            else
            {
                panel.gameObject.SetActive(false);
                isActive = false;
            }
        }
    }
}
