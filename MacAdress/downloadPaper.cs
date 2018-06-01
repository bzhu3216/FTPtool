using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace MacAdress
{
    class downloadPaper
    {

        /////////////////////////////////////
        /// 单个文件下载方法  
        /// </summary>  
        /// <param name="adss">保存文件的本地路径</param>  
        /// <param name="ftpadss">下载文件的FTP路径</param>  
        public void download(string adss, string ftpadss)
        {
            //FileMode常数确定如何打开或创建文件,指定操作系统应创建新文件。  
            //FileMode.Create如果文件已存在，它将被改写  
            //MessageBox.Show("nam");
            FileStream outputStream = new FileStream(adss, FileMode.Create);
            FtpWebRequest downRequest = (FtpWebRequest)WebRequest.Create(new Uri(ftpadss));
            //设置要发到 FTP 服务器的命令  
            downRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            FtpWebResponse response = (FtpWebResponse)downRequest.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            long cl = response.ContentLength;
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];
            readCount = ftpStream.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                outputStream.Write(buffer, 0, readCount);
                readCount = ftpStream.Read(buffer, 0, bufferSize);
            }
            ftpStream.Close();
            outputStream.Close();
            response.Close();
        }
        /////////////////////////////////////////////////////////

        public string[] ftp(string ftpads, string name, string type)
        {
            WebResponse webresp = null;
            StreamReader ftpFileListReader = null;
            FtpWebRequest ftpRequest = null;
            try
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(ftpads + name));
                ftpRequest.Method = type;
                webresp = ftpRequest.GetResponse();
                ftpFileListReader = new StreamReader(webresp.GetResponseStream(), Encoding.UTF8  );
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            StringBuilder str = new StringBuilder();
            string line = ftpFileListReader.ReadLine();
            while (line != null)
            {
                str.Append(line);
                str.Append("\n");
                line = ftpFileListReader.ReadLine();
            }
            string[] fen = str.ToString().Split('\n');
            return fen;
        }


        ///////////////////////////////////

        /// </summary>  
        /// <param name="ftpads">FTP路径</param>  
        /// <param name="name">需要下载文件路径</param>  
        /// <param name="Myads">保存的本地路径</param>  
        public void downftp(string ftpads, string name,string Myads)  
{  
    string downloadDir = Myads + name;  
    string ftpdir = ftpads + name;  
    string[] fullname = ftp(ftpads, name, WebRequestMethods.Ftp.ListDirectoryDetails);
    string[] onlyname = ftp(ftpads, name, WebRequestMethods.Ftp.ListDirectory);

            //判断是否为单个文件   
  if (false)  
    {  
        
    }  
    else  
    {
      

       
        if (!Directory.Exists(downloadDir))  
        {  
            Directory.CreateDirectory(downloadDir);  
        }
        int i = 0;
        foreach (string names in fullname)  
        {
                    if (!names.Equals(""))
                    {
                        i = i + 1;
                        //判断是否具有文件夹标识<DIR>  
                        if (names.Contains("<DIR>"))
                        {
                            string olname = names.Split(new string[] { "<DIR>" }, StringSplitOptions.None)[1].Trim();
                            //MessageBox.Show("olname");
                            downftp(ftpdir, @"\" + olname, downloadDir);
                        }
                        else
                        {
                            /* foreach (string onlynames in onlyname)  
                             {  
                                 if (onlynames == "" || onlynames == " " || names == "")  
                                 {  
                                     break;  
                                 }  
                                 else  
                                 {  
                                     if (names.Contains(" " + onlynames))  
                                     {  
                                         download(downloadDir + @"\" + onlynames, ftpads + name + "/" + onlynames);  
                                         break;  
                                     }  
                                 }  
                             } */
                            string name2 = onlyname[i - 1];
                           // MessageBox.Show(name2);
                            download(downloadDir + @"\" + name2, ftpads + name + "/" + name2);

                        }
                    } 
        }  
    }  
      
}  
    




    }
}
