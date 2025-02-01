using UnityEngine;

public class FadeInFlag : MonoBehaviour
{
  [SerializeField] private ScreenFader screenFader;

    private void Start()
    {
        screenFader = FindAnyObjectByType<ScreenFader>();
        screenFader.FadeIn();
    }
}
