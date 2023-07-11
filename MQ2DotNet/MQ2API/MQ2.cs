﻿using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MQ2DotNet.MQ2API
{
    /// <summary>
    /// Contains methods and properties relating to MQ2 functionality
    /// </summary>
    [PublicAPI]
    public class MQ2
    {
        /// <summary>
        /// Write a line of chat to the MQ2 chat window
        /// </summary>
        /// <param name="text">Text to write</param>
        public void WriteChat(string text)
        {
            NativeMethods.MQ2WriteChatf(Sanitize(text));
        }

        /// <summary>
        /// Threadsafe version of <see cref="WriteChat"/>
        /// </summary>
        /// <param name="text">Text to write</param>
        public void WriteChatSafe(string text)
        {
            // Trim so as to not crash MQ2/EQ
            NativeMethods.MQ2WriteChatfSafe(Sanitize(text));
        }

        /// <summary>
        /// Uses MQ2's parser to evaluate a formula
        /// </summary>
        /// <param name="formula">Formula to calculate</param>
        /// <param name="parse">If <c>true</c>, will first parse any MQ2 variables in <paramref name="formula"/> before calculating</param>
        /// <returns>Result of the calculation</returns>
        public double Calculate(string formula, bool parse = true)
        {
            if (parse)
                formula = Parse(formula);

            if (!NativeMethods.MQ2Calculate(formula, out var result))
                throw new FormatException("Could not parse if condition: " + formula);

            return result;
        }

        /// <summary>
        /// Use MQ2's parser to evaluate a formula and return true if it is non-zero
        /// </summary>
        /// <param name="formula">Formula to calculate</param>
        /// <param name="parse">If <c>true</c>, will first parse any MQ2 variables in <paramref name="formula"/> before calculating</param>
        /// <returns>True if the result is non-zero, otherwise false</returns>
        public bool If(string formula, bool parse = true)
        {
            return Calculate(formula, parse) != 0.0;
        }

        /// <summary>
        /// Parse any MQ2 variables in <paramref name="expression"/> and replace them with the resulting text
        /// </summary>
        /// <param name="expression">Expression to parse</param>
        /// <returns>Parsed expression</returns>
        public string Parse(string expression)
        {
            var sb = new StringBuilder(expression, 2047);
            if (!NativeMethods.MQ2ParseMacroData(sb, (uint)sb.Capacity + 1))
                throw new FormatException("Could not parse expression: " + expression);

            return sb.ToString();
        }

        /// <summary>
        /// Execute a command, exactly as if you typed it in the chat window
        /// Note: whether this will parse MQ2 variables or not depends only on the command entered. Use /noparse to force no parsing
        /// </summary>
        /// <param name="command">Command to execute</param>
        public void DoCommand(string command)
        {
            NativeMethods.MQ2HideDoCommand(GetCharSpawn(), command, false);
        }

        /// <summary>
        /// Directory of MQ2 ini files
        /// </summary>
        public string RootPath
        {
            get
            {
                var hDll = Utility.NativeMethods.LoadLibrary("MQ2Main.dll");
                return Marshal.PtrToStringAnsi(Utility.NativeMethods.GetProcAddress(hDll, "gPathMQRoot"));
            }
        }

        /// <summary>
        /// Directory of MQ2 Resources
        /// </summary>
        public string ResourcePath
        {
            get
            {
                var hDll = Utility.NativeMethods.LoadLibrary("MQ2Main.dll");
                return $"{Marshal.PtrToStringAnsi(Utility.NativeMethods.GetProcAddress(hDll, "gPathResources"))}\\MQ2DotNet";
            }
        }

        /// <summary>
        /// Directory of MQ2 Config
        /// </summary>
        public string ConfigPath
        {
            get
            {
                var hDll = Utility.NativeMethods.LoadLibrary("MQ2Main.dll");
                return Marshal.PtrToStringAnsi(Utility.NativeMethods.GetProcAddress(hDll, "gPathConfig"));
            }
        }

        private static IntPtr GetCharSpawn()
        {
            var ppPlayer = Utility.NativeMethods.GetProcAddress(Utility.NativeMethods.LoadLibrary("eqlib.dll"), "pLocalPlayer");
            var pPlayer = Marshal.ReadIntPtr(ppPlayer);
            return Marshal.ReadIntPtr(pPlayer);
        }

        private static string Sanitize(string text)
        {
            // Trim so as to not crash MQ2/EQ
            var sanitized = text.Substring(0, Math.Min(text.Length, 2047));
            var index = text.IndexOfAny(new[] { '\r', '\n' });
            if (index > 0)
                sanitized = sanitized.Substring(0, index);
            return sanitized;
        }

        #region Internal WriteChat overloads
        // TODO: Move these to where they should be
        internal static void WriteChatGeneralError(string text)
        {
            new MQ2().WriteChat($"\ag[.NET] \arError: \aw{text}");
        }

        internal static void WriteChatGeneralWarning(string text)
        {
            new MQ2().WriteChat($"\ag[.NET] \ayWarning: \aw{text}");
        }

        internal static void WriteChatGeneral(string text)
        {
            new MQ2().WriteChat($"\ag[.NET] \aw{text}");
        }

        internal static void WriteChatPluginError(string text)
        {
            new MQ2().WriteChat($"\ag[.NET Plugin] \arError: \aw{text}");
        }

        internal static void WriteChatPluginWarning(string text)
        {
            new MQ2().WriteChat($"\ag[.NET Plugin] \ayWarning: \aw{text}");
        }

        internal static void WriteChatPlugin(string text)
        {
            new MQ2().WriteChat($"\ag[.NET Plugin] \aw{text}");
        }

        internal static void WriteChatProgramError(string text)
        {
            new MQ2().WriteChat($"\ag[.NET Program] \arError: \aw{text}");
        }

        internal static void WriteChatProgramWarning(string text)
        {
            new MQ2().WriteChat($"\ag[.NET Program] \ayWarning: \aw{text}");
        }

        internal static void WriteChatProgram(string text)
        {
            new MQ2().WriteChat($"\ag[.NET Program] \aw{text}");
        }

        internal static void WriteChatScriptError(string text)
        {
            new MQ2().WriteChat($"\ag[C# Script] \arError: \aw{text}");
        }

        internal static void WriteChatScriptWarning(string text)
        {
            new MQ2().WriteChat($"\ag[C# Script] \ayWarning: \aw{text}");
        }

        internal static void WriteChatScript(string text)
        {
            new MQ2().WriteChat($"\ag[C# Script] \aw{text}");
        }
        #endregion

        #region MQ2Main imports
        private static class NativeMethods
        {
            [DllImport("MQ2Main.dll", EntryPoint = "Calculate", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Calculate([MarshalAs(UnmanagedType.LPStr)] string formula, out double result);

            [DllImport("MQ2Main.dll", EntryPoint = "ParseMacroData", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2ParseMacroData([MarshalAs(UnmanagedType.LPStr)] StringBuilder szOriginal, uint BufferSize);

            [DllImport("MQ2Main.dll", EntryPoint = "HideDoCommand", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQ2HideDoCommand(IntPtr pCharSpawn, [MarshalAs(UnmanagedType.LPStr)] string Command, bool delayed);

            [DllImport("MQ2Main.dll", EntryPoint = "WriteChatf", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQ2WriteChatf([MarshalAs(UnmanagedType.LPStr)] string buffer);

            [DllImport("MQ2Main.dll", EntryPoint = "WriteChatfSafe", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQ2WriteChatfSafe([MarshalAs(UnmanagedType.LPStr)] string buffer);
        }
        #endregion
    }
}
