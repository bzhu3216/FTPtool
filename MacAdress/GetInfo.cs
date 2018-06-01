using System;
using System.Management;
using System.Net;



namespace MacAdress
{
    public class Getinfo

    {
        public Getinfo()
        {
        }


        public static string GetUserName()
        {
            try
            {
                string strUserName = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    strUserName = mo["UserName"].ToString();
                }
                moc = null;
                mc = null;
                return strUserName;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取本机MAC地址  
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public static string GetMacAddress()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取本机的物理地址  
        /// </summary>  
        /// <returns></returns>  
        public static string getMacAddr_Local()
        {
            string madAddr = null;
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc2 = mc.GetInstances();
                foreach (ManagementObject mo in moc2)
                {
                    if (Convert.ToBoolean(mo["IPEnabled"]) == true)
                    {
                        madAddr = mo["MacAddress"].ToString();
                        madAddr = madAddr.Replace(':', '-');
                    }
                    mo.Dispose();
                }
                if (madAddr == null)
                {
                    return "unknown";
                }
                else
                {
                    return madAddr;
                }
            }
            catch (Exception)
            {
                return "unknown";
            }
        }

        public static string GetClientLocalIPv4Address()
        {
            /*
            string strLocalIP = string.Empty;
            try
            {
                strLocalIP = (Dns.GetHostAddresses(Dns.GetHostName())[1]).ToString();
               // IPAddress ipAddress = ipHost.AddressList[0];
               // strLocalIP = ipAddress.ToString();
                return strLocalIP;
            }
            catch
            {
                return "unknown";
            }*/
            try
            {
                string name = Dns.GetHostName();
                IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
                foreach (IPAddress ipa in ipadrlist)
                {
                    if ((ipa.AddressFamily).ToString().Equals("InterNetwork"))
                        return ipa.ToString();
                }

                return "0";

            }
            catch (Exception e)
            {
                return e.Message;
            }
            


        }




    }
}








   
    