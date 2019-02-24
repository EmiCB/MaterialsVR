using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * This script uses the EventTrigger component within Unity as well as the GVR reticle pointer.
 * Attach this script to the object you want to be compatible with gaze input along with an
 * EventTrigger component. Create PointerEnter and PointerExit events and select the appropriate
 * functions.
 */

public class GazeInput : MonoBehaviour
{
    //--- Variables ---
    private float _gazeTime = 2.0f;
    private float _timer;

    private bool _isGazedAt;

    Scene currentScene;

    public HideHandler pcs;
    public Movement_Handler mcs;
    public Rotation_Handler rcs;

    //--- Start ---
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();   //gets the current scene
        _timer = 0.0f;                                  //resets the timer

        pcs = FindObjectOfType<HideHandler>();
        mcs = FindObjectOfType<Movement_Handler>();
        rcs = FindObjectOfType<Rotation_Handler>();
    }

    //--- Main Loop ---
    void Update()
    {
        if (_isGazedAt)
        {
            //activates while the player is selecting an object with the reticle pointer

            _timer += Time.deltaTime;   //starts timer
            if (_timer >= _gazeTime)
            {
                //activates after the object is gazed at for 2 seconds
                PointerGaze();
                _timer = 0.0f;  //resets the timer
            }
        }
    }
    //--- Functions ---
    //- For Event Trigger -
    public void PointerEnter()
    {
        //activates when the reticle pointer is over an object
        _isGazedAt = true;  //player is gazing at an object
    }
    public void PointerExit()
    {
        //activates when the reticle pointer is not over an object
        _isGazedAt = false; //player is not gazing at anything
        _timer = 0.0f;  //resets timer
    }

    // - Internal Actions -
    private void PointerGaze()
    {
        if (currentScene.name == "New_Menu")
        {
            //activates if the player is in the menu scene
            if (this.gameObject.tag == "b")
            {
                //activates if the game object the script is attached to is a button
                objMessage.loadMessage(gameObject.name);
                SceneManager.LoadScene("SPIN6.26");
                objMessage.revolve();
            }
        }
        else if (currentScene.name == "SPIN6.26")
        {
            //activates if the player is in the main app scene
            if (this.gameObject.tag == "b")
            {
                //activates if the game object the script is attached to is a button
                //TODO: probably can make this more efficient later
                if (gameObject.name == "Back")
                {
                    //activates if the button is the back button
                    SceneManager.LoadScene("New_Menu");
                }
                else if (gameObject.name == "Quit")
                {
                    //activates if the button is the quit button
                    //TODO: fix later ?
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #else
                    Application.Quit();
                    #endif
                }
                else if (gameObject.name == "Polyhedral_Controller")
                {
                    //TODO: add in OnPointerClick
                }
                else if (gameObject.name == "Movement_Controller")
                {
                    //TODO: add in OnPointerClick
                }
                else if (gameObject.name == "Rotation_Controller")
                {
                    //TODO: add in OnPointerClick
                }
            }
        }
    }

}
