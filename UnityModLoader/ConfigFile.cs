using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Loader
{
    public class ConfigFile
    {
        public static string config_path;

        public Dictionary<string, string> entries = new Dictionary<string, string>();
        
        string text_assembled;

        private StreamWriter configWrite;

        public ConfigFile()
        {
            try
            {
                config_path = "";
                config_path = (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                    ? Application.streamingAssetsPath + "\\loader.cfg"
                    : config_path = Application.streamingAssetsPath + "/loader.cfg";
            
                if (!File.Exists(config_path))
                {
                    Debug.Log("Loading Config File for ModLoader");

                    createConfig();
                }
                else
                {
                    StreamReader config = File.OpenText(config_path);
                    config.ReadLine();
                    string[] version = config.ReadLine().Split(' ');
                    config.Close();
                    
                    if (version[2] != "1")
                    {
                        createConfig();
                    }
                    
                    
                }

                refreshConfig();
            }
            catch (Exception e)
            {
                Debug.Log("config file issue");
                Debug.Log(e);
            }
        }
        
        /// <summary>
        ///     create a new config file if it don't exist or the version is outdated
        /// </summary>
        private void createConfig()
        {
            configWrite = File.CreateText(config_path);
                    
            configWrite.WriteLine("mod_name = UnityModLoader");
            configWrite.WriteLine("version = 1");
            configWrite.WriteLine("logging = true");
            configWrite.WriteLine("logging_color = blue");
            configWrite.WriteLine("max_messages = 25");
                
            configWrite.Close();
        }
        
        /// <summary>
        ///     refresh the values of the entries dictionary
        /// </summary>
        private void refreshConfig()
        {
            entries.Clear();
            
            StreamReader config2 = File.OpenText(config_path);
            bool finished = false;
                        
            while (!finished)
            {
                text_assembled = string.Empty;
                string current_line = config2.ReadLine();

                if (string.IsNullOrEmpty(current_line))
                {
                    finished = true;
                }
                else
                {
                    string[] text = current_line.Split(' ');

                    string key = text[0];
                                
                    text[0] = null;
                    text[1] = null;

                    foreach (string text_array in text)
                    {
                        if (text_array != null)
                        {
                            text_assembled += text_array + " ";
                        }
                    }
                                
                    entries.Add(key, text_assembled);
                    Debug.Log("Found key " + key + " with value " + text_assembled);
                }
            }
            config2.Close();
        }
    }
}