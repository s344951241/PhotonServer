using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;

namespace slServer.Handler
{
    public class DefaultHandler : BaseHandler
    {
        public DefaultHandler()
        {
            OpCode = OperationCode.Default;
        }
        public override void OnPerationRequest(OperationRequest operationRequest, SendParameters sendParameters, TheClientPeer peer)
        {
            //throw new NotImplementedException();
        }
    }
}
