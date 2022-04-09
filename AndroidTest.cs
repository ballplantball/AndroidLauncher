using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidTest : MonoBehaviour
{
    public GameObject ApplicationIcon;

    public void LaunchApp(String _packageName)
    {
        AndroidJavaClass unityPlayer;
        AndroidJavaObject currentActivity;
        AndroidJavaObject packageManager;

        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");

        int flag = packageManager.GetStatic<int>("GET_META_DATA");
        AndroidJavaObject packages = packageManager.Call<AndroidJavaObject>("getInstalledApplications", flag);        
        int count = packages.Call<int>("size");        
        AndroidJavaObject[] links = new AndroidJavaObject[count];
        //AndroidJavaObject[] icons = new AndroidJavaObject[count]; java回傳drawable無法get跟轉用
        string[] names = new string[count];
        string[] packageName = new string[count];
        int ii = 0;
        
        for (int i = 0; ii < count;)
        {
            AndroidJavaObject currentObject = packages.Call<AndroidJavaObject>("get", ii);
            try
            {
                links[i] = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", currentObject.Get<AndroidJavaObject>("processName"));
                //icons[i] = packageManager.Call<AndroidJavaObject>("getApplicationIcon", currentObject);

                //在Android Studio新增Class將drawable轉成byte回傳
                var plugin = new AndroidJavaClass("com.tomhsiao.mylibrary.PluginClass");
                byte[] decodedBytes = plugin.CallStatic<byte[]>("getIcon", packageManager, currentObject);
                Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                texture.LoadImage(decodedBytes);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));

                names[i] = packageManager.Call<string>("getApplicationLabel", currentObject);
                packageName[i] = currentObject.Get<string>("processName");
                //icons[i] = packageManager.Call<AndroidJavaObject>("getApplicationIcon", currentObject);
                //Debug.Log("(" + ii + ") " + i + " " + names[i]);                
                GameObject appIcon =  Instantiate(ApplicationIcon, GameObject.FindGameObjectWithTag("List").transform);
                appIcon.GetComponent<Image>().sprite = sprite;
                i++;
                ii++;
            }
            catch
            {
                Debug.Log("skipped " + ii);
                ii++;
            }
        }

        /*launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", _packageName);
        currentActivity.Call("startActivity", launchIntent);*/
    }
}