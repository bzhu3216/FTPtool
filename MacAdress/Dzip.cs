using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksum;
using System.Windows.Forms;
namespace testzip
{
    class Dzip
    {

        //////////////////////////////////////////////////////////////////////
        public static void ZipFile(string strFile, string strZip)
        {
            try
            {
                Encoding gbk = Encoding.GetEncoding("gbk");
                ICSharpCode.SharpZipLib.Zip.ZipConstants.DefaultCodePage = gbk.CodePage;
                if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
                    strFile += Path.DirectorySeparatorChar;
                ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
                s.SetLevel(9); // 0 - store only to 9 - means best compression
                zip(strFile, s, strFile);
                s.Finish();
                s.Close();
               // MessageBox.Show("压缩完");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during processing {0}", ex);
                MessageBox.Show(ex.Message+"压缩有问题请联系老师");
                // No need to rethrow the exception as for our purposes its handled.
            }



        }




        ////////////////////////////////////////////////////////////

        private static void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
          //  Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);

            byte[] buffer = new byte[4096];
            foreach (string file in filenames)// 遍历所有的文件和目录
            {

                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {
                    zip(file, s, staticFile);
                }


                else // 否则直接压缩文件
                {

                    string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);
                    using (FileStream fs = File.OpenRead(file))
                    {

                        // Using a fixed size buffer here makes no noticeable difference for output
                        // but keeps a lid on memory usage.
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                    //打开压缩文件
                    


                    
                }
            }
        }



        /////////////////////////////////////////////////////////////////////

        public static void ZipFile2(string strFile, string strZip)
        {

            //////////////////////////////////////////////
            try
            {
                // Depending on the directory this could be very large and would require more attention
                // in a commercial package.
                string[] filenames = Directory.GetFiles(@"c:\test1");

                // 'using' statements guarantee the stream is closed properly which is a big source
                // of problems otherwise.  Its exception safe as well which is great.
                using (ZipOutputStream s = new ZipOutputStream(File.Create(@"c:\test.zip")))
                {

                    s.SetLevel(5); // 0 - store only to 9 - means best compression

                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {

                        // Using GetFileName makes the result compatible with XP
                        // as the resulting path is not absolute.
                        var entry = new ZipEntry(Path.GetFileName(file));

                        // Setup the entry data as required.

                        // Crc and size are handled by the library for seakable streams
                        // so no need to do them here.

                        // Could also use the last write time or similar for the file.
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {

                            // Using a fixed size buffer here makes no noticeable difference for output
                            // but keeps a lid on memory usage.
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }

                    // Finish/Close arent needed strictly as the using statement does this automatically

                    // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                    // the created file would be invalid.
                    s.Finish();
                    // Close is important to wrap things up and unlock the file.
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during processing {0}", ex);

                // No need to rethrow the exception as for our purposes its handled.
            }
        }

        //////////////////////////////////////////////////////////////////////


    }
}
