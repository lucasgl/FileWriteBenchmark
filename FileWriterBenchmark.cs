using System.IO.MemoryMappedFiles;
using System.Text;
using BenchmarkDotNet.Attributes;

public class FileWriterBenchmark
{
    //[Benchmark]
    public void WriteToFileAppendAllText()
    {
        for (int i = 0; i < 10000; i++)
        {
            File.AppendAllText("log1.txt", "The line to be written");
        }
    }

    //[Benchmark]
    public async Task WriteToFileStreamWriterAsync()
    {
        for (int i = 0; i < 10000; i++)
        {
            using (var writer = new StreamWriter("log2.txt", true, Encoding.UTF8))
            {
                // Use asynchronous WriteAsync method to write the line
                await writer.WriteLineAsync("The line to be written");
            }
        }
    }

    //[Benchmark]
    public void WriteToFileStreamWriter()
    {
        for (int i = 0; i < 10000; i++)
        {
            using (var sw = new StreamWriter("log3.txt", true, Encoding.UTF8))
            {
                sw.WriteLine("The line to be written");
            }
        }
    }

    [Benchmark]
    public void WriteToMemoryMappedFile()
    {
        for (int i = 0; i < 10000; i++)
        {
            var line = "The line to be written\n";
            byte[] bytesToWrite = Encoding.UTF8.GetBytes(line);
            long position = 0;
            if (File.Exists("log4.txt"))
            {
                position = new FileInfo("log4.txt").Length;
            }

            using (var mmf = MemoryMappedFile.CreateFromFile("log4.txt", FileMode.OpenOrCreate,null, position + bytesToWrite.Length))
            {
                using (var accessor = mmf.CreateViewAccessor())
                {
                    accessor.WriteArray(position, bytesToWrite, 0, bytesToWrite.Length);
                }
            }
        }
    }

}