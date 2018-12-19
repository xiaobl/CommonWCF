using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IPersonInfo”。
    [ServiceContract]
    public interface ICommonRequest
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "{type}?")]
        Message HandleAllPostRequests(string type);

        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "{type}?")]
        Message HandleAllGetRequests(string type);

        [WebInvoke(Method = "*", UriTemplate = "uploadFile", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [System.ComponentModel.Description("上传文件")]
        [OperationContract]
        string HandleAllUploadFile(Stream stream);

        [OperationContract]
        [System.ComponentModel.Description("下载文件")]
        [WebGet(UriTemplate = "show/{downfile}",BodyStyle =WebMessageBodyStyle.WrappedResponse)]
        Stream HandleAllShow(string downfile);

        [OperationContract]
        [WebGet(UriTemplate = "downFile/{fileName}")]
        Message HandleAllDownFile(string fileName);
    }
}
