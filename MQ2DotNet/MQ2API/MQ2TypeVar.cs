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
            NativeMethods.MQTypeVar__GetVarPtr(typeVar, out var varPtr);

            VarPtr = new MQ2VarPtr(varPtr);
            Type = new MQ2Type(NativeMethods.MQTypeVar__GetType(typeVar));
        }

        public MQ2TypeVar(string typeName, MQ2VarPtr varPtr)
        {
            VarPtr = varPtr;
            Type = new MQ2Type(NativeMethods.FindMQ2DataType(typeName));
        }

        public bool TryGetMember(string memberName, string index, out MQ2TypeVar typeVar)
        {
            return Type.TryGetMember(VarPtr, memberName, index, out typeVar);
        }

        public override string ToString()
        {
            return Type.ToString(VarPtr);
        }

        private static class NativeMethods
        {
            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr MQTypeVar__GetType(in NativeMQ2TypeVar typeVar);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQTypeVar__GetVarPtr(in NativeMQ2TypeVar typeVar, out NativeMQ2VarPtr varPtr);

            [DllImport("MQ2Main.dll", EntryPoint = "FindMQ2DataType", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr FindMQ2DataType([MarshalAs(UnmanagedType.LPStr)] string name);
        }
    }
}
