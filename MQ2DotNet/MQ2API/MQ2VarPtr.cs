using System;
using System.Runtime.InteropServices;

namespace MQ2DotNet.MQ2API
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct NativeMQ2VarPtr { }

    public class MQ2VarPtr
    {
        public NativeMQ2VarPtr VarPtr { get; }

        public int Int
        {
            get { return NativeMethods.MQVarPtr__GetInt(VarPtr); }
            set { NativeMethods.MQVarPtr__SetInt(VarPtr, value); }
        }

        public uint Dword
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

        public long Int64
        {
            get { return NativeMethods.MQVarPtr__GetInt64(VarPtr); }
            set { NativeMethods.MQVarPtr__SetInt64(VarPtr, value); }
        }

        public ulong UInt64
        {
            get { return NativeMethods.MQVarPtr__GetUInt64(VarPtr); }
            set { NativeMethods.MQVarPtr__SetUInt64(VarPtr, value); }
        }

        public uint HighPart
        {
            get { return NativeMethods.MQVarPtr__GetHighPart(VarPtr); }
        }

        public uint LowPart
        {
            get { return NativeMethods.MQVarPtr__GetLowPart(VarPtr); }
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
            public static extern int MQVarPtr__GetInt(in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetInt(in NativeMQ2VarPtr varPtr, int value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern uint MQVarPtr__GetDword(in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetDword(in NativeMQ2VarPtr varPtr, uint value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr MQVarPtr__GetPtr(in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetPtr(in NativeMQ2VarPtr varPtr, IntPtr value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern double MQVarPtr__GetDouble(in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetDouble(in NativeMQ2VarPtr varPtr, double value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern float MQVarPtr__GetFloat(in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetFloat(in NativeMQ2VarPtr varPtr, float value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern long MQVarPtr__GetInt64(in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetInt64(in NativeMQ2VarPtr varPtr, long value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern ulong MQVarPtr__GetUInt64(in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQVarPtr__SetUInt64(in NativeMQ2VarPtr varPtr, ulong value);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern uint MQVarPtr__GetHighPart(in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern uint MQVarPtr__GetLowPart(in NativeMQ2VarPtr varPtr);
        }
    }
}