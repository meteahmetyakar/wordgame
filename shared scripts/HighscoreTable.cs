using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HighscoreTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores != null)
        {

            /*if(highscores == null)      
            {
                AddHighscoreEntry(10000, "Test1");
                AddHighscoreEntry(9999, "Test2");
                AddHighscoreEntry(8999, "Test3");
                AddHighscoreEntry(7929, "Test4");
                AddHighscoreEntry(6999, "Test5");
                AddHighscoreEntry(5999, "Test6");
                AddHighscoreEntry(4999, "Test7");
                AddHighscoreEntry(3999, "Test8");
                AddHighscoreEntry(2999, "Test9");
                AddHighscoreEntry(1999, "Test10");
                jsonString = PlayerPrefs.GetString("highscoreTable");
                highscores = JsonUtility.FromJson<Highscores>(jsonString);
            }*/

            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)       //sorting
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                    {

                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }

            highscoreEntryTransformList = new List<Transform>();
            if (highscores.highscoreEntryList.Count > 10)   //This block works if the list has more than 10 data.
            {
                for (int i = 0; i < 10; i++)    // Creates 10 templates.
                {
                    CreateHighscoreEntryTransform(highscores.highscoreEntryList[i], entryContainer, highscoreEntryTransformList);
                }
            }
            else
            {
                foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) // creates as many templates as in the list.
                {
                    CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
                }
            }
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 69f; //Distance between templates.

        Transform entryTransform = Instantiate(entryTemplate, container);   // Creates a template clone.
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
       
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);    // Setting the position of the template.

        entryTransform.gameObject.SetActive(true);

        int score = highscoreEntry.score;   //Score is taken from memory and printed.
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;  //Name is taken from memory and printed.
        entryTransform.Find("nameText").GetComponent<Text>().text = name;


        transformList.Add(entryTransform);
    }

    public static void AddHighscoreEntry(int score, string name)   //adds the score and name parameters to the list.
    {
    
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };  

  
        string jsonString = PlayerPrefs.GetString("highscoreTable");            // We pull our Nick and score list from memory.
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);   // We create a Highscores object from the json representation.

        if (highscores == null) //if list is null
        {
          
            highscores = new Highscores()   //we create list
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }


        highscores.highscoreEntryList.Add(highscoreEntry);  //We print the parameters from the outside to our list.


        string json = JsonUtility.ToJson(highscores);       // We convert our object to json.
        PlayerPrefs.SetString("highscoreTable", json);      //We are storing the json we converted.
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

}
