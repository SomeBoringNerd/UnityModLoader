using System;
using System.Collections.Generic;
using System.IO;
using Loader;
using UnityEngine;

public class logger : MonoBehaviour
{
    private bool log = true;

    public ConfigUtil config;
    public static int instances = 0;
    
    public void Start()
    {
        instances++;

        if (instances != 1)
        {
            this.enabled = false;
        }else
        {
        
            messages = new List<string>();
            
            Application.logMessageReceived += LogMessageReceived;
            Debug.Log("[ModLoader] : SimpleLogSystem was loaded");

            config = new ConfigUtil();
            
            
            logStyle = new GUIStyle();
            
            logStyle.normal.textColor = config.getColorFromConfig(config.getString(Settings.logging_color));
            log = config.getBool(Settings.logging);
            
            Debug.Log("Loaded Logger instance " + instances);
            
            // used for debugging, please ignore.
            //base.gameObject.AddComponent<test>();
            //base.gameObject.GetComponent<test>().log = this;
        
        }
    }


    /// <summary>
    ///     Simple way to log the game on the screen
    /// </summary>
    public void LogMessageReceived(string message, string stackTrace, LogType type)
    {
        if (messages.Count >= max_msg)
        {
            this.messages.RemoveAt(0);
        }
            
        messages.Add("[" + type + "] : " + message);
        
        text = string.Empty;
        
        foreach (string text in messages)
        {
            this.text = this.text + text + "\n";
        }
    }
    
    // max message that can be seen on the screen at once (25 by default)
    private int max_msg = 35;
    
    // getting logs as string vars
    private List<string> messages;
    private string text;
    
    // GUI related vars
    private GUIStyle logStyle;
    private Rect rect;
        
    public void OnGUI()
    {
        if (!log) return;
        
        rect.position = new Vector2(2f, 2f);
        rect.size = new Vector2(500f, 768f);
        GUI.Label(rect, text, logStyle);
    }
}

/// <summary>
///     Simple class for debugging purpose
/// </summary>
public class test : MonoBehaviour
{

    public logger log;
    
    private void Start()
    {
        foreach (KeyValuePair<string, string> entry in log.config.config.entries)
        {
            Debug.Log(entry.Key + " : " + entry.Value);
        }
    }
}