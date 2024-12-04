using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager InstanceMainCanvas { get; private set; }

    private void Awake()
    {
        if (InstanceMainCanvas != null && InstanceMainCanvas != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceMainCanvas = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(ResetLevel());
    }
    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(1f);
        DataManger.InstanceData.ApplyCoin();
        DataManger.InstanceData.ApplyTextLevel();
    }
}
