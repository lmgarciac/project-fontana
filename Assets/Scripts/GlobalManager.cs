using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager _instance;

    // Public property to access the singleton instance
    public static GlobalManager Instance
    {
        get
        {
            // If the instance hasn't been set yet, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<GlobalManager>();

                // If it's still null, create a new GameObject and attach the singleton script to it
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("GlobalManager");
                    _instance = managerObject.AddComponent<GlobalManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        // Ensure there's only one instance of the singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
