using System;
using System.IO;

namespace ADcontent
{
    class Program
    {
        static void Main()
        {
            string[] dir = Directory.GetFiles(@"\\REP-APP\SFTP_ROOT\ftpuser\ADcontent\", "*.csv");
            string originalPath = dir[0];
            string destinationPath = @"\\S2013-DBS\ADcontent\";
            string currentLine;
            string headers;
            StreamWriter streamWriter = null;
            int outputFileCount = 1;
            int lineCount = 0;
            int maxLines = 100000;
            using FileStream originalFileStream = new FileStream(originalPath, FileMode.Open, FileAccess.Read);
            FileStream destinationFileStream = null;
            using StreamReader streamReader = new StreamReader(originalFileStream);
            try
            {

                currentLine = streamReader.ReadLine();
                headers = currentLine;
                while (currentLine != null)
                {
                    if (streamWriter == null | lineCount > maxLines)
                    {
                        if (streamWriter != null)
                        {
                            streamWriter.Close();
                            streamWriter = null;
                            destinationFileStream.Close();
                            destinationFileStream = null;
                            outputFileCount++;
                        }
                        destinationFileStream = new FileStream(destinationPath + "ItemDetails" + outputFileCount + ".csv", FileMode.Create, FileAccess.Write);
                        streamWriter = new StreamWriter(destinationFileStream);
                        //_logger.LogInformation("ItemDetails{0} being written", outputFileCount);
                        lineCount = 0;
                        if (outputFileCount > 1)
                        {
                            streamWriter.WriteLine(headers);

                        }
                    }

                    streamWriter.WriteLine(currentLine);
                    currentLine = streamReader.ReadLine();
                    lineCount++;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            streamReader.Dispose();
            File.Delete(originalPath);
        }
    }
}
