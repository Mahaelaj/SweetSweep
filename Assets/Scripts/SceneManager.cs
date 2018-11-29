using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Text livesText;
    [SerializeField] int lives = 5;
    public GameObject[] sweets;
    public static SceneManager instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a SceneManager.
            Destroy(gameObject);
    }

    private void Start()
    {
        sweets = GameObject.FindGameObjectsWithTag("Sweet");
    }


    public void loseLife()
    {
        lives--;
        livesText.text = lives.ToString();

        if (lives <= 0) loseLevel();
    }

    void loseLevel()
    {

    }
}
