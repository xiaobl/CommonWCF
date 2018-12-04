using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IWebSocketsServer”。
    [ServiceContract(CallbackContract = typeof(IProgressContext))]
    public interface IWebSocketsServer
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void SendMessageToServer(Message msg);
    }

    [ServiceContract]
    interface IProgressContext
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ReportProgress(Message msg);
    }
}
