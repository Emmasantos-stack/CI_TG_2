using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Guarda o tempo final
    private static int seconds;

    // Guarda os segundos
    public static void SetSeconds(int newSeconds)
    {
        seconds = newSeconds;
    }

    // Vai buscar os segundos
    public static int GetSeconds()
    {
        return seconds;
    }

    // Faz reset ao tempo
    public static void Reset()
    {
        seconds = 0;
    }
}