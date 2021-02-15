using System;
using System.Runtime.InteropServices;

namespace SessionProtocolInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetProtocolType());
            Console.Read();
        }

        public static ProtocolType GetProtocolType()
        {
            var sessionId = -1;
            var protocolId = 0;

            System.IntPtr buffer = IntPtr.Zero;
            uint bytesReturned;
            if (!WTSQuerySessionInformation(IntPtr.Zero, sessionId, WTS_INFO_CLASS.WTSClientProtocolType, out buffer, out bytesReturned))
            {
                throw new InvalidOperationException("WTSQuerySessionInformation failed");
            }
            
            protocolId = Marshal.ReadInt32(buffer);

            return (ProtocolType)protocolId;
        }

        public enum ProtocolType
        {
            Console = 0,
            ICA = 1,
            RDP = 2
        }

        [DllImport("Wtsapi32.dll")]
        static extern bool WTSQuerySessionInformation(
            System.IntPtr hServer, int sessionId, WTS_INFO_CLASS wtsInfoClass, out System.IntPtr ppBuffer, out uint pBytesReturned);

        enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo
        }
    }
}
