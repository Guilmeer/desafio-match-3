using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gazeus.DesafioMatch3
{
    public class ChangeGameScene : MonoBehaviour
    {
        public void LoadNewScene(int index)
        {
            SceneManager.LoadScene(index); // Gameplay scene is 1 
        }
    }
}
