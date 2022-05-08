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
        static bool instantiated = false;
        static bool hooked = false;
        
        /// <summary>
        /// Method to invoke in order to launch the ModLoader
        /// </summary>
        public static void Hook()
        {
            if (!hooked)
            {
                hooked = true;
                SceneManager.sceneLoaded += SceneLoaded;
            }
        }

        public static void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!instantiated && scene.name != string.Empty)
            {
                instantiated = true;
                GameObject go = new GameObject("ModLoader");
                Loader point = go.AddComponent<Loader>();
                DontDestroyOnLoad(go);
            }

        }
        
        public void Awake()
        {
            base.gameObject.AddComponent<logger>();
            
            // we only want to attempt to load a dll if the mod folder exist because, well, else, it would be empty.
            string path = "";
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                path = Application.streamingAssetsPath + "\\Mods";
            }
            else
            {
                path = Application.streamingAssetsPath + "/Mods";
            }
            
            if(Directory.Exists(path))
            {
                
                
                // get, in theory, every possible mod in the mods folder
                string[] mods = Directory.GetDirectories(path);
                
                foreach (string mod in mods)
                {
                    //Debug.Log(Application.streamingAssetsPath + "\\Mods" + "\\" + mod + "\\" + mod + ".dll");
                    Debug.Log(mod);
                    Debug.Log("possible mod found : " + Path.GetFileName(mod) + "/" + Path.GetFileName(mod) + ".dll");
                    LoadDLL(Path.GetFileName(mod));
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch{}
            }
        }
        
        /// <summary>
        /// Attempt to load a dll file into the game if it's valid
        /// </summary>
        /// <param name="DllName">Name of the mod / DLL</param>
        void LoadDLL(string DllName)
        {
            
            string path = "";
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                path = Application.streamingAssetsPath + "\\Mods" + "\\" + DllName + "\\" + DllName + ".dll" ;
            }
            else
            {
                path = Application.streamingAssetsPath + "/Mods" + "/" + DllName + "/" + DllName + ".dll";
            }
            
            try
            {
                Assembly dll = Assembly.LoadFile(path);
                // Application.streamingAssetsPath + "\\Mods" + "\\" + DllName + "\\" + DllName + ".dll";
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
