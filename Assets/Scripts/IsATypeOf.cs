using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
//Finds word that are more general than the given word.
public class IsATypeOf : MonoBehaviour
{
    public TMP_InputField Input;
    public TMP_Text Response;
    public Button Go_BUTTON;

    public GameObject mainMenuButtons; 
    public Button closeButton;

    void CLOSE_BUTTON_CALLBACK()
    {
        mainMenuButtons.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        closeButton.onClick.AddListener(CLOSE_BUTTON_CALLBACK);
        Go_BUTTON.onClick.AddListener(GO_BUTTON_CALLBACK);
    }

    public void GO_BUTTON_CALLBACK()
    {
        if (string.IsNullOrEmpty(Input.text))
        {
            Response.color = Color.red;
            Response.text = "Please type a valid word!";
            return;
        }
        
        StartCoroutine(GenerateNewResult(Input.text));
    } 

    IEnumerator GenerateNewResult(string word)
    {
        using (var request = UnityWebRequest.Get("https://wordsapiv1.p.rapidapi.com/words/"+ word +"/typeOf"))
        {
            request.SetRequestHeader("X-RapidAPI-Key", "c30eba9734mshc392d4470531fc0p1f30e3jsn4af52a782bc2");
            request.SetRequestHeader("X-RapidAPI-Host", "wordsapiv1.p.rapidapi.com");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var r = request.downloadHandler.text;
                var parsedData = ParseJson(r);
                

                Response.text =  string.Join(", ", parsedData.Value);
                Response.color = Color.white;
            }
        }
    }
    
    KeyValuePair<string, List<string>> ParseJson(string json)
    {
        var jsonData = JsonUtility.FromJson<WordData>(json);
        var keyValue = new KeyValuePair<string, List<string>>(jsonData.word, jsonData.typeOf.ToList());
        if (jsonData.typeOf.Length == 0)
        {
            Response.text = "There Were No Results!";
            Response.color = Color.red;
        }
        return keyValue;
    }
    



}


[System.Serializable]
public class WordData
{
    public string word;
    public string[] typeOf;
    public string[] inRegion;
    public string[] similarTo;
    public string[] usageOf;
    public string[] instanceOf;
    public string[] partOf;
}
