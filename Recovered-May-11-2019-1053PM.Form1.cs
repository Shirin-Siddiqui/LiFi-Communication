using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool isConnected = false;
        String[] ports;
        
        public Form1()
        {

            InitializeComponent();
           
            getAvailableComPorts();

            foreach(string port in ports)
            {
                comboBox1.Items.Add(port);
                Console.WriteLine(port);
                if(ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
        }
        SerialPort port;
    

        
        /*private void button1_Click(object sender, EventArgs e)
        {
            //port = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
            ////port.Open();
            port.Write("led");
            //port.Close();
        }*/

        private void ReceivedSerialHandler(object sender,SerialDataReceivedEventArgs e)
        {
            String texxt= port.ReadLine();
            textBox2.Text = "here";
            SerialPort sp = (SerialPort)sender;
            this.Invoke((MethodInvoker)delegate
            {
                textBox2.Text = sp.ReadExisting() ;
            });
            textBox2.Text = texxt;
            //port.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!isConnected)
            {
                connectToArduino();
            }
            else
            {
                disconnectFromArduino();
            }
            
        }
        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
    t    }
        private void connectToArduino()
        {
            isConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            port.Write("#STAR\n");
            button3.Text = "Disconnect";
            
        }
        private void disconnectFromArduino()
        {
            isConnected = false;
            port.Write("#STOP\n");
            port.Close();
            button1.Text = "Connect";
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            port.Write(textBox1.Text);
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = port.ReadLine();
          
        }

        private static void SendBinaryFile(SerialPort port, string FileName)
        {
            using (FileStream fs = File.OpenRead(FileName))
                port.Write((new BinaryReader(fs)).ReadBytes((int)fs.Length), 0, (int)fs.Length);
        }


    }
}
