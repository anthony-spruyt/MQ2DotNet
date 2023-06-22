using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace MQ2DotNet.MQ2API
{
    [StructLayout(LayoutKind.Sequential, Size = 40)]
    public struct NativeMQ2TypeVar { }

    [PublicAPI]
    public class MQ2TypeVar
    {
        private readonly NativeMQ2TypeVar _typeVar;

        public MQ2Type Type { get; }

        public MQ2VarPtr VarPtr { get; }

        public MQ2TypeVar(NativeMQ2TypeVar typeVar)
        {
            _typeVar = typeVar;

            var varPtr = NativeMethods.MQTypeVar__GetVarPtr(_typeVar);

            VarPtr = new MQ2VarPtr(varPtr);

            var type = NativeMethods.MQTypeVar__GetType(_typeVar);

            Type = new MQ2Type(type);
        }

        public MQ2TypeVar(string typeName, MQ2VarPtr varPtr)
        {
            IntPtr intPtr = NativeMethods.FindMQ2DataType(typeName);

            Type = new MQ2Type(intPtr);

            VarPtr = varPtr;
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
            public static extern IntPtr MQTypeVar__GetType(NativeMQ2TypeVar pThis);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern NativeMQ2VarPtr MQTypeVar__GetVarPtr(NativeMQ2TypeVar pThis);

            [DllImport("MQ2Main.dll", EntryPoint = "FindMQ2DataType", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr FindMQ2DataType(string name);
        }
    }
}
