﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa reprezentujaca element laczeniowy (wezel, staw),
// wzgledem ktorego wykonywany jest obrot
public class Joint : MonoBehaviour
{
    // wektor definijacy poczatkowe odsuniecie wezla od celu
    public Vector3 startOffset;

    // minimalna wartosc obrotu
    public float min;

    // maksymalna wartosc obrotu
    public float max;

    // wektor reprezentujacy os obrotu, np [1, 0, 0]
    private Vector3 axis;

    public Joint(Vector3 axis)
    {
        this.axis = axis;
    }
 
    void Awake ()
    {
        // obliczenie odsuniecia wezla od celu
        startOffset = transform.localPosition;
    }

    // funkcja zwracajaca wektor po ktorym porusza sie os
    public Vector3 getAxis()
    {
        return axis;
    }

    // funkcja zwracajaca aktualny kat dla wezla zaleznie od osi obrotu
    public float getAngle()
    {
        // jesli wektor obrotu to [1, 0, 0] (os X)
        if(axis == Vector3.right)
            return transform.localEulerAngles.x;
        // jesli wektor obrotu to [0, 1, 0] (os Y)
        else if (axis == Vector3.up)
            return transform.localEulerAngles.y;
        // jesli wektor obrotu to [0, 0, 1] (os Z)
        else 
            return transform.localEulerAngles.z;
    }

    // funkcja ustawiajaca wartosc obrotu na podstawie podanej wartosci i ustalonej osi obrotu
    public void setAngle(float angle)
    {
        // jesli wektor obrotu to [1, 0, 0] (os X)
        if(axis == Vector3.right)
            transform.localEulerAngles = new Vector3(angle, 0, 0);
        // jesli wektor obrotu to [0, 1, 0] (os Y)
        else if (axis == Vector3.up)
            transform.localEulerAngles = new Vector3(0, angle, 0);
        // jesli wektor obrotu to [0, 0, 1] (os Z)
        else transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}