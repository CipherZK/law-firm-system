using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Dip.Forms
{
    public partial class gmail : Form
    {

        private bool active = false;
        private Thread listener = null;
        private long id = 0;
        private struct MyClient
        {
            public long id;
            public StringBuilder username;
            public TcpClient client;
            public NetworkStream stream;
            public byte[] buffer;
            public StringBuilder data;
            public EventWaitHandle handle;
        };
        private ConcurrentDictionary<long, MyClient> clients = new ConcurrentDictionary<long, MyClient>();
        private Task send = null;
        private Thread disconnect = null;
        private bool exit = false;

        public gmail()
        {
            InitializeComponent();
            
        }


        private void Log(string msg = "") // clear the log if message is not supplied or is empty
        {
            if (!exit)
            {
                logTextBox.Invoke((MethodInvoker)delegate
                {
                    if (msg.Length > 0)
                    {
                        logTextBox.AppendText(string.Format("[ {0} ] {1}{2}", DateTime.Now.ToString("HH:mm"), msg, Environment.NewLine));
                    }
                    else
                    {
                        logTextBox.Clear();
                    }
                });
            }
        }

        private string ErrorMsg(string msg)
        {
            return string.Format("ERROR: {0}", msg);
        }

        private string SystemMsg(string msg)
        {
            return string.Format("SYSTEM: {0}", msg);
        }

        private void Active(bool status)
        {
            if (!exit)
            {
                startButton.Invoke((MethodInvoker)delegate
                {
                    active = status;
                    if (status)
                    {
                        addrTextBox.Enabled = false;
                        portTextBox.Enabled = false;
                        usernameTextBox.Enabled = false;
                        keyTextBox.Enabled = false;
                        startButton.Text = "Stop";
                        Log(SystemMsg("Server has started"));
                    }
                    else
                    {
                        addrTextBox.Enabled = true;
                        portTextBox.Enabled = true;
                        usernameTextBox.Enabled = true;
                        keyTextBox.Enabled = true;
                        startButton.Text = "Start";
                        Log(SystemMsg("Server has stopped"));
                    }
                });
            }
           
        }

        private void AddToGrid(long id, string name)
        {
            if (!exit)
            {
                clientsDataGridView.Invoke((MethodInvoker)delegate
                {
                    string[] row = new string[] { id.ToString(), name };
                    clientsDataGridView.Rows.Add(row);
                    totalLabel.Text = string.Format("Total clients: {0}", clientsDataGridView.Rows.Count);
                });
            }
        }

        private void RemoveFromGrid(long id)
        {
            if (!exit)
            {
                clientsDataGridView.Invoke((MethodInvoker)delegate
                {
                    foreach (DataGridViewRow row in clientsDataGridView.Rows)
                    {
                        if (row.Cells["identifier"].Value.ToString() == id.ToString())
                        {
                            clientsDataGridView.Rows.RemoveAt(row.Index);
                            break;
                        }
                    }
                    totalLabel.Text = string.Format("Total clients: {0}", clientsDataGridView.Rows.Count);
                });
            }
        }

        private void Read(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
            int bytes = 0;
            if (obj.client.Connected)
            {
                try
                {
                    bytes = obj.stream.EndRead(result);
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
            if (bytes > 0)
            {
                obj.data.AppendFormat("{0}", Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                try
                {
                    if (obj.stream.DataAvailable)
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                    }
                    else
                    {
                        string msg = string.Format("{0}: {1}", obj.username, obj.data);
                        Log(msg);
                        Send(msg, obj.id);
                        obj.data.Clear();
                        obj.handle.Set();
                    }
                }
                catch (Exception ex)
                {
                    obj.data.Clear();
                    Log(ErrorMsg(ex.Message));
                    obj.handle.Set();
                }
            }
            else
            {
                obj.client.Close();
                obj.handle.Set();
            }
        }

        private void ReadAuth(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
            int bytes = 0;
            if (obj.client.Connected)
            {
                try
                {
                    bytes = obj.stream.EndRead(result);
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
            if (bytes > 0)
            {
                obj.data.AppendFormat("{0}", Encoding.UTF8.GetString(obj.buffer, 0, bytes));
                try
                {
                    if (obj.stream.DataAvailable)
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(ReadAuth), obj);
                    }
                    else
                    {
                        JavaScriptSerializer json = new JavaScriptSerializer(); // feel free to use JSON serializer
                        Dictionary<string, string> data = json.Deserialize<Dictionary<string, string>>(obj.data.ToString());
                        if (!data.ContainsKey("username") || data["username"].Length < 1 || !data.ContainsKey("key") || !data["key"].Equals(keyTextBox.Text))
                        {
                            obj.client.Close();
                        }
                        else
                        {
                            obj.username.Append(data["username"].Length > 200 ? data["username"].Substring(0, 200) : data["username"]);
                            Send("{\"status\": \"authorized\"}", obj);
                        }
                        obj.data.Clear();
                        obj.handle.Set();
                    }
                }
                catch (Exception ex)
                {
                    obj.data.Clear();
                    Log(ErrorMsg(ex.Message));
                    obj.handle.Set();
                }
            }
            else
            {
                obj.client.Close();
                obj.handle.Set();
            }
        }

        private bool Authorize(MyClient obj)
        {
            bool success = false;
            while (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(ReadAuth), obj);
                    obj.handle.WaitOne();
                    if (obj.username.Length > 0)
                    {
                        success = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
            return success;
        }

        private void Connection(MyClient obj)
        {
            if (Authorize(obj))
            {
                clients.TryAdd(obj.id, obj);
                AddToGrid(obj.id, obj.username.ToString());
                string msg = string.Format("{0} has connected", obj.username);
                Log(SystemMsg(msg));
                Send(SystemMsg(msg), obj.id);
                while (obj.client.Connected)
                {
                    try
                    {
                        obj.stream.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(Read), obj);
                        obj.handle.WaitOne();
                    }
                    catch (Exception ex)
                    {
                        Log(ErrorMsg(ex.Message));
                    }
                }
                obj.client.Close();
                clients.TryRemove(obj.id, out MyClient tmp);
                RemoveFromGrid(tmp.id);
                msg = string.Format("{0} has disconnected", tmp.username);
                Log(SystemMsg(msg));
                Send(msg, tmp.id);
            }
        }

        private void Listener(IPAddress ip, int port)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(ip, port);
                listener.Start();
                Active(true);
                while (active)
                {
                    if (listener.Pending())
                    {
                        try
                        {
                            MyClient obj = new MyClient();
                            obj.id = id;
                            obj.username = new StringBuilder();
                            obj.client = listener.AcceptTcpClient();
                            obj.stream = obj.client.GetStream();
                            obj.buffer = new byte[obj.client.ReceiveBufferSize];
                            obj.data = new StringBuilder();
                            obj.handle = new EventWaitHandle(false, EventResetMode.AutoReset);
                            Thread th = new Thread(() => Connection(obj))
                            {
                                IsBackground = true
                            };
                            th.Start();
                            id++;
                        }
                        catch (Exception ex)
                        {
                            Log(ErrorMsg(ex.Message));
                        }
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
                Active(false);
            }
            catch (Exception ex)
            {
                Log(ErrorMsg(ex.Message));
            }
            finally
            {
                if (listener != null)
                {
                    listener.Server.Close();
                }
            }
            list1 = new TcpListener(localAddr, port);
            //TcpListener list = new TcpListener(port);
            list1.Start();
            TcpClient client = list1.AcceptTcpClient();
            MessageBox.Show("Client trying to connect");
            StreamReader sr = new StreamReader(client.GetStream());
            rd = sr.ReadLine();
            v = rd.Substring(rd.LastIndexOf('.') + 1);
            //m = int.Parse(v);
            list1.Stop();
            client.Close();
        }


        private void Write(IAsyncResult result)
        {
            MyClient obj = (MyClient)result.AsyncState;
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.EndWrite(result);
                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
        }

        private void BeginWrite(string msg, MyClient obj) // send the message to a specific client
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            if (obj.client.Connected)
            {
                try
                {
                    obj.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), obj);

                }
                catch (Exception ex)
                {
                    Log(ErrorMsg(ex.Message));
                }
            }
        }

        private void BeginWrite(string msg, long id = -1) // send the message to everyone except the sender or set ID to lesser than zero to send to everyone
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            foreach (KeyValuePair<long, MyClient> obj in clients)
            {
                if (id != obj.Value.id && obj.Value.client.Connected)
                {
                    try
                    {
                        obj.Value.stream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(Write), obj.Value);

                    }
                    catch (Exception ex)
                    {
                        Log(ErrorMsg(ex.Message));
                    }
                }
            }
        }

        private void Send(string msg, MyClient obj)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => BeginWrite(msg, obj));

            }
            else
            {
                send.ContinueWith(antecendent => BeginWrite(msg, obj));
            }
        }

        private void Send(string msg, long id = -1)
        {
            if (send == null || send.IsCompleted)
            {
                send = Task.Factory.StartNew(() => BeginWrite(msg, id));
            }
            else
            {
                send.ContinueWith(antecendent => BeginWrite(msg, id));
            }
        }


        private void Disconnect(long id = -1) // disconnect everyone if ID is not supplied or is lesser than zero
        {
            if (disconnect == null || !disconnect.IsAlive)
            {
                disconnect = new Thread(() =>
                {
                    if (id >= 0)
                    {
                        clients.TryGetValue(id, out MyClient obj);
                        obj.client.Close();
                        RemoveFromGrid(obj.id);
                    }
                    else
                    {
                        foreach (KeyValuePair<long, MyClient> obj in clients)
                        {
                            obj.Value.client.Close();
                            RemoveFromGrid(obj.Value.id);
                        }
                    }
                })
                {
                    IsBackground = true
                };
                disconnect.Start();
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            //System.Threading.ThreadPool.QueueUserWorkItem(StartTcpServer);
            
            //start button
            if (active)
            {
                active = false;
               
            }
            else if (listener == null || !listener.IsAlive)
            {
                string address = addrTextBox.Text.Trim();
                string number = portTextBox.Text.Trim();
                string username = usernameTextBox.Text.Trim();
                bool error = false;
                IPAddress ip = null;
                if (address.Length < 1)
                {
                    error = true;
                    Log(SystemMsg("Address is required"));
                }
                else
                {
                    try
                    {
                        ip = Dns.Resolve(address).AddressList[0];
                    }
                    catch
                    {
                        error = true;
                        Log(SystemMsg("Address is not valid"));
                    }
                }
                int port = -1;
                if (number.Length < 1)
                {
                    error = true;
                    Log(SystemMsg("Port number is required"));
                }
                else if (!int.TryParse(number, out port))
                {
                    error = true;
                    Log(SystemMsg("Port number is not valid"));
                }
                else if (port < 0 || port > 65535)
                {
                    error = true;
                    Log(SystemMsg("Port number is out of range"));
                }
                if (username.Length < 1)
                {
                    error = true;
                    Log(SystemMsg("Username is required"));
                }
                if (!error)
                {
                    listener = new Thread(() => Listener(ip, port))
                    {
                        IsBackground = true
                    };
                    listener.Start();
                }

            }

            

        }
        private void StartTcpServer(string msg, MyClient obj)
        {

            
            //byte[] buffer = new byte[1500];
            //int bytesread = 1;

            //StreamWriter writer = new StreamWriter("C:\\Users\\Zhane\\Desktop\\Маг\\new.zip");  //"D:\\sample.rar");

            //while (bytesread > 0)
            //{
            //    bytesread = obj.client.GetStream().Read(buffer, 0, buffer.Length);
            //    if (bytesread == 0)
            //        break;
            //    writer.BaseStream.Write(buffer, 0, buffer.Length);
            //    Message(bytesread + " Received. ");
            //}
            //writer.Close();

        }
      
        public delegate void Add(String Message);

        private void button1_Click(object sender, EventArgs e)
        {
            Log();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void sendTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
           
          

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (sendTextBox.Text.Length > 0)
                {
                    string msg = sendTextBox.Text;
                    
                    sendTextBox.Clear();
                    Log(string.Format("{0} (You): {1}", usernameTextBox.Text.Trim(), msg));
                    Send(string.Format("{0}: {1}", usernameTextBox.Text.Trim(), msg));
                }
            }
        }

        private void checkBox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (keyTextBox.PasswordChar == '*')
            {
                keyTextBox.PasswordChar = '\0';
            }
            else
            {
                keyTextBox.PasswordChar = '*';
            }
        }

        private void clientsDataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == clientsDataGridView.Columns["dc"].Index)
            {
                long.TryParse(clientsDataGridView.Rows[e.RowIndex].Cells["identifier"].Value.ToString(), out long id);
                Disconnect(id);
            }
        }

        private void gmail_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
            active = false;
            Disconnect();
        }

        private void AttechFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            sendTextBox.Text = op.FileName;
        }

       
        
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        private void guna2Button2_Click(string directory, Stream stream)
        {
            //TcpListener filelistener = new TcpListener(ip, 9000);
            //filelistener.Start();
            //TcpClient client = filelistener.AcceptTcpClient();
            //Message("Client connection accepted from :" + client.Client.RemoteEndPoint + ".");
            //byte[] buffer = new byte[1500];
            //int bytesread = 1;

            //StreamWriter writer = new StreamWriter("C:\\Users\\Zhane\\Desktop\\Маг\\new.zip");  //"D:\\sample.rar");

            //while (bytesread > 0)
            //{
            //    bytesread = client.GetStream().Read(buffer, 0, buffer.Length);
            //    if (bytesread == 0)
            //        break;
            //    writer.BaseStream.Write(buffer, 0, buffer.Length);
            //    Message(bytesread + " Received. ");
            //}
            //writer.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Log();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Forms.sendMessage r = new Forms.sendMessage();
        
            r.ShowDialog();
    
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string path = "C:\\example.txt";
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                // файл не найден
                throw new IOException("File not found: " + path);
            }
            long length = file.Length; // длина файла в байтах
            string name = file.Name; // имя файла
            Encoding utf8 = new UTF8Encoding(false); // UTF-8 без BOM, самый стандартный стандарт из всех стандартов
            Stream stream = new MemoryStream();
            using (BinaryWriter bw = new BinaryWriter(stream, utf8, true))
            {
                bw.Write(length);
                bw.Write(name);
            }
            using (FileStream fs = file.OpenRead())
            {
                fs.CopyTo(stream);
            }
        }
        
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
            /*            Stream stream = new MemoryStream();

            Directory.CreateDirectory(directory); // создает каталог, если он не существует
            Encoding utf8 = new UTF8Encoding(false);
            long length;
            string name;
            using (BinaryReader br = new BinaryReader(stream, utf8, true))
            {
                length = br.ReadInt64();
                name = br.ReadString();
            }
            // Path.GetFileName для безопасности. Вдруг кто-то пришлет серверу полный путь,
            // чтобы он записал что-то в папку Windows или повредил свои файлы. Этого нельзя допустить.
            string path = Path.Combine(directory, Path.GetFileName(name));
            using (FileStream fs = File.Create(path))
            {
                byte[] buffer = new byte[1024];
                long received = 0;
                while (received < length)
                {
                    int toReceive = (int)Math.Min(buffer.Length, length - received);
                    int bytesReceived = stream.Read(buffer, 0, toReceive);
                    if (bytesReceived == 0) // Неожиданный конец потока
                        throw new IOException("Unexpected end of stream while receiving file: " + path);
                    received += bytesReceived;
                    fs.Write(buffer, 0, bytesReceived);
                }
            }*/

        }
        byte[] b1;
        OpenFileDialog op;
        string rd;
        string v;
        int m = 20000;//number of byts
        TcpListener list1;
        TcpClient client1;
        int port = 5010;//5050
        int port1 = 5010;//5055
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        private void btnSend_Click(object sender, EventArgs e)
        {
            
            
           
        }
        private void ic()
        {

            //FileStream fs = new FileStream(@"\Documents\testfile.pdf", FileMode.Open);
                client1 = list1.AcceptTcpClient();
            Stream s = client1.GetStream();
            b1 = new byte[m];
            s.Read(b1, 0, b1.Length);
            MessageBox.Show("path: C:/Users/Zhane/Downloads", "File received");
            File.WriteAllBytes("C:/Users/Zhane/Downloads/Advokat.txt", b1);// the left side us the name of the written file
                                                                       //list.Stop();
                                                                       //client.Close();
                                                                       //  label1.Text = "File Received......";
        }

        private void gmail_Load(object sender, EventArgs e)
        {
            

                //textBox1.Text = folderBrowserDialog1.SelectedPath;
                list1 = new TcpListener(localAddr, port1);
                list1.Start();

                Thread incoming_connection = new Thread(ic);
                incoming_connection.Start();

            
        }
    }
}
