using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;
using System.Threading;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;


namespace WindowsFormsApplication1
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.LCDMsg.Visible = false;
            this.LCDList.Visible = false;
            this.DivoomUpdateDeviceList();
        }
        public static int HttpPost(string url, string sendData, out string reslut)
        {
            reslut = "";
            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(sendData);
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);  // 制备web请求
                wbRequest.Proxy = null;     //现场测试注释掉也可以上传
                wbRequest.Method = "POST";
                wbRequest.ContentType = "application/json";
                wbRequest.ContentLength = data.Length;
                wbRequest.Timeout = 1000;

                //#region //【1】获得请求流，OK
                //Stream newStream = wbRequest.GetRequestStream();
                //newStream.Write(data, 0, data.Length);
                //newStream.Close();//关闭流
                //newStream.Dispose();//释放流所占用的资源
                //#endregion

                #region //
                using (Stream wStream = wbRequest.GetRequestStream())         //using(){}作为语句，用于定义一个范围，在此范围的末尾将释放对象。
                {
                    wStream.Write(data, 0, data.Length);
                }
                #endregion

                //获取响应
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8))      //using(){}作为语句，用于定义一个范围，在此范围的末尾将释放对象。
                    {
                        reslut = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                reslut = e.Message;     //输出捕获到的异常，用OUT关键字输出
                return -1;              //出现异常，函数的返回值为-1
            }
            return 0;
        }

        public static string HttpPost2(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            Encoding encoding = Encoding.UTF8;
            byte[] postData = encoding.GetBytes(postDataStr);
            request.ContentLength = postData.Length;
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(postData, 0, postData.Length);
            myRequestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        //GET方法
        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        private void DivoomSendHttpInfo(object sender, EventArgs e)
        {
            if (this.DeviceIPAddr == null || this.LocalList == null || this.LocalList.DeviceList == null || this.LocalList.DeviceList.Length == 0)
            {
                return;

            }
            string CpuTemp_value = "--", CpuUse_value = "--", GpuTemp_value = "--", GpuUse_value = "--", DispUse_value = "--", HardDiskUse_value = "--";

            DivoomDevicePostList PostInfo = new DivoomDevicePostList();
            DivoomDevicePostItem PostItem = new DivoomDevicePostItem();
            PostInfo.Command = "Device/UpdatePCParaInfo";
            PostInfo.ScreenList = new DivoomDevicePostItem[1];
            PostItem.DispData = new string[6];

            if (DeviceIPAddr.Length > 0)
            {
                PostItem.LcdId = this.SelectLCDID;
                computer.Accept(updateVisitor);
                for (int i = 0; i < computer.Hardware.Length; i++)
                {
                    //查找硬件类型为CPU
                    if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                    {
                        for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                        {
                            //找到温度传感器
                            if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            {
                                CpuTemp_value = computer.Hardware[i].Sensors[j].Value.ToString();
                                CpuTemp_value += "C";
                            }
                            else if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Load)
                            {
                                CpuUse_value = computer.Hardware[i].Sensors[j].Value.ToString();
                                if (CpuUse_value.Length > 2)
                                {
                                    CpuUse_value = CpuUse_value.Substring(0, 2);
                                }
                                CpuUse_value += "%";
                            }
                        }
                    }
                    else if (computer.Hardware[i].HardwareType == HardwareType.GpuNvidia ||
                        computer.Hardware[i].HardwareType == HardwareType.GpuAti)
                    {
                        for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                        {
                            //找到温度传感器
                            if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            {
                                GpuTemp_value = computer.Hardware[i].Sensors[j].Value.ToString();
                                GpuTemp_value += "C";
                            }
                            else if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Load)
                            {
                                GpuUse_value = computer.Hardware[i].Sensors[j].Value.ToString();
                                if (GpuUse_value.Length > 2)
                                {
                                    GpuUse_value = GpuUse_value.Substring(0, 2);
                                }
                                GpuUse_value += "%";
                            }
                        }
                    }
                    else if (computer.Hardware[i].HardwareType == HardwareType.HDD)
                    {
                        for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                        {
                            //HDD TEMP
                            if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                            {
                                HardDiskUse_value = computer.Hardware[i].Sensors[j].Value.ToString();
                                HardDiskUse_value += "C";
                                break;
                            }
                        }
                    }
                }

                MEMORYSTATUSEX memInfo = new MEMORYSTATUSEX();
                memInfo.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));

                GlobalMemoryStatusEx(ref memInfo);

                DispUse_value = ((memInfo.ullTotalPhys - memInfo.ullAvailPhys) * 100 / memInfo.ullTotalPhys).ToString().Substring(0, 2) + "%";
                PostItem.DispData[2] = CpuTemp_value;
                PostItem.DispData[0] = CpuUse_value;
                PostItem.DispData[3] = GpuTemp_value;
                PostItem.DispData[1] = GpuUse_value;
                PostItem.DispData[5] = HardDiskUse_value;
                PostItem.DispData[4] = DispUse_value;
                PostInfo.ScreenList[0] = PostItem;
                this.CpuTemp.Text = "CpuTemp:" + CpuTemp_value;
                this.CpuUse.Text = "CpuUse:" + CpuUse_value;
                this.GpuTemp.Text = "GpuTemp:" + GpuTemp_value;
                this.GpuUse.Text = "GpuUse:" + GpuUse_value;
                this.HddUse.Text = "HddUse:" + HardDiskUse_value;
                this.DispUse.Text = "DispUse:" + DispUse_value;
                /*
                 * 
                // 获取硬盘使用情况
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        Console.WriteLine("{0} 硬盘使用情况：", drive.Name);
                        Console.WriteLine("总容量：{0} GB", drive.TotalSize / 1024 / 1024 / 1024);
                        Console.WriteLine("已使用容量：{0} GB", (drive.TotalSize - drive.AvailableFreeSpace) / 1024 / 1024 / 1024);
                        Console.WriteLine("可用容量：{0} GB", drive.AvailableFreeSpace / 1024 / 1024 / 1024);
                    }
                }

                 * */
                string para_info = JsonConvert.SerializeObject(PostInfo);
                Console.WriteLine("request info:" + para_info);
                string response_info;
                HttpPost("http://" + DeviceIPAddr + ":80/post", para_info, out response_info);
                Console.WriteLine("get info:" + response_info);
            }
        }
        private void DivoomUpdateDeviceList()
        {
            int i;
            string url_info = "http://app.divoom-gz.com/Device/ReturnSameLANDevice";
            string device_list = HttpGet(url_info, "");
            // Console.WriteLine(device_list);
            this.LocalList = JsonConvert.DeserializeObject<DivoomDeviceList>(device_list);
            this.divoomList.Items.Clear();
            for (i = 0; this.LocalList.DeviceList != null && i < this.LocalList.DeviceList.Length; i++)
            {
                this.divoomList.Items.Add(this.LocalList.DeviceList[i].DeviceName);
            }

        }
        private void refreshList_Click(object sender, EventArgs e)
        {
            this.DivoomUpdateDeviceList();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;      //可用物理内存
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        [DllImport("kernel32.dll")]
        public static extern void GlobalMemoryStatusEx(ref MEMORYSTATUSEX stat);
        private void DivoomSendSelectClock()
        {
            DeviceIPAddr = this.LocalList.DeviceList[this.divoomList.SelectedIndex].DevicePrivateIP;
            Console.WriteLine("selece items:" + DeviceIPAddr);

            if (this.LocalList.DeviceList[this.divoomList.SelectedIndex].Hardware == 400)
            {
                //get the Independence index of timegate 
                string url_info = "http://app.divoom-gz.com/Channel/Get5LcdInfoV2?DeviceType=LCD&DeviceId=" + this.LocalList.DeviceList[this.divoomList.SelectedIndex].DeviceId;
                string IndependenceStr = HttpGet(url_info, "");
                if (IndependenceStr != null && IndependenceStr.Length > 0)
                {
                    DivoomTimeGateIndependenceInfo IndependenceInfo = JsonConvert.DeserializeObject<DivoomTimeGateIndependenceInfo>(IndependenceStr);

                    this.LcdIndependence = IndependenceInfo.LcdIndependence;

                }
                this.LCDMsg.Visible = true;
                this.LCDList.Visible = true;

            }
            else
            {
                this.LCDMsg.Visible = false;
                this.LCDList.Visible = false;
            }

            DivoomDeviceSelectClockInfo PostInfo = new DivoomDeviceSelectClockInfo();

            PostInfo.LcdIndependence = this.LcdIndependence;
            PostInfo.Command = "Channel/SetClockSelectId";
            PostInfo.LcdIndex = this.LCDList.SelectedIndex;
            PostInfo.ClockId = 625;
            string para_info = JsonConvert.SerializeObject(PostInfo);
            Console.WriteLine("request info:" + para_info);
            string response_info;
            HttpPost("http://" + DeviceIPAddr + ":80/post", para_info, out response_info);

        }
        private void divoomList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DivoomSendSelectClock();



        }
        private void LCDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string raw_value = this.LCDList.SelectedItems[0].ToString();
            this.SelectLCDID = Convert.ToInt32(raw_value) - 1;

            if(this.LocalList != null && this.LocalList.DeviceList!=null && this.LocalList.DeviceList.Count() > 0)
            {
                if (this.divoomList.SelectedIndex > 0 && this.divoomList.SelectedIndex < this.LocalList.DeviceList.Count())
                {
                    this.DivoomSendSelectClock();
                }

            }


        }
        public void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("exit");
        }
    }

    public class DivoomDeviceSelectClockInfo
    {
        public int LcdIndependence { get; set; }
        public int DeviceId { get; set; }
        public int LcdIndex { get; set; }
        public int ClockId { get; set; }
        public string Command { get; set; }
    }
    public class DivoomTimeGateIndependenceInfo
    {
        public int LcdIndependence { get; set; }
        public int ChannelType { get; set; }
        public int ClockId { get; set; }
    }


    public class DivoomDevicePostItem
    {
        public int LcdId { get; set; }


        public string[] DispData { get; set; }

    }
    public class DivoomDevicePostList
    {
        public string Command { get; set; }
        public DivoomDevicePostItem[] ScreenList { get; set; }

    }
    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
                subHardware.Accept(this);
        }

        public void VisitSensor(ISensor sensor) { }

        public void VisitParameter(IParameter parameter) { }
    }

}
