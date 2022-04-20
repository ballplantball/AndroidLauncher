using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidAppManager : MonoBehaviour
{
    [SerializeField]
    GameObject ApplicationButton;

    public void LaunchApp(String _packageName)
    {
        AndroidJavaClass unityPlayer;
        AndroidJavaObject currentActivity;
        AndroidJavaObject packageManager;
        AndroidJavaObject intent;

        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        intent = new AndroidJavaObject("android.content.Intent", "android.intent.action.MAIN");
        intent.Call<AndroidJavaObject>("addCategory", "android.intent.category.LAUNCHER"); //filiter system app

        AndroidJavaObject packages = packageManager.Call<AndroidJavaObject>("getInstalledApplications", packageManager.GetStatic<int>("GET_META_DATA"));
        AndroidJavaObject packages_launcher = packageManager.Call<AndroidJavaObject>("queryIntentActivities", intent, 0);
        AndroidJavaObject[] links = new AndroidJavaObject[packages_launcher.Call<int>("size")];
        string[] names = new string[links.Length]; //app names
        string[] packageName = new string[links.Length]; //package name (ex: com.htc.app)        

        for (int i = 0; i < links.Length; i++)
        {
            AndroidJavaObject currentObject = packages_launcher.Call<AndroidJavaObject>("get", i); //ResolveInfo
            try
            {
                //links[i] = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", currentObject.Get<AndroidJavaObject>("processName")); //save app link ready to call                
                //icons[i] = packageManager.Call<AndroidJavaObject>("getApplicationIcon", currentObject);

                //names[i] = packageManager.Call<string>("getApplicationLabel", currentObject);                
                //packageName[i] = currentObject.Get<string>("processName");
                names[i] = currentObject.Call<AndroidJavaObject>("loadLabel",packageManager).Call<string>("toString");
                packageName[i] = currentObject.Get<AndroidJavaObject>("activityInfo").Get<string>("packageName");                

                GameObject AppBtn = Instantiate(ApplicationButton, GameObject.FindGameObjectWithTag("List").transform);
                
                //drawable to byet by Android Studio Class
                var plugin = new AndroidJavaClass("com.xxx.xxx.PluginClass");
                AndroidJavaObject getInfo = packageManager.Call<AndroidJavaObject>("getApplicationInfo", packageName[i],128);
                byte[] decodedBytes = plugin.CallStatic<byte[]>("getIcon", packageManager, getInfo);
                Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                texture.LoadImage(decodedBytes);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
                
                AppBtn.GetComponent<Image>().sprite = sprite; //get icon to ugui
            }
            catch
            {
                //Debug.Log("skipped " + ii);
            }
        }

        /*launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", _packageName);
        currentActivity.Call("startActivity", launchIntent);*/
    }
}