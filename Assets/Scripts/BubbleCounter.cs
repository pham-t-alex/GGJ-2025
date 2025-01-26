using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleCounter : MonoBehaviour
{
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        Player.player.BubbleCountEvent += UpdateText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(int bubbleCount)
    {
        this.text.text = bubbleCount.ToString();
    }
}
