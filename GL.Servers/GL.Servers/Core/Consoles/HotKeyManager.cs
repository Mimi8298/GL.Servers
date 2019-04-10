namespace GL.Servers.Core.Consoles
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class HotKeyManager
    {
        private static bool ConsoleHidden = false;

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private static readonly LowLevelKeyboardProc _proc = HotKeyManager.HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private static bool CONTROL_DOWN;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Initialize()
        {
            HotKeyManager._hookID = HotKeyManager.SetHook(HotKeyManager._proc);
            Application.Run();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]      private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]                                     private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]      private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]    private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]                                                 static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]                                                   static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return HotKeyManager.SetWindowsHookEx(HotKeyManager.WH_KEYBOARD_LL, proc, HotKeyManager.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)HotKeyManager.WM_KEYDOWN)
            {
                int vkCode      = Marshal.ReadInt32(lParam);
                string theKey   = ((Keys)vkCode).ToString();

                if (theKey.Contains("ControlKey"))
                {
                    HotKeyManager.CONTROL_DOWN = true;
                }
                else if (HotKeyManager.CONTROL_DOWN && theKey == "F12")
                {
                    if (ConsoleHidden)
                    {
                        ShowWindow(GetConsoleWindow(), 0);
                        HotKeyManager.ConsoleHidden = false;
                    }
                    else
                    {
                        ShowWindow(GetConsoleWindow(), 5);
                        HotKeyManager.ConsoleHidden = true;
                    }
                }
            }
            else if (nCode >= 0 && wParam == (IntPtr)HotKeyManager.WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                string theKey = ((Keys)vkCode).ToString();

                if (theKey.Contains("ControlKey"))
                {
                    HotKeyManager.CONTROL_DOWN = false;
                }
            }

            return HotKeyManager.CallNextHookEx(HotKeyManager._hookID, nCode, wParam, lParam);
        }
    }
}