using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CookieCode.Consoles
{
    // http://domoticx.com/terminal-codes-ansivt100/
    // https://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html
    // https://stackoverflow.com/questions/4842424/list-of-ansi-color-escape-sequences
    public static partial class Ansi
    {
        private const string Esc = "\u001b";

        static Ansi()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static bool IsEnabled { get; set; } = true;

        public static string Reset => IsEnabled ? $"{Ansi.Esc}[0m" : string.Empty;
        public static string Bold => IsEnabled ? $"{Ansi.Esc}[1m" : string.Empty;
        public static string Dim => IsEnabled ? $"{Ansi.Esc}[2m" : string.Empty;
        public static string Underline => IsEnabled ? $"{Ansi.Esc}[4m" : string.Empty;
        public static string Blink => IsEnabled ? $"{Ansi.Esc}[5m" : string.Empty;
        public static string Inverse => IsEnabled ? $"{Ansi.Esc}[7m" : string.Empty;
        public static string Hidden => IsEnabled ? $"{Ansi.Esc}[8m" : string.Empty;

        public static class Clear
        {
            /// <summary>Clear entire screen</summary>
            public static string Screen => IsEnabled ? $"{Ansi.Esc}[2J" : string.Empty;

            /// <summary>Clear from cursor until end of screen</summary>
            public static string ScreenEnd => IsEnabled ? $"{Ansi.Esc}[0J" : string.Empty;

            /// <summary>Clear from cursor until end of screen</summary>
            public static string ScreenStart => IsEnabled ? $"{Ansi.Esc}[1J" : string.Empty;

            /// <summary>Clear whole line(cursor position unchanged)</summary>
            public static string Line => IsEnabled ? $"{Ansi.Esc}[2K" : string.Empty;

            /// <summary>Clear line from current cursor position to end of line</summary>
            public static string LineRight => IsEnabled ? $"{Ansi.Esc}[0K" : string.Empty;

            /// <summary>Clear line from beginning to current cursor position</summary>
            public static string LineLeft => IsEnabled ? $"{Ansi.Esc}[1K" : string.Empty;
        }

        public static class Cursor
        {
            /// <summary>Move cursor to home (1, 1)</summary>
            public static string Home => IsEnabled
                ? $"{Ansi.Esc}[H"
                : string.Empty;

            /// <summary>Move cursor up by y</summary>
            public static string Up(int lines = 1) => IsEnabled
                ? $"{Ansi.Esc}[{lines}A"
                : string.Empty;

            /// <summary>Move cursor down by y</summary>
            public static string Down(int lines = 1) => IsEnabled
                ? $"{Ansi.Esc}[{lines}B"
                : string.Empty;

            /// <summary>Move cursor right by x</summary>
            public static string Right(int columns = 1) => IsEnabled
                ? $"{Ansi.Esc}[{columns}C"
                : string.Empty;

            /// <summary>Move cursor left by x</summary>
            public static string Left(int columns = 1) => IsEnabled
                ? $"{Ansi.Esc}[{columns}D"
                : string.Empty;

            /// <summary>Move cursor to beginning of line y lines down</summary>
            public static string ToNextLine(int lines = 1) => IsEnabled
                ? $"{Ansi.Esc}[{lines}E"
                : string.Empty;

            /// <summary>Move cursor to beginning of line y lines up</summary>
            public static string ToPrevLine(int lines = 1) => IsEnabled
                ? $"{Ansi.Esc}[{lines}F"
                : string.Empty;

            /// <summary>Move cursor to x, y</summary>
            public static string To(int x, int y) => IsEnabled
                ? $"{Ansi.Esc}[{x};{y}H"
                : string.Empty;
        }

        public static string Bg(ConsoleColor fore)
        {
            int index = (int)fore;
            var ansi = $"{Ansi.Esc}[{(index < 8 ? "4" : "10")}{index % 8}m";
            return ansi;
        }

        public static string Fg(ConsoleColor fore)
        {
            int index = (int)fore;
            var ansi = $"{Ansi.Esc}[{(index < 8 ? "3" : "9")}{index % 8}m";
            return ansi;
        }

        public static string FgForBg(ConsoleColor back)
        {
            var fore = back.FgForBg();
            var fg = Fg(fore);
            return fg;
        }

        public static string Bg(Color256 index)
        {
            var ansi = $"{Ansi.Esc}[48;5;{(int)index}m";
            return ansi;
        }

        public static string Fg(Color256 index)
        {
            var ansi = $"{Ansi.Esc}[38;5;{(int)index}m";
            return ansi;
        }

        public static string FgForBg(Color256 back)
        {
            var fore = back.FgForBg();
            var ansi = Ansi.Fg(fore);
            return ansi;
        }

        public static string Bg(Color back)
        {
            return IsEnabled
                ? $"{Ansi.Esc}[48;2;{back.R};{back.G};{back.B}m"
                : string.Empty;
        }

        public static string Fg(Color fore)
        {
            return IsEnabled
                ? $"{Ansi.Esc}[38;2;{fore.R};{fore.G};{fore.B}m"
                : string.Empty;
        }

        public static string FgForBg(Color back)
        {
            var fore = (((back.R + back.B + back.G) / 3) > 128) ? Color.Black : Color.White;
            var ansi = Ansi.Fg(fore);
            return ansi;
        }

        public enum Color256
        {
            Aquamarine1 = 122,
            Aquamarine1_b = 86,
            Aquamarine3 = 79,
            Blue1 = 21,
            Blue2 = 20,
            Blue3 = 19,
            BlueViolet = 57,
            CadetBlue = 72,
            CadetBlue_b = 73,
            Chartreuse1 = 118,
            Chartreuse2 = 112,
            Chartreuse2_b = 82,
            Chartreuse3 = 70,
            Chartreuse3_b = 76,
            Chartreuse4 = 64,
            CornflowerBlue = 69,
            Cornsilk1 = 230,
            Cyan1 = 51,
            Cyan2 = 50,
            Cyan3 = 43,
            DarkBlue = 18,
            DarkCyan = 36,
            DarkGoldenrod = 136,
            DarkGreen = 22,
            DarkKhaki = 143,
            DarkMagenta = 90,
            DarkMagenta_b = 91,
            DarkOliveGreen1 = 191,
            DarkOliveGreen1_b = 192,
            DarkOliveGreen2 = 155,
            DarkOliveGreen3 = 107,
            DarkOliveGreen3_b = 113,
            DarkOliveGreen3_c = 149,
            DarkOrange = 208,
            DarkOrange3 = 130,
            DarkOrange3b = 166,
            DarkRed = 52,
            DarkRed_b = 88,
            DarkSeaGreen = 108,
            DarkSeaGreen1 = 158,
            DarkSeaGreen1_b = 193,
            DarkSeaGreen2 = 151,
            DarkSeaGreen2_b = 157,
            DarkSeaGreen3 = 115,
            DarkSeaGreen3_b = 150,
            DarkSeaGreen4 = 65,
            DarkSeaGreen4_b = 71,
            DarkSlateGray1 = 123,
            DarkSlateGray2 = 87,
            DarkSlateGray3 = 116,
            DarkTurquoise = 44,
            DarkViolet = 128,
            DarkViolet_b = 92,
            DeepPink1 = 198,
            DeepPink1_b = 199,
            DeepPink2 = 197,
            DeepPink3 = 161,
            DeepPink3_b = 162,
            DeepPink4 = 125,
            DeepPink4_b = 53,
            DeepPink4_c = 89,
            DeepSkyBlue1 = 39,
            DeepSkyBlue2 = 38,
            DeepSkyBlue3 = 31,
            DeepSkyBlue3_b = 32,
            DeepSkyBlue4 = 23,
            DeepSkyBlue4_b = 24,
            DeepSkyBlue4_c = 25,
            DodgerBlue1 = 33,
            DodgerBlue2 = 27,
            DodgerBlue3 = 26,
            Gold1 = 220,
            Gold3 = 142,
            Gold3_b = 178,
            Green1 = 46,
            Green3 = 34,
            Green3_b = 40,
            Green4 = 28,
            GreenYellow = 154,
            Grey0 = 16,
            Grey100 = 231,
            Grey11 = 234,
            Grey15 = 235,
            Grey19 = 236,
            Grey23 = 237,
            Grey27 = 238,
            Grey3 = 232,
            Grey30 = 239,
            Grey35 = 240,
            Grey37 = 59,
            Grey39 = 241,
            Grey42 = 242,
            Grey46 = 243,
            Grey50 = 244,
            Grey53 = 102,
            Grey54 = 245,
            Grey58 = 246,
            Grey62 = 247,
            Grey63 = 139,
            Grey66 = 248,
            Grey69 = 145,
            Grey7 = 233,
            Grey70 = 249,
            Grey74 = 250,
            Grey78 = 251,
            Grey82 = 252,
            Grey84 = 188,
            Grey85 = 253,
            Grey89 = 254,
            Grey93 = 255,
            Honeydew2 = 194,
            HotPink = 205,
            HotPink_b = 206,
            HotPink2 = 169,
            HotPink3 = 132,
            HotPink3_b = 168,
            IndianRed = 131,
            IndianRed_b = 167,
            IndianRed1 = 203,
            IndianRed1_b = 204,
            Khaki1 = 228,
            Khaki3 = 185,
            LightCoral = 210,
            LightCyan1 = 195,
            LightCyan3 = 152,
            LightGoldenrod1 = 227,
            LightGoldenrod2 = 186,
            LightGoldenrod2_b = 221,
            LightGoldenrod3 = 179,
            LightGoldenrod3_b = 222,
            LightGreen = 119,
            LightGreen_b = 120,
            LightPink1 = 217,
            LightPink3 = 174,
            LightPink4 = 95,
            LightSalmon1 = 216,
            LightSalmon3 = 137,
            LightSalmon3_b = 173,
            LightSeaGreen = 37,
            LightSkyBlue1 = 153,
            LightSkyBlue3 = 109,
            LightSkyBlue3_b = 110,
            LightSlateBlue = 105,
            LightSlateGrey = 103,
            LightSteelBlue = 147,
            LightSteelBlue1 = 189,
            LightSteelBlue3 = 146,
            LightYellow3 = 187,
            Magenta1 = 201,
            Magenta2 = 165,
            Magenta2_b = 200,
            Magenta3 = 127,
            Magenta3_b = 163,
            Magenta3_c = 164,
            MediumOrchid = 134,
            MediumOrchid1 = 171,
            MediumOrchid1_b = 207,
            MediumOrchid3 = 133,
            MediumPurple = 104,
            MediumPurple1 = 141,
            MediumPurple2 = 135,
            MediumPurple2_b = 140,
            MediumPurple3 = 97,
            MediumPurple3_b = 98,
            MediumPurple4 = 60,
            MediumSpringGreen = 49,
            MediumTurquoise = 80,
            MediumVioletRed = 126,
            MistyRose1 = 224,
            MistyRose3 = 181,
            NavajoWhite1 = 223,
            NavajoWhite3 = 144,
            NavyBlue = 17,
            Orange1 = 214,
            Orange3 = 172,
            Orange4 = 58,
            Orange4_b = 94,
            OrangeRed1 = 202,
            Orchid = 170,
            Orchid1 = 213,
            Orchid2 = 212,
            PaleGreen1 = 121,
            PaleGreen1_b = 156,
            PaleGreen3 = 114,
            PaleGreen3_b = 77,
            PaleTurquoise1 = 159,
            PaleTurquoise4 = 66,
            PaleVioletRed1 = 211,
            Pink1 = 218,
            Pink3 = 175,
            Plum1 = 219,
            Plum2 = 183,
            Plum3 = 176,
            Plum4 = 96,
            Purple = 129,
            Purple_b = 93,
            Purple3 = 56,
            Purple4 = 54,
            Purple4_b = 55,
            Red1 = 196,
            Red3 = 124,
            Red3_b = 160,
            RosyBrown = 138,
            RoyalBlue1 = 63,
            Salmon1 = 209,
            SandyBrown = 215,
            SeaGreen1 = 84,
            SeaGreen1_b = 85,
            SeaGreen2 = 83,
            SeaGreen3 = 78,
            SkyBlue1 = 117,
            SkyBlue2 = 111,
            SkyBlue3 = 74,
            SlateBlue1 = 99,
            SlateBlue3 = 61,
            SlateBlue3_b = 62,
            SpringGreen1 = 48,
            SpringGreen2 = 42,
            SpringGreen2_b = 47,
            SpringGreen3 = 35,
            SpringGreen3_b = 41,
            SpringGreen4 = 29,
            SteelBlue = 67,
            SteelBlue1 = 75,
            SteelBlue1_b = 81,
            SteelBlue3 = 68,
            Tan = 180,
            Thistle1 = 225,
            Thistle3 = 182,
            Turquoise2 = 45,
            Turquoise4 = 30,
            Violet = 177,
            Wheat1 = 229,
            Wheat4 = 101,
            Yellow1 = 226,
            Yellow2 = 190,
            Yellow3 = 148,
            Yellow3_b = 184,
            Yellow4 = 100,
            Yellow4_b = 106,
        }

        public static bool TryEnableEscapeSequence()
        {
            if (!OperatingSystem.IsWindows())
            {
                return false;
            }

            PInvoke.ConsoleMode inMode;
            var stdin = PInvoke.GetStdHandle(PInvoke.StdHandle.STD_INPUT_HANDLE);
            if (!PInvoke.GetConsoleMode(stdin, out inMode))
            {
                return false;
            }

            inMode |= PInvoke.ConsoleMode.ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            if (!PInvoke.SetConsoleMode(stdin, inMode))
            {
                return false;
            }

            PInvoke.ConsoleMode outMode;
            var stdout = PInvoke.GetStdHandle(PInvoke.StdHandle.STD_OUTPUT_HANDLE);
            if (!PInvoke.GetConsoleMode(stdout, out outMode))
            {
                return false;
            }

            outMode |= PInvoke.ConsoleMode.ENABLE_VIRTUAL_TERMINAL_PROCESSING
                | PInvoke.ConsoleMode.DISABLE_NEWLINE_AUTO_RETURN;

            if (!PInvoke.SetConsoleMode(stdout, outMode))
            {
                return false;
            }

            return true;
        }

        internal static class PInvoke
        {
            private const string Kernel32 = "kernel32.dll";

            internal enum StdHandle : int
            {
                STD_INPUT_HANDLE = -10,
                STD_OUTPUT_HANDLE = -11,
            }

            internal enum ConsoleMode : int
            {
                ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200,
                ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004,
                DISABLE_NEWLINE_AUTO_RETURN = 0x0008,
            }

            [DllImport(Kernel32)]
            internal static extern IntPtr GetStdHandle(StdHandle nStdHandle);

            [DllImport(Kernel32, SetLastError = true)]
            internal static extern bool GetConsoleMode(IntPtr handle, out ConsoleMode mode);

            [DllImport(Kernel32, SetLastError = true)]
            internal static extern bool SetConsoleMode(IntPtr handle, ConsoleMode mode);
        }

        public static class ConsoleTestUtil
        {
            public static void WriteColorTable16()
            {
                var consoleColors = Enum.GetValues<ConsoleColor>()
                    .OrderBy(value => value)
                    .ToArray();

                if (OperatingSystem.IsWindows())
                {
                    const int MinWidth = 145;
                    Console.SetWindowSize(Math.Max(MinWidth, Console.WindowWidth), Console.WindowHeight);
                }

                Console.WriteLine("ConsoleColor Colors");
                foreach (var consoleColor in consoleColors)
                {
                    Console.BackgroundColor = consoleColor;
                    Console.ForegroundColor = consoleColor.FgForBg();
                    Console.Write($"   {(int)consoleColor,2}   ");
                    Console.ResetColor();
                    Console.Write(" ");
                }

                Console.WriteLine();
            }

            /// <summary>
            /// Minimum width of console should be 145 in order to print this
            /// </summary>
            /// <example>
            /// const int MinWidth = 145;
            /// Console.SetWindowSize(Math.Max(MinWidth, Console.WindowWidth), Console.WindowHeight);
            /// Console.WriteLine(Ansi.ColorTable);
            /// </example>
            public static string GetColorTable256()
            {
                var writer = new StringWriter();

                // standard colors
                writer.WriteLine("Standard Colors");
                for (var index = 0; index < 16; index++)
                {
                    var color = (Ansi.Color256)index;
                    writer.Write($"{Ansi.Bg(color)}{Ansi.FgForBg(color)}   {index,2}   {Ansi.Reset} ");
                }

                // 216 colors
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine("216 Colors");
                for (var index = 16; index < 232; index++)
                {
                    var x = (index - 16) % 36;
                    if (index > 16 && x == 0)
                    {
                        writer.WriteLine();
                    }

                    var color = (Ansi.Color256)index;
                    writer.Write($"{Ansi.Bg(color)}{Ansi.FgForBg(color)}{index,3}{Ansi.Reset} ");
                }

                // grayscale colors
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine("Grayscale Colors");
                for (var index = 232; index < 256; index++)
                {
                    var color = (Ansi.Color256)index;
                    writer.Write($"{Ansi.Bg(color)}{Ansi.FgForBg(color)} {index,3} {Ansi.Reset} ");
                }

                return writer.ToString();
            }

            public static void WriteColorTable256()
            {
                if (OperatingSystem.IsWindows())
                {
                    const int MinWidth = 145;
                    Console.SetWindowSize(Math.Max(MinWidth, Console.WindowWidth), Console.WindowHeight);
                }

                var colorTable = GetColorTable256();
                Console.WriteLine(colorTable);
            }

            public static void WriteColorsNames256()
            {
                var names = Enum.GetNames<Color256>()
                    .OrderBy(name => name)
                    .ToArray();

                foreach (var name in names)
                {
                    var back = Enum.Parse<Color256>(name);
                    var fore = back.FgForBg();
                    var ansi = $"{Bg(back)}{Fg(fore)} {name} {Ansi.Reset}";
                    Console.WriteLine(ansi);
                }
            }

            public static void WriteColor(ConsoleColor color, string text)
            {
                var line = $"{Ansi.Bg(color)}{Ansi.FgForBg(color)} {text} {Ansi.Reset}";
                Console.WriteLine(line);
            }

            public static void WriteColor(Color256 color, string text)
            {
                var line = $"{Ansi.Bg(color)}{Ansi.FgForBg(color)} {text} {Ansi.Reset}";
                Console.WriteLine(line);
            }

            public static void WriteColor(Color color, string text)
            {
                var line = $"{Ansi.Bg(color)}{Ansi.FgForBg(color)} {text} {Ansi.Reset}";
                Console.WriteLine(line);
            }
        }
    }
}