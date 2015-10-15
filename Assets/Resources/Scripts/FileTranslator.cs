using UnityEngine;
using System.Collections;
using System.IO;

public static class FileTranslator {

	public static void WriteShit(string file, string text)
    {
        StreamWriter writer = new StreamWriter(file);
        writer.Write(text);
        writer.Close();
    }

    public static string ReadShit(string file)
    {
        if (File.Exists(file))
        {
            StreamReader reader = new StreamReader(file);
            string read = reader.ReadToEnd();
            reader.Close();
            return read;
        }
        else
            return string.Empty;
    }

}