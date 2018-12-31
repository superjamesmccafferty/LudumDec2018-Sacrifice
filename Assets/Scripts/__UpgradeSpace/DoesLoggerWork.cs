using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesLoggerWork : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Logger.Info("Does this work");
    }

    private void OnApplicationQuit()
    {
        Logger.Instance.Dispose();
    }
}
