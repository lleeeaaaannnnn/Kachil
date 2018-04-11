using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    Text loadingText;
    bool fading;
    void Start()
    {
        fading = true;
        loadingText = GetComponentInChildren<Text>();
        StartCoroutine(Fade());
    }
    void OnDisable()
    {
        fading = false;
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(.5f);

        while (fading)
        {
            yield return new WaitForSeconds(.5f);
            loadingText.color = Color.Lerp(Color.clear, Color.white, .1f);
        }
    }
}
