﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// klasa reprezentujaca menadzer programu
// obsluguje ona glowne przyciski dolnego menu
public class Manager : MonoBehaviour
{
    // obiekt typu Button sluzacy do restartu aplikacji
    public Button restartBtn;

    // obiekt typu Button sluzacy do resetowania pozycji wszystkich osi
    public Button resetBtn;

    // obiekt typu Button sluzacy do wlaczenia/wylaczenia trybu inverse kinematics
    public Button inverseBtn;

    // obiekt typu Button sluzacy pokazywania i chowania menu bocznych
    public Button showHideBtn;

    // obiekt typu Button sluzacy do wylaczenia aplikacji
    public Button exitBtn;

    // obiekt reprezentujacy lewe i prawe menu
    public GameObject content;

    // tablica osi sluzaca m.in do resetu pozycji wszystkich osi
    public Axis[] axes = new Axis[6];

    // obiekt obslugujacy logike kinematyki odwrotnej
    public Kinematics ik;

    // panel sterujacy kinematyki odwrotnej
    public GameObject ikPanel;
    
    // zmienna okreslajaca czy boczne menu sa aktualnie widoczne
    private bool visibleSides = true;

    // zmienna okreslajaca czy kinematyka odwrotna jest aktywna
    private bool inverseEnabled = false;

    // funkcja jest uruchamiana przed pierwszym update'm
    void Start()
    {
        // dodanie funkcji obslugujacych klikniecia poszegolnych przyciskow
        restartBtn.onClick.AddListener(restart);
        resetBtn.onClick.AddListener(resetPositions);
        inverseBtn.onClick.AddListener(inverse);
        showHideBtn.onClick.AddListener(showHide);
        exitBtn.onClick.AddListener(exit);
    }

    // funkcja obslugujaca klikniecie przycisku restart
    private void restart() 
    {
        // zaladowanie aktualnej sceny od nowa
        SceneManager.LoadScene("Main");
    }

    // funkcja obslugujaca klikniecie przycisku inverse
    private void inverse() 
    {
        inverseEnabled = !inverseEnabled;

        if(inverseEnabled)
        {
            // zmiana tekstu przycisku
            inverseBtn.GetComponentInChildren<Text>().text = "EXHIBITION";

            // deaktywacja paneli bocznych
            content.SetActive(false);

            // aktywacja panelu kinematyki odwrotnej
            ikPanel.SetActive(true);
            
            // aktywacja kinematyki odwrotnej
            ik.gameObject.SetActive(true);
            
            resetPositions();
        }
        else
        {
            // zmiana tekstu przycisku
            inverseBtn.GetComponentInChildren<Text>().text = "IK MOVE";

            // deaktywacja panelu kinematyki odwrotnej
            ikPanel.SetActive(false);

            // aktywacja paneli bocznych
            content.SetActive(true);

            // deaktywacja kinematyki odwrotnej
            ik.gameObject.SetActive(false);

            resetPositions();
        }
    }

    // funkcja obslugujaca klikniecie przycisku showHide
    private void showHide() 
    {
        // jesli kinematyka odwrotna jest wlaczona to button jest nieaktywny
        if(ik.gameObject.activeInHierarchy)
            return;

        // negacja wartosci binarnej
        visibleSides = !visibleSides;

        // aktywacja badz deaktywacja paneli bocznych w zaleznosci od wartosci zmiennej visibleSides
        content.SetActive(visibleSides);

        // zmiana tekstu przycisku w zaleznosci od wartosci zmiennej
        // jesli visibleSides jest true (panele boczne sa widoczne) to tekst to HIDE (mozliwosc schowania paneli)
        // jestli visibleSide jest false (panele boczne nie sa widoczne) to tekst to SHOW (mozliwosc pokazania paneli)
        showHideBtn.GetComponentInChildren<Text>().text = visibleSides ? "HIDE" : "SHOW";
    }

    // funkcja obslugujaca klikniecie przycisku exit
    private void exit()
    {
        SceneManager.LoadScene("Credits");
    }

    // funkcja resetuje wszystkie osi
    private void resetPositions()
    {
        if(ik.gameObject.activeInHierarchy)
            ik.target.reset();
        
        for(int i = 0; i < axes.Length; i++)
            axes[i].moveTo(0f);
    }
}
