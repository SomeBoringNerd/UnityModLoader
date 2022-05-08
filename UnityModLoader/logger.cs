using System;
using System.Collections.Generic;
using UnityEngine;

public class logger : MonoBehaviour
{
    public void Awake()
    {
        messages = new List<string>();
            
        logStyle = new GUIStyle();
        logStyle.normal.textColor = Color.blue;
            
        Application.logMessageReceived += LogMessageReceived;
        Debug.Log("[ModTemplate] : Ez Logger V1 was loaded");
    }


    /// <summary>
    ///     Simple way to log the game into a log.txt file
    /// </summary>
    public void LogMessageReceived(string message, string stackTrace, LogType type)
    {
        bool flag = (long)messages.Count >= 25L;
        if (flag)
        {
            this.messages.RemoveAt(0);
        }
            
        messages.Add("[" +type + "] : " + message);
        text = string.Empty;
        foreach (string text in messages)
        {
            this.text = this.text + text + "\n";
        }
    }
    // logging code //
        
    private const uint MAX_MESSAGES = 25U;
        
    private List<string> messages;
    private GUIStyle logStyle;
    private string text;
        
    private Rect rect;
        
    public void OnGUI()
    {
        rect.position = new Vector2(2f, 2f);
        rect.size = new Vector2(200f, 600f);
        GUI.Label(rect, text, logStyle);
    }
}