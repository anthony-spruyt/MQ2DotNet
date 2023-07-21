using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MQ2DotNet.MQ2API
{
    public class MQ2Type
    {
        public IntPtr IntPtr { get; }

        public MQ2Type(IntPtr intPtr)
        {
            if (intPtr == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(intPtr));
            }

            IntPtr = intPtr;
        }

        public bool TryGetMember(MQ2VarPtr varPtr, string memberName, string index, out MQ2TypeVar typeVar)
        {
            if (IntPtr == IntPtr.Zero)
            {
                typeVar = null;

                return false;
            }

            if (!NativeMethods.MQ2Type__GetMember(IntPtr, varPtr.VarPtr, memberName, index, out var nativeTypeVar))
            {
                typeVar = null;

                return false;
            }

            typeVar = new MQ2TypeVar(nativeTypeVar);

            return true;
        }

        public string ToString(MQ2VarPtr varPtr)
        {
            if (IntPtr == IntPtr.Zero)
            {
                return ToString();
            }

            var stringBuilder = new StringBuilder(2048);

            if (!NativeMethods.MQ2Type__ToString(IntPtr, varPtr.VarPtr, stringBuilder))
            {
                return ToString();
            }

            return stringBuilder.ToString();
        }

        private static class NativeMethods
        {
            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Type__FromData(IntPtr type, out NativeMQ2VarPtr varPtr, ref NativeMQ2TypeVar typeVar);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Type__FromString(IntPtr type, out NativeMQ2VarPtr varPtr, [MarshalAs(UnmanagedType.LPStr)] string source);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQ2Type__InitVariable(IntPtr type, out NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void MQ2Type__FreeVariable(IntPtr type, in NativeMQ2VarPtr varPtr);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Type__GetMember(IntPtr type, in NativeMQ2VarPtr varPtr, [MarshalAs(UnmanagedType.LPStr)] string memberName, [MarshalAs(UnmanagedType.LPStr)] string index, out NativeMQ2TypeVar typeVar);

            [DllImport("MQ2DotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool MQ2Type__ToString(IntPtr type, in NativeMQ2VarPtr varPtr, [MarshalAs(UnmanagedType.LPStr)] StringBuilder destination);
        }
    }
}
