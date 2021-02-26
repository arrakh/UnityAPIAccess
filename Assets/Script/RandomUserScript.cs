using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;

public class RandomUserScript : MonoBehaviour
{
    Dictionary<int, UserData> dataTable = new Dictionary<int, UserData>();

    [Header("Important Fields")]
    public string link;
    public List<string> randomGreeting;
    public List<Sprite> sprites;

    [Header("UI Stuff")]
    public TMP_Text textbox;
    public TMP_Text errorMessage;
    public GameObject loadingThrobber;
    public Image userImage;
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        GenerateData();
    }

    void GenerateData()
    {
        WWW jsonReq = new WWW(link);
        StartCoroutine(ProcessUserRequest(jsonReq));
    }

    private IEnumerator ProcessUserRequest(WWW req)
    {
        textbox.text = string.Empty;
        loadingThrobber.SetActive(true);

        yield return req;

        loadingThrobber.SetActive(false);

        textbox.text = "Generating Data...";

        JSONArray dataArr = JSON.Parse(req.text).AsArray;

        foreach (JSONNode node in dataArr)
        {
            UserData data = new UserData(node["name"], node["email"], sprites[UnityEngine.Random.Range(0, sprites.Count - 1)]);
            dataTable.Add(node["id"].AsInt, data);
        }

        SetRandomDisplay();
    }

    private void SetUserDisplay(UserData toDisplay)
    {
        //UserData toDisplay;

        //if (!dataTable.TryGetValue(id, out toDisplay))
        //{
        //    errorMessage.text = "Requested id does not exist!";
        //    return;
        //}

        userImage.sprite = toDisplay.image;

        textbox.text = 
            $"Hi! My name is <b>{toDisplay.name}</b>!\n\n" +
            $"My email is <b>{toDisplay.email}</b>\n\n" +
            RandomGreeting;
    }

    private string RandomGreeting => randomGreeting[UnityEngine.Random.Range(0, randomGreeting.Count - 1)];

    public void SetDisplayToInput()
    {
        errorMessage.text = string.Empty;

        int id;
        UserData display;

        if(!Int32.TryParse(inputField.text, out id))
        {
            errorMessage.text = "Input is NOT a number";
            return;
        }

        if (!dataTable.TryGetValue(id, out display))
        {
            errorMessage.text = "Requested id does not exist!";
            return;
        }

        SetUserDisplay(display);
    }

    public void SetRandomDisplay()
    {
        SetUserDisplay(dataTable.ElementAt(UnityEngine.Random.Range(0, dataTable.Count - 1)).Value);
    }
}

public class UserData
{
    public string name;
    public string email;
    public Sprite image;

    public UserData(string name, string email, Sprite image)
    {
        this.name = name;
        this.email = email;
        this.image = image;
    }
}
