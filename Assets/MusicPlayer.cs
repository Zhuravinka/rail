using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadFirstScene", 3f);
    }

    // Update is called once per frame
    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
