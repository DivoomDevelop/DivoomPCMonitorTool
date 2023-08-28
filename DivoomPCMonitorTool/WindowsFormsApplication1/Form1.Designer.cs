namespace WindowsFormsApplication1
{
    public class DivoomDeviceInfo
    {
        public int DeviceId { get; set; }

        public int Hardware { get; set; }

        public string DeviceName { get; set; }
        public string DevicePrivateIP { get; set; }
        public string DeviceMac { get; set; }

    }
    public class DivoomDeviceList
    {

        public DivoomDeviceInfo[] DeviceList { get; set; }

    }
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.refreshList = new System.Windows.Forms.Button();
            this.CpuUse = new System.Windows.Forms.TextBox();
            this.CpuTemp = new System.Windows.Forms.TextBox();
            this.GpuUse = new System.Windows.Forms.TextBox();
            this.GpuTemp = new System.Windows.Forms.TextBox();
            this.DispUse = new System.Windows.Forms.TextBox();
            this.HddUse = new System.Windows.Forms.TextBox();
            this.divoomList = new System.Windows.Forms.ListBox();
            this.LCDList = new System.Windows.Forms.ListBox();
            this.LCDMsg = new System.Windows.Forms.Label();
            this.DeviceListMsg = new System.Windows.Forms.Label();
            this.HardwareInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // DeviceListMsg
            // 
            this.DeviceListMsg.Location = new System.Drawing.Point(100, 10);
            this.DeviceListMsg.Name = "DeviceListMsg";
            this.DeviceListMsg.Size = new System.Drawing.Size(100, 20);
            this.DeviceListMsg.Text = "Device list";
            // 
            // listbox
            // 
            this.divoomList.Location = new System.Drawing.Point(100, 30);
            this.divoomList.Name = "DeviceList";
            this.divoomList.Size = new System.Drawing.Size(100, 230);
            this.divoomList.TabIndex = 2;//CheckedListBoxes
            this.divoomList.SelectedIndexChanged += new System.EventHandler(this.divoomList_SelectedIndexChanged);
            // 
            // refresh list
            // 
            this.refreshList.Location = new System.Drawing.Point(100, 260);
            this.refreshList.Name = "refresh list";
            this.refreshList.Size = new System.Drawing.Size(100, 23);
            this.refreshList.TabIndex = 0;
            this.refreshList.Text = "refresh list";
            this.refreshList.UseVisualStyleBackColor = true;
            this.refreshList.Click += new System.EventHandler(this.refreshList_Click);

            // 
            // LCDMsg
            // 
            this.LCDMsg.Location = new System.Drawing.Point(20, 10);
            this.LCDMsg.Name = "DeviceListMsg";
            this.LCDMsg.Size = new System.Drawing.Size(80, 20);
            this.LCDMsg.Text = "Select LCD";
            // LCDList
            // 
            this.LCDList.Location = new System.Drawing.Point(20, 30);
            this.LCDList.Name = "LCDList";
            this.LCDList.Size = new System.Drawing.Size(20, 100);
            this.LCDList.TabIndex = 2;//CheckedListBoxes
            this.LCDList.SelectedIndexChanged += new System.EventHandler(this.LCDList_SelectedIndexChanged);
            this.LCDList.Items.Add("1");
            this.LCDList.Items.Add("2");
            this.LCDList.Items.Add("3");
            this.LCDList.Items.Add("4");
            this.LCDList.Items.Add("5");
            this.LCDList.SetSelected(0, true);


            // 
            // HardwareInfo
            // 
            this.HardwareInfo.Location = new System.Drawing.Point(220, 10);
            this.HardwareInfo.Name = "HardwareInfo";
            this.HardwareInfo.Size = new System.Drawing.Size(180, 20);
            this.HardwareInfo.Text = "Hardware information";

            // 
            // CpuUse
            // 
            this.CpuUse.Location = new System.Drawing.Point(220, 30);
            this.CpuUse.Name = "PCUser";
            this.CpuUse.Size = new System.Drawing.Size(100, 20);
            this.CpuUse.TabIndex = 1;
            // 

            // CpuTemp
            // 
            this.CpuTemp.Location = new System.Drawing.Point(220, 60);
            this.CpuTemp.Name = "textBox1";
            this.CpuTemp.Size = new System.Drawing.Size(100, 20);
            this.CpuTemp.TabIndex = 1;

            // 
            // CpuUse
            // 
            this.GpuUse.Location = new System.Drawing.Point(220, 90);
            this.GpuUse.Name = "PCUser";
            this.GpuUse.Size = new System.Drawing.Size(100, 20);
            this.GpuUse.TabIndex = 1;
            // 

            // CpuTemp
            // 
            this.GpuTemp.Location = new System.Drawing.Point(220, 120);
            this.GpuTemp.Name = "textBox1";
            this.GpuTemp.Size = new System.Drawing.Size(100, 20);
            this.GpuTemp.TabIndex = 1;


            // CpuTemp
            // 
            this.DispUse.Location = new System.Drawing.Point(220, 150);
            this.DispUse.Name = "textBox1";
            this.DispUse.Size = new System.Drawing.Size(100, 20);
            this.DispUse.TabIndex = 1;


            // CpuTemp
            // 
            this.HddUse.Location = new System.Drawing.Point(220, 180);
            this.HddUse.Name = "textBox1";
            this.HddUse.Size = new System.Drawing.Size(100, 20);
            this.HddUse.TabIndex = 1;

            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.CpuUse);
            this.Controls.Add(this.CpuTemp);
            this.Controls.Add(this.GpuUse);
            this.Controls.Add(this.GpuTemp);
            this.Controls.Add(this.DispUse);
            this.Controls.Add(this.HddUse);
            this.Controls.Add(this.refreshList);
            this.Controls.Add(this.divoomList);
            this.Controls.Add(this.LCDList);
            this.Controls.Add(this.LCDMsg);
            this.Controls.Add(this.DeviceListMsg);
            this.Controls.Add(this.HardwareInfo);
            System.AppDomain.CurrentDomain.ProcessExit += new System.EventHandler(CurrentDomain_ProcessExit);  
            this.Name = "DivoomPcTool";
            this.Text = "DivoomPcTool";
            this.SelectLCDID = 0;
            this.DeviceIPAddr = "";

            this.updateVisitor = new UpdateVisitor();
            this.computer = new OpenHardwareMonitor.Hardware.Computer();
            this.computer.HDDEnabled = true;
            this.computer.Open();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();//创建定时器
            timer.Tick += new System.EventHandler(DivoomSendHttpInfo);//事件处理
            timer.Enabled = true;//设置启用定时器
            timer.Interval = 2000;//执行时间
            timer.Start();//开启定时器
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox divoomList;
        private System.Windows.Forms.ListBox LCDList;
        private System.Windows.Forms.Button refreshList;
        private System.Windows.Forms.TextBox CpuUse;
        private System.Windows.Forms.TextBox CpuTemp;
        private System.Windows.Forms.TextBox GpuUse;
        private System.Windows.Forms.TextBox GpuTemp;
        private System.Windows.Forms.TextBox DispUse;
        private System.Windows.Forms.TextBox HddUse;
        private System.Windows.Forms.Label LCDMsg;
        private System.Windows.Forms.Label DeviceListMsg;
        private System.Windows.Forms.Label HardwareInfo;
        private DivoomDeviceList LocalList;
        private int SelectLCDID;
        private System.Windows.Forms.Timer timer;
        private string DeviceIPAddr;
        private int LcdIndependence;

        UpdateVisitor updateVisitor;
        OpenHardwareMonitor.Hardware.Computer computer;

    }
}

