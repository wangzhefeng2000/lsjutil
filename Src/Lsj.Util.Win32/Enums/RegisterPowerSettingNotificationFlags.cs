﻿using System;
using System.Collections.Generic;
using System.Text;
using static Lsj.Util.Win32.User32;

namespace Lsj.Util.Win32.Enums
{
    /// <summary>
    /// <para>
    /// <see cref="RegisterPowerSettingNotification"/> flags.
    /// </para>
    /// <para>
    /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-registerpowersettingnotification
    /// </para>
    /// </summary>
    public enum RegisterPowerSettingNotificationFlags
    {
        /// <summary>
        /// Notifications are sent using <see cref="WindowsMessages.WM_POWERBROADCAST"/> messages with a wParam parameter of <see cref="PowerManagementEvents.PBT_POWERSETTINGCHANGE"/>.
        /// </summary>
        DEVICE_NOTIFY_WINDOW_HANDLE = 0,

        /// <summary>
        /// Notifications are sent to the HandlerEx callback function with a dwControl parameter of SERVICE_CONTROL_POWEREVENT and a dwEventType of <see cref="PowerManagementEvents.PBT_POWERSETTINGCHANGE"/>.
        /// </summary>
        DEVICE_NOTIFY_SERVICE_HANDLE = 1,
    }
}