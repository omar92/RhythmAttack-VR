using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowUpdatedLeaderBoard : MonoBehaviour
{
    public SortedDictionaryVariable dictionary;
   // public List<GameObject> list;


    public void ShowLeaderboard()
    {
        for (int i = 0; i < Mathf.Min(dictionary.Value.Count, transform.childCount); i++)
        {
            transform.GetChild(i+1).gameObject.SetActive(true);
            transform.GetChild(i + 1).GetComponentsInChildren<Text>()[1].text = dictionary.Value.Keys.ElementAt(i).ToString();
            transform.GetChild(i + 1).GetComponentsInChildren<Text>()[1].text = dictionary.Value.Values.ElementAt(i).ToString();
        }
    }
}
