using System;
using System.Runtime.InteropServices;

namespace MQ2DotNet.MQ2API
{
    [StructLayout(LayoutKind.Explicit, Size = 40)]
    public struct NativeMQ2TypeVar { }

    public class MQ2TypeVar
    {
        public MQ2Type Type { get; }

        public MQ2VarPtr VarPtr { get; }

        public MQ2TypeVar(NativeMQ2TypeVar typeVar)
        {
            Type = new MQ2Type(NativeMethods.MQTypeVar__GetType(typeVar));

            NativeMethods.MQTypeVar__GetVarPtr(typeVar, out var varPtr);
            VarPtr = new MQ2VarPtr(varPtr);
        }

        public bool TryGetMember(string memberName, string index, out MQ2TypeVar typeVar)
        {
            return Type.TryGetMember(VarPtr, memberName, index, out typeVar);
        }

        public override string ToString()
        {
            return Type.ToString(VarPtr);
        }

        internal static MQ2TypeVar FromGroundItemPtr(IntPtr pGround)
        {
            if (!NativeMethods.MQTypeVar__FromGroundItemPtr(pGround, out var nativeTypeVar))
            {
                throw new ArgumentException($"Invalid EQObject pointer.", nameof(pGround));
            }

            return new MQ2TypeVar(nativeTypeVar);
        }

        internal static MQ2TypeVar FromSpawnInfoPtr(IntPtr pSpawn)
        {
            if (!NativeMethods.MQTypeVar__FromSpawnInfoPtr(pSpawn, out var nativeTypeVar))
            {
                throw new ArgumentException($"Invalid EQObject pointer.", nameof(pSpawn));
            }

            return new MQ2TypeVar(nativeTypeVar);
        }

        private static class NativeMethods
        {
            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr MQTypeVar__GetType(in NativeMQ2TypeVar typeVar);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQTypeVar__GetVarPtr(in NativeMQ2TypeVar typeVar, out NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQTypeVar__FromSpawnInfoPtr(IntPtr @object, out NativeMQ2TypeVar typeVar);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQTypeVar__FromGroundItemPtr(IntPtr @object, out NativeMQ2TypeVar typeVar);
        }
    }
}
