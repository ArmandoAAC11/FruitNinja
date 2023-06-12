using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public int maxNumberOfError;
    public int currentNumberOfError = 0;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            currentNumberOfError++;
            if(currentNumberOfError == maxNumberOfError)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }

            Destroy(collision.gameObject);
        }
    }
}
