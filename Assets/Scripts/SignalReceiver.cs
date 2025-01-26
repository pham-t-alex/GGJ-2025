using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignalReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishSunRoof()
    {
        PersistentData.Instance.postSunRoof = true;
        SceneManager.LoadScene("After Sun Roof");
    }
}
