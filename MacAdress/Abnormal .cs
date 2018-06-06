using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;


namespace MacAdress
{
    /// <summary>
    /// ///////////////////////////////////////////////////////
    /// </summary>
    public class abnormal

    {
        private string ftpUserID = "FTPSE";//用户名  
        private string ftpPassword = "Se50214252";//密码 
        private UploadFileFTP up;
        public abnormal()
        {
            up = new UploadFileFTP();
        }



       


        //////////////////////////////////////////////////////

        public  bool CompareFile(String  p_11 ,String p_22)
        {
            string p_1 = p_11 ;
            string p_2 = p_22 ;
            bool same;

            //计算第一个文件的哈希值
            var hash = System.Security.Cryptography.HashAlgorithm.Create();
            var stream_1 = new System.IO.FileStream(p_1, System.IO.FileMode.Open);
            byte[] hashByte_1 = hash.ComputeHash(stream_1);
            stream_1.Close();
            //计算第二个文件的哈希值
            FtpWebRequest downRequest = (FtpWebRequest)WebRequest.Create(new Uri(p_22));
            downRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);// ftp用户名和密码
            //设置要发到 FTP 服务器的命令  
            downRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            FtpWebResponse response = (FtpWebResponse)downRequest.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            // var stream_2 = new System.IO.FileStream(p_2, System.IO.FileMode.Open);
            byte[] hashByte_2 = hash.ComputeHash(ftpStream);
            ftpStream.Close();

            //比较两个哈希值
            if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                // Console.WriteLine("两个文件相等");
                same = true;
            else
                // Console.WriteLine("两个文件不等");
                same = false;

            return same;

        }

        //////////////////////////////////////////


        public  bool CompareDir(String pd_11, String pd_22,string ddir)
        {   ///////////只要本地文件在服务器上的就可以，服务器上多的不管
            
            string pd_1 = pd_11+ddir;
            string pd_2 = pd_22;
            bool samed=true;

            ///////////
            //string dir = localDir + dirName + @"\"; //获取当前目录（父目录在目录名）  
            //string dir = localDir + dirName + @"\";
            //检测本地目录是否存在  
            if (!Directory.Exists(pd_1))
            {
                //Response.Write("本地目录：“" + dir + "” 不存在！<br/>");
                MessageBox.Show("本地目录：“" + pd_1 + "” 不存在111！");
                
            }
            //检测FTP的目录路径是否存在  
            if (!up.CheckDirectoryExist2(pd_22, ddir))
            {
                // MakeDir(ftpPath, dirName);//不存在，则创建此文件夹 
                samed = false;
                { samed = false;
                    //MessageBox.Show("msg2");
                }

            }
            List<List<string>> infos = up.GetDirDetails(pd_1); //获取当前目录下的所有文件和文件夹  

            //先上传文件  
            //Response.Write(dir + "下的文件数：" + infos[0].Count.ToString() + "<br/>");  
            for (int i = 0; i < infos[0].Count; i++)
            {
                // Console.WriteLine(infos[0][i]);
                if (!CompareFile(pd_1 + @"\" + infos[0][i], pd_2 + ddir + "/" + infos[0][i])) { samed = false;
                  //  MessageBox.Show("msg1");
                }
            }
            //再处理文件夹  
            //Response.Write(dir + "下的目录数：" + infos[1].Count.ToString() + "<br/>");  
            for (int i = 0; i < infos[1].Count; i++)
            {
                if(!CompareDir(pd_1+@"\" , pd_2 + ddir + "/", infos[1][i])) samed=false ;
                //Response.Write("文件夹【" + dirName + "】上传成功！<br/>");  
            }
            //////////



            


            return samed;

        }


////////////////////////////////////////////////////////////////////





        }
}


