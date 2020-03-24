﻿using Lsj.Util.Win32.BaseTypes;
using Lsj.Util.Win32.Enums;
using Lsj.Util.Win32.Structs;
using System;
using System.Runtime.InteropServices;
using static Lsj.Util.Win32.Constants;
using static Lsj.Util.Win32.Enums.ExitWindowsExFlags;
using static Lsj.Util.Win32.Enums.SystemErrorCodes;
using static Lsj.Util.Win32.Enums.SystemShutdownReasonCodes;
using static Lsj.Util.Win32.Gdi32;
using static Lsj.Util.Win32.Kernel32;

namespace Lsj.Util.Win32
{
    /// <summary>
    /// User32.dll
    /// </summary>
    public static partial class User32
    {
        /// <summary>
        /// HWND_MESSAGE
        /// </summary>
        public static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

        /// <summary>
        /// <para>
        /// Calls the <see cref="ExitWindowsEx"/> function to log off the interactive user.
        /// Applications should call <see cref="ExitWindowsEx"/> directly.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-exitwindows
        /// </para>
        /// </summary>
        /// <param name="dwReserved">
        /// This parameter must be zero.
        /// </param>
        /// <param name="Code">
        /// This parameter must be zero.
        /// </param>
        /// <remarks>
        /// The system sends a <see cref="WM_QUERYENDSESSION"/> to the main window of each running application.
        /// An application agrees to terminate by returning <see cref="BOOL.TRUE"/> when it receives this message
        /// (or by allowing the <see cref="DefWindowProc"/> function to process the message).
        /// If any application returns <see cref="BOOL.FALSE"/> when it receives the <see cref="WM_QUERYENDSESSION"/> message, the logoff is canceled.
        /// After the system processes the results of the <see cref="WM_QUERYENDSESSION"/> message,
        /// it sends the <see cref="WM_ENDSESSION"/> message with the wParam parameter set to <see cref="BOOL.TRUE"/> if the system is shutting down
        /// and to <see cref="BOOL.FALSE"/> if it is not.
        /// </remarks>
        public static void ExitWindows(DWORD dwReserved, UINT Code) => ExitWindowsEx(EWX_LOGOFF, (SystemShutdownReasonCodes)0xFFFFFFFF);

        /// <summary>
        /// <para>
        /// Logs off the interactive user, shuts down the system, or shuts down and restarts the system.
        /// It sends the <see cref="WM_QUERYENDSESSION"/> message to all applications to determine if they can be terminated.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-exitwindowsex
        /// </para>
        /// </summary>
        /// <param name="uFlags">
        /// The shutdown type. This parameter must include one of the following values.
        /// <see cref="EWX_HYBRID_SHUTDOWN"/>, <see cref="EWX_LOGOFF"/>, <see cref="EWX_POWEROFF"/>, <see cref="EWX_REBOOT"/>,
        /// <see cref="EWX_RESTARTAPPS"/>, <see cref="EWX_SHUTDOWN"/>
        /// This parameter can optionally include one of the following values.
        /// <see cref="EWX_FORCE"/>, <see cref="EWX_FORCEIFHUNG"/>
        /// </param>
        /// <param name="dwReason">
        /// The reason for initiating the shutdown. This parameter must be one of the <see cref="SystemShutdownReasonCodes"/>.
        /// If this parameter is zero, the <see cref="SHTDN_REASON_FLAG_PLANNED"/> reason code will not be set and
        /// therefore the default action is an undefined shutdown that is logged as "No title for this reason could be found".
        /// By default, it is also an unplanned shutdown.
        /// Depending on how the system is configured, an unplanned shutdown triggers the creation of a file that contains the system state information,
        /// which can delay shutdown. Therefore, do not use zero for this parameter.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is <see cref="BOOL.TRUE"/>.
        /// Because the function executes asynchronously, a nonzero return value indicates that the shutdown has been initiated.
        /// It does not indicate whether the shutdown will succeed.
        /// It is possible that the system, the user, or another application will abort the shutdown.
        /// If the function fails, the return value is <see cref="BOOL.FALSE"/>.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        /// <remarks>
        /// The <see cref="ExitWindowsEx"/> function returns as soon as it has initiated the shutdown process.
        /// The shutdown or logoff then proceeds asynchronously. The function is designed to stop all processes in the caller's logon session.
        /// Therefore, if you are not the interactive user, the function can succeed without actually shutting down the computer.
        /// If you are not the interactive user, use the <see cref="InitiateSystemShutdown"/> or <see cref="InitiateSystemShutdownEx"/> function.
        /// A non-zero return value does not mean the logoff was or will be successful.
        /// The shutdown is an asynchronous process, and it can occur long after the API call has returned, or not at all.
        /// Even if the timeout value is zero, the shutdown can still be aborted by applications, services, or even the system.
        /// The non-zero return value indicates that the validation of the rights and parameters was successful and
        /// that the system accepted the shutdown request.
        /// When this function is called, the caller must specify whether or not applications with unsaved changes should be forcibly closed.
        /// If the caller chooses not to force these applications to close and an application with unsaved changes is running on the console session,
        /// the shutdown will remain in progress until the user logged into the console session aborts the shutdown, saves changes,
        /// closes the application, or forces the application to close.
        /// During this period, the shutdown may not be aborted except by the console user, and another shutdown may not be initiated.
        /// Calling this function with the value of the <paramref name="uFlags"/> parameter set to <see cref="EWX_FORCE"/> avoids this situation.
        /// Remember that doing this may result in loss of data.
        /// To set a shutdown priority for an application relative to other applications in the system,
        /// use the <see cref="SetProcessShutdownParameters"/> function.
        /// During a shutdown or log-off operation, running applications are allowed a specific amount of time to respond to the shutdown request.
        /// If this time expires before all applications have stopped, the system displays a user interface that allows
        /// the user to forcibly shut down the system or to cancel the shutdown request.
        /// If the <see cref="EWX_FORCE"/> value is specified, the system forces running applications to stop when the time expires.
        /// If the <see cref="EWX_FORCEIFHUNG"/> value is specified, the system forces hung applications to close and does not display the dialog box.
        /// Console processes receive a separate notification message, <see cref="CTRL_SHUTDOWN_EVENT"/>
        /// or <see cref="CTRL_LOGOFF_EVENT"/>, as the situation warrants.
        /// A console process routes these messages to its <see cref="HandlerRoutine"/> function.
        /// <see cref="ExitWindowsEx"/> sends these notification messages asynchronously; thus, an application cannot assume
        /// that the console notification messages have been handled when a call to <see cref="ExitWindowsEx"/> returns.
        /// To shut down or restart the system, the calling process must use the <see cref="AdjustTokenPrivileges"/> function
        /// to enable the <see cref="SE_SHUTDOWN_NAME"/> privilege.
        /// For more information, see Running with Special Privileges.
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "ExitWindowsEx", SetLastError = true)]
        public static extern BOOL ExitWindowsEx([In]ExitWindowsExFlags uFlags, [In]SystemShutdownReasonCodes dwReason);

        /// <summary>
        /// <para>
        /// Translates a string into the OEM-defined character set.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-chartooemw
        /// </para>
        /// </summary>
        /// <param name="pSrc">
        /// The null-terminated string to be translated.
        /// </param>
        /// <param name="pDst">
        /// The destination buffer, which receives the translated string.
        /// If the CharToOem function is being used as an ANSI function, the string can be translated in place
        /// by setting the <paramref name="pDst"/> parameter to the same address as the <paramref name="pSrc"/> parameter.
        /// This cannot be done if CharToOem is being used as a wide-character function.
        /// </param>
        /// <returns>
        /// The return value is always <see langword="true"/> except when you pass the same address
        /// to <paramref name="pSrc"/> and <paramref name="pDst"/> in the wide-character version of the function.
        /// In this case the function returns <see langword="false"/> and
        /// <see cref="GetLastError"/> returns <see cref="ERROR_INVALID_ADDRESS"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "CharToOemW", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CharToOem([MarshalAs(UnmanagedType.LPWStr)][In]string pSrc, [In]IntPtr pDst);

        /// <summary>
        /// <para>
        /// Retrieves an <see cref="AR_STATE"/> value containing the state of screen auto-rotation for the system,
        /// for example whether auto-rotation is supported, and whether it is enabled by the user.
        /// <see cref="GetAutoRotationState"/> provides a robust and diverse way of querying for auto-rotation state, and more.
        /// For example, if you want your app to behave differently when multiple monitors are attached
        /// then you can determine that from the <see cref="AR_STATE"/> returned.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-getautorotationstate
        /// </para>
        /// </summary>
        /// <param name="pState">
        /// Pointer to a location in memory that will receive the current state of auto-rotation for the system.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the method succeeds, otherwise <see langword="false"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetAutoRotationState", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetAutoRotationState([In][Out]ref AR_STATE pState);

        /// <summary>
        /// <para>
        /// Returns the dots per inch (dpi) value for the associated window.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-getdpiforwindow 
        /// </para>
        /// </summary>
        /// <param name="hwnd">The window you want to get information about.</param>
        /// <returns>
        /// The DPI for the window which depends on the DPI_AWARENESS of the window. An invalid hwnd value will result in a return value of 0.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetDpiForWindow", SetLastError = true)]
        public static extern uint GetDpiForWindow([In]IntPtr hwnd);

        /// <summary>
        /// <para>
        /// The GetMonitorInfo function retrieves information about a display monitor.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-getmonitorinfow
        /// </para>
        /// </summary>
        /// <param name="hMonitor">
        /// A handle to the display monitor of interest.
        /// </param>
        /// <param name="lpmi">
        /// A pointer to a <see cref="MONITORINFOEX"/> structure that receives information about
        /// the specified display monitor.
        /// You must set <see cref="MONITORINFOEX.cbSize"/> member of the structure to <code>sizeof(MONITORINFOEX)</code>
        /// before calling the <see cref="GetMonitorInfo"/> function. Doing so lets the function determine the type of structure you are passing to it.
        /// <see cref="MONITORINFOEX"/> structure is a superset of the <see cref="MONITORINFO"/> structure. 
        /// It has one additional member: a string that contains a name for the display monitor.
        /// Most applications have no use for a display monitor name, and so can save some bytes by using a <see cref="MONITORINFO"/> structure.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is <see langword="true"/>.
        /// If the function fails, the return value is <see langword="false"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetMonitorInfoW", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorInfo([In]IntPtr hMonitor, [In][Out]ref MONITORINFOEX lpmi);

        /// <summary>
        /// <para>
        /// Retrieves a handle to the drop-down menu or submenu activated by the specified menu item.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-getsubmenu
        /// </para>
        /// </summary>
        /// <param name="hMenu">
        /// A handle to the menu.
        /// </param>
        /// <param name="nPos">
        /// The zero-based relative position in the specified menu of an item that activates a drop-down menu or submenu.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the drop-down menu or submenu activated by the menu item.
        /// If the menu item does not activate a drop-down menu or submenu, the return value is <see cref="IntPtr.Zero"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetSubMenu", SetLastError = true)]
        public static extern IntPtr GetSubMenu([In]IntPtr hMenu, [In]int nPos);

        /// <summary>
        /// <para>
        /// Retrieves the current color of the specified display element.
        /// Display elements are the parts of a window and the display that appear on the system display screen.
        /// </para>
        /// </summary>
        /// <param name="nIndex">
        /// The display element whose color is to be retrieved
        /// </param>
        /// <returns>
        /// The function returns the red, green, blue (RGB) color value of the given element.
        /// If the <paramref name="nIndex"/> parameter is out of range, the return value is zero.
        /// Because zero is also a valid RGB value, you cannot use <see cref="GetSysColor"/> to determine 
        /// whether a system color is supported by the current platform.
        /// Instead, use the <see cref="GetSysColorBrush"/> function, which returns <see cref="IntPtr.Zero"/> if the color is not supported.
        /// </returns>
        /// <remarks>
        /// To display the component of the RGB value, use the <see cref="GetRValue"/>, <see cref="GetGValue"/>, and <see cref="GetBValue"/> macros.
        /// System colors for monochrome displays are usually interpreted as shades of gray.
        /// To paint with a system color brush, an application should use <see cref="GetSysColorBrush"/>(nIndex),
        /// instead of <see cref="CreateSolidBrush"/>(<see cref="GetSysColor"/>(nIndex)),
        /// because <see cref="GetSysColorBrush"/> returns a cached brush, instead of allocating a new one.
        /// Color is an important visual element of most user interfaces. For guidelines about using color in your applications, see Color.
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetSysColor", SetLastError = true)]
        public static extern uint GetSysColor([In]SystemColors nIndex);

        /// <summary>
        /// <para>
        /// The <see cref="GetSysColorBrush"/> function retrieves a handle identifying a logical brush that corresponds to the specified color index.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-getsyscolorbrush
        /// </para>
        /// </summary>
        /// <param name="nIndex">
        /// A color index.
        /// This value corresponds to the color used to paint one of the window elements.
        /// See <see cref="GetSysColor"/> for system color index values.
        /// </param>
        /// <returns>
        /// The return value identifies a logical brush if the nIndex parameter is supported by the current platform.
        /// Otherwise, it returns <see cref="IntPtr.Zero"/>.
        /// </returns>
        /// <remarks>
        /// A brush is a bitmap that the system uses to paint the interiors of filled shapes.
        /// An application can retrieve the current system colors by calling the <see cref="GetSysColor"/> function.
        /// An application can set the current system colors by calling the <see cref="SetSysColors"/> function.
        /// An application must not register a window class for a window using a system brush.
        /// To register a window class with a system color, see the documentation of the <see cref="WNDCLASS.hbrBackground"/> member
        /// of the <see cref="WNDCLASS"/> or <see cref="WNDCLASSEX"/> structures.
        /// System color brushes track changes in system colors.
        /// In other words, when the user changes a system color, the associated system color brush automatically changes to the new color.
        /// To paint with a system color brush, an application should use <see cref="GetSysColorBrush"/> (nIndex) instead of
        /// <see cref="CreateSolidBrush"/> ( <see cref="GetSysColor"/> (nIndex)),
        /// because <see cref="GetSysColorBrush"/> returns a cached brush instead of allocating a new one.
        /// System color brushes are owned by the system so you don't need to destroy them.
        /// Although you don't need to delete the logical brush that <see cref="GetSysColorBrush"/> returns,
        /// no harm occurs by calling <see cref="DeleteObject"/>.
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetSysColorBrush", SetLastError = true)]
        public static extern IntPtr GetSysColorBrush([In]SystemColors nIndex);

        /// <summary>
        /// <para>
        /// Retrieves the specified system metric or system configuration setting.
        /// Note that all dimensions retrieved by <see cref="GetSystemMetrics"/> are in pixels.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics
        /// </para>
        /// </summary>
        /// <param name="smIndex">
        /// The system metric or configuration setting to be retrieved.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the requested system metric or configuration setting.
        /// If the function fails, the return value is 0. <see cref="GetLastError"/> does not provide extended error information.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetSystemMetrics", SetLastError = true)]
        public static extern int GetSystemMetrics([In]SystemMetric smIndex);

        /// <summary>
        /// <para>
        /// The <see cref="MonitorFromWindow"/> function retrieves a handle to the display monitor 
        /// that has the largest area of intersection with the bounding rectangle of a specified window.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-monitorfromwindow
        /// </para>
        /// </summary>
        /// <param name="hwnd">A handle to the window of interest.</param>
        /// <param name="dwFlags">Determines the function's return value if the window does not intersect any display monitor.</param>
        /// <returns>
        /// If the window intersects one or more display monitor rectangles,
        /// the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window.
        /// If the window does not intersect a display monitor, the return value depends on the value of <paramref name="dwFlags"/>.
        /// </returns>
        /// <remarks>
        /// If the window is currently minimized, <see cref="MonitorFromWindow"/> uses the rectangle of the window before it was minimized.
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "MonitorFromWindow", SetLastError = true)]
        public static extern IntPtr MonitorFromWindow([In]IntPtr hwnd, [In]MonitorDefaultFlags dwFlags);

        /// <summary>
        /// <para>
        /// The <see cref="GetUserObjectSecurity"/> function retrieves security information for the specified user object.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-getuserobjectsecurity
        /// </para>
        /// </summary>
        /// <param name="hObj">
        /// A handle to the user object for which to return security information.
        /// </param>
        /// <param name="pSIRequested">
        /// A pointer to a <see cref="SECURITY_INFORMATION"/> value that specifies the security information being requested.
        /// </param>
        /// <param name="pSID">
        /// A pointer to a <see cref="SECURITY_DESCRIPTOR"/> structure in self-relative format that contains the requested information
        /// when the function returns. This buffer must be aligned on a 4-byte boundary.
        /// </param>
        /// <param name="nLength">
        /// The length, in bytes, of the buffer pointed to by the <paramref name="pSID"/> parameter.
        /// </param>
        /// <param name="lpnLengthNeeded">
        /// A pointer to a variable to receive the number of bytes required to store the complete security descriptor.
        /// If this variable's value is greater than the value of the <paramref name="nLength"/> parameter when the function returns,
        /// the function returns <see langword="false"/> and none of the security descriptor is copied to the buffer.
        /// Otherwise, the entire security descriptor is copied.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is <see langword="true"/>.
        /// If the function fails, the return value is <see langword="false"/>.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        /// <remarks>
        /// To read the owner, group, or discretionary access control list (DACL) from the user object's security descriptor,
        /// the calling process must have been granted <see cref="READ_CONTROL"/> access when the handle was opened.
        /// To read the system access control list (SACL) from the security descriptor,
        /// the calling process must have been granted <see cref="ACCESS_SYSTEM_SECURITY"/> access when the handle was opened.
        /// The correct way to get this access is to enable the <see cref="SE_SECURITY_NAME"/> privilege in the caller's current token,
        /// open the handle for <see cref="ACCESS_SYSTEM_SECURITY"/> access, and then disable the privilege.
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetUserObjectSecurity", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetUserObjectSecurity([In]IntPtr hObj, [In]IntPtr pSIRequested, [In]IntPtr pSID,
            [In]uint nLength, [Out]out uint lpnLengthNeeded);

        /// <summary>
        /// <para>
        /// Registers the application to receive power setting notifications for the specific power setting event.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-registerpowersettingnotification
        /// </para>
        /// </summary>
        /// <param name="hRecipient">
        /// Handle indicating where the power setting notifications are to be sent.
        /// For interactive applications, the <paramref name="Flags"/> parameter should be
        /// <see cref="RegisterPowerSettingNotificationFlags.DEVICE_NOTIFY_WINDOW_HANDLE"/>,
        /// and the <paramref name="hRecipient"/> parameter should be a window handle. 
        /// For services, the <paramref name="Flags"/> parameter should be
        /// <see cref="RegisterPowerSettingNotificationFlags.DEVICE_NOTIFY_SERVICE_HANDLE"/>,
        /// and the <paramref name="hRecipient"/> parameter should be a <see cref="SERVICE_STATUS_HANDLE"/> as returned
        /// from <see cref="RegisterServiceCtrlHandlerEx"/>.
        /// </param>
        /// <param name="PowerSettingGuid">
        /// The GUID of the power setting for which notifications are to be sent.
        /// </param>
        /// <param name="Flags">
        /// Flags.
        /// </param>
        /// <returns>
        /// Returns a notification handle for unregistering for power notifications.
        /// If the function fails, the return value is <see cref="IntPtr.Zero"/>.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegisterPowerSettingNotification", SetLastError = true)]
        public static extern IntPtr RegisterPowerSettingNotification([In]IntPtr hRecipient,
            [MarshalAs(UnmanagedType.LPStruct)][In]Guid PowerSettingGuid, [In]RegisterPowerSettingNotificationFlags Flags);
    }
}
