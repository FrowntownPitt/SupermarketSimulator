using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager _instance;

    private void Start()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        if(this != _instance)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(_instance);


    }
}
