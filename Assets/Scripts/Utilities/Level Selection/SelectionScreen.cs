using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    string sceneName;

    [SerializeField]
    Image[] stars;



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
        gameObject.SetActive(false);
        SceneLoadingManager.instance.LoadScene(sceneName);
    }


    public void Activate()
    {
        for (int i = 0; i < UserProfileManager.instance.activeProfile.GetStarsForLevel("level1"); i++)
        {
            stars[i].color = Color.white;
        }
    }


}
