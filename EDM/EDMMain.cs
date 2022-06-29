using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace EDM
{
    public class EDM
    {
        public static string programPath;
        public PlanDeCuentas PDC;

        public EDM(string programPath_)
        {
            programPath = programPath_;
            PDC = new PlanDeCuentas();
        }

        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int ComputeLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public static bool BackupProcess(string zipfilename, string path)
        {
            //if (!zipfilename.ToLower().EndsWith(".zip"))
            //    zipfilename += ".zip";

            if (System.IO.File.Exists(zipfilename))
                System.IO.File.Delete(zipfilename);

            System.IO.DirectoryInfo f = new System.IO.DirectoryInfo(path);
            //System.IO.FileInfo[] filesInf = f.GetFiles();
            string[] archivos = System.IO.Directory.GetFiles(path);

            string folder = System.IO.Path.GetDirectoryName(path);

            using (ZipFile sf = new ZipFile(zipfilename))
            {
                sf.BufferSize = 1024 * 1024;
                sf.CodecBufferSize = 1024 * 1024;

                sf.StatusMessageTextWriter = null;
                //sf.AddDirectory(path, folder);

                for (int i = 0; i < archivos.Length; i++)
                {
                    string fd = archivos[i];
                    sf.AddFile(fd, "\\");
                }

                sf.Save();
            }

            return true;
        }

        public static bool RestoreProcess(string zipfilename, string targetPath)
        {
            using (ZipFile sf = new ZipFile(zipfilename))
            {
                sf.ExtractAll(targetPath, ExtractExistingFileAction.OverwriteSilently);
            }

            return true;
        }
    }
}
