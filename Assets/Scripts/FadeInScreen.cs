using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInScreen : MonoBehaviour
{
    [SerializeField] private float fadeInTime = 5f;
    private float fadeInTimeLeft;
    [SerializeField] private float maxOpacity = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (Player.player != null)
        {
            Player.player.CaughtEvent += Activate;
        }
    }

    public void Activate()
    {
        fadeInTimeLeft = fadeInTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeInTimeLeft > 0)
        {
            fadeInTimeLeft -= Time.deltaTime;
            Color c = GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(c.r, c.g, c.b, maxOpacity * (fadeInTime - fadeInTimeLeft) / fadeInTime);
            if (fadeInTimeLeft < 0)
            {
                fadeInTimeLeft = 0;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }
}
