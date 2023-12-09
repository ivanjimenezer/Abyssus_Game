using UnityEngine;
using UnityEngine.SceneManagement;

public class getBack : MonoBehaviour
{
    public bool isDead;
    public void Update ()
    {   if(isDead)
        {// Check for a touch on the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            // Load the Menu scene
            SceneManager.LoadScene("menu"); // Replace "MenuScene" with the actual name of your menu scene
        }}
        isDead = false;
    }
}
