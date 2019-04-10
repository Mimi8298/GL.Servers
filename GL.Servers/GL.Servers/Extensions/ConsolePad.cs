namespace GL.Servers.Extensions
{
    public class ConsolePad
    {
        public static string Padding(string Message, int Limit = 25, string ReplaceWith = "...")
        {
            if (Message.Length > Limit)
            {
                Message = Message.Remove(Message.Length - (Message.Length - Limit + ReplaceWith.Length), Message.Length - Limit + ReplaceWith.Length) + ReplaceWith;
            }
            else if (Message.Length < Limit)
            {
                int Length      = Limit - Message.Length;

                int LeftPad     = Length / 2;
                int RightPad    = Length / 2;

                for (int i = 0; i < Length; i++)
                {
                    Message += " ";
                }
            }

            return Message;
        }
    }
}