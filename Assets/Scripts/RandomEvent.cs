using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvent : MonoBehaviour
{
    [SerializeField] List<Sprite> randomItems;
    [SerializeField] TMP_Text randomItemName;
    [SerializeField] GameObject panel;

    [SerializeField] EventManagerSO eventManager;

    private int randomIndex;
    private Sprite item;


    private void OnEnable()
    {
        eventManager.onRandomEvent += Randomize;
    }

    private void OnDisable()
    {
        eventManager.onRandomEvent -= Randomize;
    }
    public void Randomize()
    {
        Debug.Log($"randomizing...");
        randomIndex = Random.Range(0, randomItems.Count);
        item = randomItems[randomIndex];
    
        panel.GetComponent<Image>().sprite = item;
        randomItemName.text = "You get..." + item.name;
    }
}
