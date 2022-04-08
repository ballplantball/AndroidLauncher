using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidTest : MonoBehaviour
{
    public Text debug_text;
    public void LaunchApp(String _packageName)
    {
        AndroidJavaClass unityPlayer;
        AndroidJavaObject currentActivity;
        AndroidJavaObject packageManager;
        //AndroidJavaObject launchIntent;

        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");

        int flag = packageManager.GetStatic<int>("GET_META_DATA");
        AndroidJavaObject packages = packageManager.Call<AndroidJavaObject>("getInstalledApplications", flag);        
        int count = packages.Call<int>("size");        
        AndroidJavaObject[] links = new AndroidJavaObject[count];
        string[] names = new string[count];
        string[] packageName = new string[count];
        //icons = new AndroidJavaObject[count];
        //List<byte[]> byteimg = new List<byte[]>();
        int ii = 0;
        for (int i = 0; ii < count;)
        {
            //get the object
            AndroidJavaObject currentObject = packages.Call<AndroidJavaObject>("get", ii);
            try
            {
                //try to add the variables to the next entry                
                links[i] = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", currentObject.Get<AndroidJavaObject>("processName"));
                names[i] = packageManager.Call<string>("getApplicationLabel", currentObject);
                packageName[i] = currentObject.Get<string>("processName");
                //icons[i] = packageManager.Call<AndroidJavaObject>("getApplicationIcon", currentObject);
                //Debug.Log("(" + ii + ") " + i + " " + names[i]);
                Debug.Log("(" + ii + ") " + i + " " + packageName[i]);
                //go to the next app and entry
                i++;
                ii++;
            }
            catch
            {
                //if it fails, just go to the next app and try to add to that same entry.
                Debug.Log("skipped " + ii);
                ii++;
            }
        }

        /*launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", _packageName);
        currentActivity.Call("startActivity", launchIntent);*/
    }
}
