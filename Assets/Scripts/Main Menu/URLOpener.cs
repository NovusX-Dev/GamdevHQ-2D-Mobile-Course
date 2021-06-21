using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOpener : MonoBehaviour
{
    [SerializeField] private string _linkedInURL, _mediumURL;

    public void OpenLinkedIn()
    {
        Application.OpenURL(_linkedInURL);
    }

    public void OpenMedium()
    {
        Application.OpenURL(_mediumURL);
    }
}
