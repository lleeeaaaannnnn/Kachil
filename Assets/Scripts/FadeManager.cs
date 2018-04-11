using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

    public static FadeManager Instance { set; get; }

    public Image levelCompleteImage;
    public Image levelFailedImage;
    public Text levelCompleteText;
    public Text levelFailedText;

    bool isInTransition;
    bool isShowing;
    float transition;
    float duration;
    Image fadeImage;
    Text fadeText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Fade(float duration, bool levelComplete, bool showing)
    {
        isShowing = showing;
        this.duration = duration;
        isInTransition = true;
        transition = 0;
        fadeImage = (levelComplete) ? levelCompleteImage : levelFailedImage;
        fadeText = (levelComplete) ? levelCompleteText : levelFailedText;
        
    }

    void Update ()
    {
        if (!isInTransition)
            return;

        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(Color.clear, Color.black, transition);
        fadeText.color = Color.Lerp(Color.clear, Color.white, transition);

        if (transition >= 1 || transition <= 0)
            isInTransition = false;
    }
}
