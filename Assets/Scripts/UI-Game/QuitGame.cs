using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Start() { Invoke(nameof(ExitGame), 3f); }

    private void ExitGame() { Application.Quit(); }
}
