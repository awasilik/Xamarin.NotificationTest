using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace notificator
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private const string NotificationChannel = "channel_1";
        private const int NotificationId = 1000;
        private NotificationManager notificationManager;
        private Notification notification;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            var showButton = FindViewById<Button>(Resource.Id.show_button);
            showButton.Click += (s, e) => ShowNotification();

            var dismissButton = FindViewById<Button>(Resource.Id.dismiss_button);
            dismissButton.Click += (s, e) => HideNotification();

            var notifiationChannel = new NotificationChannel(NotificationChannel, "Readable notification channel", NotificationImportance.Default);

            notificationManager = GetSystemService(NotificationService) as NotificationManager;
            notificationManager.CreateNotificationChannel(notifiationChannel);

            notification = new Notification.Builder(this, NotificationChannel)
                .SetColorized(true)
                .SetColor(GetColor(Resource.Color.notificationColor))
                .SetContentTitle("Time to eat!")
                .SetContentText("Omnomnomnom")
                .SetSmallIcon(Resource.Drawable.ic_bowl)
                .SetOngoing(true)
                .SetAutoCancel(false)
                .Build();
        }

        private void ShowNotification()
        {
            notificationManager.Notify(NotificationId, notification);
        }

        private void HideNotification()
        {
            notificationManager.Cancel(NotificationId);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
