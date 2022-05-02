using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Back()
    {
        anim.SetTrigger("FadeOut");
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(sceneName);
    }


}
