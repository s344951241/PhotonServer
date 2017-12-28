using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using slServer.Tools;
using slServer.Manager;

namespace slServer.Handler
{
    public class LoginHandler : BaseHandler
    {
        public LoginHandler()
        {
            OpCode = OperationCode.Login;
        }
        public override void OnPerationRequest(OperationRequest operationRequest, SendParameters sendParameters, TheClientPeer peer)
        {
            string name = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            UserManager manager = new UserManager();
            bool result = manager.VerifyUser(name, password);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if (result)
            {
                SLServer.log.Info("username and password right");
                response.ReturnCode = (short)ReturnCode.Success;
                ((TheClientPeer)peer).Username = name; 
            }
            else
            {
                SLServer.log.Warn("username and password error");
                response.ReturnCode = (short)ReturnCode.Failed;
            }
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
