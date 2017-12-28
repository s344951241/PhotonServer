using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotonHostRuntimeInterfaces;
using slServer.Handler;
using slServer.Tools;
using Common;

namespace slServer
{
    public class TheClientPeer : ClientPeer
    {
        public string Username;
        public Vector3Data Pos;
        public TheClientPeer(InitRequest initRequest) : base(initRequest)
        {
           
        }


        //断开链接的处理
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            SLServer.log.Info(reasonDetail);
            SLServer.instance.PeerList.Remove(this);
        }
        //处理客户端请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            BaseHandler handle = DicTool.GetValue<OperationCode, BaseHandler>(SLServer.instance.DicHandler, (OperationCode)operationRequest.OperationCode);
            if (handle == null)
            {
                handle = DicTool.GetValue<OperationCode, BaseHandler>(SLServer.instance.DicHandler, OperationCode.Default);
            }
            handle.OnPerationRequest(operationRequest, sendParameters, this);

            //switch (operationRequest.OperationCode)
            //{
            //    case 1:
            //        Dictionary<byte, object> dic = operationRequest.Parameters;
            //        SLServer.log.Info("收到客户端协议:1参数:" + Convert.ToInt32(dic[1])+Convert.ToString(dic[2]));

            //        OperationResponse response = new OperationResponse(1);
            //        Dictionary<byte, object> param = new Dictionary<byte, object>();
            //        param.Add(1, 101);
            //        param.Add(2, "服务端");
            //        response.SetParameters(param);
            //        SendOperationResponse(response, sendParameters);

            //        EventData data = new EventData();
            //        data.Code = 1;
            //        Dictionary<byte, object> dic1 = new Dictionary<byte, object>();
            //        dic1.Add(1, "连接服务器成功返回");
            //        data.SetParameters(dic1);
            //        SendEvent(data, new SendParameters());
            //        break;
            //}
        }
    }
}
