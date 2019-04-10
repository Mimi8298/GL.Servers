namespace GL.Apps.GobelinLand
{
    using Android.App;
    using Android.Widget;
    using Android.OS;

    [Activity(Label = "GobelinLand", MainLauncher = true)]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Called when the activity is starting.
        /// </summary>
        /// <param name="savedInstanceState">If the activity is being re-initialized after
        /// previously being shut down then this Bundle contains the data it most
        /// recently supplied in <c><see cref="M:Android.App.Activity.OnSaveInstanceState(Android.OS.Bundle)" /></c>.  <format type="text/html"><b><i>Note: Otherwise it is null.</i></b></format></param>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when the activity is starting.  This is where most initialization
        /// should go: calling <c><see cref="M:Android.App.Activity.SetContentView(System.Int32)" /></c> to inflate the
        /// activity's UI, using <c><see cref="M:Android.App.Activity.FindViewById(System.Int32)" /></c> to programmatically interact
        /// with widgets in the UI, calling
        /// <c><see cref="M:Android.App.Activity.ManagedQuery(Android.Net.Uri, System.String[], System.String[], System.String[], System.String[])" /></c> to retrieve
        /// cursors for data being displayed, etc.
        /// </para>
        /// <para tool="javadoc-to-mdoc">You can call <c><see cref="M:Android.App.Activity.Finish" /></c> from within this function, in
        /// which case onDestroy() will be immediately called without any of the rest
        /// of the activity lifecycle (<c><see cref="M:Android.App.Activity.OnStart" /></c>, <c><see cref="M:Android.App.Activity.OnResume" /></c>,
        /// <c><see cref="M:Android.App.Activity.OnPause" /></c>, etc) executing.
        /// </para>
        /// <para tool="javadoc-to-mdoc">
        ///   <i>Derived classes must call through to the super class's
        /// implementation of this method.  If they do not, an exception will be
        /// thrown.</i>
        /// </para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/app/Activity.html#onCreate(android.os.Bundle)" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 1" />
        /// <altmember cref="M:Android.App.Activity.OnStart" />
        /// <altmember cref="M:Android.App.Activity.OnSaveInstanceState(Android.OS.Bundle)" />
        /// <altmember cref="M:Android.App.Activity.OnRestoreInstanceState(Android.OS.Bundle)" />
        /// <altmember cref="M:Android.App.Activity.OnPostCreate(Android.OS.Bundle)" />
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
        }
    }
}

