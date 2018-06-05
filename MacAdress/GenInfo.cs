using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace MacAdress
{
    class GenInfo
    {

        public static void GenFile(String name, String id, String ip, string mac)
        {
            try
            {
                System.DateTime currentTime = DateTime.Now;
                String path;
                path = @"c:\" + id + name + "-" + currentTime.ToString("m") + @"上传文件夹";
                if (Directory.Exists(path))
                {
                    MessageBox.Show("文件夹已存在！");
                }
                else
                {
                    Directory.CreateDirectory(path);
                    FileStream fs = new FileStream(path + @"\log.dat", FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(id);
                    sw.WriteLine(name);
                    sw.WriteLine(ip);
                    sw.WriteLine(mac);
                    sw.Close();
                    //  MessageBox.Show("创建成功!");

                }

            }
            catch
            {
                MessageBox.Show("创建上传文件夹不成功");
            }
        }

        //////////////////////////////////////////////////////////////////////////
     
    public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // 将所有文件拷贝到新文件夹中
            foreach (FileInfo fi in source.GetFiles())
            {
               // Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // 使用递归拷贝子文件夹
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }


        ////////////////////////////////////////////////////////////
        public static bool Checkfile(string path, string name)

        {
            if (File.Exists(path + name))
                return true;
            else

            return false;

        }






        /////////////////////////////////////////////////////////////////////////





    }







}
