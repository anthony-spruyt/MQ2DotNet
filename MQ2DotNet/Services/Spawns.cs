using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using MQ2DotNet.MQ2API;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2DotNet.Utility;

namespace MQ2DotNet.Services
{
    /// <summary>
    /// Contains utility methods and properties relating to spawns
    /// </summary>
    [PublicAPI]
    public class Spawns
    {
# if WIN64
        private const int NEXT_SPAWN_PTR_SIZE = 16;
# else
        private const int NEXT_SPAWN_PTR_SIZE = 8;
#endif

#if WIN64
        private const int NEXT_GROUND_ITEM_PTR_SIZE = 8;
#else
        private const int NEXT_GROUND_ITEM_PTR_SIZE = 4;
#endif

        private readonly MQ2TypeFactory _mq2TypeFactory;

        internal Spawns(MQ2TypeFactory mq2TypeFactory)
        {
            _mq2TypeFactory = mq2TypeFactory;
        }

        /// <summary>
        /// All spawns in the current zone
        /// </summary>
        public IEnumerable<SpawnType> All
        {
            get
            {
                // Broken, doesnt always crash but returns bunch of empty / default data. The count seems valid since in guild hall there were only 8 spawns (7npc + character) at the time.
                // So the count is fine, but the data type mapping/binding is bad.
                var hDll = NativeMethods.LoadLibrary("eqlib.dll");
                var ppSpawnManager = Marshal.ReadIntPtr(NativeMethods.GetProcAddress(hDll, "pSpawnManager"));
                var pSpawnManager = Marshal.ReadIntPtr(ppSpawnManager);
                var pSpawn = Marshal.ReadIntPtr(pSpawnManager + NEXT_SPAWN_PTR_SIZE);

                while (pSpawn != IntPtr.Zero)
                {
                    yield return new SpawnType(_mq2TypeFactory, pSpawn);
                    pSpawn = Marshal.ReadIntPtr(pSpawn + NEXT_SPAWN_PTR_SIZE);
                }
            }
        }

        /// <summary>
        /// All ground spawns in the current zone
        /// </summary>
        public IEnumerable<GroundType> AllGround
        {
            get
            {
                // This hard crashes the app currently.
                var pGroundItemListManager = GetItemList();
                var pGroundItem = Marshal.ReadIntPtr(pGroundItemListManager);

                while (pGroundItem != IntPtr.Zero)
                {
                    yield return new GroundType(_mq2TypeFactory, pGroundItem);
                    pGroundItem = Marshal.ReadIntPtr(pGroundItem + NEXT_GROUND_ITEM_PTR_SIZE);
                }
            }
        }

        #region Unmanaged imports
        [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetItemList();
        #endregion
    }
}