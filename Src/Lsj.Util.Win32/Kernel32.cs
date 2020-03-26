﻿using Lsj.Util.Win32.BaseTypes;
using Lsj.Util.Win32.Enums;
using System;
using System.Runtime.InteropServices;
using System.Text;
using static Lsj.Util.Win32.Enums.SystemErrorCodes;

namespace Lsj.Util.Win32
{
    /// <summary>
    /// Kernel32.dll
    /// </summary>
    public static partial class Kernel32
    {
        /// <summary>
        /// NUMA_NO_PREFERRED_NODE
        /// </summary>
        public const uint NUMA_NO_PREFERRED_NODE = 0xffffffff;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpProc"></param>
        [Obsolete]
        public static void FreeProcInstance(FARPROC lpProc)
        {

        }

        /// <summary>
        /// <para>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/libloaderapi/nf-libloaderapi-getprocaddress
        /// </para>
        /// </summary>
        /// <param name="hModule">
        /// A handle to the DLL module that contains the function or variable.
        /// The <see cref="LoadLibrary"/>, <see cref="LoadLibraryEx"/>, <see cref="LoadPackagedLibrary"/>,
        /// or <see cref="GetModuleHandle"/> function returns this handle.
        /// The <see cref="GetProcAddress"/> function does not retrieve addresses from modules
        /// that were loaded using the <see cref="LOAD_LIBRARY_AS_DATAFILE"/> flag.
        /// For more information, see <see cref="LoadLibraryEx"/>.
        /// </param>
        /// <param name="lpProcName">
        /// The function or variable name, or the function's ordinal value.
        /// If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the address of the exported function or variable.
        /// If the function fails, the return value is <see cref="IntPtr.Zero"/>.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        /// <remarks>
        /// The spelling and case of a function name pointed to by <paramref name="lpProcName"/> must be identical to that
        /// in the EXPORTS statement of the source DLL's module-definition (.def) file.
        /// The exported names of functions may differ from the names you use when calling these functions in your code.
        /// This difference is hidden by macros used in the SDK header files.
        /// For more information, see Conventions for Function Prototypes.
        /// The <paramref name="lpProcName"/> parameter can identify the DLL function by specifying an ordinal value
        /// associated with the function in the EXPORTS statement.
        /// <see cref="GetProcAddress"/> verifies that the specified ordinal is in the range 1 through the highest ordinal value exported in the .def file.
        /// The function then uses the ordinal as an index to read the function's address from a function table.
        /// If the .def file does not number the functions consecutively from 1 to N (where N is the number of exported functions),
        /// an error can occur where <see cref="GetProcAddress"/> returns an invalid, non-NULL address,
        /// even though there is no function with the specified ordinal.
        /// If the function might not exist in the DLL module—for example, if the function is available only on Windows Vista
        /// but the application might be running on Windows XP—specify the function by name rather than by ordinal value
        /// and design your application to handle the case when the function is not available, as shown in the following code fragment.
        /// <code>
        /// typedef void (WINAPI *PGNSI)(LPSYSTEM_INFO);
        /// // Call GetNativeSystemInfo if supported or GetSystemInfo otherwise.
        /// PGNSI pGNSI;
        /// SYSTEM_INFO si;
        /// ZeroMemory(&si, sizeof(SYSTEM_INFO));
        /// pGNSI = (PGNSI) GetProcAddress(GetModuleHandle(TEXT("kernel32.dll")), "GetNativeSystemInfo");
        /// if(NULL != pGNSI)
        /// {
        ///     pGNSI(&amp;si);
        /// }
        /// else 
        /// {
        ///     GetSystemInfo(&amp;si);
        /// }
        /// </code>
        /// For the complete example that contains this code fragment, see Getting the System Version.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetProcAddress", SetLastError = true, ThrowOnUnmappableChar = true)]
        public static extern FARPROC GetProcAddress([In]HMODULE hModule, [MarshalAs(UnmanagedType.LPStr)][In]string lpProcName);

        /// <summary>
        /// <para>
        /// Determines whether the calling process has read access to the memory at the specified address.
        /// Important
        /// This function is obsolete and should not be used. Despite its name,
        /// it does not guarantee that the pointer is valid or that the memory pointed to is safe to use.
        /// For more information, see Remarks on this page.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/nf-winbase-isbadcodeptr
        /// </para>
        /// </summary>
        /// <param name="lpfn">
        /// A pointer to a memory address.
        /// </param>
        /// <returns>
        /// If the calling process has read access to the specified memory, the return value is <see cref="BOOL.FALSE"/>.
        /// If the calling process does not have read access to the specified memory, the return value is <see cref="BOOL.TRUE"/>.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// If the application is compiled as a debugging version, and the process does not have read access to the specified memory location,
        /// the function causes an assertion and breaks into the debugger.
        /// Leaving the debugger, the function continues as usual, and returns a <see cref="BOOL.FALSE"/> value.
        /// This behavior is by design, as a debugging aid.
        /// </returns>
        /// <remarks>
        /// In a preemptive multitasking environment, it is possible for some other thread to change the process's access to the memory being tested.
        /// Even when the function indicates that the process has read access to the specified memory,
        /// you should use structured exception handling when attempting to access the memory.
        /// Use of structured exception handling enables the system to notify the process if an access violation exception occurs,
        /// giving the process an opportunity to handle the exception.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "IsBadCodePtr", SetLastError = true)]
        public static extern BOOL IsBadCodePtr([In]FARPROC lpfn);

        /// <summary>
        /// <para>
        /// Verifies that the calling process has read access to the specified range of memory.
        /// Important This function is obsolete and should not be used.
        /// Despite its name, it does not guarantee that the pointer is valid or that the memory pointed to is safe to use.
        /// For more information, see Remarks on this page.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/nf-winbase-isbadreadptr
        /// </para>
        /// </summary>
        /// <param name="lp">
        /// A pointer to the first byte of the memory block.
        /// </param>
        /// <param name="ucb">
        /// The size of the memory block, in bytes.
        /// If this parameter is zero, the return value is <see cref="BOOL.FALSE"/>.
        /// </param>
        /// <returns>
        /// If the calling process has read access to all bytes in the specified memory range, the return value is <see cref="BOOL.FALSE"/>.
        /// If the calling process does not have read access to all bytes in the specified memory range, the return value is <see cref="BOOL.TRUE"/>.
        /// If the application is compiled as a debugging version, and the process does not have read access to all bytes in the specified memory range,
        /// the function causes an assertion and breaks into the debugger.
        /// Leaving the debugger, the function continues as usual, and returns a <see cref="BOOL.TRUE"/> value.
        /// This behavior is by design, as a debugging aid.
        /// </returns>
        /// <remarks>
        /// This function is typically used when working with pointers returned from third-party libraries,
        /// where you cannot determine the memory management behavior in the third-party DLL.
        /// Threads in a process are expected to cooperate in such a way that one will not free memory that the other needs.
        /// Use of this function does not negate the need to do this.
        /// If this is not done, the application may fail in an unpredictable manner.
        /// Dereferencing potentially invalid pointers can disable stack expansion in other threads.
        /// A thread exhausting its stack, when stack expansion has been disabled, results in the immediate termination of the parent process,
        /// with no pop-up error window or diagnostic information.
        /// If the calling process has read access to some, but not all, of the bytes in the specified memory range,
        /// the return value is <see cref="BOOL.TRUE"/>.
        /// In a preemptive multitasking environment, it is possible for some other thread to change the process's access to the memory being tested.
        /// Even when the function indicates that the process has read access to the specified memory,
        /// you should use structured exception handling when attempting to access the memory.
        /// Use of structured exception handling enables the system to notify the process if an access violation exception occurs,
        /// giving the process an opportunity to handle the exception.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "IsBadReadPtr", SetLastError = true)]
        public static extern BOOL IsBadReadPtr([In]IntPtr lp, [In]UINT_PTR ucb);

        /// <summary>
        /// <para>
        /// Verifies that the calling process has read access to the specified range of memory.
        /// Important
        /// This function is obsolete and should not be used. Despite its name,
        /// it does not guarantee that the pointer is valid or that the memory pointed to is safe to use.
        /// For more information, see Remarks on this page.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/nf-winbase-isbadstringptrw
        /// </para>
        /// </summary>
        /// <param name="lpsz">
        /// A pointer to a null-terminated string, either Unicode or ASCII.
        /// </param>
        /// <param name="ucchMax">
        /// The maximum size of the string, in TCHARs.
        /// The function checks for read access in all characters up to the string's terminating null character or
        /// up to the number of characters specified by this parameter, whichever is smaller.
        /// If this parameter is zero, the return value is <see cref="BOOL.FALSE"/>.
        /// </param>
        /// <returns>
        /// If the calling process has read access to all characters up to the string's terminating null character
        /// or up to the number of characters specified by <paramref name="ucchMax"/>, the return value is <see cref="BOOL.FALSE"/>.
        /// If the calling process does not have read access to all characters up to the string's terminating null character
        /// or up to the number of characters specified by <paramref name="ucchMax"/>, the return value is <see cref="BOOL.TRUE"/>.
        /// If the application is compiled as a debugging version, and the process does not have read access to the entire memory range specified,
        /// the function causes an assertion and breaks into the debugger.
        /// Leaving the debugger, the function continues as usual, and returns a <see cref="BOOL.FALSE"/> value This behavior is by design,
        /// as a debugging aid.
        /// </returns>
        /// <remarks>
        /// This function is typically used when working with pointers returned from third-party libraries,
        /// where you cannot determine the memory management behavior in the third-party DLL.
        /// Threads in a process are expected to cooperate in such a way that one will not free memory that the other needs.
        /// Use of this function does not negate the need to do this.
        /// If this is not done, the application may fail in an unpredictable manner.
        /// Dereferencing potentially invalid pointers can disable stack expansion in other threads.
        /// A thread exhausting its stack, when stack expansion has been disabled, results in the immediate termination of the parent process,
        /// with no pop-up error window or diagnostic information.
        /// If the calling process has read access to some, but not all, of the specified memory range, the return value is <see cref="BOOL.FALSE"/>.
        /// In a preemptive multitasking environment, it is possible for some other thread to change the process's access to the memory being tested.
        /// Even when the function indicates that the process has read access to the specified memory,
        /// you should use structured exception handling when attempting to access the memory.
        /// Use of structured exception handling enables the system to notify the process if an access violation exception occurs,
        /// giving the process an opportunity to handle the exception.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "IsBadStringPtrW", SetLastError = true)]
        public static extern BOOL IsBadStringPtr([In]IntPtr lpsz, UINT_PTR ucchMax);

        /// <summary>
        /// <para>
        /// Verifies that the calling process has write access to the specified range of memory.
        /// Important This function is obsolete and should not be used. Despite its name,
        /// it does not guarantee that the pointer is valid or that the memory pointed to is safe to use.
        /// For more information, see Remarks on this page.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/nf-winbase-isbadwriteptr
        /// </para>
        /// </summary>
        /// <param name="lp">
        /// A pointer to the first byte of the memory block.
        /// </param>
        /// <param name="ucb">
        /// The size of the memory block, in bytes. If this parameter is zero, the return value is <see cref="BOOL.FALSE"/>.
        /// </param>
        /// <returns>
        /// If the calling process has write access to all bytes in the specified memory range, the return value is <see cref="BOOL.FALSE"/>.
        /// If the calling process does not have write access to all bytes in the specified memory range, the return value is <see cref="BOOL.TRUE"/>.
        /// If the application is run under a debugger and the process does not have write access to all bytes in the specified memory range,
        /// the function causes a first chance STATUS_ACCESS_VIOLATION exception.
        /// The debugger can be configured to break for this condition.
        /// After resuming process execution in the debugger, the function continues as usual and returns a <see cref="BOOL.TRUE"/> value.
        /// This behavior is by design and serves as a debugging aid.
        /// </returns>
        /// <remarks>
        /// This function is typically used when working with pointers returned from third-party libraries,
        /// where you cannot determine the memory management behavior in the third-party DLL.
        /// Threads in a process are expected to cooperate in such a way that one will not free memory that the other needs.
        /// Use of this function does not negate the need to do this. If this is not done, the application may fail in an unpredictable manner.
        /// Dereferencing potentially invalid pointers can disable stack expansion in other threads. A thread exhausting its stack,
        /// when stack expansion has been disabled, results in the immediate termination of the parent process,
        /// with no pop-up error window or diagnostic information.
        /// If the calling process has write access to some, but not all, of the bytes in the specified memory range,
        /// the return value is <see cref="BOOL.TRUE"/>.
        /// In a preemptive multitasking environment, it is possible for some other thread to change the process's access to the memory being tested.
        /// Even when the function indicates that the process has write access to the specified memory,
        /// you should use structured exception handling when attempting to access the memory.
        /// Use of structured exception handling enables the system to notify the process if an access violation exception occurs,
        /// giving the process an opportunity to handle the exception.
        /// <see cref="IsBadWritePtr"/> is not multithread safe. To use it properly on a pointer shared by multiple threads,
        /// call it inside a critical region of code that allows only one thread to access the memory being checked.
        /// Use operating system–level objects such as critical sections or mutexes or the interlocked functions to create the critical region of code.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "IsBadReadPtr", SetLastError = true)]
        public static extern BOOL IsBadWritePtr([In]LPVOID lp, [In]UINT_PTR ucb);

        /// <summary>
        /// <para>
        /// Determines whether the specified process is running under WOW64 or an Intel64 of x64 processor.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/wow64apiset/nf-wow64apiset-iswow64process
        /// </para>
        /// </summary>
        /// <param name="hProcess">
        /// A handle to the process.
        /// The handle must have the <see cref="PROCESS_QUERY_INFORMATION"/> or <see cref="PROCESS_QUERY_LIMITED_INFORMATION"/> access right.
        /// For more information, see Process Security and Access Rights.
        /// Windows Server 2003 and Windows XP:  The handle must have the <see cref="PROCESS_QUERY_INFORMATION"/> access right.
        /// </param>
        /// <param name="Wow64Process">
        /// A pointer to a value that is set to <see langword="true"/> if the process is running under WOW64 on an Intel64 or x64 processor.
        /// If the process is running under 32-bit Windows, the value is set to <see langword="false"/>.
        /// If the process is a 32-bit application running under 64-bit Windows 10 on ARM, the value is set to <see langword="false"/>.
        /// If the process is a 64-bit application running under 64-bit Windows, the value is also set to <see langword="false"/>.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is <see langword="true"/>.
        /// If the function fails, the return value is <see langword="false"/>.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        /// <remarks>
        /// Applications should use <see cref="IsWow64Process2"/> instead of <see cref="IsWow64Process"/> to determine if a process is running under WOW.
        /// <see cref="IsWow64Process2"/> removes the ambiguity inherent to multiple WOW environments
        /// by explicitly returning both the architecture of the host and guest for a given process.
        /// Applications can use this information to reliably identify situations such as running under emulation on ARM64.
        /// To compile an application that uses this function, define _WIN32_WINNT as 0x0501 or later.
        /// For more information, see Using the Windows Headers.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "IsWow64Process", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In]IntPtr hProcess, [Out]out bool Wow64Process);

#pragma warning disable IDE1006
        /// <summary>
        /// <para>
        /// Compares two character strings. The comparison is case-sensitive.
        /// To perform a comparison that is not case-sensitive, use the lstrcmpi function.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/nf-winbase-lstrcmpw
        /// </para>
        /// </summary>
        /// <param name="lpString1">
        /// The first null-terminated string to be compared.
        /// </param>
        /// <param name="lpString2">
        /// The second null-terminated string to be compared.
        /// </param>
        /// <returns>
        /// If the string pointed to by <paramref name="lpString1"/> is less than the string pointed to by <paramref name="lpString2"/>,
        /// the return value is negative.
        /// If the string pointed to by <paramref name="lpString1"/> is greater than the string pointed to by <paramref name="lpString2"/>,
        /// the return value is positive.
        /// If the strings are equal, the return value is zero.
        /// </returns>
        /// <remarks>
        /// The <see cref="lstrcmp"/> function compares two strings by checking the first characters against each other,
        /// the second characters against each other, and so on until it finds an inequality or reaches the ends of the strings.
        /// Note that the <paramref name="lpString1"/> and <paramref name="lpString2"/> parameters must be null-terminated,
        /// otherwise the string comparison can be incorrect.
        /// The function calls <see cref="CompareStringEx"/>, using the current thread locale, and subtracts 2 from the result,
        /// to maintain the C run-time conventions for comparing strings.
        /// The language (user locale) selected by the user at setup time, or through Control Panel,
        /// determines which string is greater (or whether the strings are the same).
        /// If no language (user locale) is selected, the system performs the comparison by using default values.
        /// With a double-byte character set (DBCS) version of the system, this function can compare two DBCS strings.
        /// The <see cref="lstrcmp"/> function uses a word sort, rather than a string sort.
        /// A word sort treats hyphens and apostrophes differently than it treats other symbols that are not alphanumeric,
        /// in order to ensure that words such as "coop" and "co-op" stay together within a sorted list.
        /// For a detailed discussion of word sorts and string sorts, see Handling Sorting in Your Applications.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "lstrcmpW", SetLastError = true)]
        public static extern int lstrcmp([MarshalAs(UnmanagedType.LPWStr)][In]string lpString1, [MarshalAs(UnmanagedType.LPWStr)][In]string lpString2);

        /// <summary>
        /// <para>
        /// Compares two character strings. The comparison is not case-sensitive.
        /// To perform a comparison that is case-sensitive, use the <see cref="lstrcmp"/> function.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/nf-winbase-lstrcmpiw
        /// </para>
        /// </summary>
        /// <param name="lpString1">
        /// The first null-terminated string to be compared.
        /// </param>
        /// <param name="lpString2">
        /// The second null-terminated string to be compared.
        /// </param>
        /// <returns>
        /// If the string pointed to by <paramref name="lpString1"/> is less than the string pointed to by <paramref name="lpString2"/>,
        /// the return value is negative.
        /// If the string pointed to by <paramref name="lpString1"/> is greater than the string pointed to by <paramref name="lpString2"/>,
        /// the return value is positive.
        /// If the strings are equal, the return value is zero.
        /// </returns>
        /// <remarks>
        /// The <see cref="lstrcmpi"/> function compares two strings by checking the first characters against each other,
        /// the second characters against each other, and so on until it finds an inequality or reaches the ends of the strings.
        /// Note that the <paramref name="lpString1"/> and <paramref name="lpString2"/> parameters must be null-terminated,
        /// otherwise the string comparison can be incorrect.
        /// The function calls <see cref="CompareStringEx"/>, using the current thread locale, and subtracts 2 from the result,
        /// to maintain the C run-time conventions for comparing strings.
        /// For some locales, the lstrcmpi function may be insufficient.
        /// If this occurs, use <see cref="CompareStringEx"/> to ensure proper comparison.
        /// For example, in Japan call with the NORM_IGNORECASE, NORM_IGNOREKANATYPE, and NORM_IGNOREWIDTH values
        /// to achieve the most appropriate non-exact string comparison.
        /// The NORM_IGNOREKANATYPE and NORM_IGNOREWIDTH values are ignored in non-Asian locales, so you can set these values
        /// for all locales and be guaranteed to have a culturally correct "insensitive" sorting regardless of the locale.
        /// Note that specifying these values slows performance, so use them only when necessary.
        /// With a double-byte character set (DBCS) version of the system, this function can compare two DBCS strings.
        /// The lstrcmpi function uses a word sort, rather than a string sort.
        /// A word sort treats hyphens and apostrophes differently than it treats other symbols that are not alphanumeric,
        /// in order to ensure that words such as "coop" and "co-op" stay together within a sorted list.
        /// For a detailed discussion of word sorts and string sorts, see Handling Sorting in Your Applications.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "lstrcmpiW", SetLastError = true)]
        public static extern int lstrcmpi([MarshalAs(UnmanagedType.LPWStr)][In]string lpString1, [MarshalAs(UnmanagedType.LPWStr)][In]string lpString2);
#pragma warning restore IDE1006

        /// <summary>
        /// MakeProcInstance
        /// </summary>
        /// <param name="lpProc"></param>
        /// <param name="hInstance"></param>
        /// <returns></returns>
        [Obsolete]
        public static FARPROC MakeProcInstance(FARPROC lpProc, HINSTANCE hInstance) => lpProc;

        /// <summary>
        /// <para>
        /// Retrieves the current value of the performance counter, which is a high resolution (&lt;1us) time stamp
        /// that can be used for time-interval measurements.
        /// </para>
        /// <para>
        /// https://docs.microsoft.com/zh-cn/windows/win32/api/profileapi/nf-profileapi-queryperformancecounter
        /// </para>
        /// </summary>
        /// <param name="lpPerformanceCount">
        /// A pointer to a variable that receives the current performance-counter value, in counts.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is <see langword="true"/>.
        /// If the function fails, the return value is <see langword="false"/>.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// On systems that run Windows XP or later, the function will always succeed and will thus never return zero.
        /// </returns>
        /// <remarks>
        /// For more info about this function and its usage, see Acquiring high-resolution time stamps.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryPerformanceCounter", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool QueryPerformanceCounter([Out]out LARGE_INTEGER lpPerformanceCount);

        /// <summary>
        /// <para>
        /// Retrieves the frequency of the performance counter.
        /// The frequency of the performance counter is fixed at system boot and is consistent across all processors.
        /// Therefore, the frequency need only be queried upon application initialization, and the result can be cached.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/profileapi/nf-profileapi-queryperformancefrequency
        /// </para>
        /// </summary>
        /// <param name="lpFrequency">
        /// A pointer to a variable that receives the current performance-counter frequency, in counts per second.
        /// If the installed hardware doesn't support a high-resolution performance counter, this parameter can be zero
        /// (this will not occur on systems that run Windows XP or later).
        /// </param>
        /// <returns>
        /// If the installed hardware supports a high-resolution performance counter, the return value is <see langword="true"/>.
        /// If the function fails, the return value is <see langword="false"/>.
        /// To get extended error information, call <see cref="GetLastError"/>.
        /// On systems that run Windows XP or later, the function will always succeed and will thus never return zero.
        /// </returns>
        /// <remarks>
        /// For more info about this function and its usage, see Acquiring high-resolution time stamps.
        /// </remarks>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "QueryPerformanceFrequency", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool QueryPerformanceFrequency([Out]out LARGE_INTEGER lpFrequency);

        /// <summary>
        /// <para>
        /// Runs the specified application.
        /// </para>
        /// <para>
        /// From: https://docs.microsoft.com/zh-cn/windows/win32/api/winbase/nf-winbase-winexec
        /// </para>
        /// </summary>
        /// <param name="lpCmdLine">
        /// The command line (file name plus optional parameters) for the application to be executed.
        /// If the name of the executable file in the <paramref name="lpCmdLine"/> parameter does not contain a directory path,
        /// the system searches for the executable file in this sequence:
        /// The directory from which the application loaded.
        /// The current directory.
        /// The Windows system directory. The <see cref="GetSystemDirectory"/> function retrieves the path of this directory.
        /// The Windows directory. The <see cref="GetWindowsDirectory"/> function retrieves the path of this directory.
        /// The directories listed in the PATH environment variable.
        /// </param>
        /// <param name="uCmdShow">
        /// The display options.
        /// For a list of the acceptable values, see the description of the nCmdShow parameter of the <see cref="ShowWindow"/> function.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is greater than 31.
        /// If the function fails, the return value is one of the following error values.
        /// 0: The system is out of memory or resources.
        /// <see cref="ERROR_BAD_FORMAT"/>: The .exe file is invalid.
        /// <see cref="ERROR_FILE_NOT_FOUND"/>: The specified file was not found.
        /// <see cref="ERROR_PATH_NOT_FOUND"/>: The specified path was not found.
        /// </returns>
        /// <remarks>
        /// The <see cref="WinExec"/> function returns when the started process calls the <see cref="GetMessage"/> function or a time-out limit is reached.
        /// To avoid waiting for the time out delay, call the <see cref="GetMessage"/> function as soon as possible
        /// in any process started by a call to <see cref="WinExec"/>.
        /// Security Remarks
        /// The executable name is treated as the first white space-delimited string in <paramref name="lpCmdLine"/>.
        /// If the executable or path name has a space in it, there is a risk that a different executable could be run
        /// because of the way the function parses spaces. The following example is dangerous because the function will attempt to run "Program.exe",
        /// if it exists, instead of "MyApp.exe".
        /// <code>
        /// WinExec("C:\\Program Files\\MyApp", ...)
        /// </code>
        /// If a malicious user were to create an application called "Program.exe" on a system,
        /// any program that incorrectly calls <see cref="WinExec"/> using the Program Files directory will run this application
        /// instead of the intended application.
        /// To avoid this problem, use <see cref="CreateProcess"/> rather than <see cref="WinExec"/>.
        /// However, if you must use <see cref="WinExec"/> for legacy reasons, make sure the application name is enclosed in quotation marks
        /// as shown in the example below.
        /// <code>
        /// WinExec("\"C:\\Program Files\\MyApp.exe\" -L -S", ...)
        /// </code>
        /// </remarks>
        [Obsolete("This function is provided only for compatibility with 16-bit Windows. Applications should use the CreateProcess function.")]
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "WinExec", SetLastError = true)]
        public static extern UINT WinExec([MarshalAs(UnmanagedType.LPWStr)][In]string lpCmdLine, [In]ShowWindowCommands uCmdShow);
    }
}
