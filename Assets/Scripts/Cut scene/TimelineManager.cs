using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class TilelineManager : MonoBehaviour
{
    private bool fix = false;
    public Animator playerAnimator;
    public RuntimeAnimatorController playerAnim;
    public PlayableDirector director;

    // Start is called before the first frame update
    void OnEnable()
    {
        playerAnim = playerAnimator.runtimeAnimatorController;
        playerAnimator.runtimeAnimatorController = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (fix)
        {
            SceneManager.LoadScene("Level1");
        }
        if(director.state != PlayState.Playing)
        {
            fix = true;
            playerAnimator.runtimeAnimatorController = playerAnim;
           
        }
    }
}
