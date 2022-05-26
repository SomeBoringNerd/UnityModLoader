using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Reflection;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Loader
{
    public class Loader : MonoBehaviour
    {
        // one-time use variables
        // to be more precise :
        // instantiated : if an instance of the class was created
        // hooked : if the loader was successfully loaded
        static bool instantiated = false;
        static bool hooked = false;
        
        /// <summary>
        /// Method to invoke in order to launch the ModLoader
        /// </summary>
        public static void Hook()
        {
            if (hooked) return;
            
            hooked = true;
            SceneManager.sceneLoaded += SceneLoaded;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        /// <summary>
        /// Launched every time a scene is loaded
        /// </summary>
        public static void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (instantiated && scene.name == string.Empty) return;
            
            GameObject go = new GameObject("ModLoader");
            Loader point = go.AddComponent<Loader>();
            DontDestroyOnLoad(go);
            
            
            // initialize the logger and pass to it the config file
            if(point.gameObject.GetComponent<logger>() == null){
                point.gameObject.AddComponent<logger>();
            }
            
            point.Load();
        }
        
        /// <summary>
        ///     In short, the main part of the loader.
        /// </summary>
        public void Load()
        {
            // we dont want to load each mod each time a scene is loaded
            if (instantiated) return;
            
            // we only want to attempt to load a dll if the mod folder exist because, well, else, it would be empty.
            string path = "";

            path = (Application.platform == RuntimePlatform.WindowsPlayer ||
                    Application.platform == RuntimePlatform.WindowsEditor)
                ? Application.streamingAssetsPath + "\\Mods"
                : Application.streamingAssetsPath + "/Mods";
            
            if(Directory.Exists(path))
            {
                // get, in theory, every possible mod in the mods folder
                string[] mods = Directory.GetDirectories(path);
                
                foreach (string mod in mods)
                {
                    Debug.Log("mod found : " + Path.GetFileName(mod) + "/" + Path.GetFileName(mod) + ".dll");
                    LoadDLL(Path.GetFileName(mod));
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch(Exception e)
                {
                    Debug.Log("couldn't create mod folder : " + e);
                }
            }
            
            instantiated = true;
        }
        
        /// <summary>
        /// Attempt to load a dll file into the game if it's valid
        /// </summary>
        /// <param name="DllName">Name of the mod / DLL</param>
        void LoadDLL(string DllName)
        {
            
            string path = "";
            path = (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) 
                ? Application.streamingAssetsPath + "\\Mods" + "\\" + DllName + "\\" + DllName + ".dll"
                : Application.streamingAssetsPath + "/Mods" + "/" + DllName + "/" + DllName + ".dll";
            
            try
            {
                Assembly dll = Assembly.LoadFile(path);
                
                Type[] entry = dll.GetExportedTypes();

                foreach (Type type in entry)
                {
                    if (type.Namespace == "Loader" && type.Name == DllName)
                    {
                        type.GetMethod("Init").Invoke(this, null);
                    }
                }

            }
            catch (Exception e)
            {
                Debug.LogError("Couldn't load " + DllName + " for the reason : " + e);
            }
        }
    }
}
