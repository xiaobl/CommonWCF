using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IPersonInfo”。
    [ServiceContract]
    public interface ICommonRequest
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "post/{type}?requestBody={requestBody}")]
        string HandleAllPostRequests(string type, string requestBody);
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get/{type}?requestBody={requestBody}")]
        string HandleAllGetRequests(string type, string requestBody);
        [WebInvoke(Method = "*", UriTemplate = "upLoadFile", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [System.ComponentModel.Description("上传文件")]
        [OperationContract]
        string HandleAllUploadFile(Stream stream);
        [OperationContract]
        [System.ComponentModel.Description("下载文件")]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "downFile/{downfile}")]
        Stream HandleAllDownFile(string downfile);
    }
}
