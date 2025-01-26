using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string preSunRoofScene;
    [SerializeField] private string postSunRoofScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (PersistentData.Instance.postSunRoof)
            {
                SceneManager.LoadScene(postSunRoofScene);
            }
            else
            {
                SceneManager.LoadScene(preSunRoofScene);
            }
        }
    }
}
