﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class urlbutton : MonoBehaviour
{

    public string url; 

    public void Open()
    {
        Application.OpenURL(url);
        Debug.Log("openHomepage");
    }

}