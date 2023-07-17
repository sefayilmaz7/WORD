using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button typeOfButton;
    public Button similiarToButton;
    public Button UsafeOfButton;
    public Button InRegionButton;
    public Button InstanceOfButton;
    public Button PartOfButton;

    public GameObject typeOf;
    public GameObject similiarTo;
    public GameObject usageOf;
    public GameObject inRegion;
    public GameObject instanceOf;
    public GameObject partOf;

    [Header("Splash Screen")] 
    public GameObject splashScreen;
    public Image fillProgressBar;

    private void Awake()
    {
        typeOfButton.onClick.AddListener(TYPE_OF_CALLBACK);
        similiarToButton.onClick.AddListener(SIMILIAR_TO_CALLBACK);
        UsafeOfButton.onClick.AddListener(USAGE_OF_CALLBACK);
        InRegionButton.onClick.AddListener(IN_REGION_CALLBACK);
        InstanceOfButton.onClick.AddListener(INSTANCE_OF_CALLBACK);
        PartOfButton.onClick.AddListener(PART_OF_CALLBACK);
    }

    private void Start()
    {
        CloseSplashScreen();
    }

    public void TYPE_OF_CALLBACK()
    {
        typeOf.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void SIMILIAR_TO_CALLBACK()
    {
        similiarTo.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void USAGE_OF_CALLBACK()
    {
        usageOf.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void IN_REGION_CALLBACK()
    {
        inRegion.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void INSTANCE_OF_CALLBACK()
    {
        instanceOf.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void PART_OF_CALLBACK()
    {
        partOf.SetActive(true);
        gameObject.SetActive(false);
    }
    
    private bool CheckConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {

            return false;
        }
        
        return true;
    }

    private void CloseSplashScreen()
    {
        if (CheckConnection())
        {
            fillProgressBar.DOFillAmount(1, 1).OnComplete(() =>
            {
                splashScreen.GetComponent<Image>().DOFade(0, 0.3f).OnComplete(() => splashScreen.SetActive(false));
            });
        }
    }
}
