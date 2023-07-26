﻿using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Plugin;
using MQ2DotNet.Program;
using MQ2DotNet.Services;
using MQ2DotNet.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MQ2DotNet
{
    /// <summary>
    /// Class containing functions for MQ2DotNetLoader to call from the regular plugin callbacks
    /// </summary>
    public static class PluginStub
    {
        #region Delegates/typedefs
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        // ReSharper disable InconsistentNaming
        private delegate void fMQShutdownPlugin();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQCleanUI();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQReloadUI();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQDrawHUD();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQSetGameState(uint gameState);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQPulse();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint fMQIncomingChat([MarshalAs(UnmanagedType.LPStr)] string line, uint color);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint fMQWriteChatColor([MarshalAs(UnmanagedType.LPStr)] string line, uint color, uint filter);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQSpawn(IntPtr pSpawn);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQGroundItem(IntPtr pGroundItem);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQBeginZone();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQEndZone();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQZoned();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQMacroStart([MarshalAs(UnmanagedType.LPStr)] string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQMacroStop([MarshalAs(UnmanagedType.LPStr)] string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQLoadPlugin([MarshalAs(UnmanagedType.LPStr)] string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void fMQUnloadPlugin([MarshalAs(UnmanagedType.LPStr)] string name);
        // ReSharper restore InconsistentNaming
        #endregion

        #region Instances of delegates
        // These are required to keep the garbage collector from cleaning up the parameter to GetFunctionPointerForDelegate
        private static readonly fMQShutdownPlugin _shutdownPlugin = ShutdownPlugin;
        private static readonly fMQBeginZone _beginZone = BeginZone;
        private static readonly fMQEndZone _endZone = EndZone;
        private static readonly fMQGroundItem _onAddGroundItem = OnAddGroundItem;
        private static readonly fMQSpawn _onAddSpawn = OnAddSpawn;
        private static readonly fMQCleanUI _onCleanUI = OnCleanUI;
        private static readonly fMQDrawHUD _onDrawHUD = OnDrawHUD;
        private static readonly fMQIncomingChat _onIncomingChat = OnIncomingChat;
        private static readonly fMQPulse _onPulse = OnPulse;
        private static readonly fMQReloadUI _onReloadUI = OnReloadUI;
        private static readonly fMQGroundItem _onRemoveGroundItem = OnRemoveGroundItem;
        private static readonly fMQSpawn _onRemoveSpawn = OnRemoveSpawn;
        private static readonly fMQWriteChatColor _onWriteChatColor = OnWriteChatColor;
        private static readonly fMQSetGameState _setGameState = SetGameState;
        private static readonly fMQZoned _onZoned = OnZoned;
        private static readonly fMQMacroStart _onMacroStart = OnMacroStart;
        private static readonly fMQMacroStop _onMacroStop = OnMacroStop;
        private static readonly fMQLoadPlugin _onLoadPlugin = OnLoadPlugin;
        private static readonly fMQUnloadPlugin _onUnloadPlugin = OnUnloadPlugin;
        #endregion

        /// <summary>
        /// Synchronization context that runs all continuations in OnPulse
        /// </summary>
        /// <remarks>Only used for async commands, of which we have none. It's here because it's required by Commands.</remarks>
        private static readonly EventLoopContext _eventLoopContext = new EventLoopContext();

        /// <summary>
        /// Service for adding commands
        /// </summary>
        private static readonly Commands _commands = new Commands(_eventLoopContext);

        private static readonly List<AppDomainBase> _appDomains = new List<AppDomainBase>();

        private static readonly string _configFilePath = new MQ2().ConfigPath + "\\MQ2DotNet.ini";

        private static readonly string _resourcePath = new MQ2().ResourcePath;


        private static ReadOnlyDictionary<string, PluginAppDomain> Plugins => new ReadOnlyDictionary<string, PluginAppDomain>(_appDomains
            .Where(d => d is PluginAppDomain)
            .Cast<PluginAppDomain>()
            .ToDictionary(p => p.Name, p => p));

        private static ReadOnlyDictionary<string, ProgramAppDomain> Programs => new ReadOnlyDictionary<string, ProgramAppDomain>(_appDomains
            .Where(d => d is ProgramAppDomain)
            .Cast<ProgramAppDomain>()
            .ToDictionary(p => p.Name, p => p));

        /// <summary>
        /// Entrypoint, called by MQ2DotNetLoader
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static int InitializePlugin(string arg)
        {
            // This will be the first managed function that gets called. 
            try
            {
                // MQ2DotNet.dll exports function pointers that it then calls when the corresponding plugin function is called
                // Here we set them to our managed functions in this class
                IntPtr hDll = NativeMethods.LoadLibrary("MQ2DotNetLoader.dll");
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfShutdownPlugin"), Marshal.GetFunctionPointerForDelegate(_shutdownPlugin));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnCleanUI"), Marshal.GetFunctionPointerForDelegate(_onCleanUI));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnReloadUI"), Marshal.GetFunctionPointerForDelegate(_onReloadUI));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnDrawHUD"), Marshal.GetFunctionPointerForDelegate(_onDrawHUD));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfSetGameState"), Marshal.GetFunctionPointerForDelegate(_setGameState));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnPulse"), Marshal.GetFunctionPointerForDelegate(_onPulse));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnIncomingChat"), Marshal.GetFunctionPointerForDelegate(_onIncomingChat));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnWriteChatColor"), Marshal.GetFunctionPointerForDelegate(_onWriteChatColor));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnAddSpawn"), Marshal.GetFunctionPointerForDelegate(_onAddSpawn));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnRemoveSpawn"), Marshal.GetFunctionPointerForDelegate(_onRemoveSpawn));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnAddGroundItem"), Marshal.GetFunctionPointerForDelegate(_onAddGroundItem));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnRemoveGroundItem"), Marshal.GetFunctionPointerForDelegate(_onRemoveGroundItem));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfBeginZone"), Marshal.GetFunctionPointerForDelegate(_beginZone));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfEndZone"), Marshal.GetFunctionPointerForDelegate(_endZone));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnZoned"), Marshal.GetFunctionPointerForDelegate(_onZoned));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnMacroStart"), Marshal.GetFunctionPointerForDelegate(_onMacroStart));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnMacroStop"), Marshal.GetFunctionPointerForDelegate(_onMacroStop));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnLoadPlugin"), Marshal.GetFunctionPointerForDelegate(_onLoadPlugin));
                Marshal.WriteIntPtr(NativeMethods.GetProcAddress(hDll, "g_pfOnUnloadPlugin"), Marshal.GetFunctionPointerForDelegate(_onUnloadPlugin));

#if DEBUG
                MQ2.WriteChatGeneral($"Loaded debug version {GitVersionInformation.MajorMinorPatch} ({GitVersionInformation.ShortSha})");
#else
                MQ2.WriteChatGeneral($"Loaded version {GitVersionInformation.MajorMinorPatch} ({GitVersionInformation.ShortSha})");
#endif

                // Add command to load/unload .net plugins
                _commands.AddCommand("/netplugin", NetPluginCommand);

                // And command to run/end .net programs
                _commands.AddCommand("/netrun", NetRunCommand);
                _commands.AddCommand("/netend", NetEndCommand);

                // Load any plugins that are set to autoload. Fuck ini files
                try
                {
                    foreach (Match match in Regex.Matches(File.ReadAllText(_configFilePath), @"(?<name>\w+)=1"))
                        LoadPlugin(match.Groups["name"].Value, true, false);
                }
                catch (FileNotFoundException)
                {
                    // It's OK if this doesn't exist, it gets created automatically by WritePrivateProfile and this is the only place it gets read
                }

                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private static void ShutdownPlugin()
        {
            _commands.Dispose();

            // Unload each app domain (by disposing of it)
            foreach (var appDomain in _appDomains.ToList())
            {
                appDomain.Dispose();
                _appDomains.Remove(appDomain);
            }
        }

        #region .NET plugin commands
        private static void NetPluginCommand(params string[] args)
        {
            // Argument parsing & validation
            if (args.Length == 0 || args.Length > 3
                                 || (args.Length >= 2 && args[1] != "noauto" && args[1] != "unload")
                                 || (args.Length == 3 && (args[1] != "unload" || args[2] != "noauto")))
            {
                MQ2.WriteChatPlugin("Usage: /netplugin <plugin> [unload] [noauto]");
                MQ2.WriteChatPlugin("Usage: /netplugin list");
                return;
            }

            // List command
            if (args[0] == "list")
            {
                foreach (var plugin in Plugins.Keys)
                    MQ2.WriteChatPlugin(plugin);
                MQ2.WriteChatPlugin($"{Plugins.Count} plugins loaded");
                return;
            }

            var pluginName = args[0];
            var unload = args.Length >= 2 && args[1] == "unload";
            var noauto = (args.Length >= 2 && args[1] == "noauto") || (args.Length >= 3 && args[2] == "noauto");

            // Load/unload command
            if (unload)
                UnloadPlugin(pluginName, noauto);
            else
                LoadPlugin(pluginName, noauto, true);
        }

        private static void LoadPlugin(string pluginName, bool noauto, bool showSuccessMessage)
        {
            if (Plugins.ContainsKey(pluginName))
            {
                MQ2.WriteChatPlugin($"{pluginName} already loaded");
                return;
            }

            try
            {
                // First look for it in its own folder
                var pluginFilePath = $"{_resourcePath}\\Plugins\\{pluginName}\\{pluginName}.dll";
                if (!File.Exists(pluginFilePath))
                {
                    // Then in the plugins folder
                    pluginFilePath = $"{_resourcePath}\\Plugins\\{pluginName}.dll";
                    if (!File.Exists(pluginFilePath))
                    {
                        MQ2.WriteChatPluginError($"Couldn't find plugin: {pluginName}");
                        return;
                    }
                }

                _appDomains.Add(PluginAppDomain.Load(pluginFilePath, pluginName + "PluginDomain"));
                NativeMethods.WritePrivateProfileString("Plugins", pluginName, noauto ? "0" : "1", _configFilePath);

                // No need to spam for each plugin that's loaded automatically from the ini file
                if (showSuccessMessage)
                    MQ2.WriteChatPlugin($"{pluginName} loaded");
            }
            catch (Exception e)
            {
                MQ2.WriteChatPluginError($"Error loading plugin {pluginName}:");
                MQ2.WriteChatPluginError(e.ToString());
            }
        }

        private static void UnloadPlugin(string pluginName, bool noauto)
        {
            if (Plugins.TryGetValue(pluginName, out var pluginAppDomain))
            {
                pluginAppDomain.Dispose();
                _appDomains.Remove(pluginAppDomain);

                if (noauto)
                    NativeMethods.WritePrivateProfileString("Plugins", pluginName, "0", _configFilePath);

                MQ2.WriteChatPlugin($"{pluginName} unloaded");
            }
            else
            {
                MQ2.WriteChatPlugin($"{pluginName} is not loaded");
            }
        }
        #endregion

        #region .NET program commands
        private static void NetRunCommand(params string[] args)
        {
            if (args.Length == 0)
            {
                MQ2.WriteChatProgram("Usage: /netrun <program> [<arg1> <arg2> ...]");
                return;
            }

            StartProgram(args[0], args.Skip(1).ToArray());
        }

        private static void NetEndCommand(params string[] args)
        {
            if (args.Length != 1)
            {
                MQ2.WriteChatProgram("Usage: /netend <program|*>");
                return;
            }

            if (Programs.Count == 0)
            {
                MQ2.WriteChatProgram("No programs running");
                return;
            }

            var programName = args[0];

            if (programName == "*")
            {
                foreach (var kvp in Programs.ToArray())
                    EndProgram(kvp.Key);
            }
            else
            {
                if (Programs.ContainsKey(programName))
                    EndProgram(programName);
                else
                    MQ2.WriteChatProgram($"{programName} is not running");
            }
        }

        private static void StartProgram(string programName, string[] args)
        {
            if (Programs.ContainsKey(programName))
            {
                MQ2.WriteChatProgram($"{programName} already running");
                return;
            }

            try
            {
                // First look for it in its own folder
                var programFilePath = $"{_resourcePath}\\Programs\\{programName}\\{programName}.dll";
                if (!File.Exists(programFilePath))
                {
                    // Then in the programs folder
                    programFilePath = $"{_resourcePath}\\Programs\\{programName}.dll";
                    if (!File.Exists(programFilePath))
                    {
                        MQ2.WriteChatProgramError($"Couldn't find program: {programName}");
                        return;
                    }
                }

                var programAppDomain = ProgramAppDomain.Load(programFilePath, programName + "ProgramDomain");
                _appDomains.Add(programAppDomain);

                MQ2.WriteChatProgram($"Starting {programName}");

                programAppDomain.Start(args);
            }
            catch (Exception e)
            {
                MQ2.WriteChatProgramError($"Error loading program {programName}:");
                MQ2.WriteChatProgramError(e.ToString());
            }
        }

        private static void EndProgram(string programName)
        {
            if (Programs.TryGetValue(programName, out var programAppDomain))
            {
                // Try to cancel cleanly
                programAppDomain.Cancel();

                if (programAppDomain.Status != TaskStatus.Canceled)
                    MQ2.WriteChatProgramWarning($"{programName} did not respond to cancellation");

                // Regardless, unload the AppDomain which will shut everything down
                programAppDomain.Dispose();
                _appDomains.Remove(programAppDomain);

                MQ2.WriteChatProgram($"{programName} stopped");
            }
            else
            {
                MQ2.WriteChatProgram($"{programName} is not running");
            }
        }
        #endregion

        #region Plugin API callbacks, each of these will invoke the corresponding method on each loaded AppDomain
        private static void OnPulse()
        {
            _eventLoopContext.DoEvents(true);

            foreach (var appDomain in _appDomains.ToList())
            {
                try
                {
                    appDomain.OnPulse();

                    if (appDomain is ProgramAppDomain program && program.Finished)
                    {
                        MQ2.WriteChatProgram($"{program.Name} finished: {program.Status}");
                        appDomain.Dispose();
                        _appDomains.Remove(appDomain);
                    }
                }
                catch (AppDomainUnloadedException) { }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnPulse in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void BeginZone()
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeBeginZone();
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in BeginZone in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void EndZone()
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeEndZone();
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in EndZone in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnAddGroundItem(IntPtr pNewGroundItem)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnAddGroundItem(pNewGroundItem);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnAddGroundItem in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnAddSpawn(IntPtr pNewSpawn)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnAddSpawn(pNewSpawn);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnAddSpawn in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnCleanUI()
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnCleanUI();
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnCleanUI in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnDrawHUD()
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnDrawHUD();
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnDrawHUD in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static uint OnIncomingChat(string line, uint color)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnChatEQ(line);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnChatEQ in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }

                try
                {
                    appDomain.InvokeOnChat(line);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnChat in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }

            return 0;
        }

        private static void OnReloadUI()
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnReloadUI();
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnReloadUI in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnRemoveGroundItem(IntPtr pGroundItem)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnRemoveGroundItem(pGroundItem);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnRemoveGroundItem in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnRemoveSpawn(IntPtr pSpawn)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnRemoveSpawn(pSpawn);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnRemoveSpawn in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static uint OnWriteChatColor(string line, uint color, uint filter)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnChatMQ2(line);
                }
                catch
                {
                    // Writing an error message in chat caused by an error message in chat is probably not a great idea
                    //MQ2.WriteChatPluginError($"Exception in OnChatMQ2 in {appDomain.Name}");
                    //MQ2.WriteChatPluginError(e.ToString());
                }

                try
                {
                    appDomain.InvokeOnChat(line);
                }
                catch
                {
                    //MQ2.WriteChatPluginError($"Exception in OnChat in {appDomain.Name}");
                    //MQ2.WriteChatPluginError(e.ToString());
                }
            }

            return 0;
        }

        private static void OnZoned()
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnZoned();
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnZoned in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnMacroStart(string name)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnMacroStart(name);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnMacroStart in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnMacroStop(string name)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnMacroStop(name);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnMacroStop in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnLoadPlugin(string name)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnLoadPlugin(name);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnLoadPlugin in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void OnUnloadPlugin(string name)
        {
            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeOnUnloadPlugin(name);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in OnUnloadPlugin in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }

        private static void SetGameState(uint gameState)
        {
            var gameStateEnum = Enum.IsDefined(typeof(GameState), gameState)
                ? (GameState)gameState
                : GameState.Unknown;

            foreach (var appDomain in _appDomains)
            {
                try
                {
                    appDomain.InvokeSetGameState(gameStateEnum);
                }
                catch (Exception e)
                {
                    MQ2.WriteChatPluginError($"Exception in SetGameState in {appDomain.Name}");
                    MQ2.WriteChatPluginError(e.ToString());
                }
            }
        }
        #endregion
    }
}
