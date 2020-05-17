using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;
using OpenCvSharp;

class Program
{
    public static void Main()
    {

        Thread ti = new Thread(new ThreadStart(() => Fun()));
        ti.Start();
        Thread.Sleep(1000);
        Thread ti2 = new Thread(new ThreadStart(() => Fun2()));
        ti2.Start();
        Thread.Sleep(3000);


    }

    static void Fun2() {

        Console.WriteLine("Entered in 2nd thread");
        Process process = new Process();
        ProcessStartInfo ps = new ProcessStartInfo();
        ps.WindowStyle = ProcessWindowStyle.Normal;
        ps.CreateNoWindow = true;
        ps.UseShellExecute = false;
        ps.RedirectStandardOutput = true;
        ps.RedirectStandardError = true;




        ps.FileName = "adb.exe ";
        ps.Arguments = "connect " + "192.168.0.2";
        process.StartInfo = ps;
        process.Start();
        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());
        process.WaitForExit(); process.Close();


        ps.FileName = "adb.exe ";
        ps.Arguments = "reverse " + "--remove-all";
        process.StartInfo = ps;
        process.Start();
        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());
        process.WaitForExit(); process.Close();

        ps.FileName = "adb.exe ";
        ps.Arguments = "forward " + "--remove-all";
        process.StartInfo = ps;
        process.Start();
        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());
        process.WaitForExit(); process.Close();

        ps.FileName = "adb.exe ";
        ps.Arguments = "push " + "scrcpy-server " + "/data/local/tmp/scrcpy-server.jar";
        process.StartInfo = ps;process.Start();
        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());
        process.WaitForExit(); process.Close();

        ps.FileName = "adb.exe ";
        ps.Arguments = "reverse " + "localabstract:scrcpy " + "tcp:27183";
        process.StartInfo = ps;process.Start();
        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());
        

        ps.FileName = "adb.exe ";
        ps.Arguments = "shell " + "CLASSPATH=/data/local/tmp/scrcpy-server.jar " +
     "app_process " + "/ " + "com.genymobile.scrcpy.Server " +
    "1.12.1 " + "512 " + "8000000 " + "0 " + "false " + "- " + "true " + "false";
        process.StartInfo = ps;process.Start();
        Console.WriteLine(process.StandardError.ReadToEnd());
        Console.WriteLine(process.StandardOutput.ReadToEnd());
        process.WaitForExit(); process.Close();process.Dispose();

    }
    static void Fun()
    {
        
        TcpListener server = null;
        TcpListener server2 = null;

        Int32 port = 27183;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(localAddr, port);
        TcpClient client=null;
        NetworkStream stream = null;

        Int32 port2 = 27184;
        IPAddress localAddr2 = IPAddress.Parse("127.0.0.1");
        server2 = new TcpListener(localAddr2, port2);
        TcpClient client2=null;
        NetworkStream stream2 = null;

        //byte[] bb1 = new byte[4];

        byte[] bb2 = new byte[64];
        byte[] bb3 = new byte[12];
        byte[] bb4 = new byte[10000];

        try
        {

            server.Start();
            server2.Start();

            while (true)
            {

                Console.WriteLine("Waiting for a connection Server No.1---------- ");
                client = server.AcceptTcpClient();
                Console.WriteLine("Connected!========>1");
                stream = client.GetStream();

                stream.Read(bb2, 0, bb2.Length);

                Console.WriteLine(System.Text.Encoding.ASCII.GetString(bb2, 0, bb2.Length));
                stream.Read(bb3, 0, bb3.Length);

                Console.WriteLine(System.Text.Encoding.ASCII.GetString(bb3, 0, bb3.Length));


                //stream.Read(bb5, 0, bb5.Length);


                //stream.Read(bb1, 0, bb1.Length);


               // stream.Read(bb4, 0, bb4.Length);
                //Console.WriteLine("Is it Converted======>"+ ByteArrayToHexString(bb4));


                //Console.WriteLine(System.Text.Encoding.ASCII.GetString(bb5, 0, bb5.Length));


                //stream.Read(bb4, 0, bb4.Length);

                //Console.WriteLine(System.Text.Encoding.ASCII.GetString(bb4, 0, bb4.Length));

                Console.Write("Waiting for a connection Server No.2---------- ");

                //Process process2 = Process.Start("C:/Users/fujistu/Documents/Visual Studio 2019/projects/Nwcloni/bin/x64/Debug/Nwcloni.exe");

                Thread ti3 = new Thread(new ThreadStart(() => Fun3()));
                ti3.Start();
                
                Thread.Sleep(2000);

                client2 = server2.AcceptTcpClient();
                Console.WriteLine("Connected!========>2");
                
                stream2 = client2.GetStream();
                
                while (true)
                {

                    stream.Read(bb4, 0, bb4.Length);
                    //Console.WriteLine("Is it Converted======>" + ByteArrayToHexString(bb4));

                    //Console.WriteLine(ByteArrayToHexString(bb4));
                    for (int n = 0; n < 3; n++)
                    {
                    
                        //Thread.Sleep(40);
                        stream2.Write(bb4, 0, (bb4.Length));
                    
                    }
                
                }

            }

             }
        catch (SocketException e)
        {
            Console.WriteLine("One Number Exception on Socket", e);

        }

        catch (ArgumentNullException ane)
        {

            Console.WriteLine("Two Number on Augument Null", ane.ToString());

        }

        catch (Exception e)
        {

            Console.WriteLine("Third is Gneeal", e.ToString());

        }

        finally
        {
            // Stop listening for new clients.
            server.Stop();
            server2.Stop();
        }


        stream.Flush();
        stream.Close();
        stream2.Flush();
        stream2.Close();
        client.Close();
        client2.Close();
        server.Stop();
        server2.Stop();


        Process process = new Process();
        ProcessStartInfo ps = new ProcessStartInfo();


        

        ps.WindowStyle = ProcessWindowStyle.Normal;
        ps.CreateNoWindow = true;
        ps.UseShellExecute = false;
        ps.RedirectStandardOutput = true;
        ps.RedirectStandardOutput = true;
        ps.RedirectStandardError = true;


        ps.FileName = "adb.exe ";
        ps.Arguments = "reverse " + "--remove-all";
        process.StartInfo = ps;
        process.Start();

        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());

        process.WaitForExit(); process.Close();



        ps.FileName = "adb.exe ";
        ps.Arguments = "forward " + "--remove-all";
        process.StartInfo = ps;
        process.Start();

        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());

        process.WaitForExit(); process.Close(); 


        ps.FileName = "adb.exe ";
        ps.Arguments = "kill-server";
        process.StartInfo = ps;
        process.Start();

        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());

        process.WaitForExit(); process.WaitForExit(); process.Close();
        process.Dispose();



        Console.WriteLine("\nHit enter to continue...");
        Console.Read();

    }

    static void Fun3() {

        VideoCapture capture = null;


        capture = new VideoCapture("http://127.0.0.1:27184");


        int sleepTime = (int)Math.Round(1000 / capture.Fps);


        using (var window = new Window("capture"))
        {
            // Frame image buffer
            Mat image = new Mat();


            // When the movie playback reaches end, Mat.data becomes NULL.
            while (true)
            {

            Found:

                capture.Read(image); // same as cvQueryFrame

                if (image.Empty())
                {
                    Console.WriteLine("Empty");
                    //Console.ReadKey();
                    goto Found;

                }

                window.ShowImage(image);
                Cv2.WaitKey(5);


            }
        }
    }

    static void DisplayBitArray(BitArray bitArray)
    {
        int y = 0;

        for (int i = 0; i < bitArray.Count; i++)
        {

            y++;

            bool bit = bitArray.Get(i);

            Console.Write(bit ? 1 : 0);

            if (y == 8)
            {

                Console.WriteLine("break");
                y = 0;
            }
        }
        Console.WriteLine();
    }


    public static string ByteArrayToHexString(byte[] Bytes)
    {
        StringBuilder Result = new StringBuilder(Bytes.Length * 2);
        string HexAlphabet = "0123456789ABCDEF";

        foreach (byte B in Bytes)
        {
            Result.Append(HexAlphabet[(int)(B >> 4)]);
            Result.Append(HexAlphabet[(int)(B & 0xF)]);
        }

        return Result.ToString();
    }

    public static byte[] HexStringToByteArray(string Hex)
    {
        byte[] Bytes = new byte[Hex.Length / 2];
        int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
       0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
       0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
        {
            Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                              HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
        }

        return Bytes;
    }

    static string ByteToHexBitFiddle(byte[] bytes)
    {
        char[] c = new char[bytes.Length * 2];
        int b;
        for (int i = 0; i < bytes.Length; i++)
        {
            b = bytes[i] >> 4;
            c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
            b = bytes[i] & 0xF;
            c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
        }
        return new string(c);
    }

}
