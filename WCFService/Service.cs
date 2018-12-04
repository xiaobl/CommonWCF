using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.WebSockets;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
    public delegate void DisPlayContent(LogInfo logInfo);
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“PersonInfo”。
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : ICommonRequest, IWebSocketsServer
    {
        public DisPlayContent AddConten = null;
        #region 常规请求
        /// <summary>
        /// 统一所有POST请求入口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public string HandleAllPostRequests(string type, string requestBody)
        {
            if (type.Equals(RequestType.Login.ToString(),StringComparison.InvariantCultureIgnoreCase))
            {
                return InterfaceHandler.Login(requestBody);
            }
            else
            {
                OperationContext context = OperationContext.Current;
                Task.Run(() =>
                {
                    LogInfo logInfo = new LogInfo();
                    logInfo.RequestType = type;
                    logInfo.RequestData = requestBody;
                    logInfo.RequestTime = DateTime.Now;
                    logInfo.ErrorMessage = "请求路径不存在";

                    logInfo.ClientInfo = InterfaceHandler.ClientInfo(context);
                    AddConten(logInfo);
                });
                return InterfaceHandler.ErrorRequest(400, "请求路径不存在");
            }
        }
        /// <summary>
        /// 统一所有GET请求入口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public string HandleAllGetRequests(string type, string requestBody)
        {
            Task.Run(() =>
            {
                JObject content = JObject.FromObject(new { date = DateTime.Now.ToString("o"), type, data = requestBody });
            });
            if (type.Equals(RequestType.Login.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return InterfaceHandler.Login(requestBody);
            }
            else
            {
                return InterfaceHandler.ErrorRequest(400, "请求路径不存在");
            }
        }   
        /// <summary>
        /// 统一所有上传文件入口
        /// </summary>
        /// <param name="getStream"></param>
        public string HandleAllUploadFile(Stream stream)
        {
            //MultipartFormDataParser 第三方库
            HttpMultipartParser.MultipartFormDataParser content = new HttpMultipartParser.MultipartFormDataParser(stream);
            try
            {
                using (FileStream fs = new FileStream(@"D:\" + content.Files[0].FileName, FileMode.OpenOrCreate))
                {
                    content.Files[0].Data.CopyTo(fs);
                    StreamReader sr = new StreamReader(fs);
                    fs.Flush();
                    return InterfaceHandler.UploadReturnInfo(true);
                }
            }
            catch (Exception e)
            {
                return InterfaceHandler.UploadReturnInfo(false);
            }
        }
        /// <summary>
        /// 统一所有下载文件入口
        /// </summary>
        /// <param name="downfile"></param>
        /// <returns></returns>
        public Stream HandleAllDownFile(string downfile)
        {
            string runDir = "d:\\";
            string filePath = System.IO.Path.Combine(runDir, downfile);
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                return fs;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region 常联接
        List<IChannel> callBacks = new List<IChannel>();
        List<IProgressContext> calls = new List<IProgressContext>();
        public void SendMessageToServer(Message msg)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IProgressContext>();
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            var ss = endpoint.Address + ":" + endpoint.Port.ToString();


            ReturnData();
            calls.Add(callback);
            callBacks.Add((IChannel)callback);
            if (msg.IsEmpty || ((IChannel)callback).State != CommunicationState.Opened)
            {
                return;
            }

            byte[] body = msg.GetBody<byte[]>();
            string msgTextFromClient = Encoding.UTF8.GetString(body);

            string msgTextToClient = string.Format(
                "Got message {0} at {1}",
                msgTextFromClient,
                DateTime.Now.ToLongTimeString());
            callback.ReportProgress(CreateMessage(msgTextToClient));

        }

        private async void ReturnData()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
                string str1 = File.ReadAllText(@"C:\Users\44632\Desktop\DaringTunnelV1File\methaneInfo.json", Encoding.UTF8);
                foreach (var item in calls)
                {
                    try
                    {
                        //item.ReportProgress(CreateMessage(JsonConvert.SerializeObject(new PersonInfoData() { Name = "xiaobenli" })));
                        item.ReportProgress(CreateMessage(str1));
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }
        private Message CreateMessage(string msgText)
        {
            Message msg = ByteStreamMessage.CreateMessage(new ArraySegment<byte>(Encoding.UTF8.GetBytes(msgText)));
            msg.Properties["WebSocketMessageProperty"] = new WebSocketMessageProperty { MessageType = WebSocketMessageType.Text };
            return msg;
        }


        public string postText()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    enum RequestType
    {
        Login=0
    }
    public static class InterfaceHandler
    {
        public static string Login(string body)
        {
            string str = File.ReadAllText(@"C:\Users\44632\Desktop\DaringTunnelV1File\login.json", Encoding.UTF8);
            return str;
        }
        public static string ErrorRequest(int errorData, string message)
        {
            return JsonConvert.SerializeObject(JObject.FromObject(new { code = errorData, data = message }));
        }
        public static string UploadReturnInfo(bool isSccess)
        {
            return JsonConvert.SerializeObject(JObject.FromObject(new { code = 200, data = "sccess" }));
        }
        public static string ClientInfo(OperationContext context)
        {
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return endpoint.Address + ":" + endpoint.Port.ToString();
        }
    }
    public class LogInfo: INotifyPropertyChanged
    {
        private string requestType;
        public string RequestType
        {
            get { return requestType; }
            set { requestType = value; OnPropertyChanged("RequestType"); }
        }

        private string requestData;
        public string RequestData
        {
            get { return requestData; }
            set { requestData = value; OnPropertyChanged("RequestData"); }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }

        private string clientInfo;
        public string ClientInfo
        {
            get { return clientInfo; }
            set { clientInfo = value; OnPropertyChanged("ClientInfo"); }
        }

        private DateTime requestTime;
        public DateTime RequestTime
        {
            get { return requestTime; }
            set { requestTime = value; OnPropertyChanged("RequestTime"); }
        }
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
