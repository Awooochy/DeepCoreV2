using System.Linq;
using System.Text;
using ExitGames.Client.Photon;

namespace DeepCore.Client.Module.Photon
{
    internal class ChatBoxLogger
    {
        public static bool isEnabled = false;
        internal static bool OnEvent(EventData param_1)
        {
            if (isEnabled)
            {
                if (param_1.Code == 43)
                {
                    byte[] byteArray = Misc.SerializationUtil.ToByteArray(param_1.CustomData);
                    DeepConsole.LogConsole("Photon", $"Someone say: {ConvertBytesToText(byteArray)}");
                }
            }
            return true;
        }
        public static string PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("");
            foreach (var b in bytes)
            {
                sb.Append(b);
            }
            return sb.ToString();
        }
        public static string ConvertBytesToText(byte[] bytes)
        {
            try
            {
                string result = Encoding.UTF8.GetString(bytes).TrimEnd('\0');
                result = new string(result.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
                if (result.Any(c => char.IsControl(c) && !char.IsWhiteSpace(c)))
                {
                    return "[Non-printable characters detected]";
                }
                return result;
            }
            catch
            {
                return "[Invalid Encoding]";
            }
        }
    }
}
