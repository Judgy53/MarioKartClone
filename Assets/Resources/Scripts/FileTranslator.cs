using UnityEngine;
using System.Collections;
using System.IO;

public static class FileTranslator {

	public static void WriteFile(string file, string text)
    {
        StreamWriter writer = new StreamWriter(file);
        writer.Write(text);
        writer.Close();
    }

    public static string ReadFile(string file)
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