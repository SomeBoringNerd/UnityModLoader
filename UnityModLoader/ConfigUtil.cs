using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


// i hate that class

namespace Loader
{
    public class ConfigUtil
    {
        public ConfigFile config;
        
        private string result;
        
        public ConfigUtil()
        {
            config = new ConfigFile();
        }
        
        /// <summary>
        ///     Return the value of a key in the config file as a string
        /// </summary>
        /// <param name="EntryName">Name of the key</param>
        /// <returns>the value of the key</returns>
        /// <exception cref="Exception">if the key provided is incorrect.</exception>
        public string getString(string EntryName)
        {
            if (config.entries.ContainsKey(EntryName))
            {
                foreach (KeyValuePair<string, string> entry in config.entries)
                {
                    if (entry.Key == EntryName)
                    {
                        result = entry.Value;
                    }
                }
            }

            return result;
        } 
        
        /// <summary>
        ///     Return the value of a key in the config file as a boolean
        /// </summary>
        /// <param name="EntryName">Name of the key</param>
        /// <returns>the value of the key</returns>
        /// <exception cref="Exception">if the key provided is incorrect.</exception>
        public bool getBool(string EntryName)
        {
            if (config.entries.ContainsKey(EntryName))
            {
                foreach (KeyValuePair<string, string> entry in config.entries)
                {
                    if (entry.Key == EntryName)
                    {
                        result = entry.Value;
                    }
                }
            }

            return result.Replace(" ", "") == "true";
        }
        
        /// <summary>
        ///     Return the value of a key in the config file as a float
        /// </summary>
        /// <param name="EntryName">Name of the key</param>
        /// <returns>the value of the key</returns>
        /// <exception cref="Exception">if the key provided is incorrect.</exception>
        public float getFloat(string EntryName)
        {
            if (config.entries.ContainsKey(EntryName))
            {
                foreach (KeyValuePair<string, string> entry in config.entries)
                {
                    if (entry.Key == EntryName)
                    {
                        result = entry.Value;
                    }
                }
            }

            float a;
            bool res = float.TryParse(result, out a);
                
            return a;
        } 
        
        /// <summary>
        ///     Return the value of a key in the config file as an integer
        /// </summary>
        /// <param name="EntryName">Name of the key</param>
        /// <returns>the value of the key</returns>
        /// <exception cref="Exception">if the key provided is incorrect.</exception>
        public int getInt(string EntryName)
        {
            if (config.entries.ContainsKey(EntryName))
            {
                foreach (KeyValuePair<string, string> entry in config.entries)
                {
                    if (entry.Key == EntryName)
                    {
                        result = entry.Value;
                    }
                }
            }

            int a;
                
            bool res = int.TryParse(result, out a);
                
            return a;
        } 
        
        /// <summary>
        /// Get a color from a string
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Color getColorFromConfig(string color)
        {
            Color color_tmp = new Color();

            color = color.ToLower(CultureInfo.CurrentCulture);
            color = color.Replace(" ", "");
            
            switch (color)
            {
                case "red":
                    color_tmp = Color.red;
                    break;
                case "green":
                    color_tmp = Color.green;
                    break;
                case "blue":
                    color_tmp = Color.blue;
                    break;
                case "white":
                    color_tmp = Color.white;
                    break;
                case "black":
                    color_tmp = Color.black;
                    break;
                case "yellow":
                    color_tmp = Color.yellow;
                    break;
                case "cyan":
                    color_tmp = Color.cyan;
                    break;
                case "magenta":
                    color_tmp = Color.magenta;
                    break;
                case "gray":
                    color_tmp = Color.gray;
                    break;
                case "grey":
                    color_tmp = Color.grey;
                    break;
                case "clear":
                    color_tmp = Color.clear;
                    break;
            }


            return color_tmp;
        }
    }

    public static class Settings
    {
        public static string mod_name = "mod_name";

        public static string version = "version";
        
        public static string logging_color = "logging_color";
        
        public static string logging = "logging";
        
        public static string max_messages = "max_messages";
    }
}