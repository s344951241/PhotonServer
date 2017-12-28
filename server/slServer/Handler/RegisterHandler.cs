using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using slServer.Tools;
using slServer.Manager;
using slServer.Model;

namespace slServer.Handler
{
    class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            OpCode = OperationCode.Register;
        }
        public override void OnPerationRequest(OperationRequest operationRequest, SendParameters sendParameters, TheClientPeer peer)
        {
            string name = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            UserManager manager = new UserManager();

            ReturnCode code;

            if (manager.GetByUsername(name) != null)
            {
                code = ReturnCode.Failed;
            }
            else
            {
                User user = new User() { Username = name,Password = password };
                //user.Username = name;
                //user.Password = password;
                manager.Add(user);
                code = ReturnCode.Success;
            }
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.ReturnCode = (short)code;
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
