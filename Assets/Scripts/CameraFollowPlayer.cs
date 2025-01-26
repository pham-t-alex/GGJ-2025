using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (Player.player != null)
        {
            Vector3 pos = Player.player.transform.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, pos, Time.deltaTime / smoothing);
            smoothPos.z = -10;
            transform.position = smoothPos;
        }
    }
}
