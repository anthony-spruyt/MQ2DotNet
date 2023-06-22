using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace MQ2DotNet.MQ2API
{
    [StructLayout(LayoutKind.Sequential, Size = 32)]
    public struct NativeMQ2VarPtr { }
    [PublicAPI]
    public class MQ2VarPtr
    {
        public NativeMQ2VarPtr VarPtr { get; }

        public int Int
        {
            get { return NativeMethods.MQVarPtr__GetInt(VarPtr); }
            set { NativeMethods.MQVarPtr__SetInt(VarPtr, value); }
        }

        public UInt32 Dword
        {
            get { return NativeMethods.MQVarPtr__GetDword(VarPtr); }
            set { NativeMethods.MQVarPtr__SetDword(VarPtr, value); }
        }

        public IntPtr Ptr
        {
            get { return NativeMethods.MQVarPtr__GetPtr(VarPtr); }
            set { NativeMethods.MQVarPtr__SetPtr(VarPtr, value); }
        }

        public double Double
        {
            get { return NativeMethods.MQVarPtr__GetDouble(VarPtr); }
            set { NativeMethods.MQVarPtr__SetDouble(VarPtr, value); }
        }

        public float Float
        {
            get { return NativeMethods.MQVarPtr__GetFloat(VarPtr); }
            set { NativeMethods.MQVarPtr__SetFloat(VarPtr, value); }
        }

        public Int64 Int64
        {
            get { return NativeMethods.MQVarPtr__GetInt64(VarPtr); }
            set { NativeMethods.MQVarPtr__SetInt64(VarPtr, value); }
        }

        public UInt64 UInt64
        {
            get { return NativeMethods.MQVarPtr__GetUInt64(VarPtr); }
            set { NativeMethods.MQVarPtr__SetUInt64(VarPtr, value); }
        }

        public MQ2VarPtr(NativeMQ2VarPtr varPtr)
        {
            VarPtr = varPtr;
        }

        public MQ2VarPtr(IntPtr intPtr)
        {
            VarPtr = default;

            Ptr = intPtr;
        }

        private static class NativeMethods
        {
            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Int32 MQVarPtr__GetInt(NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetInt(NativeMQ2VarPtr varPtr, Int32 value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern UInt32 MQVarPtr__GetDword(NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetDword(NativeMQ2VarPtr varPtr, UInt32 value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr MQVarPtr__GetPtr(NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetPtr(NativeMQ2VarPtr varPtr, IntPtr value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern double MQVarPtr__GetDouble(NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetDouble(NativeMQ2VarPtr varPtr, double value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern float MQVarPtr__GetFloat(NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetFloat(NativeMQ2VarPtr varPtr, float value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern Int64 MQVarPtr__GetInt64(NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetInt64(NativeMQ2VarPtr varPtr, Int64 value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern UInt64 MQVarPtr__GetUInt64(NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetUInt64(NativeMQ2VarPtr varPtr, UInt64 value);
        }
    }
}