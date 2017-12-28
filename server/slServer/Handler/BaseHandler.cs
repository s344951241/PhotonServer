using Common;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slServer.Handler
{
    public abstract class BaseHandler
    {
        public OperationCode OpCode;
        public abstract void OnPerationRequest(OperationRequest operationRequest, SendParameters sendParameters, TheClientPeer peer);
    }
}
