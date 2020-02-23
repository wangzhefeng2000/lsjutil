﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Lsj.Util.Win32.Enums.FILE_INFO_BY_HANDLE_CLASS;

namespace Lsj.Util.Win32.Structs
{
    /// <summary>
    /// <para>
    /// Contains identification information for a file.
    /// This structure is returned from the <see cref="GetFileInformationByHandleEx"/> function
    /// when <see cref="FileIdInfo"/> is passed in the FileInformationClass parameter.
    /// </para>
    /// <para>
    /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/ns-winbase-file_id_info
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_ID_INFO
    {
        /// <summary>
        /// The serial number of the volume that contains a file.
        /// </summary>
        public ulong VolumeSerialNumber;

        /// <summary>
        /// The 128-bit file identifier for the file.
        /// The file identifier and the volume serial number uniquely identify a file on a single computer.
        /// To determine whether two open handles represent the same file, combine the identifier and the volume serial number
        /// for each file and compare them.
        /// </summary>
        public FILE_ID_128 FileId;
    }
}
