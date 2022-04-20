# Unity原生調用Android應用程式
`AndroidTest.cs ,原用ApplicationInfo呼叫所有APP與Icon(但系統也都撈到)`
`AndroidAppManager.cs ,改使用ResolveInfo篩選category(Icon尚未同步,須改PluginClass)`

Unity直接調用原生Android指令 :   
https://docs.unity3d.com/ScriptReference/AndroidJavaClass.html   
https://docs.unity3d.com/ScriptReference/AndroidJavaObject.html   

Android PackageManager文件 :   
https://developer.android.com/reference/android/content/pm/PackageManager   
https://developer.android.com/reference/android/content/pm/ApplicationInfo  
https://developer.android.com/reference/android/content/pm/ResolveInfo    
    
參考資料 :   
1. 獲取所有應用及APP圖片 `會抓到全部APP` :   
https://forum.unity.com/threads/using-androidjavaclass-to-return-installed-apps.337296/   
https://stackoverflow.com/questions/46205865/how-to-get-android-installed-app-icon-in-unity   
2. 獲取所有應用(忽略系統) `flag來抓取系統與否，但不完整判別已安裝app` :   
https://codeantenna.com/a/9OahFlPy0l   
3. 獲取Intent判別 "android.intent.category.LAUNCHER" `分類篩選後於unity呼叫ResolveInfo的方法` :   
https://stackoverflow.com/questions/17504169/how-to-get-installed-applications-in-android-and-no-system-apps   
https://www.twblogs.net/a/5ee7b67b8b72e6226833ad4e   
https://www.233tw.com/unity/21198   
