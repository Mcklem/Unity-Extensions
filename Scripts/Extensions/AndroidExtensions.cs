using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AndroidExtensions {

    public static int GetSDKVersion()
    {
        AndroidJavaClass androidBuildClass = new AndroidJavaClass("android.os.Build$VERSION");
        return androidBuildClass.GetStatic<int>("SDK_INT");
    }

    /*Bellow API 24
    public static void OpenFile(string path)
    {
        AndroidJavaObject file = new AndroidJavaObject("java.io.File", path);
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uri = uriClass.CallStatic<AndroidJavaObject>("fromFile", file);

        AndroidJavaClass UnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = UnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<string>("ACTION_VIEW"));
        intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/pdf");
        //intent.Call("setFlags", intentClass.GetStatic<int>("FLAG_ACTIVITY_NO_HISTORY"));
        activity.Call("startActivity",intent);
    }*/

     /// [Deprecated]
    public static void OpenFile(string path)
    {

        AndroidJavaClass UnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = UnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject file = new AndroidJavaObject("java.io.File", path);
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

        
        AndroidJavaClass fileProvider = new AndroidJavaClass("android.support.v4.content$FileProvider");

        AndroidJavaObject intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<string>("ACTION_VIEW"));

        string packageName = activity.Call<string>("getPackageName");
        string authority = packageName + ".fileprovider";

        AndroidJavaObject uri = fileProvider.CallStatic<AndroidJavaObject>("getUriForFile", activity.Call<AndroidJavaObject>("getContext"), authority, file);
        intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/pdf");
        intent.Call<AndroidJavaObject>("addFlags", intentClass.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION"));

        activity.Call("startActivity", intent);
    }

    public static void DownloadFromUri(string url)
    {
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uri = uriClass.CallStatic<AndroidJavaObject>("parse", url);

        AndroidJavaObject request = new AndroidJavaObject("android.app.DownloadManager$Request", uri);

        AndroidJavaClass environmentClass = new AndroidJavaClass("android.os.Environment");
        AndroidJavaClass requestClass = new AndroidJavaClass("android.app.DownloadManager$Request");

        // This put the download in the same Download dir the browser uses
        string DIRECTORY_DOWNLOADS = environmentClass.GetStatic<string>("DIRECTORY_DOWNLOADS");
        request.Call("setDestinationInExternalPublicDir", DIRECTORY_DOWNLOADS, "fileName");

        // When downloading music and videos they will be listed in the player
        // (Seems to be available since Honeycomb only)
        request.Call("allowScanningByMediaScanner");

        // Notify user when download is completed
        // (Seems to be available since Honeycomb only)
        request.Call("setNotificationVisibility", requestClass.GetStatic<int>("VISIBILITY_VISIBLE_NOTIFY_COMPLETED"));


        AndroidJavaClass UnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = UnityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject downloadManager = activity.Call<AndroidJavaObject>("getSystemService", activity.GetStatic<string>("DOWNLOAD_SERVICE"));

        // Start download
        downloadManager.Call("enqueue", request);
    }

    public enum Length { SHORT = 0, LONG = 1 };

    /// <summary>
    /// Show an Android toast message.
    /// </summary>
    /// <param name="message">Message string to show in the toast.</param>
    public static void ShowToast(string message, Length length = Length.SHORT)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, (int)length);
                toastObject.Call("show");
            }));
        }
    }

    /// <summary>
    /// Required file:// or http://
    /// </summary>
    /// <param name="uri"></param>
    public static void OpenURI(string uri)
    {
        if (GetSDKVersion() < 24)
        {
            if (!uri.Contains("http://") && !uri.Contains("https://")&& !uri.Contains("file://")) uri = "file://" + uri;
            using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject currentIntent = currentActivity.Call<AndroidJavaObject>("getIntent");
                AndroidJavaObject currentUriIntent = currentIntent.CallStatic<AndroidJavaObject>("parseUri", uri, 0);
                currentActivity.Call("startActivity", currentUriIntent);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(uri)) Application.OpenURL(uri);
            else Debug.LogError("Error trying to open the current uri " + uri);
        }       
    }

    /// <summary>
    /// Show an Android notification message.
    /// </summary>
    /// <param name="message">Message string to show in the notification.</param>
    public static void ShowNotification(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        /*if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.app.NotificationManager");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject notificationObject = toastClass.CallStatic<AndroidJavaObject>("notify", unityActivity,
                    message, (int)length);
                notificationObject.Call("show");
            }));
        }*/
    }

    /*
    // prepare intent which is triggered if the
    // notification is selected

    Intent intent = new Intent(this, NotificationReceiver.class);
    // use System.currentTimeMillis() to have a unique ID for the pending intent
    PendingIntent pIntent = PendingIntent.getActivity(this, (int) System.currentTimeMillis(), intent, 0);

    // build notification
    // the addAction re-use the same intent to keep the example short
    Notification n  = new Notification.Builder(this)
            .setContentTitle("New mail from " + "test@gmail.com")
            .setContentText("Subject")
            .setSmallIcon(R.drawable.icon)
            .setContentIntent(pIntent)
            .setAutoCancel(true)
            .addAction(R.drawable.icon, "Call", pIntent)
            .addAction(R.drawable.icon, "More", pIntent)
            .addAction(R.drawable.icon, "And more", pIntent).build();


    NotificationManager notificationManager =
    (NotificationManager) getSystemService(NOTIFICATION_SERVICE);

    notificationManager.notify(0, n);
     */
}
